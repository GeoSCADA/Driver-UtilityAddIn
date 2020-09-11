Imports System
Imports System.IO
Imports ClearSCADA.DBObjFramework
Imports AddIn.Globals

<Table("2D Lookup", "Lookup")>
Public Class TwoDLookup
    Inherits DBObject

    ' Array of X values (in/out)
    <DataField("X", "Array of X Values.", OPC.OPC_BASE + 350)>
    Public X(-1) As Double

    ' Array of Y values (in/out)
    <DataField("Y", "Array of Y Values.", OPC.OPC_BASE + 351)>
    Public Y(-1) As Double

    ' Count of value pairs - minimum of X and Y sizes
    <DataField("ValueCount", "Count of Values.", OPC.OPC_BASE + 352, [ReadOnly]:=True, ViewInfoTitle:="Count of value pairs")>
    Public ReadOnly Property ValueCount As Integer
        Get
            If (X.Length <= Y.Length) Then
                Return X.Length
            Else
                Return Y.Length
            End If
        End Get
    End Property

    ' First pair - as an array of two values.
    <DataField("FirstValuePair", "X and Y values of first pair.", OPC.OPC_BASE + 355, [ReadOnly]:=True, ViewInfoTitle:="First value pair")>
    Public ReadOnly Property FirstValuePair As String
        Get
            If (X.Length >= 1 And Y.Length >= 1) Then
                Return X(0).ToString & "," & Y(0).ToString
            Else
                Return ""
            End If
        End Get
    End Property

    ' Last pair - as an array of two values
    <DataField("LastValuePair", "X and Y values of last pair.", OPC.OPC_BASE + 356, [ReadOnly]:=True, ViewInfoTitle:="Last value pair")>
    Public ReadOnly Property LastValuePair As String
        Get
            If (X.Length >= 1 And Y.Length >= 1) Then
                Return X(ValueCount - 1).ToString & "," & Y(ValueCount - 1).ToString
            Else
                Return ""
            End If
        End Get
    End Property

    ' Stepped or Linear
    <Label("Interpolation", 1, 1)>
    <ConfigField("Interpolation", "Interpolation Type.", 1, 2, OPC.OPC_BASE + 357)>
    <[Enum](New String() {"Linear", "Step First", "Step Last"})>
    Public Interpolation As Byte

    Public Overrides Function MethodAvailable(UserCtx As UserContext, MethodName As String) As Boolean
        Dim MParam(0) As String
        MParam(0) = "CFG" 'Configuration
        Dim ConfigAccess As Boolean = CBool(Me.Method("CheckAccess", MParam))
        Select Case MethodName
            Case "ClearValues"
                Return ConfigAccess
            Case "AddValuePair"
                Return ConfigAccess
            Case "LoadXValues"
                Return ConfigAccess
            Case "LoadYValues"
                Return ConfigAccess
            Case "ImportFromFile"
                Return ConfigAccess
            Case Else
                Return MyBase.MethodAvailable(UserCtx, MethodName)
        End Select
    End Function

    ' Clear Values
    <Method("Clear Values", "Clear all values from this array.", OPC.OPC_BASE + 358)>
    Public Sub ClearValues()
        ReDim X(-1)
        ReDim Y(-1)
    End Sub

    ' Add value pair
    <Method("Add Value Pair", "Add a pair of XY values to this array.", OPC.OPC_BASE + 359)>
    Public Sub AddValuePair(<Argument("X Value", "Floating point value")> ByVal Xval As Double, <Argument("Y Value", "Floating point value")> ByVal Yval As Double)
        Dim l As Integer = ValueCount
        ReDim Preserve X(l)
        ReDim Preserve Y(l)
        X(l) = Xval
        Y(l) = Yval
        ' Not currently sorted
    End Sub

    ' Set X Values from CSV string
    <Method("Load X Values", "Load multiple X values from a string of comma or tab separated values. Data is cleared first.", OPC.OPC_BASE + 360)>
    Public Sub LoadXValues(<Argument("Values", "List of comma or tab separated values")> ByVal ValuesText As String)
        ReDim X(-1)
        Dim Values() As String
        Dim l As Integer
        Dim Xval As Double
        If ValuesText.Contains(",") Then
            Values = Split(ValuesText, ",")
        Else
            Values = Split(ValuesText, vbTab)
        End If
        For Each V As String In Values
            ' Check number
            If Double.TryParse(V, Xval) Then
                l = X.Length
                ReDim Preserve X(l)
                X(l) = Xval
            End If
        Next
        ' Not currently sorted
    End Sub

    ' Set Y Values from CSV string
    <Method("Load Y Values", "Load multiple Y values from a string of comma or tab separated values. Data is cleared first.", OPC.OPC_BASE + 361)>
    Public Sub LoadYValues(<Argument("Values", "List of comma or tab separated values")> ByVal ValuesText As String)
        ReDim Y(-1)
        Dim Values() As String
        Dim l As Integer
        Dim Yval As Double
        If ValuesText.Contains(",") Then
            Values = Split(ValuesText, ",")
        Else
            Values = Split(ValuesText, vbTab)
        End If
        For Each V As String In Values
            ' Check number
            If Double.TryParse(V, Yval) Then
                l = Y.Length
                ReDim Preserve Y(l)
                Y(l) = Yval
            End If
        Next
        ' Not currently sorted
    End Sub

    ' Set Values from CSV file
    <Method("Import from File", "Clear and load multiple pairs of XY values to this array from a file of comma-separated values with new lines between pairs. File must be on Main server.", OPC.OPC_BASE + 362)>
    Public Sub ImportFromFile(<Argument("File Name", "Path to CSV File. Must be on server.")> ByVal Filename As String)
        Try
            Using sr As StreamReader = New StreamReader(Filename)
                ReDim X(-1)
                ReDim Y(-1)

                While Not sr.EndOfStream
                    Dim Line As String = sr.ReadLine
                    Dim Values() As String
                    If Line.Contains(",") Then
                        Values = Split(Line, ",")
                    Else
                        Values = Split(Line, vbTab)
                    End If
                    If Values.Length = 2 Then
                        Dim l As Integer
                        Dim Xval As Double
                        Dim Yval As Double
                        If Double.TryParse(Values(0), Xval) And Double.TryParse(Values(1), Yval) Then
                            l = ValueCount
                            ReDim Preserve X(l)
                            ReDim Preserve Y(l)
                            X(l) = Xval
                            Y(l) = Yval
                        End If
                    End If
                End While
            End Using
            ' Not currently sorted

        Catch ex As Exception
            'Cannot read file
        End Try
    End Sub

    ' Lookup (X) to return Y
    <Method("Look up Y value from X", "Get a Y value from an input X value.", OPC.OPC_BASE + 363)>
    Public Function LookupYfromX(<Argument("X Value", "Look up Y from this X.")> ByVal Xval As Double) As Double
        ' If no values
        Dim l As Integer = ValueCount
        If l = 0 Then
            'Return a zero value if there are no pairs
            Return 0
        End If
        If l = 1 Then
            'Return the only Y value if there is only one pair
            Return Y(0)
        End If
        'Less than lowest
        If (Xval <= X(0)) Then
            Return Y(0)
        End If
        'Higher than highest
        If (Xval >= X(l - 1)) Then
            Return Y(l - 1)
        End If
        Select Case Interpolation
            Case 0 'Interpolated
                'Find first X value greater than Y, if there is one
                For i As Integer = 0 To l - 2
                    If Xval <= X(i + 1) Then
                        'Value falls between i-1 and i
                        'Check div zero
                        If (X(i + 1) - X(i) = 0) Then
                            Return Y(i)
                        End If
                        'Interpolate
                        Return Y(i) + (Xval - X(i)) * (Y(i + 1) - Y(i)) / (X(i + 1) - X(i))
                        Exit For
                    End If
                Next
            Case 1 'Step First
                'Find first X value greater than Y, if there is one
                For i As Integer = 0 To ValueCount - 2
                    If Xval <= X(i + 1) Then
                        'Value falls between i-1 and i
                        Return Y(i + 1)
                        Exit For
                    End If
                Next
            Case 2 'Step Last
                'Find first X value greater than Y, if there is one
                For i As Integer = 0 To ValueCount - 2
                    If Xval <= X(i + 1) Then
                        'Value falls between i-1 and i
                        Return Y(i)
                        Exit For
                    End If
                Next
        End Select
    End Function

    ' Lookup (Y) to return X

End Class

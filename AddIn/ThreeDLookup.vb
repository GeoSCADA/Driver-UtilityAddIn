Imports System
Imports System.IO
Imports ClearSCADA.DBObjFramework
Imports AddIn.Globals

<Table("3D Lookup", "Lookup")>
Public Class ThreeDLookup
    Inherits DBObject

    ' Array of X values (in/out)
    <DataField("X", "Array of X Values.", OPC.OPC_BASE + 360)>
    Public X(-1) As Double

    ' Array of Y values (in/out)
    <DataField("Y", "Array of Y Values.", OPC.OPC_BASE + 361)>
    Public Y(-1) As Double

    ' 2D Array of Z values (in/out)
    <DataField("Z", "Two Dimensional Array of Z Values.", OPC.OPC_BASE + 362)>
    Public Z(-1, -1) As Double

    ' Count of X values
    <DataField("XValueCount", "Count of X Values.", OPC.OPC_BASE + 363, [ReadOnly]:=True, ViewInfoTitle:="Count of X values")>
    Public ReadOnly Property XValueCount As Integer
        Get
            Return X.Length
        End Get
    End Property

    ' Count of Y values
    <DataField("YValueCount", "Count of Y Values.", OPC.OPC_BASE + 364, [ReadOnly]:=True, ViewInfoTitle:="Count of Y values")>
    Public ReadOnly Property YValueCount As Integer
        Get
            Return Y.Length
        End Get
    End Property

    ' Size of Z values in X direction
    <DataField("ZSizeInXDir", "Size of Z array in X Direction.", OPC.OPC_BASE + 365, [ReadOnly]:=True, ViewInfoTitle:="Size of Z array in X direction")>
    Public ReadOnly Property ZSizeInXDir As Integer
        Get
            Return Z.GetUpperBound(0) + 1
        End Get
    End Property

    ' Size of Z values in Y direction
    <DataField("ZSizeInYDir", "Size of Z array in Y Direction.", OPC.OPC_BASE + 366, [ReadOnly]:=True, ViewInfoTitle:="Size of Z array in Y direction")>
    Public ReadOnly Property ZSizeInYDir As Integer
        Get
            Return Z.GetUpperBound(1) + 1
        End Get
    End Property

    ' Validity of the data - has to have at least one value and dimensions must match
    <DataField("DataIsValid", "True if data sizes match up.", OPC.OPC_BASE + 367, [ReadOnly]:=True, ViewInfoTitle:="Data is valid")>
    Public ReadOnly Property DataIsValid As Boolean
        Get
            If X.Length = ZSizeInXDir And Y.Length = ZSizeInYDir And X.Length >= 1 And Y.Length >= 1 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ' First values - as string of comma sep values.
    <DataField("FirstValue", "X, Y and Z values of first value.", OPC.OPC_BASE + 368, [ReadOnly]:=True, ViewInfoTitle:="First value set")>
    Public ReadOnly Property FirstValue As String
        Get
            If DataIsValid Then
                Return X(0).ToString & "," & Y(0).ToString & "," & Z(0, 0)
            Else
                Return ""
            End If
        End Get
    End Property

    ' Last values - as string of comma sep values.
    <DataField("LastValue", "X, Y and Z values of last value.", OPC.OPC_BASE + 369, [ReadOnly]:=True, ViewInfoTitle:="Last value set")>
    Public ReadOnly Property LastValue As String
        Get
            If DataIsValid Then
                Return X(X.Length - 1).ToString & "," & Y(Y.Length - 1).ToString & "," & Z(X.Length - 1, Y.Length - 1)
            Else
                Return ""
            End If
        End Get
    End Property

    ' Stepped or Linear
    <Label("Interpolation", 1, 1)>
    <ConfigField("Interpolation", "Interpolation Type.", 1, 2, OPC.OPC_BASE + 370)>
    <[Enum](New String() {"Linear", "Step First", "Step Last"})>
    Public Interpolation As Byte

    Public Overrides Function MethodAvailable(UserCtx As UserContext, MethodName As String) As Boolean
        Dim MParam(0) As String
        MParam(0) = "CFG" 'Configuration
        Dim ConfigAccess As Boolean = CBool(Me.Method("CheckAccess", MParam))
        Select Case MethodName
            Case "ClearValues"
                Return ConfigAccess
            Case "LoadXValues"
                Return ConfigAccess
            Case "LoadYValues"
                Return ConfigAccess
            Case "SetZValue"
                Return ConfigAccess
            Case "ImportZFromFile"
                Return ConfigAccess
            Case "ImportXYZFromFile"
                Return ConfigAccess
            Case Else
                Return MyBase.MethodAvailable(UserCtx, MethodName)
        End Select
    End Function

    ' Clear Values
    <Method("Clear Values", "Clear all values from this array.", OPC.OPC_BASE + 371)>
    Public Sub ClearValues()
        ReDim X(-1)
        ReDim Y(-1)
        ReDim Z(-1, -1)
    End Sub

    ' Set X Values from CSV string
    <Method("Load X Values", "Load multiple X values from a string of comma or tab separated values. X values are cleared first.", OPC.OPC_BASE + 372)>
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
    <Method("Load Y Values", "Load multiple Y values from a string of comma or tab separated values. Y values are cleared first.", OPC.OPC_BASE + 373)>
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

	' Set Z Values from entered values
	<Method("Set Z Value", "Set a Z value based on its indexed X,Y position (first item is 0,0). Z data is first cleared if X or Y sizes are changed.", OPC.OPC_BASE + 374)>
	Public Sub SetZValue(<Argument("Xindex", "X index")> ByVal Xindex As Integer, <Argument("Yindex", "Y index")> ByVal Yindex As Integer, <Argument("Zvalue", "Z value")> ByVal Zvalue As Double)
        If (Xindex > XValueCount - 1 Or Yindex > YValueCount - 1 Or Xindex < 0 Or Yindex < 0) Then
            ' Out of range. Ignore
        Else
            ' If Z array is not the right size then recreate it
            If (XValueCount <> ZSizeInXDir Or YValueCount <> ZSizeInYDir) Then
                ReDim Z(XValueCount - 1, YValueCount - 1)
            End If
            Z(Xindex, Yindex) = Zvalue
        End If
    End Sub

    ' Set Values from CSV file
    <Method("Import Z from File", "Clear and load array of Z values to this array from a file of comma-separated values (X) with new lines between lines (Y). File must be on Main server.", OPC.OPC_BASE + 375)>
    Public Sub ImportZFromFile(<Argument("File Name", "Path to CSV File. Must be on server.")> ByVal Filename As String)
        Try
            Using sr As StreamReader = New StreamReader(Filename)
                ' If Z array is not the right size then recreate it
                If (XValueCount <> ZSizeInXDir Or YValueCount <> ZSizeInYDir) Then
                    ReDim Z(XValueCount - 1, YValueCount - 1)
                End If

                Dim Xindex As Integer = 0
                Dim Yindex As Integer = 0
                While Not sr.EndOfStream
                    Dim Line As String = sr.ReadLine
                    Dim Values() As String
                    If Line.Contains(",") Then
                        Values = Split(Line, ",")
                    Else
                        Values = Split(Line, vbTab)
                    End If
                    If Values.Length > 0 Then
                        For Each s As String In Values
                            Dim Zval As Double
                            If Double.TryParse(s, Zval) Then
                                ' Limit check
                                If (Xindex < ZSizeInXDir) Then
                                    If (Yindex < ZSizeInYDir) Then
                                        Z(Xindex, Yindex) = Zval
                                    End If
                                End If
                            End If
                            Xindex = Xindex + 1
                        Next
                    End If
                    Yindex = Yindex + 1
                    Xindex = 0
                End While
            End Using

        Catch ex As Exception
            'Cannot read file
        End Try
    End Sub

    ' Set Values from CSV file
    <Method("Import XYZ from File", "Clear and load array of XYZ values to this array from a file of comma-separated values with first line as X values and first column as Y values. Value at (0,0) is ignored. File must be on Main server.", OPC.OPC_BASE + 376)>
    Public Sub ImportXYZFromFile(<Argument("File Name", "Path to CSV File. Must be on server.")> ByVal Filename As String)
        Try
            ' Two passes. First resolve X and Y arrays, then import Z
            Using sr As StreamReader = New StreamReader(Filename)
                ReDim X(-1)
                ReDim Y(-1)
                Dim Xindex As Integer = -1
                Dim Yindex As Integer = -1
                While Not sr.EndOfStream
                    Dim Line As String = sr.ReadLine
                    Dim Values() As String
                    If Line.Contains(",") Then
                        Values = Split(Line, ",")
                    Else
                        Values = Split(Line, vbTab)
                    End If
                    If Values.Length > 0 Then
                        For Each s As String In Values
                            Dim Zval As Double
                            If Double.TryParse(s, Zval) Then
                                If (Yindex = -1) Then 'First Line is the X values
                                    If (Xindex > -1) Then 'Ignore first X value
                                        ReDim Preserve X(Xindex)
                                        X(Xindex) = Zval
                                    End If
                                Else
                                    If (Xindex = -1) Then 'First column are Y values
                                        ReDim Preserve Y(Yindex)
                                        Y(Yindex) = Zval
                                    End If
                                End If
                            End If
                            Xindex = Xindex + 1
                        Next
                    End If
                    Yindex = Yindex + 1
                    Xindex = -1
                End While
            End Using
            ' Size Z
            ' If Z array is not the right size then recreate it
            If (XValueCount <> ZSizeInXDir Or YValueCount <> ZSizeInYDir) Then
                ReDim Z(XValueCount - 1, YValueCount - 1)
            End If
            ' Import Z
            Using sr As StreamReader = New StreamReader(Filename)
                Dim Xindex As Integer = -1
                Dim Yindex As Integer = -1
                While Not sr.EndOfStream
                    Dim Line As String = sr.ReadLine
                    Dim Values() As String
                    If Line.Contains(",") Then
                        Values = Split(Line, ",")
                    Else
                        Values = Split(Line, vbTab)
                    End If
                    If Values.Length > 0 Then
                        For Each s As String In Values
                            Dim Zval As Double
                            If Double.TryParse(s, Zval) Then
                                If (Yindex = -1) Then 'First Line is the X values
                                Else
                                    If (Xindex = -1) Then 'First column are Y values
                                    Else
                                        ' Limit check
                                        If (Xindex < ZSizeInXDir) Then
                                            If (Yindex < ZSizeInYDir) Then
                                                Z(Xindex, Yindex) = Zval
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            Xindex = Xindex + 1
                        Next
                    End If
                    Yindex = Yindex + 1
                    Xindex = -1
                End While
            End Using
        Catch ex As Exception
            'Cannot read file
        End Try
    End Sub

    ' Lookup (X) to return Y
    <Method("Look up Z value from X and Y", "Get a Z value from input X and Y values.", OPC.OPC_BASE + 377)>
    Public Function LookupZfromXY(<Argument("X Value", "Look up Z from this X.")> ByVal Xval As Double, <Argument("Y Value", "Look up Z from this Y.")> ByVal Yval As Double) As Double
        If (DataIsValid) Then

            Dim Xindex As Integer
            Dim Xnextindex As Integer

            If XValueCount = 1 Then
                'Only one value
                Xindex = 0
                Xnextindex = 0
                'Less than lowest
            ElseIf (Xval <= X(0)) Then
                Xindex = 0
                Xnextindex = 0
                'Higher than highest
            ElseIf (Xval >= X(XValueCount - 1)) Then
                Xindex = XValueCount - 1
                Xnextindex = XValueCount - 1
            Else
                For i As Integer = 0 To XValueCount - 2
                    If Xval <= X(i + 1) Then
                        'Value falls between i and i+1
                        Select Case Interpolation
                            Case 0 'Interpolated
                                Xindex = i
                                Xnextindex = i + 1
                            Case 1 'Step First
                                Xindex = i + 1
                                Xnextindex = i + 1
                            Case 2 'Step Last
                                Xindex = i
                                Xnextindex = i
                        End Select
                        Exit For
                    End If
                Next
            End If

            Dim Yindex As Integer
            Dim Ynextindex As Integer

            If YValueCount = 1 Then
                'Only one value
                Yindex = 0
                Ynextindex = 0
                'Less than lowest
            ElseIf (Yval <= Y(0)) Then
                Yindex = 0
                Ynextindex = 0
                'Higher than highest
            ElseIf (Yval >= Y(YValueCount - 1)) Then
                Yindex = YValueCount - 1
                Ynextindex = YValueCount - 1
            Else
                For i As Integer = 0 To YValueCount - 2
                    If Yval <= Y(i + 1) Then
                        'Value falls between i and i+1
                        Select Case Interpolation
                            Case 0 'Interpolated
                                Yindex = i
                                Ynextindex = i + 1
                            Case 1 'Step First
                                Yindex = i + 1
                                Ynextindex = i + 1
                            Case 2 'Step Last
                                Yindex = i
                                Ynextindex = i
                        End Select
                        Exit For
                    End If
                Next
            End If

            ' Work out the proportion of X direction
            Dim Xproportion As Double = 0
            If Xindex <> Xnextindex Then
                'Check div zero
                If X(Xnextindex) - X(Xindex) <> 0 Then
                    Xproportion = (Xval - X(Xindex)) / (X(Xnextindex) - X(Xindex))
                End If
            End If
            Dim XproportionM As Double = 1 - Xproportion

            ' Work out the proportion of Y direction
            Dim Yproportion As Double = 0
            If Yindex <> Ynextindex Then
                'Check div zero
                If Y(Ynextindex) - Y(Yindex) <> 0 Then
                    Yproportion = (Yval - Y(Yindex)) / (Y(Ynextindex) - Y(Yindex))
                End If
            End If
            Dim YproportionM As Double = 1 - Yproportion

            ' Z is the proportion of all 4 surrounding values
            Dim Zval As Double = 0
            Zval = Z(Xindex, Yindex) * (XproportionM * YproportionM) +
                Z(Xindex + 1, Yindex) * (Xproportion * YproportionM) +
                Z(Xindex, Yindex + 1) * (XproportionM * Yproportion) +
                Z(Xindex + 1, Yindex + 1) * (Xproportion * Yproportion)
            Return Zval
        Else
            Return vbNull
        End If
    End Function

End Class

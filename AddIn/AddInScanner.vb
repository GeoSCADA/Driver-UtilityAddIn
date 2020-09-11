Imports ClearSCADA.DBObjFramework
Imports ClearSCADA.DBObjFramework.PointSourceEntry
Imports AddIn.Globals


<Table("AddIn Scanner", "Scanner")>
Public Class AddInScanner
    Inherits Scanner

    <Label("Enabled", 1, 1)>
    <ConfigField("In Service", "Controls whether scanner is active.", 1, 2, &H350501B)>
    Public Overrides Property InService() As Boolean
        Get
            Return MyBase.InService
        End Get
        Set(ByVal value As Boolean)
            MyBase.InService = value
        End Set
    End Property


    ' The severity used to log alarms and events
    <Label("Severity", 3, 1)> _
    <ConfigField("Severity", "The severity used to log alarms and events", 3, 2, &H350501C)> _
    Public Overrides Property Severity() As UShort
        Get
            Return MyBase.Severity
        End Get
        Set(ByVal value As UShort)
            MyBase.Severity = value
        End Set
    End Property

    ' Scan rate between scans
    <Label("Scan Rate", 4, 1)> _
    <ConfigField("ScanRate", "The interval between calculation updates", 4, 2, &H3505045)> _
    <Interval(IntervalType.Seconds)> _
    Public NormalScanRate As UInt32 = 3600

    ' The scan offset
    <Label("Scan Offset", 5, 1)> _
    <ConfigField("NormalScanOffset", "The calculation update offset", 5, 2, &H350504C, Length:=10)> _
    Public NormalScanOffset As String = "H"

    Private Function SendDriverAction(ByVal Action As UInteger, ByVal Parameters() As Object, ByVal Description As String) As Boolean
        Try
            If Description.Length > 63 Then
                ' Will fail if the description is too long!
                Description = Left(Description, 63)
            End If

            [Module].Log("SendDriverAction: Sending Action: " & Action)
            DriverAction(Action, Parameters, Description)
        Catch ex As Exception
            [Module].Log("SendDriverAction: Failed " & ex.Message)
        End Try
    End Function


    '***********************************************************************************
    'Validation
    Public Overrides Sub OnValidateConfig(ByVal Errors As ClearSCADA.DBObjFramework.MessageInfo)

        MyBase.OnValidateConfig(Errors)
    End Sub

    '***********************************************************************************
    'Send Receive
    Public Overrides Sub OnReceive(Type As UInteger, Data As Object, ByRef Reply As Object)
        If (Type = OPC.OPC_AddIn_GetTariffValueNow) Then
            ' For the point id in Data, return the current Tariff Value
            Dim PointId As UInt32
            PointId = DirectCast(Data, UInt32)
            Dim p As DBObject
            p = Scanner.GetObject(PointId)
            Dim TariffPoint As TariffAnalogue
            TariffPoint = TryCast(p, TariffAnalogue)
            If (Not TariffPoint Is Nothing) Then
                Reply = TariffPoint.GetValueNow
            End If
        Else
                MyBase.OnReceive(Type, Data, Reply)
        End If
    End Sub


End Class


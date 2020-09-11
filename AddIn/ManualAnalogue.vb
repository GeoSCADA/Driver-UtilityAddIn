Imports System.Threading
Imports ClearSCADA.DBObjFramework
Imports ClearSCADA.DBObjFramework.PointSourceEntry
Imports AddIn.Globals

<Table("Manual Analogue", "Manual")>
Public Class ManualAnalogue
    Inherits AnaloguePoint

    '***********************************************************************************
    'Enable all flags, as the prime purpose of this point is to log data
    Public Sub New()
        MyBase.HistoricFilter(Reason.ValueChange) = True
        MyBase.HistoricFilter(Reason.StateChange) = True
        MyBase.HistoricFilter(Reason.Report) = True
        MyBase.HistoricFilter(Reason.EndofPeriod) = True
    End Sub

    '***********************************************************************************
    'Database Fields
    <Label("Enabled", 1, 1)>
    <ConfigField("In Service", "Controls whether point is active.", 1, 2, &H350501B)>
    Public Overrides Property InService() As Boolean
        Get
            Return MyBase.InService
        End Get
        Set(ByVal value As Boolean)
            MyBase.InService = value
        End Set
    End Property

    <Label("Scanner", 2, 1)>
    <ConfigField("Scanner", "Scanner", 2, 2, &H350532F)>
    Public Shadows Property ScannerId() As Reference(Of AddInScanner)
        Get
            Return New Reference(Of AddInScanner)(MyBase.ScannerId)
        End Get
        Set(ByVal value As Reference(Of AddInScanner))
            MyBase.ScannerId = New Reference(Of Scanner)(value)
        End Set
    End Property

    'This section is mandatory and controls the history logged when a point value is updated
    <Label("Historic Data Filter", 7, 1, Width:=4, Height:=2)>
    <Label("Significant Change", 8, 1)>
    <ConfigField("Significant Change Historic Filter", "Controls whether Value Changes are logged historically", 8, 2, &H3506913)>
    Public Property HistoricFilterValue() As Boolean
        Get
            Return MyBase.HistoricFilter(Reason.ValueChange)
        End Get
        Set(ByVal value As Boolean)
            MyBase.HistoricFilter(Reason.ValueChange) = value
        End Set
    End Property

    <Label("State Change", 9, 1)>
    <ConfigField("State Change Historic Filter", "Controls whether State Changes are logged historically", 9, 2, &H3506914)>
    Public Property HistoricFilterState() As Boolean
        Get
            Return MyBase.HistoricFilter(Reason.StateChange)
        End Get
        Set(ByVal value As Boolean)
            MyBase.HistoricFilter(Reason.StateChange) = value
        End Set
    End Property

    <Label("Report", 8, 3)>
    <ConfigField("Report Historic Filter", "Controls whether Timed Report values are logged historically", 8, 4, &H3506915)>
    Public Property HistoricFilterReport() As Boolean
        Get
            Return MyBase.HistoricFilter(Reason.Report)
        End Get
        Set(ByVal value As Boolean)
            MyBase.HistoricFilter(Reason.Report) = value
        End Set
    End Property

    <Label("End Of Period", 9, 3)>
    <ConfigField("EOP Historic Filter", "Controls whether EOP values are logged historically", 9, 4, &H3506916)>
    Public Property HistoricFilterEOP() As Boolean
        Get
            Return MyBase.HistoricFilter(Reason.EndofPeriod)
        End Get
        Set(ByVal value As Boolean)
            MyBase.HistoricFilter(Reason.EndofPeriod) = value
        End Set
    End Property

    <Method("Enter Value", "Processes a Telemetry Value", OPC.OPC_AddIn_DA_PROCESSVALUE)>
    Public Sub ProcessValue(ByVal TimeStamp As Date, ByVal QualityValue As Int32, ByVal Value As Double, ByVal Reason As Int32)
        Dim NewQuality As Quality

		' You can cast any value (that can be converted to the underlying type of the enum) 
		' to the enum variable, even if there is no corresponding enumeration for the value,
		' hence the use of IsDefined
		If [Enum].IsDefined(GetType(Quality), QualityValue) Then
            NewQuality = CType(QualityValue, Quality)

            If Me.ScannerId.Id <> ClearSCADA.DBObjFramework.Reference(Of AddInScanner).NullRefId Then
				' We can only perform a DriverAction on a point if it is attached to a scanner, The call
				' actually results in OnAction being called on the DriverScanner object.
				' If the last point process time is later than the requested time then insert history rather than processing value
				If TimeStamp > CDate(Me.Item("CurrentTime")) Then
                    Me.DriverAction(OPC.OPC_AddIn_DA_PROCESSVALUE, New Object() {Me.Id, TimeStamp, NewQuality, Value, Reason}, "ProcessPointValue")
                Else
					'Change/add old value. Execute method, passing relevant parameters
					Dim Args(2) As Object 'Timestamp, Value, Quality, Reason is preset
					Try
                        Args(0) = TimeStamp
                        Args(1) = Value
                        Args(2) = NewQuality
                        Me.Method("Historic.ModifyValue", Args)

                    Catch ex As Exception
                        Dim ModLog As New AddInModule
                        ModLog.Log("Failed Method Historic.ModifyValue for " & Me.Item("FullName").ToString & ", " & ex.Message)
                    End Try
                End If
            End If
        Else
            ' Throwing the exception will result in the Catastrophic error dialog but our message will be logged!
            Throw New InvalidCastException("Invalid Quality Code: " & QualityValue)
        End If
    End Sub

    '***********************************************************************************
    'Validation
    Public Overrides Sub OnValidateConfig(ByVal Errors As ClearSCADA.DBObjFramework.MessageInfo)
        'If historic storage is disabled, produce an error
        If CBool(Me.Item("Historic")) = False Then 'Cannot highlight correct field, just leaving cursor here.
            Errors.Add(Me, "ScannerId", "Historic data storage must be enabled on this point")
        End If

        MyBase.OnValidateConfig(Errors)
    End Sub

    'Validate syntax in driver - initiated from onConfigChange, as can filter on type
    'OnValidateConfig cannot do this
    Public Overrides Sub OnConfigChange(ByVal EventCode As ClearSCADA.DBObjFramework.FrameworkBase.ConfigEvent, ByVal Errors As ClearSCADA.DBObjFramework.MessageInfo, ByVal OldObject As ClearSCADA.DBObjFramework.DBObject)
        If EventCode = ConfigEvent.Modified Then
			'Optional
		End If
    End Sub

End Class

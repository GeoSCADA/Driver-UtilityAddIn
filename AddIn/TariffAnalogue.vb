Imports System.Threading
Imports ClearSCADA.DBObjFramework
Imports ClearSCADA.DBObjFramework.PointSourceEntry
Imports AddIn.Globals

<Table("Tariff Analogue", "Tariff")> _
Public Class TariffAnalogue
    Inherits AnaloguePoint

    '***********************************************************************************
    'Enable all flags
    Public Sub New()
        MyBase.HistoricFilter(Reason.ValueChange) = True
        MyBase.HistoricFilter(Reason.StateChange) = True
        MyBase.HistoricFilter(Reason.Report) = True
        MyBase.HistoricFilter(Reason.EndofPeriod) = True
    End Sub

    '***********************************************************************************
    'Database Fields
    <Label("Enabled", 1, 6)> _
    <ConfigField("In Service", "Controls whether point is active.", 1, 7, &H350501B)> _
    Public Overrides Property InService() As Boolean
        Get
            Return MyBase.InService
        End Get
        Set(ByVal value As Boolean)
            MyBase.InService = value
        End Set
    End Property

    <Label("Scanner", 2, 6)> _
    <ConfigField("Scanner", "Scanner", 2, 7, &H350532F)> _
    Public Shadows Property ScannerId() As Reference(Of AddInScanner)
        Get
            Return New Reference(Of AddInScanner)(MyBase.ScannerId)
        End Get
        Set(ByVal value As Reference(Of AddInScanner))
            MyBase.ScannerId = New Reference(Of Scanner)(value)
        End Set
    End Property

    '***********************************************************************************
    <Label("Month", 3, 1, Flags:=FormFlags.AlignLeft)> _
    <ConfigField("Month01", "Start Month 01", 4, 1, OPC.OPC_BASE + 401)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month01 As Byte

    <Label("to", 3, 2, Flags:=FormFlags.AlignLeft)> _
    <ConfigField("MonthTo01", "End Month 01", 4, 2, OPC.OPC_BASE + 402)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo01 As Byte

    <Label("Days", 3, 3, Flags:=FormFlags.AlignLeft)> _
    <ConfigField("Days01", "Days 01", 4, 3, OPC.OPC_BASE + 403)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days01 As Byte

    <Label("Time", 3, 4, Flags:=FormFlags.AlignLeft)> _
    <ConfigField("Hour01", "Start Hour 01", 4, 4, OPC.OPC_BASE + 404)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour01 As Byte

    <Label("to", 3, 5, Flags:=FormFlags.AlignLeft)> _
    <ConfigField("HourTo01", "End Hour 01", 4, 5, OPC.OPC_BASE + 405)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo01 As Byte

    <Label("Cost", 3, 6, Flags:=FormFlags.AlignLeft)> _
    <ConfigField("Cost01", "Cost 01", 4, 6, OPC.OPC_BASE + 406)> _
    Public Cost01 As Double

    '**********************************************************************
    <ConfigField("Month02", "Start Month 02", 5, 1, OPC.OPC_BASE + 411)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month02 As Byte

    <ConfigField("MonthTo02", "End Month 02", 5, 2, OPC.OPC_BASE + 412)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo02 As Byte

    <ConfigField("Days02", "Days 02", 5, 3, OPC.OPC_BASE + 413)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days02 As Byte

    <ConfigField("Hour02", "Start Hour 02", 5, 4, OPC.OPC_BASE + 414)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour02 As Byte

    <ConfigField("HourTo02", "End Hour 02", 5, 5, OPC.OPC_BASE + 415)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo02 As Byte

    <ConfigField("Cost02", "Cost 02", 5, 6, OPC.OPC_BASE + 416)> _
    Public Cost02 As Double

    '**********************************************************************
    <ConfigField("Month03", "Start Month 03", 6, 1, OPC.OPC_BASE + 421)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month03 As Byte

    <ConfigField("MonthTo03", "End Month 03", 6, 2, OPC.OPC_BASE + 422)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo03 As Byte

    <ConfigField("Days03", "Days 03", 6, 3, OPC.OPC_BASE + 423)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days03 As Byte

    <ConfigField("Hour03", "Start Hour 03", 6, 4, OPC.OPC_BASE + 424)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour03 As Byte

    <ConfigField("HourTo03", "End Hour 03", 6, 5, OPC.OPC_BASE + 425)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo03 As Byte

    <ConfigField("Cost03", "Cost 03", 6, 6, OPC.OPC_BASE + 426)> _
    Public Cost03 As Double

    '**********************************************************************
    <ConfigField("Month04", "Start Month 04", 7, 1, OPC.OPC_BASE + 431)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month04 As Byte

    <ConfigField("MonthTo04", "End Month 04", 7, 2, OPC.OPC_BASE + 432)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo04 As Byte

    <ConfigField("Days04", "Days 04", 7, 3, OPC.OPC_BASE + 433)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days04 As Byte

    <ConfigField("Hour04", "Start Hour 04", 7, 4, OPC.OPC_BASE + 434)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour04 As Byte

    <ConfigField("HourTo04", "End Hour 04", 7, 5, OPC.OPC_BASE + 435)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo04 As Byte

    <ConfigField("Cost04", "Cost 04", 7, 6, OPC.OPC_BASE + 436)> _
    Public Cost04 As Double

    '**********************************************************************
    <ConfigField("Month05", "Start Month 05", 8, 1, OPC.OPC_BASE + 441)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month05 As Byte

    <ConfigField("MonthTo05", "End Month 05", 8, 2, OPC.OPC_BASE + 442)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo05 As Byte

    <ConfigField("Days05", "Days 05", 8, 3, OPC.OPC_BASE + 443)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days05 As Byte

    <ConfigField("Hour05", "Start Hour 05", 8, 4, OPC.OPC_BASE + 444)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour05 As Byte

    <ConfigField("HourTo05", "End Hour 05", 8, 5, OPC.OPC_BASE + 445)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo05 As Byte

    <ConfigField("Cost05", "Cost 05", 8, 6, OPC.OPC_BASE + 446)> _
    Public Cost05 As Double

    '**********************************************************************
    <ConfigField("Month06", "Start Month 06", 9, 1, OPC.OPC_BASE + 451)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month06 As Byte

    <ConfigField("MonthTo06", "End Month 06", 9, 2, OPC.OPC_BASE + 452)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo06 As Byte

    <ConfigField("Days06", "Days 06", 9, 3, OPC.OPC_BASE + 453)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days06 As Byte

    <ConfigField("Hour06", "Start Hour 06", 9, 4, OPC.OPC_BASE + 454)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour06 As Byte

    <ConfigField("HourTo06", "End Hour 06", 9, 5, OPC.OPC_BASE + 455)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo06 As Byte

    <ConfigField("Cost06", "Cost 06", 9, 6, OPC.OPC_BASE + 456)> _
    Public Cost06 As Double

    '**********************************************************************
    <ConfigField("Month07", "Start Month 07", 10, 1, OPC.OPC_BASE + 461)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month07 As Byte

    <ConfigField("MonthTo07", "End Month 07", 10, 2, OPC.OPC_BASE + 462)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo07 As Byte

    <ConfigField("Days07", "Days 07", 10, 3, OPC.OPC_BASE + 463)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days07 As Byte

    <ConfigField("Hour07", "Start Hour 07", 10, 4, OPC.OPC_BASE + 464)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour07 As Byte

    <ConfigField("HourTo07", "End Hour 07", 10, 5, OPC.OPC_BASE + 465)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo07 As Byte

    <ConfigField("Cost07", "Cost 07", 10, 6, OPC.OPC_BASE + 466)> _
    Public Cost07 As Double

    '**********************************************************************
    <ConfigField("Month08", "Start Month 08", 11, 1, OPC.OPC_BASE + 561)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month08 As Byte

    <ConfigField("MonthTo08", "End Month 08", 11, 2, OPC.OPC_BASE + 562)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo08 As Byte

    <ConfigField("Days08", "Days 08", 11, 3, OPC.OPC_BASE + 563)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days08 As Byte

    <ConfigField("Hour08", "Start Hour 08", 11, 4, OPC.OPC_BASE + 564)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour08 As Byte

    <ConfigField("HourTo08", "End Hour 08", 11, 5, OPC.OPC_BASE + 565)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo08 As Byte

    <ConfigField("Cost08", "Cost 08", 11, 6, OPC.OPC_BASE + 566)> _
    Public Cost08 As Double

    '**********************************************************************
    <ConfigField("Month09", "Start Month 09", 12, 1, OPC.OPC_BASE + 571)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month09 As Byte

    <ConfigField("MonthTo09", "End Month 09", 12, 2, OPC.OPC_BASE + 572)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo09 As Byte

    <ConfigField("Days09", "Days 09", 12, 3, OPC.OPC_BASE + 573)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days09 As Byte

    <ConfigField("Hour09", "Start Hour 09", 12, 4, OPC.OPC_BASE + 574)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour09 As Byte

    <ConfigField("HourTo09", "End Hour 09", 12, 5, OPC.OPC_BASE + 575)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo09 As Byte

    <ConfigField("Cost09", "Cost 09", 12, 6, OPC.OPC_BASE + 576)> _
    Public Cost09 As Double

    '**********************************************************************
    <ConfigField("Month10", "Start Month 10", 13, 1, OPC.OPC_BASE + 581)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month10 As Byte

    <ConfigField("MonthTo10", "End Month 10", 13, 2, OPC.OPC_BASE + 582)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo10 As Byte

    <ConfigField("Days10", "Days 10", 13, 3, OPC.OPC_BASE + 583)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days10 As Byte

    <ConfigField("Hour10", "Start Hour 10", 13, 4, OPC.OPC_BASE + 584)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour10 As Byte

    <ConfigField("HourTo10", "End Hour 10", 13, 5, OPC.OPC_BASE + 585)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo10 As Byte

    <ConfigField("Cost10", "Cost 10", 13, 6, OPC.OPC_BASE + 586)> _
    Public Cost10 As Double

    '**********************************************************************
    <ConfigField("Month11", "Start Month 11", 14, 1, OPC.OPC_BASE + 591)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month11 As Byte

    <ConfigField("MonthTo11", "End Month 11", 14, 2, OPC.OPC_BASE + 592)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo11 As Byte

    <ConfigField("Days11", "Days 11", 14, 3, OPC.OPC_BASE + 593)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days11 As Byte

    <ConfigField("Hour11", "Start Hour 11", 14, 4, OPC.OPC_BASE + 594)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour11 As Byte

    <ConfigField("HourTo11", "End Hour 11", 14, 5, OPC.OPC_BASE + 595)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo11 As Byte

    <ConfigField("Cost11", "Cost 11", 14, 6, OPC.OPC_BASE + 596)> _
    Public Cost11 As Double

    '**********************************************************************
    <ConfigField("Month12", "Start Month 12", 15, 1, OPC.OPC_BASE + 601)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month12 As Byte

    <ConfigField("MonthTo12", "End Month 12", 15, 2, OPC.OPC_BASE + 602)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo12 As Byte

    <ConfigField("Days12", "Days 12", 15, 3, OPC.OPC_BASE + 603)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days12 As Byte

    <ConfigField("Hour12", "Start Hour 12", 15, 4, OPC.OPC_BASE + 604)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour12 As Byte

    <ConfigField("HourTo12", "End Hour 12", 15, 5, OPC.OPC_BASE + 605)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo12 As Byte

    <ConfigField("Cost12", "Cost 12", 15, 6, OPC.OPC_BASE + 606)> _
    Public Cost12 As Double

    '**********************************************************************
    <ConfigField("Month13", "Start Month 13", 16, 1, OPC.OPC_BASE + 611)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month13 As Byte

    <ConfigField("MonthTo13", "End Month 13", 16, 2, OPC.OPC_BASE + 612)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo13 As Byte

    <ConfigField("Days13", "Days 13", 16, 3, OPC.OPC_BASE + 613)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days13 As Byte

    <ConfigField("Hour13", "Start Hour 13", 16, 4, OPC.OPC_BASE + 614)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour13 As Byte

    <ConfigField("HourTo13", "End Hour 13", 16, 5, OPC.OPC_BASE + 615)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo13 As Byte

    <ConfigField("Cost13", "Cost 13", 16, 6, OPC.OPC_BASE + 616)> _
    Public Cost13 As Double

    '**********************************************************************
    <ConfigField("Month14", "Start Month 14", 17, 1, OPC.OPC_BASE + 621)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month14 As Byte

    <ConfigField("MonthTo14", "End Month 14", 17, 2, OPC.OPC_BASE + 622)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo14 As Byte

    <ConfigField("Days14", "Days 14", 17, 3, OPC.OPC_BASE + 623)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days14 As Byte

    <ConfigField("Hour14", "Start Hour 14", 17, 4, OPC.OPC_BASE + 624)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour14 As Byte

    <ConfigField("HourTo14", "End Hour 14", 17, 5, OPC.OPC_BASE + 625)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo14 As Byte

    <ConfigField("Cost14", "Cost 14", 17, 6, OPC.OPC_BASE + 626)> _
    Public Cost14 As Double

    '**********************************************************************
    <ConfigField("Month15", "Start Month 15", 18, 1, OPC.OPC_BASE + 631)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month15 As Byte

    <ConfigField("MonthTo15", "End Month 15", 18, 2, OPC.OPC_BASE + 632)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo15 As Byte

    <ConfigField("Days15", "Days 15", 18, 3, OPC.OPC_BASE + 633)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days15 As Byte

    <ConfigField("Hour15", "Start Hour 15", 18, 4, OPC.OPC_BASE + 634)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour15 As Byte

    <ConfigField("HourTo15", "End Hour 15", 18, 5, OPC.OPC_BASE + 635)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo15 As Byte

    <ConfigField("Cost15", "Cost 15", 18, 6, OPC.OPC_BASE + 636)> _
    Public Cost15 As Double

    '**********************************************************************
    <ConfigField("Month16", "Start Month 16", 19, 1, OPC.OPC_BASE + 641)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month16 As Byte

    <ConfigField("MonthTo16", "End Month 16", 19, 2, OPC.OPC_BASE + 642)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo16 As Byte

    <ConfigField("Days16", "Days 16", 19, 3, OPC.OPC_BASE + 643)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days16 As Byte

    <ConfigField("Hour16", "Start Hour 16", 19, 4, OPC.OPC_BASE + 644)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour16 As Byte

    <ConfigField("HourTo16", "End Hour 16", 19, 5, OPC.OPC_BASE + 645)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo16 As Byte

    <ConfigField("Cost16", "Cost 16", 19, 6, OPC.OPC_BASE + 646)> _
    Public Cost16 As Double

    '**********************************************************************
    <ConfigField("Month17", "Start Month 17", 20, 1, OPC.OPC_BASE + 651)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month17 As Byte

    <ConfigField("MonthTo17", "End Month 17", 20, 2, OPC.OPC_BASE + 652)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo17 As Byte

    <ConfigField("Days17", "Days 17", 20, 3, OPC.OPC_BASE + 653)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days17 As Byte

    <ConfigField("Hour17", "Start Hour 17", 20, 4, OPC.OPC_BASE + 654)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour17 As Byte

    <ConfigField("HourTo17", "End Hour 17", 20, 5, OPC.OPC_BASE + 655)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo17 As Byte

    <ConfigField("Cost17", "Cost 17", 20, 6, OPC.OPC_BASE + 656)> _
    Public Cost17 As Double

    '**********************************************************************
    <ConfigField("Month18", "Start Month 18", 21, 1, OPC.OPC_BASE + 661)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month18 As Byte

    <ConfigField("MonthTo18", "End Month 18", 21, 2, OPC.OPC_BASE + 662)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo18 As Byte

    <ConfigField("Days18", "Days 18", 21, 3, OPC.OPC_BASE + 663)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days18 As Byte

    <ConfigField("Hour18", "Start Hour 18", 21, 4, OPC.OPC_BASE + 664)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour18 As Byte

    <ConfigField("HourTo18", "End Hour 18", 21, 5, OPC.OPC_BASE + 665)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo18 As Byte

    <ConfigField("Cost18", "Cost 18", 21, 6, OPC.OPC_BASE + 666)> _
    Public Cost18 As Double

    '**********************************************************************
    <ConfigField("Month19", "Start Month 19", 22, 1, OPC.OPC_BASE + 671)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month19 As Byte

    <ConfigField("MonthTo19", "End Month 19", 22, 2, OPC.OPC_BASE + 672)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo19 As Byte

    <ConfigField("Days19", "Days 19", 22, 3, OPC.OPC_BASE + 673)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days19 As Byte

    <ConfigField("Hour19", "Start Hour 19", 22, 4, OPC.OPC_BASE + 674)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour19 As Byte

    <ConfigField("HourTo19", "End Hour 19", 22, 5, OPC.OPC_BASE + 675)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo19 As Byte

    <ConfigField("Cost19", "Cost 19", 22, 6, OPC.OPC_BASE + 676)> _
    Public Cost19 As Double

    '**********************************************************************
    <ConfigField("Month20", "Start Month 20", 23, 1, OPC.OPC_BASE + 681)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month20 As Byte

    <ConfigField("MonthTo20", "End Month 20", 23, 2, OPC.OPC_BASE + 682)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo20 As Byte

    <ConfigField("Days20", "Days 20", 23, 3, OPC.OPC_BASE + 683)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days20 As Byte

    <ConfigField("Hour20", "Start Hour 20", 23, 4, OPC.OPC_BASE + 684)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour20 As Byte

    <ConfigField("HourTo20", "End Hour 20", 23, 5, OPC.OPC_BASE + 685)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo20 As Byte

    <ConfigField("Cost20", "Cost 20", 23, 6, OPC.OPC_BASE + 686)> _
    Public Cost20 As Double

    '**********************************************************************
    <ConfigField("Month21", "Start Month 21", 24, 1, OPC.OPC_BASE + 691)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month21 As Byte

    <ConfigField("MonthTo21", "End Month 21", 24, 2, OPC.OPC_BASE + 692)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo21 As Byte

    <ConfigField("Days21", "Days 21", 24, 3, OPC.OPC_BASE + 693)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days21 As Byte

    <ConfigField("Hour21", "Start Hour 21", 24, 4, OPC.OPC_BASE + 694)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour21 As Byte

    <ConfigField("HourTo21", "End Hour 21", 24, 5, OPC.OPC_BASE + 695)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo21 As Byte

    <ConfigField("Cost21", "Cost 21", 24, 6, OPC.OPC_BASE + 696)> _
    Public Cost21 As Double

    '**********************************************************************
    <ConfigField("Month22", "Start Month 22", 25, 1, OPC.OPC_BASE + 701)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month22 As Byte

    <ConfigField("MonthTo22", "End Month 22", 25, 2, OPC.OPC_BASE + 702)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo22 As Byte

    <ConfigField("Days22", "Days 22", 25, 3, OPC.OPC_BASE + 703)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days22 As Byte

    <ConfigField("Hour22", "Start Hour 22", 25, 4, OPC.OPC_BASE + 704)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour22 As Byte

    <ConfigField("HourTo22", "End Hour 22", 25, 5, OPC.OPC_BASE + 705)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo22 As Byte

    <ConfigField("Cost22", "Cost 22", 25, 6, OPC.OPC_BASE + 706)> _
    Public Cost22 As Double

    '**********************************************************************
    <ConfigField("Month23", "Start Month 23", 26, 1, OPC.OPC_BASE + 711)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month23 As Byte

    <ConfigField("MonthTo23", "End Month 23", 26, 2, OPC.OPC_BASE + 712)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo23 As Byte

    <ConfigField("Days23", "Days 23", 26, 3, OPC.OPC_BASE + 713)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days23 As Byte

    <ConfigField("Hour23", "Start Hour 23", 26, 4, OPC.OPC_BASE + 714)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour23 As Byte

    <ConfigField("HourTo23", "End Hour 23", 26, 5, OPC.OPC_BASE + 715)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo23 As Byte

    <ConfigField("Cost23", "Cost 23", 26, 6, OPC.OPC_BASE + 716)> _
    Public Cost23 As Double

    '**********************************************************************
    <ConfigField("Month24", "Start Month 24", 27, 1, OPC.OPC_BASE + 721)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public Month24 As Byte

    <ConfigField("MonthTo24", "End Month 24", 27, 2, OPC.OPC_BASE + 722)> _
    <[Enum](New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})> _
    Public MonthTo24 As Byte

    <ConfigField("Days24", "Days 24", 27, 3, OPC.OPC_BASE + 723)> _
    <[Enum](New String() {"None", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "All", "Week Day", "Week End", "Mon-Thu"})> _
    Public Days24 As Byte

    <ConfigField("Hour24", "Start Hour 24", 27, 4, OPC.OPC_BASE + 724)> _
    <[Enum](New String() {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"})> _
    Public Hour24 As Byte

    <ConfigField("HourTo24", "End Hour 24", 27, 5, OPC.OPC_BASE + 725)> _
    <[Enum](New String() {"00:59", "01:59", "02:59", "03:59", "04:59", "05:59", "06:59", "07:59", "08:59", "09:59", "10:59", "11:59", "12:59", "13:59", "14:59", "15:59", "16:59", "17:59", "18:59", "19:59", "20:59", "21:59", "22:59", "23:59"})> _
    Public HourTo24 As Byte

    <ConfigField("Cost24", "Cost 24", 27, 6, OPC.OPC_BASE + 726)> _
    Public Cost24 As Double

    '***********************************************************************************
    'This section is mandatory and controls the history logged when a point value is updated
    <Label("Historic Data Filter", 30, 1, Width:=4, Height:=2)>
    <Label("Significant Change", 31, 1)> _
    <ConfigField("Significant Change Historic Filter", "Controls whether Value Changes are logged historically", 31, 2, &H3506913)> _
    Public Property HistoricFilterValue() As Boolean
        Get
            Return MyBase.HistoricFilter(Reason.ValueChange)
        End Get
        Set(ByVal value As Boolean)
            MyBase.HistoricFilter(Reason.ValueChange) = value
        End Set
    End Property

    <Label("State Change", 32, 1)> _
    <ConfigField("State Change Historic Filter", "Controls whether State Changes are logged historically", 32, 2, &H3506914)> _
    Public Property HistoricFilterState() As Boolean
        Get
            Return MyBase.HistoricFilter(Reason.StateChange)
        End Get
        Set(ByVal value As Boolean)
            MyBase.HistoricFilter(Reason.StateChange) = value
        End Set
    End Property

    <Label("Report", 31, 3)> _
    <ConfigField("Report Historic Filter", "Controls whether Timed Report values are logged historically", 31, 4, &H3506915)> _
    Public Property HistoricFilterReport() As Boolean
        Get
            Return MyBase.HistoricFilter(Reason.Report)
        End Get
        Set(ByVal value As Boolean)
            MyBase.HistoricFilter(Reason.Report) = value
        End Set
    End Property

    <Label("End Of Period", 32, 3)> _
   <ConfigField("EOP Historic Filter", "Controls whether EOP values are logged historically", 32, 4, &H3506916)> _
    Public Property HistoricFilterEOP() As Boolean
        Get
            Return MyBase.HistoricFilter(Reason.EndofPeriod)
        End Get
        Set(ByVal value As Boolean)
            MyBase.HistoricFilter(Reason.EndofPeriod) = value
        End Set
    End Property

    '***********************************************************************************
    <DataField("GetValueNow", "Read Value of Tariff for current time", OPC.OPC_AddIn_GetTariffValueNow, ViewInfoTitle:="Current Tariff Value")> _
    Public ReadOnly Property GetValueNow() As Double
        Get
            Return (GetValue(Now()))
        End Get
    End Property

    <Method("GetValue", "Read Value of Tariff at Time/Date", OPC.OPC_AddIn_GetTariffValue)> _
    Public Function GetValue(ByVal TimeStamp As Date) As Double
        'Assumed that the Date/Time is GMT and should be converted to BST first
        Dim LocalTime As Date = TimeStamp.ToLocalTime()
        'Get the Month, Day of Week and Hour numbers
        Dim M As Integer = LocalTime.Month() - 1 '1-12 convert to 0-11
        Dim D As Integer = LocalTime.DayOfWeek() '0=Sunday
        If D = 0 Then D = 7 'Convert to 7-Sunday
        Dim H As Integer = LocalTime.Hour() '0-23

        'Check Month
        'Check Day of Week
        'Check Hour

        If (Month01 <= MonthTo01 And M >= Month01 And M <= MonthTo01) Or (Month01 > MonthTo01 And (M >= Month01 Or M <= MonthTo01)) Then
            If (Days01 >= 1 And Days01 <= 7 And D = Days01) Or (Days01 = 8) Or (Days01 = 9 And D <= 5) Or (Days01 = 10 And D >= 6) Or (Days01 = 11 And D <= 4) Then
                If (Hour01 <= HourTo01 And H >= Hour01 And H <= HourTo01) Or (Hour01 > HourTo01 And (H >= Hour01 Or H <= HourTo01)) Then
                    Return (Cost01)
                End If
            End If
        End If
        If (Month02 <= MonthTo02 And M >= Month02 And M <= MonthTo02) Or (Month02 > MonthTo02 And (M >= Month02 Or M <= MonthTo02)) Then
            If (Days02 >= 1 And Days02 <= 7 And D = Days02) Or (Days02 = 8) Or (Days02 = 9 And D <= 5) Or (Days02 = 10 And D >= 6) Or (Days02 = 11 And D <= 4) Then
                If (Hour02 <= HourTo02 And H >= Hour02 And H <= HourTo02) Or (Hour02 > HourTo02 And (H >= Hour02 Or H <= HourTo02)) Then
                    Return (Cost02)
                End If
            End If
        End If
        If (Month03 <= MonthTo03 And M >= Month03 And M <= MonthTo03) Or (Month03 > MonthTo03 And (M >= Month03 Or M <= MonthTo03)) Then
            If (Days03 >= 1 And Days03 <= 7 And D = Days03) Or (Days03 = 8) Or (Days03 = 9 And D <= 5) Or (Days03 = 10 And D >= 6) Or (Days03 = 11 And D <= 4) Then
                If (Hour03 <= HourTo03 And H >= Hour03 And H <= HourTo03) Or (Hour03 > HourTo03 And (H >= Hour03 Or H <= HourTo03)) Then
                    Return (Cost03)
                End If
            End If
        End If
        If (Month04 <= MonthTo04 And M >= Month04 And M <= MonthTo04) Or (Month04 > MonthTo04 And (M >= Month04 Or M <= MonthTo04)) Then
            If (Days04 >= 1 And Days04 <= 7 And D = Days04) Or (Days04 = 8) Or (Days04 = 9 And D <= 5) Or (Days04 = 10 And D >= 6) Or (Days04 = 11 And D <= 4) Then
                If (Hour04 <= HourTo04 And H >= Hour04 And H <= HourTo04) Or (Hour04 > HourTo04 And (H >= Hour04 Or H <= HourTo04)) Then
                    Return (Cost04)
                End If
            End If
        End If
        If (Month05 <= MonthTo05 And M >= Month05 And M <= MonthTo05) Or (Month05 > MonthTo05 And (M >= Month05 Or M <= MonthTo05)) Then
            If (Days05 >= 1 And Days05 <= 7 And D = Days05) Or (Days05 = 8) Or (Days05 = 9 And D <= 5) Or (Days05 = 10 And D >= 6) Or (Days05 = 11 And D <= 4) Then
                If (Hour05 <= HourTo05 And H >= Hour05 And H <= HourTo05) Or (Hour05 > HourTo05 And (H >= Hour05 Or H <= HourTo05)) Then
                    Return (Cost05)
                End If
            End If
        End If
        If (Month06 <= MonthTo06 And M >= Month06 And M <= MonthTo06) Or (Month06 > MonthTo06 And (M >= Month06 Or M <= MonthTo06)) Then
            If (Days06 >= 1 And Days06 <= 7 And D = Days06) Or (Days06 = 8) Or (Days06 = 9 And D <= 5) Or (Days06 = 10 And D >= 6) Or (Days06 = 11 And D <= 4) Then
                If (Hour06 <= HourTo06 And H >= Hour06 And H <= HourTo06) Or (Hour06 > HourTo06 And (H >= Hour06 Or H <= HourTo06)) Then
                    Return (Cost06)
                End If
            End If
        End If
        If (Month07 <= MonthTo07 And M >= Month07 And M <= MonthTo07) Or (Month07 > MonthTo07 And (M >= Month07 Or M <= MonthTo07)) Then
            If (Days07 >= 1 And Days07 <= 7 And D = Days07) Or (Days07 = 8) Or (Days07 = 9 And D <= 5) Or (Days07 = 10 And D >= 6) Or (Days07 = 11 And D <= 4) Then
                If (Hour07 <= HourTo07 And H >= Hour07 And H <= HourTo07) Or (Hour07 > HourTo07 And (H >= Hour07 Or H <= HourTo07)) Then
                    Return (Cost07)
                End If
            End If
        End If
        If (Month08 <= MonthTo08 And M >= Month08 And M <= MonthTo08) Or (Month08 > MonthTo08 And (M >= Month08 Or M <= MonthTo08)) Then
            If (Days08 >= 1 And Days08 <= 7 And D = Days08) Or (Days08 = 8) Or (Days08 = 9 And D <= 5) Or (Days08 = 10 And D >= 6) Or (Days08 = 11 And D <= 4) Then
                If (Hour08 <= HourTo08 And H >= Hour08 And H <= HourTo08) Or (Hour08 > HourTo08 And (H >= Hour08 Or H <= HourTo08)) Then
                    Return (Cost08)
                End If
            End If
        End If
        If (Month09 <= MonthTo09 And M >= Month09 And M <= MonthTo09) Or (Month09 > MonthTo09 And (M >= Month09 Or M <= Month09)) Then
            If (Days09 >= 1 And Days09 <= 7 And D = Days09) Or (Days09 = 8) Or (Days09 = 9 And D <= 5) Or (Days09 = 10 And D >= 6) Or (Days09 = 11 And D <= 4) Then
                If (Hour09 <= HourTo09 And H >= Hour09 And H <= HourTo09) Or (Hour09 > HourTo09 And (H >= Hour09 Or H <= HourTo09)) Then
                    Return (Cost09)
                End If
            End If
        End If
        If (Month10 <= MonthTo10 And M >= Month10 And M <= MonthTo10) Or (Month10 > MonthTo10 And (M >= Month10 Or M <= MonthTo10)) Then
            If (Days10 >= 1 And Days10 <= 7 And D = Days10) Or (Days10 = 8) Or (Days10 = 9 And D <= 5) Or (Days10 = 10 And D >= 6) Or (Days10 = 11 And D <= 4) Then
                If (Hour10 <= HourTo10 And H >= Hour10 And H <= HourTo10) Or (Hour10 > HourTo10 And (H >= Hour10 Or H <= HourTo10)) Then
                    Return (Cost10)
                End If
            End If
        End If
        If (Month11 <= MonthTo11 And M >= Month11 And M <= MonthTo11) Or (Month11 > MonthTo11 And (M >= Month11 Or M <= MonthTo11)) Then
            If (Days11 >= 1 And Days11 <= 7 And D = Days11) Or (Days11 = 8) Or (Days11 = 9 And D <= 5) Or (Days11 = 10 And D >= 6) Or (Days11 = 11 And D <= 4) Then
                If (Hour11 <= HourTo11 And H >= Hour11 And H <= HourTo11) Or (Hour11 > HourTo11 And (H >= Hour11 Or H <= HourTo11)) Then
                    Return (Cost11)
                End If
            End If
        End If
        If (Month12 <= MonthTo12 And M >= Month12 And M <= MonthTo12) Or (Month12 > MonthTo12 And (M >= Month12 Or M <= MonthTo12)) Then
            If (Days12 >= 1 And Days12 <= 7 And D = Days12) Or (Days12 = 8) Or (Days12 = 9 And D <= 5) Or (Days12 = 10 And D >= 6) Or (Days12 = 11 And D <= 4) Then
                If (Hour12 <= HourTo12 And H >= Hour12 And H <= HourTo12) Or (Hour12 > HourTo12 And (H >= Hour12 Or H <= HourTo12)) Then
                    Return (Cost12)
                End If
            End If
        End If
        If (Month13 <= MonthTo13 And M >= Month13 And M <= MonthTo13) Or (Month13 > MonthTo13 And (M >= Month13 Or M <= MonthTo13)) Then
            If (Days13 >= 1 And Days13 <= 7 And D = Days13) Or (Days13 = 8) Or (Days13 = 9 And D <= 5) Or (Days13 = 10 And D >= 6) Or (Days13 = 11 And D <= 4) Then
                If (Hour13 <= HourTo13 And H >= Hour13 And H <= HourTo13) Or (Hour13 > HourTo13 And (H >= Hour13 Or H <= HourTo13)) Then
                    Return (Cost13)
                End If
            End If
        End If
        If (Month14 <= MonthTo14 And M >= Month14 And M <= MonthTo14) Or (Month14 > MonthTo14 And (M >= Month14 Or M <= MonthTo14)) Then
            If (Days14 >= 1 And Days14 <= 7 And D = Days14) Or (Days14 = 8) Or (Days14 = 9 And D <= 5) Or (Days14 = 10 And D >= 6) Or (Days14 = 11 And D <= 4) Then
                If (Hour14 <= HourTo14 And H >= Hour14 And H <= HourTo14) Or (Hour14 > HourTo14 And (H >= Hour14 Or H <= HourTo14)) Then
                    Return (Cost14)
                End If
            End If
        End If
        If (Month15 <= MonthTo15 And M >= Month15 And M <= MonthTo15) Or (Month15 > MonthTo15 And (M >= Month15 Or M <= MonthTo15)) Then
            If (Days15 >= 1 And Days15 <= 7 And D = Days15) Or (Days15 = 8) Or (Days15 = 9 And D <= 5) Or (Days15 = 10 And D >= 6) Or (Days15 = 11 And D <= 4) Then
                If (Hour15 <= HourTo15 And H >= Hour15 And H <= HourTo15) Or (Hour15 > HourTo15 And (H >= Hour15 Or H <= HourTo15)) Then
                    Return (Cost15)
                End If
            End If
        End If
        If (Month16 <= MonthTo16 And M >= Month16 And M <= MonthTo16) Or (Month16 > MonthTo16 And (M >= Month16 Or M <= MonthTo16)) Then
            If (Days16 >= 1 And Days16 <= 7 And D = Days16) Or (Days16 = 8) Or (Days16 = 9 And D <= 5) Or (Days16 = 10 And D >= 6) Or (Days16 = 11 And D <= 4) Then
                If (Hour16 <= HourTo16 And H >= Hour16 And H <= HourTo16) Or (Hour16 > HourTo16 And (H >= Hour16 Or H <= HourTo16)) Then
                    Return (Cost16)
                End If
            End If
        End If
        If (Month17 <= MonthTo17 And M >= Month17 And M <= MonthTo17) Or (Month17 > MonthTo17 And (M >= Month17 Or M <= MonthTo17)) Then
            If (Days17 >= 1 And Days17 <= 7 And D = Days17) Or (Days17 = 8) Or (Days17 = 9 And D <= 5) Or (Days17 = 10 And D >= 6) Or (Days17 = 11 And D <= 4) Then
                If (Hour17 <= HourTo17 And H >= Hour17 And H <= HourTo17) Or (Hour17 > HourTo17 And (H >= Hour17 Or H <= HourTo17)) Then
                    Return (Cost17)
                End If
            End If
        End If
        If (Month18 <= MonthTo18 And M >= Month18 And M <= MonthTo18) Or (Month18 > MonthTo18 And (M >= Month18 Or M <= MonthTo18)) Then
            If (Days18 >= 1 And Days18 <= 7 And D = Days18) Or (Days18 = 8) Or (Days18 = 9 And D <= 5) Or (Days18 = 10 And D >= 6) Or (Days18 = 11 And D <= 4) Then
                If (Hour18 <= HourTo18 And H >= Hour18 And H <= HourTo18) Or (Hour18 > HourTo18 And (H >= Hour18 Or H <= HourTo18)) Then
                    Return (Cost18)
                End If
            End If
        End If
        If (Month19 <= MonthTo19 And M >= Month19 And M <= MonthTo19) Or (Month19 > MonthTo19 And (M >= Month19 Or M <= MonthTo19)) Then
            If (Days19 >= 1 And Days19 <= 7 And D = Days19) Or (Days19 = 8) Or (Days19 = 9 And D <= 5) Or (Days19 = 10 And D >= 6) Or (Days19 = 11 And D <= 4) Then
                If (Hour19 <= HourTo19 And H >= Hour19 And H <= HourTo19) Or (Hour19 > HourTo19 And (H >= Hour19 Or H <= HourTo19)) Then
                    Return (Cost19)
                End If
            End If
        End If
        If (Month20 <= MonthTo20 And M >= Month20 And M <= MonthTo20) Or (Month20 > MonthTo20 And (M >= Month20 Or M <= MonthTo20)) Then
            If (Days20 >= 1 And Days20 <= 7 And D = Days20) Or (Days20 = 8) Or (Days20 = 9 And D <= 5) Or (Days20 = 10 And D >= 6) Or (Days20 = 11 And D <= 4) Then
                If (Hour20 <= HourTo20 And H >= Hour20 And H <= HourTo20) Or (Hour20 > HourTo20 And (H >= Hour20 Or H <= HourTo20)) Then
                    Return (Cost20)
                End If
            End If
        End If
        If (Month21 <= MonthTo21 And M >= Month21 And M <= MonthTo21) Or (Month21 > MonthTo21 And (M >= Month21 Or M <= MonthTo21)) Then
            If (Days21 >= 1 And Days21 <= 7 And D = Days21) Or (Days21 = 8) Or (Days21 = 9 And D <= 5) Or (Days21 = 10 And D >= 6) Or (Days21 = 11 And D <= 4) Then
                If (Hour21 <= HourTo21 And H >= Hour21 And H <= HourTo21) Or (Hour21 > HourTo21 And (H >= Hour21 Or H <= HourTo21)) Then
                    Return (Cost21)
                End If
            End If
        End If
        If (Month22 <= MonthTo22 And M >= Month22 And M <= MonthTo22) Or (Month22 > MonthTo22 And (M >= Month22 Or M <= MonthTo22)) Then
            If (Days22 >= 1 And Days22 <= 7 And D = Days22) Or (Days22 = 8) Or (Days22 = 9 And D <= 5) Or (Days22 = 10 And D >= 6) Or (Days22 = 11 And D <= 4) Then
                If (Hour22 <= HourTo22 And H >= Hour22 And H <= HourTo22) Or (Hour22 > HourTo22 And (H >= Hour22 Or H <= HourTo22)) Then
                    Return (Cost22)
                End If
            End If
        End If
        If (Month23 <= MonthTo23 And M >= Month23 And M <= MonthTo23) Or (Month23 > MonthTo23 And (M >= Month23 Or M <= MonthTo23)) Then
            If (Days23 >= 1 And Days23 <= 7 And D = Days23) Or (Days23 = 8) Or (Days23 = 9 And D <= 5) Or (Days23 = 10 And D >= 6) Or (Days23 = 11 And D <= 4) Then
                If (Hour23 <= HourTo23 And H >= Hour23 And H <= HourTo23) Or (Hour23 > HourTo23 And (H >= Hour23 Or H <= HourTo23)) Then
                    Return (Cost23)
                End If
            End If
        End If
        If (Month24 <= MonthTo24 And M >= Month24 And M <= MonthTo24) Or (Month24 > MonthTo24 And (M >= Month24 Or M <= MonthTo24)) Then
            If (Days24 >= 1 And Days24 <= 7 And D = Days24) Or (Days24 = 8) Or (Days24 = 9 And D <= 5) Or (Days24 = 10 And D >= 6) Or (Days24 = 11 And D <= 4) Then
                If (Hour24 <= HourTo24 And H >= Hour24 And H <= HourTo24) Or (Hour24 > HourTo24 And (H >= Hour24 Or H <= HourTo24)) Then
                    Return (Cost24)
                End If
            End If
        End If
        Return (0)
    End Function

    '***********************************************************************************
    'Validation
    Public Overrides Sub OnValidateConfig(ByVal Errors As ClearSCADA.DBObjFramework.MessageInfo)

        MyBase.OnValidateConfig(Errors)
    End Sub

	Public Overrides Sub OnConfigChange(ByVal EventCode As ClearSCADA.DBObjFramework.FrameworkBase.ConfigEvent, ByVal Errors As ClearSCADA.DBObjFramework.MessageInfo, ByVal OldObject As ClearSCADA.DBObjFramework.DBObject)
		If EventCode = ConfigEvent.Modified Then
			'Optional validation
		End If
	End Sub

End Class

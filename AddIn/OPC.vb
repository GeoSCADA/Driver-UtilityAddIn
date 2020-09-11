Public Class OPC
#Region "OPC Range Allocated"
	'Not officially allocated - needs to be done
	'For testing, you may use properties in the range 0x0450000 to 0x045FFFF
#End Region

	'Refer to Analogue to see what the current max is
	Public Const OPC_BASE As Int32 = &H468E000 'was &H4535A0
	Public Const OPC_AddIn_DA_PROCESSVALUE As Int32 = OPC_BASE + 344
    Public Const OPC_AddIn_GetTariffValue As Int32 = OPC_BASE + 345
    Public Const OPC_AddIn_GetTariffValueNow As Int32 = OPC_BASE + 346

	'Strictly speaking, all OPC_BASE+'n' need their own Enum or Const entries

	Public Enum AddInScannerDriverActions As UInteger
        ProcessValue = OPC_AddIn_DA_PROCESSVALUE
    End Enum

    Public Enum AddInScannerModuleFunctions As UInteger
        GetTariffValue = OPC_AddIn_GetTariffValue
    End Enum
End Class

Imports ClearSCADA.DBObjFramework

Public Class Globals

	Friend Shared Function ValidateCDBPointReference(ByVal PointId As UInt32, ByVal AllowNull As Boolean, ByVal ReferenceName As String, ByVal Errors As ClearSCADA.DBObjFramework.MessageInfo) As Boolean
		' This is not the preferred way of checking if the point is derived from CDBPoint,
		' PDEV-582 has requested core functionality to check the points place in the schema.
		Dim DBObj As DBObject = Nothing

		If PointId <> Reference(Of DBObject).NullRefId Then
			Try
				DBObj = DBObject.GetObject(PointId)

				'If this is a CParamDouble then it is a valid reference
				Dim SCXType As String = CStr(DBObj.Item("TypeName"))
				If SCXType = "CParamDouble" Then Return True

				'If this is a Tariff then it is a valid reference
				If SCXType = "TariffAnalogue" Then Return True

				'Verify it is a point - checking two different fields gives us a little more security.
				Dim UpdateTime As Date = CDate(DBObj.Item("CurrentTime"))
				Dim ProcessCount As Int32 = CInt(DBObj.Item("ProcessCount"))

				'OK So far, but also confirm historic is enabled
				If CBool(DBObj.Item("Historic")) = False Then
					Errors.Add(ReferenceName & " Must have historic data enabled.")
					Return False
				End If
				Return True
			Catch ex As Exception
				' This Exception is thrown if the object cannot access "CurrentTime" - therefore is not derived from CDBPoint!
				Errors.Add(ReferenceName & " Must reference an object derived from CDBPoint.")
			End Try
		ElseIf AllowNull Then
			Return True ' We don't care that the point reference is not setup
		Else
			Errors.Add(ReferenceName & " Must reference a point.")
		End If

		Return False
	End Function

End Class


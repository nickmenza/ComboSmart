Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Creates the properties class
			Dim prop As gxProperty = new gxProperty()
			
			' Creates the mygroup with no values
			prop.SetProperty("/mygroup")

			' Creates the testprop1 property in mygroup with integer value 0
			prop.SetProperty("/mygroup/testprop1", 0)

			' Creates the testprop2 property in mygroup with integer value 50
			prop.SetProperty("/mygroup/testprop2", 50)

			' Reads integer values
			Dim ivalue1 As integer = prop.GetPropertyInt("/mygroup/testprop1")
			Dim ivalue2 As integer = prop.GetPropertyInt("/mygroup/testprop2")

			' Reads floating-point values
			Dim fvalue1 As double = prop.GetPropertyFloat("/mygroup/testprop1")
			Dim fvalue2 As double = prop.GetPropertyFloat("/mygroup/testprop2")

			' Reads string values
			Dim svalue1 As string = prop.GetProperty("/mygroup/testprop1")
			Dim svalue2 As string = prop.GetProperty("/mygroup/testprop2")

			' Displays properties	
			Console.WriteLine("Property1: integer={0}, float={1}, string='{2}'", ivalue1,fvalue1,svalue1)
			Console.WriteLine("Property2: integer={0}, float={1}, string='{2}'", ivalue2,fvalue2,svalue2)

			' Removes the mygroup and all created subproperties
			prop.RmProperties("/mygroup")

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

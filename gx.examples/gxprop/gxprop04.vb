Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Creates mymodule and its properties in the /mygroup branch
			Dim myfile As gxPropertyFile = new gxPropertyFile()
		
			myfile.SetProperty("/mygroup/mymodule/testprop1", 1)
	
			' Saves to myfile.xml
			myfile.Save("myfile.xml")
	
			' Removes all properties in the class
			myfile.RmProperties("/")

			' Loads properties from myfile.xml
			myfile.Load("myfile.xml")
	
			' Gets properties
			Dim properties() As string = myfile.GetAllProperties("/") 
		
			' Displays properties
			Console.WriteLine("Properties In the '/':")
			Dim ix As integer
			For ix = 0 To properties.Length-1
				Console.WriteLine(properties.GetValue(ix))
			Next
				
			Console.WriteLine("See the myfile.xml")
				
	
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

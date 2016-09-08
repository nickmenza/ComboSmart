Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Creates the properties class
			Dim prop as gxProperty = new gxProperty()
		
			' Gets properties
			Dim properties as Array = prop.GetProperties("/")
				
			' Displays to the console
			Console.WriteLine("Properties In '/':")
			
			Dim ix as integer
			For ix = 0 To properties.Length-1
				Console.WriteLine(properties.GetValue(ix))
			Next
		
			' Gets all inherited properties
			properties = prop.GetAllProperties("/")
			' Displays to the console
			Console.WriteLine("Properties And all inherited properties In '/':")
			
			For ix = 0 To properties.Length-1
				Console.WriteLine(properties.GetValue(ix))
			Next
	
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

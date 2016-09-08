Imports System
Imports gx

Module Main
	Sub DisplayProperties(prop As gxProperty, path As String, level As Integer)
		' Get properties in this level
		Dim properties() As String = prop.GetProperties(path)
		
		' Write all properties
		Dim ix As Integer
		If Not (properties Is Nothing) Then			
			For ix = 0 To properties.Length-1
				Dim l As Integer
				For l = 0 To level
					Console.Write(" ")
				Next
				Console.WriteLine(properties.GetValue(ix))
				' Displays all subproperties in this level (recursive)
				DisplayProperties(prop, path+"/"+properties.GetValue(ix), level+1)
			Next			
		End If
	End Sub
	
	Sub Main()
		Try
			' Creates the properties class
			Dim prop As gxProperty = new gxProperty()
				
			DisplayProperties(prop, "/", 0)
	
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

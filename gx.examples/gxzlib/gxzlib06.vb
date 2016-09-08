Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Creates the ZIP object
			Dim zip As gxZip = new gxZip()

			' Opens the test02.zip file
            zip.Open("../../../../test02.zip")
            
			' Locates the testfile
			If Not zip.LocateFile("testfile.txt", 0) Then
				Console.WriteLine("File not found: 'testfile.txt'")
				Return
			End If

			' Opens the file
			zip.OpenFile()
			Console.WriteLine("Compression level: {0}", zip.GetFileLevel())
		
			' Reads data from the file
			Dim data() As Byte = zip.ReadFromFile(256)
	
			' Closes the file
			zip.CloseFile()
			
			Console.Write("Contents of testfile.txt: '")
			Dim ix As Integer
            For ix = 0 To data.Length - 1
                Console.Write(Chr(data.GetValue(ix)))
            Next
            Console.WriteLine("'")

			' Closes ZIP
			zip.Close()	

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

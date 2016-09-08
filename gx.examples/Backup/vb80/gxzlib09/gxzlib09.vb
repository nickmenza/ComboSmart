Imports System
Imports gx

Module Main
	Sub Main()
		Try

			Dim zip As gxZip = new gxZip()
	
			' Opens the test04.zip file
            zip.Open("../../../../test04.zip")

			' Locates the testfile and reads the global extra field
			If Not zip.LocateFile("testfile.txt", 0)
				Console.WriteLine("File not found: 'testfile.txt'")
				return
			End If
			
			Dim gextrafield() As Byte = zip.GetFileGlobalExtraField()
			
			Console.Write("Global extra field: '")
			Dim ix As Integer
            Dim c As Char
            For ix = 0 To gextrafield.Length-1
                c = Chr(gextrafield.GetValue(ix))
               Console.Write(c)
            Next
            Console.WriteLine("'")
				
			' Opens the file
			zip.OpenFile()
			Console.WriteLine("Compression level: {0}", zip.GetFileLevel())
	
			' Reads and prints local extra field
			Dim lextrafield() As Byte = zip.ReadLocalExtraField(256)
	
			Console.Write("Local extra field: '")
            For ix = 0 To lextrafield.Length-1
                c = Chr(lextrafield.GetValue(ix))
               Console.Write(c)
            Next
            Console.WriteLine("'")
			
			' Reads data from the file
			Dim data() As Byte = zip.ReadFromFile(256)
				
			Console.Write("Contents of testfile.txt: '")
            For ix = 0 To data.Length-1
                c = Chr(data.GetValue(ix))
               Console.Write(c)
            Next
            Console.WriteLine("'")
	
			' Closes the file
			zip.CloseFile()
		
			' Closes ZIP
			zip.Close()

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

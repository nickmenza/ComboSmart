Imports System
Imports gx

Module Main
	Sub Main()
		Try
			Dim content_string As String = "Content of testfile.txt"
			Dim content(content_string.Length) As Byte
			Dim ix As Integer
			Dim b As Byte
			For ix = 0 To content_string.Length-1
            	b = Microsoft.VisualBasic.AscW(content_string.Chars(ix)) mod 256
            	content.SetValue(b, ix)
            Next
    
			Dim zip As gxZip = new gxZip()
	
			' Creates the test02.zip file
            zip.Create("../../../../test02.zip", GX_CREATEZIPMODES.GX_ZIP_CREATE)

			' Creates the testfile.txt in the ZIP
			zip.CreateFile("testfile.txt", GX_ZLEVELS.GX_Z_BEST_COMPRESSION, _
				               GX_ZSTRATEGIES.GX_Z_DEFAULT_STRATEGY, _
				               "Comment for testfile")
	
			' Writes the content
			zip.WriteToFile(content)
	
			' Closes testfile.txt
			zip.CloseFile()

			' Closes the ZIP and writes the comment	
			zip.Close("This is the global comment of the ZIP")

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

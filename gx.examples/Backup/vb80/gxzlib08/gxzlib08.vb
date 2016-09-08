Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Content info and extra fields
            Dim content_string As String = "Content of testfile.txt"
            Dim content(content_string.Length) As Byte
            Dim ix As Integer
            Dim b As Byte
            For ix = 0 To content_string.Length-1
               b = Microsoft.VisualBasic.AscW(content_string.Chars(ix)) mod 256
               content.SetValue(b, ix)
            Next

            Dim local_extra_field_string As String = "String in the local extra field"
            Dim local_extra_field(local_extra_field_string.Length) As Byte
            For ix = 0 To local_extra_field_string.Length-1
               b = Microsoft.VisualBasic.AscW(local_extra_field_string.Chars(ix)) mod 256
               local_extra_field.SetValue(b, ix)
            Next

            Dim global_extra_field_string As String = "String in the global extra field"
            Dim global_extra_field(global_extra_field_string.Length) As Byte
            For ix = 0 To global_extra_field_string.Length-1
               b = Microsoft.VisualBasic.AscW(global_extra_field_string.Chars(ix)) mod 256
               global_extra_field.SetValue(b, ix)
            Next

			Dim zip As gxZip = new gxZip()

			' Creates the test04.zip file
            zip.Create("../../../../test04.zip", GX_CREATEZIPMODES.GX_ZIP_CREATE)

			' Creates the testfile.txt in the ZIP
			Dim info As gxZipFileInfo = New gxZipFileInfo()
			Dim nullcrc As UInt32 = zip.GetInitialCRC()
			zip.CreateFile("testfile.txt", GX_ZLEVELS.GX_Z_BEST_COMPRESSION, _
				    GX_ZSTRATEGIES.GX_Z_DEFAULT_STRATEGY, "Comment for testfile", _
					info, "", nullcrc, _
					local_extra_field, global_extra_field)

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

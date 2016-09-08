Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Content of testfile.txt
         	Dim content1_string As String = "Content #1 of testfile.txt"
         	Dim content1(content1_string.Length) As Byte
         	Dim ix As Integer
         	Dim b As Byte
         	For ix = 0 To content1_string.Length-1
            	b = Microsoft.VisualBasic.AscW(content1_string.Chars(ix)) mod 256
            	content1.SetValue(b, ix)
            Next
			
         	Dim content2_string As String = "Content #2 of testfile.txt"
         	Dim content2(content2_string.Length) As Byte
         	For ix = 0 To content2_string.Length-1
            	b = Microsoft.VisualBasic.AscW(content2_string.Chars(ix)) mod 256
            	content2.SetValue(b, ix)
            Next
			
			Dim zip As gxZip = new gxZip()

			' Creates the test03.zip file
            zip.Create("../../../../test03.zip", GX_CREATEZIPMODES.GX_ZIP_CREATE)

			' Calculates the CRC value of the contents
			Dim crc As UInt32 = zip.GetInitialCRC()
			crc = zip.CalculateCRC(crc, content1)
			crc = zip.CalculateCRC(crc, content2)

			' Creates the testfile.txt in the ZIP
			Dim info As gxZipFileInfo
			info.tm_sec = 0
			info.tm_min = 0
			info.tm_hour = 0
			info.tm_mday = 0
			info.tm_mon = 0
			info.tm_year = 0
			info.dosdate = 0
			info.internal_fa = 0
			info.external_fa = 0
			Dim _empty(0) As Byte
			zip.CreateFile("testfile.txt", GX_ZLEVELS.GX_Z_BEST_COMPRESSION, _
			        GX_ZSTRATEGIES.GX_Z_DEFAULT_STRATEGY, "Comment for testfile", _
			        info, "test", crc, _empty, _empty)
			
			' Writes the content
			zip.WriteToFile(content1)
			zip.WriteToFile(content2)

			' Closes testfile.txt
			zip.CloseFile()
	
			' Closes the ZIP and writes the comment	
			zip.Close("This is the global comment of the ZIP")
	

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

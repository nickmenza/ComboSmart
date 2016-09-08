Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Source (uncompressed) data
			Dim source_string As String = "0123456789012345678901234567890123456789"
			Dim source_data(source_string.Length) As Byte
			Dim ix As Integer
			Dim b As Byte
	        For ix = 0 To source_string.Length-1
    	    	b = Microsoft.VisualBasic.AscW(source_string.Chars(ix)) mod 256
        	    source_data.SetValue(b, ix)
         	Next
		
			Dim zlib As gxGZip = new gxGZip()
	
			' Creates a .gz file
            zlib.Create("../../../../test01.gz", GX_ZLEVELS.GX_Z_BEST_COMPRESSION, GX_ZSTRATEGIES.GX_Z_DEFAULT_STRATEGY)

	
			' Writes a string to the file
			zlib.Write(source_data)
	
			' Closes the file
			zlib.Close()

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

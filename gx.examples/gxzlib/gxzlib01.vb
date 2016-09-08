Imports System
Imports gx

Module Main
	Sub Main()
		Try
			Dim gz As gxGZip = new gxGZip()
		
			' Source (uncompressed) data
			Dim source_string As String = "0123456789012345678901234567890123456789"
			Dim source_data(source_string.Length) As Byte
			
			Dim ix As Integer
			Dim b As Byte
			For ix = 0 To source_string.Length-1
				b = Microsoft.VisualBasic.AscW(source_string.Chars(ix)) mod 256
				source_data.SetValue(b, ix)
			Next

		
			' Target buffer (for compressed data)
			Console.WriteLine("Compressing {0} bytes..", source_data.Length)
				
			Dim compressed_data() As Byte = gz.Compress(source_data, 1024, GX_ZLEVELS.GX_Z_BEST_COMPRESSION)
		
			Console.WriteLine("Compressed: size={0} bytes", compressed_data.Length)
		
			' Buffer for uncompressed data
			Dim uncompressed_data() As Byte = gz.Uncompress(compressed_data, 1024)
		
			Console.WriteLine("Uncompressed: size={0} bytes", uncompressed_data.Length)
	

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

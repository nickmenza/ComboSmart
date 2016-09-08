Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Creates the ZIP object
			Dim zip As gxZip = new gxZip()

			' Opens the test02.zip file
            zip.Open("../../../../test02.zip")

            ' Queries info
			Console.WriteLine("ZIP Information: ( nentries={0}, commentlen={1} )", _
				                  zip.GetNEntries(), zip.GetCommentLength())

			' Retrieves the comment
			Console.WriteLine("ZIP Comment: '{0}'", zip.GetComment())

			' Lists files
			Dim st As Boolean = zip.FirstFile()
			While st:
				Dim fi As gxZipFileInfo = zip.GetFileInfo()
				Dim fdi As gxZipFileDetInfo = zip.GetFileDetInfo()
			
				Console.WriteLine( _
					       "{0}: comment='{1}', method={2}, CRC={3}, comp={4}, uncomp={5}", _
							zip.GetFileName(), zip.GetFileComment(), _
							fdi.method, fdi.crc, _
							fdi.compressed_size, fdi.uncompressed_size)
			
				st = zip.NextFile()
			End While

			' Closes the ZIP
			zip.Close()
	

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

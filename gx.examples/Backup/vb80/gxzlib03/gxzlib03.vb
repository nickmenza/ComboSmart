Imports System
Imports gx

Module Main
	Sub Main()
		Try

			' Creates the ZIP object
			Dim zlib As gxGZip = new gxGZip()
	
			' Opens the .gz file
            zlib.Open("../../../../test01.gz")

            ' Reads a piece of data
			Dim data() As byte = zlib.Read(128)
	
			' Writes data to stdout
			Console.Write("Uncompress size: {0}, data: ", data.Length)
			Dim ix As Integer
			Dim c As Char
			For ix = 0 To data.Length-1
                c = Chr(data.GetValue(ix))
				Console.Write(c)
			Next
			Console.WriteLine()
	
			' Closes the file
			zlib.Close()
	
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

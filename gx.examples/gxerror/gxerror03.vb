Imports System
Imports gx

Module Main
	Sub Main()
		Try
	
			' Sets some error code and description text (no format string)
			gxSystem.SetError(GX_ERROR_CODES.GX_EINVAL, "Line #1: Error occurred")
			gxSystem.AppendError(GX_ERROR_CODES.GX_EREAD, "Line #2: Read error occurred")
			gxSystem.PrependError(GX_ERROR_CODES.GX_EWRITE, "Line #0: Write error occurred")

			' Displays the error code and description text to STDOUT
			Console.WriteLine("Error code: {0}, Description: {1}", gxSystem.GetErrorCode(), gxSystem.GetErrorString())

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

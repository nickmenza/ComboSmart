Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Updates the system error buffer
			gxSystem.SetError(GX_ERROR_CODES.GX_EINVAL, "(Test string)")

			' Gets the error and displays it
			Console.WriteLine("The GX system's error buffer is: {0} {1}", gxSystem.GetErrorCode(), gxSystem.GetErrorString())

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Clear the error buffer
			gxSystem.ClearError()

			' Gets the error and displays it
			Console.WriteLine("1. Error code: {0}, Description: {1}", gxSystem.GetErrorCode(), gxSystem.GetErrorString())
			
			' Updates the system error buffer
			gxSystem.SetError(GX_ERROR_CODES.GX_EINVAL, "(Error occurred in my test application)")

			' Gets the error and displays it
			Console.WriteLine("2. Error code: {0}, Description: {1}", gxSystem.GetErrorCode(), gxSystem.GetErrorString())

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Creates the LOG
			Dim log As gxLog = new gxLog("default")

			' Writes some log record by the system logger
			log.WriteError("This is a simple error text")
	
			log.WriteError("This is an error text with parameters: " & 123 & "ABC" & 123.456)
	
			log.WriteWarning("This is a simple warning text")

			log.WriteWarning("This is a warning text with parameters: " & 123 & "ABC" & 123.456)

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

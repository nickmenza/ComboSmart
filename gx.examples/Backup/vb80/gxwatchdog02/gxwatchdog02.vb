Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' If the counter reaches zero the watchdog will try to reboot the computer
			Dim counter As Integer = 10
		
			Dim wd As gxWatchdog = New gxWatchdog("default")
			
			Dim i As Integer
			For i = 1 To 5
				Console.WriteLine(i & ". Updating counter to " & counter)
				wd.Set(counter)

				' Iteration()
				System.Threading.Thread.Sleep(1000)
			Next
		
			' Disables the watchdog device
			Console.WriteLine("Disabling the watchdog device..")
			wd.Set(0)

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

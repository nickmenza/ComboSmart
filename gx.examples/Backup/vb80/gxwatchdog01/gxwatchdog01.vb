Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' If the counter reaches zero the watchdog will try to reboot the computer
			Dim counter As integer = 5 ' 5 seconds
		
			Dim wd As gxWatchdog = new gxWatchdog("default")
		
			wd.Set(counter)
				
			System.Threading.Thread.Sleep(10*1000)

			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

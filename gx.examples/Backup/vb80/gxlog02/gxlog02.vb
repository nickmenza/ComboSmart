Imports System
Imports gx
Imports Microsoft.VisualBasic

Module Main
	Sub Main()
		Try
			' Creates the LOG
			Dim log As gxLog = new gxLog("default")

			' Sets parameters of the object
			log.SetProperty("ident", "my_test")
			log.SetProperty("file", "mylog.txt")
			log.SetProperty("format", "$y-$o-$d $h:$m:$s [$L] $M" & Chr(13) & Chr(10))
			log.SetProperty("filter", GX_LOGLEVELS.GX_INFO)
			log.SetProperty("maxfilesize", -1) ' no limit

			' Writes two log lines
			log.WriteInfo("This is the first line of the mylog.txt")
			log.WriteEmergency("Second line")
	
			Console.WriteLine("See the mylog.txt file!")
 	
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

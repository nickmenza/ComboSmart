'
' This example demonstrates: how to query information about the GX system
'
Imports System
Imports gx

Module Main
	Sub Main()
		Try
			gxSystem.ClearError()
            Console.WriteLine("GX licences in the system:")
            Console.WriteLine("Freeflow licences:          {0}", gxSystem.GetSystemInfo(gx.GX_SYSINFO_FLAGS.GX_SYSINFO_FLAG_FREEFLOW))
            Console.WriteLine("Parking licences:           {0}", gxSystem.GetSystemInfo(gx.GX_SYSINFO_FLAGS.GX_SYSINFO_FLAG_PARKING))
            Console.WriteLine("Container licences:         {0}", gxSystem.GetSystemInfo(gx.GX_SYSINFO_FLAGS.GX_SYSINFO_FLAG_ACCR))
            Console.WriteLine("Parking Lane licences:      {0}", gxSystem.GetSystemInfo(gx.GX_SYSINFO_FLAGS.GX_SYSINFO_FLAG_PARKINGLANE))
            Console.WriteLine("Face ident licences:        {0}", gxSystem.GetSystemInfo(gx.GX_SYSINFO_FLAGS.GX_SYSINFO_FLAG_FACE))
            Console.WriteLine("Passport Reader licences:   {0}", gxSystem.GetSystemInfo(gx.GX_SYSINFO_FLAGS.GX_SYSINFO_FLAG_PR))
        Catch e As Exception
            Console.WriteLine("Exception occurred: {0}", e.Message)
        End Try
	End Sub
End Module
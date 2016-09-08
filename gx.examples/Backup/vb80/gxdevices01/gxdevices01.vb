'
' This example demonstrates: how to query devices in the GX system
'
Imports System
Imports gx

Module Main
	Sub Main()
		Try
			gxSystem.ClearError()
			Console.WriteLine("Test: {0}", gxSystem.GetErrorString())

			Dim filter As gxDeviceInfo
            Dim devices() As gxDeviceInfo

            devices = gxSystem.ListDevices(Convert.ToUInt32(0), filter)

            If devices.Length = 0 Then
                Console.WriteLine("No GX devices found!")
            Else
                Console.WriteLine("Devices in the GX system:")
                Dim ix As Integer
                For ix = 0 To devices.Length - 1
                    Console.WriteLine("{0}. Name: {1}, Type: {2}, Serial: {3}, Prio: {4}", ix + 1, devices(ix).GetName(), devices(ix).GetType(), devices(ix).GetSerial(), devices(ix).GetPriority())
                Next
                Console.WriteLine("{0} devices found.", devices.Length)
            End If
        Catch e As Exception
			Console.WriteLine("Exception occurred: {0}", e.Message)
		End Try
	End Sub
End Module
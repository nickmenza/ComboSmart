'
' This example demonstrates: how to query devices in the GX system
'
Imports System
Imports gx
Imports Pr22
Module Main
    Private pr As DocumentReaderDevice = Nothing
    Sub Main()
        Console.WriteLine("dsad")
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

    Public Function Program() As Integer

        ' To open more than one device simultaneously, create more DocumentReaderDevice objects 

        System.Console.Out.WriteLine("Opening system")
        System.Console.Out.WriteLine()
        pr = New DocumentReaderDevice()

        'pr = New System.EventHandler(Of Pr22.Events.ConnectionEventArgs)(AddressOf onDeviceConnected)
        'pr.DeviceUpdate += New System.EventHandler(Of Pr22.Events.UpdateEventArgs)(AddressOf onDeviceUpdate)

        Dim deviceList As System.Collections.Generic.List(Of String) = DocumentReaderDevice.GetDeviceList()

        If deviceList.Count = 0 Then
            System.Console.Out.WriteLine("No device found!")
            Return 0
        End If

        System.Console.Out.WriteLine(deviceList.Count + " device" + (If(deviceList.Count > 1, "s", "")) + " found.")
        For Each devName As String In deviceList
            System.Console.Out.WriteLine(Convert.ToString("  Device: ") & devName)
        Next
        System.Console.Out.WriteLine()

        System.Console.Out.WriteLine("Connecting to the first device by its name: " + deviceList(0))
        System.Console.Out.WriteLine()
        System.Console.Out.WriteLine("If this is the first usage of this device on this PC,")
        System.Console.Out.WriteLine("the ""calibration file"" will be downloaded from the device.")
        System.Console.Out.WriteLine("This can take a while.")
        System.Console.Out.WriteLine()

        pr.UseDevice(deviceList(0))

        System.Console.Out.WriteLine("The device is opened.")

        System.Console.Out.WriteLine("Closing the device.")
        pr.Close()
        System.Console.Out.WriteLine()


        ' Opening the first device without using any device lists. 


        System.Console.Out.WriteLine("Connecting to the first device by its ordinal number: 0")

        pr.UseDevice(0)

        System.Console.Out.WriteLine("The device is opened.")

        System.Console.Out.WriteLine("Closing the device.")
        pr.Close()
        Return 0
    End Function

    Private Sub onDeviceConnected(a As Object, e As Pr22.Events.ConnectionEventArgs)
        System.Console.WriteLine("Connection event. Device number:{0}", e.DeviceNumber)
    End Sub
    Private Sub onDeviceUpdate(a As Object, e As Pr22.Events.UpdateEventArgs)
        System.Console.WriteLine("Update event.")
        Select Case e.part
            Case 1
                System.Console.WriteLine("  Reading calibration file from device.")
                Exit Select
            Case 2
                System.Console.WriteLine("  Scanner firmware update.")
                Exit Select
            Case 4
                System.Console.WriteLine("  RFID reader firmware update.")
                Exit Select
        End Select
    End Sub
    '----------------------------------------------------------------------
End Module
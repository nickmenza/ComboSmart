Imports Pr22
Public Class openscanner
    Private pr As DocumentReaderDevice = Nothing
    Public Function Open()
        System.Console.Out.WriteLine("Opening a device")
        System.Console.Out.WriteLine()
        pr = New DocumentReaderDevice()

        'pr.Connection += New System.EventHandler(Of Pr22.Events.ConnectionEventArgs)(AddressOf onDeviceConnected)
        'pr.DeviceUpdate += New System.EventHandler(Of Pr22.Events.UpdateEventArgs)(AddressOf onDeviceUpdate)

        Try
            pr.UseDevice(0)
        Catch generatedExceptionName As Pr22.Exceptions.NoSuchDevice
            System.Console.Out.WriteLine("No device found!")
            Return Nothing
        End Try

        System.Console.Out.WriteLine("The device {0} is opened.", pr.Peripherals.Info.DeviceName)

        Return pr
    End Function
End Class

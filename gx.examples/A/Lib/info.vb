Imports Pr22
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System
Public Class info
    Private pr As DocumentReaderDevice = Nothing

    '----------------------------------------------------------------------
    ''' <summary>
    ''' Opens the first document reader device.
    ''' </summary>
    ''' <returns></returns>
    Public Function Open() As Integer
        System.Console.Out.WriteLine("Opening a device")
        System.Console.Out.WriteLine()
        pr = New DocumentReaderDevice()

        'pr.Connection += New System.EventHandler(Of Pr22.Events.ConnectionEventArgs)(AddressOf onDeviceConnected)
        'pr.DeviceUpdate += New System.EventHandler(Of Pr22.Events.UpdateEventArgs)(AddressOf onDeviceUpdate)

        Try
            pr.UseDevice(0)
        Catch generatedExceptionName As Pr22.Exceptions.NoSuchDevice
            System.Console.Out.WriteLine("No device found!")
            Return 1
        End Try

        System.Console.Out.WriteLine("The device {0} is opened.", pr.Peripherals.Info.DeviceName)
        System.Console.Out.WriteLine()
        Return 0
    End Function
    '----------------------------------------------------------------------

    Public Function Program() As Integer
        'Devices can be manipulated only after opening.
        If Open() <> 0 Then
            Return 1
        End If

        System.Console.Out.WriteLine("SDK versions:")
        System.Console.Out.WriteLine(vbTab & "Assembly: ")
        System.Console.Out.WriteLine(vbTab & "Interface: " & pr.GetVersion("A"c))
        System.Console.Out.WriteLine(vbTab & "System: " & pr.GetVersion("S"c))
        System.Console.Out.WriteLine()

        Dim scannerinfo As Pr22.DocScanner.Information = pr.Scanner.Info

        'Devices provide proper image quality only if they are calibrated.
        'Devices are calibrated by default. If you receive the message "not calibrated"
        'then please contact your hardware supplier.
        System.Console.Out.WriteLine("Calibration state of the device:")
        If scannerinfo.IsCalibrated() Then
            System.Console.Out.WriteLine(vbTab & "calibrated")
        Else
            System.Console.Out.WriteLine(vbTab & "not calibrated")
        End If
        System.Console.Out.WriteLine()

        System.Console.Out.WriteLine("Available lights for image scanning:")
        Dim lights As List(Of Pr22.Imaging.Light) = scannerinfo.GetLights()
        For Each light As Pr22.Imaging.Light In lights
            System.Console.Out.WriteLine(vbTab & light)
        Next
        System.Console.Out.WriteLine()

        System.Console.Out.WriteLine("Available object windows for image scanning:")
        For i As Integer = 0 To scannerinfo.GetWindowCount() - 1
            Dim frame As System.Drawing.Rectangle = scannerinfo.GetSize(i)
            System.Console.Out.WriteLine(vbTab & i & ": " & frame.Width / (1000.0F) & " x " & frame.Height / 1000.0F & " mm")
        Next
        System.Console.Out.WriteLine()

        System.Console.Out.WriteLine("Scanner component versions:")
        System.Console.Out.WriteLine(vbTab & "Firmware: " & scannerinfo.GetVersion("F"c))
        System.Console.Out.WriteLine(vbTab & "Hardware: " & scannerinfo.GetVersion("H"c))
        System.Console.Out.WriteLine(vbTab & "Software: " & scannerinfo.GetVersion("S"c))
        System.Console.Out.WriteLine()

        System.Console.Out.WriteLine("Available card readers:")
        Dim readers As List(Of ECardReader) = pr.Readers
        For i As Integer = 0 To readers.Count - 1
            System.Console.Out.WriteLine(vbTab & i & ": " & Convert.ToString(readers(i).Info.HwType))
        Next
        System.Console.Out.WriteLine()

        System.Console.Out.WriteLine("Available status LEDs:")
        Dim leds As List(Of Pr22.Control.StatusLed) = pr.Peripherals.StatusLeds
        For i As Integer = 0 To leds.Count - 1
            System.Console.Out.WriteLine(vbTab & i & ": color " & Convert.ToString(leds(i).Light))
        Next
        System.Console.Out.WriteLine()

        System.Console.Out.WriteLine("Closing the device.")
        pr.Close()
        Return 0
    End Function
    '----------------------------------------------------------------------
    ' Event handlers
    '----------------------------------------------------------------------

    Private Sub onDeviceConnected(a As Object, e As Pr22.Events.ConnectionEventArgs)
        System.Console.WriteLine("Connection event. Device number:{0}", e.DeviceNumber)
    End Sub
    '----------------------------------------------------------------------

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



End Class

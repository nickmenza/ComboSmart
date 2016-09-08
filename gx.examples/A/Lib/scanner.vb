Imports Pr22
Imports System
Public Class scanner
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
        System.Console.Out.WriteLine("This tutorial guides you through a complex image scanning process.")
        System.Console.Out.WriteLine("This will demonstrate all possible options of page management.")
        System.Console.Out.WriteLine("The stages of the scan process will be saved into separate zip files")
        System.Console.Out.WriteLine("in order to provide the possibility of comparing them to each other.")
        System.Console.Out.WriteLine()

        'Devices can be manipulated only after opening.
        If Open() <> 0 Then
            Return 1
        End If

        'Subscribing to scan events
        'pr.ScanStarted += New System.EventHandler(Of Pr22.Events.PageEventArgs)(AddressOf ScanStarted)
        'pr.ImageScanned += New System.EventHandler(Of Pr22.Events.ImageEventArgs)(AddressOf ImageScanned)
        'pr.ScanFinished += New System.EventHandler(Of Pr22.Events.PageEventArgs)(AddressOf ScanFinished)
        'pr.DocFrameFound += New System.EventHandler(Of Pr22.Events.PageEventArgs)(AddressOf DocFrameFound)

        Dim Scanner As DocScanner = pr.Scanner

        'first page
        If True Then
            Dim FirstTask As New Pr22.Task.DocScannerTask()

            System.Console.Out.WriteLine("At first the device scans only a white image...")
            FirstTask.Add(Pr22.Imaging.Light.White)
            Dim page1 As Pr22.Processing.Page = Scanner.Scan(FirstTask, Pr22.Imaging.PagePosition.First)

            System.Console.Out.WriteLine("And then the program saves it as a PNG file.")
            page1.[Select](Pr22.Imaging.Light.White).GetImage().Save(Pr22.Imaging.RawImage.FileFormat.Png).Save("white.png")

            System.Console.Out.WriteLine("Saving stage 1.")
            pr.Engine.GetRootDocument().Save(Pr22.Processing.Document.FileFormat.Zipped).Save("1stScan.zip")
            System.Console.Out.WriteLine()

            System.Console.Out.WriteLine("If scanning of an additional infra image of the same page is required...")
            System.Console.Out.WriteLine("We need to scan it into the current page.")
            FirstTask.Add(Pr22.Imaging.Light.Infra)
            Scanner.Scan(FirstTask, Pr22.Imaging.PagePosition.Current)

            System.Console.Out.WriteLine("Saving stage 2.")
            pr.Engine.GetRootDocument().Save(Pr22.Processing.Document.FileFormat.Zipped).Save("2ndScan.zip")
            System.Console.Out.WriteLine()
        End If

        'second page
        If True Then
            System.Console.Out.WriteLine("At this point, if scanning of an additional page of the document is needed")
            System.Console.Out.WriteLine("with all of the available lights except the infra light.")
            System.Console.Out.WriteLine("It is recommended to executed in one scan process - as it is the fastest in such a way.")
            Dim SecondTask As New Pr22.Task.DocScannerTask()
            SecondTask.Add(Pr22.Imaging.Light.All).Del(Pr22.Imaging.Light.Infra)
            System.Console.Out.WriteLine()

            System.Console.Out.WriteLine("At this point, the user have to change the document on the reader then press any key.")
            System.Console.ReadKey(True)

            System.Console.Out.WriteLine("Scanning the images.")
            Scanner.Scan(SecondTask, Pr22.Imaging.PagePosition.[Next])

            System.Console.Out.WriteLine("Saving stage 3.")
            pr.Engine.GetRootDocument().Save(Pr22.Processing.Document.FileFormat.Zipped).Save("3rdScan.zip")
            System.Console.Out.WriteLine()

            System.Console.Out.WriteLine("Upon putting incorrect page on the scanner, the scanned page has to be removed.")
            Scanner.CleanUpLastPage()

            System.Console.Out.WriteLine("And the user have to change the document on the reader again then press any key.")
            System.Console.ReadKey(True)

            System.Console.Out.WriteLine("Scanning...")
            Scanner.Scan(SecondTask, Pr22.Imaging.PagePosition.[Next])

            System.Console.Out.WriteLine("Saving stage 4.")
            pr.Engine.GetRootDocument().Save(Pr22.Processing.Document.FileFormat.Zipped).Save("4thScan.zip")
            System.Console.Out.WriteLine()
        End If

        System.Console.Out.WriteLine("Scanning processes are finished.")
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

    Private Sub ScanStarted(a As Object, e As Pr22.Events.PageEventArgs)
        System.Console.WriteLine("Scan started. Page:{0}", e.Page)
    End Sub
    '----------------------------------------------------------------------

    Private Sub ImageScanned(a As Object, e As Pr22.Events.ImageEventArgs)
        System.Console.WriteLine("Image scanned. Page:{0} Light:{1}", e.Page, e.Light)
        Dim img As Pr22.Imaging.RawImage = DirectCast(a, DocumentReaderDevice).Scanner.GetPage(e.Page).[Select](e.Light).GetImage()
        img.Save(Pr22.Imaging.RawImage.FileFormat.Bmp).Save("page_" & Convert.ToString(e.Page) & "_light_" & Convert.ToString(e.Light) & ".bmp")
    End Sub
    '----------------------------------------------------------------------

    Private Sub ScanFinished(a As Object, e As Pr22.Events.PageEventArgs)
        System.Console.WriteLine("Page scanned. Page:{0} Status:{1}", e.Page, e.Status)
    End Sub
    '----------------------------------------------------------------------

    Private Sub DocFrameFound(a As Object, e As Pr22.Events.PageEventArgs)
        System.Console.WriteLine("Document frame found. Page:{0}", e.Page)
    End Sub
    '----------------------------------------------------------------------
End Class

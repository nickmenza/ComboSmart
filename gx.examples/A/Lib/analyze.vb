Imports Pr22
Imports Pr22.Processing
Public Class analyze
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
    Public Function Test_Scanner(value) As Array
        pr = value
        Dim passport_array As Array
        If pr IsNot Nothing Then
            Dim Scanner As DocScanner = pr.Scanner
            Dim OcrEngine As Engine = pr.Engine

            System.Console.WriteLine("Capturing some images to read from.")
            Dim ScanTask As New Pr22.Task.DocScannerTask()

            'For OCR (MRZ) reading purposes, IR (infrared) image is recommended.
            ScanTask.Add(Pr22.Imaging.Light.White).Add(Pr22.Imaging.Light.Infra)

            Dim DocPage As Page = Scanner.Scan(ScanTask, Pr22.Imaging.PagePosition.First)
            System.Console.WriteLine()

            ' Pictuce VizFace
            System.Console.WriteLine("Reading all the textual and graphical field data as well as " + "authentication result from the Visual Inspection Zone.")
            Dim VIZReadingTask As New Pr22.Task.EngineTask()
            VIZReadingTask.Add(FieldSource.Viz, FieldId.All)
            Dim VizDoc As Document = OcrEngine.Analyze(DocPage, VIZReadingTask)
            PrintDocFields(VizDoc)
            VizDoc.Save(Document.FileFormat.Xml).Save("VIZ.xml")

            ' Data Person   'Specify the fields we would like to receive.
            System.Console.WriteLine("Reading all the field data of the Machine Readable Zone.")
            Dim MrzReadingTask As New Pr22.Task.EngineTask()
            MrzReadingTask.Add(FieldSource.Mrz, FieldId.All)
            Dim MrzDoc As Document = OcrEngine.Analyze(DocPage, MrzReadingTask)

            passport_array = PrintDocFields(MrzDoc)
            'For Each num In passport_array 'test for array
            'Console.WriteLine(num)
            'Next
            System.Console.WriteLine("Scanner finished")
            Return passport_array
        Else
            passport_array = Nothing
            Return passport_array
        End If


    End Function
    Public Function Program() As Array
        'Devices can be manipulated only after opening.
        If Open() <> 0 Then
            Console.WriteLine("No device")
        Else

            'Subscribing to scan events
            'pr.ScanStarted += New System.EventHandler(Of Pr22.Events.PageEventArgs)(AddressOf ScanStarted)
            'pr.ImageScanned += New System.EventHandler(Of Pr22.Events.ImageEventArgs)(AddressOf ImageScanned)
            'pr.ScanFinished += New System.EventHandler(Of Pr22.Events.PageEventArgs)(AddressOf ScanFinished)
            'pr.DocFrameFound += New System.EventHandler(Of Pr22.Events.PageEventArgs)(AddressOf DocFrameFound)

            Dim Scanner As DocScanner = pr.Scanner
            Dim OcrEngine As Engine = pr.Engine

            System.Console.WriteLine("Capturing some images to read from.")
            Dim ScanTask As New Pr22.Task.DocScannerTask()

            'For OCR (MRZ) reading purposes, IR (infrared) image is recommended.
            ScanTask.Add(Pr22.Imaging.Light.White).Add(Pr22.Imaging.Light.Infra)

            Dim DocPage As Page = Scanner.Scan(ScanTask, Pr22.Imaging.PagePosition.First)
            System.Console.WriteLine()

            System.Console.WriteLine("Reading all the field data of the Machine Readable Zone.")
            Dim MrzReadingTask As New Pr22.Task.EngineTask()


            System.Console.WriteLine("Capturing more images for VIZ reading and image authentication.")
            ScanTask = New Pr22.Task.DocScannerTask()
            'Reading from VIZ -except face photo- Is available in special OCR engines only.
            ScanTask.Add(Pr22.Imaging.Light.All)
            DocPage = Scanner.Scan(ScanTask, Pr22.Imaging.PagePosition.Current)
            System.Console.WriteLine()

            System.Console.WriteLine("Reading all the textual and graphical field data as well as " +
                "authentication result from the Visual Inspection Zone.")
            Dim VIZReadingTask As New Pr22.Task.EngineTask()
            VIZReadingTask.Add(FieldSource.Viz, FieldId.All)


            'Specify the fields we would like to receive.
            MrzReadingTask.Add(FieldSource.Mrz, FieldId.All)
            Dim MrzDoc As Document = OcrEngine.Analyze(DocPage, MrzReadingTask)
            Dim passport_array As Array
            passport_array = PrintDocFields(MrzDoc)
            'For Each num In passport_array 'test for array
            'Console.WriteLine(num)
            'Next
            System.Console.WriteLine("Scanner finished")
            pr.Close()
            Return passport_array


        End If


    End Function
    '----------------------------------------------------------------------
    ''' <summary>
    ''' Prints a hexa dump line from a part of an array.
    ''' </summary>
    ''' <param name="arr">The whole array.</param>
    ''' <param name="pos">Position of the first item to print.</param>
    ''' <param name="sz">Number of items to print.</param>
    Private Shared Sub PrintBinary(arr As Byte(), pos As Integer, sz As Integer)
        Dim p0 As Integer
        p0 = pos
        While p0 < arr.Length AndAlso p0 < pos + sz
            System.Console.Write(arr(p0).ToString("X2") & " ")
            p0 += 1
        End While

        While p0 < pos + sz
            System.Console.Write("   ")
            p0 += 1
        End While

        p0 = pos
        While p0 < arr.Length AndAlso p0 < pos + sz
            System.Console.Write(If(arr(p0) < &H21 OrElse arr(p0) > &H7E, "."c, ChrW(arr(p0))))
            p0 += 1
        End While

        System.Console.WriteLine()
    End Sub
    '----------------------------------------------------------------------
    ''' <summary>
    ''' Prints out all fields of a document structure to console.
    ''' </summary>
    ''' <remarks>
    ''' Values are printed in three different forms: raw, formatted and standardized.
    ''' Status (checksum result) is printed together with fieldname and raw value.
    ''' At the end, images of all fields are saved into png format.
    ''' </remarks>
    ''' <param name="doc"></param>
    Public Function PrintDocFields(doc As Document) As Array
        Dim passport_array(7) As String
        Dim Fields As System.Collections.Generic.List(Of FieldReference) = doc.GetFields()

        'System.Console.WriteLine("  {0, -20}{1, -17}{2}", "FieldId", "Status", "Value")
        'System.Console.WriteLine("  {0, -20}{1, -17}{2}", "-------", "------", "-----")
        'System.Console.WriteLine()

        For Each CurrentFieldRef As FieldReference In Fields
            Try

                Dim CurrentField As Field = doc.GetField(CurrentFieldRef)
                Dim Value As String = "", FormattedValue As String = "", StandardizedValue As String = ""
                Dim binValue As Byte() = Nothing
                Try
                    Value = CurrentField.GetStandardizedStringValue()
                Catch generatedExceptionName As Pr22.Exceptions.EntryNotFound
                Catch generatedExceptionName As Pr22.Exceptions.InvalidParameter
                    binValue = CurrentField.GetBinaryValue()
                End Try
                Try
                    FormattedValue = CurrentField.GetFormattedStringValue()
                Catch generatedExceptionName As Pr22.Exceptions.EntryNotFound
                End Try
                Try
                    StandardizedValue = CurrentField.GetStandardizedStringValue()
                Catch generatedExceptionName As Pr22.Exceptions.EntryNotFound
                End Try
                Dim Status As Status = Status.NoChecksum
                Try
                    Status = CurrentField.GetStatus()
                Catch generatedExceptionName As Pr22.Exceptions.EntryNotFound
                End Try
                Dim Fieldname As String = CurrentFieldRef.ToString()
                If binValue IsNot Nothing Then

                Else
                    Select Case Fieldname
                        Case "MrzGivenname"
                            passport_array(0) = FormattedValue
                            'array.Add(FormattedValue)
                            'Console.WriteLine("Firstname")
                        Case "MrzSurname"
                            passport_array(1) = FormattedValue
                            'array.Add(FormattedValue)
                        Case "MrzDocumentNumber"
                            passport_array(2) = FormattedValue
                            'array.Add(FormattedValue)
                        Case "MrzIssueCountry"
                            passport_array(3) = FormattedValue
                            'array.Add(FormattedValue)
                        Case "MrzPersonalData1"
                            passport_array(4) = FormattedValue
                            'array.Add(FormattedValue)
                        Case "MrzBirthDate"
                            passport_array(5) = DAY_MONTH_YEAR(FormattedValue)
                            'array.Add(FormattedValue)
                        Case "MrzExpiryDate"
                            passport_array(6) = DAY_MONTH_YEAR(FormattedValue)
                            'array.Add(FormattedValue)
                        Case "MrzSex"
                            passport_array(7) = mr_or_miss(FormattedValue)
                            'array.Add(FormattedValue)

                        Case Else

                    End Select


                    'System.Console.WriteLine("test" + Value)
                    'System.Console.WriteLine("  {0, -20}{1, -17}[{2}]", Fieldname, Status, FormattedValue)
                    'System.Console.WriteLine(vbTab & "{1, -31}[{0}]", FormattedValue, "   - Formatted")
                    'System.Console.WriteLine(vbTab & "{1, -31}[{0}]", StandardizedValue, "   - Standardized")

                End If
                Try
                    CurrentField.GetImage().Save(Pr22.Imaging.RawImage.FileFormat.Png).Save(Fieldname & ".png")
                Catch generatedExceptionName As Pr22.Exceptions.General
                End Try
            Catch generatedExceptionName As Pr22.Exceptions.General
            End Try
        Next
        System.Console.WriteLine()

        For Each comp As FieldCompare In doc.GetFieldCompareList()
            'System.Console.WriteLine((("Comparing " & comp.field1 & " vs. ") + comp.field2 & " results ") + comp.confidence)
        Next
        System.Console.WriteLine()
        Return passport_array
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
    '---------------BIRTHDAY-----------------------------------------------
    Private Function DAY_MONTH_YEAR(value) As String
        Dim Day As String = value.Substring(4, 2)
        Dim month As String = value.Substring(2, 2)
        Dim year As String = value.Substring(0, 2)
        Dim birthday_new As String

        Select Case month
            Case "01"
                month = "JAN"
            Case "02"
                month = "FEB"
            Case "03"
                month = "MAR"
            Case "04"
                month = "APR"
            Case "05"
                month = "MAY"
            Case "06"
                month = "JUN"
            Case "07"
                month = "JUL"
            Case "08"
                month = "AUG"
            Case "09"
                month = "SEP"
            Case "10"
                month = "OCT"
            Case "11"
                month = "NOV"
            Case "12"
                month = "DEC"
            Case Else
                month = Nothing
        End Select

        If year > "60" Then
            year = "19" + year
        Else
            year = "20" + year
        End If
        birthday_new = Day + " " + month + " " + year
        Console.WriteLine(birthday_new)
        Return birthday_new
    End Function
    '----------------------------------------------------------------------

    Public Function mr_or_miss(value)

        Dim result As String
        If value = "M" Then
            result = "Mr"
        Else
            result = "Mrs"
        End If
        Return result
    End Function
    '----------------------------------------------------------------------

End Class

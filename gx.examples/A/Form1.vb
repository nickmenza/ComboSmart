Imports Pr22
Public Class Form1
    Private pr As DocumentReaderDevice = Nothing
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim openscanner As New openscanner
        pr = openscanner.Open()
    End Sub
    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed
        pr.Close()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim passport_array As Array
        Dim analyze As New analyze()
        Dim appPath As String = My.Application.Info.DirectoryPath + "\VizFace.png"
        'Console.WriteLine(appPath)
        'passport_array = analyze.Program()
        passport_array = analyze.Test_Scanner(pr)
        Dim number_check As Integer = 0
        Console.WriteLine(pr)
        'check connect scanner 
        If pr IsNot Nothing Then

            'check scanner person sucess
            For Each num In passport_array
                If num IsNot Nothing Then
                    number_check = number_check + 1

                End If
            Next
            If number_check > 5 Then
                Label1.Text = passport_array(7) + "." + passport_array(0) 'firstname
                Label2.Text = passport_array(1) 'lastname
                Label3.Text = passport_array(2) 'passportId
                Label4.Text = passport_array(3) 'country
                Label5.Text = passport_array(4) 'identification
                Label6.Text = passport_array(5) 'date of birth
                Label7.Text = passport_array(6) 'date of expiry
                Label8.Text = passport_array(7) 'sex
                PictureBox1.ImageLocation = appPath
                Console.WriteLine("Person data")
            Else

                Label1.Text = Nothing
                Label2.Text = Nothing
                Label3.Text = Nothing
                Label4.Text = Nothing
                Label5.Text = Nothing
                Label6.Text = Nothing
                Label7.Text = Nothing
                PictureBox1.ImageLocation = Nothing
                Console.WriteLine("No data")
            End If
        Else
            Console.WriteLine("No Device")
        End If

    End Sub

End Class

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim passport As Array
        Dim analyze As New analyze()
        passport = analyze.Program()
        Dim Form1 = New Form1

        If (passport IsNot Nothing) Then
            Label1.Text = passport(0)
            Label2.Text = passport(1)
            Label3.Text = passport(2)
            Label4.Text = passport(3)
            Label5.Text = passport(4)
            Label6.Text = passport(5)
            Label7.Text = passport(6)
        End If

        'Label1.DataBindings.Add(passport(0))
    End Sub

End Class
Imports System
Imports Pr22

Module Module1

    Sub Main()

        Dim open As New open
        Dim info As New info
        Dim scanner As New scanner()
        Dim analyze As New analyze()
        'analyze.Program()
        'scanner.Program()
        'scanner.Test()
        'info.Program()
        'open.Program()
        Dim ass As String = "701130"
        birthday(ass)
        Console.ReadLine()

    End Sub

    Private Function birthday(value) As String
        Dim Day As String = value.Substring(4, 2)
        Dim month As String = value.Substring(2, 2)
        Dim year As String = value.Substring(0, 2)
        Dim birthday_new As String
        Dim Year_now As String
        Year_now = Now.Year

        Console.Write(Year_now)
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

        If Year_now.Substring(2, 2) > year Then
            year = (Year_now.Substring(0, 2) - 1) & year
        Else
            year = (Year_now.Substring(0, 2)) & year
        End If

        birthday_new = Day + " " + month + " " + year
        Console.WriteLine(birthday_new)
        Return birthday_new
    End Function
End Module

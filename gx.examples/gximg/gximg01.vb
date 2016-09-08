Imports System
Imports gx

Module Main
    Sub Main()
        Try
            ' Creates a new image class with default properties
            Dim image As gxImage = New gxImage("default")

            ' Loads the sample image
            image.Load("../../../../../../../data/mushroom.jpg")

            ' Converts it to grayscale
            image.Convert(GX_PIXELFORMATS.GX_GRAY)

            ' Saves it
            image.Save("test.bmp", GX_IMGFILEFORMATS.GX_BMP)

        Catch ex As Exception
            Console.WriteLine("An error occurred: {0}", ex.Message)
        End Try
    End Sub
End Module

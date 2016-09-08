Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Creates an image handler with default properties
			Dim imghandler_jpeg10 As gxImageHandler = new gxImageHandler("default")
			' Updates the JPEG quality to 10
			imghandler_jpeg10.SetProperty("jpeg/compress/quality", 10)
	
			' Creates an image handler with default properties
			Dim imghandler_jpeg80 As gxImageHandler = new gxImageHandler("default")
			' Updates the JPEG quality to 80
			imghandler_jpeg80.SetProperty("jpeg/compress/quality", 80)
	
			' Creates an image with the first image handler (where the JPEG quality is 10)
			Dim image1 As gxImage = new gxImage(imghandler_jpeg10)
			' Creates an image with the second image handler (where the JPEG quality is 80)
			Dim image2 As gxImage = new gxImage(imghandler_jpeg80)
	
			' Loads the sample image
            image1.Load("../../../../../../../data/mushroom.jpg")
            ' Saves it to JPEG format (where the quality is 10)
			image1.Save("test_jpeg10.jpg", GX_IMGFILEFORMATS.GX_JPEG)

			' Loads the sample image
            image2.Load("../../../../../../../data/mushroom.jpg")
            ' Saves it to JPEG format (where the quality is 80)
			image2.Save("test_jpeg80.jpg", GX_IMGFILEFORMATS.GX_JPEG)
				
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

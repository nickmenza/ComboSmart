Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Creates a new image handler with default properties
			Dim imghandler As gxImageHandler = new gxImageHandler("default")

			' Creates an image handler for linear zoom
			Dim linearhndl As gxImageHandler = new gxImageHandler("default")
			' Updates the zoom mode
			linearhndl.SetProperty("zoom_mode", GX_ZOOMMODES.GX_ZOOM_LINEAR)
	
			' Creates an image handler for nearest zoom
			Dim nearesthndl As gxImageHandler = new gxImageHandler("default")
			' Updates the zoom mode
			nearesthndl.SetProperty("zoom_mode", GX_ZOOMMODES.GX_ZOOM_NEAREST)
	
			' Creates a new image object
			Dim image As gxImage = new gxImage(imghandler)
			' Loads the sample image
            image.Load("../../../../../../../data/mushroom.jpg")

			' Creates an image object for linear zoom operations
			Dim linearzoomed As gxImage = new gxImage(linearhndl)
			' Creates an image object for nearest zoom operations
			Dim nearestzoomed As gxImage = new gxImage(nearesthndl)
	
			' Diminutives and saves the image
			linearzoomed.Zoom(image, image.GetXSize()/4, image.GetYSize()/4, 0)
			linearzoomed.Save("test_lzoom1.jpg", GX_IMGFILEFORMATS.GX_JPEG)
	
			nearestzoomed.Zoom(image, image.GetXSize()/4, image.GetYSize()/4, 0)
			nearestzoomed.Save("test_nzoom1.jpg", GX_IMGFILEFORMATS.GX_JPEG)
	
			' Magnifies and saves the image
			linearzoomed.Zoom(image, image.GetXSize()*4, image.GetYSize()*4, 0)
			linearzoomed.Save("test_lzoom2.jpg", GX_IMGFILEFORMATS.GX_JPEG)

			nearestzoomed.Zoom(image, image.GetXSize()*4, image.GetYSize()*4, 0)
			nearestzoomed.Save("test_nzoom2.jpg", GX_IMGFILEFORMATS.GX_JPEG)
	
			' Zooms an area (58x35 - 316x191) and saves it
			linearzoomed.Zoom(image, 258, 156, 0,   256*58, 256*35, 256*(58+258), 256*(35+156))
			linearzoomed.Save("test_lzoom3.jpg", GX_IMGFILEFORMATS.GX_JPEG)

			nearestzoomed.Zoom(image, 258, 156, 0,   256*58, 256*35, 256*(58+258), 256*(35+156))
			nearestzoomed.Save("test_nzoom3.jpg", GX_IMGFILEFORMATS.GX_JPEG)
					
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

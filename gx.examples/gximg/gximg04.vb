Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Creates a new image handler with default properties
			Dim imghandler As gxImageHandler = new gxImageHandler("default")
	
			' Creates a new image object
			Dim image As gxImage = new gxImage(imghandler)
			' Loads the sample image
            image.Load("../../../../../../../data/mushroom.jpg")
            
			' Creates an image object to work
			Dim workimg As gxImage = new gxImage(imghandler)
	
			' Rotates and saves the image
			Dim degree As Integer
			For degree = 90 To 360 Step 90
				' Rotates the image
			 	workimg.Rotate(image, degree)
		
				' Saves it
				Dim fname As String = "test_rotate" & degree & ".jpg"
				workimg.Save(fname, GX_IMGFILEFORMATS.GX_JPEG)
			Next
	
	
			' Mirrors the image
			workimg.Mirror(image, GX_MIRRORFLAGS.GX_MIRROR_VERTICAL)
	
			' Saves it
			workimg.Save("test_mirror.jpg", GX_IMGFILEFORMATS.GX_JPEG)
 					
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

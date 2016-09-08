Imports System
Imports gx

Module Main
	Sub Main()
		' If the difference greater than or equal with this define the program
		' prints 'MOTION'
		Dim MOTION_OCCURRED As Integer = 10
		
		Try
			' Creates the motion detector object
			Dim motdet As gxMotionDetector = new gxMotionDetector("default")
            motdet.MPStartTransaction()
            motdet.SetProperty("xsize", 352)    ' Width and height of images
			motdet.SetProperty("ysize", 288)
			motdet.SetProperty("scale_x", 8)	' Diminutives to 44x36
			motdet.SetProperty("scale_y", 8)
			motdet.SetProperty("block_x", 4)	' Block size is 4x4 pixel
			motdet.SetProperty("block_y", 4)
			motdet.SetProperty("contrast_min", 5)	' The minimal contrast is five in grayscale level
			motdet.SetProperty("sensibility", 10)	' The block sensivility for the area
            motdet.MPCommit()

			' Creates the image
			Dim image As gxImage = new gxImage("default")
	
			Dim image_ix As Integer = 0	' Index for images
			Dim fname As string = ""	' Filename of an image

			Dim result As gxMotionDetectorResult = New gxMotionDetectorResult()
			Dim st As Boolean = True
			While st		' Infinite loop
				image_ix = image_ix+1
				
                fname = "../../../../../../../data/frames/frame_"
                If image_ix < 10 Then
                    fname = fname & "0"
                End If
				fname = fname & image_ix & ".jp2"
		
				' Loads a still
				Try
					st = image.Load(fname, GX_PIXELFORMATS.GX_GRAY)
				Catch ex As exception:
					If image_ix = 1 Then
						Console.WriteLine("Load() error: {0}", gxSystem.GetErrorString())
						' An error occurred in the image.Load()
						st = False
					Else				
						Console.WriteLine("{0}: No image, jump to first", fname)
			
						' Sets index to first image
						image_ix = 0
						st = True
					End If
				End Try
				
				If st Then		
					' Puts the still to motion detector module
					motdet.Iteration(image, result)
		
					' If the max_difference greater than zero (or a value what you
					' specified your program) then motion occurred.
					If result.GetMaxDifference() > MOTION_OCCURRED Then
						Console.WriteLine("{0}: MOTION! Max: {1}, Min: {2}, ( {3}x{4} - {5}x{6} )", _
							fname, _
							result.GetMaxDifference(), result.GetMinDifference(), _
							result.GetFoundX1(), result.GetFoundY1(), _
							result.GetFoundX2(), result.GetFoundY2())
					Else
						Console.WriteLine("{0}: Max: {1}, Min: {2}", _
							fname, _
							result.GetMaxDifference(), result.GetMinDifference())
					End If
					
					System.Threading.Thread.Sleep(500)
				End If	
			End While

		Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

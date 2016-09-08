Imports System
Imports gx

Module Main
	' Right shifts an image 
	Sub RShiftImage(image As gxImage, shift As Integer)
		If image.GetPixelFormat() <> GX_PIXELFORMATS.GX_GRAY Then
			Console.WriteLine("** Invalid pixel format")
			System.Threading.Thread.CurrentThread.Abort()
		Else
			Dim pixels() As Byte = image.GetPixels()
			Dim ix As Integer = (pixels.Length-shift)-1
			
			While ix > 0
				pixels.SetValue(pixels.GetValue(ix), ix+shift)
				ix = ix-1
			End While
			
			image.LoadFromMem(pixels, -1)
		End If			
	End Sub

	' Creates a motion stream from a PNG
	Sub CreateStills
		Dim image As gxImage = new gxImage("default")
        image.Load("../../../../../../../data/slow_src.png", GX_PIXELFORMATS.GX_UNDEF)

		Dim ix As Integer
		For ix = 1 To 50 Step 1
			Dim fname As String = "slow_"
			If ix < 10 Then
				fname = fname & "0"
			End If
			fname = fname & ix & ".png"
			Console.WriteLine("Creating {0}..", fname)
			image.Save(fname, GX_IMGFILEFORMATS.GX_PNG)
			RShiftImage(image, 3)
		Next
	End Sub

	Sub Main()
		' If the difference greater than or equal with this define the program
		' prints 'MOTION'
		Dim MOTION_OCCURRED As Integer = 50
		
		Try
			' Creates stills from the original PNG
			CreateStills()

			' Creates the motion detector object for fast motions
            Dim md_fast As gxMotionDetector = New gxMotionDetector("default")
            md_fast.MPStartTransaction()
			md_fast.SetProperty("xsize", 640)	' Width and height of images
			md_fast.SetProperty("ysize", 480)
			md_fast.SetProperty("scale_x", 8)	' Diminutives to 44x36
			md_fast.SetProperty("scale_y", 8)
			md_fast.SetProperty("block_x", 4)	' Block size is 4x4 pixel
			md_fast.SetProperty("block_y", 4)
			md_fast.SetProperty("contrast_min", 1)	' The minimal contrast is five in grayscale level
            md_fast.SetProperty("sensibility", 20)  ' The block sensivility for the area
            md_fast.MPCommit()

			' Creates the motion detector object for slow motions
            Dim md_slow As gxMotionDetector = New gxMotionDetector("default")
            md_slow.MPStartTransaction()
			md_slow.SetProperty("xsize", 640)	' Width and height of images
			md_slow.SetProperty("ysize", 480)
			md_slow.SetProperty("scale_x", 8)	' Diminutives to 44x36
			md_slow.SetProperty("scale_y", 8)
			md_slow.SetProperty("block_x", 4)	' Block size is 4x4 pixel
			md_slow.SetProperty("block_y", 4)
			md_slow.SetProperty("contrast_min", 1)	' The minimal contrast is five in grayscale level
			md_slow.SetProperty("sensibility", 20)	' The block sensivility for the area
            md_slow.MPCommit()

			' Creates the image
			Dim image As gxImage = new gxImage("default")
	
			Dim image_ix As Integer = 5	' Index for images
			Dim image_counter As Integer = 0	' Counter for images
			Dim fname As string = ""	' Filename of an image

			' Results for fast and slow motions
			Dim res_fast As gxMotionDetectorResult = New gxMotionDetectorResult()
			Dim res_slow As gxMotionDetectorResult = new gxMotionDetectorResult()

			Dim st As Boolean = True
			While st		' Infinite loop
				image_ix = image_ix+1

				fname = "slow_"
				If image_ix < 10 Then
					fname = fname & "0"
				End If
				fname = fname & image_ix & ".png"
		
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
				
				If st And (image_ix <> 0) Then		
					' Puts the still to motion detector module
					md_fast.Iteration(image, res_fast)

					' If the max_difference greater than zero (or a value what you
					' specified your program) then motion occurred.
					If res_fast.GetMaxDifference() > MOTION_OCCURRED Then
						Console.WriteLine("{0}: f> MOTION! Max: {1}, Min: {2}, ( {3}x{4} - {5}x{6} )", _
							fname, _
							res_fast.GetMaxDifference(), res_fast.GetMinDifference(), _
							res_fast.GetFoundX1(), res_fast.GetFoundY1(), _
							res_fast.GetFoundX2(), res_fast.GetFoundY2())
					Else
						Console.WriteLine("{0}: f> Max: {1}, Min: {2}", _
							fname, _
							res_fast.GetMaxDifference(), res_fast.GetMinDifference())
					End If

					' Check one in ten images (it detects the slow motion)
					image_counter = image_counter+1
					If (image_counter Mod 10) = 1 Then
						md_slow.Iteration(image, res_slow)

						' If the max_difference greater than zero (or a value what you
						' specified your program) then motion occurred.
						If res_slow.GetMaxDifference() > MOTION_OCCURRED Then
							Console.WriteLine("{0}: s> MOTION! Max: {1}, Min: {2}, ( {3}x{4} - {5}x{6} )", _
								fname, _
								res_slow.GetMaxDifference(), res_slow.GetMinDifference(), _
								res_slow.GetFoundX1(), res_slow.GetFoundY1(), _
								res_slow.GetFoundX2(), res_slow.GetFoundY2())
						Else
							Console.WriteLine("{0}: s> Max: {1}, Min: {2}", _
								fname, _
								res_slow.GetMaxDifference(), res_slow.GetMinDifference())
						End If
					End If
	
					System.Threading.Thread.Sleep(500)
				End If	
			End While


			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

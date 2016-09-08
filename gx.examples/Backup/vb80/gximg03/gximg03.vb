Imports System
Imports gx

Module Main
	Sub Main()
		Try
			' Pixels of the 4x4 chessboard (0x00 = black pixel, 0xFF = white pixel)
			Dim pixels() As Byte = { _
				0, 255, 0, 255, _
				255, 0, 255, 0, _
				0, 255, 0, 255, _
				255, 0, 255, 0 _
			}
			
			' Creates a new image object with default properties
			Dim image As gxImage = new gxImage("default")
	
			' Creates a pixel area for the chessboard
			'  pixelformat:  grayscale (GX_GRAY: 1 byte per pixel)
			'  width,height: 4x4 pixel
			'  sline:        4 byte (size of a row = 4 pixel * 1 byte /grayscale/ )
			image.Create(GX_PIXELFORMATS.GX_GRAY, 4, 4, 4)
	
			' Loads the pixel data from the memory (-1 = RAW format)
			image.LoadFromMem(pixels, -1)

			' Saves the chessboard image
			image.Save("test_chessboard.png", GX_IMGFILEFORMATS.GX_PNG)
					
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

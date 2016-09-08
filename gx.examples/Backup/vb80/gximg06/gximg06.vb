Imports System
Imports gx

Module Main
	Sub SaveImages(image As gxImage, nstr As String)
		Dim format_names() As String = { _
			"bmp", "raw", "jpg", "jp2", "jpc" _
		}
		Dim format_codes() As Integer = { _
			GX_IMGFILEFORMATS.GX_BMP, _
			GX_IMGFILEFORMATS.GX_RAW, _
			GX_IMGFILEFORMATS.GX_JPEG, _
			GX_IMGFILEFORMATS.GX_JPEG2K_JP2, _
			GX_IMGFILEFORMATS.GX_JPEG2K_JPC _
		}
		Dim ix As Integer
		For ix = 0 To format_names.Length()-1
			Dim fname As String = "test_" & nstr & "." & format_names.GetValue(ix)
			Console.Write(fname)
			image.Save(fname, format_codes.GetValue(ix))
			Console.WriteLine(" saved.")
		Next
	End Sub
	
	Sub Main()
		Try
			' Creates a new image handler with default properties
			Dim imghandler As gxImageHandler = new gxImageHandler("default")
	
			' Creates an image class
			Dim image As gxImage = new gxImage(imghandler)
				
			' Creates the 512x512 RGBA image
			image.Create(GX_PIXELFORMATS.GX_RGBA, 512, 512, 0)
				
			Dim pixels(512*512*4) As Byte
			Dim j As Integer
			For j = 0 To image.GetYSize()-1
				Dim i As Integer
				For i = 0 To image.GetXSize()-1
					pixels.SetValue(cbyte(j mod 256), (j*512+i)*4+0)
					pixels.SetValue(cbyte(i mod 256), (j*512+i)*4+1)
					pixels.SetValue(cbyte((i+j) mod 256), (j*512+i)*4+2)
					pixels.SetValue(cbyte(128), (j*512+i)*4+3)
				Next
			Next
				
			image.LoadFromMem(pixels, -1)
	
			' Saves images with various pixel formats
			Dim format_names() As String = { _
				"rgb", "bgr", "rgba", "bgra", _
				"rgb12", "bgr12", "rgb555", "bgr555", _
				"rgb565", "bgr565", "gray", "gray12", _
				"yuv422" _
			}
			Dim format_codes() As Integer = { _
				GX_PIXELFORMATS.GX_RGB, _
				GX_PIXELFORMATS.GX_BGR, _
				GX_PIXELFORMATS.GX_RGBA, _
				GX_PIXELFORMATS.GX_BGRA, _
				GX_PIXELFORMATS.GX_RGB12, _
				GX_PIXELFORMATS.GX_BGR12, _
				GX_PIXELFORMATS.GX_RGB555, _
				GX_PIXELFORMATS.GX_BGR555, _
				GX_PIXELFORMATS.GX_RGB565, _
				GX_PIXELFORMATS.GX_BGR565, _
				GX_PIXELFORMATS.GX_GRAY, _
				GX_PIXELFORMATS.GX_GRAY12, _
				GX_PIXELFORMATS.GX_YUV422 _
			}

			Dim temp As gxImage = New gxImage(imghandler)
			Dim ix As Integer
			For ix = 0 To format_names.Length()-1
				' Converts the image to a pixel format
				Console.Write("Converting to " & format_names.GetValue(ix) & ".. ")
				temp.Convert(image, format_codes.GetValue(ix), image.GetXSize(), image.GetYSize(), 0)
				Console.WriteLine(" Done")
		
				' Saves the image to any file formats
				SaveImages(temp, format_names.GetValue(ix))
			Next 
				
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module

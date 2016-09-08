' ******************************************************************************
' * GX error file - v7.2.8
' * 2004-2010 (c) ARH Inc. (http://www.arhungary.hu)
' ******************************************************************************
' GX system error codes and messages </b>
' The GX is the base system for the majority of the Adaptive Recognition Hungary Inc. products.
' It is a collection of loadable modules and library functions and gives an easy to program
' interface to the hardware devices.
' This file contains the error codes and descriptions of the GX system.
' ******************************************************************************

Option Explicit

Class GxErrorCode
    ' System and GX-specific error codes.
    Public Const GX_ENOERR As Long = &H0    ' No Error
    Public Const GX_ENOENT As Long = &H2    ' Entry not found (ENOENT)
    Public Const GX_ENOMEM As Long = &HC    ' Memory allocation error (ENOMEM)
    Public Const GX_EACCES As Long = &HD    ' Permission denied (EACCES)
    Public Const GX_EFAULT As Long = &HE    ' Bad address or program error (EFAULT)
    Public Const GX_EBUSY As Long = &H10   ' Resource busy (EBUSY)
    Public Const GX_EEXIST As Long = &H11   ' File exists (EEXIST)
    Public Const GX_ENODEV As Long = &H13   ' No such device (ENODEV)
    Public Const GX_EINVAL As Long = &H16   ' Invalid parameter (EINVAL)
    Public Const GX_ERANGE As Long = &H22   ' Data out of range (ERANGE)
    Public Const GX_EDATA As Long = &H3D   ' No data available (Linux - ENODATA)
    Public Const GX_ECOMM As Long = &H46   ' Communication error on send (Linux - ECOMM)
    Public Const GX_ETIMEDOUT As Long = &H6E   ' Function timed out (Linux - ETIMEDOUT)

    ' General error codes
    Public Const GX_EOPEN As Long = &H1000 ' File open error
    Public Const GX_ECREAT As Long = &H1001 ' File creation error
    Public Const GX_EREAD As Long = &H1002 ' File read error
    Public Const GX_EWRITE As Long = &H1003 ' File write error
    Public Const GX_EFILE As Long = &H1004 ' File content

    Public Const GX_EINVIMG As Long = &H1010 ' Invalid image
    Public Const GX_EINVFUNC As Long = &H1011 ' Invalid function

    Public Const GX_EHWKEY As Long = &H1012 ' Hardware key does not work properly
    Public Const GX_EVERSION As Long = &H1013 ' Invalid version
    Public Const GX_EASSERT As Long = &H1014 ' Assertion occurred

    Public Const GX_EDISCON As Long = &H1015 ' Device is disconnected
    Public Const GX_EIMGPROC As Long = &H1016 ' Image processing failed
    Public Const GX_EAUTH As Long = &H1017 ' Authenticity cannot be determined

    ' Module error codes
    ' GX_xxx = 0xmmmm8xxx    // mmmm => group code

    Public Const GX_ENOMODULE As Long = &H8000 ' The specified module not found (module: '%ls')

End Class


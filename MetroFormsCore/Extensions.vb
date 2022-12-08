Imports System.Runtime

Namespace Sisecom.Windows.Forms.Metro.Extensions

    <HideModuleName()>
    Public Module Commons

        <Runtime.CompilerServices.Extension()>
        Public Function GetDarkColor(c As Color, ByVal d As Byte) As Color
            Dim r As Byte = 0
            Dim g As Byte = 0
            Dim b As Byte = 0

            If (c.R > d) Then r = (c.R - d)
            If (c.G > d) Then g = (c.G - d)
            If (c.B > d) Then b = (c.B - d)

            Dim c1 As Color = Color.FromArgb(r, g, b)
            Return c1
        End Function
        <Runtime.CompilerServices.Extension()>
        Public Function GetLightColor(c As Color, ByVal d As Byte) As Color
            Dim r As Byte = 255
            Dim g As Byte = 255
            Dim b As Byte = 255

            If (CInt(c.R) + CInt(d) <= 255) Then r = (c.R + d)
            If (CInt(c.G) + CInt(d) <= 255) Then g = (c.G + d)
            If (CInt(c.B) + CInt(d) <= 255) Then b = (c.B + d)

            Dim c2 As Color = Color.FromArgb(r, g, b)
            Return c2
        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function GetNegativeColor(c As Color) As Color
            Return Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B)
        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function GetNegativeImage(c As Image) As Bitmap
            Return GetNegativeImage(New Bitmap(c))
        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function GetNegativeImage(c As Bitmap) As Bitmap

            If Bitmap.GetPixelFormatSize(c.PixelFormat) = 32 AndAlso c.PixelFormat <> Imaging.PixelFormat.Indexed Then
                Dim bmpRect As New Rectangle(0, 0, c.Width, c.Height)
                Dim BitmapData As Imaging.BitmapData = c.LockBits(bmpRect, Imaging.ImageLockMode.ReadWrite, c.PixelFormat)

                Dim ByteData As Byte()
                ReDim ByteData(c.Width * c.Height * 4 - 1)

                InteropServices.Marshal.Copy(BitmapData.Scan0, ByteData, 0, ByteData.Length)

                For ii = LBound(ByteData) To UBound(ByteData)
                    Select Case ii Mod 4
                        Case 0, 1, 2 'blue, green, red
                            ByteData(ii) = CByte(255 - ByteData(ii))
                        Case 3 'alpha
                    End Select
                Next

                InteropServices.Marshal.Copy(ByteData, 0, BitmapData.Scan0, ByteData.Length)
                c.UnlockBits(BitmapData)

            End If
            Return c

        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function GetColorMask(c As Image, ByVal SolidColor As Color) As Image
            Return GetColorMask(New Bitmap(c), SolidColor)
        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function GetColorMask(c As Bitmap, ByVal SolidColor As Color) As Bitmap

            Dim bmpRect As New Rectangle(0, 0, c.Width, c.Height)
            Dim BitmapData As Imaging.BitmapData = c.LockBits(bmpRect, Imaging.ImageLockMode.ReadWrite, c.PixelFormat)

            Dim ByteData As Byte()
            ReDim ByteData(c.Width * c.Height * 4 - 1)

            InteropServices.Marshal.Copy(BitmapData.Scan0, ByteData, 0, ByteData.Length)

            For ii = LBound(ByteData) To UBound(ByteData) Step 4
                Dim AlphaByte = ByteData(ii + 3)
                ByteData(ii) = BlendByte(ByteData(ii), SolidColor.B, AlphaByte) 'blue
                ByteData(ii + 1) = BlendByte(ByteData(ii + 1), SolidColor.G, AlphaByte)  'green
                ByteData(ii + 2) = BlendByte(ByteData(ii + 2), SolidColor.R, AlphaByte)  'red
                ByteData(ii + 3) = BlendByte(ByteData(ii + 3), SolidColor.A, AlphaByte)  'alpha
            Next

            InteropServices.Marshal.Copy(ByteData, 0, BitmapData.Scan0, ByteData.Length)
            c.UnlockBits(BitmapData)

            Return c

        End Function

        Private Function BlendByte(ByVal From As Byte, ByVal [To] As Byte, ByVal Amount As Byte) As Byte
            Return CByte(((CInt([To]) - [From]) * (Amount / 255)) + [From])
        End Function




    End Module

End Namespace
Imports System.ComponentModel
Imports Sisecom.Windows.Forms.Metro.Extensions

Namespace Sisecom.Windows.Forms.Metro

    <ToolboxBitmap(GetType(System.Windows.Forms.ToolTip))> _
    Public Class ToolTip
        Inherits System.Windows.Forms.ToolTip

        Private Sub ToolTip_Draw(sender As Object, e As DrawToolTipEventArgs) Handles Me.Draw

            If e.AssociatedControl.FindForm IsNot Nothing Then
                If TypeOf e.AssociatedControl.FindForm Is Sisecom.Windows.Forms.Metro.Form Then
                    With CType(e.AssociatedControl.FindForm, Sisecom.Windows.Forms.Metro.Form)
                        Me.BorderColor = .BorderColor
                        Me.BorderWidth = .BorderWidth / 2
                    End With
                End If
            End If

            e.DrawBackground()
            e.DrawText()

            Dim _p As New System.Drawing.Pen(Me.BorderColor, Me.BorderWidth)
            e.Graphics.DrawRectangle(_p, CInt(Me.BorderWidth / 2), CInt(Me.BorderWidth / 2), e.Bounds.Width - Me.BorderWidth, e.Bounds.Height - Me.BorderWidth)

        End Sub

        Private Sub ToolTip_Popup(sender As Object, e As PopupEventArgs) Handles Me.Popup

            If Math.Abs((Me.BackColor.GetBrightness - Me.ForeColor.GetBrightness)) <= 0.5 Then Me.ForeColor = Me.ForeColor.GetNegativeColor()

            e.ToolTipSize = New System.Drawing.Size( _
                Me.Padding.Left + e.ToolTipSize.Width + Me.Padding.Right, _
                Me.Padding.Top + e.ToolTipSize.Height + Me.Padding.Bottom)
           
        End Sub

#Region " Border "

        Private _BorderColor As System.Drawing.Color = System.Drawing.Color.Gray
        <DefaultValue("System.Drawing.Color.Gray")> _
        <Category("Border")> _
        Public Property BorderColor As System.Drawing.Color
            Get
                Return _BorderColor
            End Get
            Set(value As System.Drawing.Color)
                If _BorderColor <> value Then
                    _BorderColor = value
                End If
            End Set
        End Property

        Private _BorderWidth As Integer = 2
        <DefaultValue(2)> _
        <Category("Border")> _
        Public Property BorderWidth As Integer
            Get
                Return _BorderWidth
            End Get
            Set(value As Integer)
                If _BorderWidth <> value Then
                    _BorderWidth = value
                End If
            End Set
        End Property

#End Region

        Private _Padding As New System.Windows.Forms.Padding(3)
        <DefaultValue(3)> _
        Public Property Padding As System.Windows.Forms.Padding
            Get
                Return _Padding
            End Get
            Set(value As System.Windows.Forms.Padding)
                _Padding = value
            End Set
        End Property

    End Class

End Namespace
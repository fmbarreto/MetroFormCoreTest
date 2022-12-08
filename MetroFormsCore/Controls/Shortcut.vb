Imports System.ComponentModel

Namespace Sisecom.Windows.Forms.Metro

    Public Class Shortcut
        Inherits System.Windows.Forms.Control

        Private _Text As String = ""
        <Description("The text that will display as the caption."), Category("Appearance"), DefaultValue("DividerLabel")>
        Public Overrides Property Text() As String
            Get
                Return Me._Text
            End Get
            Set(value As String)
                Me._Text = value
                Me.Invalidate()
            End Set
        End Property

        Private _Image As System.Drawing.Image
        Public Property Image As System.Drawing.Image
            Get
                Return _Image
            End Get
            Set(value As System.Drawing.Image)
                If _Image IsNot value Then
                    _Image = value
                    Me.Invalidate()
                End If
            End Set
        End Property
        Public Property HoverColor As System.Drawing.Color = Color.DarkRed

        'Private _Title As String = "Title"
        '<DefaultValue("Title")> _
        'Public Property Title As String
        '    Get
        '        Return _Title
        '    End Get
        '    Set(value As String)
        '        If _Title <> value Then
        '            _Title = value
        '            Me.Invalidate()
        '        End If
        '    End Set
        'End Property

        Private Sub _PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown
            If e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter Then
                Me.PerformClick()
            End If
        End Sub

        Public Sub PerformClick()
            MyBase.InvokeOnClick(Me, New EventArgs)
        End Sub

        Private _Badge As String = "0"
        <DefaultValue("0")>
        Public Property Badge As String
            Get
                Return _Badge
            End Get
            Set(value As String)
                If _Badge <> value Then
                    _Badge = value
                    Me.Invalidate()
                End If
            End Set
        End Property
        Private _ShowBadge As Boolean = False
        <DefaultValue(False)>
        Public Property ShowBadge As Boolean
            Get
                Return _ShowBadge
            End Get
            Set(value As Boolean)
                If _ShowBadge <> value Then
                    _ShowBadge = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _BadgeColor As System.Drawing.Color
        Public Property BadgeColor As System.Drawing.Color
            Get
                Return _BadgeColor
            End Get
            Set(value As System.Drawing.Color)
                If _BadgeColor <> value Then
                    _BadgeColor = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Protected Overrides Sub OnPaint(e As PaintEventArgs)

            Dim _Original As SizeF = e.Graphics.MeasureString(Me.Text, New System.Drawing.Font(Me.Font.FontFamily, 9))

            Dim _FinalText As String = Me.Text
            Dim _Final As SizeF = e.Graphics.MeasureString(_FinalText, New System.Drawing.Font(Me.Font.FontFamily, 9))

            Do While (_Final.Width) + (Me.Padding.Left + Me.Padding.Right) > Me.Width
                _FinalText = _FinalText.Substring(0, _FinalText.Length - IIf(_FinalText.EndsWith("..."), 4, 1)) & "..."
                _Final = e.Graphics.MeasureString(_FinalText, New System.Drawing.Font(Me.Font.FontFamily, 9))
            Loop

            e.Graphics.DrawString(_FinalText, New System.Drawing.Font(Me.Font.FontFamily, 9),
                                  New System.Drawing.SolidBrush(IIf(Me.BackColor.GetBrightness >= 0.5, Me.ForeColor, System.Drawing.Color.White)),
                                  New System.Drawing.Point((Me.Width - _Final.Width) / 2, ((Me.Height + _Final.Height + 45) / 2) - 6 - Me.Padding.Bottom))

            If Me.Image IsNot Nothing Then
                e.Graphics.DrawImage(Me.Image, CInt((Me.Width - 45) / 2), CInt((Me.Height - 45) / 2) - (_Final.Height / 2), 45, 45)
            End If

            If Me.ShowBadge Then

                Dim _BadgeTemp As String = Me.Badge

                Dim _mb As SizeF = e.Graphics.MeasureString(_BadgeTemp, New System.Drawing.Font(Me.Font.FontFamily, 9))
                Dim _w As Integer = IIf(_mb.Width > 14, _mb.Width, 14) + 6

                Dim _badgerect As New System.Drawing.Rectangle(Me.Width - _w + 3, 0, _w, 20)
                e.Graphics.FillRectangle(New System.Drawing.SolidBrush(Me.BadgeColor), _badgerect)

                e.Graphics.DrawString(_BadgeTemp, New System.Drawing.Font(Me.Font.FontFamily, 9),
                               New System.Drawing.SolidBrush(IIf(Me.BadgeColor.GetBrightness >= 0.5, Me.ForeColor, System.Drawing.Color.White)),
                               New System.Drawing.Point(Me.Width - (_badgerect.Width / 2) - (_mb.Width / 2) + 1, 1))

            End If

        End Sub

        Sub New()

            Me.Size = New System.Drawing.Size(92, 92)
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))

        End Sub

        Private Sub Shortcut_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
            Me.InvokeGotFocus(Me, New EventArgs)
        End Sub

        Private Sub Shortcut_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            Me.Left += 1
            Me.Top += 1
        End Sub

        Dim _pressed As Boolean = False
        Private Sub Shortcut_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
            If (e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter) AndAlso Not _pressed Then
                Me.Left += 1
                Me.Top += 1
                _pressed = True
            End If
        End Sub
        Private Sub Shortcut_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
            Me.Left -= 1
            Me.Top -= 1
        End Sub
        Private Sub Shortcut_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
            If (e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter) AndAlso _pressed Then
                Me.Left -= 1
                Me.Top -= 1
                _pressed = False
            End If
        End Sub
        Private Sub Shortcut_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, Me.GotFocus
            Me.CreateGraphics.DrawLine(New System.Drawing.Pen(Me.HoverColor, 2), 0, Me.Height - 1, Me.Width, Me.Height - 1)
        End Sub

        Private Sub Shortcut_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave, Me.LostFocus
            Me.Invalidate()
        End Sub

    End Class

End Namespace
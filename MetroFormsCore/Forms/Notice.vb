Imports System.ComponentModel
Imports Sisecom.Windows.Forms.Metro.Extensions

Namespace Sisecom.Windows.Forms.Metro

    Public Class Notice

        Private _Text As String = "Notice"

        <DefaultValue("Notice")> _
        Public Shadows Property Text As String
            Get
                Return _Text
            End Get
            Set(value As String)
                If _Text <> value Then
                    _Text = value
                    MyBase.Text = _Text
                End If
            End Set
        End Property

        Private _BackColor As System.Drawing.Color = SystemColors.Highlight
        <DefaultValue("SystemColors.Highlight")> _
        Public Shadows Property BackColor As System.Drawing.Color
            Get
                Return _BackColor
            End Get
            Set(value As System.Drawing.Color)
                If _BackColor <> value Then
                    _BackColor = value
                    MyBase.BackColor = _BackColor
                End If
            End Set
        End Property

        Private _Message As String = "Message"
        <DefaultValue("Message")> _
        Public Property Message As String
            Get
                Return _Message
            End Get
            Set(value As String)
                If _Message <> value Then
                    _Message = value
                End If
            End Set
        End Property

        Private _Interval As Integer = 10000
        <DefaultValue(10000)> _
        Public Property Interval As Integer
            Get
                Return _Interval
            End Get
            Set(value As Integer)
                If _Interval <> value Then
                    _Interval = value
                End If
            End Set
        End Property

        Private _Image As System.Drawing.Image
        Public Property Image As System.Drawing.Image
            Get
                Return _Image
            End Get
            Set(value As System.Drawing.Image)
                _Image = value
            End Set
        End Property

        Private _ImageIcon As System.Drawing.Image
        Public Property ImageIcon As System.Drawing.Image
            Get
                Return _ImageIcon
            End Get
            Set(value As System.Drawing.Image)
                _ImageIcon = value
            End Set
        End Property

        Private _ShowImageIcon As Boolean = False
        <DefaultValue("False")> _
        Public Property ShowImageIcon As Boolean
            Get
                Return _ShowImageIcon
            End Get
            Set(value As Boolean)
                If _ShowImageIcon <> value Then
                    _ShowImageIcon = value
                End If
            End Set
        End Property

        Public Shadows Event Closed As EventHandler

        Private Sub _New(text As String, message As String, image As System.Drawing.Image, showimageicon As Boolean, imageicon As System.Drawing.Image, backcolor As System.Drawing.Color, time As Integer)

            Dim _FinalText As String = text
            Dim _Final As System.Drawing.SizeF = Me.CreateGraphics.MeasureString(_FinalText, Me.LabelTitle.Font)

            Do While (_Final.Width) + Me.LabelTitle.Left + Me.ButtonClose.Width + IIf(image IsNot Nothing, Me.PictureBoxImage.Width, 0) > Me.Width
                _FinalText = _FinalText.Substring(0, _FinalText.Length - IIf(_FinalText.EndsWith("..."), 4, 1)) & "..."
                _Final = Me.CreateGraphics.MeasureString(_FinalText, Me.LabelTitle.Font)
            Loop

            Me.Text = _FinalText

            Me.LabelTitle.Text = Me.Text
            Me.Message = message
            Me.LabelMessage.Text = Me.Message

            If backcolor <> Nothing Then
                Me.BackColor = backcolor
            End If

            Me.ToolTipNotice.BackColor = Me.BackColor.GetLightColor(64)
            Me.Interval = time
            If image IsNot Nothing Then Me.Image = image
            Me.ShowImageIcon = showimageicon
            If imageicon IsNot Nothing Then Me.ImageIcon = imageicon
        End Sub

        Public Overloads Sub Show()
            Dim _bgColor As System.Drawing.Color
            If Me.Parent IsNot Nothing Then
                If TypeOf (Me.Parent) Is Sisecom.Windows.Forms.Metro.Form Then
                    With CType(Me.Parent, Sisecom.Windows.Forms.Metro.Form)
                        _bgColor = .BorderColor
                    End With
                End If
            End If
            MyBase.Show()
        End Sub

        Public Overloads Sub Show(text As String, message As String)
            Me._New(text, message, Nothing, False, Nothing, Nothing, 10000)
            Me.Show()
        End Sub
        Public Overloads Sub Show(text As String, message As String, image As System.Drawing.Image)
            Me._New(text, message, image, False, Nothing, Nothing, 10000)
            Me.Show()
        End Sub
        Public Overloads Sub Show(text As String, message As String, image As System.Drawing.Image, interval As Integer)
            Me._New(text, message, image, False, Nothing, Nothing, interval)
            Me.Show()
        End Sub

        Public Overloads Sub Show(text As String, message As String, backcolor As System.Drawing.Color)
            Me._New(text, message, Nothing, False, Nothing, backcolor, 10000)
            Me.Show()
        End Sub
        Public Overloads Sub Show(text As String, message As String, backcolor As System.Drawing.Color, interval As Integer)
            Me._New(text, message, Nothing, False, Nothing, backcolor, interval)
            Me.Show()
        End Sub
        Public Overloads Sub Show(text As String, message As String, interval As Integer)
            Me._New(text, message, Nothing, False, Nothing, Nothing, interval)
            Me.Show()
        End Sub

        Public Overloads Sub Show(text As String, message As String, image As System.Drawing.Image, backcolor As System.Drawing.Color)
            Me._New(text, message, image, False, Nothing, backcolor, 10000)
            Me.Show()
        End Sub
        Public Overloads Sub Show(text As String, message As String, image As System.Drawing.Image, backcolor As System.Drawing.Color, interval As Integer)
            Me._New(text, message, image, False, Nothing, backcolor, interval)
            Me.Show()
        End Sub

        Public Overloads Sub Show(text As String, message As String, image As System.Drawing.Image, backcolor As System.Drawing.Color, interval As Integer, icon As System.Drawing.Image)
            Me._New(text, message, image, True, icon, backcolor, interval)
            Me.Show()
        End Sub

        Sub New(text As String, message As String, interval As Integer)
            Me.InitializeComponent()
            Me._New(text, message, Nothing, Nothing, Nothing, Nothing, interval)
        End Sub

        Sub New(text As String, message As String)
            Me.InitializeComponent()
            Me._New(text, message, Nothing, False, Nothing, SystemColors.Highlight, 10000)
        End Sub
        Sub New(text As String, message As String, image As System.Drawing.Image)
            Me.InitializeComponent()
            Me._New(text, message, image, False, Nothing, SystemColors.Highlight, 10000)
        End Sub
        Sub New(text As String, message As String, image As System.Drawing.Image, interval As Integer)
            Me.InitializeComponent()
            Me._New(text, message, image, False, Nothing, SystemColors.Highlight, interval)
        End Sub

        Sub New(text As String, message As String, backcolor As System.Drawing.Color)
            Me.InitializeComponent()
            Me._New(text, message, Nothing, False, Nothing, backcolor, 10000)
        End Sub
        Sub New(text As String, message As String, backcolor As System.Drawing.Color, interval As Integer)
            Me.InitializeComponent()
            Me._New(text, message, Nothing, False, Nothing, backcolor, interval)
        End Sub

        Sub New(text As String, message As String, image As System.Drawing.Image, backcolor As System.Drawing.Color)
            Me.InitializeComponent()
            Me._New(text, message, image, False, Nothing, backcolor, 10000)
        End Sub
        Sub New(text As String, message As String, image As System.Drawing.Image, backcolor As System.Drawing.Color, interval As Integer)
            Me.InitializeComponent()
            Me._New(text, message, image, False, Nothing, backcolor, interval)
        End Sub

        Sub New(text As String, message As String, image As System.Drawing.Image, backcolor As System.Drawing.Color, interval As Integer, icon As System.Drawing.Image)
            Me.InitializeComponent()
            Me._New(text, message, image, True, icon, backcolor, interval)
        End Sub

        Private Sub Notice_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
            RaiseEvent Closed(Me, New EventArgs)
        End Sub


        Private Sub Notice_Load(sender As Object, e As EventArgs) Handles Me.Load
            Me.PictureBoxImage.Image = Me.Image
            Me.PictureBoxImage.Visible = Me.Image IsNot Nothing
            Me.PictureBoxIcon.Visible = Me.ShowImageIcon

            If ImageIcon IsNot Nothing And Me.ShowImageIcon Then
                Me.PictureBoxIcon.Image = Me.ImageIcon
            ElseIf Me.ShowImageIcon Then
                Me.PictureBoxIcon.Image = Me.Icon.ToBitmap
            Else
                Me.PictureBoxIcon.Image = Nothing
            End If

            Me.TimerClose.Interval = Me.Interval

            Me.ButtonClose.Image = IIf((Me.BackColor.GetBrightness) >= 0.5, My.Resources.close_d, My.Resources.close_l)

            Me.StartPosition = FormStartPosition.Manual
            Me.Location = New System.Drawing.Point(Screen.GetWorkingArea(New Point(0, 0)).Width - Me.Width, 16)

        End Sub
        Private Sub Notice_Shown(sender As Object, e As EventArgs) Handles Me.Shown
            Me.TimerClose.Enabled = Me.TimerClose.Interval <> 0
        End Sub

        
        Private Sub TimerClose_Tick(sender As Object, e As EventArgs) Handles TimerClose.Tick
            Me.ButtonClose.PerformClick()
        End Sub

        'Private Sub ButtonClose_MouseEnter(sender As Object, e As EventArgs) Handles ButtonClose.MouseEnter
        '    Me.ButtonClose.Visible = True
        'End Sub
        Private Sub Notice_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, _
            PictureBoxIcon.MouseEnter, LabelMessage.MouseEnter, LabelTitle.MouseEnter, PictureBoxIcon.MouseEnter, _
            ButtonClose.MouseEnter
            If TimerClose.Interval <> 0 And TimerClose.Enabled = True Then
                TimerClose.Enabled = False
            End If
        End Sub
        Private Sub Notice_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave, _
            PictureBoxIcon.MouseLeave, LabelMessage.MouseLeave, LabelTitle.MouseLeave, PictureBoxIcon.MouseLeave, _
            ButtonClose.MouseLeave
            If TimerClose.Interval <> 0 And TimerClose.Enabled = False Then
                TimerClose.Enabled = True
            End If
        End Sub

        Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
            Me.Close()
            Me.TimerClose.Enabled = False
            Me.TimerClose.Dispose()
        End Sub

        'Protected Overrides Sub OnPaint(e As PaintEventArgs)
        '    MyBase.OnPaint(e)

        '    Dim _p As New System.Drawing.Pen(Me.BackColor.GetLightColor(125), 1)
        '    e.Graphics.DrawRectangle(_p, 0, 0, Me.Width - 1, Me.Height - 1)

        'End Sub



        '#Region " Fadding "

        '        Private WithEvents Timer2 As New Timer
        '        Private WithEvents Timer1 As New Timer

        '        Private Sub Fade_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '            Me.Opacity = 0
        '            Timer1.Enabled = True
        '        End Sub

        '        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        '            Me.Opacity += 0.2
        '            If Me.Opacity = 1 Then
        '                Timer1.Enabled = False
        '            End If
        '        End Sub

        '        Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        '            Me.Opacity -= 0.2
        '            If Me.Opacity = 0 Then
        '                Timer2.Enabled = False
        '                Me.Close()
        '            End If

        '        End Sub

        '#End Region



    End Class

End Namespace

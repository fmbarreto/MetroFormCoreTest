Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports Sisecom.Windows.Forms.Metro.Extensions

Namespace Sisecom.Windows.Forms.Metro

    Public Class Form

        Sub New()

            ' Init
            InitializeComponent()

            ' Form Default Style
            Me.Icon = My.Resources.wfm_icon
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.DoubleBuffered = True
            Me.SetStyle(ControlStyles.ResizeRedraw, True)

            Me.TileBarFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TitleBarForeColor = Color.Black
            Me.TitleBarForeColorOnFocus = Color.Black
            Me.TileBarLogoSize = Me.pbIconMetroForm.Size
            'Me.TitleBarLogo = Me.Icon.ToBitmap
            'Me.TitleBarLogoOnFocus = Me.Icon.ToBitmap

            ' Mdi Background color
            If Me.IsMdiChild Then _
                Me.Controls.OfType(Of MdiClient)().All(Function(i)
                                                           i.BackColor = Me.BackColor
                                                           Return True
                                                       End Function)

            Dim rect As System.Drawing.Rectangle = Screen.GetWorkingArea(Me)
            Me.MaximizedBounds = Screen.GetWorkingArea(rect)

        End Sub

        'Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        '    'Me.ttMetroForm.SetToolTip(Me.btnMinimizeMetroForm, Me.tsmiMinimizeMetroForm.Text)
        '    'Me.ttMetroForm.SetToolTip(Me.btnMaximizeRestoreMetroForm,
        '    '                           IIf(Me.WindowState = FormWindowState.Maximized,
        '    '                               Me.tsmiRestoreMetroForm.Text,
        '    '                               Me.tsmiMaximizeMetroForm.Text))
        '    'Me.ttMetroForm.SetToolTip(Me.btnCloseMetroForm, Me.tsmiCloseMetroForm.Text)
        'End Sub

        'Private _TextReducted As Boolean = False

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            Using brush As New LinearGradientBrush(Me.ClientRectangle, Me.GradientBackColor1, Me.GradientBackColor2, 90.0F)
                e.Graphics.FillRectangle(brush, Me.ClientRectangle)
            End Using
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            'ContextMenuStrip Render
            Dim _render As New Sisecom.Windows.Forms.Metro.Renderer
            _render.AlterColor = True
            _render.OverrideColor = Me.ForeColor

            With _render.Menustrip

                '.MarginBorder = Me.BorderColor
                '.MarginLeft = Me.BorderColor
                '.MarginRight = Me.BorderColor

                .MenuBorderDark = Me.BorderColor
                .MenuBorderLight = Me.BorderColor

                .BackgroundTop = Me.BackColor
                .BackgroundBottom = Me.BackColor

                .MenustripButtonBorder = Me.BorderColor
                .MenustripButtonBackground = Me.BackColor

                With .Items
                    .InnerBorder = Me.BorderColor
                    .BorderTop = Me.BorderColor
                    .BorderBottom = Me.BorderColor
                    .BorderAngle = 0
                    .HoverBackgroundTop = Me.BorderColor.GetLightColor(92)
                    .HoverBackgroundBottom = Me.BorderColor.GetLightColor(92)
                End With

            End With

            Me.cmsMetroForm.Renderer = _render ' ToolStripRenderer(Me)

            'Text title
            'Dim _FinalText As String = Me.Text
            'Dim _Final As System.Drawing.SizeF = e.Graphics.MeasureString(_FinalText, Me.LabelTitle.Font)

            'Do While (_Final.Width) + Me.LabelTitle.Left + (Me.ButtonMinimize.Width * 3) > Me.Width
            '    _FinalText = _FinalText.Substring(0, _FinalText.Length - IIf(_FinalText.EndsWith("..."), 4, 1)) & "..."
            '    _Final = e.Graphics.MeasureString(_FinalText, Me.LabelTitle.Font)
            'Loop

            Me.lblTitleMetroForm.Text = Me.Text

            ' Title
            Me.pnlTitleMetroForm.Visible = Me.ShowTitleBar
            Me.pnlTitleMetroForm.SendToBack()
            'Me.pbIconMetroForm.Image = Me.TitleBarLogo
            Me.pbIconMetroForm.Visible = Me.ShowIcon
            'Me.LabelTitle.Left = IIf(Me.ShowIcon, Me.PictureBoxIcon.Left + Me.PictureBoxIcon.Width, Me.PictureBoxIcon.Left)

            ' Tooltip
            Me.ttMetroForm.BackColor = Me.BackColor
            Me.ttMetroForm.ForeColor = Me.ForeColor.GetLightColor(92)

            Me.ttMetroForm.BorderColor = Me.BorderColor
            Me.ttMetroForm.BorderWidth = Me.BorderWidth / 2

            ' Border paiting
            Me.Padding = New System.Windows.Forms.Padding(Me.BorderWidth)

            If Me.WindowState = FormWindowState.Normal Then
                If Me.Capture = False Then
                    Dim _p As New System.Drawing.Pen(Me.BorderColor, Me.BorderWidth)
                    e.Graphics.DrawRectangle(_p, CInt(Me.BorderWidth / 2), CInt(Me.BorderWidth / 2), Me.Width - Me.BorderWidth, Me.Height - Me.BorderWidth)
                Else
                    Me.Invalidate()
                End If
            End If

            ' Control box
            Me.btnMinimizeMetroForm.Visible = Me.MinimizeBox And Me.ControlBox
            Me.tsmiMinimizeMetroForm.Enabled = Me.btnMinimizeMetroForm.Visible
            Me.btnMaximizeRestoreMetroForm.Visible = Me.MaximizeBox And Me.ControlBox
            Me.tsmiMaximizeMetroForm.Enabled = Me.btnMaximizeRestoreMetroForm.Visible And WindowState <> FormWindowState.Maximized
            Me.tsmiRestoreMetroForm.Enabled = Me.btnMaximizeRestoreMetroForm.Visible And WindowState = FormWindowState.Maximized
            Me.btnCloseMetroForm.Visible = Me.ControlBox
            Me.tsmiCloseMetroForm.Enabled = Me.btnCloseMetroForm.Visible

            Me.pnlTitleMetroFormButtons.Width = IIf(Me.btnMinimizeMetroForm.Visible, Me.btnMinimizeMetroForm.Width, 0) + IIf(Me.btnMaximizeRestoreMetroForm.Visible, Me.btnMaximizeRestoreMetroForm.Width, 0) + IIf(Me.btnCloseMetroForm.Visible, Me.btnCloseMetroForm.Width, 0)

            Me.btnMinimizeMetroForm.FlatAppearance.MouseOverBackColor = Me.TiTleBarBackColor.GetLightColor(92)
            Me.btnMaximizeRestoreMetroForm.FlatAppearance.MouseOverBackColor = Me.TiTleBarBackColor.GetLightColor(92)
            Me.btnCloseMetroForm.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 255, 89, 89) 'Me.TiTleBarBackColor.GetLightColor(92)

            Me.btnMinimizeMetroForm.FlatAppearance.MouseDownBackColor = Me.TiTleBarBackColor.GetLightColor(128)
            Me.btnMaximizeRestoreMetroForm.FlatAppearance.MouseDownBackColor = Me.TiTleBarBackColor.GetLightColor(128)
            Me.btnCloseMetroForm.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 89, 89) 'Me.TiTleBarBackColor.GetLightColor(128)

            Select Case Me.BackColor.GetBrightness
                Case Is >= 0.5
                    Me.btnMinimizeMetroForm.Image = My.Resources.minimize_d
                    Me.btnMaximizeRestoreMetroForm.Image = IIf(Me.WindowState = FormWindowState.Normal, My.Resources.maximize_d, My.Resources.restore_d)
                    Me.btnCloseMetroForm.Image = My.Resources.close_d
                    'Me.PictureBoxSizingGrip.Image = My.Resources.resize_d
                    'Me.ForeColor = System.Drawing.Color.White
                Case Else
                    Me.btnMinimizeMetroForm.Image = My.Resources.minimize_d.GetNegativeImage
                    Me.btnMaximizeRestoreMetroForm.Image = IIf(Me.WindowState = FormWindowState.Normal, My.Resources.maximize_d.GetNegativeImage, My.Resources.restore_d.GetNegativeImage)
                    Me.btnCloseMetroForm.Image = My.Resources.close_d.GetNegativeImage
                    'Me.PictureBoxSizingGrip.Image = My.Resources.resize_l
                    'Me.ForeColor = System.Drawing.Color.Black
            End Select

            Me.btnCloseMetroForm.Top = Me.BorderWidth
            Me.btnMaximizeRestoreMetroForm.Top = Me.BorderWidth
            Me.btnMinimizeMetroForm.Top = Me.BorderWidth

            Me.btnCloseMetroForm.Left = Me.pnlTitleMetroFormButtons.Width - Me.btnCloseMetroForm.Width - Me.BorderWidth 'Me.Width - Me.btnCloseMetroForm.Width - Me.BorderWidth
            Me.btnMaximizeRestoreMetroForm.Left = Me.pnlTitleMetroFormButtons.Width - Me.btnMaximizeRestoreMetroForm.Width - IIf(Me.btnCloseMetroForm.Visible, Me.btnCloseMetroForm.Width, 0) - Me.BorderWidth 'Me.Width - Me.btnMaximizeRestoreMetroForm.Width - IIf(Me.btnCloseMetroForm.Visible, Me.btnCloseMetroForm.Width, 0) - Me.BorderWidth
            Me.btnMinimizeMetroForm.Left = Me.pnlTitleMetroFormButtons.Width - Me.btnMinimizeMetroForm.Width - IIf(Me.btnMaximizeRestoreMetroForm.Visible, Me.btnMaximizeRestoreMetroForm.Width, 0) - IIf(Me.btnCloseMetroForm.Visible, Me.btnCloseMetroForm.Width, 0) - Me.BorderWidth 'Me.Width - Me.btnMinimizeMetroForm.Width - IIf(Me.btnMaximizeRestoreMetroForm.Visible, Me.btnMaximizeRestoreMetroForm.Width, 0) - IIf(Me.btnCloseMetroForm.Visible, Me.btnCloseMetroForm.Width, 0) - Me.BorderWidth

            'Me.PictureBoxSizingGrip.Left = Me.Width - Me.BorderWidth - Me.PictureBoxSizingGrip.Width - Me.PictureBoxSizingGrip.Margin.Right - Me.PictureBoxSizingGrip.Margin.Left
            'Me.PictureBoxSizingGrip.Top = Me.Height - Me.BorderWidth - Me.PictureBoxSizingGrip.Height - Me.PictureBoxSizingGrip.Margin.Bottom - Me.PictureBoxSizingGrip.Margin.Top
        End Sub

#Region " ShowDialog "

        Private _IsDialog As Boolean
        Public ReadOnly Property IsDialog As Boolean
            Get
                Return _IsDialog
            End Get
        End Property
        Public Overloads Function ShowDialog() As DialogResult
            _IsDialog = True
            Return MyBase.ShowDialog
        End Function

#End Region

#Region " Shadow "

        'Private Const CS_DROPSHADOW As Integer = &H20000 '131072
        'Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        '    Get
        '        If Not Me.DesignMode Then
        '            Dim cp As CreateParams = MyBase.CreateParams
        '            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
        '            Return cp
        '        Else
        '            Return MyBase.CreateParams
        '        End If
        '    End Get
        'End Property

        Private aeroEnabled As Boolean

        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            Get
                CheckAeroEnabled()
                Dim cp As CreateParams = MyBase.CreateParams
                If Not aeroEnabled Then
                    cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
                    Return cp
                Else
                    Return cp
                End If
            End Get
        End Property

        'Protected Overrides Sub WndProc(ByRef m As Message)
        '    Select Case m.Msg
        '        Case WM_NCPAINT
        '            Dim val = 2
        '            If aeroEnabled Then
        '                DwmSetWindowAttribute(Handle, 2, val, 4)
        '                Dim margens As New MARGINS()
        '                With margens
        '                    .bottomHeight = Me.ShadowSize
        '                    .leftWidth = Me.ShadowSize
        '                    .rightWidth = Me.ShadowSize
        '                    .topHeight = Me.ShadowSize
        '                End With
        '                DwmExtendFrameIntoClientArea(Handle, margens)
        '            End If
        '            Exit Select
        '    End Select
        '    MyBase.WndProc(m)
        'End Sub
        Private Sub CheckAeroEnabled()
            If Environment.OSVersion.Version.Major >= 6 Then
                Dim enabled As Integer = 0
                Dim response As Integer = DwmIsCompositionEnabled(enabled)
                aeroEnabled = (enabled = 1)
            Else
                aeroEnabled = False
            End If
        End Sub

        Private _ShadowSize As Integer = 4
        <DefaultValue(4)>
        Public Property ShadowSize() As Integer
            Get
                Return _ShadowSize
            End Get
            Set(ByVal value As Integer)
                If _ShadowSize <> value Then
                    _ShadowSize = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Public Structure MARGINS
            Public leftWidth As Integer
            Public rightWidth As Integer
            Public topHeight As Integer
            Public bottomHeight As Integer
        End Structure

        <DllImport("dwmapi")>
        Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
        End Function

        <DllImport("dwmapi")>
        Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
        End Function

        <DllImport("dwmapi.dll")>
        Public Shared Function DwmIsCompositionEnabled(ByRef pfEnabled As Integer) As Integer
        End Function

        Public Const CS_DROPSHADOW As Integer = &H20000
        Public Const WM_NCPAINT As Integer = &H85

#End Region

#Region " Properties "

#Region " Gradient "

        Private _GradientBackColor1 As System.Drawing.Color = System.Drawing.Color.White
        <DefaultValue("System.Drawing.Color.White")>
        Public Property GradientBackColor1() As System.Drawing.Color
            Get
                Return _GradientBackColor1
            End Get
            Set(ByVal value As System.Drawing.Color)
                If _GradientBackColor1 <> value Then
                    _GradientBackColor1 = value
                    RaisePaintEvent(Me, Nothing)
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _GradientBackColor2 As System.Drawing.Color = System.Drawing.Color.LightGray
        <DefaultValue("System.Drawing.Color.LightGray")>
        Public Property GradientBackColor2() As System.Drawing.Color
            Get
                Return _GradientBackColor2
            End Get
            Set(ByVal value As System.Drawing.Color)
                If _GradientBackColor2 <> value Then
                    _GradientBackColor2 = value
                    RaisePaintEvent(Me, Nothing)
                    Me.Invalidate()
                End If
            End Set
        End Property

#End Region

#Region " Border "

        Private _TitleBarBorderColor As System.Drawing.Color = System.Drawing.Color.Gray
        <DefaultValue("System.Drawing.Color.Gray")>
        <Category("Border")>
        Public Property TitleBarBorderColor() As System.Drawing.Color
            Get
                Return _TitleBarBorderColor
            End Get
            Set(value As System.Drawing.Color)
                If _TitleBarBorderColor <> value Then
                    pnlTitleBarMetroForm.BackColor = value
                    _TitleBarBorderColor = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _TitleBarBorderWidth As Integer = 2
        <DefaultValue(2)>
        <Category("Border")>
        Public Property TitleBarBorderWidth As Integer
            Get
                Return _TitleBarBorderWidth
            End Get
            Set(value As Integer)
                If _TitleBarBorderWidth <> value Then
                    pnlTitleBarMetroForm.Height = value
                    _TitleBarBorderWidth = value
                    Me.Invalidate()
                End If
            End Set
        End Property


        Private _BorderColor As System.Drawing.Color = System.Drawing.Color.Gray
        <DefaultValue("System.Drawing.Color.Gray")>
        <Category("Border")>
        Public Property BorderColor As System.Drawing.Color
            Get
                Return _BorderColor
            End Get
            Set(value As System.Drawing.Color)
                If _BorderColor <> value Then
                    _BorderColor = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _BorderWidth As Integer = 2
        <DefaultValue(2)>
        <Category("Border")>
        Public Property BorderWidth As Integer
            Get
                Return _BorderWidth
            End Get
            Set(value As Integer)
                If _BorderWidth <> value Then
                    Me.Height += value - _BorderWidth
                    Me.Width += value - _BorderWidth
                    _BorderWidth = value
                    Me.Invalidate()
                End If
            End Set
        End Property

#End Region

#Region " StatusBar "

        Private _SizingGrip As Boolean = True
        <DefaultValue(True)>
        Public Property SizingGrip As Boolean
            Get
                Return _SizingGrip
            End Get
            Set(value As Boolean)
                If _SizingGrip <> value Then
                    _SizingGrip = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _ShowStatusBar As Boolean = False
        <DefaultValue(False)>
        Public Property ShowStatusBar() As Boolean
            Get
                Return _ShowStatusBar
            End Get
            Set(ByVal value As Boolean)
                If _ShowStatusBar <> value Then
                    _ShowStatusBar = value
                    Me.Invalidate()
                End If
            End Set
        End Property

#End Region

#Region " Title Bar "

        Private privTiTleBarBackColor As Color = Color.Transparent
        Private privTiTleBarForeColor As Color = Color.Transparent

        Private _ShowTitleBar As Boolean = True
        <DefaultValue(True)>
        Public Property ShowTitleBar() As Boolean
            Get
                Return _ShowTitleBar
            End Get
            Set(ByVal value As Boolean)
                If _ShowTitleBar <> value Then
                    _ShowTitleBar = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _TitleBarBackColor As Color
        <DefaultValue("Transparent")>
        Public Property TiTleBarBackColor() As Color
            Get
                Return _TitleBarBackColor
            End Get
            Set(ByVal value As Color)
                If _TitleBarBackColor <> value Then
                    _TitleBarBackColor = value
                    Me.btnCloseMetroForm.BackColor = value
                    Me.btnMaximizeRestoreMetroForm.BackColor = value
                    Me.btnMinimizeMetroForm.BackColor = value
                    Me.pnlTitleMetroForm.BackColor = value
                    Me.pbIconMetroForm.BackColor = Color.Transparent
                    If Me.privTiTleBarBackColor = Color.Transparent And Me.privTiTleBarBackColor <> value Then
                        Me.privTiTleBarBackColor = value
                    End If
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _TitleBarBackColorOnFocus As Color
        <DefaultValue("Transparent")>
        Public Property TitleBarBackColorOnFocus() As Color
            Get
                Return _TitleBarBackColorOnFocus
            End Get
            Set(ByVal value As Color)
                If _TitleBarBackColorOnFocus <> value Then
                    _TitleBarBackColorOnFocus = value
                End If
            End Set
        End Property

        Private _TitleBarForeColor As Color
        <DefaultValue("Black")>
        Public Property TitleBarForeColor() As Color
            Get
                Return _TitleBarForeColor
            End Get
            Set(ByVal value As Color)
                If _TitleBarForeColor <> value Then
                    _TitleBarForeColor = value
                    Me.lblTitleMetroForm.ForeColor = value
                    If Me.privTiTleBarForeColor = Color.Transparent And Me.privTiTleBarForeColor <> value Then
                        Me.privTiTleBarForeColor = value
                    End If
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _TitleBarForeColorOnFocus As Color
        <DefaultValue("Black")>
        Public Property TitleBarForeColorOnFocus() As Color
            Get
                Return _TitleBarForeColorOnFocus
            End Get
            Set(ByVal value As Color)
                If _TitleBarForeColorOnFocus <> value Then
                    _TitleBarForeColorOnFocus = value
                End If
                Me.Invalidate()
            End Set
        End Property

        Private _TileBarFont As Font
        Public Property TileBarFont() As System.Drawing.Font
            Get
                Return _TileBarFont
            End Get
            Set(ByVal value As System.Drawing.Font)
                _TileBarFont = value
                Me.lblTitleMetroForm.Font = value
            End Set
        End Property

        Private _TitleBarLogo As Image
        Public Property TitleBarLogo() As Image
            Get
                Return _TitleBarLogo
            End Get
            Set(ByVal value As Image)
                _TitleBarLogo = value
            End Set
        End Property

        Private _TitleBarLogoOnFocus As Image
        Public Property TitleBarLogoOnFocus() As Image
            Get
                Return _TitleBarLogoOnFocus
            End Get
            Set(ByVal value As Image)
                _TitleBarLogoOnFocus = value
            End Set
        End Property

        Private _TileBarLogoSize As Size
        Public Property TileBarLogoSize() As Size
            Get
                Return _TileBarLogoSize
            End Get
            Set(ByVal value As Size)
                If value <> _TileBarLogoSize Then
                    _TileBarLogoSize = value
                    Me.pbIconMetroForm.Size = value
                    Me.pnlTitleMetroFormLogo.Size = value
                    Me.pnlTitleMetroFormLogo.Width = value.Width + 6
                    Me.Invalidate()
                End If
            End Set
        End Property

#End Region

#End Region

#Region " Form moving "

        Private Const WM_SYSCOMMAND As Integer = &H112&
        Private Const MOUSE_MOVE As Integer = &HF012&

        <System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
        Private Shared Sub ReleaseCapture()
        End Sub

        <System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint:="SendMessage")>
        Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
        End Sub

        Private Sub MoveForm()
            ReleaseCapture()
            SendMessage(Me.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0)
        End Sub

        Private _Size As System.Drawing.Size

        Private Sub Form_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, lblTitleMetroForm.MouseMove
            If e.Button = System.Windows.Forms.MouseButtons.Left Then

                If _Size <> Nothing Then
                    Me.Size = _Size
                    _Size = Nothing
                End If

                MoveForm()

                'ABAIXO A LOGICA MOVE/REDIMENSIONA O FORMULÁRIO CASO O MESMO SEJA MOVIDO PARA UMA POSIÇÃO À ESQUERDA DO MONITOR QUE SEJA
                'SUPERIOR A LARGURA DO FORMULARIO
                If Me.MaximizeBox Then
                    If Me.Left <= (Me.Width * -1) Then
                        If Me.Size <> New System.Drawing.Size(Screen.GetWorkingArea(New Point(e.X, e.Y)).Width / 2, Screen.GetWorkingArea(New Point(e.X, e.Y)).Height) And
                            Me.Location <> New System.Drawing.Point(0, 0) Then
                            _Size = Me.Size
                            Me.Size = New System.Drawing.Size(Screen.GetWorkingArea(New Point(e.X, e.Y)).Width / 2, Screen.GetWorkingArea(New Point(e.X, e.Y)).Height)
                            Me.Location = New System.Drawing.Point(0, 0)
                        End If
                    End If
                End If
            Else
                Me.Cursor = Cursors.[Default]
            End If
        End Sub

#End Region

#Region " Control box "

        Public Event CloseClick As EventHandler
        Public Event MaximizeClick As EventHandler
        Public Event RestoreClick As EventHandler
        Public Event MinimizeClick As EventHandler

        Friend Overridable Sub OnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimizeMetroForm.Click, tsmiMinimizeMetroForm.Click
            Me.WindowState = FormWindowState.Minimized
            RaiseEvent MinimizeClick(Me, e)
        End Sub
        Friend Overridable Sub OnMaximizeClick(sender As Object, e As EventArgs) Handles btnMaximizeRestoreMetroForm.Click, tsmiMaximizeMetroForm.Click
            If Me.WindowState = FormWindowState.Normal Then
                Me.WindowState = FormWindowState.Maximized
                Me.ttMetroForm.SetToolTip(Me.btnMaximizeRestoreMetroForm, Me.tsmiRestoreMetroForm.Text)
                RaiseEvent MaximizeClick(Me, e)
            Else
                Me.WindowState = FormWindowState.Normal
                Me.ttMetroForm.SetToolTip(Me.btnMaximizeRestoreMetroForm, Me.tsmiMaximizeMetroForm.Text)
                RaiseEvent RestoreClick(Me, e)
            End If
        End Sub
        Friend Overridable Sub OnRestoreClick(sender As Object, e As EventArgs) Handles tsmiRestoreMetroForm.Click 'Handles ButtonMaximizeRestore.Click
            OnMaximizeClick(sender, e)
        End Sub
        Friend Overridable Sub OnCloseClick(sender As Object, e As EventArgs) Handles btnCloseMetroForm.Click, tsmiCloseMetroForm.Click
            RaiseEvent CloseClick(Me, e)
            Me.Close()
        End Sub
        Friend Overridable Sub OnControlButtonEnter(sender As Object, e As EventArgs) Handles btnMinimizeMetroForm.MouseEnter, btnMaximizeRestoreMetroForm.MouseEnter, btnCloseMetroForm.MouseEnter
            Try
                If Not IsNothing(sender) Then
                    With CType(sender, System.Windows.Forms.Button)
                        If .Image IsNot Nothing Then
                            .Tag = .Image
                            .Image = .Image.GetColorMask(Me.BorderColor.GetDarkColor(64))
                        End If
                    End With
                End If
            Catch ex As Exception
            End Try
        End Sub
        Friend Overridable Sub OnControlButtonLeave(sender As Object, e As EventArgs) Handles btnMinimizeMetroForm.MouseLeave, btnMaximizeRestoreMetroForm.MouseLeave, btnCloseMetroForm.MouseLeave
            Try
                If Not IsNothing(sender) Then
                    With CType(sender, System.Windows.Forms.Button)
                        .Image = .Tag
                        .Tag = Nothing
                    End With
                End If
            Catch ex As Exception
            End Try
        End Sub
        Private Sub OnControlButtonDown(sender As Object, e As EventArgs) Handles btnMinimizeMetroForm.MouseDown, btnMaximizeRestoreMetroForm.MouseDown, btnCloseMetroForm.MouseDown
            Try
                If Not IsNothing(sender) Then
                    With CType(sender, System.Windows.Forms.Button)
                        If .Tag IsNot Nothing Then
                            .Image = CType(.Tag, System.Drawing.Image).GetColorMask(Me.BorderColor)
                        End If
                    End With
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region

        Private Sub Form_Click(sender As Object, e As MouseEventArgs) Handles pbIconMetroForm.MouseClick, Me.MouseClick, lblTitleMetroForm.MouseClick
            If sender Is pbIconMetroForm Then
                If e.Button = System.Windows.Forms.MouseButtons.Left Then _
                pbIconMetroForm.ContextMenuStrip.Show(pbIconMetroForm, pbIconMetroForm.Location)
            Else
                If e.Button = System.Windows.Forms.MouseButtons.Right Then
                    Dim _rect As New System.Drawing.Rectangle(0, 0, Me.Width, Me.pbIconMetroForm.Height + Me.pbIconMetroForm.Top)
                    If _rect.Contains(e.X, e.Y) Then
                        pbIconMetroForm.ContextMenuStrip.Show(sender, e.Location)
                    End If
                End If
            End If
        End Sub

        Private Sub Form_DoubleClick(sender As Object, e As MouseEventArgs) Handles lblTitleMetroForm.MouseDoubleClick
            Dim _rect As New System.Drawing.Rectangle(0, 0, Me.Width, Me.pbIconMetroForm.Height + Me.pbIconMetroForm.Top)
            If _rect.Contains(e.X, e.Y) Then
                Me.btnMaximizeRestoreMetroForm.PerformClick()
            End If

        End Sub

#Region " StatusStrip Mouse "

        'Private Const WM_NCLBUTTONDOWN As Integer = &HA1S
        'Private Const HTBOTTOMRIGHT As Integer = 17

        'Private Sub StatusStripMetro_MouseDown(sender As Object, e As MouseEventArgs) Handles StatusStripMetro.MouseDown
        '    If Me.WindowState <> FormWindowState.Maximized Then
        '        If e.Button = Windows.Forms.MouseButtons.Left Then
        '            Me.Capture = False

        '            Dim theCursor As Cursor = Cursors.Arrow
        '            Dim Direction As New IntPtr(Bottom)

        '            If e.X = StatusStripMetro.Width - 1 Or e.X = StatusStripMetro.Width - 2 Or e.X = StatusStripMetro.Width - 3 Or e.X = StatusStripMetro.Width - 4 Or e.X = StatusStripMetro.Width - 5 _
        '                Or e.Y = StatusStripMetro.Height - 1 Or e.Y = StatusStripMetro.Height - 2 Or e.Y = StatusStripMetro.Height - 3 Or e.Y = StatusStripMetro.Height - 4 Or e.Y = StatusStripMetro.Height - 5 Then
        '                Select Case e.X
        '                    Case StatusStripMetro.Width - 5 To StatusStripMetro.Width - 1
        '                        Select Case e.Y
        '                            Case StatusStripMetro.Height - 5 To StatusStripMetro.Height - 1
        '                                Direction = CType(HTBOTTOMRIGHT, IntPtr)
        '                                theCursor = Cursors.SizeNWSE
        '                        End Select
        '                End Select
        '                Me.Cursor = theCursor
        '                Dim msg As Message = _
        '                        Message.Create(Me.Handle, WM_NCLBUTTONDOWN, _
        '                            Direction, IntPtr.Zero)
        '                Me.DefWndProc(msg)

        '            End If
        '        End If
        '    End If
        'End Sub
        'Private Sub StatusStripMetro_MouseMove(sender As Object, e As MouseEventArgs) Handles StatusStripMetro.MouseMove
        '    If Me.WindowState <> FormWindowState.Maximized Then
        '        If e.X = StatusStripMetro.Width - 1 Or e.X = StatusStripMetro.Width - 2 Or e.X = StatusStripMetro.Width - 3 Or e.X = StatusStripMetro.Width - 4 Or e.X = StatusStripMetro.Width - 5 _
        '        Or e.Y = StatusStripMetro.Height - 1 Or e.Y = StatusStripMetro.Height - 2 Or e.Y = StatusStripMetro.Height - 3 Or e.Y = StatusStripMetro.Height - 4 Or e.Y = StatusStripMetro.Height - 5 Then
        '            Dim theCursor As Cursor = Cursors.Arrow
        '            Select Case e.X
        '                Case StatusStripMetro.Width - 5 To StatusStripMetro.Width - 1
        '                    Select Case e.Y
        '                        Case StatusStripMetro.Height - 5 To StatusStripMetro.Height - 1
        '                            theCursor = Cursors.SizeNWSE
        '                            'If e.Button = Windows.Forms.MouseButtons.Left Then
        '                            '    'Me.ARNG_WINDOW(Me.Size)
        '                            'End If
        '                            Me.Invalidate()
        '                    End Select
        '            End Select
        '            Me.Cursor = theCursor
        '        End If
        '    End If
        'End Sub
        'Private Sub StatusStripMetro_MouseUp(sender As Object, e As MouseEventArgs) Handles StatusStripMetro.MouseUp
        '    If StatusStripMetro.SizeGripBounds.Contains(e.Location) Then
        '        Me.Cursor = Cursors.SizeNWSE
        '    End If
        'End Sub

        Private Sub Form_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            Me.Invalidate()
        End Sub

#End Region

        Private Sub MyMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                'If Me.Width - BorderWidth > e.Location.X AndAlso e.Location.X > BorderWidth AndAlso e.Location.Y > BorderWidth Then
                '    MoveControl(Me.Handle)
                'Else
                If Me.WindowState <> FormWindowState.Maximized Then
                    ResizeForm(resizeDir)
                End If
                'End If
            End If
        End Sub

        Public Enum ResizeDirection
            None = 0
            Left = 1
            TopLeft = 2
            Top = 4
            TopRight = 8
            Right = 16
            BottomRight = 32
            Bottom = 64
            BottomLeft = 128
        End Enum

        Private _resizeDir As ResizeDirection = ResizeDirection.None

        Public Property resizeDir() As ResizeDirection
            Get
                Return _resizeDir
            End Get
            Set(ByVal value As ResizeDirection)
                If _resizeDir <> value Then
                    _resizeDir = value
                    'Change cursor
                    Select Case value
                        Case ResizeDirection.Left
                            Me.Cursor = Cursors.SizeWE

                        Case ResizeDirection.Right
                            Me.Cursor = Cursors.SizeWE

                        Case ResizeDirection.Top
                            Me.Cursor = Cursors.SizeNS

                        Case ResizeDirection.Bottom
                            Me.Cursor = Cursors.SizeNS

                        Case ResizeDirection.BottomLeft
                            Me.Cursor = Cursors.SizeNESW

                        Case ResizeDirection.TopRight
                            Me.Cursor = Cursors.SizeNESW

                        Case ResizeDirection.BottomRight
                            Me.Cursor = Cursors.SizeNWSE

                        Case ResizeDirection.TopLeft
                            Me.Cursor = Cursors.SizeNWSE

                        Case Else
                            Me.Cursor = Cursors.Default
                    End Select
                End If

            End Set
        End Property

        'Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        '    MyBase.OnMouseMove(e)
        '    'Calculate which direction to resize based on mouse position
        '    resizeDir = ResizeDirection.None
        '    Me.Cursor = Cursors.Default

        '    If e.Location.X < 4 And e.Location.Y < 4 Then
        '        resizeDir = ResizeDirection.TopLeft
        '    ElseIf e.Location.X < 4 And e.Location.Y > Me.Height - 4 Then
        '        resizeDir = ResizeDirection.BottomLeft
        '    ElseIf e.Location.X > Me.Width - 4 And e.Location.Y > Me.Height - 4 Then
        '        resizeDir = ResizeDirection.BottomRight

        '    ElseIf e.Location.X > Me.Width - 4 And e.Location.Y < 4 Then
        '        resizeDir = ResizeDirection.TopRight
        '    ElseIf e.Location.X < 4 Then
        '        resizeDir = ResizeDirection.Left
        '    ElseIf e.Location.X > Me.Width - 4 Then
        '        resizeDir = ResizeDirection.Right
        '    ElseIf e.Location.Y < 4 Then
        '        resizeDir = ResizeDirection.Top
        '    ElseIf e.Location.Y > Me.Height - 4 Then
        '        resizeDir = ResizeDirection.Bottom
        '    ElseIf e.Location.X > Me.Width - 4 And e.Location.Y > Me.Height - 4 Then
        '        resizeDir = ResizeDirection.BottomRight
        '    Else
        '        resizeDir = ResizeDirection.None
        '        Me.Cursor = Cursors.Default
        '    End If
        'End Sub

        Protected Overrides Sub OnControlAdded(e As ControlEventArgs)
            MyBase.OnControlAdded(e)
            On Error Resume Next
            AddHandler DirectCast(e.Control, Control).MouseMove, AddressOf MyMouseMove
            AddHandler DirectCast(e.Control, Control).MouseDown, AddressOf MyMouseDown
        End Sub

        Public Overridable Sub MyMouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
            'Calculate which direction to resize based on mouse position
            resizeDir = ResizeDirection.None
            MyBase.Cursor = Cursors.Default

            If sender.name <> MyBase.Name Then Exit Sub

            'If e.Location.X < 4 And e.Location.Y < 4 Then
            '    resizeDir = ResizeDirection.TopLeft
            'ElseIf e.Location.X < 4 And e.Location.Y > MyBase.Height - 4 Then
            '    resizeDir = ResizeDirection.BottomLeft
            'ElseIf e.Location.X > MyBase.Width - 4 And e.Location.Y > MyBase.Height - 4 Then
            '    resizeDir = ResizeDirection.BottomRight

            'ElseIf e.Location.X > MyBase.Width - 4 And e.Location.Y < 4 Then
            '    resizeDir = ResizeDirection.TopRight
            'ElseIf e.Location.X < 4 Then
            '    resizeDir = ResizeDirection.Left
            'ElseIf e.Location.X > MyBase.Width - 4 Then
            '    resizeDir = ResizeDirection.Right
            'ElseIf e.Location.Y < 4 Then
            '    resizeDir = ResizeDirection.Top
            'ElseIf e.Location.Y > MyBase.Height - 4 Then
            '    resizeDir = ResizeDirection.Bottom
            'ElseIf e.Location.X > MyBase.Width - 4 And e.Location.Y > MyBase.Height - 4 Then
            '    resizeDir = ResizeDirection.BottomRight
            'Else
            '    resizeDir = ResizeDirection.None
            '    MyBase.Cursor = Cursors.Default
            'End If

            If e.Location.X > MyBase.Width - 4 And e.Location.Y > MyBase.Height - 4 Then
                resizeDir = ResizeDirection.BottomRight
            Else
                resizeDir = ResizeDirection.None
                MyBase.Cursor = Cursors.Default
            End If

            'Location = New Point(e.Location.X - Me._start_point.X, e.Location.Y - Me._start_point.Y)

        End Sub

        Private Sub MoveControl(ByVal hWnd As IntPtr)
            ReleaseCapture()
            SendMessage(hWnd, WM_NCLBUTTONDOWN, HTCAPTION, 0)
        End Sub

        Private Sub ResizeForm(ByVal direction As ResizeDirection)
            Dim dir As Integer = -1
            Select Case direction
                Case ResizeDirection.Left
                    dir = HTLEFT
                Case ResizeDirection.TopLeft
                    dir = HTTOPLEFT
                Case ResizeDirection.Top
                    dir = HTTOP
                Case ResizeDirection.TopRight
                    dir = HTTOPRIGHT
                Case ResizeDirection.Right
                    dir = HTRIGHT
                Case ResizeDirection.BottomRight
                    dir = HTBOTTOMRIGHT
                Case ResizeDirection.Bottom
                    dir = HTBOTTOM
                Case ResizeDirection.BottomLeft
                    dir = HTBOTTOMLEFT
            End Select

            If dir <> -1 Then
                ReleaseCapture()
                SendMessage(Me.Handle, WM_NCLBUTTONDOWN, dir, 0)
            End If

        End Sub

        Private Const WM_NCLBUTTONDOWN As Integer = &HA1
        Private Const HTBORDER As Integer = 18
        Private Const HTBOTTOM As Integer = 15
        Private Const HTBOTTOMLEFT As Integer = 16
        Private Const HTBOTTOMRIGHT As Integer = 17
        Private Const HTCAPTION As Integer = 2
        Private Const HTLEFT As Integer = 10
        Private Const HTRIGHT As Integer = 11
        Private Const HTTOP As Integer = 12
        Private Const HTTOPLEFT As Integer = 13
        Private Const HTTOPRIGHT As Integer = 14

        Protected Overrides Sub OnActivated(e As EventArgs)
            Me.TiTleBarBackColor = Me.TitleBarBackColorOnFocus
            Me.TitleBarForeColor = Me.TitleBarForeColorOnFocus
            Me.pbIconMetroForm.Image = Me.TitleBarLogoOnFocus
            MyBase.OnActivated(e)
            Me.Invalidate()
        End Sub

        Protected Overrides Sub OnDeactivate(e As EventArgs)
            Me.TiTleBarBackColor = Me.privTiTleBarBackColor
            Me.TitleBarForeColor = Me.privTiTleBarForeColor
            Me.pbIconMetroForm.Image = Me.TitleBarLogo
            MyBase.OnDeactivate(e)
            Me.Invalidate()
        End Sub

        'Protected Overrides Sub OnLoad(e As EventArgs)
        '    MyBase.OnLoad(e)
        '    Try
        '        Dim rect As System.Drawing.Rectangle = Screen.GetWorkingArea(Me)
        '        Me.MaximizedBounds = Screen.GetWorkingArea(rect)
        '    Catch ex As Exception
        '        '
        '    End Try
        'End Sub


    End Class


End Namespace
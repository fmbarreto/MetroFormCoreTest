Imports System.ComponentModel


Namespace Sisecom.Windows.Forms.Metro

    <DefaultEvent("Click")>
    <Serializable>
    Public Class SlidePanel

        Public ListOfPanels As New List(Of System.Windows.Forms.Panel)
        Private PanelVisible As Panel
        Private WithEvents mtimer As New Timer
        Private Count As Integer
        Public Event ChangeIndex As EventHandler

        Public Sub New()
            InitializeComponent()
            If Me.SlideStep = 0 Then
                Me.SlideStep = CInt(Me.pnlPanels.Width / 10)
            End If
        End Sub

        Private _Title As String = "Título"
        <Browsable(True)>
        <DefaultValue("Título")>
        <Editor("MultilineStringEditor", "UITypeEditor")>
        Public Property Title As String
            Get
                Return _Title
            End Get
            Set(value As String)
                If _Title <> value Then
                    _Title = value
                    Me.lblTitulo.Text = value
                    If value = "" Then
                        Me.lblTitulo.Visible = False
                    Else
                        Me.lblTitulo.Visible = True
                    End If
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _MaxIndex As Long = 0
        <Browsable(True)>
        <DefaultValue(0)>
        Public Property MaxIndex() As Long
            Get
                Return _MaxIndex
            End Get
            Set(ByVal value As Long)
                If _MaxIndex <> value Then
                    _MaxIndex = value
                    Me.InvokePaint(Me, Nothing)
                End If
            End Set
        End Property

        Private _Index As Long = 0
        <Browsable(False)>
        <DefaultValue(0)>
        Public Property Index() As Long
            Get
                Return _Index
            End Get
            Set(ByVal value As Long)
                If _Index <> value Then
                    Me.LastIndex = Me.Index
                    _Index = value
                    If Me.Index = 0 Then
                        Me.btnLeft.Visible = False
                    Else
                        Me.btnLeft.Visible = True
                    End If
                    If Me.Index = Me.MaxIndex Then
                        Me.btnRight.Visible = False
                    Else
                        Me.btnRight.Visible = True
                    End If
                    RaiseEvent ChangeIndex(Me, New EventArgs)
                End If
            End Set
        End Property

        Public ReadOnly Property PanelCenter() As Long
            Get
                Return Me.pnlPanels.Width
            End Get
        End Property

        Private _SlideStep As Integer = 0
        <Browsable(True)>
        <DefaultValue(0)>
        Public Property SlideStep() As Integer
            Get
                Return _SlideStep
            End Get
            Set(ByVal value As Integer)
                If _SlideStep <> value Then
                    _SlideStep = value
                End If
            End Set
        End Property

        Private _LastIndex As Long = 0
        <Browsable(False)>
        <DefaultValue(0)>
        Public Property LastIndex() As Long
            Get
                Return _LastIndex
            End Get
            Set(ByVal value As Long)
                If _LastIndex <> value Then
                    _LastIndex = value
                End If
            End Set
        End Property

        Private _ForeColor As Color = Color.FromArgb(255, 255, 255)
        Public Overrides Property ForeColor() As Color
            Get
                Return _ForeColor
            End Get
            Set(ByVal value As Color)
                If _ForeColor <> value Then
                    _ForeColor = value
                End If
            End Set
        End Property

        Private _ButtonLeftImage As Image
        Public Property ButtonLeftImage() As Image
            Get
                Return _ButtonLeftImage
            End Get
            Set(ByVal value As Image)
                If _ButtonLeftImage IsNot value Then
                    _ButtonLeftImage = value
                    Me.btnLeft.Image = value
                End If
            End Set
        End Property

        Private _ButtonRightImage As Image
        Public Property ButtonRightImage() As Image
            Get
                Return _ButtonRightImage
            End Get
            Set(ByVal value As Image)
                If _ButtonRightImage IsNot value Then
                    _ButtonRightImage = value
                    Me.btnRight.Image = value
                End If
            End Set
        End Property

        Protected Overrides Sub OnSizeChanged(e As EventArgs)
            MyBase.OnSizeChanged(e)
            Me.Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            If Me.Index = 0 Then
                Me.btnLeft.Visible = False
            Else
                Me.btnLeft.Visible = True
            End If

            If Me.Index = Me.MaxIndex Then
                Me.btnRight.Visible = False
            Else
                Me.btnRight.Visible = True
            End If

            Me.pnl1.Size = Me.pnlPanels.Size
            Me.pnl2.Size = Me.pnlPanels.Size

            If Me.Index = 0 And Me.LastIndex = 0 Then
                Me.pnl1.Location = New Point(0, 0)
                Me.pnl2.Location = New Point(Me.pnl2.Width, 0)
                Me.PanelVisible = Me.pnl1
                If Not IsNothing(ListOfPanels) AndAlso ListOfPanels.Count > 0 AndAlso Me.MaxIndex = 0 Then
                    Me.MaxIndex = Me.ListOfPanels.Count - 1
                End If
            End If


        End Sub

        Protected Overrides Sub OnClick(e As EventArgs)
            MyBase.OnClick(e)
        End Sub

        Private Sub Buttons_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles btnLeft.PreviewKeyDown, btnRight.PreviewKeyDown
            If e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter Then
                Me.PerformClick()
            End If
        End Sub

        Public Sub PerformClick()
            MyBase.InvokeOnClick(Me, New EventArgs)
        End Sub

        Private Sub Buttons_MouseDown(sender As Object, e As MouseEventArgs) Handles btnLeft.MouseDown, btnRight.MouseDown
            '
        End Sub

        Private Sub Buttons_KeyDown(sender As Object, e As KeyEventArgs) Handles btnLeft.KeyDown, btnRight.KeyDown
            '
        End Sub

        Private Sub Buttons_MouseUp(sender As Object, e As MouseEventArgs) Handles btnLeft.MouseUp, btnRight.MouseUp
            '
        End Sub

        Private Sub Buttons_KeyUp(sender As Object, e As KeyEventArgs) Handles btnLeft.KeyUp, btnRight.KeyUp
            '
        End Sub

        Private Sub Buttons_MouseEnter(sender As Object, e As EventArgs) Handles btnLeft.MouseEnter, btnLeft.GotFocus, btnRight.MouseEnter, btnRight.GotFocus
            Dim _rect As New Rectangle(0, 0, Me.Width, Me.Height)
        End Sub

        Private Sub Buttons_MouseLeave(sender As Object, e As EventArgs) Handles btnLeft.MouseLeave, btnLeft.LostFocus, btnRight.MouseLeave, btnRight.LostFocus
            Me.Invalidate()
        End Sub

        Public Sub Buttons_Click(sender As Object, e As EventArgs) Handles btnLeft.Click, btnRight.Click
            Me.InvokeOnClick(Me, New EventArgs)
            Dim objeto As Button = TryCast(sender, Button)

            If Not IsNothing(objeto) Then
                Select Case objeto.Name
                    Case "btnLeft"
                        Me.LastIndex = Me.Index
                        Me.Index -= 1
                    Case "btnRight"
                        Me.LastIndex = Me.Index
                        Me.Index += 1
                End Select
            End If

            If Me.Index >= LastIndex Then
                If Me.PanelVisible.Name = Me.pnl1.Name Then
                    Me.pnl1.Location = New Point(0, 0)
                    Me.pnl2.Location = New Point(Me.pnlPanels.Width, 0)
                Else
                    Me.pnl1.Location = New Point(Me.pnlPanels.Width, 0)
                    Me.pnl2.Location = New Point(0, 0)
                End If
            Else
                If Me.PanelVisible.Name = Me.pnl1.Name Then
                    Me.pnl1.Location = New Point(0, 0)
                    Me.pnl2.Location = New Point(-Me.pnlPanels.Width, 0)
                Else
                    Me.pnl1.Location = New Point(-Me.pnlPanels.Width, 0)
                    Me.pnl2.Location = New Point(0, 0)
                End If
            End If

            Me.AddPanels(Me.ListOfPanels(Index))

            Me.Count = 0
            Me.mtimer.Enabled = True
            Me.mtimer.Interval = 1
        End Sub

        Public Sub ClearPanels()
            Me.pnl1.Controls.Clear()
            Me.pnl2.Controls.Clear()
            Me.ListOfPanels.Clear()
            Me.Index = 0
            Me.LastIndex = 0
        End Sub

        Public Sub AddPanels(ByVal p As Panel)
            If Me.pnl1.Controls.Count = 0 Then
                Me.pnl1.Controls.Add(p)
            Else
                If Me.PanelVisible.Name = Me.pnl1.Name Then
                    Me.pnl2.Controls.Clear()
                    Me.pnl2.Controls.Add(p)
                Else
                    Me.pnl1.Controls.Clear()
                    Me.pnl1.Controls.Add(p)
                End If
            End If
            p.Dock = DockStyle.Fill
            p.Visible = True
        End Sub

        Private Sub mtimer_Tick(sender As Object, e As EventArgs) Handles mtimer.Tick
            If Me.Count >= Me.pnlPanels.Width Then
                If Me.PanelVisible.Name = Me.pnl1.Name Then
                    Me.PanelVisible = Me.pnl2
                Else
                    Me.PanelVisible = Me.pnl1
                End If
                If Me.Index >= LastIndex Then
                    If Me.PanelVisible.Name = Me.pnl1.Name Then
                        Me.pnl1.Location = New Point(0, 0)
                        Me.pnl2.Location = New Point(Me.pnlPanels.Width, 0)
                    Else
                        Me.pnl1.Location = New Point(Me.pnlPanels.Width, 0)
                        Me.pnl2.Location = New Point(0, 0)
                    End If
                Else
                    If Me.PanelVisible.Name = Me.pnl1.Name Then
                        Me.pnl1.Location = New Point(0, 0)
                        Me.pnl2.Location = New Point(-Me.pnlPanels.Width, 0)
                    Else
                        Me.pnl1.Location = New Point(-Me.pnlPanels.Width, 0)
                        Me.pnl2.Location = New Point(0, 0)
                    End If
                End If
                Me.mtimer.Enabled = False
                Exit Sub
            End If
            If Me.Index >= Me.LastIndex Then
                Me.pnl1.Location = New Point(Me.pnl1.Location.X - Me.SlideStep, 0)
                Me.pnl2.Location = New Point(Me.pnl2.Location.X - Me.SlideStep, 0)
            Else
                Me.pnl1.Location = New Point(Me.pnl1.Location.X + Me.SlideStep, 0)
                Me.pnl2.Location = New Point(Me.pnl2.Location.X + Me.SlideStep, 0)
            End If
            Me.Count += Me.SlideStep
        End Sub
    End Class

End Namespace

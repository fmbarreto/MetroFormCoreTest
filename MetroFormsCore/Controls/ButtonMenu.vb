Imports System.ComponentModel


Namespace Sisecom.Windows.Forms.Metro

    <DefaultEvent("Click")>
    <Serializable>
    Public Class ButtonMenu

        Public Event CheckedChange(ByVal e As EventArgs)

        Public Sub New()
            InitializeComponent()
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
                    Me.Width = Me.lblTitulo.Width
                    Me.Height = Me.lblTitulo.Height + Me.pnlBarra.Height
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _Index As Long = 0
        <Browsable(True)>
        <DefaultValue(0)>
        Public Property Index() As Long
            Get
                Return _Index
            End Get
            Set(ByVal value As Long)
                If _Index <> value Then
                    _Index = value
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

        Private _Checked As Boolean = False
        <BrowsableAttribute(True)>
        <DefaultValue(False)>
        Public Property Checked As Boolean
            Get
                Return _Checked
            End Get
            Set(value As Boolean)
                If _Checked <> value Then
                    _Checked = value
                    If value = True Then
                        Me.pnlBarra.BackColor = BarColor
                    End If
                    RaiseEvent CheckedChange(New EventArgs)
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _BarColor As Color = Color.FromArgb(71, 117, 175)
        Public Property BarColor() As Color
            Get
                Return _BarColor
            End Get
            Set(ByVal value As Color)
                _BarColor = value
                If _BarColor <> value Then
                    Me.pnlBarra.BackColor = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _MouseEnterBarColor As Color = Color.White
        Public Property MouseEnterBarColor() As Color
            Get
                Return _MouseEnterBarColor
            End Get
            Set(ByVal value As Color)
                _MouseEnterBarColor = value
            End Set
        End Property

        Protected Overrides Sub OnFontChanged(e As EventArgs)
            MyBase.OnFontChanged(e)
            Me.Width = Me.lblTitulo.Width
            Me.Height = Me.lblTitulo.Height + Me.pnlBarra.Height
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            If Me.Checked Then
                Me.lblTitulo.Font = New Font(Me.Font, FontStyle.Bold)
                Me.pnlBarra.Visible = True
            Else
                Me.lblTitulo.Font = New Font(Me.Font, FontStyle.Regular)
                Me.pnlBarra.Visible = False
            End If

            Try
                Dim Controle As Control = Me.Parent
                If Not IsNothing(Controle) And Me.Checked Then
                    For Each c As Control In Controle.Controls
                        Select Case c.GetType()
                            Case GetType(ButtonMenu)
                                If c.Name <> Me.Name Then
                                    Dim co As ButtonMenu = DirectCast(c, ButtonMenu)
                                    If co.Checked Then Me.LastIndex = co.Index
                                    co.Checked = Not Me.Checked
                                End If
                        End Select
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Protected Overrides Sub OnClick(e As EventArgs)
            'Me.Checked = True 'Not Me.Checked
            MyBase.OnClick(e)
        End Sub

        Private Sub ButtonMenu_MouseClick(sender As Object, e As MouseEventArgs) Handles lblTitulo.MouseClick, pnlBarra.MouseClick
            Me.Checked = True 'Not Me.Checked
            Me.InvokeGotFocus(Me, New EventArgs)
            Me.InvokeOnClick(Me, New EventArgs)
        End Sub

        Public Function PesquisaControleFocado(ByVal ctr As Control) As Control
            Dim container As ContainerControl = TryCast(ctr, ContainerControl)
            Do While (container IsNot Nothing)
                ctr = container.ActiveControl
                container = TryCast(ctr, ContainerControl)
            Loop
            Return ctr
        End Function

        Private Sub _PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles lblTitulo.PreviewKeyDown, pnlBarra.PreviewKeyDown
            If e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter Then
                Me.PerformClick()
            End If
        End Sub

        Public Sub PerformClick()
            MyBase.InvokeOnClick(Me, New EventArgs)
        End Sub

        Private _pressed As Boolean = False
        Public Property pressed() As Boolean
            Get
                Return _pressed
            End Get
            Set(ByVal value As Boolean)
                _pressed = value
            End Set
        End Property

        Private Sub ButtonMenu_MouseDown(sender As Object, e As MouseEventArgs) Handles lblTitulo.MouseDown, pnlBarra.MouseDown
            Me.Left += 1
            Me.Top += 1
        End Sub

        Private Sub ButtonMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles lblTitulo.KeyDown, pnlBarra.KeyDown
            If (e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter) AndAlso Not _pressed Then
                Me.Left += 1
                Me.Top += 1
                _pressed = True
            End If
        End Sub

        Private Sub ButtonMenu_MouseUp(sender As Object, e As MouseEventArgs) Handles lblTitulo.MouseUp, pnlBarra.MouseUp
            Me.Left -= 1
            Me.Top -= 1
        End Sub

        Private Sub ButtonMenu_KeyUp(sender As Object, e As KeyEventArgs) Handles lblTitulo.KeyUp, pnlBarra.KeyUp
            If (e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter) AndAlso _pressed Then
                Me.Left -= 1
                Me.Top -= 1
                _pressed = False
            End If
        End Sub

        Private Sub ButtonMenu_MouseEnter(sender As Object, e As EventArgs) Handles lblTitulo.MouseEnter, lblTitulo.GotFocus, pnlBarra.MouseEnter, pnlBarra.GotFocus
            Dim _rect As New Rectangle(0, 0, Me.Width, Me.Height)
            If Me.Checked Then
                Me.pnlBarra.BackColor = MouseEnterBarColor
            End If
        End Sub

        Private Sub ButtonMenu_MouseLeave(sender As Object, e As EventArgs) Handles lblTitulo.MouseLeave, lblTitulo.LostFocus, pnlBarra.MouseLeave, pnlBarra.LostFocus
            Me.Invalidate()
            If Me.Checked Then
                Me.pnlBarra.BackColor = BarColor
            End If
        End Sub

    End Class

End Namespace

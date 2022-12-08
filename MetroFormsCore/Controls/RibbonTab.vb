Imports System.ComponentModel
Imports Sisecom.Windows.Forms.Metro.Extensions

Namespace Sisecom.Windows.Forms.Metro

    <DefaultEvent("ItemClick")>
    Public Class RibbonTab

        Public Sub OnParentInvalidate(sender As Object, e As EventArgs)
            Me.Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

            If Me.FindForm IsNot Nothing Then
                RemoveHandler Me.FindForm.Invalidated, AddressOf OnParentInvalidate
                AddHandler Me.FindForm.Invalidated, AddressOf OnParentInvalidate

                If TypeOf Me.FindForm Is Sisecom.Windows.Forms.Metro.Form Then
                    With CType(Me.FindForm, Sisecom.Windows.Forms.Metro.Form)
                        Me.BorderColor = .BorderColor
                    End With
                End If

            End If

            Me.Size = New System.Drawing.Size(Me.Width, 25)

            With Me.ButtonFile
                Dim _MeasureStringWidth As Integer = e.Graphics.MeasureString(Me.FileText, Me.Font).Width
                If _MeasureStringWidth > .Width Then
                    .Width = _MeasureStringWidth + Padding.Left + Padding.Right + .MinimumSize.Width
                Else
                    .Width = .MinimumSize.Width
                End If

                .Text = Me.FileText
                .BackColor = Me.BorderColor
                .ForeColor = Me.ForeColor.GetNegativeColor

            End With

            If Not Me.DesignMode Then
                _Items_Changed(Me, Nothing)
            End If

            MyBase.OnPaint(e)

        End Sub

        Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.ButtonFile.Location = New System.Drawing.Point(0, 0)
            Me.Controls.Add(Me.ButtonFile)
            _Items = New List(Of String)

        End Sub

        Private WithEvents _Items As List(Of String)

        <ListBindable(True)>
        <Localizable(True)>
        <Browsable(True)>
        <Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")>
        Public Property Items As List(Of String)
            Get
                Return _Items
            End Get
            Set(value As List(Of String))
                If _Items IsNot value Then
                    _Items = value
                End If
            End Set
        End Property

        Private _FileText As String = "FILE"
        <ListBindable(True)>
        <Localizable(True)>
        <DefaultValue("FILE")>
        Public Property FileText As String
            Get
                Return _FileText
            End Get
            Set(value As String)
                If value <> _FileText Then
                    _FileText = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Public Event ItemClick As EventHandler(Of EventArgs)

        Friend Overridable Sub OnItemClick(sender As Object, e As EventArgs)
            RaiseEvent ItemClick(sender, e)
        End Sub

        Private Sub OnItemEnter(sender As Object, e As EventArgs)
            CType(sender, Button).ForeColor = Me.BorderColor
        End Sub
        Private Sub OnItemLeave(sender As Object, e As EventArgs)
            CType(sender, Button).ForeColor = Me.ForeColor
        End Sub

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
                End If
            End Set
        End Property

        Private Sub ButtonFile_Click(sender As Object, e As EventArgs) Handles ButtonFile.Click
            RaiseEvent ItemClick(sender, e)
        End Sub

        Private Sub _Items_Changed(sender As Object, e As EventArgs)
            'Agregue su código personalizado de dibujo aquí

            If Me.Controls.OfType(Of Button).Count(Function(b) b IsNot Me.ButtonFile) <> Me.Items.Count Then

                For Each _item As Button In Me.Controls.OfType(Of Button).Where(Function(b) b IsNot Me.ButtonFile)
                    Me.Controls.Remove(_item)
                    _item.Dispose()
                Next

                For Each item As String In Me.Items.OfType(Of String)()
                    Dim _btn As New Button

                    With _btn
                        .FlatAppearance.BorderSize = 0
                        .FlatAppearance.MouseOverBackColor = Me.BackColor.GetDarkColor(4)
                        .FlatAppearance.MouseDownBackColor = Me.BackColor.GetDarkColor(8)
                        .FlatStyle = System.Windows.Forms.FlatStyle.Flat
                        .Font = Me.Font
                        .ForeColor = Me.ForeColor
                        .Location = New System.Drawing.Point(78, 0)
                        .Margin = Me.ButtonFile.Margin
                        .Size = Me.ButtonFile.MinimumSize
                        .Text = item
                        .UseVisualStyleBackColor = False
                        .TabStop = False
                    End With

                    AddHandler _btn.Click, AddressOf OnItemClick
                    AddHandler _btn.MouseEnter, AddressOf OnItemEnter
                    AddHandler _btn.MouseLeave, AddressOf OnItemLeave

                    Me.Controls.Add(_btn)

                Next
            End If

        End Sub

    End Class

End Namespace
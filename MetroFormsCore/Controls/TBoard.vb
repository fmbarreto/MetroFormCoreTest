Imports System.ComponentModel

Namespace Sisecom.Windows.Forms.Metro

    Public Class TBoard

        Private _States As String() = _
            {"Waiting", "Tech", "Jobs", "Completed"}
        Public Property States As String()
            Get
                Return _States
            End Get
            Set(value As String())
                _States = value
                Me.Invalidate()
            End Set
        End Property

        Private _StatesColors As System.Drawing.Color() = _
            {System.Drawing.Color.Salmon, System.Drawing.Color.Peru, System.Drawing.Color.Olive, System.Drawing.Color.YellowGreen}
        Public Property StatesColors As System.Drawing.Color()
            Get
                Return _StatesColors
            End Get
            Set(value As System.Drawing.Color())
                _StatesColors = value
                Me.Invalidate()
            End Set
        End Property

        Private _ItemMinHeight As Integer = 32
        <DefaultValue(32)> _
        Public Property ItemMinHeight As Integer
            Get
                Return _ItemMinHeight
            End Get
            Set(value As Integer)
                If _ItemMinHeight <> value Then
                    _ItemMinHeight = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _ShowCounters As Boolean = True
        <DefaultValue(True)> _
        <Browsable(True)> _
        Public Property ShowCounters As Boolean
            Get
                Return _ShowCounters
            End Get
            Set(value As Boolean)
                If _ShowCounters <> value Then
                    _ShowCounters = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _CounterFormat As String = "{0}"
        <DefaultValue("{0}")> _
        <Browsable(True)> _
        Public Property CounterFormat As String
            Get
                Return _CounterFormat
            End Get
            Set(value As String)
                If _CounterFormat <> value Then
                    _CounterFormat = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Public Function Count(state As Integer) As Integer
            Return Me.TableLayoutPanelBoard.GetControlFromPosition(state, 1).Controls.Count
        End Function

        Public Function Add(text As String, Optional state As Integer = 0) As TBoardItem
            Dim _return As TBoardItem = New TBoardItem With {.Text = text, .State = state}
            Me.Add(_return)
            Return _return
        End Function
        Public Sub Add(item As TBoardItem)
            _Items.Add(item)
        End Sub
        Public Sub Remove(item As TBoardItem)
            _Items.Remove(item)
        End Sub

        Public Sub Clear()
            Me._Items.Clear()
        End Sub

        Private WithEvents _Items As New TBoardItemCollection

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            For Each item As Control In Me.TableLayoutPanelBoard.Controls
                item.Dispose()
            Next
            Me.TableLayoutPanelBoard.Controls.Clear()

            Me.TableLayoutPanelBoard.RowCount = 1
            Me.TableLayoutPanelBoard.ColumnCount = 0
            Me.TableLayoutPanelBoard.RowStyles.Clear()
            Me.TableLayoutPanelBoard.ColumnStyles.Clear()

            Me.TableLayoutPanelBoard.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Me.TableLayoutPanelBoard.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

            For Each item As String In _States.ToList
                'Dim _title As Label = CType(Me.TableLayoutPanelBoard.GetControlFromPosition(_States.ToList.IndexOf(item), 0), Label)
                '_title.Text = item
                Dim _lsp As New Panel
                With _lsp
                    .AutoSize = True
                    .Dock = DockStyle.Fill
                    .ForeColor = Me.ForeColor
                    If StatesColors.Length - 1 >= _States.ToList.IndexOf(item) Then
                        .BackColor = StatesColors(_States.ToList.IndexOf(item))
                    Else
                        .BackColor = CType(Me.FindForm, Form).BorderColor
                    End If
                    .Padding = New System.Windows.Forms.Padding(6)
                    .Margin = Nothing
                End With

                Dim _ls As New Label
                With _ls
                    .AutoSize = True
                    .Dock = DockStyle.Fill
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Text = item
                    .Padding = Nothing
                    .Margin = Nothing
                End With

                Dim _lsc As New Label
                With _lsc
                    .AutoSize = True
                    .Dock = DockStyle.Right
                    .TextAlign = ContentAlignment.MiddleRight
                    .Tag = Me.CounterFormat
                    .Text = String.Format(Me.CounterFormat, 0)
                    .Padding = Nothing
                    .Margin = Nothing
                    .Visible = Me.ShowCounters
                End With

                _lsp.Controls.Add(_lsc)
                _lsp.Controls.Add(_ls)

                Me.TableLayoutPanelBoard.Controls.Add(_lsp, Me.TableLayoutPanelBoard.ColumnCount, 0)
                Me.TableLayoutPanelBoard.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))

                Dim _flp As New FlowLayoutPanel
                With _flp
                    .Dock = DockStyle.Fill
                    .AllowDrop = True
                    .Padding = Nothing
                    .Margin = Nothing
                    AddHandler .DragDrop, AddressOf FlowLayoutPanel_DragDrop
                    AddHandler .DragEnter, AddressOf FlowLayoutPanel_DragEnter
                End With

                Me.TableLayoutPanelBoard.Controls.Add(_flp, Me.TableLayoutPanelBoard.ColumnCount, 1)

                Me.TableLayoutPanelBoard.ColumnCount += 1
            Next

            For Each item As TBoardItem In Me._Items
                Dim _flpitem As FlowLayoutPanel = CType(Me.TableLayoutPanelBoard.GetControlFromPosition(item.State, 1), FlowLayoutPanel)

                Dim _its As Integer = e.Graphics.MeasureString(Split(item.Text, vbCrLf)(0), item.Font).Height * (Split(item.Text, vbCrLf).Length) + 12
                Dim _FinalHeight As Integer = Me.ItemMinHeight

                If _its > _FinalHeight Then _FinalHeight = _its

                item.Dock = DockStyle.Top
                item.Size = New System.Drawing.Size(_flpitem.Width - (item.Margin.Left * 2), _FinalHeight)
                item.HoverColor = StatesColors(item.State)
                _flpitem.SetFlowBreak(item, True)
                _flpitem.Controls.Add(item)

                With CType(CType(Me.TableLayoutPanelBoard.GetControlFromPosition(item.State, 0), Panel).Controls(0), Label)
                    .Text = String.Format(.Tag, _flpitem.Controls.Count)
                End With

            Next

        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            'MyBase.OnResize(e)
            'Me.SuspendLayout()
            For Each item As TBoardItem In Me._Items
                Dim _flpitem As FlowLayoutPanel = CType(Me.TableLayoutPanelBoard.GetControlFromPosition(item.State, 1), FlowLayoutPanel)
                If _flpitem IsNot Nothing Then
                    'item.SuspendLayout()
                    item.Size = New System.Drawing.Size(_flpitem.Width - (item.Margin.Left * 2), Me.ItemMinHeight)
                    'item.ResumeLayout()
                End If
            Next
            'Me.ResumeLayout()
        End Sub

        Private Sub TBoard_Load(sender As Object, e As EventArgs) Handles Me.Load

            'For Each item As FlowLayoutPanel In Me.TableLayoutPanelBoard.Controls.OfType(Of FlowLayoutPanel)()
            '    AddHandler item.DragDrop, AddressOf FlowLayoutPanel_DragDrop
            '    AddHandler item.DragEnter, AddressOf FlowLayoutPanel_DragEnter
            'Next


            For Each item As TBoardItem In Me._Items
                Me.Controls.Add(item)
            Next

        End Sub

        Private Sub item_MouseDown(sender As Object, e As MouseEventArgs)
            If e.Clicks = 2 Then
                If e.Button = System.Windows.Forms.MouseButtons.Left Then _
                    RaiseEvent ItemDoubleClick(Me, New TBoardItemEventArgs(sender))
            Else
                If e.Button = System.Windows.Forms.MouseButtons.Left Then _
                    CType(sender, Control).DoDragDrop(sender, DragDropEffects.Move)
            End If

        End Sub

        Public Event ItemClick As EventHandler(Of TBoardItemEventArgs)
        Public Event ItemDoubleClick As EventHandler(Of TBoardItemEventArgs)

        Public Event ItemStateChanged As EventHandler(Of TBoardItemEventArgs)

        Public Class TBoardItemEventArgs
            Inherits EventArgs
            Public Property Item As TBoardItem
            Sub New()
            End Sub
            Sub New(item As TBoardItem)
                Me.New()
                Me.Item = item
            End Sub
        End Class

        Private Sub FlowLayoutPanel_DragDrop(sender As Object, e As DragEventArgs)

            Dim source As TBoardItem = e.Data.GetData(GetType(TBoardItem))
            RaiseEvent ItemClick(Me, New TBoardItemEventArgs(source))

            With Me.TableLayoutPanelBoard

                If .GetPositionFromControl(sender).Column <> source.State Then

                    Dim _lsource As Label = CType(.GetControlFromPosition(source.State, 0), Panel).Controls(0)
                    Dim _ldest As Label = CType(.GetControlFromPosition(.GetPositionFromControl(sender).Column, 0), Panel).Controls(0)

                    CType(sender, FlowLayoutPanel).Controls.Add(source)

                    _lsource.Text = String.Format(_lsource.Tag, CType(.GetControlFromPosition(.GetPositionFromControl(_lsource.Parent).Column, 1), FlowLayoutPanel).Controls.Count)
                    _ldest.Text = String.Format(_ldest.Tag, CType(sender, FlowLayoutPanel).Controls.Count)

                    source.State = .GetPositionFromControl(sender).Column
                    CType(source, TBoardItem).HoverColor = .GetControlFromPosition(source.State, 0).BackColor

                    RaiseEvent ItemStateChanged(Me, New TBoardItemEventArgs(source))

                End If

                'source.BackColor = Color.DimGray

            End With

        End Sub
        Private Sub FlowLayoutPanel_DragEnter(sender As Object, e As DragEventArgs)
            If (e.Data.GetDataPresent(GetType(TBoardItem))) Then
                e.Effect = DragDropEffects.Move

                'Dim _c As TBoardItem = e.Data.GetData(GetType(TBoardItem))
                'Dim _lc As TBoardItem = Me.TableLayoutPanelBoard.GetControlFromPosition(_c.State, 1).Controls.OfType(Of TBoardItem).LastOrDefault
                'If _c.Parent IsNot sender Then
                '    '_c.BackColor = _c.HoverColor
                'End If

            Else
                e.Effect = DragDropEffects.None
            End If
        End Sub

        Private Sub _Items_ItemsChanged(sender As Object, e As CollectionChangeEventArgs) Handles _Items.ItemsChanged
            Select Case e.Action
                Case CollectionChangeAction.Add
                    With CType(e.Element, TBoardItem)
                        AddHandler .MouseDown, AddressOf item_MouseDown
                    End With
                    Me.Invalidate()
                Case CollectionChangeAction.Remove
                    For Each item As FlowLayoutPanel In Me.TableLayoutPanelBoard.Controls.OfType(Of FlowLayoutPanel)()
                        item.Controls.Remove(e.Element)
                    Next
                    CType(e.Element, TBoardItem).Dispose()
            End Select
        End Sub

    End Class

    Public Class TBoardItemCollection
        Implements ICollection(Of TBoardItem)

        Public Event ItemsChanged As CollectionChangeEventHandler

        Private _Items As New List(Of TBoardItem)
        Public Property Items() As List(Of TBoardItem)
            Get
                Return _Items
            End Get
            Set(value As List(Of TBoardItem))
                If Not IsReadOnly Then
                    _Items = value
                End If
            End Set
        End Property

        Public Property Item(index As Integer) As TBoardItem
            Get
                Return _Items(index)
            End Get
            Set(value As TBoardItem)
                If Not IsReadOnly Then
                    _Items(index) = value
                    _Items(index).Invalidate()
                    RaiseEvent ItemsChanged(Me, New CollectionChangeEventArgs(CollectionChangeAction.Refresh, _Items(index)))
                End If
            End Set
        End Property

        Public Event ItemAdded As EventHandler
        Public Sub Add(item As TBoardItem) Implements ICollection(Of TBoardItem).Add
            If Not _IsReadOnly Then
                _Items.Add(item)
                RaiseEvent ItemsChanged(Me, New CollectionChangeEventArgs(CollectionChangeAction.Add, item))
                RaiseEvent ItemAdded(Me, New EventArgs)
            End If
        End Sub

        Public Sub Clear() Implements ICollection(Of TBoardItem).Clear
            If Not _IsReadOnly Then _Items.Clear()
        End Sub

        Public Function Contains(item As TBoardItem) As Boolean Implements ICollection(Of TBoardItem).Contains
            Return _Items.Contains(item)
        End Function

        Public Sub CopyTo(array() As TBoardItem, arrayIndex As Integer) Implements ICollection(Of TBoardItem).CopyTo
            _Items.CopyTo(array, arrayIndex)
        End Sub

        Public ReadOnly Property Count As Integer Implements ICollection(Of TBoardItem).Count
            Get
                Return _Items.Count
            End Get
        End Property

        Private _IsReadOnly As Boolean = False
        Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of TBoardItem).IsReadOnly
            Get
                Return _IsReadOnly
            End Get
        End Property

        Public Event ItemRemoved As EventHandler
        Public Function Remove(item As TBoardItem) As Boolean Implements ICollection(Of TBoardItem).Remove
            If Not _IsReadOnly Then
                Return Me._Items.Remove(item)
                RaiseEvent ItemsChanged(Me, New CollectionChangeEventArgs(CollectionChangeAction.Remove, item))
                RaiseEvent ItemRemoved(Me, New EventArgs)
            Else
                Return False
            End If
        End Function

        Public Function GetEnumerator() As IEnumerator(Of TBoardItem) Implements IEnumerable(Of TBoardItem).GetEnumerator
            Return Me._Items.GetEnumerator
        End Function

        Private Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return Me.GetEnumerator()
        End Function

    End Class

    Public Class TBoardItem
        Inherits System.Windows.Forms.Control

        'Sub New()
        '    Me.BackColor = Color.LightGray
        'End Sub

        Private _Text As String = ""
        <Description("The text that will display as the caption."), Category("Appearance"), DefaultValue("DividerLabel")> _
        Public Overrides Property Text() As String
            Get
                Return Me._Text
            End Get
            Set(value As String)
                Me._Text = value
                Me.Invalidate()
            End Set
        End Property

        Public Property State As Integer
        Public Property HoverColor As System.Drawing.Color = Color.DarkRed

        Private Sub _MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter ', Me.GotFocus
            Dim _rect As New System.Drawing.Rectangle(1, 1, Me.Width - 2, Me.Height - 2)
            Me.CreateGraphics.DrawRectangle(New System.Drawing.Pen(Me.HoverColor, 2), _rect)
            Me.Cursor = Cursors.Hand
            'Dim _fillp As System.Drawing.Point() = {
            '    New System.Drawing.Point(Me.Width - (Me.Width / 4), 1), _
            '    New System.Drawing.Point(Me.Width, 1), _
            '    New System.Drawing.Point(Me.Width, (Me.Height / 4))}
            'Me.CreateGraphics.FillPolygon(New System.Drawing.SolidBrush(Me.HoverColor), _fillp)

            'Me.BackColor = Me.BackColor.GetDarkColor(16)
        End Sub
        Private Sub _MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave ', Me.LostFocus
            'Me.BackColor = Me.BackColor.GetLightColor(16)
            Me.Invalidate()
            Me.Cursor = Cursors.Default
        End Sub
        Private Sub _MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                Me.InvokeGotFocus(Me, New EventArgs)
            End If
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)

            Dim _FinalText As String = Me.Text
            Dim _Final As SizeF = e.Graphics.MeasureString(_FinalText, Me.Font)

            Do While (_Final.Width) + (12) > Me.Width
                _FinalText = _FinalText.Substring(0, _FinalText.Length - IIf(_FinalText.EndsWith("..."), 4, 1)) & "..."
                _Final = e.Graphics.MeasureString(_FinalText, Me.Font)
            Loop

            Dim _fs As System.Drawing.SizeF = e.Graphics.MeasureString(_FinalText, Me.Font)

            Dim _p As New System.Drawing.Point(6, (Me.Height / 2) - (_fs.Height / 2))
            e.Graphics.DrawString(_FinalText, Me.Font, New SolidBrush(Me.ForeColor), _p)

        End Sub

    End Class

End Namespace

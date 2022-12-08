Imports System.ComponentModel
Imports Sisecom.Windows.Forms.Metro.Extensions

Namespace Sisecom.Windows.Forms.Metro

    <DefaultEvent("ValueChanged")> _
    <DefaultProperty("Value")> _
    Public Class ToggleTrackBar

#Region " Properties "

        Private _Value As Integer = 0
        <DefaultValue(0)> _
        Public Property Value As Integer
            Get
                Return _Value
            End Get
            Set(value As Integer)
                If _Value <> value AndAlso value >= _MinValue And value <= _MaxValue Then
                    _Value = value
                    OnValueChanged(New EventArgs)
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _MinValue As Integer = 0
        <DefaultValue(0)> _
        Public Property MinValue As Integer
            Get
                Return _MinValue
            End Get
            Set(value As Integer)
                If _MinValue <> value Then
                    If Me.Value < _MinValue Then Me.Value = value
                    _MinValue = value
                    OnValueChanged(New EventArgs)
                    Me.Invalidate()
                End If
            End Set
        End Property
        Private _MaxValue As Integer = 100
        <DefaultValue(100)> _
        Public Property MaxValue As Integer
            Get
                Return _MaxValue
            End Get
            Set(value As Integer)
                If _MaxValue <> value Then
                    If Me.Value > _MaxValue Then Me.Value = value
                    _MaxValue = value
                    OnValueChanged(New EventArgs)
                    Me.Invalidate()
                End If
            End Set
        End Property

        Public Event ValueChanged As EventHandler
        Friend Overridable Sub OnValueChanged(e As EventArgs)
            RaiseEvent ValueChanged(Me, e)
        End Sub

        Private _ActiveColor As System.Drawing.Color = System.Drawing.SystemColors.ActiveBorder
        <DefaultValue("System.Drawing.SystemColors.ActiveBorder")> _
        Public Property ActiveColor As System.Drawing.Color
            Get
                Return _ActiveColor
            End Get
            Set(value As System.Drawing.Color)
                If value <> _ActiveColor Then
                    _ActiveColor = value
                    Me.Invalidate()
                End If
            End Set
        End Property
        Private _InactiveColor As System.Drawing.Color = System.Drawing.SystemColors.ScrollBar
        <DefaultValue("System.Drawing.SystemColors.ScrollBar")> _
        Public Property InactiveColor As System.Drawing.Color
            Get
                Return _InactiveColor
            End Get
            Set(value As System.Drawing.Color)
                If value <> _InactiveColor Then
                    _InactiveColor = value
                    Me.Invalidate()
                End If
            End Set
        End Property

#End Region

        Public Sub OnParentInvalidate(sender As Object, e As EventArgs)
            Me.Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)

            If Me.FindForm IsNot Nothing Then
                RemoveHandler Me.FindForm.Invalidated, AddressOf OnParentInvalidate
                AddHandler Me.FindForm.Invalidated, AddressOf OnParentInvalidate

                If TypeOf Me.FindForm Is Sisecom.Windows.Forms.Metro.Form Then
                    With CType(Me.FindForm, Sisecom.Windows.Forms.Metro.Form)
                        Me.ActiveColor = .BorderColor
                    End With
                End If
            End If

            Me.PanelState1.BackColor = Me.ActiveColor
            Me.PanelState2.BackColor = Me.InactiveColor

            If PanelSwitch.Left < 0 Then
                PanelSwitch.Left = 0
            ElseIf PanelSwitch.Left > (Me.PanelMain.Width - PanelSwitch.Width) Then
                PanelSwitch.Left = (Me.PanelMain.Width - PanelSwitch.Width)
            Else
                PanelSwitch.Location = New Point((((Me.Value * 100) / Me.MaxValue) * (Me.PanelMain.Width - PanelSwitch.Width)) / 100, 0)
            End If

            Me.PanelState1.Width = Me.PanelState1.Margin.Left + Me.PanelSwitch.Left

        End Sub

        Private _x As Integer
        Private _y As Integer
        Private _move As Boolean

        Private Sub PanelSwitch_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelSwitch.MouseDown
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                _move = True
                _x = e.X
                _y = e.Y
            End If
        End Sub

        Private Sub PanelSwitch_MouseEnter(sender As Object, e As System.EventArgs) Handles PanelSwitch.MouseEnter, PanelState1.MouseEnter, PanelState2.MouseEnter
            CType(sender, Control).Tag = CType(sender, Control).BackColor
            CType(sender, Control).BackColor = CType(sender, Control).BackColor.GetLightColor(16)
        End Sub
        Private Sub PanelSwitch_MouseLeave(sender As Object, e As System.EventArgs) Handles PanelSwitch.MouseLeave, PanelState1.MouseLeave, PanelState2.MouseLeave
            If CType(sender, Control).Tag IsNot Nothing Then
                CType(sender, Control).BackColor = CType(sender, Control).Tag
                CType(sender, Control).Tag = Nothing
            End If
        End Sub

        Private Sub PanelSwitch_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelSwitch.MouseMove
            If _move Then

                PanelSwitch.Location = New Point((PanelSwitch.Left + e.X - _x), 0)
                Me.Invalidate()

                Dim Val As Integer = _
                    ((Me.PanelSwitch.Left * 100) / (Me.PanelMain.Width - Me.PanelSwitch.Width) _
                    * Me.MaxValue) / 100

                Me.Value = Val

            End If

        End Sub
        Private Sub PanelSwitch_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelSwitch.MouseUp
            _move = False
        End Sub

        Private Sub PanelSwitch_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles PanelSwitch.PreviewKeyDown
            If e.KeyCode = Keys.Left Then
                Me.Value -= 1
            ElseIf e.KeyCode = Keys.Right Then
                Me.Value += 1
            End If
        End Sub

        Private Sub _GotFocus(sender As Object, e As EventArgs) Handles PanelMain.Click, PanelState1.Click, PanelState2.Click, PanelSwitch.Click, PanelSwitch.GotFocus
            Me.InvokeGotFocus(Me, New EventArgs)
        End Sub
        Private Sub _LostFocus(sender As Object, e As EventArgs) Handles PanelSwitch.LostFocus
            Me.InvokeLostFocus(Me, New EventArgs)
        End Sub
        Private Sub ScrollRangeBar_GotFocus(sender As Object, e As System.EventArgs) Handles Me.GotFocus
            Me.PanelSwitch.Focus()
        End Sub

        Private Sub PanelState_MouseClick(sender As Object, e As MouseEventArgs) Handles PanelState1.MouseClick, PanelState2.MouseClick

            Dim Val As Integer = _
                    (((e.Location.X) * 100) / (Me.PanelMain.Width - Me.PanelSwitch.Width) _
                    * Me.MaxValue) / 100

            Me.Value = Val

        End Sub

    End Class

End Namespace
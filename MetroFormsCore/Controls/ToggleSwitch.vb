Imports System.ComponentModel
Imports Sisecom.Windows.Forms.Metro.Extensions

Namespace Sisecom.Windows.Forms.Metro

    <DefaultEvent("ValueChanged")> _
    <DefaultProperty("Value")> _
    Public Class ToggleSwitch

        Private _Value As Boolean = False
        <DefaultValue(False)> _
        Public Property Value As Boolean
            Get
                Return _Value
            End Get
            Set(value As Boolean)
                If _Value <> value Then
                    _Value = value
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

            If Me.Value Then
                PanelSwitch.Location = New Point(Me.PanelMain.Width - PanelSwitch.Width, 0)
                Me.PanelState.BackColor = Me.ActiveColor
            Else
                PanelSwitch.Location = New Point(0, 0)
                Me.PanelState.BackColor = Me.InactiveColor
            End If


        End Sub

        Private Sub PanelSwitch_DoubleClick(sender As Object, e As System.EventArgs) Handles PanelSwitch.DoubleClick, PanelState.Click
            CType(sender, Panel).Tag = Nothing
            Me.Value = Not Me.Value
        End Sub

        Private _x As Integer
        Private _y As Integer
        Private _move As Boolean

        Private Sub PanelSwitch_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelSwitch.MouseDown
            If e.Button = Global.System.Windows.Forms.MouseButtons.Left Then
                _move = True
                _x = e.X
                _y = e.Y
            End If
        End Sub

        Private Sub PanelSwitch_MouseEnter(sender As Object, e As System.EventArgs) Handles PanelSwitch.MouseEnter, PanelState.MouseEnter
            CType(sender, Control).Tag = CType(sender, Control).BackColor
            CType(sender, Control).BackColor = CType(sender, Control).BackColor.GetLightColor(16)
        End Sub
        Private Sub PanelSwitch_MouseLeave(sender As Object, e As System.EventArgs) Handles PanelSwitch.MouseLeave, PanelState.MouseLeave
            If CType(sender, Control).Tag IsNot Nothing Then
                CType(sender, Control).BackColor = CType(sender, Control).Tag
                CType(sender, Control).Tag = Nothing
            End If
        End Sub

        Private Sub PanelSwitch_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelSwitch.MouseMove
            If _move Then
                PanelSwitch.Location = New Point((PanelSwitch.Left + e.X - _x), 0)

                If PanelSwitch.Left >= (Me.PanelState.Width / 2) Then
                    Me.PanelState.BackColor = Me.ActiveColor
                Else
                    Me.PanelState.BackColor = Me.InactiveColor
                End If
            End If

        End Sub
        Private Sub PanelSwitch_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelSwitch.MouseUp
            _move = False
            Me.Value = PanelSwitch.Left >= (Me.PanelState.Width / 2)
        End Sub

        Private Sub _GotFocus(sender As Object, e As EventArgs) Handles PanelMain.Click, PanelState.Click, PanelSwitch.Click, PanelSwitch.GotFocus
            Me.InvokeGotFocus(Me, New EventArgs)
        End Sub
        Private Sub _LostFocus(sender As Object, e As EventArgs) Handles PanelSwitch.LostFocus
            Me.InvokeLostFocus(Me, New EventArgs)
        End Sub
        Private Sub Switch_GotFocus(sender As Object, e As System.EventArgs) Handles Me.GotFocus
            Me.PanelSwitch.Focus()
        End Sub

        Private Sub PanelSwitch_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles PanelSwitch.PreviewKeyDown
            If e.KeyCode = Keys.Left Then
                Me.Value = False
            ElseIf e.KeyCode = Keys.Right Then
                Me.Value = True
            ElseIf e.KeyCode = Keys.Space Then
                Me.Value = Not Me.Value
            End If
        End Sub

    End Class

End Namespace
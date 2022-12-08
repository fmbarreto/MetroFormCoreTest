Imports Sisecom.Windows.Forms.Metro.Extensions

Namespace Sisecom.Windows.Forms.Metro

    Public Class StatusStrip

        Public Sub OnParentInvalidate(sender As Object, e As EventArgs)
            Me.Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)

            If Me.FindForm IsNot Nothing Then
                RemoveHandler Me.FindForm.Invalidated, AddressOf OnParentInvalidate
                AddHandler Me.FindForm.Invalidated, AddressOf OnParentInvalidate

                If TypeOf Me.FindForm Is Sisecom.Windows.Forms.Metro.Form Then

                    With CType(Me.FindForm, Sisecom.Windows.Forms.Metro.Form)

                        Me.Visible = .ShowStatusBar
                        Me.SizingGrip = .SizingGrip And .WindowState = FormWindowState.Normal

                        Me.BackColor = .BorderColor
                        For Each item As ToolStripItem In Me.Items
                            If Math.Abs((Me.BackColor.GetBrightness - Me.ForeColor.GetBrightness)) <= 0.5 Then item.ForeColor = Me.ForeColor.GetNegativeColor
                        Next

                        Me.SendToBack()

                    End With

                End If
            End If

            MyBase.OnPaint(e)

        End Sub

#Region " StatusStrip Mouse "

        Private Const WM_NCLBUTTONDOWN As Integer = &HA1S
        Private Const HTBOTTOMRIGHT As Integer = 17

        Private Sub StatusStripMetro_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            If Me.FindForm IsNot Nothing Then
                If Me.FindForm.WindowState <> FormWindowState.Maximized Then
                    If e.Button = System.Windows.Forms.MouseButtons.Left Then
                        Me.FindForm.Capture = False

                        Dim theCursor As Cursor = Cursors.Arrow
                        Dim Direction As New IntPtr(Bottom)

                        If e.X = Me.Width - 1 Or e.X = Me.Width - 2 Or e.X = Me.Width - 3 Or e.X = Me.Width - 4 Or e.X = Me.Width - 5 _
                            Or e.Y = Me.Height - 1 Or e.Y = Me.Height - 2 Or e.Y = Me.Height - 3 Or e.Y = Me.Height - 4 Or e.Y = Me.Height - 5 Then
                            Select Case e.X
                                Case Me.Width - 5 To Me.Width - 1
                                    Select Case e.Y
                                        Case Me.Height - 5 To Me.Height - 1
                                            Direction = CType(HTBOTTOMRIGHT, IntPtr)
                                            theCursor = Cursors.SizeNWSE
                                    End Select
                            End Select
                            Me.FindForm.Cursor = theCursor
                            Dim msg As Message =
                                    Message.Create(Me.FindForm.Handle, WM_NCLBUTTONDOWN,
                                        Direction, IntPtr.Zero)
                            Me.DefWndProc(msg)

                        End If
                    End If
                End If
            End If
        End Sub
        Private Sub StatusStripMetro_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            If Me.FindForm IsNot Nothing Then
                If Me.FindForm.WindowState <> FormWindowState.Maximized Then
                    If e.X = Me.Width - 1 Or e.X = Me.Width - 2 Or e.X = Me.Width - 3 Or e.X = Me.Width - 4 Or e.X = Me.Width - 5 _
                    Or e.Y = Me.Height - 1 Or e.Y = Me.Height - 2 Or e.Y = Me.Height - 3 Or e.Y = Me.Height - 4 Or e.Y = Me.Height - 5 Then
                        Dim theCursor As Cursor = Cursors.Arrow
                        Select Case e.X
                            Case Me.Width - 5 To Me.Width - 1
                                Select Case e.Y
                                    Case Me.Height - 5 To Me.Height - 1
                                        theCursor = Cursors.SizeNWSE
                                        Me.FindForm.Invalidate()
                                End Select
                        End Select
                        Me.FindForm.Cursor = theCursor
                    End If
                End If
            End If
        End Sub
        Private Sub StatusStripMetro_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
            If Me.SizeGripBounds.Contains(e.Location) And Me.FindForm IsNot Nothing Then
                Me.FindForm.Cursor = Cursors.SizeNWSE
            End If
        End Sub


#End Region



    End Class

End Namespace
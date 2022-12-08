Namespace Sisecom.Windows.Forms.Metro

    Public Class ToolStripRenderer
        Inherits System.Windows.Forms.ToolStripRenderer

        Private _form As Sisecom.Windows.Forms.Metro.Form

        Sub New(form As Sisecom.Windows.Forms.Metro.Form)
            MyBase.New()
            Me._form = form
        End Sub

        'Protected Overrides Sub OnRenderToolStripBackground(e As ToolStripRenderEventArgs)
        '    If _form IsNot Nothing Then
        '        Dim b = New SolidBrush(_form.BackColor)
        '        e.Graphics.FillRectangle(b, e.ConnectedArea)
        '    Else
        '        MyBase.OnRenderToolStripBackground(e)
        '    End If

        'End Sub

        Protected Overrides Sub OnRenderItemText(ByVal e As ToolStripItemTextRenderEventArgs)
            If _form IsNot Nothing Then
                e.TextColor = _form.ForeColor
            End If
            MyBase.OnRenderItemText(e)
        End Sub

        Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
            If _form IsNot Nothing Then
                If e.Item.Selected Then
                    Dim b = New SolidBrush(_form.BackColor)
                    Try
                        e.Graphics.FillRectangle(b, e.Item.ContentRectangle)
                    Finally
                        b.Dispose()
                    End Try
                Else
                    'Dim b = New SolidBrush(_form.BorderColor)
                    'Try
                    '    e.Graphics.FillRectangle(b, e.Item.ContentRectangle)
                    'Finally
                    '    b.Dispose()
                    'End Try
                End If
            Else
                MyBase.OnRenderMenuItemBackground(e)
            End If
        End Sub

        'Protected Overrides Sub OnRenderButtonBackground(e As ToolStripItemRenderEventArgs)
        '    Dim r As Rectangle = Rectangle.Inflate(e.Item.ContentRectangle, -2, -2)

        '    If e.Item.Selected Then
        '        Dim b = New SolidBrush(ProfessionalColors.SeparatorLight)
        '        Try
        '            e.Graphics.FillRectangle(b, r)
        '        Finally
        '            b.Dispose()
        '        End Try
        '    Else
        '        Dim p As New Pen(ProfessionalColors.SeparatorLight)
        '        Try
        '            e.Graphics.DrawRectangle(p, r)
        '        Finally
        '            p.Dispose()
        '        End Try
        '    End If
        'End Sub


    End Class

End Namespace
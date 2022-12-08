Imports System.ComponentModel

Namespace Sisecom.Windows.Forms.Extras

    Public Class Divider
        Inherits Label

        Protected Overrides Sub OnFontChanged(e As EventArgs)
            MyBase.OnFontChanged(e)
            Me.ResetHeight()
        End Sub
        Protected Overrides Sub OnTextChanged(e As EventArgs)
            MyBase.OnTextChanged(e)
            Me.ResetHeight()
        End Sub
        Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            MyBase.OnSizeChanged(e)
            Me.ResetHeight()
        End Sub

        Private Sub ResetHeight()
            Dim preferredSize As Size = Me.GetPreferredSize(MyBase.Size)
            Dim sf As SizeF = Me.CreateGraphics.MeasureString(Me.Text, Me.Font)
            MyBase.Size = New Size(MyBase.Width, IIf(sf.Height > 0, sf.Height, preferredSize.Height))
        End Sub

        Public Sub New()
            If Me.FindForm IsNot Nothing Then
                Me.Font = Me.FindForm.Font ' New Font(Font.FontFamily, 9)
            Else
                Me.Font = New Font("Segoe UI", 9)
            End If
            Me.ForeColor = SystemColors.Highlight
            Me.AutoSize = False
            ResetHeight()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            e.Graphics.Clear(BackColor)

            Dim sbDividerColor As New SolidBrush(Me._LineColor)
            Dim sbForeColor As New SolidBrush(Me.ForeColor)

            e.Graphics.DrawString(Me.Text, Me.Font, sbForeColor, ClientRectangle.X, ClientRectangle.Y)
            Dim sf As SizeF = e.Graphics.MeasureString(Me.Text, Me.Font)

            If Me.Position = Positions.Center Then
                Dim rect As New Rectangle(CType(sf.Width, Integer), (CType(sf.Height, Integer) / 2) + 1, Me.Width - CType(sf.Width, Integer), 1)
                e.Graphics.FillRectangle(sbDividerColor, rect)
            ElseIf Me.Position = Positions.Below Then
                Dim rect As New Rectangle(1, CType(sf.Height, Integer), Me.Width, 1)
                e.Graphics.FillRectangle(sbDividerColor, rect)
            End If

            sbForeColor.Dispose()
            sbDividerColor.Dispose()
        End Sub

        Public Enum Positions As Integer
            Center
            Below
        End Enum

        Private _Position As Positions = Positions.Center
        <Description("The placement of the divider line."), Category("Appearance"), DefaultValue(Positions.Center)> _
        Public Property Position() As Positions
            Get
                Return Me._Position
            End Get
            Set(value As Positions)
                Me._Position = value
                Me.Invalidate()
            End Set
        End Property

        Private _LineColor As Color = SystemColors.Highlight
        <Description("The color of the divider line."), Category("Appearance")> _
        Public Property LineColor() As Color
            Get
                Return Me._LineColor
            End Get
            Set(value As Color)
                Me._LineColor = value
                Me.Invalidate()
            End Set
        End Property

        Private _text As String = ""
        <Description("The text that will display as the caption."), Category("Appearance"), DefaultValue("DividerLabel")> _
        Public Overrides Property Text() As String
            Get
                Return Me._text
            End Get
            Set(value As String)
                Me._text = value
                Me.Invalidate()
            End Set
        End Property

    End Class

End Namespace
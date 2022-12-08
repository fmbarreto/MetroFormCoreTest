Namespace Sisecom.Windows.Forms.Metro

    <ToolboxBitmap(GetType(System.Windows.Forms.Button))>
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class ButtonMenu
        Inherits System.Windows.Forms.UserControl

        'O UserControl substitui o descarte para limpar a lista de componentes.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Exigido pelo Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
        'Pode ser modificado usando o Windows Form Designer.  
        'Não o modifique usando o editor de códigos.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlTitulo = New System.Windows.Forms.Panel()
            Me.lblTitulo = New System.Windows.Forms.Label()
            Me.pnlBarra = New System.Windows.Forms.Panel()
            Me.pnlTitulo.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTitulo
            '
            Me.pnlTitulo.Controls.Add(Me.lblTitulo)
            Me.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlTitulo.Location = New System.Drawing.Point(0, 0)
            Me.pnlTitulo.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
            Me.pnlTitulo.Name = "pnlTitulo"
            Me.pnlTitulo.Size = New System.Drawing.Size(61, 26)
            Me.pnlTitulo.TabIndex = 0
            '
            'lblTitulo
            '
            Me.lblTitulo.AutoSize = True
            Me.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblTitulo.Location = New System.Drawing.Point(0, 0)
            Me.lblTitulo.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblTitulo.Name = "lblTitulo"
            Me.lblTitulo.Size = New System.Drawing.Size(61, 26)
            Me.lblTitulo.TabIndex = 0
            Me.lblTitulo.Text = "Título"
            Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlBarra
            '
            Me.pnlBarra.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlBarra.Location = New System.Drawing.Point(0, 26)
            Me.pnlBarra.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
            Me.pnlBarra.Name = "pnlBarra"
            Me.pnlBarra.Size = New System.Drawing.Size(61, 5)
            Me.pnlBarra.TabIndex = 1
            Me.pnlBarra.Visible = False
            '
            'ButtonMenu
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 26.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.Transparent
            Me.Controls.Add(Me.pnlTitulo)
            Me.Controls.Add(Me.pnlBarra)
            Me.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
            Me.Name = "ButtonMenu"
            Me.Size = New System.Drawing.Size(61, 31)
            Me.pnlTitulo.ResumeLayout(False)
            Me.pnlTitulo.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents lblTitulo As Label
        Private WithEvents pnlTitulo As Panel
        Private WithEvents pnlBarra As Panel
    End Class

End Namespace
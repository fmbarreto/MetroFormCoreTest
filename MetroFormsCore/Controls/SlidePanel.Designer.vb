Namespace Sisecom.Windows.Forms.Metro

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class SlidePanel
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SlidePanel))
            Me.pnlLeft = New System.Windows.Forms.Panel()
            Me.btnLeft = New System.Windows.Forms.Button()
            Me.imgLista = New System.Windows.Forms.ImageList(Me.components)
            Me.pnlPanels = New System.Windows.Forms.Panel()
            Me.pnl1 = New System.Windows.Forms.Panel()
            Me.pnl2 = New System.Windows.Forms.Panel()
            Me.pnlRight = New System.Windows.Forms.Panel()
            Me.btnRight = New System.Windows.Forms.Button()
            Me.lblTitulo = New System.Windows.Forms.Label()
            Me.pnlLeft.SuspendLayout()
            Me.pnlPanels.SuspendLayout()
            Me.pnlRight.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlLeft
            '
            Me.pnlLeft.Controls.Add(Me.btnLeft)
            Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
            Me.pnlLeft.Name = "pnlLeft"
            Me.pnlLeft.Size = New System.Drawing.Size(12, 339)
            Me.pnlLeft.TabIndex = 0
            '
            'btnLeft
            '
            Me.btnLeft.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnLeft.FlatAppearance.BorderSize = 0
            Me.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnLeft.ImageIndex = 0
            Me.btnLeft.ImageList = Me.imgLista
            Me.btnLeft.Location = New System.Drawing.Point(0, 0)
            Me.btnLeft.Name = "btnLeft"
            Me.btnLeft.Size = New System.Drawing.Size(12, 339)
            Me.btnLeft.TabIndex = 0
            Me.btnLeft.TabStop = False
            Me.btnLeft.UseVisualStyleBackColor = True
            '
            'imgLista
            '
            Me.imgLista.ImageStream = CType(resources.GetObject("imgLista.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imgLista.TransparentColor = System.Drawing.Color.Transparent
            Me.imgLista.Images.SetKeyName(0, "Left")
            Me.imgLista.Images.SetKeyName(1, "Right")
            '
            'pnlPanels
            '
            Me.pnlPanels.Controls.Add(Me.pnl1)
            Me.pnlPanels.Controls.Add(Me.pnl2)
            Me.pnlPanels.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlPanels.Location = New System.Drawing.Point(12, 0)
            Me.pnlPanels.Name = "pnlPanels"
            Me.pnlPanels.Size = New System.Drawing.Size(400, 339)
            Me.pnlPanels.TabIndex = 1
            '
            'pnl1
            '
            Me.pnl1.Location = New System.Drawing.Point(0, 0)
            Me.pnl1.Name = "pnl1"
            Me.pnl1.Size = New System.Drawing.Size(400, 339)
            Me.pnl1.TabIndex = 0
            '
            'pnl2
            '
            Me.pnl2.Location = New System.Drawing.Point(0, 0)
            Me.pnl2.Name = "pnl2"
            Me.pnl2.Size = New System.Drawing.Size(400, 339)
            Me.pnl2.TabIndex = 1
            '
            'pnlRight
            '
            Me.pnlRight.Controls.Add(Me.btnRight)
            Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlRight.Location = New System.Drawing.Point(412, 0)
            Me.pnlRight.Name = "pnlRight"
            Me.pnlRight.Size = New System.Drawing.Size(12, 339)
            Me.pnlRight.TabIndex = 2
            '
            'btnRight
            '
            Me.btnRight.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnRight.FlatAppearance.BorderSize = 0
            Me.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRight.ImageIndex = 1
            Me.btnRight.ImageList = Me.imgLista
            Me.btnRight.Location = New System.Drawing.Point(0, 0)
            Me.btnRight.Name = "btnRight"
            Me.btnRight.Size = New System.Drawing.Size(12, 339)
            Me.btnRight.TabIndex = 1
            Me.btnRight.TabStop = False
            Me.btnRight.UseVisualStyleBackColor = True
            '
            'lblTitulo
            '
            Me.lblTitulo.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTitulo.Location = New System.Drawing.Point(32, 0)
            Me.lblTitulo.Name = "lblTitulo"
            Me.lblTitulo.Size = New System.Drawing.Size(382, 31)
            Me.lblTitulo.TabIndex = 0
            Me.lblTitulo.Text = "Título"
            '
            'SlidePanel
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.Transparent
            Me.Controls.Add(Me.pnlPanels)
            Me.Controls.Add(Me.lblTitulo)
            Me.Controls.Add(Me.pnlRight)
            Me.Controls.Add(Me.pnlLeft)
            Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ForeColor = System.Drawing.Color.White
            Me.Name = "SlidePanel"
            Me.Size = New System.Drawing.Size(424, 339)
            Me.pnlLeft.ResumeLayout(False)
            Me.pnlPanels.ResumeLayout(False)
            Me.pnlRight.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlLeft As Panel
        Friend WithEvents btnLeft As Button
        Friend WithEvents pnlPanels As Panel
        Friend WithEvents pnlRight As Panel
        Friend WithEvents btnRight As Button
        Friend WithEvents lblTitulo As Label
        Friend WithEvents pnl2 As Panel
        Friend WithEvents pnl1 As Panel
        Friend WithEvents imgLista As ImageList
    End Class

End Namespace
Namespace Sisecom.Windows.Forms.Metro

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Notice
        Inherits System.Windows.Forms.Form

        'Form reemplaza a Dispose para limpiar la lista de componentes.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        'Se puede modificar usando el Diseñador de Windows Forms.  
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.PictureBoxImage = New System.Windows.Forms.PictureBox()
            Me.LabelMessage = New System.Windows.Forms.Label()
            Me.PictureBoxIcon = New System.Windows.Forms.PictureBox()
            Me.LabelTitle = New System.Windows.Forms.Label()
            Me.TimerClose = New System.Windows.Forms.Timer(Me.components)
            Me.ButtonClose = New System.Windows.Forms.Button()
            Me.ToolTipNotice = New Sisecom.Windows.Forms.Metro.ToolTip()
            CType(Me.PictureBoxImage, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'PictureBoxImage
            '
            Me.PictureBoxImage.Dock = System.Windows.Forms.DockStyle.Left
            Me.PictureBoxImage.Location = New System.Drawing.Point(0, 0)
            Me.PictureBoxImage.Name = "PictureBoxImage"
            Me.PictureBoxImage.Size = New System.Drawing.Size(90, 90)
            Me.PictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.PictureBoxImage.TabIndex = 0
            Me.PictureBoxImage.TabStop = False
            Me.PictureBoxImage.Visible = False
            '
            'LabelMessage
            '
            Me.LabelMessage.Dock = System.Windows.Forms.DockStyle.Fill
            Me.LabelMessage.Location = New System.Drawing.Point(90, 30)
            Me.LabelMessage.Name = "LabelMessage"
            Me.LabelMessage.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
            Me.LabelMessage.Size = New System.Drawing.Size(279, 60)
            Me.LabelMessage.TabIndex = 2
            Me.LabelMessage.Text = "Message"
            '
            'PictureBoxIcon
            '
            Me.PictureBoxIcon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PictureBoxIcon.Location = New System.Drawing.Point(323, 57)
            Me.PictureBoxIcon.Margin = New System.Windows.Forms.Padding(0)
            Me.PictureBoxIcon.Name = "PictureBoxIcon"
            Me.PictureBoxIcon.Size = New System.Drawing.Size(24, 24)
            Me.PictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.PictureBoxIcon.TabIndex = 12
            Me.PictureBoxIcon.TabStop = False
            Me.PictureBoxIcon.Visible = False
            '
            'LabelTitle
            '
            Me.LabelTitle.AutoSize = True
            Me.LabelTitle.Dock = System.Windows.Forms.DockStyle.Top
            Me.LabelTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LabelTitle.Location = New System.Drawing.Point(90, 0)
            Me.LabelTitle.Name = "LabelTitle"
            Me.LabelTitle.Padding = New System.Windows.Forms.Padding(9, 9, 0, 0)
            Me.LabelTitle.Size = New System.Drawing.Size(64, 30)
            Me.LabelTitle.TabIndex = 10
            Me.LabelTitle.Text = "Notice"
            Me.LabelTitle.UseMnemonic = False
            '
            'TimerClose
            '
            Me.TimerClose.Enabled = True
            Me.TimerClose.Interval = 10000
            '
            'ButtonClose
            '
            Me.ButtonClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ButtonClose.FlatAppearance.BorderSize = 0
            Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ButtonClose.Image = Global.My.Resources.Resources.close_d
            Me.ButtonClose.Location = New System.Drawing.Point(343, 1)
            Me.ButtonClose.Margin = New System.Windows.Forms.Padding(0)
            Me.ButtonClose.Name = "ButtonClose"
            Me.ButtonClose.Size = New System.Drawing.Size(24, 24)
            Me.ButtonClose.TabIndex = 11
            Me.ButtonClose.TabStop = False
            Me.ButtonClose.UseVisualStyleBackColor = True
            '
            'ToolTipNotice
            '
            Me.ToolTipNotice.AutoPopDelay = 2000
            Me.ToolTipNotice.BackColor = System.Drawing.SystemColors.Highlight
            Me.ToolTipNotice.BorderColor = System.Drawing.Color.Gray
            Me.ToolTipNotice.ForeColor = System.Drawing.Color.White
            Me.ToolTipNotice.InitialDelay = 500
            Me.ToolTipNotice.OwnerDraw = True
            Me.ToolTipNotice.Padding = New System.Windows.Forms.Padding(3)
            Me.ToolTipNotice.ReshowDelay = 100
            '
            'Notice
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Highlight
            Me.ClientSize = New System.Drawing.Size(369, 90)
            Me.ControlBox = False
            Me.Controls.Add(Me.ButtonClose)
            Me.Controls.Add(Me.PictureBoxIcon)
            Me.Controls.Add(Me.LabelMessage)
            Me.Controls.Add(Me.LabelTitle)
            Me.Controls.Add(Me.PictureBoxImage)
            Me.DoubleBuffered = True
            Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ForeColor = System.Drawing.SystemColors.ControlLightLight
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.MaximumSize = New System.Drawing.Size(369, 90)
            Me.MinimumSize = New System.Drawing.Size(369, 90)
            Me.Name = "Notice"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
            Me.Text = "Notice"
            Me.TopMost = True
            CType(Me.PictureBoxImage, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents PictureBoxImage As System.Windows.Forms.PictureBox
        Private WithEvents PictureBoxIcon As System.Windows.Forms.PictureBox
        Private WithEvents LabelMessage As System.Windows.Forms.Label
        Private WithEvents LabelTitle As System.Windows.Forms.Label
        Private WithEvents TimerClose As System.Windows.Forms.Timer
        Friend WithEvents ToolTipNotice As ToolTip
        Private WithEvents ButtonClose As System.Windows.Forms.Button
    End Class

End Namespace
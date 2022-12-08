Namespace Sisecom.Windows.Forms.Metro

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ToggleTrackBar
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
            Me.PanelMain = New System.Windows.Forms.Panel()
            Me.PanelSwitch = New System.Windows.Forms.Button()
            Me.PanelState1 = New System.Windows.Forms.Panel()
            Me.PanelState2 = New System.Windows.Forms.Panel()
            Me.PanelMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'PanelMain
            '
            Me.PanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.PanelMain.Controls.Add(Me.PanelSwitch)
            Me.PanelMain.Controls.Add(Me.PanelState1)
            Me.PanelMain.Controls.Add(Me.PanelState2)
            Me.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PanelMain.Location = New System.Drawing.Point(0, 0)
            Me.PanelMain.Margin = New System.Windows.Forms.Padding(0)
            Me.PanelMain.Name = "PanelMain"
            Me.PanelMain.Size = New System.Drawing.Size(192, 24)
            Me.PanelMain.TabIndex = 3
            Me.PanelMain.TabStop = True
            '
            'PanelSwitch
            '
            Me.PanelSwitch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.PanelSwitch.BackColor = System.Drawing.Color.Black
            Me.PanelSwitch.FlatAppearance.BorderSize = 0
            Me.PanelSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PanelSwitch.Location = New System.Drawing.Point(80, 0)
            Me.PanelSwitch.Margin = New System.Windows.Forms.Padding(0)
            Me.PanelSwitch.Name = "PanelSwitch"
            Me.PanelSwitch.Size = New System.Drawing.Size(16, 22)
            Me.PanelSwitch.TabIndex = 1
            Me.PanelSwitch.UseVisualStyleBackColor = False
            '
            'PanelState1
            '
            Me.PanelState1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.PanelState1.BackColor = System.Drawing.SystemColors.Highlight
            Me.PanelState1.Location = New System.Drawing.Point(2, 2)
            Me.PanelState1.Margin = New System.Windows.Forms.Padding(2, 2, 0, 2)
            Me.PanelState1.Name = "PanelState1"
            Me.PanelState1.Size = New System.Drawing.Size(78, 18)
            Me.PanelState1.TabIndex = 2
            Me.PanelState1.TabStop = True
            '
            'PanelState2
            '
            Me.PanelState2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PanelState2.BackColor = System.Drawing.SystemColors.ScrollBar
            Me.PanelState2.Location = New System.Drawing.Point(2, 2)
            Me.PanelState2.Margin = New System.Windows.Forms.Padding(0, 2, 2, 2)
            Me.PanelState2.Name = "PanelState2"
            Me.PanelState2.Size = New System.Drawing.Size(185, 18)
            Me.PanelState2.TabIndex = 0
            Me.PanelState2.TabStop = True
            '
            'ToggleTrackBar
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.Transparent
            Me.Controls.Add(Me.PanelMain)
            Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MinimumSize = New System.Drawing.Size(192, 24)
            Me.Name = "ToggleTrackBar"
            Me.Size = New System.Drawing.Size(192, 24)
            Me.PanelMain.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents PanelMain As System.Windows.Forms.Panel
        Friend WithEvents PanelSwitch As System.Windows.Forms.Button
        Friend WithEvents PanelState2 As System.Windows.Forms.Panel
        Friend WithEvents PanelState1 As System.Windows.Forms.Panel

    End Class

End Namespace
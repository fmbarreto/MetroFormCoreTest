Namespace Sisecom.Windows.Forms.Metro

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class TextBox
        Inherits System.Windows.Forms.Control

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
            Me.components = New System.ComponentModel.Container()
            Me.TextBoxText = New System.Windows.Forms.TextBox()
            Me.PanelTextBox = New System.Windows.Forms.Panel()
            Me.ButtonAction = New System.Windows.Forms.Button()
            Me.PanelMain = New System.Windows.Forms.Panel()
            Me.CueProvider = New Sisecom.Windows.Forms.Extras.CueProvider(Me.components)
            Me.PanelTextBox.SuspendLayout()
            Me.PanelMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'TextBoxText
            '
            Me.TextBoxText.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TextBoxText.BackColor = System.Drawing.Color.White
            Me.TextBoxText.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.CueProvider.SetCueText(Me.TextBoxText, "TextBox")
            Me.CueProvider.SetHasCue(Me.TextBoxText, True)
            Me.TextBoxText.Location = New System.Drawing.Point(4, 0)
            Me.TextBoxText.Name = "TextBoxText"
            Me.TextBoxText.Size = New System.Drawing.Size(112, 22)
            Me.TextBoxText.TabIndex = 0
            '
            'PanelTextBox
            '
            Me.PanelTextBox.AutoSize = True
            Me.PanelTextBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.PanelTextBox.BackColor = System.Drawing.Color.White
            Me.PanelTextBox.Controls.Add(Me.TextBoxText)
            Me.PanelTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PanelTextBox.Location = New System.Drawing.Point(0, 0)
            Me.PanelTextBox.Name = "PanelTextBox"
            Me.PanelTextBox.Size = New System.Drawing.Size(120, 28)
            Me.PanelTextBox.TabIndex = 1
            '
            'ButtonAction
            '
            Me.ButtonAction.Dock = System.Windows.Forms.DockStyle.Right
            Me.ButtonAction.FlatAppearance.BorderSize = 0
            Me.ButtonAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ButtonAction.Image = Global.My.Resources.Resources.password_d
            Me.ButtonAction.Location = New System.Drawing.Point(120, 0)
            Me.ButtonAction.Margin = New System.Windows.Forms.Padding(0)
            Me.ButtonAction.Name = "ButtonAction"
            Me.ButtonAction.Size = New System.Drawing.Size(28, 28)
            Me.ButtonAction.TabIndex = 0
            Me.ButtonAction.UseVisualStyleBackColor = True
            Me.ButtonAction.Visible = False
            '
            'PanelMain
            '
            Me.PanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.PanelMain.Controls.Add(Me.PanelTextBox)
            Me.PanelMain.Controls.Add(Me.ButtonAction)
            Me.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PanelMain.Location = New System.Drawing.Point(0, 0)
            Me.PanelMain.Name = "PanelMain"
            Me.PanelMain.Size = New System.Drawing.Size(150, 30)
            Me.PanelMain.TabIndex = 2
            '
            'TextBox
            '
            Me.BackColor = System.Drawing.Color.White
            Me.Controls.Add(Me.PanelMain)
            Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Size = New System.Drawing.Size(150, 30)
            Me.PanelTextBox.ResumeLayout(False)
            Me.PanelTextBox.PerformLayout()
            Me.PanelMain.ResumeLayout(False)
            Me.PanelMain.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents ButtonAction As System.Windows.Forms.Button
        Private WithEvents TextBoxText As System.Windows.Forms.TextBox
        Private WithEvents PanelTextBox As System.Windows.Forms.Panel
        Private WithEvents CueProvider As Sisecom.Windows.Forms.Extras.CueProvider
        Friend WithEvents PanelMain As System.Windows.Forms.Panel

    End Class

End Namespace
Namespace Sisecom.Windows.Forms.Metro


    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class RibbonTab
        Inherits System.Windows.Forms.FlowLayoutPanel

        'Control reemplaza a Dispose para limpiar la lista de componentes.
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

        'Requerido por el Diseñador de controles
        Private components As System.ComponentModel.IContainer

        ' NOTA: el Diseñador de componentes requiere el siguiente procedimiento
        ' Se puede modificar usando el Diseñador de componentes. No lo modifique
        ' usando el editor de código.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.ButtonFile = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'ButtonFile
            '
            Me.ButtonFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))
            Me.ButtonFile.FlatAppearance.BorderSize = 0
            Me.ButtonFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ButtonFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ButtonFile.ForeColor = System.Drawing.Color.White
            Me.ButtonFile.Location = New System.Drawing.Point(0, 0)
            Me.ButtonFile.Margin = New System.Windows.Forms.Padding(0)
            Me.ButtonFile.MinimumSize = New System.Drawing.Size(75, 25)
            Me.ButtonFile.Name = "ButtonFile"
            Me.ButtonFile.Size = New System.Drawing.Size(75, 25)
            Me.ButtonFile.TabIndex = 16
            Me.ButtonFile.TabStop = False
            Me.ButtonFile.Text = "FILE"
            Me.ButtonFile.UseVisualStyleBackColor = False
            '
            'RibbonTab
            '
            Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ButtonFile As System.Windows.Forms.Button

    End Class


End Namespace
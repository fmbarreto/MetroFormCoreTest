Namespace Sisecom.Windows.Forms.Metro

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SearchControl
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SearchControl))
            Me.TextBoxSearch = New System.Windows.Forms.TextBox()
            Me.ButtonSearch = New System.Windows.Forms.Button()
            Me.CueProviderSearch = New Sisecom.Windows.Forms.Extras.CueProvider(Me.components)
            Me.SuspendLayout()
            '
            'TextBoxSearch
            '
            Me.TextBoxSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TextBoxSearch.BackColor = System.Drawing.Color.White
            Me.TextBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.CueProviderSearch.SetCueText(Me.TextBoxSearch, "Search...")
            Me.CueProviderSearch.SetHasCue(Me.TextBoxSearch, True)
            Me.TextBoxSearch.Location = New System.Drawing.Point(6, 5)
            Me.TextBoxSearch.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.TextBoxSearch.Name = "TextBoxSearch"
            Me.TextBoxSearch.Size = New System.Drawing.Size(219, 18)
            Me.TextBoxSearch.TabIndex = 0
            '
            'ButtonSearch
            '
            Me.ButtonSearch.Dock = System.Windows.Forms.DockStyle.Right
            Me.ButtonSearch.FlatAppearance.BorderSize = 0
            Me.ButtonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ButtonSearch.Image = CType(resources.GetObject("ButtonSearch.Image"), System.Drawing.Image)
            Me.ButtonSearch.Location = New System.Drawing.Point(228, 0)
            Me.ButtonSearch.Margin = New System.Windows.Forms.Padding(0)
            Me.ButtonSearch.Name = "ButtonSearch"
            Me.ButtonSearch.Size = New System.Drawing.Size(28, 28)
            Me.ButtonSearch.TabIndex = 1
            Me.ButtonSearch.UseVisualStyleBackColor = True
            '
            'SearchControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.TextBoxSearch)
            Me.Controls.Add(Me.ButtonSearch)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Name = "SearchControl"
            Me.Size = New System.Drawing.Size(256, 28)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents CueProviderSearch As Extras.CueProvider
        Private WithEvents TextBoxSearch As System.Windows.Forms.TextBox
        Private WithEvents ButtonSearch As System.Windows.Forms.Button

    End Class

End Namespace

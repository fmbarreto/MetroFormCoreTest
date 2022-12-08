Namespace Sisecom.Windows.Forms.Metro

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class Form
        Inherits System.Windows.Forms.Form

        'Form reemplaza a Dispose para limpiar la lista de componentes.
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

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        'Se puede modificar usando el Diseñador de Windows Forms.  
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form))
            Me.cmsMetroForm = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tsmiRestoreMetroForm = New System.Windows.Forms.ToolStripMenuItem()
            Me.tsmiMoveMetroForm = New System.Windows.Forms.ToolStripMenuItem()
            Me.tsmiSizeMetroForm = New System.Windows.Forms.ToolStripMenuItem()
            Me.tsmiMinimizeMetroForm = New System.Windows.Forms.ToolStripMenuItem()
            Me.tsmiMaximizeMetroForm = New System.Windows.Forms.ToolStripMenuItem()
            Me.tssOptionsMetroForm = New System.Windows.Forms.ToolStripSeparator()
            Me.tsmiCloseMetroForm = New System.Windows.Forms.ToolStripMenuItem()
            Me.lblTitleMetroForm = New System.Windows.Forms.Label()
            Me.pbIconMetroForm = New System.Windows.Forms.PictureBox()
            Me.btnMinimizeMetroForm = New System.Windows.Forms.Button()
            Me.btnMaximizeRestoreMetroForm = New System.Windows.Forms.Button()
            Me.btnCloseMetroForm = New System.Windows.Forms.Button()
            Me.pnlTitleMetroForm = New System.Windows.Forms.Panel()
            Me.pnlTopTitleMetroForm = New System.Windows.Forms.Panel()
            Me.pnlTitleMetroFormButtons = New System.Windows.Forms.Panel()
            Me.pnlTitleMetroFormLogo = New System.Windows.Forms.Panel()
            Me.pnlTitleBarMetroForm = New System.Windows.Forms.Panel()
            Me.ttMetroForm = New Sisecom.Windows.Forms.Metro.ToolTip()
            Me.cmsMetroForm.SuspendLayout()
            CType(Me.pbIconMetroForm, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlTitleMetroForm.SuspendLayout()
            Me.pnlTopTitleMetroForm.SuspendLayout()
            Me.pnlTitleMetroFormButtons.SuspendLayout()
            Me.pnlTitleMetroFormLogo.SuspendLayout()
            Me.SuspendLayout()
            '
            'cmsMetroForm
            '
            Me.cmsMetroForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiRestoreMetroForm, Me.tsmiMoveMetroForm, Me.tsmiSizeMetroForm, Me.tsmiMinimizeMetroForm, Me.tsmiMaximizeMetroForm, Me.tssOptionsMetroForm, Me.tsmiCloseMetroForm})
            Me.cmsMetroForm.Name = "ContextMenuStripForm"
            resources.ApplyResources(Me.cmsMetroForm, "cmsMetroForm")
            '
            'tsmiRestoreMetroForm
            '
            resources.ApplyResources(Me.tsmiRestoreMetroForm, "tsmiRestoreMetroForm")
            Me.tsmiRestoreMetroForm.Image = Global.My.Resources.Resources.restore_d
            Me.tsmiRestoreMetroForm.Name = "tsmiRestoreMetroForm"
            '
            'tsmiMoveMetroForm
            '
            resources.ApplyResources(Me.tsmiMoveMetroForm, "tsmiMoveMetroForm")
            Me.tsmiMoveMetroForm.Name = "tsmiMoveMetroForm"
            '
            'tsmiSizeMetroForm
            '
            resources.ApplyResources(Me.tsmiSizeMetroForm, "tsmiSizeMetroForm")
            Me.tsmiSizeMetroForm.Name = "tsmiSizeMetroForm"
            '
            'tsmiMinimizeMetroForm
            '
            Me.tsmiMinimizeMetroForm.Image = Global.My.Resources.Resources.minimize_d
            resources.ApplyResources(Me.tsmiMinimizeMetroForm, "tsmiMinimizeMetroForm")
            Me.tsmiMinimizeMetroForm.Name = "tsmiMinimizeMetroForm"
            '
            'tsmiMaximizeMetroForm
            '
            Me.tsmiMaximizeMetroForm.Image = Global.My.Resources.Resources.maximize_d
            resources.ApplyResources(Me.tsmiMaximizeMetroForm, "tsmiMaximizeMetroForm")
            Me.tsmiMaximizeMetroForm.Name = "tsmiMaximizeMetroForm"
            '
            'tssOptionsMetroForm
            '
            Me.tssOptionsMetroForm.Name = "tssOptionsMetroForm"
            resources.ApplyResources(Me.tssOptionsMetroForm, "tssOptionsMetroForm")
            '
            'tsmiCloseMetroForm
            '
            Me.tsmiCloseMetroForm.Image = Global.My.Resources.Resources.close_d
            resources.ApplyResources(Me.tsmiCloseMetroForm, "tsmiCloseMetroForm")
            Me.tsmiCloseMetroForm.Name = "tsmiCloseMetroForm"
            '
            'lblTitleMetroForm
            '
            Me.lblTitleMetroForm.AutoEllipsis = True
            resources.ApplyResources(Me.lblTitleMetroForm, "lblTitleMetroForm")
            Me.lblTitleMetroForm.Name = "lblTitleMetroForm"
            Me.lblTitleMetroForm.UseCompatibleTextRendering = True
            '
            'pbIconMetroForm
            '
            Me.pbIconMetroForm.ContextMenuStrip = Me.cmsMetroForm
            resources.ApplyResources(Me.pbIconMetroForm, "pbIconMetroForm")
            Me.pbIconMetroForm.Name = "pbIconMetroForm"
            Me.pbIconMetroForm.TabStop = False
            '
            'btnMinimizeMetroForm
            '
            resources.ApplyResources(Me.btnMinimizeMetroForm, "btnMinimizeMetroForm")
            Me.btnMinimizeMetroForm.FlatAppearance.BorderSize = 0
            Me.btnMinimizeMetroForm.Image = Global.My.Resources.Resources.minimize_d
            Me.btnMinimizeMetroForm.Name = "btnMinimizeMetroForm"
            Me.btnMinimizeMetroForm.TabStop = False
            Me.btnMinimizeMetroForm.UseVisualStyleBackColor = True
            '
            'btnMaximizeRestoreMetroForm
            '
            resources.ApplyResources(Me.btnMaximizeRestoreMetroForm, "btnMaximizeRestoreMetroForm")
            Me.btnMaximizeRestoreMetroForm.FlatAppearance.BorderSize = 0
            Me.btnMaximizeRestoreMetroForm.Image = Global.My.Resources.Resources.maximize_d
            Me.btnMaximizeRestoreMetroForm.Name = "btnMaximizeRestoreMetroForm"
            Me.btnMaximizeRestoreMetroForm.TabStop = False
            Me.btnMaximizeRestoreMetroForm.UseVisualStyleBackColor = True
            '
            'btnCloseMetroForm
            '
            resources.ApplyResources(Me.btnCloseMetroForm, "btnCloseMetroForm")
            Me.btnCloseMetroForm.FlatAppearance.BorderSize = 0
            Me.btnCloseMetroForm.Image = Global.My.Resources.Resources.close_d
            Me.btnCloseMetroForm.Name = "btnCloseMetroForm"
            Me.btnCloseMetroForm.TabStop = False
            Me.btnCloseMetroForm.UseVisualStyleBackColor = True
            '
            'pnlTitleMetroForm
            '
            Me.pnlTitleMetroForm.Controls.Add(Me.pnlTopTitleMetroForm)
            Me.pnlTitleMetroForm.Controls.Add(Me.pnlTitleBarMetroForm)
            resources.ApplyResources(Me.pnlTitleMetroForm, "pnlTitleMetroForm")
            Me.pnlTitleMetroForm.Name = "pnlTitleMetroForm"
            '
            'pnlTopTitleMetroForm
            '
            Me.pnlTopTitleMetroForm.Controls.Add(Me.lblTitleMetroForm)
            Me.pnlTopTitleMetroForm.Controls.Add(Me.pnlTitleMetroFormButtons)
            Me.pnlTopTitleMetroForm.Controls.Add(Me.pnlTitleMetroFormLogo)
            resources.ApplyResources(Me.pnlTopTitleMetroForm, "pnlTopTitleMetroForm")
            Me.pnlTopTitleMetroForm.Name = "pnlTopTitleMetroForm"
            '
            'pnlTitleMetroFormButtons
            '
            Me.pnlTitleMetroFormButtons.Controls.Add(Me.btnCloseMetroForm)
            Me.pnlTitleMetroFormButtons.Controls.Add(Me.btnMinimizeMetroForm)
            Me.pnlTitleMetroFormButtons.Controls.Add(Me.btnMaximizeRestoreMetroForm)
            resources.ApplyResources(Me.pnlTitleMetroFormButtons, "pnlTitleMetroFormButtons")
            Me.pnlTitleMetroFormButtons.Name = "pnlTitleMetroFormButtons"
            '
            'pnlTitleMetroFormLogo
            '
            Me.pnlTitleMetroFormLogo.Controls.Add(Me.pbIconMetroForm)
            resources.ApplyResources(Me.pnlTitleMetroFormLogo, "pnlTitleMetroFormLogo")
            Me.pnlTitleMetroFormLogo.Name = "pnlTitleMetroFormLogo"
            '
            'pnlTitleBarMetroForm
            '
            Me.pnlTitleBarMetroForm.BackColor = System.Drawing.Color.Gray
            resources.ApplyResources(Me.pnlTitleBarMetroForm, "pnlTitleBarMetroForm")
            Me.pnlTitleBarMetroForm.Name = "pnlTitleBarMetroForm"
            '
            'ttMetroForm
            '
            Me.ttMetroForm.AutoPopDelay = 2000
            Me.ttMetroForm.BackColor = System.Drawing.Color.Black
            Me.ttMetroForm.BorderColor = System.Drawing.Color.Gray
            Me.ttMetroForm.ForeColor = System.Drawing.Color.White
            Me.ttMetroForm.InitialDelay = 500
            Me.ttMetroForm.OwnerDraw = True
            Me.ttMetroForm.Padding = New System.Windows.Forms.Padding(3)
            Me.ttMetroForm.ReshowDelay = 100
            '
            'Form
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.Controls.Add(Me.pnlTitleMetroForm)
            Me.ForeColor = System.Drawing.Color.Black
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "Form"
            Me.cmsMetroForm.ResumeLayout(False)
            CType(Me.pbIconMetroForm, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlTitleMetroForm.ResumeLayout(False)
            Me.pnlTopTitleMetroForm.ResumeLayout(False)
            Me.pnlTitleMetroFormButtons.ResumeLayout(False)
            Me.pnlTitleMetroFormLogo.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Private WithEvents btnMinimizeMetroForm As System.Windows.Forms.Button
        Private WithEvents btnMaximizeRestoreMetroForm As System.Windows.Forms.Button
        Private WithEvents btnCloseMetroForm As System.Windows.Forms.Button
        Private WithEvents pbIconMetroForm As System.Windows.Forms.PictureBox
        Private WithEvents pnlTitleMetroForm As System.Windows.Forms.Panel
        Private WithEvents lblTitleMetroForm As System.Windows.Forms.Label
        Private WithEvents tsmiRestoreMetroForm As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents tsmiMinimizeMetroForm As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents tsmiMaximizeMetroForm As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents tssOptionsMetroForm As System.Windows.Forms.ToolStripSeparator
        Private WithEvents tsmiCloseMetroForm As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents tsmiMoveMetroForm As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents tsmiSizeMetroForm As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents ttMetroForm As Sisecom.Windows.Forms.Metro.ToolTip
        Private WithEvents cmsMetroForm As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents pnlTitleBarMetroForm As Panel
        Friend WithEvents pnlTopTitleMetroForm As Panel
        Friend WithEvents pnlTitleMetroFormLogo As Panel
        Friend WithEvents pnlTitleMetroFormButtons As Panel
    End Class

End Namespace
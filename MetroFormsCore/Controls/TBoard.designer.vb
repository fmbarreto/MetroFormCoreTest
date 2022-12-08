Namespace Sisecom.Windows.Forms.Metro

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class TBoard
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
            Me.TableLayoutPanelBoard = New System.Windows.Forms.TableLayoutPanel()
            Me.SuspendLayout()
            '
            'TableLayoutPanelBoard
            '
            Me.TableLayoutPanelBoard.ColumnCount = 1
            Me.TableLayoutPanelBoard.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 352.0!))
            Me.TableLayoutPanelBoard.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanelBoard.Location = New System.Drawing.Point(0, 0)
            Me.TableLayoutPanelBoard.Margin = New System.Windows.Forms.Padding(0)
            Me.TableLayoutPanelBoard.Name = "TableLayoutPanelBoard"
            Me.TableLayoutPanelBoard.RowCount = 1
            Me.TableLayoutPanelBoard.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229.0!))
            Me.TableLayoutPanelBoard.Size = New System.Drawing.Size(352, 229)
            Me.TableLayoutPanelBoard.TabIndex = 0
            '
            'TBoard
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.TableLayoutPanelBoard)
            Me.Font = New System.Drawing.Font("Segoe UI", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.Name = "TBoard"
            Me.Size = New System.Drawing.Size(352, 229)
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents TableLayoutPanelBoard As System.Windows.Forms.TableLayoutPanel

    End Class

End Namespace

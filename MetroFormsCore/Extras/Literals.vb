Imports System.ComponentModel

Namespace Components

    <System.Drawing.ToolboxBitmap(GetType(Label))> _
    Public Class Literals

        'Sub New()
        '    _Items = New List(Of String)
        'End Sub

        Private _Items As New List(Of String)
        <ListBindable(True)> _
        <Localizable(True)> _
        <Browsable(True)> _
        <Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")> _
        Public Property Items As List(Of String)
            Get
                Return _Items
            End Get
            Set(value As List(Of String))
                _Items = value
            End Set
        End Property

    End Class

End Namespace
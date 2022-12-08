Imports System.ComponentModel
Imports System.Runtime.InteropServices

Namespace Sisecom.Windows.Forms.Extras

    <ProvideProperty("HasCue", GetType(System.Windows.Forms.Control))> _
    <ProvideProperty("CueText", GetType(System.Windows.Forms.Control))> _
    <ToolboxBitmap(GetType(TextBox))> _
    Public Class CueProvider
        Inherits System.ComponentModel.Component
        Implements IExtenderProvider


        <System.Diagnostics.DebuggerNonUserCode()> _
        Public Sub New(ByVal container As System.ComponentModel.IContainer)
            MyClass.New()

            'Requerido para la compatibilidad con el Diseñador de composiciones de clases Windows.Forms
            If (container IsNot Nothing) Then
                container.Add(Me)
            End If

        End Sub

        <System.Diagnostics.DebuggerNonUserCode()> _
        Public Sub New()
            MyBase.New()

            'El Diseñador de componentes requiere esta llamada.
            InitializeComponent()

        End Sub

        'Component reemplaza a Dispose para limpiar la lista de componentes.
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

        'Requerido por el Diseñador de componentes
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de componentes requiere el siguiente procedimiento
        'Se puede modificar usando el Diseñador de componentes.
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
        End Sub

        Private m_properties As New Hashtable
        Protected m_activecontrol As System.Windows.Forms.Control

        Private Class Properties
            Public Property HasCue As Boolean
            Public Property CueText As String
        End Class

        Private Function EnsurePropertiesExists(key As Object) As Properties
            Dim p As Properties = DirectCast(m_properties(key), Properties)

            If p Is Nothing Then
                p = New Properties()
                m_properties(key) = p
            End If

            Return p
        End Function

        Public Function CanExtend(extendee As Object) As Boolean Implements System.ComponentModel.IExtenderProvider.CanExtend
            If TypeOf extendee Is TextBox Then 'Or TypeOf extendee Is ComboBox Then 'Or TypeOf extendee Is Controls.DynamicControl
                m_activecontrol = extendee
                Return True
            Else
                m_activecontrol = Nothing
                Return False
            End If
        End Function

        <DefaultValue(False)> _
        Public Function GetHasCue(b As Control) As Boolean
            Return EnsurePropertiesExists(b).HasCue
        End Function
        Public Sub SetHasCue(b As Control, value As Boolean)

            EnsurePropertiesExists(b).HasCue = value

            If value Then
                CueProviderActions.SetCue(b, EnsurePropertiesExists(b).CueText)
                '    'AddHandler b.GotFocus, AddressOf Control_GotFocus
                '    'AddHandler b.LostFocus, AddressOf Control_LostFocus
            Else
                CueProviderActions.ClearCue(b)
                '    'RemoveHandler b.GotFocus, AddressOf Control_GotFocus
                '    'RemoveHandler b.LostFocus, AddressOf Control_LostFocus
            End If

            b.Invalidate()
        End Sub

        <DefaultValue("CueText")> _
        Public Function GetCueText(b As Control) As String
            Return EnsurePropertiesExists(b).CueText
        End Function
        Public Sub SetCueText(b As Control, value As String)

            EnsurePropertiesExists(b).CueText = value
            If EnsurePropertiesExists(b).HasCue Then
                CueProviderActions.SetCue(b, value)
            Else
                CueProviderActions.ClearCue(b)
            End If

            b.Invalidate()

        End Sub

        'Private Sub Control_GotFocus(sender As Object, e As EventArgs)
        '    If sender.Text = sender.Tag Then
        '        sender.Text = Nothing
        '        sender.Font = New System.Drawing.Font(CType(sender, Control).Font.FontFamily, sender.Font.Size, System.Drawing.FontStyle.Regular)
        '        sender.ForeColor = System.Drawing.SystemColors.WindowText
        '    End If

        '    m_activecontrol = CType(sender, System.Windows.Forms.ContextMenuStrip).SourceControl
        'End Sub

        'Private Sub Control_LostFocus(sender As Object, e As EventArgs)
        '    If sender.Text.Trim = sender.Tag Or sender.Text.Trim = "" Then
        '        sender.Text = sender.Tag
        '        sender.Font = New System.Drawing.Font(CType(sender, Control).Font.FontFamily, sender.Font.Size, System.Drawing.FontStyle.Italic)
        '        sender.ForeColor = System.Drawing.SystemColors.ScrollBar
        '    End If
        'End Sub

    End Class

    <HideModuleName()> _
    Public Module CueProviderActions

        Private Const EM_SETCUEBANNER As Integer = &H1501

        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Private Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As Int32
        End Function

        ''' <summary>
        ''' Sets a text box's cue text.
        ''' </summary>
        ''' <param name="textBox">The text box.</param>
        ''' <param name="cue">The cue text.</param>
        Public Sub SetCue(textBox As TextBox, cue As String)
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, cue)
        End Sub

        ''' <summary>
        ''' Clears a text box's cue text.
        ''' </summary>
        ''' <param name="textBox">The text box</param>
        Public Sub ClearCue(textBox As TextBox)
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, String.Empty)
        End Sub

    End Module

End Namespace
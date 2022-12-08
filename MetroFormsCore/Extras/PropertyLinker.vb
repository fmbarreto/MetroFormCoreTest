Imports System.ComponentModel

Namespace Sisecom.Windows.Forms.Extras

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class PropertyLinker
        Inherits System.ComponentModel.Component

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

    End Class

    <ProvideProperty("SourceControl", GetType(System.Windows.Forms.Control))> _
    <ProvideProperty("SourceProperty", GetType(System.Windows.Forms.Control))> _
    <ProvideProperty("DestinationProperty", GetType(System.Windows.Forms.Control))> _
    Public Class PropertyLinker
        Implements IExtenderProvider

        Private m_properties As New Hashtable
        Protected m_activecontrol As System.Windows.Forms.Control

        Private Class Properties
            Public Property SourceControl As Control
            '<EditorAttribute(GetType(PropertyDescriptor), GetType(System.Drawing.Design.UITypeEditor))> _
            Public Property SourceProperty As String
            '<EditorAttribute(GetType(PropertyDescriptor), GetType(System.Drawing.Design.UITypeEditor))> _
            Public Property DestinationProperty As String
        End Class

        Private Function EnsurePropertiesExists(key As Object) As Properties
            Dim p As Properties = DirectCast(m_properties(key), Properties)

            If p Is Nothing Then
                p = New Properties()
                m_properties(key) = p
            End If

            Return p
        End Function
        Public Function CanExtend(extendee As Object) As Boolean Implements IExtenderProvider.CanExtend
            If TypeOf extendee Is Control Then
                m_activecontrol = extendee
                Return True
            Else
                m_activecontrol = Nothing
                Return False
            End If
        End Function

        Public Function GetSourceControl(b As Control) As Control
            Return EnsurePropertiesExists(b).SourceControl
        End Function
        Public Function GetSourceProperty(b As Control) As String
            Return EnsurePropertiesExists(b).SourceProperty
        End Function
        Public Function GetDestinationProperty(b As Control) As String
            Return EnsurePropertiesExists(b).DestinationProperty
        End Function


        Public Sub SetSourceControl(b As Control, value As Control)
            EnsurePropertiesExists(b).SourceControl = value
            b.Invalidate()
        End Sub
        Public Sub SetSourceProperty(b As Control, value As String)

            EnsurePropertiesExists(b).SourceProperty = value
            ExchangeProperties(b)
            b.Invalidate()

            If EnsurePropertiesExists(b).SourceControl IsNot Nothing And EnsurePropertiesExists(b).SourceProperty <> String.Empty Then
                Dim _SourceProperty As PropertyDescriptor = TypeDescriptor.GetProperties(Me.GetSourceControl(b))(Me.GetSourceProperty(b))
                _SourceProperty.RemoveValueChanged(EnsurePropertiesExists(b).SourceControl, AddressOf _PropertyChanged)
                _SourceProperty.AddValueChanged(EnsurePropertiesExists(b).SourceControl, AddressOf _PropertyChanged)
            End If

        End Sub
        Public Sub SetDestinationProperty(b As Control, value As String)

            EnsurePropertiesExists(b).DestinationProperty = value
            ExchangeProperties(b)
            b.Invalidate()

        End Sub

        Private Sub _PropertyChanged(sender As Object, e As EventArgs)

            Dim _destinationControl As Control = m_properties.OfType(Of DictionaryEntry).Where(Function(d) CType(d.Value, Properties).SourceControl Is sender).FirstOrDefault.Key
            If _destinationControl IsNot Nothing Then
                ExchangeProperties(_destinationControl)
            End If

        End Sub

        Private Sub ExchangeProperties(b As Control)

            If EnsurePropertiesExists(b).SourceControl IsNot Nothing _
                And EnsurePropertiesExists(b).SourceProperty <> String.Empty _
                And EnsurePropertiesExists(b).DestinationProperty <> String.Empty Then

                Try

                    Dim _SourceProperty As PropertyDescriptor = TypeDescriptor.GetProperties(Me.GetSourceControl(b))(Me.GetSourceProperty(b))
                    Dim _DestinationProperty As PropertyDescriptor = TypeDescriptor.GetProperties(b)(Me.GetDestinationProperty(b))

                    Dim _value As Object = _SourceProperty.GetValue(Me.GetSourceControl(b))
                    _DestinationProperty.SetValue(b, Convert.ChangeType(_SourceProperty.GetValue(Me.GetSourceControl(b)), _DestinationProperty.PropertyType))

                Catch ex As Exception

                End Try

            End If

        End Sub


    End Class

End Namespace


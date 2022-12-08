Imports System.ComponentModel

Namespace Sisecom.Windows.Forms.Metro

    Public Class Notices

        Private _Stack As New Stack(Of Notice)

        Private IsStarted As Boolean

        Public Sub Start()
            IsStarted = True
            _Closed(Me, New EventArgs)
        End Sub

        Public Event NoticeClosed As EventHandler
        Private Sub _Closed(sender As Object, e As EventArgs)
            If _Stack.Count > 0 Then
                With _Stack.Pop
                    If Me.Automatic Then
                        AddHandler .Closed, AddressOf _Closed
                    End If
                    '.Parent = Me.ContainerControl
                    .Show()
                    RaiseEvent NoticeClosed(Me, New EventArgs)
                End With
            Else
                IsStarted = False
            End If
        End Sub

        Public Event NoticeAdded As EventHandler
        Public Sub Add(notice As Notice)
            _Stack.Push(notice)
            RaiseEvent NoticeAdded(Me, New EventArgs)
            If Me.Automatic And Not IsStarted Then
                Me.Start()
            End If
        End Sub

        Private _Automatic As Boolean = True
        <DefaultValue(True)> _
        Public Property Automatic As Boolean
            Get
                Return _Automatic
            End Get
            Set(value As Boolean)
                If _Automatic <> value Then
                    _Automatic = value
                End If
            End Set
        End Property

        'Private _containerControl As ContainerControl
        'Public Property ContainerControl() As ContainerControl
        '    Get
        '        Return _containerControl
        '    End Get
        '    Set(value As ContainerControl)
        '        _containerControl = value
        '    End Set
        'End Property

        'Public Overrides Property Site As ISite
        '    Get
        '        Return MyBase.Site
        '    End Get
        '    Set(value As ISite)
        '        MyBase.Site = value
        '        If value Is Nothing Then
        '            Return
        '        End If

        '        Dim host As IDesignerHost = TryCast(value.GetService(GetType(IDesignerHost)), IDesignerHost)
        '        If host IsNot Nothing Then
        '            Dim componentHost As IComponent = host.RootComponent
        '            If TypeOf componentHost Is ContainerControl Then
        '                ContainerControl = TryCast(componentHost, ContainerControl)
        '            End If
        '        End If
        '    End Set
        'End Property

    End Class
End Namespace
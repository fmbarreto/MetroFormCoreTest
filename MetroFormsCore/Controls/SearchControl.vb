Imports System.ComponentModel

Namespace Sisecom.Windows.Forms.Metro

    Public Class SearchControl

        Public Property ProgressBarColor As System.Drawing.Color = System.Drawing.Color.LightGreen

        Private _CueText As String = "Search..."
        <DefaultValue("Search...")> _
        Public Property CueText As String
            Get
                Return _CueText
            End Get
            Set(value As String)
                If _CueText <> value Then
                    _CueText = value
                    Me.CueProviderSearch.SetCueText(Me.TextBoxSearch, _CueText)
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _AutoSearch As Boolean = False
        <DefaultValue(False)> _
        Public Property AutoSearch As Boolean
            Get
                Return _AutoSearch
            End Get
            Set(value As Boolean)
                If _AutoSearch <> value Then
                    _AutoSearch = value
                End If
            End Set
        End Property

        Private WithEvents _BackgroundWorkerSearch As System.ComponentModel.BackgroundWorker
        Public Property BackgroundWorkerSearch As System.ComponentModel.BackgroundWorker
            Get
                Return _BackgroundWorkerSearch
            End Get
            Set(value As System.ComponentModel.BackgroundWorker)
                _BackgroundWorkerSearch = value
            End Set
        End Property

        Private _PanelProgress As Panel
        'Private _Text As String
        Public Overrides Property Text As String
            Get
                'If _Text = "" Then
                '    _Text = Me.TextBoxSearch.Text
                'End If
                'Return _Text
                Return Me.TextBoxSearch.Text
            End Get
            Set(value As String)
                'If _Text <> value Then
                '    Me.TextBoxSearch.Text = _Text
                'End If
                Me.TextBoxSearch.Text = value
            End Set
        End Property

        Public Sub ReportProgress(percentProgress As Integer)
            If percentProgress <> -1 Then
                If _PanelProgress IsNot Nothing Then
                    _PanelProgress.Width = (Me.Width * percentProgress) / 100
                End If
                'Me.BackgroundWorkerSearch.ReportProgress(percentProgress)
                'ElseIf percentProgress = -2 Then
                '    If _PanelProgress IsNot Nothing Then
                '        Me.Controls.Remove(_PanelProgress)
                '        _PanelProgress.Dispose()
                '        _PanelProgress = Nothing
                '    End If
            Else
                '    _PanelProgress.Width = Me.Width / 100
                '    If _PanelProgress.Left >= Me.Width Then
                '        _PanelProgress.Left += (Me.Width / 100)
                '    End If
            End If
        End Sub

        Public Event Searching As EventHandler
        Public Event SearchStarted As EventHandler
        Public Event SearchCompleted As EventHandler

        Friend Overridable Sub OnSearchStart(sender As Object, e As EventArgs) Handles ButtonSearch.Click
            Me.SetStartedState()
            RaiseEvent SearchStarted(Me, e)

            If BackgroundWorkerSearch IsNot Nothing Then
                BackgroundWorkerSearch.RunWorkerAsync(Me.TextBoxSearch.Text)
            Else
                Me.SetCompletedState()
                RaiseEvent SearchCompleted(sender, e)
            End If

        End Sub
        Friend Overridable Sub OnSearching(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _BackgroundWorkerSearch.DoWork
            RaiseEvent Searching(Me, e)
        End Sub

        Private Sub BackgroundWorkerSearch_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles _BackgroundWorkerSearch.ProgressChanged
            _PanelProgress.Width = (Me.Width * e.ProgressPercentage) / 100
        End Sub
        Private Sub BackgroundWorkerSearch_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _BackgroundWorkerSearch.RunWorkerCompleted
            Me.SetCompletedState()
            RaiseEvent SearchCompleted(sender, e)
        End Sub

        Public Sub SetStartedState()
            _PanelProgress = New Panel
            _PanelProgress.Location = New System.Drawing.Point(0, 0)
            _PanelProgress.Size = New System.Drawing.Size(0, 3)
            _PanelProgress.BackColor = Me.ProgressBarColor
            Me.Controls.Add(_PanelProgress)
            Me.TextBoxSearch.Enabled = False
        End Sub

        Public Sub SetCompletedState()
            If _PanelProgress IsNot Nothing Then
                Me.Controls.Remove(_PanelProgress)
                _PanelProgress.Dispose()
                _PanelProgress = Nothing
            End If
            Me.TextBoxSearch.Enabled = True
        End Sub

        Private Sub TextBoxSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxSearch.KeyDown
            If Not Me.AutoSearch Then
                If e.KeyCode = Keys.Enter Then
                    Me.OnSearchStart(Me.ButtonSearch, New EventArgs)
                ElseIf e.KeyCode = Keys.Escape Then
                    Me.TextBoxSearch.ResetText()
                End If
            Else
                If Me.TextBoxSearch.Text.Trim <> String.Empty Then Me.OnSearchStart(Me.ButtonSearch, New EventArgs)
            End If
        End Sub

        Private Sub _GotFocus(sender As Object, e As EventArgs) Handles ButtonSearch.Click, TextBoxSearch.GotFocus
            Me.InvokeGotFocus(Me, New EventArgs)
        End Sub
        Private Sub _LostFocus(sender As Object, e As EventArgs) Handles ButtonSearch.LostFocus, TextBoxSearch.LostFocus
            Me.InvokeLostFocus(Me, New EventArgs)
            'If Me.ButtonSearch.Focus = False And Me.TextBoxSearch.Focus = False Then 
        End Sub
        'Private Sub SearchControl_GotFocus(sender As Object, e As System.EventArgs) Handles Me.GotFocus
        '    Me.TextBoxSearch.Focus()
        'End Sub

    End Class

End Namespace
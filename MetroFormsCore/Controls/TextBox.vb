Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Sisecom.Windows.Forms.Metro

    Public Enum TextBoxStyles As Integer
        Normal
        Cleaner
        Password
        Search
    End Enum

    Public Class TextBox

        Private _Style As TextBoxStyles = TextBoxStyles.Normal
        <DefaultValue(TextBoxStyles.Normal)> _
        Public Property Style As TextBoxStyles
            Get
                Return _Style
            End Get
            Set(value As TextBoxStyles)
                If _Style <> value Then
                    _Style = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _Text As String = ""
        <Description("The text that will display as the caption."), Category("Appearance"), DefaultValue("")>
        <Browsable(True)>
        <Editor("MultilineStringEditor", "UITypeEditor")>
        Public Overrides Property Text() As String
            Get
                Return Me._Text
            End Get
            Set(value As String)
                If Me._Text <> value Then
                    Me._Text = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _TextFormat = String.Empty
        Public Property TextFormat As String
            Get
                Return _TextFormat
            End Get
            Set(value As String)
                If value <> _TextFormat Then
                    _TextFormat = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _CueText As String = "TextBox"
        <DefaultValue("TextBox")> _
        Public Property CueText As String
            Get
                Return _CueText
            End Get
            Set(value As String)
                If _CueText <> value Then
                    _CueText = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _Multiline As Boolean = False
        <DefaultValue(False)> _
        Public Property Multiline As Boolean
            Get
                Return _Multiline
            End Get
            Set(value As Boolean)
                If _Multiline <> value Then
                    _Multiline = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _ReadOnly As Boolean = False
        <DefaultValue(False)> _
        Public Property [ReadOnly] As Boolean
            Get
                Return _ReadOnly
            End Get
            Set(value As Boolean)
                If _ReadOnly <> value Then
                    _ReadOnly = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _UseCue As Boolean = False
        <DefaultValue(False)> _
        Public Property UseCue As Boolean
            Get
                Return _UseCue
            End Get
            Set(value As Boolean)
                If _UseCue <> value Then
                    _UseCue = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Private _CharacterCasing As System.Windows.Forms.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        <DefaultValue(System.Windows.Forms.CharacterCasing.Normal)>
        Public Property CharacterCasing As System.Windows.Forms.CharacterCasing
            Get
                Return _CharacterCasing
            End Get
            Set(value As System.Windows.Forms.CharacterCasing)
                If _CharacterCasing <> value Then
                    _CharacterCasing = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        'Private _UsePasswordChar = False
        '<DefaultValue(False)> _
        'Public Property UsePasswordChar As Boolean
        '    Get
        '        Return _UsePasswordChar
        '    End Get
        '    Set(value As Boolean)
        '        If _UsePasswordChar <> value Then
        '            _UsePasswordChar = value
        '            Me.Invalidate()
        '        End If
        '    End Set
        'End Property

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            'MyBase.OnPaint(e)

            Me.ButtonAction.Visible = Me.Style <> TextBoxStyles.Normal

            Me.CueProvider.SetHasCue(Me.TextBoxText, Me.UseCue)
            Me.CueProvider.SetCueText(Me.TextBoxText, Me.CueText)

            Me.TextBoxText.ReadOnly = Me.ReadOnly
            Me.TextBoxText.Multiline = Me.Multiline
            Me.TextBoxText.UseSystemPasswordChar = Me.Style = TextBoxStyles.Password

            If Me.TextFormat IsNot String.Empty Then
                Try
                    '        Dim _texts As New List(Of String)
                    '        Dim _text As String = Me.TextBoxText.Text
                    '        Dim _parts As Integer = New System.Text.RegularExpressions.Regex("{[0-999]}").Match(Me.TextFormat).Length
                    '        Dim _len As Integer = _text.Length / _parts

                    '        Dim i As Integer = 0
                    '        Do While i <> _text.Length
                    '            _texts.Add(_text.Substring(i, _len - 1))
                    '            i += _len - 1
                    '        Loop

                    '        Me.TextBoxText.Text = String.Format(Me.TextFormat, _texts.ToArray)
                    Me.TextBoxText.Text = String.Format(Me.TextFormat, Me.Text)
                Catch ex As Exception
                    Me.TextBoxText.Text = Me.Text
                End Try
            Else
                Me.TextBoxText.Text = Me.Text
            End If

            Me.TextBoxText.CharacterCasing = Me.CharacterCasing
            Me.TextBoxText.Top = (Me.Height / 2) - (Me.TextBoxText.Height / 2) - 1

            Select Case Me.Style
                Case TextBoxStyles.Search
                    Me.ButtonAction.Image = My.Resources.search_d
                Case TextBoxStyles.Password
                    Me.ButtonAction.Image = My.Resources.password_d
                Case TextBoxStyles.Cleaner
                    Me.ButtonAction.Image = My.Resources.close_d
            End Select


        End Sub

#Region " Cleaner "

        Friend Overridable Sub OnClear(sender As Object, e As EventArgs) Handles ButtonAction.Click
            If Me.Style = TextBoxStyles.Cleaner Then
                Me.TextBoxText.Text = ""
                Me.TextBoxText.Focus()
            End If
        End Sub

#End Region

#Region " Search "

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

        Public Event Searching As EventHandler
        Public Event SearchStarted As EventHandler
        Public Event SearchCompleted As EventHandler

        Private _PanelProgress As Panel
        Public Property ProgressBarColor As System.Drawing.Color = System.Drawing.Color.LightGreen

        Public Sub SetStartedState()
            _PanelProgress = New Panel
            _PanelProgress.Location = New System.Drawing.Point(0, 0)
            _PanelProgress.Size = New System.Drawing.Size(0, 3)
            _PanelProgress.BackColor = Me.ProgressBarColor
            '_PanelProgress.Dock = DockStyle.Top
            Me.ButtonAction.Image = My.Resources.close_d
            Me.PanelTextBox.Controls.Add(_PanelProgress)
            Me.TextBoxText.Enabled = False
        End Sub

        Public Sub SetCompletedState()
            If _PanelProgress IsNot Nothing Then
                Me.PanelTextBox.Controls.Remove(_PanelProgress)
                _PanelProgress.Dispose()
                _PanelProgress = Nothing
            End If
            Me.ButtonAction.Image = My.Resources.search_d
            Me.TextBoxText.Enabled = True
        End Sub

        Private WithEvents _BackgroundWorkerSearch As System.ComponentModel.BackgroundWorker
        Public Property BackgroundWorkerSearch As System.ComponentModel.BackgroundWorker
            Get
                Return _BackgroundWorkerSearch
            End Get
            Set(value As System.ComponentModel.BackgroundWorker)
                _BackgroundWorkerSearch = value
            End Set
        End Property

        Friend Overridable Sub OnSearchStart(sender As Object, e As EventArgs) Handles ButtonAction.Click
            If Me.Style = TextBoxStyles.Search Then
                If BackgroundWorkerSearch IsNot Nothing Then
                    If BackgroundWorkerSearch.IsBusy Then
                        BackgroundWorkerSearch.CancelAsync()
                    Else
                        Me.SetStartedState()
                        RaiseEvent SearchStarted(Me, e)
                        BackgroundWorkerSearch.RunWorkerAsync(Me.TextBoxText.Text)
                    End If
                Else
                    Me.SetCompletedState()
                    RaiseEvent SearchCompleted(sender, e)
                End If
            End If
        End Sub
        Friend Overridable Sub OnSearching(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _BackgroundWorkerSearch.DoWork
            RaiseEvent Searching(Me, e)
        End Sub
        Private Sub TextBoxSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxText.KeyDown
            If Me.Style = TextBoxStyles.Search Then
                If Not Me.AutoSearch Then
                    If e.KeyCode = Keys.Enter Then
                        Me.OnSearchStart(Me.ButtonAction, New EventArgs)
                    ElseIf e.KeyCode = Keys.Escape Then
                        Me.TextBoxText.ResetText()
                    End If
                Else
                    If Me.TextBoxText.Text.Trim <> String.Empty Then Me.OnSearchStart(Me.ButtonAction, New EventArgs)
                End If
            End If
            Me.OnKeyDown(e)
        End Sub

        Private Sub _BackgroundWorkerSearch_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles _BackgroundWorkerSearch.ProgressChanged
            _PanelProgress.Width = (e.ProgressPercentage * Me.Width) / 100
        End Sub

        Private Sub _BackgroundWorkerSearch_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles _BackgroundWorkerSearch.RunWorkerCompleted
            Me.SetCompletedState()
            If Not e.Cancelled Then
                RaiseEvent SearchCompleted(Me, e)
            End If
        End Sub

#End Region

#Region " Password "

        Private Sub ButtonAction_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonAction.MouseDown
            If Me.Style = TextBoxStyles.Password Then
                Me.TextBoxText.UseSystemPasswordChar = False
            End If
        End Sub
        Private Sub ButtonAction_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonAction.MouseUp
            If Me.Style = TextBoxStyles.Password Then
                Me.TextBoxText.UseSystemPasswordChar = True
            End If
            'If Me.Style = TextBoxStyles.Password Then Me.TextBoxText.Focus()
        End Sub

#End Region

        Protected Overrides Sub OnFontChanged(e As EventArgs)
            MyBase.OnFontChanged(e)
            Me.ResetHeight()
        End Sub
        Protected Overrides Sub OnTextChanged(e As EventArgs)
            MyBase.OnTextChanged(e)
            Me.ResetHeight()
        End Sub
        Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            MyBase.OnSizeChanged(e)
            Me.ResetHeight()
        End Sub

        Private Sub ResetHeight()
            If Me.Multiline = False AndAlso Me.Style = TextBoxStyles.Normal Then
                Dim preferredSize As Size = Me.GetPreferredSize(MyBase.Size)
                MyBase.Size = New Size(MyBase.Width, preferredSize.Height) ' + 6)
            End If
        End Sub

        Private Sub _TextChanged(sender As Object, e As EventArgs) Handles TextBoxText.TextChanged
            Me._Text = Me.TextBoxText.Text
            OnTextChanged(New EventArgs)
        End Sub

        Private Sub _GotFocus(sender As Object, e As EventArgs) Handles ButtonAction.Click, TextBoxText.GotFocus
            Me.InvokeGotFocus(Me, New EventArgs)
        End Sub
        Private Sub _LostFocus(sender As Object, e As EventArgs) Handles ButtonAction.LostFocus, TextBoxText.LostFocus
            Me.InvokeLostFocus(Me, New EventArgs)
        End Sub

        Private Sub TextBoxText_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBoxText.PreviewKeyDown
            Me.OnPreviewKeyDown(e)
        End Sub

    End Class

End Namespace
Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace Sisecom.Windows.Forms.Metro

#Region "Renderer based renderer class"
    ''' <summary>
    ''' A ToolstripManager rendering class with advanced control features
    ''' </summary>
    Public Class Renderer
        Inherits ToolStripProfessionalRenderer
#Region "Initialization and Setup"

        ''' <summary>
        ''' Creates a new Renderer class for modifications
        ''' </summary>
        Public Sub New()
            _tsManager = New IToolstrip()
            _btnManager = New IButton()
            _dBtnManager = New IDropDownButton()
            _tsCtrlManager = New IToolstripControls()
            _pManager = New IPanel()
            _sBtnManager = New ISplitButton()
            _sBarManager = New IStatusBar()
            _mnuManager = New IMenustrip()
        End Sub



#End Region

#Region "Private variables"

        Private _tsManager As IToolstrip = Nothing
        Private _btnManager As IButton = Nothing
        Private _tsCtrlManager As IToolstripControls = Nothing
        Private _pManager As IPanel = Nothing
        Private _sBtnManager As ISplitButton = Nothing
        Private _sBarManager As IStatusBar = Nothing
        Private _mnuManager As IMenustrip = Nothing
        Private _dBtnManager As IDropDownButton = Nothing

        Private _smoothText As [Boolean] = True
        Private _overrideColor As Color = Color.FromArgb(47, 92, 150)
        Private _overrideText As [Boolean] = True

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the manager to edit and change the appearance of the Toolstrip
        ''' </summary>
        <[ReadOnly](True)> _
        Public ReadOnly Property Menustrip() As IMenustrip
            Get
                Return _mnuManager
            End Get
        End Property

        ''' <summary>
        ''' Gets the manager to edit and change the appearance of the Toolstrip
        ''' </summary>
        <[ReadOnly](True)> _
        Public ReadOnly Property Toolstrip() As IToolstrip
            Get
                Return _tsManager
            End Get
        End Property

        ''' <summary>
        ''' Gets the manager to edit and change the appearance of Toolstrip buttons
        ''' </summary>
        <[ReadOnly](True)> _
        Public ReadOnly Property ToolstripButton() As IButton
            Get
                Return _btnManager
            End Get
        End Property

        ''' <summary>
        ''' Gets the manager to edit and change the appearance of other Toolstrip controls
        ''' </summary>
        <[ReadOnly](True)> _
        Public ReadOnly Property ToolstripControls() As IToolstripControls
            Get
                Return _tsCtrlManager
            End Get
        End Property

        ''' <summary>
        ''' Gets the manager to edit and change the appearance of the Panels
        ''' </summary>
        <[ReadOnly](True)> _
        Public ReadOnly Property Panels() As IPanel
            Get
                Return _pManager
            End Get
        End Property

        ''' <summary>
        ''' Gets the manager to edit and change the appearance of the Toolstrip split buttons
        ''' </summary>
        <[ReadOnly](True)> _
        Public ReadOnly Property SplitButton() As ISplitButton
            Get
                Return _sBtnManager
            End Get
        End Property

        ''' <summary>
        ''' Gets the manager to edit and change the appearance of the Status-bar
        ''' </summary>
        <[ReadOnly](True)> _
        Public ReadOnly Property StatusBar() As IStatusBar
            Get
                Return _sBarManager
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets whether to smooth the font text on all controls
        ''' </summary>
        Public Property SmoothText() As [Boolean]
            Get
                Return _smoothText
            End Get
            Set(value As [Boolean])
                _smoothText = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the text if the AlterColor is set to true
        ''' </summary>
        Public Property OverrideColor() As Color
            Get
                Return _overrideColor
            End Get
            Set(value As Color)
                _overrideColor = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether to override the font-color on all controls
        ''' </summary>
        Public Property AlterColor() As [Boolean]
            Get
                Return _overrideText
            End Get
            Set(value As [Boolean])
                _overrideText = value
            End Set
        End Property

#End Region

#Region "Important -- Functions for getting drawing pointers"

#Region "CreatePanelBrush -- Gets a brush based on the docking position of a panel"
        '
        '		/// <summary>
        '		/// Gets a brush dependent on the dock style of a panel
        '		/// </summary>
        '		/// <param name="Panel">The panel which is docked</param>
        '		/// <returns></returns>
        '		private Brush CreatePanelBrush(ToolStripPanel Panel)
        '		{
        '			switch (Panel.Dock)
        '			{
        '				case DockStyle.Top: return new SolidBrush(ContentPanelTop);
        '				case DockStyle.Bottom: return new SolidBrush(ContentPanelBottom);
        '				case DockStyle.Left:
        '				case DockStyle.Right:
        '					return new LinearGradientBrush(Panel.ClientRectangle, ContentPanelTop, ContentPanelBottom, 90f);
        '			}
        '
        '			return null;
        '		}
        '		 

#End Region

#Region "CreateDrawingPath -- Gets a path based on a rectangular area and a provided curve value"
        ''' <summary>
        ''' Creates a GraphicsPath that appreciates an area where things can be drawn
        ''' </summary>
        ''' <param name="Area">The rectangular area which will serve as the base</param>
        ''' <param name="Curve">The curve amount of the corners</param>
        ''' <returns></returns>
        Private Function CreateDrawingPath(Area As Rectangle, Curve As Single) As GraphicsPath
            Dim Result As New GraphicsPath()

            Result.AddLine(Area.Left + Curve, Area.Top, Area.Right - Curve, Area.Top)
            ' Top
            Result.AddLine(Area.Right - Curve, Area.Top, Area.Right, Area.Top + Curve)
            ' Top-right
            Result.AddLine(Area.Right, Area.Top + Curve, Area.Right, Area.Bottom - Curve)
            ' Right
            Result.AddLine(Area.Right, Area.Bottom - Curve, Area.Right - Curve, Area.Bottom)
            ' Bottom-right
            Result.AddLine(Area.Right - Curve, Area.Bottom, Area.Left + Curve, Area.Bottom)
            ' Bottom
            Result.AddLine(Area.Left + Curve, Area.Bottom, Area.Left, Area.Bottom - Curve)
            ' Bottom-left
            Result.AddLine(Area.Left, Area.Bottom - Curve, Area.Left, Area.Top + Curve)
            ' Left
            Result.AddLine(Area.Left, Area.Top + Curve, Area.Left + Curve, Area.Top)
            ' Top-left
            Return Result
        End Function


#End Region

#Region "CreateTrianglePath -- Gets a path based on a rectangle boundary as a triangle shape"
        ''' <summary>
        ''' Creates a triangle based on the size and bounds sectors
        ''' </summary>
        ''' <param name="Bounds">The area which the triangle is confined to</param>
        ''' <param name="Size">The size of the triangle</param>
        ''' <param name="Direction">The direction which the triangle is pointing</param>
        ''' <returns></returns>
        Private Function CreateTrianglePath(Bounds As Rectangle, Size As Int32, Direction As ArrowDirection) As GraphicsPath
            Dim Result As New GraphicsPath()
            Dim x As Integer, y As Integer, c As Integer, j As Integer

            If Direction = ArrowDirection.Left OrElse Direction = ArrowDirection.Right Then
                x = Bounds.Right - (Bounds.Width - Size) / 2
                y = Bounds.Y + Bounds.Height / 2
                c = Size
                j = 0
            Else
                x = Bounds.X + Bounds.Width / 2
                y = Bounds.Bottom - ((Bounds.Height - (Size - 1)) / 2)
                c = Size - 1
                j = Size - 2
            End If

            Select Case Direction
                Case ArrowDirection.Right
                    Result.AddLine(x, y, x - c, y - c)
                    Result.AddLine(x - c, y - c, x - c, y + c)
                    Result.AddLine(x - c, y + c, x, y)
                    Exit Select
                Case ArrowDirection.Down
                    Result.AddLine(x + j, y - j, x - j, y - j)
                    Result.AddLine(x - j, y - j, x, y)
                    Result.AddLine(x, y, x + j, y - j)
                    Exit Select
                Case ArrowDirection.Left
                    Result.AddLine(x - c, y, x, y - c)
                    Result.AddLine(x, y - c, x, y + c)
                    Result.AddLine(x, y + c, x - c, y)
                    Exit Select
            End Select

            Return Result
        End Function


#End Region

#Region "GetButtonBackColor -- Returns different background gradient colors for a normal button state"
        ''' <summary>
        ''' Gets a color array based on the state of a normal button
        ''' </summary>
        ''' <param name="Item">The button to check the state of</param>
        ''' <returns></returns>
        Private Function GetButtonBackColor(Item As ToolStripButton, Type As ButtonType) As Color()
            Dim [Return] As Color() = New Color(2) {}

            If (Not Item.Selected) AndAlso (Not Item.Pressed AndAlso Not Item.Checked) Then
                [Return](0) = Color.Transparent
                [Return](1) = Color.Transparent
            ElseIf (Item.Selected) AndAlso (Not Item.Pressed AndAlso Not Item.Checked) Then
                [Return](0) = _btnManager.HoverBackgroundTop
                [Return](1) = _btnManager.HoverBackgroundBottom
            Else
                [Return](0) = _btnManager.ClickBackgroundTop
                [Return](1) = _btnManager.ClickBackgroundBottom
            End If

            Return [Return]
        End Function


#End Region

#Region "GetButtonBackColor -- Returns different background gradient colors for a split-button state"
        ''' <summary>
        ''' Gets a color array based on the state of a split-button
        ''' </summary>
        ''' <param name="Item">The button to check the state of</param>
        ''' <returns></returns>
        Private Function GetButtonBackColor(Item As ToolStripSplitButton, Type As ButtonType) As Color()
            Dim [Return] As Color() = New Color(2) {}

            If (Not Item.Selected) AndAlso (Not Item.ButtonPressed AndAlso Not Item.DropDownButtonPressed) Then
                [Return](0) = Color.Transparent
                [Return](1) = Color.Transparent
            ElseIf (Item.Selected) AndAlso (Not Item.ButtonPressed AndAlso Not Item.DropDownButtonPressed) Then
                [Return](0) = _sBtnManager.HoverBackgroundTop
                [Return](1) = _sBtnManager.HoverBackgroundBottom
            Else
                If Item.ButtonPressed Then
                    [Return](0) = _sBtnManager.ClickBackgroundTop
                    [Return](1) = _sBtnManager.ClickBackgroundBottom
                ElseIf Item.DropDownButtonPressed Then
                    [Return](0) = _mnuManager.MenustripButtonBackground
                    [Return](1) = _mnuManager.MenustripButtonBackground
                End If
            End If

            Return [Return]
        End Function


#End Region

#Region "GetButtonBackColor -- Returns different background gradient colors for a menu-item state"
        ''' <summary>
        ''' Gets a color array based on the state of a menu-item
        ''' </summary>
        ''' <param name="Item">The button to check the state of</param>
        ''' <returns></returns>
        Private Function GetButtonBackColor(Item As ToolStripMenuItem, Type As ButtonType) As Color()
            Dim [Return] As Color() = New Color(2) {}

            If (Not Item.Selected) AndAlso (Not Item.Pressed AndAlso Not Item.Checked) Then
                [Return](0) = Color.Transparent
                [Return](1) = Color.Transparent
            ElseIf (Item.Selected OrElse Item.Pressed) AndAlso (Not Item.Checked) Then
                If Item.Pressed AndAlso Item.OwnerItem Is Nothing Then
                    [Return](0) = _mnuManager.MenustripButtonBackground
                    [Return](1) = _mnuManager.MenustripButtonBackground
                Else
                    [Return](0) = _mnuManager.Items.HoverBackgroundTop
                    [Return](1) = _mnuManager.Items.HoverBackgroundBottom
                End If
            Else
                [Return](0) = _mnuManager.Items.ClickBackgroundTop
                [Return](1) = _mnuManager.Items.ClickBackgroundBottom
            End If

            Return [Return]
        End Function


#End Region

#Region "GetButtonBackColor -- Returns different background gradient colors for a dropdownbutton state"
        ''' <summary>
        ''' Gets a color array based on the state of a drop-down button
        ''' </summary>
        ''' <param name="Item">The button to check the state of</param>
        ''' <returns></returns>
        Private Function GetButtonBackColor(Item As ToolStripDropDownButton, Type As ButtonType) As Color()
            Dim [Return] As Color() = New Color(2) {}

            If (Not Item.Selected) AndAlso (Not Item.Pressed) Then
                [Return](0) = Color.Transparent
                [Return](1) = Color.Transparent
            ElseIf (Item.Selected) AndAlso (Not Item.Pressed) Then
                [Return](0) = _dBtnManager.HoverBackgroundTop
                [Return](1) = _dBtnManager.HoverBackgroundBottom
            Else
                [Return](0) = _mnuManager.MenustripButtonBackground
                [Return](1) = _mnuManager.MenustripButtonBackground
            End If

            Return [Return]
        End Function


#End Region

#Region "GetBlend -- Gets a blend property based on the blending options and current state"
        ''' <summary>
        ''' Gets a blending property for a specified type of Toolstrip item
        ''' </summary>
        ''' <param name="TSItem">The Toolstrip item</param>
        ''' <param name="Type">The type of item this is</param>
        ''' <returns></returns>
        Private Function GetBlend(TSItem As ToolStripItem, Type As ButtonType) As Blend
            Dim BackBlend As Blend = Nothing

            If Type = ButtonType.NormalButton Then
                Dim Item As ToolStripButton = CType(TSItem, ToolStripButton)

                If Item.Selected AndAlso (Not Item.Checked AndAlso Not Item.Pressed) AndAlso (_btnManager.BlendOptions And BlendRender.Hover) = BlendRender.Hover Then
                    BackBlend = _btnManager.BackgroundBlend
                ElseIf Item.Pressed AndAlso (Not Item.Checked) AndAlso (_btnManager.BlendOptions And BlendRender.Click) = BlendRender.Click Then
                    BackBlend = _btnManager.BackgroundBlend
                ElseIf Item.Checked AndAlso (_btnManager.BlendOptions And BlendRender.Check) = BlendRender.Check Then
                    BackBlend = _btnManager.BackgroundBlend
                End If
            End If
            If Type = ButtonType.DropDownButton Then
                Dim Item As ToolStripDropDownButton = CType(TSItem, ToolStripDropDownButton)

                If Item.Selected AndAlso (Not Item.Pressed) AndAlso (_btnManager.BlendOptions And BlendRender.Hover) = BlendRender.Hover Then
                    BackBlend = _btnManager.BackgroundBlend
                End If
            ElseIf Type = ButtonType.MenuItem Then
                Dim Item As ToolStripMenuItem = CType(TSItem, ToolStripMenuItem)

                If Item.Selected AndAlso (Not Item.Checked AndAlso Not Item.Pressed) AndAlso (_btnManager.BlendOptions And BlendRender.Hover) = BlendRender.Hover Then
                    BackBlend = _mnuManager.Items.BackgroundBlend
                ElseIf Item.Pressed AndAlso (Not Item.Checked) AndAlso (_btnManager.BlendOptions And BlendRender.Click) = BlendRender.Click Then
                    BackBlend = _mnuManager.Items.BackgroundBlend
                ElseIf Item.Checked AndAlso (_btnManager.BlendOptions And BlendRender.Check) = BlendRender.Check Then
                    BackBlend = _mnuManager.Items.BackgroundBlend
                End If
            ElseIf Type = ButtonType.SplitButton Then
                Dim Item As ToolStripSplitButton = CType(TSItem, ToolStripSplitButton)

                If Item.Selected AndAlso (Not Item.ButtonPressed AndAlso Not Item.DropDownButtonPressed) AndAlso (_sBtnManager.BlendOptions And BlendRender.Hover) = BlendRender.Hover Then
                    BackBlend = _sBtnManager.BackgroundBlend
                ElseIf Item.ButtonPressed AndAlso (Not Item.DropDownButtonPressed) AndAlso (_sBtnManager.BlendOptions And BlendRender.Click) = BlendRender.Click Then
                    BackBlend = _sBtnManager.BackgroundBlend
                End If
            End If

            Return BackBlend
        End Function


#End Region

#End Region

#Region "Important -- Functions for drawing"

#Region "PaintBackground -- Simply fills a rectangle with a color"
        ''' <summary>
        ''' Fills a specified boundary with color
        ''' </summary>
        ''' <param name="Link">The Graphics object to draw onto</param>
        ''' <param name="Boundary">The boundaries to draw the color</param>
        ''' <param name="Brush">The brush to fill the color</param>
        Public Sub PaintBackground(Link As Graphics, Boundary As Rectangle, Brush As Brush)
            Link.FillRectangle(Brush, Boundary)
        End Sub


#End Region

#Region "PaintBackground -- Fills a rectangle with Top and Bottom colors"
        ''' <summary>
        ''' Fills a specified boundary with a gradient with specified colors
        ''' </summary>
        ''' <param name="Link">The Graphics object to draw onto</param>
        ''' <param name="Boundary">The boundaries to draw the color</param>
        ''' <param name="Top">The color of the gradient at the top</param>
        ''' <param name="Bottom">The color of the gradient at the bottom</param>
        Public Sub PaintBackground(Link As Graphics, Boundary As Rectangle, Top As Color, Bottom As Color)
            PaintBackground(Link, Boundary, Top, Bottom, 90.0F, Nothing)
        End Sub


#End Region

#Region "PaintBackground -- Fills a rectangle with Top and Bottom colors at a given angle"
        ''' <summary>
        ''' Fills a specified boundary with a gradient with specified colors at a given angle
        ''' </summary>
        ''' <param name="Link">The Graphics object to draw onto</param>
        ''' <param name="Boundary">The boundaries to draw the color</param>
        ''' <param name="Top">The color of the gradient at the top</param>
        ''' <param name="Bottom">The color of the gradient at the bottom</param>
        ''' <param name="Angle">The angle which the gradient is drawn (null defaults to 90f)</param>
        Public Sub PaintBackground(Link As Graphics, Boundary As Rectangle, Top As Color, Bottom As Color, Angle As Single)
            PaintBackground(Link, Boundary, Top, Bottom, Angle, Nothing)
        End Sub


#End Region

#Region "PaintBackground -- Fills a rectangle with Top and Bottom colors at a given angle with blending"
        ''' <summary>
        ''' Fills a specified boundary with a gradient with specified colors at a given angle and with blending properties
        ''' </summary>
        ''' <param name="Link">The Graphics object to draw onto</param>
        ''' <param name="Boundary">The boundaries to draw the color</param>
        ''' <param name="Top">The color of the gradient at the top</param>
        ''' <param name="Bottom">The color of the gradient at the bottom</param>
        ''' <param name="Angle">The angle which the gradient is drawn (null defaults to 90f)</param>
        ''' <param name="Blend">The blending options to draw the gradient</param>
        Public Sub PaintBackground(Link As Graphics, Boundary As Rectangle, Top As Color, Bottom As Color, Angle As Single, Blend As Blend)
            If Angle = Nothing Then
                Angle = 90.0F
            End If

            Using Fill As New LinearGradientBrush(Boundary, Top, Bottom, Angle)
                If Blend IsNot Nothing Then
                    Fill.Blend = Blend
                End If

                Link.FillRectangle(Fill, Boundary)
                Fill.Dispose()
            End Using
        End Sub


#End Region

#Region "PaintBorder -- Draws a border along a set path"
        ''' <summary>
        ''' Draws a set path with a defined brush
        ''' </summary>
        ''' <param name="Link">The Graphics object to draw onto</param>
        ''' <param name="Path">The path to draw along</param>
        ''' <param name="Brush">The brush to fill the color</param>
        Public Sub PaintBorder(Link As Graphics, Path As GraphicsPath, Brush As Brush)
            Link.DrawPath(New Pen(Brush), Path)
        End Sub


#End Region

#Region "PaintBorder -- Draws a border along a set path with Top and Bottom colors"
        ''' <summary>
        ''' Draws a set path with specified colors
        ''' </summary>
        ''' <param name="Link">The Graphics object to draw onto</param>
        ''' <param name="Path">The path to draw along</param>
        ''' <param name="Area">The area of span the border gradient covers</param>
        ''' <param name="Top">The color of the gradient at the top</param>
        ''' <param name="Bottom">The color of the gradient at the bottom</param>
        Public Sub PaintBorder(Link As Graphics, Path As GraphicsPath, Area As Rectangle, Top As Color, Bottom As Color)
            PaintBorder(Link, Path, Area, Top, Bottom, 90.0F, _
                Nothing)
        End Sub


#End Region

#Region "PaintBorder -- Draws a border along a set path with Top and Bottom colors at a given angle"
        ''' <summary>
        ''' Draws a set path with specified colors at a given angle
        ''' </summary>
        ''' <param name="Link">The Graphics object to draw onto</param>
        ''' <param name="Path">The path to draw along</param>
        ''' <param name="Area">The area of span the border gradient covers</param>
        ''' <param name="Top">The color of the gradient at the top</param>
        ''' <param name="Bottom">The color of the gradient at the bottom</param>
        ''' <param name="Angle">The angle which the gradient is drawn (null defaults to 90f)</param>
        Public Sub PaintBorder(Link As Graphics, Path As GraphicsPath, Area As Rectangle, Top As Color, Bottom As Color, Angle As Single)
            PaintBorder(Link, Path, Area, Top, Bottom, Angle, _
                Nothing)
        End Sub


#End Region

#Region "PaintBorder -- Draws a border along a set path with Top and Bottom colors at a given angle with blending"
        ''' <summary>
        ''' Draws a set path with specified colors at a given angle with blending properties
        ''' </summary>
        ''' <param name="Link">The Graphics object to draw onto</param>
        ''' <param name="Path">The path to draw along</param>
        ''' <param name="Top">The color of the gradient at the top</param>
        ''' <param name="Bottom">The color of the gradient at the bottom</param>
        ''' <param name="Angle">The angle which the gradient is drawn (null defaults to 90f)</param>
        ''' <param name="Blend">The blending options to draw the gradient</param>
        Public Sub PaintBorder(Link As Graphics, Path As GraphicsPath, Area As Rectangle, Top As Color, Bottom As Color, Angle As Single, _
            Blend As Blend)
            If Angle = Nothing Then
                Angle = 90.0F
            End If

            Using Fill As New LinearGradientBrush(Area, Top, Bottom, Angle)
                If Blend IsNot Nothing Then
                    Fill.Blend = Blend
                End If

                Link.DrawPath(New Pen(Fill), Path)
                Fill.Dispose()
            End Using
        End Sub


#End Region

#End Region

#Region "Important -- Functions handling the OnRender delegations"

#Region "IDrawToolstripButton -- Draws a Toolstrip button applying the backround and border"
        ''' <summary>
        ''' Draws a Toolstrip button
        ''' </summary>
        ''' <param name="Item">The Toolstrip button</param>
        ''' <param name="Link">The Graphics object to handle</param>
        ''' <param name="Parent">The parent Toolstrip</param>
        Public Sub IDrawToolstripButton(Item As ToolStripButton, Link As Graphics, Parent As ToolStrip)
            Dim Area As New Rectangle(New Point(0, 0), New Size(Item.Bounds.Size.Width - 1, Item.Bounds.Size.Height - 1))

            Dim BackBlend As Blend = GetBlend(Item, ButtonType.NormalButton)
            Dim Render As Color() = GetButtonBackColor(Item, ButtonType.NormalButton)

            Using Path As GraphicsPath = CreateDrawingPath(Area, _btnManager.Curve)
                Link.SetClip(Path)

                PaintBackground(Link, Area, Render(0), Render(1), _btnManager.BackgroundAngle, BackBlend)

                Link.ResetClip()

                Link.SmoothingMode = SmoothingMode.AntiAlias

                Using OBPath As GraphicsPath = CreateDrawingPath(Area, _btnManager.Curve)
                    PaintBorder(Link, OBPath, Area, _btnManager.BorderTop, _btnManager.BorderBottom, _btnManager.BorderAngle, _
                        _btnManager.BorderBlend)

                    OBPath.Dispose()
                End Using

                Area.Inflate(-1, -1)

                Using IBPath As GraphicsPath = CreateDrawingPath(Area, _btnManager.Curve)
                    Using InnerBorder As New SolidBrush(_btnManager.InnerBorder)
                        PaintBorder(Link, IBPath, InnerBorder)

                        InnerBorder.Dispose()
                    End Using
                End Using

                Link.SmoothingMode = SmoothingMode.[Default]
            End Using
        End Sub


#End Region

#Region "IDrawDropDownButton -- Draws a Toolstrip dropdownbutton applying the backround and border"
        ''' <summary>
        ''' Draws a Toolstrip button
        ''' </summary>
        ''' <param name="Item">The Toolstrip button</param>
        ''' <param name="Link">The Graphics object to handle</param>
        ''' <param name="Parent">The parent Toolstrip</param>
        Public Sub IDrawDropDownButton(Item As ToolStripDropDownButton, Link As Graphics, Parent As ToolStrip)
            Dim Area As New Rectangle(New Point(0, 0), New Size(Item.Bounds.Size.Width - 1, Item.Bounds.Size.Height - 1))

            Dim BackBlend As Blend = GetBlend(Item, ButtonType.DropDownButton)
            Dim Render As Color() = GetButtonBackColor(Item, ButtonType.DropDownButton)

            Using Path As GraphicsPath = CreateDrawingPath(Area, _btnManager.Curve)
                Link.SetClip(Path)

                PaintBackground(Link, Area, Render(0), Render(1), _btnManager.BackgroundAngle, BackBlend)

                Link.ResetClip()

                Link.SmoothingMode = SmoothingMode.AntiAlias

                Using OBPath As GraphicsPath = CreateDrawingPath(Area, _btnManager.Curve)
                    PaintBorder(Link, OBPath, Area, _btnManager.BorderTop, _btnManager.BorderBottom, _btnManager.BorderAngle, _
                        _btnManager.BorderBlend)

                    OBPath.Dispose()
                End Using

                If Not Item.Pressed Then
                    Area.Inflate(-1, -1)

                    Using IBPath As GraphicsPath = CreateDrawingPath(Area, _dBtnManager.Curve)
                        Using InnerBorder As New SolidBrush(_dBtnManager.InnerBorder)
                            PaintBorder(Link, IBPath, InnerBorder)

                            InnerBorder.Dispose()
                        End Using
                    End Using
                End If

                Link.SmoothingMode = SmoothingMode.[Default]
            End Using
        End Sub


#End Region

#Region "IDrawToolstripBackground -- Draws a Toolstrip background"
        ''' <summary>
        ''' Draws the Toolstrip background
        ''' </summary>
        ''' <param name="Item">The Toolstrip being drawn</param>
        ''' <param name="Link">The Graphics object to handle</param>
        ''' <param name="Bounds">The affected bounds</param>
        Public Sub IDrawToolstripBackground(Item As ToolStrip, Link As Graphics, Bounds As Rectangle)
            Dim Area As New Rectangle(0, 0, Bounds.Width - 1, Bounds.Height - 1)

            Link.SmoothingMode = SmoothingMode.None

            Using Path As GraphicsPath = CreateDrawingPath(Area, _tsManager.Curve)
                Link.SetClip(Path)

                PaintBackground(Link, Area, _tsManager.BackgroundTop, _tsManager.BackgroundBottom, _tsManager.BackgroundAngle, _tsManager.BackgroundBlend)

                Link.ResetClip()

                Path.Dispose()
            End Using
        End Sub


#End Region

#Region "IDrawToolstripSplitButton -- Draws a Toolstrip split-button with the arrow"
        ''' <summary>
        ''' Draws a Toolstrip split-button
        ''' </summary>
        ''' <param name="Item">The Toolstrip split-button</param>
        ''' <param name="Link">The Graphics object to handle</param>
        ''' <param name="Parent">The parent Toolstrip</param>
        Public Sub IDrawToolstripSplitButton(Item As ToolStripSplitButton, Link As Graphics, Parent As ToolStrip)
            If Item.Selected OrElse Item.DropDownButtonPressed OrElse Item.ButtonPressed Then
                Dim Area As New Rectangle(New Point(0, 0), New Size(Item.Bounds.Size.Width - 1, Item.Bounds.Size.Height - 1))

                Dim BackBlend As Blend = GetBlend(Item, ButtonType.SplitButton)
                Dim NormalRender As Color() = New Color() {_sBtnManager.HoverBackgroundTop, _sBtnManager.HoverBackgroundBottom}
                Dim Render As Color() = GetButtonBackColor(Item, ButtonType.SplitButton)

                Using Path As GraphicsPath = CreateDrawingPath(Area, _sBtnManager.Curve)
                    Link.SetClip(Path)

                    If Not Item.DropDownButtonPressed Then
                        PaintBackground(Link, Area, NormalRender(0), NormalRender(1), _sBtnManager.BackgroundAngle, BackBlend)
                    Else
                        PaintBackground(Link, Area, Render(0), Render(1))
                    End If

                    If Item.ButtonPressed Then
                        Dim ButtonArea As New Rectangle(New Point(0, 0), New Size(Item.ButtonBounds.Width, Item.ButtonBounds.Height - 1))

                        PaintBackground(Link, ButtonArea, Render(0), Render(1), _sBtnManager.BackgroundAngle, _sBtnManager.BackgroundBlend)
                    End If

                    Link.ResetClip()

                    Link.SmoothingMode = SmoothingMode.AntiAlias

                    Using OBPath As GraphicsPath = CreateDrawingPath(Area, _sBtnManager.Curve)
                        Dim TopColor As Color = (If(Item.DropDownButtonPressed, _mnuManager.MenustripButtonBorder, _sBtnManager.BorderTop))
                        Dim BottomColor As Color = (If(Item.DropDownButtonPressed, _mnuManager.MenustripButtonBorder, _sBtnManager.BorderBottom))

                        PaintBorder(Link, OBPath, Area, TopColor, BottomColor, _sBtnManager.BorderAngle, _
                            _sBtnManager.BorderBlend)

                        OBPath.Dispose()
                    End Using

                    If Not Item.DropDownButtonPressed Then
                        Area.Inflate(-1, -1)

                        Using IBPath As GraphicsPath = CreateDrawingPath(Area, _sBtnManager.Curve)
                            Using InnerBorder As New SolidBrush(_sBtnManager.InnerBorder)
                                PaintBorder(Link, IBPath, InnerBorder)


                                Link.DrawRectangle(New Pen(_sBtnManager.InnerBorder), New Rectangle(Item.ButtonBounds.Width, 1, 2, Item.ButtonBounds.Height - 3))

                                InnerBorder.Dispose()
                            End Using
                        End Using

                        Using SplitLine As New LinearGradientBrush(New Rectangle(0, 0, 1, Item.Height), _sBtnManager.BorderTop, _sBtnManager.BorderBottom, _sBtnManager.BackgroundAngle)
                            If _sBtnManager.BackgroundBlend IsNot Nothing Then
                                SplitLine.Blend = _sBtnManager.BackgroundBlend
                            End If

                            Link.DrawLine(New Pen(SplitLine), Item.ButtonBounds.Width + 1, 0, Item.ButtonBounds.Width + 1, Item.Height - 1)

                            SplitLine.Dispose()
                        End Using
                    End If

                    Link.SmoothingMode = SmoothingMode.[Default]
                End Using
            End If

            Dim ArrowSize As Int32 = 5

            If (_sBtnManager.ArrowDisplay = ArrowDisplay.Always) OrElse (_sBtnManager.ArrowDisplay = ArrowDisplay.Hover AndAlso Item.Selected) Then
                Using TrianglePath As GraphicsPath = CreateTrianglePath(New Rectangle(Item.DropDownButtonBounds.Left + (ArrowSize / 2) - 1, (Item.DropDownButtonBounds.Height / 2) - (ArrowSize / 2) - 3, ArrowSize * 2, ArrowSize * 2), ArrowSize, ArrowDirection.Down)
                    Link.FillPath(New SolidBrush(_sBtnManager.ArrowColor), TrianglePath)

                    TrianglePath.Dispose()

                End Using
            End If
        End Sub


#End Region

#Region "IDrawStatusbarBackground -- Draws the statusbar background"
        ''' <summary>
        ''' Draws the Statusbar background
        ''' </summary>
        ''' <param name="Item">The Statusbar being drawn</param>
        ''' <param name="Link">The Graphics object to handle</param>
        ''' <param name="Bounds">The affected bounds</param>
        Public Sub IDrawStatusbarBackground(Item As StatusStrip, Link As Graphics, Bounds As Rectangle)
            PaintBackground(Link, Bounds, _sBarManager.BackgroundTop, _sBarManager.BackgroundBottom, _sBarManager.BackgroundAngle, _sBarManager.BackgroundBlend)

            Link.DrawLine(New Pen(_sBarManager.DarkBorder), 0, 0, Bounds.Width, 0)

            Link.DrawLine(New Pen(_sBarManager.LightBorder), 0, 1, Bounds.Width, 1)
        End Sub


#End Region

#Region "IDrawMenustripItem -- Draws a Menustrip item applying the background and border"
        ''' <summary>
        ''' Draws a Menustrip item
        ''' </summary>
        ''' <param name="Item">The Menustrip item</param>
        ''' <param name="Link">The Graphics object to handle</param>
        ''' <param name="Parent">The parent Toolstrip</param>
        Public Sub IDrawMenustripItem(Item As ToolStripMenuItem, Link As Graphics, Parent As ToolStrip)
            Dim Area As New Rectangle(New Point(0, 0), New Size(Item.Bounds.Size.Width - 1, Item.Bounds.Size.Height - 1))

            If Item.OwnerItem IsNot Nothing Then
                Area.X += 2
                Area.Width -= 3
            End If

            Dim BackBlend As Blend = GetBlend(Item, ButtonType.MenuItem)
            Dim Render As Color() = GetButtonBackColor(Item, ButtonType.MenuItem)

            Using Path As GraphicsPath = CreateDrawingPath(Area, _btnManager.Curve)
                Link.SetClip(Path)

                PaintBackground(Link, Area, Render(0), Render(1), _btnManager.BackgroundAngle, BackBlend)

                Link.ResetClip()

                Link.SmoothingMode = SmoothingMode.AntiAlias

                Using OBPath As GraphicsPath = CreateDrawingPath(Area, _btnManager.Curve)
                    PaintBorder(Link, OBPath, Area, _mnuManager.MenustripButtonBorder, _mnuManager.MenustripButtonBorder, _btnManager.BorderAngle, _
                        _btnManager.BorderBlend)

                    OBPath.Dispose()
                End Using

                If Not Item.Pressed Then
                    Area.Inflate(-1, -1)

                    Using IBPath As GraphicsPath = CreateDrawingPath(Area, _btnManager.Curve)
                        Using InnerBorder As New SolidBrush(_btnManager.InnerBorder)
                            PaintBorder(Link, IBPath, InnerBorder)

                            InnerBorder.Dispose()
                        End Using
                    End Using
                End If

                Link.SmoothingMode = SmoothingMode.[Default]
            End Using
        End Sub


#End Region

#End Region

#Region "Important* -- The OnRender protected overrides"

#Region "Render Button Background -- Handles drawing toolstrip/menu/status-strip buttons"
        ''' <summary>
        ''' Covers the button background rendering
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnRenderButtonBackground(e As ToolStripItemRenderEventArgs)
            If TypeOf e.ToolStrip Is ContextMenuStrip OrElse TypeOf e.ToolStrip Is ToolStripDropDownMenu OrElse TypeOf e.ToolStrip Is MenuStrip Then
                Dim Item As ToolStripMenuItem = CType(e.Item, ToolStripMenuItem)

                If Item.Selected OrElse Item.Checked OrElse Item.Pressed Then
                    IDrawMenustripItem(Item, e.Graphics, e.ToolStrip)
                End If
            ElseIf TypeOf e.ToolStrip Is StatusStrip Then
            Else
                Dim Item As ToolStripButton = CType(e.Item, ToolStripButton)

                If Item.Selected OrElse Item.Checked OrElse Item.Pressed Then
                    IDrawToolstripButton(Item, e.Graphics, e.ToolStrip)
                End If
            End If
        End Sub


#End Region

#Region "Render Dropdown Button Background"
        Protected Overrides Sub OnRenderDropDownButtonBackground(e As ToolStripItemRenderEventArgs)
            If e.Item.Selected OrElse e.Item.Pressed Then
                IDrawDropDownButton(CType(e.Item, ToolStripDropDownButton), e.Graphics, e.ToolStrip)
            End If
        End Sub


#End Region

#Region "Render Image Margin -- Handles drawing the image margin on drop-down menus"
        Protected Overrides Sub OnRenderImageMargin(e As ToolStripRenderEventArgs)
            Dim Area As New Rectangle(2, 2, e.AffectedBounds.Width, e.AffectedBounds.Height - 4)

            PaintBackground(e.Graphics, Area, _mnuManager.MarginLeft, _mnuManager.MarginRight, 0.0F)

            e.Graphics.DrawLine(New Pen(_mnuManager.MenuBorderDark), e.AffectedBounds.Width + 1, 2, e.AffectedBounds.Width + 1, e.AffectedBounds.Height - 3)
        End Sub


#End Region

#Region "Render Item Text -- Allows smoothing of text and changing the color"
        Protected Overrides Sub OnRenderItemText(e As ToolStripItemTextRenderEventArgs)
            If _smoothText Then
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
            End If

            If _overrideText Then
                e.TextColor = _overrideColor
            End If

            MyBase.OnRenderItemText(e)
        End Sub


#End Region

#Region "Render Menuitem Background -- Handles drawing menu-item backgrounds"
        Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
            Dim Item As ToolStripMenuItem = CType(e.Item, ToolStripMenuItem)

            If (Not Item.Selected AndAlso Not Item.Checked AndAlso Not Item.Pressed) OrElse Item.Enabled = False Then
                Return
            End If

            If TypeOf e.ToolStrip Is MenuStrip OrElse TypeOf e.ToolStrip Is ToolStripDropDownMenu OrElse TypeOf e.ToolStrip Is ContextMenuStrip Then
                IDrawMenustripItem(Item, e.Graphics, e.ToolStrip)
            End If
        End Sub


#End Region

#Region "Render Seperator -- Handles drawing the seperator for the toolstrip and contextmenu controls"
        Protected Overrides Sub OnRenderSeparator(e As ToolStripSeparatorRenderEventArgs)
            If TypeOf e.ToolStrip Is ContextMenuStrip OrElse TypeOf e.ToolStrip Is ToolStripDropDownMenu Then
                ' Draw it

                e.Graphics.DrawLine(New Pen(_mnuManager.SeperatorDark), _mnuManager.SeperatorInset, 3, e.Item.Width + 1, 3)
                e.Graphics.DrawLine(New Pen(_mnuManager.SeperatorLight), _mnuManager.SeperatorInset, 4, e.Item.Width + 1, 4)
            Else
                If e.Vertical Then
                    e.Graphics.DrawLine(New Pen(_tsCtrlManager.SeperatorDark), 3, 5, 3, e.Item.Height - 6)
                    e.Graphics.DrawLine(New Pen(_tsCtrlManager.SeperatorLight), 4, 6, 4, e.Item.Height - 6)
                Else
                    e.Graphics.DrawLine(New Pen(_tsCtrlManager.SeperatorDark), 8, 0, e.Item.Width - 6, 0)
                    e.Graphics.DrawLine(New Pen(_tsCtrlManager.SeperatorLight), 9, 1, e.Item.Width - 6, 1)
                End If
            End If
        End Sub


#End Region

#Region "Render SplitButton Background -- Handles drawing the split button"
        Protected Overrides Sub OnRenderSplitButtonBackground(e As ToolStripItemRenderEventArgs)
            Dim Item As ToolStripSplitButton = CType(e.Item, ToolStripSplitButton)

            IDrawToolstripSplitButton(Item, e.Graphics, e.ToolStrip)
        End Sub


#End Region

#Region "Render Statusstrip Sizing Grip"
        Protected Overrides Sub OnRenderStatusStripSizingGrip(e As ToolStripRenderEventArgs)
            Using Top As New SolidBrush(_sBarManager.GripTop), Bottom As New SolidBrush(_sBarManager.GripBottom)
                Dim d As Int32 = _sBarManager.GripSpacing
                Dim y As Int32 = e.AffectedBounds.Bottom - (d * 4)

                Dim a As Integer = 1
                While a < 4
                    y = y + d

                    Dim b As Integer = 1
                    While a >= b
                        Dim x As Int32 = e.AffectedBounds.Right - (d * b)

                        e.Graphics.FillRectangle(Bottom, x + 1, y + 1, 2, 2)
                        e.Graphics.FillRectangle(Top, x, y, 2, 2)
                        System.Math.Max(System.Threading.Interlocked.Increment(b), b - 1)
                    End While
                    System.Math.Max(System.Threading.Interlocked.Increment(a), a - 1)
                End While
            End Using
        End Sub


#End Region

#Region "Render Toolstrip Background -- Handles drawing toolstrip/menu/status-strip backgrounds"
        Protected Overrides Sub OnRenderToolStripBackground(e As ToolStripRenderEventArgs)
            If TypeOf e.ToolStrip Is ContextMenuStrip OrElse TypeOf e.ToolStrip Is ToolStripDropDownMenu Then
                PaintBackground(e.Graphics, e.AffectedBounds, _mnuManager.BackgroundTop, _mnuManager.BackgroundBottom, 90.0F, _mnuManager.BackgroundBlend)

                Dim Border As New Rectangle(0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1)

                Using Path As GraphicsPath = CreateDrawingPath(Border, 0)
                    e.Graphics.ExcludeClip(New Rectangle(1, 0, e.ConnectedArea.Width, e.ConnectedArea.Height - 1))

                    PaintBorder(e.Graphics, Path, New SolidBrush(_mnuManager.MenuBorderDark))

                    e.Graphics.ResetClip()

                    Path.Dispose()
                End Using
            ElseIf TypeOf e.ToolStrip Is MenuStrip Then
                Dim Area As Rectangle = e.AffectedBounds

                PaintBackground(e.Graphics, Area, New SolidBrush(_pManager.ContentPanelTop))
            ElseIf TypeOf e.ToolStrip Is StatusStrip Then
                IDrawStatusbarBackground(CType(e.ToolStrip, StatusStrip), e.Graphics, e.AffectedBounds)
            Else
                e.ToolStrip.BackColor = Color.Transparent

                IDrawToolstripBackground(e.ToolStrip, e.Graphics, e.AffectedBounds)
            End If
        End Sub


#End Region

#Region "Render Toolstrip Border -- Handles drawing the border for toolstrip/menu/status-strip controls"
        Protected Overrides Sub OnRenderToolStripBorder(e As ToolStripRenderEventArgs)
            If TypeOf e.ToolStrip Is ContextMenuStrip OrElse TypeOf e.ToolStrip Is ToolStripDropDownMenu Then
            ElseIf TypeOf e.ToolStrip Is StatusStrip Then
            ElseIf TypeOf e.ToolStrip Is MenuStrip Then
            Else
                Dim Area As New Rectangle(0, -2, e.AffectedBounds.Width - 2, e.AffectedBounds.Height + 1)
                Using Path As GraphicsPath = CreateDrawingPath(Area, _tsManager.Curve)
                    PaintBorder(e.Graphics, Path, e.AffectedBounds, _tsManager.BorderTop, _tsManager.BorderBottom, _tsManager.BorderAngle, _
                        _tsManager.BorderBlend)

                    Path.Dispose()
                End Using
            End If
        End Sub


#End Region

#Region "Render Toolstrip Content Panel Background -- Handles drawing the content panel background"
        Protected Overrides Sub OnRenderToolStripContentPanelBackground(e As ToolStripContentPanelRenderEventArgs)
            If e.ToolStripContentPanel.ClientRectangle.Width < 3 OrElse e.ToolStripContentPanel.ClientRectangle.Height < 3 Then
                Return
            End If

            e.Handled = True

            e.Graphics.SmoothingMode = _pManager.Mode

            PaintBackground(e.Graphics, e.ToolStripContentPanel.ClientRectangle, _pManager.ContentPanelTop, _pManager.ContentPanelBottom, _pManager.BackgroundAngle, _pManager.BackgroundBlend)
        End Sub


#End Region

#Region "Render Toolstrip Panel Background -- Handles drawing the backgrounds for each panel"
        Protected Overrides Sub OnRenderToolStripPanelBackground(e As ToolStripPanelRenderEventArgs)
            If e.ToolStripPanel.ClientRectangle.Width < 3 OrElse e.ToolStripPanel.ClientRectangle.Height < 3 Then
                Return
            End If

            e.Handled = True

            Select Case e.ToolStripPanel.Dock
                Case DockStyle.Top
                    PaintBackground(e.Graphics, e.ToolStripPanel.ClientRectangle, New SolidBrush(_pManager.ContentPanelTop))
                    Exit Select

                Case DockStyle.Bottom
                    PaintBackground(e.Graphics, e.ToolStripPanel.ClientRectangle, New SolidBrush(_pManager.ContentPanelBottom))
                    Exit Select

                Case DockStyle.Left, DockStyle.Right
                    PaintBackground(e.Graphics, e.ToolStripPanel.ClientRectangle, _pManager.ContentPanelTop, _pManager.ContentPanelBottom, _pManager.BackgroundAngle, _pManager.BackgroundBlend)
                    Exit Select
            End Select
        End Sub


#End Region

#End Region

#Region "Other functions"

#Region "Apply -- Applies any recent changes to the renderer"
        ''' <summary>
        ''' Applies any and all changes made to the Renderer
        ''' </summary>
        Public Sub Apply()
            ToolStripManager.Renderer = Me
        End Sub


#End Region

#End Region
    End Class
#End Region

#Region "Renderer -- Toolstrip controlling class"
    ''' <summary>
    ''' A class designed to be used in the Renderer master control to customize the look and feel of the base Toolstrip
    ''' </summary>
    Public Class IToolstrip
        Implements IDisposable
#Region "Initialization and Setup"

        ''' <summary>
        ''' Creates a new IToolstrip class for customization
        ''' </summary>
        Public Sub New()
            DefaultBlending()
        End Sub



        ''' <summary>
        ''' Creates a new IToolstrip class for customization
        ''' </summary>
        ''' <param name="Import">The IToolstrip to import the settings from</param>
        Public Sub New(Import As IToolstrip)
            DefaultBlending()

            Apply(Import)
        End Sub



        ''' <summary>
        ''' Disposes of the IToolstrip class and clears all resources related to it
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub



#End Region

#Region "Private variables"

        Private _curve As Integer = 2

        Private _borderTop As Color = Color.Transparent
        Private _borderBottom As Color = Color.FromArgb(71, 117, 177)
        Private _borderBlend As Blend = Nothing
        Private _borderAngle As Single = 90

        Private _backTop As Color = Color.FromArgb(227, 239, 255)
        Private _backBottom As Color = Color.FromArgb(163, 193, 234)
        Private _backBlend As Blend = Nothing
        Private _backAngle As Single = 90

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the color of the Toolstrip background gradient from the top
        ''' </summary>
        Public Property BackgroundTop() As Color
            Get
                Return _backTop
            End Get

            Set(value As Color)
                _backTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Toolstrip background gradient from the bottom
        ''' </summary>
        Public Property BackgroundBottom() As Color
            Get
                Return _backBottom
            End Get

            Set(value As Color)
                _backBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blending that will occur when rendering the Toolstrip background
        ''' If set to null, the Toolstrip will simply draw the gradient
        ''' </summary>
        Public Property BackgroundBlend() As Blend
            Get
                Return _backBlend
            End Get

            Set(value As Blend)
                _backBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the Toolstrip background will be drawn
        ''' </summary>
        Public Property BackgroundAngle() As Single
            Get
                Return _backAngle
            End Get

            Set(value As Single)
                _backAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Toolstrip border gradient from the top
        ''' </summary>
        Public Property BorderTop() As Color
            Get
                Return _borderTop
            End Get

            Set(value As Color)
                _borderTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Toolstrip border gradient from the bottom
        ''' </summary>
        Public Property BorderBottom() As Color
            Get
                Return _borderBottom
            End Get

            Set(value As Color)
                _borderBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blending that will occur when rendering the Toolstrip border
        ''' If set to null, the Toolstrip will simply draw the border
        ''' </summary>
        Public Property BorderBlend() As Blend
            Get
                Return _borderBlend
            End Get

            Set(value As Blend)
                _borderBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the Toolstrip border will be drawn
        ''' </summary>
        Public Property BorderAngle() As Single
            Get
                Return _borderAngle
            End Get

            Set(value As Single)
                _borderAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the curve of the border of the Toolstrip
        ''' </summary>
        Public Property Curve() As Integer
            Get
                Return _curve
            End Get

            Set(value As Integer)
                _curve = value
            End Set
        End Property

#End Region

#Region "Methods"

        ''' <summary>
        ''' Imports the settings from a previous or pre-defined IToolstrip and applies it to the current
        ''' </summary>
        ''' <param name="Import">The IToolstrip to import the settings from</param>
        Public Sub Apply(Import As IToolstrip)
            _backTop = Import._borderTop
            _backBottom = Import._borderBottom
            _backAngle = Import._borderAngle
            _backBlend = Import._backBlend

            _borderTop = Import._borderTop
            _borderBottom = Import._borderBottom
            _borderAngle = Import._borderAngle
            _borderBlend = Import._borderBlend

            _curve = Import._curve
        End Sub



        ''' <summary>
        ''' Sets the blending for both border and background to their defaults
        ''' </summary>
        Public Sub DefaultBlending()
            _borderBlend = New Blend()
            _borderBlend.Positions = New Single() {0.0F, 0.1F, 0.2F, 0.3F, 0.4F, 0.5F, _
                0.6F, 0.7F, 0.8F, 0.9F, 1.0F}
            _borderBlend.Factors = New Single() {0.1F, 0.2F, 0.3F, 0.3F, 0.3F, 0.4F, _
                0.4F, 0.4F, 0.5F, 0.7F, 0.7F}

            _backBlend = New Blend()
            _backBlend.Positions = New Single() {0.0F, 0.3F, 0.5F, 0.8F, 1.0F}
            _backBlend.Factors = New Single() {0.0F, 0.0F, 0.0F, 0.5F, 1.0F}
        End Sub



#End Region
    End Class
#End Region

#Region "Renderer -- Toolstrip extended controls"
    Public Class IToolstripControls
        Implements IDisposable
#Region "Initialization and Setup"

        ''' <summary>
        ''' Creates a new IToolstripControls class for customization
        ''' </summary>
        Public Sub New()
        End Sub



        ''' <summary>
        ''' Disposes of the IToolstripControls class and clears all resources related to it
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub



#End Region

#Region "Private variables"

        Private _sepDark As Color = Color.FromArgb(154, 198, 255)
        Private _sepLight As Color = Color.White
        Private _sepHeight As Integer = 8

        Private _gripTop As Color = Color.FromArgb(111, 157, 217)
        Private _gripBottom As Color = Color.White
        Private _gripStyle As GripType = GripType.Dotted
        Private _gripDistance As Integer = 4
        Private _gripSize As New Size(2, 2)

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the color of the Toolstrip seperator on the dark side
        ''' </summary>
        Public Property SeperatorDark() As Color
            Get
                Return _sepDark
            End Get

            Set(value As Color)
                _sepDark = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Toolstrip seperator on the light side
        ''' </summary>
        Public Property SeperatorLight() As Color
            Get
                Return _sepLight
            End Get

            Set(value As Color)
                _sepLight = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the height of the Toolstrip seperator control
        ''' </summary>
        Public Property SeperatorHeight() As Integer
            Get
                Return _sepHeight
            End Get

            Set(value As Integer)
                _sepHeight = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the grip dots/line at the top
        ''' </summary>
        Public Property GripTop() As Color
            Get
                Return _gripTop
            End Get

            Set(value As Color)
                _gripTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the grip shadow
        ''' </summary>
        Public Property GripShadow() As Color
            Get
                Return _gripBottom
            End Get

            Set(value As Color)
                _gripBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets in what mode the grip will be drawn
        ''' </summary>
        Public Property GripStyle() As GripType
            Get
                Return _gripStyle
            End Get
            Set(value As GripType)
                _gripStyle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the distance, in pixels, between each grip dot
        ''' </summary>
        Public Property GripDistance() As Integer
            Get
                Return _gripDistance
            End Get
            Set(value As Integer)
                _gripDistance = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the size of the dots or lines for the grip
        ''' </summary>
        Public Property GripSize() As Size
            Get
                Return _gripSize
            End Get
            Set(value As Size)
                _gripSize = value
            End Set
        End Property

#End Region

#Region "Methods"

        ''' <summary>
        ''' Imports the settings from a previous or pre-defined IToolstripControls and applies it to the current
        ''' </summary>
        ''' <param name="Import">The IToolstripControls to import the settings from</param>
        Public Sub Apply(Import As IToolstripControls)
            _sepDark = Import._sepDark
            _sepLight = Import._sepLight
            _sepHeight = Import._sepHeight

            _gripTop = Import._gripTop
            _gripBottom = Import._gripBottom
            _gripDistance = Import._gripDistance
            _gripStyle = Import._gripStyle
            _gripSize = Import._gripSize
        End Sub



#End Region
    End Class
#End Region

#Region "Renderer -- Button controlling class"
    Public Class IButton
        Implements IDisposable
#Region "Initialization and Setup"

        ''' <summary>
        ''' Creates a new IButton class for customization
        ''' </summary>
        Public Sub New()
            DefaultBlending()
        End Sub



        ''' <summary>
        ''' Creates a new IButton class for customization
        ''' </summary>
        ''' <param name="Import">The IButton to import the settings from</param>
        Public Sub New(Import As IButton)
            DefaultBlending()

            Apply(Import)
        End Sub



        ''' <summary>
        ''' Disposes of the IButton class and clears all resources related to it
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub



#End Region

#Region "Private variables"

        Private _borderTop As Color = Color.FromArgb(157, 183, 217)
        Private _borderBottom As Color = Color.FromArgb(157, 183, 217)
        Private _borderInner As Color = Color.FromArgb(255, 247, 185)
        Private _borderBlend As Blend = Nothing
        Private _borderAngle As Single = 90.0F

        Private _hoverBackTop As Color = Color.FromArgb(255, 249, 218)
        Private _hoverBackBottom As Color = Color.FromArgb(237, 189, 62)

        Private _clickBackTop As Color = Color.FromArgb(245, 207, 57)
        Private _clickBackBottom As Color = Color.FromArgb(245, 225, 124)

        Private _backAngle As Single = 90.0F
        Private _backBlend As Blend = Nothing

        Private _blendRender As BlendRender = BlendRender.Hover Or BlendRender.Click Or BlendRender.Check
        Private _curve As Integer = 1

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the color of the Button background at the top, when hovered over
        ''' </summary>
        Public Property HoverBackgroundTop() As Color
            Get
                Return _hoverBackTop
            End Get

            Set(value As Color)
                _hoverBackTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button background at the bottom, when hovered over
        ''' </summary>
        Public Property HoverBackgroundBottom() As Color
            Get
                Return _hoverBackBottom
            End Get

            Set(value As Color)
                _hoverBackBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button background at the top, when clicked
        ''' </summary>
        Public Property ClickBackgroundTop() As Color
            Get
                Return _clickBackTop
            End Get

            Set(value As Color)
                _clickBackTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button background at the bottom, when clicked
        ''' </summary>
        Public Property ClickBackgroundBottom() As Color
            Get
                Return _clickBackBottom
            End Get

            Set(value As Color)
                _clickBackBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blending that will occur when rendering the Button background
        ''' If set to null, the Button will simply draw the gradient
        ''' </summary>
        Public Property BackgroundBlend() As Blend
            Get
                Return _backBlend
            End Get

            Set(value As Blend)
                _backBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the Button background will be drawn
        ''' </summary>
        Public Property BackgroundAngle() As Single
            Get
                Return _backAngle
            End Get

            Set(value As Single)
                _backAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button border gradient from the top
        ''' </summary>
        Public Property BorderTop() As Color
            Get
                Return _borderTop
            End Get

            Set(value As Color)
                _borderTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button border gradient from the bottom
        ''' </summary>
        Public Property BorderBottom() As Color
            Get
                Return _borderBottom
            End Get

            Set(value As Color)
                _borderBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blending that will occur when rendering the Button border
        ''' If set to null, the Button will simply draw the border
        ''' </summary>
        Public Property BorderBlend() As Blend
            Get
                Return _borderBlend
            End Get

            Set(value As Blend)
                _borderBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the Button border will be drawn
        ''' </summary>
        Public Property BorderAngle() As Single
            Get
                Return _borderAngle
            End Get

            Set(value As Single)
                _borderAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the inside border
        ''' </summary>
        Public Property InnerBorder() As Color
            Get
                Return _borderInner
            End Get
            Set(value As Color)
                _borderInner = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets when to apply the rendering ("Normal" does not apply here)
        ''' </summary>
        Public Property BlendOptions() As BlendRender
            Get
                Return _blendRender
            End Get

            Set(value As BlendRender)
                _blendRender = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the curve of the border of the Button
        ''' </summary>
        Public Property Curve() As Integer
            Get
                Return _curve
            End Get

            Set(value As Integer)
                _curve = value
            End Set
        End Property

#End Region

#Region "Methods"

        ''' <summary>
        ''' Imports the settings from a previous or pre-defined IButton and applies it to the current
        ''' </summary>
        ''' <param name="Import">The IButton to import the settings from</param>
        Public Sub Apply(Import As IButton)
            _borderTop = Import._borderTop
            _borderBottom = Import._borderBottom
            _borderAngle = Import._borderAngle
            _borderBlend = Import._borderBlend

            _hoverBackTop = Import._hoverBackTop
            _hoverBackBottom = Import._hoverBackBottom
            _clickBackTop = Import._clickBackTop
            _clickBackBottom = Import._clickBackBottom

            _backAngle = Import._backAngle
            _backBlend = Import._backBlend

            _blendRender = Import._blendRender
            _curve = Import._curve
        End Sub



        ''' <summary>
        ''' Sets the blending for both border and background to their defaults
        ''' </summary>
        Public Sub DefaultBlending()
            _borderBlend = Nothing

            _backBlend = New Blend()
            _backBlend.Positions = New Single() {0.0F, 0.5F, 0.5F, 1.0F}
            _backBlend.Factors = New Single() {0.0F, 0.2F, 1.0F, 0.3F}
        End Sub



#End Region
    End Class

#End Region

#Region "Renderer -- Dropdown Button controlling class"
    Public Class IDropDownButton
        Implements IDisposable
#Region "Initialization and Setup"

        ''' <summary>
        ''' Creates a new IButton class for customization
        ''' </summary>
        Public Sub New()
            DefaultBlending()
        End Sub



        ''' <summary>
        ''' Creates a new IButton class for customization
        ''' </summary>
        ''' <param name="Import">The IButton to import the settings from</param>
        Public Sub New(Import As IDropDownButton)
            DefaultBlending()

            Apply(Import)
        End Sub



        ''' <summary>
        ''' Disposes of the IButton class and clears all resources related to it
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub



#End Region

#Region "Private variables"

        Private _borderTop As Color = Color.FromArgb(157, 183, 217)
        Private _borderBottom As Color = Color.FromArgb(157, 183, 217)
        Private _borderInner As Color = Color.FromArgb(255, 247, 185)
        Private _borderBlend As Blend = Nothing
        Private _borderAngle As Single = 90.0F

        Private _hoverBackTop As Color = Color.FromArgb(255, 249, 218)
        Private _hoverBackBottom As Color = Color.FromArgb(237, 189, 62)

        Private _backAngle As Single = 90.0F
        Private _backBlend As Blend = Nothing

        Private _blendRender As BlendRender = BlendRender.Hover Or BlendRender.Check
        Private _curve As Integer = 1

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the color of the Button background at the top, when hovered over
        ''' </summary>
        Public Property HoverBackgroundTop() As Color
            Get
                Return _hoverBackTop
            End Get

            Set(value As Color)
                _hoverBackTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button background at the bottom, when hovered over
        ''' </summary>
        Public Property HoverBackgroundBottom() As Color
            Get
                Return _hoverBackBottom
            End Get

            Set(value As Color)
                _hoverBackBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blending that will occur when rendering the Button background
        ''' If set to null, the Button will simply draw the gradient
        ''' </summary>
        Public Property BackgroundBlend() As Blend
            Get
                Return _backBlend
            End Get

            Set(value As Blend)
                _backBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the Button background will be drawn
        ''' </summary>
        Public Property BackgroundAngle() As Single
            Get
                Return _backAngle
            End Get

            Set(value As Single)
                _backAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button border gradient from the top
        ''' </summary>
        Public Property BorderTop() As Color
            Get
                Return _borderTop
            End Get

            Set(value As Color)
                _borderTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button border gradient from the bottom
        ''' </summary>
        Public Property BorderBottom() As Color
            Get
                Return _borderBottom
            End Get

            Set(value As Color)
                _borderBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blending that will occur when rendering the Button border
        ''' If set to null, the Button will simply draw the border
        ''' </summary>
        Public Property BorderBlend() As Blend
            Get
                Return _borderBlend
            End Get

            Set(value As Blend)
                _borderBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the Button border will be drawn
        ''' </summary>
        Public Property BorderAngle() As Single
            Get
                Return _borderAngle
            End Get

            Set(value As Single)
                _borderAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the inside border
        ''' </summary>
        Public Property InnerBorder() As Color
            Get
                Return _borderInner
            End Get
            Set(value As Color)
                _borderInner = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets when to apply the rendering ("Normal" and "Click" do not apply here)
        ''' </summary>
        Public Property BlendOptions() As BlendRender
            Get
                Return _blendRender
            End Get

            Set(value As BlendRender)
                _blendRender = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the curve of the border of the Button
        ''' </summary>
        Public Property Curve() As Integer
            Get
                Return _curve
            End Get

            Set(value As Integer)
                _curve = value
            End Set
        End Property

#End Region

#Region "Methods"

        ''' <summary>
        ''' Imports the settings from a previous or pre-defined IDropDownButton and applies it to the current
        ''' </summary>
        ''' <param name="Import">The IDropDownButton to import the settings from</param>
        Public Sub Apply(Import As IDropDownButton)
            _borderTop = Import._borderTop
            _borderBottom = Import._borderBottom
            _borderAngle = Import._borderAngle
            _borderBlend = Import._borderBlend

            _hoverBackTop = Import._hoverBackTop
            _hoverBackBottom = Import._hoverBackBottom

            _backAngle = Import._backAngle
            _backBlend = Import._backBlend

            _blendRender = Import._blendRender
            _curve = Import._curve
        End Sub



        ''' <summary>
        ''' Sets the blending for both border and background to their defaults
        ''' </summary>
        Public Sub DefaultBlending()
            _borderBlend = Nothing

            _backBlend = New Blend()
            _backBlend.Positions = New Single() {0.0F, 0.5F, 0.5F, 1.0F}
            _backBlend.Factors = New Single() {0.0F, 0.2F, 1.0F, 0.3F}
        End Sub



#End Region
    End Class

#End Region

#Region "Renderer -- Split Button controlling class"
    Public Class ISplitButton
        Implements IDisposable
#Region "Initialization and Setup"

        ''' <summary>
        ''' Creates a new ISplitButton class for customization
        ''' </summary>
        Public Sub New()
            DefaultBlending()
        End Sub



        ''' <summary>
        ''' Disposes of the ISplitButton class and clears all resources related to it
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub



#End Region

#Region "Private variables"

        Private _borderTop As Color = Color.FromArgb(157, 183, 217)
        Private _borderBottom As Color = Color.FromArgb(157, 183, 217)
        Private _borderInner As Color = Color.FromArgb(255, 247, 185)
        Private _borderBlend As Blend = Nothing
        Private _borderAngle As Single = 90.0F

        Private _hoverBackTop As Color = Color.FromArgb(255, 249, 218)
        Private _hoverBackBottom As Color = Color.FromArgb(237, 189, 62)

        Private _clickBackTop As Color = Color.FromArgb(245, 207, 57)
        Private _clickBackBottom As Color = Color.FromArgb(245, 225, 124)

        Private _backAngle As Single = 90.0F
        Private _backBlend As Blend = Nothing

        Private _arrowDisplay As ArrowDisplay = ArrowDisplay.Always
        Private _arrowColor As Color = Color.Black

        Private _blendRender As BlendRender = BlendRender.Hover Or BlendRender.Click Or BlendRender.Check
        Private _curve As Integer = 1

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the color of the Button background at the top, when hovered over
        ''' </summary>
        Public Property HoverBackgroundTop() As Color
            Get
                Return _hoverBackTop
            End Get

            Set(value As Color)
                _hoverBackTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button background at the bottom, when hovered over
        ''' </summary>
        Public Property HoverBackgroundBottom() As Color
            Get
                Return _hoverBackBottom
            End Get

            Set(value As Color)
                _hoverBackBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button background at the top, when clicked
        ''' </summary>
        Public Property ClickBackgroundTop() As Color
            Get
                Return _clickBackTop
            End Get

            Set(value As Color)
                _clickBackTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button background at the bottom, when clicked
        ''' </summary>
        Public Property ClickBackgroundBottom() As Color
            Get
                Return _clickBackBottom
            End Get

            Set(value As Color)
                _clickBackBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blending that will occur when rendering the Button background
        ''' If set to null, the Button will simply draw the gradient
        ''' </summary>
        Public Property BackgroundBlend() As Blend
            Get
                Return _backBlend
            End Get

            Set(value As Blend)
                _backBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the Button background will be drawn
        ''' </summary>
        Public Property BackgroundAngle() As Single
            Get
                Return _backAngle
            End Get

            Set(value As Single)
                _backAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button border gradient from the top
        ''' </summary>
        Public Property BorderTop() As Color
            Get
                Return _borderTop
            End Get

            Set(value As Color)
                _borderTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the Button border gradient from the bottom
        ''' </summary>
        Public Property BorderBottom() As Color
            Get
                Return _borderBottom
            End Get

            Set(value As Color)
                _borderBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blending that will occur when rendering the Button border
        ''' If set to null, the Button will simply draw the border
        ''' </summary>
        Public Property BorderBlend() As Blend
            Get
                Return _borderBlend
            End Get

            Set(value As Blend)
                _borderBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the Button border will be drawn
        ''' </summary>
        Public Property BorderAngle() As Single
            Get
                Return _borderAngle
            End Get

            Set(value As Single)
                _borderAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the inside border
        ''' </summary>
        Public Property InnerBorder() As Color
            Get
                Return _borderInner
            End Get
            Set(value As Color)
                _borderInner = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets when to apply the rendering ("Normal" does not apply here)
        ''' </summary>
        Public Property BlendOptions() As BlendRender
            Get
                Return _blendRender
            End Get

            Set(value As BlendRender)
                _blendRender = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the curve of the border of the Button
        ''' </summary>
        Public Property Curve() As Integer
            Get
                Return _curve
            End Get

            Set(value As Integer)
                _curve = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets when to display the drop-down arrow
        ''' </summary>
        Public Property ArrowDisplay() As ArrowDisplay
            Get
                Return _arrowDisplay
            End Get
            Set(value As ArrowDisplay)
                _arrowDisplay = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the drop-down arrow
        ''' </summary>
        Public Property ArrowColor() As Color
            Get
                Return _arrowColor
            End Get
            Set(value As Color)
                _arrowColor = value
            End Set
        End Property

#End Region

#Region "Methods"

        ''' <summary>
        ''' Imports the settings from a previous or pre-defined ISplitButton and applies it to the current
        ''' </summary>
        ''' <param name="Import">The ISplitButton to import the settings from</param>
        Public Sub Apply(Import As ISplitButton)
            _borderTop = Import._borderTop
            _borderBottom = Import._borderBottom
            _borderAngle = Import._borderAngle
            _borderBlend = Import._borderBlend

            _hoverBackTop = Import._hoverBackTop
            _hoverBackBottom = Import._hoverBackBottom
            _clickBackTop = Import._clickBackTop
            _clickBackBottom = Import._clickBackBottom

            _backAngle = Import._backAngle
            _backBlend = Import._backBlend

            _blendRender = Import._blendRender
            _curve = Import._curve

            _arrowDisplay = Import._arrowDisplay
            _arrowColor = Import._arrowColor
        End Sub



        ''' <summary>
        ''' Sets the blending for both border and background to their defaults
        ''' </summary>
        Public Sub DefaultBlending()
            _borderBlend = Nothing

            _backBlend = New Blend()
            _backBlend.Positions = New Single() {0.0F, 0.5F, 0.5F, 1.0F}
            _backBlend.Factors = New Single() {0.0F, 0.2F, 1.0F, 0.3F}
        End Sub



#End Region
    End Class
#End Region

#Region "Renderer -- Content and Panel controlling class"
    Public Class IPanel
        Implements IDisposable
#Region "Initialization and Setup"

        ''' <summary>
        ''' Creates a new IPanel class for customization
        ''' </summary>
        Public Sub New()
        End Sub



        ''' <summary>
        ''' Disposes of the IButton class and clears all resources related to it
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub



#End Region

#Region "Private variables"

        Private _cPanelTop As Color = Color.FromArgb(191, 219, 255)
        Private _cPanelBottom As Color = Color.FromArgb(132, 171, 227)
        Private _cPanelAngle As Single = 90.0F
        Private _cPanelBlend As Blend = Nothing

        Private _mode As SmoothingMode = SmoothingMode.HighSpeed

        Private _panelsInherit As [Boolean] = False

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the color of the gradient at the top of the content panel
        ''' </summary>
        Public Property ContentPanelTop() As Color
            Get
                Return _cPanelTop
            End Get
            Set(value As Color)
                _cPanelTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the gradient at the bottom of the content panel
        ''' </summary>
        Public Property ContentPanelBottom() As Color
            Get
                Return _cPanelBottom
            End Get
            Set(value As Color)
                _cPanelBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether each panel inherits the shading from the content panel
        ''' </summary>
        Public Property PanelInheritance() As [Boolean]
            Get
                Return _panelsInherit
            End Get
            Set(value As [Boolean])
                _panelsInherit = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the background gradient is drawn
        ''' </summary>
        Public Property BackgroundAngle() As Single
            Get
                Return _cPanelAngle
            End Get
            Set(value As Single)
                _cPanelAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blend of the background
        ''' </summary>
        Public Property BackgroundBlend() As Blend
            Get
                Return _cPanelBlend
            End Get
            Set(value As Blend)
                _cPanelBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a mode to render the background in
        ''' </summary>
        Public Property Mode() As SmoothingMode
            Get
                Return _mode
            End Get
            Set(value As SmoothingMode)
                _mode = value
            End Set
        End Property

#End Region
    End Class
#End Region

#Region "Renderer -- Status bar controlling class"
    Public Class IStatusBar
        Implements IDisposable
#Region "Initialization and Setup"

        ''' <summary>
        ''' Creates a new IStatusBar class for customization
        ''' </summary>
        Public Sub New()
            DefaultBlending()
        End Sub



        ''' <summary>
        ''' Disposes of the IButton class and clears all resources related to it
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub



#End Region

#Region "Private variables"

        Private _backTop As Color = Color.FromArgb(227, 239, 255)
        Private _backBottom As Color = Color.FromArgb(173, 209, 255)
        Private _backBlend As Blend = Nothing
        Private _backAngle As Single = 90

        Private _borderDark As Color = Color.FromArgb(86, 125, 176)
        Private _borderLight As Color = Color.White
        Private _borderBlend As Blend = Nothing
        Private _borderAngle As Single = 90

        Private _gripTop As Color = Color.FromArgb(114, 152, 204)
        Private _gripBottom As Color = Color.FromArgb(248, 248, 248)
        Private _gripSpacing As Int32 = 4

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the color of the gradient of the background at the top
        ''' </summary>
        Public Property BackgroundTop() As Color
            Get
                Return _backTop
            End Get
            Set(value As Color)
                _backTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the gradient of the background at the bottom
        ''' </summary>
        Public Property BackgroundBottom() As Color
            Get
                Return _backBottom
            End Get
            Set(value As Color)
                _backBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the blending that will apply to the background
        ''' </summary>
        Public Property BackgroundBlend() As Blend
            Get
                Return _backBlend
            End Get
            Set(value As Blend)
                _backBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the angle which the background gradient will be drawn
        ''' </summary>
        Public Property BackgroundAngle() As Single
            Get
                Return _backAngle
            End Get
            Set(value As Single)
                _backAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the dark border
        ''' </summary>
        Public Property DarkBorder() As Color
            Get
                Return _borderDark
            End Get
            Set(value As Color)
                _borderDark = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the light border
        ''' </summary>
        Public Property LightBorder() As Color
            Get
                Return _borderLight
            End Get
            Set(value As Color)
                _borderLight = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the grip at the top-most
        ''' </summary>
        Public Property GripTop() As Color
            Get
                Return _gripTop
            End Get
            Set(value As Color)
                _gripTop = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the grip at the bottom-most
        ''' </summary>
        Public Property GripBottom() As Color
            Get
                Return _gripBottom
            End Get
            Set(value As Color)
                _gripBottom = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the spacing of the grip blocks
        ''' </summary>
        Public Property GripSpacing() As Int32
            Get
                Return _gripSpacing
            End Get
            Set(value As Int32)
                _gripSpacing = value
            End Set
        End Property

#End Region

#Region "Methods"

        ''' <summary>
        ''' Imports the settings from a previous or pre-defined IStatusBar and applies it to the current
        ''' </summary>
        ''' <param name="Import">The IStatusBar to import the settings from</param>
        Public Sub Apply(Import As IStatusBar)
            _borderDark = Import._borderDark
            _borderLight = Import._borderLight

            _backTop = Import._backTop
            _backBottom = Import._backBottom
            _backAngle = Import._backAngle
            _backBlend = Import._backBlend
        End Sub



        ''' <summary>
        ''' Sets the blending for both border and background to their defaults
        ''' </summary>
        Public Sub DefaultBlending()
            _borderBlend = Nothing

            _backBlend = New Blend()
            _backBlend.Positions = New Single() {0.0F, 0.25F, 0.25F, 0.57F, 0.86F, 1.0F}
            _backBlend.Factors = New Single() {0.1F, 0.6F, 1.0F, 0.4F, 0.0F, 0.95F}
        End Sub



#End Region
    End Class
#End Region

#Region "Renderer -- Menustrip controlling class"
    ''' <summary>
    ''' A class designed to be used in the Renderer master control to customize the look and feel of the base Menustrip
    ''' </summary>
    Public Class IMenustrip
        Implements IDisposable
#Region "Initialization and Setup"

        ''' <summary>
        ''' Creates a new IToolstrip class for customization
        ''' </summary>
        Public Sub New()
            _buttons = New IButton()

            DefaultBlending()
        End Sub



        ''' <summary>
        ''' Creates a new IMenustrip class for customization
        ''' </summary>
        ''' <param name="Import">The IMenustrip to import the settings from</param>
        Public Sub New(Import As IMenustrip)
            _buttons = New IButton()

            DefaultBlending()

            Apply(Import)
        End Sub



        ''' <summary>
        ''' Disposes of the IMenustrip class and clears all resources related to it
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub



#End Region

#Region "Private variables"

        Private _menuBorderDark As Color = Color.FromArgb(157, 183, 217)
        Private _menuBorderLight As Color = Color.Transparent

        Private _menuBackInh As InheritenceType = InheritenceType.FromContentPanel
        Private _menuBackTop As Color = Color.White
        Private _menuBackBottom As Color = Color.White
        Private _menuBackBlend As Blend = Nothing

        Private _menuStripBtnBackground As Color = Color.White
        Private _menuStripBtnBorder As Color = Color.FromArgb(157, 183, 217)

        Private _buttons As IButton = Nothing

        Private _marginLeft As Color = Color.FromArgb(242, 255, 255)
        Private _marginRight As Color = Color.FromArgb(233, 238, 238)
        Private _marginBorder As Color = Color.FromArgb(197, 197, 197)

        Private _sepDark As Color = Color.FromArgb(197, 197, 197)
        Private _sepLight As Color = Color.FromArgb(254, 254, 254)
        Private _sepInset As Int32 = 30

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the color of the menu-strip border (dark)
        ''' </summary>
        Public Property MenuBorderDark() As Color
            Get
                Return _menuBorderDark
            End Get
            Set(value As Color)
                _menuBorderDark = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the menu-strip border (light)
        ''' </summary>
        Public Property MenuBorderLight() As Color
            Get
                Return _menuBorderLight
            End Get
            Set(value As Color)
                _menuBorderLight = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets how the background of the menu-strip is inherited
        ''' </summary>
        Public Property BackgroundInheritence() As InheritenceType
            Get
                Return _menuBackInh
            End Get
            Set(value As InheritenceType)
                _menuBackInh = value
            End Set
        End Property

        ''' <summary>
        ''' If inheritence is set to none, the color of the background gradient at the top
        ''' </summary>
        Public Property BackgroundTop() As Color
            Get
                Return _menuBackTop
            End Get
            Set(value As Color)
                _menuBackTop = value
            End Set
        End Property

        ''' <summary>
        ''' If inheritence is set to none, the color of the background gradient at the bottom
        ''' </summary>
        Public Property BackgroundBottom() As Color
            Get
                Return _menuBackBottom
            End Get
            Set(value As Color)
                _menuBackBottom = value
            End Set
        End Property

        ''' <summary>
        ''' If inheritence is set to none, the blending option for the background
        ''' </summary>
        Public Property BackgroundBlend() As Blend
            Get
                Return _menuBackBlend
            End Get
            Set(value As Blend)
                _menuBackBlend = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the margin gradient at the left
        ''' </summary>
        Public Property MarginLeft() As Color
            Get
                Return _marginLeft
            End Get
            Set(value As Color)
                _marginLeft = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the margin gradient at the right
        ''' </summary>
        Public Property MarginRight() As Color
            Get
                Return _marginRight
            End Get
            Set(value As Color)
                _marginRight = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the margin border (displayed to the right)
        ''' </summary>
        Public Property MarginBorder() As Color
            Get
                Return _marginBorder
            End Get
            Set(value As Color)
                _marginBorder = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the root menu-strip button background when it is selected
        ''' </summary>
        Public Property MenustripButtonBackground() As Color
            Get
                Return _menuStripBtnBackground
            End Get
            Set(value As Color)
                _menuStripBtnBackground = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the root menu-strip button border when it is selected
        ''' </summary>
        Public Property MenustripButtonBorder() As Color
            Get
                Return _menuStripBtnBorder
            End Get
            Set(value As Color)
                _menuStripBtnBorder = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the seperator dark color
        ''' </summary>
        Public Property SeperatorDark() As Color
            Get
                Return _sepDark
            End Get
            Set(value As Color)
                _sepDark = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the seperator light color
        ''' </summary>
        Public Property SeperatorLight() As Color
            Get
                Return _sepLight
            End Get
            Set(value As Color)
                _sepLight = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the inset position of the seperator from the left
        ''' </summary>
        Public Property SeperatorInset() As Int32
            Get
                Return _sepInset
            End Get
            Set(value As Int32)
                _sepInset = value
            End Set
        End Property

        ''' <summary>
        ''' Gets the class that handles the look and feel of the menu-strip items
        ''' </summary>
        <[ReadOnly](True)> _
        Public ReadOnly Property Items() As IButton
            Get
                Return _buttons
            End Get
        End Property

#End Region

#Region "Methods"

        ''' <summary>
        ''' Imports the settings from a previous or pre-defined IMenustrip and applies it to the current
        ''' </summary>
        ''' <param name="Import">The IMenustrip to import the settings from</param>
        Public Sub Apply(Import As IMenustrip)
            _menuBackInh = Import._menuBackInh
            _menuBackTop = Import._menuBackTop
            _menuBackBottom = Import._menuBackBottom
            _menuBorderDark = Import._menuBorderDark
            _menuBorderLight = Import._menuBorderLight
            _menuBackBlend = Import._menuBackBlend
            _buttons = Import._buttons
        End Sub



        ''' <summary>
        ''' Sets the blending for the background to it's default
        ''' </summary>
        Public Sub DefaultBlending()
            _menuBackBlend = New Blend()
            _menuBackBlend.Positions = New Single() {0.0F, 0.3F, 0.5F, 0.8F, 1.0F}
            _menuBackBlend.Factors = New Single() {0.0F, 0.0F, 0.0F, 0.5F, 1.0F}
        End Sub



#End Region
    End Class
#End Region

#Region "Renderer -- Enumerators"

    ''' <summary>
    ''' Defines when to show an arrow
    ''' </summary>
    Public Enum ArrowDisplay
        Always
        Hover
        Never
    End Enum

    ''' <summary>
    ''' Defines when to use a blend property
    ''' </summary>
    Public Enum BlendRender
        ''' <summary>
        ''' Use the blend when the object is drawn
        ''' </summary>
        Normal
        ''' <summary>
        ''' Use the blend when the object is hovered over
        ''' </summary>
        Hover
        ''' <summary>
        ''' Use the blend when the object is clicked
        ''' </summary>
        Click
        ''' <summary>
        ''' Use the blend when the object is checked
        ''' </summary>
        Check
        ''' <summary>
        ''' Always use the blend regardless of the state of the object
        ''' </summary>
        All = Normal Or Hover Or Click Or Check
    End Enum

    ''' <summary>
    ''' Defines a method of drawing a grip on a control
    ''' </summary>
    Public Enum GripType
        ''' <summary>
        ''' Draws the grip as a set of dots
        ''' </summary>
        Dotted
        ''' <summary>
        ''' Draws the grip as two lines
        ''' </summary>
        Lines
        ''' <summary>
        ''' Does not draw the grip at all, but the object remains moveable
        ''' </summary>
        None
    End Enum

    ''' <summary>
    ''' Defines a specific type of button to search by
    ''' </summary>
    Public Enum ButtonType
        NormalButton
        SplitButton
        MenuItem
        DropDownButton
    End Enum

    ''' <summary>
    ''' Defines a method for background or object inheritence
    ''' </summary>
    Public Enum InheritenceType
        FromContentPanel
        None
    End Enum

    ''' <summary>
    ''' Defines a method of rendering
    ''' </summary>
    Public Enum RenderingMode
        System
        Professional
        [Custom]
    End Enum

#End Region
End Namespace
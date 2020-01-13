Public Class Habitat

    Dim Name As String
    Dim ColorMap As Color
    Dim ColorHeadSnake As Brush
    Dim ColorBodySnake As Brush

    Public Sub New(Name As String, ColorMap As Color, ColorHeadSnake As Brush, ColorBodySnake As Brush)

        Me.Name = Name
        Me.ColorMap = ColorMap
        Me.ColorHeadSnake = ColorHeadSnake
        Me.ColorBodySnake = ColorBodySnake

    End Sub

    Public Property GSName() As String
        Get
            Return Name
        End Get
        Set(Value_ As String)
            Name = Value_
        End Set
    End Property

    Public Property GSColorMap() As Color
        Get
            Return ColorMap
        End Get
        Set(Value_ As Color)
            ColorMap = Value_
        End Set
    End Property

    Public Property GSColorHeadSnake() As Brush
        Get
            Return ColorHeadSnake
        End Get
        Set(Value_ As Brush)
            ColorHeadSnake = Value_
        End Set
    End Property

    Public Property GSColorBodySnake() As Brush
        Get
            Return ColorBodySnake
        End Get
        Set(Value_ As Brush)
            ColorBodySnake = Value_
        End Set
    End Property




End Class

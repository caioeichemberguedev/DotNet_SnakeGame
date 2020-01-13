Public Class Snake


    Dim IndexCampo As Integer
    Dim Location As Point
    Dim ColoracaoHead As Brush
    Dim ColoracaoBody As Brush
    Dim Direcao As String

    Public Sub New(IndexCampo As Integer, Location As Point, ColoracaoHead As Brush, ColoracaoBody As Brush, Direcao As String)

        Me.IndexCampo = IndexCampo
        Me.Location = Location
        Me.ColoracaoHead = ColoracaoHead
        Me.ColoracaoBody = ColoracaoBody
        Me.Direcao = Direcao

    End Sub

    Public Property GSIndexCampo() As Integer
        Get
            Return IndexCampo
        End Get
        Set(IndexCampo_ As Integer)
            IndexCampo = IndexCampo_
        End Set
    End Property


    Public Property GSLocation() As Point
        Get
            Return Location
        End Get
        Set(Location_ As Point)
            Location = Location_
        End Set
    End Property

    Public Property GSColoracaoHead() As Brush
        Get
            Return ColoracaoHead
        End Get
        Set(ColoracaoHead_ As Brush)
            ColoracaoHead = ColoracaoHead_
        End Set
    End Property

    Public Property GSColoracaoBody() As Brush
        Get
            Return ColoracaoBody
        End Get
        Set(ColoracaoBody_ As Brush)
            ColoracaoBody = ColoracaoBody_
        End Set
    End Property

    Public Property GSDirecao() As String
        Get
            Return Direcao
        End Get
        Set(Direcao_ As String)
            Direcao = Direcao_
        End Set
    End Property
End Class

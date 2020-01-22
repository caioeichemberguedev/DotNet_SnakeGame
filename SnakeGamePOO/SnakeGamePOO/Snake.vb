Public Class Snake


    Dim IndexCampo As Integer
    Dim Location As Point
    Dim Direcao As String

    Public Sub New(IndexCampo As Integer, Location As Point, Direcao As String)

        Me.IndexCampo = IndexCampo
        Me.Location = Location
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

    Public Property GSDirecao() As String
        Get
            Return Direcao
        End Get
        Set(Direcao_ As String)
            Direcao = Direcao_
        End Set
    End Property
End Class

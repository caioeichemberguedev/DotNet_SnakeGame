Public Class Cheats

    Dim NomeCheat As String
    Dim Tecla1 As String
    Dim Tecla2 As String
    Dim Tecla3 As String
    Dim Tecla4 As String
    Dim Ativado As Boolean

    Public Sub New(NomeCheat As String, Tecla1 As String, Tecla2 As String, Tecla3 As String, Tecla4 As String, Ativado As Boolean)

        Me.NomeCheat = NomeCheat
        Me.Tecla1 = Tecla1
        Me.Tecla2 = Tecla2
        Me.Tecla3 = Tecla3
        Me.Tecla4 = Tecla4
        Me.Ativado = Ativado

    End Sub

    Public Property GSNomeCheat() As String
        Get
            Return NomeCheat
        End Get
        Set(Value_ As String)
            NomeCheat = Value_
        End Set
    End Property

    Public Property GSTecla1() As String
        Get
            Return Tecla1
        End Get
        Set(Value_ As String)
            Tecla1 = Value_
        End Set
    End Property

    Public Property GSTecla2() As String
        Get
            Return Tecla2
        End Get
        Set(Value_ As String)
            Tecla2 = Value_
        End Set
    End Property

    Public Property GSTecla3() As String
        Get
            Return Tecla3
        End Get
        Set(Value_ As String)
            Tecla3 = Value_
        End Set
    End Property

    Public Property GSTecla4() As String
        Get
            Return Tecla4
        End Get
        Set(Value_ As String)
            Tecla4 = Value_
        End Set
    End Property

    Public Property GSAtivado() As Boolean
        Get
            Return Ativado
        End Get
        Set(Value_ As Boolean)
            Ativado = Value_
        End Set
    End Property

End Class

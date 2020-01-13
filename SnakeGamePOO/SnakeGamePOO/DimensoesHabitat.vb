Public Class DimensoesHabitat

    Dim Location As Point
    Dim LineNumber As Integer
    Dim ColumnNumber As Integer

    Public Sub New(Location As Point, LineNumber As Integer, ColumnNumber As Integer)

        Me.Location = Location
        Me.LineNumber = LineNumber
        Me.ColumnNumber = ColumnNumber

    End Sub

    Public Property GSLocation() As Point
        Get
            Return Location
        End Get
        Set(Value_ As Point)
            Location = Value_
        End Set
    End Property

    Public Property GSLineNumber() As Integer
        Get
            Return LineNumber
        End Get
        Set(Value_ As Integer)
            LineNumber = Value_
        End Set
    End Property

    Public Property GSColumnNumber() As Integer
        Get
            Return ColumnNumber
        End Get
        Set(Value_ As Integer)
            ColumnNumber = Value_
        End Set
    End Property


End Class
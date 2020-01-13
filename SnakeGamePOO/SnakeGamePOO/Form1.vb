Imports System.Drawing.Drawing2D
Imports System.Threading.Thread

Public Class Form1

    Private ListaCobra As ArrayList = New ArrayList
    Private ListaCampo As ArrayList = New ArrayList
    Private ListaHabitat As ArrayList = New ArrayList

    Private N_Snake_Nivel As Integer
    Private N_Snake_Tamanho As Integer
    Private N_Snake_Canibal As Integer
    Private N_Snake_Velocidade As Double
    Private N_Snake_Regeneracao As Integer
    Private N_Snake_PontosPercorridos As Integer

    Private N_Relogio1_Tick As Double
    Private N_Relogio2_Tempo As Integer

    Private B_Game_Pause As Boolean
    Private B_Game_Dead As Boolean = False
    Private B_Game_AutorizaMovimento As Boolean = False
    Private B_Game_JogoIniciado As Boolean = False
    Private N_Game_TimerSecond As Integer
    Private S_Game_TimerSecond As String
    Private N_Game_TimerMinute As Integer
    Private S_Game_TimerMinute As String
    Private S_Game_TimerGeneral As String
    Private N_Game_PontuacaoGeral As Integer
    Private T_Game_Timer() As Timer

    Private P_Food_Ponto As Point
    Private R_Food_Sorteio As New Random

    Private N_Habitat_Matriz As Integer = 30

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfiguraTela()
        ConfiguraTimers()
        ConfiguraDimensões()
        PopulaEConfiguraHabitat()
        PrepareGame()
    End Sub

    Private Sub ConfiguraTela()
        Me.Text = "Snake Game"
        Me.CenterToScreen()
        Me.FormBorderStyle = FormBorderStyle.FixedToolWindow
    End Sub

    Private Sub ConfiguraTimers()
        For k = 0 To 2
            ReDim Preserve T_Game_Timer(k)
            T_Game_Timer(k) = New Timer
            With T_Game_Timer(k)
                .Enabled = False
                Select Case k
                    Case 0
                        .Interval = 70 'TEMPO INICIAL DO JOGO PARA INTERVALO DE MOVIMENTAÇÃO DO CORPO DA COBRA
                        AddHandler T_Game_Timer(k).Tick, AddressOf Relogio0_RealizaAtualizaçõesDeMovimento
                    Case 1
                        .Interval = 120 'INTERVALO DE TEMPO EM QUE A COBRA MUDA DE COR, IDENTIFICANDO COLISÃO COM PROPRIO CORPO
                        AddHandler T_Game_Timer(k).Tick, AddressOf Relogio1_IdentificaçãoDeColisãoComPróprioCorpo
                    Case 2
                        .Interval = 1000 'RELOGIO PARA TEMPO JOGADO
                        AddHandler T_Game_Timer(k).Tick, AddressOf Relogio2_AtualizaTempoJogado
                End Select
            End With
        Next
    End Sub

    Private Sub ConfiguraDimensões()
        Dim n_Qtd_Linha As Integer = 0
        Dim n_Qtd_Colunas As Integer = 0
        Dim PontoCampo As Point

        For k = 0 To TotalDeCampoPossiveis(N_Habitat_Matriz)
            PontoCampo = New Point(10 * ((n_Qtd_Colunas * 2) + 1), 10 * ((n_Qtd_Linha * 2) + 1))
            ListaCampo.Add(New DimensoesHabitat(PontoCampo, n_Qtd_Linha, n_Qtd_Colunas))
            If n_Qtd_Colunas = N_Habitat_Matriz - 1 Then
                n_Qtd_Colunas = 0
                n_Qtd_Linha += 1
            Else
                n_Qtd_Colunas += 1
            End If
        Next
    End Sub

    Private Sub PopulaEConfiguraHabitat()
        ListaHabitat.Add(New Habitat("Laboratory", Color.Black, Brushes.Green, Brushes.LimeGreen))
        ListaHabitat.Add(New Habitat("Forest", Color.DarkGreen, Brushes.Black, Brushes.Gray))
        ListaHabitat.Add(New Habitat("Desert", Color.Beige, Brushes.Purple, Brushes.Magenta))
        ListaHabitat.Add(New Habitat("Water", Color.DeepSkyBlue, Brushes.DarkBlue, Brushes.Blue))
        ListaHabitat.Add(New Habitat("Mountain", Color.Gray, Brushes.Red, Brushes.Orange))
        For k = 0 To ListaHabitat.Count - 1
            ComboBox1.Items.Add(ListaHabitat(k).GSName)
        Next
        ComboBox1.SelectedIndex = 0
        PB_Habitat.BackColor = ListaHabitat(ComboBox1.SelectedIndex).GSColorMap
        Refresh()
    End Sub

    Private Sub PrepareGame()
        For k = 0 To 2
            T_Game_Timer(k).Enabled = False
        Next
        N_Relogio1_Tick = 70
        N_Relogio2_Tempo = 0
        N_Snake_Nivel = 0
        N_Snake_Tamanho = 4
        N_Snake_Regeneracao = 3
        S_Game_TimerGeneral = "00:00"
        N_Game_TimerSecond = 0
        N_Game_TimerMinute = 0
        N_Snake_PontosPercorridos = 0
        N_Game_PontuacaoGeral = 0
        ListaCobra.Clear()
        B_Game_Dead = False
        B_Game_JogoIniciado = False

        For k = 0 To N_Snake_Tamanho - 1
            Dim Valor1 As Integer = (N_Snake_Tamanho - 1) - k
            ListaCobra.Add(New Snake(Valor1, ListaCampo(Valor1).GSLocation, ListaHabitat(ComboBox1.SelectedIndex).GSColorHeadSnake, ListaHabitat(ComboBox1.SelectedIndex).GSColorBodySnake, "Leste"))
        Next
        P_Food_Ponto = GeraComida()

        AtualizaInformativo()
        Refresh()
    End Sub

    Private Function TotalDeCampoPossiveis(ByVal Valor As Integer) As Integer
        Return (Valor ^ 2) - 1
    End Function


    Private Sub Direcionamento_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If B_Game_Dead = False Then
            Cursor.Position = New Point(0, 0)
            T_Game_Timer(0).Stop()
            Select Case e.KeyCode
                Case Keys.Up
                    If ListaCobra(0).GSDirecao <> "Sul" And ListaCobra(0).GSDirecao <> "Norte" Then
                        ListaCobra(0).GSDirecao = "Norte"
                        AtualizaSnakeGeral()
                        Exit Sub
                    End If
                Case Keys.Down
                    If ListaCobra(0).GSDirecao <> "Norte" And ListaCobra(0).GSDirecao <> "Sul" Then
                        ListaCobra(0).GSDirecao = "Sul"
                        AtualizaSnakeGeral()
                        Exit Sub
                    End If
                Case Keys.Right
                    If ListaCobra(0).GSDirecao <> "Oeste" And ListaCobra(0).GSDirecao <> "Leste" Then
                        ListaCobra(0).GSDirecao = "Leste"
                        AtualizaSnakeGeral()
                        Exit Sub
                    End If
                Case Keys.Left
                    If ListaCobra(0).GSDirecao <> "Leste" And ListaCobra(0).GSDirecao <> "Oeste" Then
                        ListaCobra(0).GSDirecao = "Oeste"
                        AtualizaSnakeGeral()
                        Exit Sub
                    End If
                Case Keys.Insert
                    'CONFIGURAR CHEATS
                    Exit Sub
                Case Keys.R
                    AutorizaMovimentoGame()
                    PrepareGame()
                    Exit Sub
            End Select
            If B_Game_AutorizaMovimento = True Then
                T_Game_Timer(2).Stop()
                B_Game_Pause = True
            End If
            AtualizaInformativo()
        Else
            Select Case e.KeyCode
                Case Keys.R
                    AutorizaMovimentoGame()
                    PrepareGame()
                    Exit Sub
            End Select
        End If
    End Sub

    Private Sub Relogio0_RealizaAtualizaçõesDeMovimento()
        AtualizaSnakeGeral()
    End Sub

    Private Sub AtualizaIndexSnake()
        For k As Integer = 1 To ListaCobra.Count - 1
            ListaCobra(ListaCobra.Count - k).GSIndexCampo = ListaCobra(ListaCobra.Count - (k + 1)).GSIndexCampo
        Next
    End Sub

    Private Sub AtualizaSnakeGeral()
        B_Game_JogoIniciado = True
        If T_Game_Timer(2).Enabled = False Then
            T_Game_Timer(2).Start()
        End If
        N_Snake_PontosPercorridos += 1

        AtualizaIndexSnake()
        Select Case ListaCobra(0).GSDirecao
            Case "Norte"
                If ListaCampo(ListaCobra(0).GSIndexCampo).GSLineNumber > 0 Then
                    ListaCobra(0).GSIndexCampo -= N_Habitat_Matriz
                Else
                    ListaCobra(0).GSIndexCampo += (TotalDeCampoPossiveis(N_Habitat_Matriz) - (N_Habitat_Matriz - 1))
                End If
            Case "Sul"
                If ListaCampo(ListaCobra(0).GSIndexCampo).GSLineNumber < N_Habitat_Matriz - 1 Then
                    ListaCobra(0).GSIndexCampo += N_Habitat_Matriz
                Else
                    ListaCobra(0).GSIndexCampo -= (TotalDeCampoPossiveis(N_Habitat_Matriz) - (N_Habitat_Matriz - 1))
                End If
            Case "Leste"
                If ListaCampo(ListaCobra(0).GSIndexCampo).GSColumnNumber < N_Habitat_Matriz - 1 Then
                    ListaCobra(0).GSIndexCampo += 1
                Else
                    ListaCobra(0).GSIndexCampo -= N_Habitat_Matriz - 1
                End If
            Case "Oeste"
                If ListaCampo(ListaCobra(0).GSIndexCampo).GSColumnNumber > 0 Then
                    ListaCobra(0).GSIndexCampo -= 1
                Else
                    ListaCobra(0).GSIndexCampo += N_Habitat_Matriz - 1
                End If
        End Select
        AtualizaLocationSnake()
        If Comendo() = True Then
            Exit Sub
        End If
        StartStopTimer()
        AtualizaInformativo()
        PB_Habitat.Refresh()
    End Sub

    Private Sub AtualizaInformativo()
        N_Snake_Velocidade = 71 - N_Relogio1_Tick
        Label1.Text = "Pontuação: " & vbCrLf & N_Game_PontuacaoGeral & vbCrLf & vbCrLf & "Nível: " & vbCrLf & N_Snake_Nivel & vbCrLf & vbCrLf & "Tamanho: " & vbCrLf & N_Snake_Tamanho & vbCrLf & vbCrLf &
                        "Regenerações: " & vbCrLf & N_Snake_Regeneracao & vbCrLf & vbCrLf & "Velocidade: " & vbCrLf & Format(N_Snake_Velocidade, "0.00") & "Km/h" & vbCrLf & vbCrLf & "Tempo: " & vbCrLf & S_Game_TimerGeneral &
            vbCrLf & vbCrLf & "Direção atual:" & vbCrLf & ListaCobra(0).GSDirecao & vbCrLf & vbCrLf & "Blocos percorridos:" & vbCrLf & N_Snake_PontosPercorridos

        If B_Game_Dead = False Then
            If B_Game_AutorizaMovimento = False Then
                Label2.ForeColor = Color.White
                Label2.Text = "Ao definir seu habitat clique em OK para autorizar movimentos do jogo"
            ElseIf B_Game_AutorizaMovimento = True And B_Game_JogoIniciado = False Then
                Label2.ForeColor = Color.LimeGreen
                Label2.Text = "Pressione Norte(↑) ou Sul(↓) para iniciar"
            ElseIf B_Game_AutorizaMovimento = True And B_Game_JogoIniciado = True And B_Game_Pause = False Then
                Label2.ForeColor = Color.White
                Label2.Text = "Para pausar o jogo pressione a mesma direção que já está indo ou R para resetar o jogo"
            ElseIf B_Game_AutorizaMovimento = True And B_Game_JogoIniciado = True And B_Game_Pause = True Then
                Select Case ListaCobra(0).GSDirecao
                    Case "Norte"
                        Label2.ForeColor = Color.DeepSkyBlue
                        Label2.Text = "PAUSE: Pressione Oeste ou Leste para continuar"
                    Case "Sul"
                        Label2.ForeColor = Color.DeepSkyBlue
                        Label2.Text = "PAUSE: Pressione Oeste ou Leste para continuar"
                    Case Else
                        Label2.ForeColor = Color.DeepSkyBlue
                        Label2.Text = "PAUSE: Pressione Norte ou Sul para continuar"
                End Select
            End If
        Else
            Label2.ForeColor = Color.Red
            Label2.Text = "GAME OVER - pressione R para resetar o jogo"
        End If
    End Sub

    Private Sub AtualizaLocationSnake()
        For k As Integer = 1 To ListaCobra.Count - 1
            ListaCobra(ListaCobra.Count - k).GSLocation = ListaCobra(ListaCobra.Count - (k + 1)).GSLocation
        Next
        ListaCobra(0).GSLocation = ListaCampo(ListaCobra(0).GSIndexCampo).GSLocation
    End Sub

    Private Sub StartStopTimer()
        If T_Game_Timer(0).Enabled = False And B_Game_Dead = False Then
            B_Game_Pause = False
            T_Game_Timer(0).Start()
        End If
    End Sub

    Private Function Comendo() As Boolean
        If ComeuProprioCorpo(ListaCobra(0).GSIndexCampo) = False Then
            If ListaCobra(0).GSLocation = P_Food_Ponto Then
                ListaCobra.Add(New Snake(ListaCobra(ListaCobra.Count - 1).GSIndexCampo, ListaCobra(ListaCobra.Count - 1).GSLocation, ListaHabitat(ComboBox1.SelectedIndex).GSColorHeadSnake, ListaHabitat(ComboBox1.SelectedIndex).GSColorBodySnake, "Leste"))
                P_Food_Ponto = GeraComida()
                If N_Relogio1_Tick > 1 Then
                    N_Relogio1_Tick -= 0.15
                    T_Game_Timer(0).Interval = N_Relogio1_Tick
                End If
                N_Snake_Tamanho += 1
                N_Snake_Nivel += 1
            End If
            N_Game_PontuacaoGeral = CalculaPontuacao(N_Snake_Nivel, N_Snake_PontosPercorridos, N_Snake_Tamanho)
        Else
            For k As Integer = 1 To N_Snake_Canibal
                ListaCobra.RemoveAt(ListaCobra.Count - 1)
                N_Snake_Tamanho -= 1
            Next
            N_Snake_Regeneracao -= 1
            If N_Snake_Regeneracao < 0 Then
                B_Game_Dead = True
                N_Snake_Regeneracao = 0
                T_Game_Timer(0).Enabled = False
                AtualizaInformativo()
                For k = 0 To ListaCobra.Count - 1
                    ListaCobra(k).GSColoracaoHead = Brushes.DarkRed
                    ListaCobra(k).GSColoracaoBody = Brushes.DarkRed
                    Refresh()
                    Sleep(60)
                Next
                For k = 0 To ListaCobra.Count - 1
                    ListaCobra(k).GSColoracaoHead = Brushes.WhiteSmoke
                    ListaCobra(k).GSColoracaoBody = Brushes.WhiteSmoke
                Next
                Refresh()
                AtualizaInformativo()
                Return True
            Else
                T_Game_Timer(1).Start()
            End If
        End If


        Return False
    End Function

    Private Function GeraComida()
        Dim Valor As Integer = R_Food_Sorteio.Next(0, ListaCampo.Count)
        Do While VerificaSeComidaNasceraNaSnake(Valor) = True
            Valor = R_Food_Sorteio.Next(0, ListaCampo.Count)
        Loop
        Return ListaCampo(Valor).GSLocation
    End Function

    Private Function VerificaSeComidaNasceraNaSnake(ByVal Valor As Integer) As Boolean
        For k = 0 To ListaCobra.Count - 1
            If Valor = ListaCobra(k).GSIndexCampo Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function ComeuProprioCorpo(ByVal Valor As Integer) As Boolean
        For k = 4 To ListaCobra.Count - 1
            If Valor = ListaCobra(k).GSIndexCampo Then
                N_Snake_Canibal = ListaCobra.Count - k
                Return True
            End If
        Next
        Return False
    End Function


    Private Sub Desenhando(sender As Object, e As PaintEventArgs) Handles PB_Habitat.Paint
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        For k = 0 To ListaCobra.Count - 1
            Dim CorpoCobra As New Rectangle(ListaCobra(k).GSLocation.X - 10, ListaCobra(k).GSLocation.Y - 10, 20, 20)
            If k <> 0 Then
                Dim HeadColor As Brush = ListaCobra(k).GSColoracaoHead
                e.Graphics.FillRectangle(HeadColor, CorpoCobra)
            Else
                Dim BodyColor As Brush = ListaCobra(k).GSColoracaoBody
                e.Graphics.FillRectangle(BodyColor, CorpoCobra)
            End If
        Next

        e.Graphics.FillEllipse(Brushes.Red, P_Food_Ponto.X - 9, P_Food_Ponto.Y - 9, 18, 18)
        If B_Game_Dead = True Then
            Dim mycolor As Color = ListaHabitat(ComboBox1.SelectedIndex).GSColorMap
            Dim mybrushes = New SolidBrush(mycolor)
            e.Graphics.FillEllipse(mybrushes, P_Food_Ponto.X - 9, P_Food_Ponto.Y - 9, 18, 18)
        End If

    End Sub

    Private Function CalculaPontuacao(ByVal FrutasComidas, ByVal PontosPercorridos, ByVal TamanhoSnake) As Integer
        If FrutasComidas = 0 Then
            Return 0
        End If
        Return (FrutasComidas / PontosPercorridos) * (TamanhoSnake * 500)

    End Function

    Private Sub Relogio1_IdentificaçãoDeColisãoComPróprioCorpo()
        N_Relogio2_Tempo += 1
        Dim IndexValue As Integer = ComboBox1.SelectedIndex
        If N_Relogio2_Tempo Mod 2 = 0 Then
            For k = 0 To ListaCobra.Count - 1
                ListaCobra(k).GSColoracaoHead = Brushes.White
                ListaCobra(k).GSColoracaoBody = Brushes.WhiteSmoke
            Next
        Else
            For k = 0 To ListaCobra.Count - 1
                ListaCobra(k).GSColoracaoHead = ListaHabitat(IndexValue).GSColorHeadSnake
                ListaCobra(k).GSColoracaoBody = ListaHabitat(IndexValue).GSColorBodySnake
            Next
        End If
        If N_Relogio2_Tempo = 12 Then
            For k = 0 To ListaCobra.Count - 1
                ListaCobra(k).GSColoracaoHead = ListaHabitat(IndexValue).GSColorHeadSnake
                ListaCobra(k).GSColoracaoBody = ListaHabitat(IndexValue).GSColorBodySnake
            Next
            N_Relogio2_Tempo = 0
            T_Game_Timer(1).Stop()
        End If
    End Sub

    Private Sub Relogio2_AtualizaTempoJogado()
        N_Game_TimerSecond += 1
        If N_Game_TimerSecond = 60 Then
            N_Game_TimerMinute += 1
            N_Game_TimerSecond = 0
        End If
        If N_Game_TimerSecond < 10 Then
            S_Game_TimerSecond = "0" & N_Game_TimerSecond
        Else
            S_Game_TimerSecond = N_Game_TimerSecond
        End If
        If N_Game_TimerMinute = 0 Then
            S_Game_TimerMinute = "00"
        ElseIf N_Game_TimerMinute < 10 Then
            S_Game_TimerMinute = "0" & N_Game_TimerMinute
        Else
            S_Game_TimerMinute = N_Game_TimerMinute
        End If
        S_Game_TimerGeneral = S_Game_TimerMinute & ":" & S_Game_TimerSecond
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        PB_Habitat.BackColor = ListaHabitat(ComboBox1.SelectedIndex).GSColorMap
        For k = 0 To ListaCobra.Count - 1
            ListaCobra(k).GSColoracaoHead = ListaHabitat(ComboBox1.SelectedIndex).GSColorHeadSnake
            ListaCobra(k).GSColoracaoBody = ListaHabitat(ComboBox1.SelectedIndex).GSColorBodySnake
        Next
        Refresh()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AutorizaMovimentoGame()
    End Sub

    Private Sub AutorizaMovimentoGame()
        If B_Game_AutorizaMovimento = False Then
            B_Game_AutorizaMovimento = True
            Button1.Enabled = False
            ComboBox1.Enabled = False
            AtualizaInformativo()
            Me.Focus()
        Else
            B_Game_AutorizaMovimento = False
            T_Game_Timer(0).Enabled = False
            Button1.Enabled = True
            ComboBox1.Enabled = True
            AtualizaInformativo()
            ComboBox1.Focus()
        End If
    End Sub
End Class

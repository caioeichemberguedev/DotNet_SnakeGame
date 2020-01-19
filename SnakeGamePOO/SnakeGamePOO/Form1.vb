Imports System.Drawing.Drawing2D
Imports System.Threading.Thread

Public Class Form1

    Private ListaCobra As ArrayList = New ArrayList
    Private ListaCampo As ArrayList = New ArrayList
    Private ListaHabitat As ArrayList = New ArrayList

    Private ListaDe4TeclasPressionadas As New List(Of String)
    Private ListaQuePegaDoArrayParaComparar As New List(Of String)
    Private ListaDeCheatCadastrado As ArrayList = New ArrayList

    Private N_Snake_Nivel As Integer
    Private N_Snake_TamanhoInicial As Integer
    Private N_Snake_Canibal As Integer
    Private N_Snake_Velocidade As Double
    Private N_Snake_Regeneracao As Integer
    Private N_Snake_PontosPercorridos As Integer
    Private B_Snake_Fantasma As Boolean = False

    Private N_Relogio0_Tick As Double
    Private N_Relogio1_TrocaCor As Integer
    Private N_Game_TimerSecond As Integer
    Private S_Game_TimerSecond As String
    Private N_Game_TimerMinute As Integer
    Private S_Game_TimerMinute As String
    Private S_Game_TimerGeneral As String
    Private T_Game_Timer() As Timer
    Private N_Game_PontuacaoGeral As Integer
    Private B_Game_Pause As Boolean
    Private B_Game_Dead As Boolean = False
    Private B_Game_Vitoria As Boolean = False

    Private N_Food_Ponto As Integer
    Private R_Food_Sorteio As New Random

    Private N_Habitat_Matriz As Integer = 30

    'PARA ESTATISTICA DE PORCENTAGEM DE ORIENTAÇÃO
    Private n_Game_GoToNorte As Integer
    Private n_Game_GoToSul As Integer
    Private n_Game_GoToLeste As Integer
    Private n_Game_GoToOeste As Integer
    'PARA ESTATISTICA DE PORCENTAGEM DE ORIENTAÇÃO



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListaDeCheatCadastrado.Add(New Cheats("Vida Infinita", "L", "I", "F", "E", False))
        ConfiguraTela()
        ConfiguraDimensões()
        PopulaEConfiguraHabitat()
        CriandoTimers()
        PreparandoGame()
    End Sub

    Private Sub ConfiguraTela()
        Me.KeyPreview = True
        Me.Text = "Snake Game"
        Me.CenterToScreen()
        Me.FormBorderStyle = FormBorderStyle.FixedToolWindow
    End Sub

    Private Sub CriandoTimers()
        For k = 0 To 2
            ReDim Preserve T_Game_Timer(k)
            T_Game_Timer(k) = New Timer
            With T_Game_Timer(k)
                .Enabled = False
            End With
        Next
        AddHandler T_Game_Timer(0).Tick, AddressOf Relogio0_RealizaAtualizaçõesDeMovimento
        AddHandler T_Game_Timer(1).Tick, AddressOf Relogio1_IdentificaçãoDeColisãoComPróprioCorpo
        AddHandler T_Game_Timer(2).Tick, AddressOf Relogio2_AtualizaTempoJogado
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

    Private Sub PreparandoGame()
        For k = 0 To 2
            T_Game_Timer(k).Enabled = False
        Next
        N_Snake_Regeneracao = 3
        N_Snake_Nivel = 0
        N_Snake_TamanhoInicial = 4
        N_Relogio0_Tick = 75
        T_Game_Timer(0).Interval = N_Relogio0_Tick 'TEMPO INICIAL DO JOGO PARA INTERVALO DE MOVIMENTAÇÃO DO CORPO DA COBRA
        T_Game_Timer(1).Interval = 120 'INTERVALO DE TEMPO EM QUE A COBRA MUDA DE COR, IDENTIFICANDO COLISÃO COM PROPRIO CORPO/VIDA ZERADA
        T_Game_Timer(2).Interval = 1000 'RELOGIO PARA TEMPO JOGADO
        N_Relogio1_TrocaCor = 12
        S_Game_TimerGeneral = "00:00"
        N_Game_TimerSecond = 0
        N_Game_TimerMinute = 0

        N_Snake_PontosPercorridos = 0
        N_Game_PontuacaoGeral = 0
        ListaCobra.Clear()
        B_Game_Dead = False
        B_Game_Vitoria = False
        ComboBox1.Enabled = True

        n_Game_GoToNorte = 0
        n_Game_GoToSul = 0
        n_Game_GoToLeste = 0
        n_Game_GoToOeste = 0

        For k = 0 To N_Snake_TamanhoInicial - 1
            Dim Valor1 As Integer = (N_Snake_TamanhoInicial - 1) - k
            ListaCobra.Add(New Snake(Valor1, ListaCampo(Valor1).GSLocation, ListaHabitat(ComboBox1.SelectedIndex).GSColorHeadSnake, ListaHabitat(ComboBox1.SelectedIndex).GSColorBodySnake, "Leste"))
        Next
        N_Food_Ponto = GeraComida()

        AtualizaInformativo()
        AtualizaEstatisticas()
        Refresh()
    End Sub

    Private Function TotalDeCampoPossiveis(ByVal Valor As Integer) As Integer
        Return (Valor ^ 2) - 1
    End Function


    Private Sub Direcionamento_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If B_Game_Dead = False And T_Game_Timer(0).Enabled = True Then
            Select Case e.KeyCode
                Case Keys.Up
                    If ListaCobra(0).GSDirecao <> "Sul" And ListaCobra(0).GSDirecao <> "Norte" Then
                        ListaCobra(0).GSDirecao = "Norte"
                        n_Game_GoToNorte += 1
                        AtualizaSnakeGeral()
                        Exit Sub
                    End If
                Case Keys.Down
                    If ListaCobra(0).GSDirecao <> "Norte" And ListaCobra(0).GSDirecao <> "Sul" Then
                        ListaCobra(0).GSDirecao = "Sul"
                        n_Game_GoToSul += 1
                        AtualizaSnakeGeral()
                        Exit Sub
                    End If
                Case Keys.Right
                    If ListaCobra(0).GSDirecao <> "Oeste" And ListaCobra(0).GSDirecao <> "Leste" Then
                        ListaCobra(0).GSDirecao = "Leste"
                        n_Game_GoToLeste += 1
                        AtualizaSnakeGeral()
                        Exit Sub
                    End If
                Case Keys.Left
                    If ListaCobra(0).GSDirecao <> "Leste" And ListaCobra(0).GSDirecao <> "Oeste" Then
                        ListaCobra(0).GSDirecao = "Oeste"
                        n_Game_GoToOeste += 1
                        AtualizaSnakeGeral()
                        Exit Sub
                    End If
            End Select
        End If

        Select Case e.KeyCode
            Case Keys.Enter
                If B_Game_Dead = False Then
                    StartGame()
                End If
            Case Keys.R
                PreparandoGame()
            Case Keys.Escape
                T_Game_Timer(0).Stop()
                If MessageBox.Show("Deseja mesmo sair do jogo?", "Message", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Me.Close()
                Else
                    T_Game_Timer(0).Start()
                End If
            Case Else
                ListaDe4TeclasPressionadas.Add(e.KeyCode.ToString)
                If ListaDe4TeclasPressionadas.Count = 5 Then
                    ListaDe4TeclasPressionadas.RemoveAt(0)
                End If
                If ListaDeCheatCadastrado.Count > 0 Then
                    For k = 0 To ListaDeCheatCadastrado.Count - 1
                        ListaQuePegaDoArrayParaComparar.Clear()
                        ListaQuePegaDoArrayParaComparar.Add(ListaDeCheatCadastrado(k).GSTecla1)
                        ListaQuePegaDoArrayParaComparar.Add(ListaDeCheatCadastrado(k).GSTecla2)
                        ListaQuePegaDoArrayParaComparar.Add(ListaDeCheatCadastrado(k).GSTecla3)
                        ListaQuePegaDoArrayParaComparar.Add(ListaDeCheatCadastrado(k).GSTecla4)

                        If Verifica() = True Then
                            If ListaDeCheatCadastrado(k).GSAtivado = False Then
                                ListaDeCheatCadastrado(k).GSAtivado = True
                                Label2.Text = ListaDeCheatCadastrado(k).GSNomeCheat & ": Código Ativado!"
                                Label2.ForeColor = Color.LimeGreen
                            Else
                                ListaDeCheatCadastrado(k).GSAtivado = False
                                Label2.Text = ListaDeCheatCadastrado(k).GSNomeCheat & ": Código Desativado!"
                                Label2.ForeColor = Color.Red
                            End If
                            If k = 0 Then
                                B_Snake_Fantasma = ListaDeCheatCadastrado(k).GSAtivado
                            End If
                            Exit For
                        End If
                    Next
                End If
        End Select
    End Sub

    Private Function Verifica() As Boolean
        If ListaQuePegaDoArrayParaComparar.SequenceEqual(ListaDe4TeclasPressionadas) Then
            Return True
        End If
        Return False
    End Function

    Private Sub Relogio0_RealizaAtualizaçõesDeMovimento()
        AtualizaSnakeGeral()
    End Sub

    Private Sub AtualizaSnakeGeral()
        'sem esse stop abaixo a snake aparenta duplicar a quantidade de campos percorridos dando a impressao de que esta deslizando, pois o timer e as setas atualizam sua direção!
        T_Game_Timer(0).Stop()
        N_Snake_PontosPercorridos += 1

        AtualizaIndexBodySnake()
        MovimentoHeadSnake()
        AtualizaPositionsBodySnake()

        If ColisãoGeraFimDeJogo(ListaCobra(0).GSIndexCampo) = True Then
            If B_Game_Dead = False Then
                AtualizaIndexBodySnake()
                MovimentoHeadSnake()
                AtualizaPositionsBodySnake()
                AtualizaInformativo()
            End If
        Else
            T_Game_Timer(0).Start()
        End If
        AtualizaEstatisticas()
        PB_Habitat.Refresh()
    End Sub

    Private Sub AtualizaIndexBodySnake()
        For k As Integer = 1 To ListaCobra.Count - 1
            ListaCobra(ListaCobra.Count - k).GSIndexCampo = ListaCobra(ListaCobra.Count - (k + 1)).GSIndexCampo
        Next
    End Sub

    Private Sub MovimentoHeadSnake()
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

        'para testar vitória comente a rotina acima e descomente a rotina abaixo e inicie o tamanho da snake com uns 880 para facilitar e diminua o intervalo de tempo para o minimo(1) se quiser.
        'If ListaCobra(0).GSIndexCampo = 899 Then
        '    ListaCobra(0).GSIndexCampo = 0
        'Else
        '    ListaCobra(0).GSIndexCampo += 1
        'End If
    End Sub

    Private Sub AtualizaPositionsBodySnake()
        For k = 1 To ListaCobra.Count - 1
            ListaCobra(ListaCobra.Count - k).GSLocation = ListaCobra(ListaCobra.Count - (k + 1)).GSLocation
        Next
        ListaCobra(0).GSLocation = ListaCampo(ListaCobra(0).GSIndexCampo).GSLocation
    End Sub

    Private Function ColisãoGeraFimDeJogo(ByVal ValorIndexHead As Integer) As Boolean

        If ComeuProprioCorpo(ValorIndexHead) = True Then
            For k = 1 To N_Snake_Canibal
                ListaCobra.RemoveAt(ListaCobra.Count - 1)
            Next
            If VerificaSeGastouUltimaVida(N_Snake_Regeneracao) = True Then Return True
            T_Game_Timer(1).Start()
        Else
            If ComeuComida(ValorIndexHead) = True Then
                N_Snake_Nivel += 1
                N_Game_PontuacaoGeral = CalculaPontuacao(N_Snake_Nivel, N_Snake_PontosPercorridos, ListaCobra.Count)
                ListaCobra.Add(New Snake(ListaCobra(ListaCobra.Count - 1).GSIndexCampo, ListaCobra(ListaCobra.Count - 1).GSLocation, ListaHabitat(ComboBox1.SelectedIndex).GSColorHeadSnake, ListaHabitat(ComboBox1.SelectedIndex).GSColorBodySnake, "Leste"))
                If VerificaSeGanhouJogo(ListaCobra.Count) = True Then
                    N_Food_Ponto = 900
                    Return True
                Else
                    AumentaVelocidadeDoJogo()
                    N_Food_Ponto = GeraComida()
                End If
            End If
        End If

        Return False
    End Function

    Private Function ComeuProprioCorpo(ByVal Valor As Integer) As Boolean
        If B_Snake_Fantasma = False Then
            For k = 4 To ListaCobra.Count - 1
                If Valor = ListaCobra(k).GSIndexCampo Then
                    N_Snake_Canibal = ListaCobra.Count - k
                    N_Snake_Regeneracao -= 1
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Private Function VerificaSeGastouUltimaVida(ByVal Regeneração As Integer) As Boolean
        If Regeneração < 0 Then
            B_Game_Dead = True
            N_Snake_Regeneracao = 0
            T_Game_Timer(1).Start()
            AtualizaInformativo()
            Return True
        End If
        Return False
    End Function

    Private Function ComeuComida(ByVal ValorIndexHead As Integer) As Boolean
        If ValorIndexHead = N_Food_Ponto Then
            AtualizaInformativo()
            Return True
        End If
        Return False
    End Function

    Private Function GeraComida() As Integer
        Dim Valor As Integer = R_Food_Sorteio.Next(0, ListaCampo.Count)
        Do While VerificaSeComidaNasceraNaSnake(Valor) = True
            Valor = R_Food_Sorteio.Next(0, ListaCampo.Count)
        Loop
        Return Valor
    End Function

    Private Function VerificaSeComidaNasceraNaSnake(ByVal Valor As Integer) As Boolean
        For k = 0 To ListaCobra.Count - 1
            If Valor = ListaCobra(k).GSIndexCampo Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function VerificaSeGanhouJogo(ByVal TamanhoDaSnake As Integer) As Boolean
        If TamanhoDaSnake = TotalDeCampoPossiveis(N_Habitat_Matriz) - 39 Then
            'A Snake não preenche todo o espaço para ganhar pois deixaria a geração de novos foods com delay muito perceptivel
            B_Game_Vitoria = True
            Return True
        End If
        Return False
    End Function

    Private Sub AumentaVelocidadeDoJogo()
        If N_Relogio0_Tick > 50 Then
            N_Relogio0_Tick -= 0.2
            T_Game_Timer(0).Interval = N_Relogio0_Tick
        End If
    End Sub

    Private Function CalculaPontuacao(ByVal FrutasComidas, ByVal PontosPercorridos, ByVal TamanhoSnake) As Integer
        If FrutasComidas = 0 Then
            Return 0
        End If
        Return (FrutasComidas / PontosPercorridos) * (TamanhoSnake * 500)
    End Function

    Private Sub AtualizaInformativo()
        If B_Game_Dead = False Then
            If T_Game_Timer(0).Enabled = False And N_Snake_PontosPercorridos = 0 Then
                Label2.ForeColor = Color.White
                Label2.Text = "Ao definir o habitat pressione [ENTER] para iniciar"
            ElseIf B_Game_Pause = False And B_Game_Vitoria = False Then
                Label2.ForeColor = Color.DeepSkyBlue
                Label2.Text = "Pressione [ENTER] para Pausar OU [R] para resetar"
            ElseIf T_Game_Timer(0).Enabled = False And B_Game_Pause = True Then
                Label2.ForeColor = Color.LimeGreen
                Label2.Text = "Pressione [ENTER] para Retornar"
            ElseIf T_Game_Timer(0).Enabled = False And B_Game_Vitoria = True Then
                Label2.ForeColor = Color.Yellow
                Label2.Text = "Você Ganhou!!! - Pressione [R] para resetar"
            End If
        Else
            Label2.ForeColor = Color.Red
            Label2.Text = "GAME OVER - pressione [R] para resetar"
        End If
    End Sub

    Private Sub AtualizaEstatisticas()
        N_Snake_Velocidade = 1000 / N_Relogio0_Tick
        Label1.Text = "Tamanho: " & ListaCobra.Count & vbCrLf & vbCrLf &
                      "Pontuação: " & N_Game_PontuacaoGeral & vbCrLf & vbCrLf &
                      "Regenerações: " & N_Snake_Regeneracao & vbCrLf & vbCrLf &
                      "Tempo: " & S_Game_TimerGeneral & vbCrLf & vbCrLf &
                      "Velocidade: " & vbCrLf &
                                         Format(N_Snake_Velocidade, "0.00") & " Blocos/s" & vbCrLf & vbCrLf &
                      "Direção atual: " & ListaCobra(0).GSDirecao
    End Sub


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

        If B_Game_Vitoria = False And B_Game_Dead = False Then
            e.Graphics.FillEllipse(Brushes.Red, ListaCampo(N_Food_Ponto).GSLocation.X - 9, ListaCampo(N_Food_Ponto).GSLocation.Y - 9, 18, 18)
        End If

    End Sub


    Private Sub Relogio1_IdentificaçãoDeColisãoComPróprioCorpo()
        PB_Habitat.Refresh()
        Dim IndexValue As Integer = ComboBox1.SelectedIndex
        If N_Relogio1_TrocaCor Mod 2 = 0 Then
            If B_Game_Dead = False Then
                For k = 0 To ListaCobra.Count - 1
                    ListaCobra(k).GSColoracaoHead = Brushes.White
                    ListaCobra(k).GSColoracaoBody = Brushes.WhiteSmoke
                Next
            Else
                For k = 0 To ListaCobra.Count - 1
                    ListaCobra(k).GSColoracaoHead = Brushes.DarkRed
                    ListaCobra(k).GSColoracaoBody = Brushes.Red
                Next
            End If
        Else
            For k = 0 To ListaCobra.Count - 1
                ListaCobra(k).GSColoracaoHead = ListaHabitat(IndexValue).GSColorHeadSnake
                ListaCobra(k).GSColoracaoBody = ListaHabitat(IndexValue).GSColorBodySnake
            Next
        End If
        N_Relogio1_TrocaCor -= 1

        If N_Relogio1_TrocaCor = 0 Then
            For k = 0 To ListaCobra.Count - 1
                ListaCobra(k).GSColoracaoHead = ListaHabitat(IndexValue).GSColorHeadSnake
                ListaCobra(k).GSColoracaoBody = ListaHabitat(IndexValue).GSColorBodySnake
            Next
            N_Relogio1_TrocaCor = 12
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

    Private Sub StartGame()
        ComboBox1.Enabled = False
        Cursor.Position = New Point(0, 0)
        If T_Game_Timer(0).Enabled = True Then
            T_Game_Timer(0).Stop()
            T_Game_Timer(2).Stop()
            B_Game_Pause = True
        Else
            T_Game_Timer(0).Start()
            T_Game_Timer(2).Start()
            B_Game_Pause = False
        End If
        AtualizaInformativo()
    End Sub

    Private Sub RecordsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecordsToolStripMenuItem.Click
        Dim StatusCodigo As String
        If ListaDeCheatCadastrado(0).GSAtivado = False Then
            StatusCodigo = "Desativado"
        Else
            StatusCodigo = "Ativado"
        End If
        MessageBox.Show("Vida Infinita= LIFE, Situação: " & StatusCodigo, "Cheats, digite o código a qualquer momento para Ativar/Desativar")
    End Sub

    Private Sub EstatisticasParaNerdsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EstatisticasParaNerdsToolStripMenuItem.Click
        Dim totaldeorientaçoes As Integer = n_Game_GoToNorte + n_Game_GoToSul + n_Game_GoToLeste + n_Game_GoToOeste
        Dim porcN As Double = (n_Game_GoToNorte / totaldeorientaçoes) * 100
        Dim porcS As Double = (n_Game_GoToSul / totaldeorientaçoes) * 100
        Dim porcL As Double = (n_Game_GoToLeste / totaldeorientaçoes) * 100
        Dim porcO As Double = (n_Game_GoToOeste / totaldeorientaçoes) * 100

        MessageBox.Show("Food Point: " & vbCrLf & N_Food_Ponto & vbCrLf & vbCrLf &
                        "Head Point: " & vbCrLf & ListaCobra(0).GSIndexCampo & vbCrLf & vbCrLf &
                        "Maçãs comidas: " & vbCrLf & N_Snake_Nivel & vbCrLf & vbCrLf &
                        "Blocos percorridos: " & vbCrLf & N_Snake_PontosPercorridos & vbCrLf & vbCrLf &
                        "Porcentagem de mudança na orientação:" & vbCrLf &
                                    "N = " & Format(porcN, "0.00") & "%" & vbCrLf &
                                    "S = " & Format(porcS, "0.00") & "%" & vbCrLf &
                                    "L = " & Format(porcL, "0.00") & "%" & vbCrLf &
                                    "O = " & Format(porcO, "0.00") & "%", "Informações adicionais do jogo atual")
    End Sub

    Private Sub InfosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfosToolStripMenuItem.Click
        MessageBox.Show("O jogo é ganho ao chegar ao tamanho 860!" & vbCrLf & "Ao todo são 900 campos possiveis de preenchimento.", "Informações adicionais", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class

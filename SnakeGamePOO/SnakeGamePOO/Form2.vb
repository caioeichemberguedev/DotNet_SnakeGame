Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()

        Dim TamanhoParaVitoria As Integer = (Form1.TotalDeCampoPossiveis(Form1.N_Game_matrix) + 1) * 0.7

        Label1.Text = "Dimensões definidas em " & Form1.N_Game_Dimensao & " pixels, Para ganhar o jogo a" & vbCrLf &
                        "snake deverá chegar no tamanho de " & TamanhoParaVitoria & " (70% do habitat), " & vbCrLf &
                        "100% é igual a " & Form1.TotalDeCampoPossiveis(Form1.N_Game_matrix) + 1 & " blocos possíveis para percorrer!" & vbCrLf & vbCrLf &
                        "Esta porcentagem foi definida para melhor fluidez do jogo."


        If Form1.B_VisualizaMensagem = False Then
            CheckBox1.CheckState = CheckState.Checked
        End If
        Form1.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.CheckState = CheckState.Checked Then
            Form1.B_VisualizaMensagem = False
        Else
            Form1.B_VisualizaMensagem = True
        End If
        Form1.Enabled = True
        Me.Close()
    End Sub

    Private Sub Form2_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Form1.Enabled = True
    End Sub
End Class
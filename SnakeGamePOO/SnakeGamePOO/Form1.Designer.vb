<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PB_Habitat = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label0 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EstatisticasParaNerdsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheatsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComoGanharToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpçõesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PB_Habitat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PB_Habitat
        '
        Me.PB_Habitat.BackColor = System.Drawing.Color.Black
        Me.PB_Habitat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PB_Habitat.Location = New System.Drawing.Point(12, 28)
        Me.PB_Habitat.Name = "PB_Habitat"
        Me.PB_Habitat.Size = New System.Drawing.Size(600, 600)
        Me.PB_Habitat.TabIndex = 1
        Me.PB_Habitat.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(618, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 18)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label1"
        '
        'Label0
        '
        Me.Label0.AutoSize = True
        Me.Label0.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label0.Location = New System.Drawing.Point(618, 28)
        Me.Label0.Name = "Label0"
        Me.Label0.Size = New System.Drawing.Size(143, 18)
        Me.Label0.TabIndex = 3
        Me.Label0.Text = "Dimensões do Jogo"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Black
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(0, 639)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(784, 26)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Label2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(621, 49)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(149, 26)
        Me.ComboBox1.TabIndex = 10
        Me.ComboBox1.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EstatisticasParaNerdsToolStripMenuItem, Me.CheatsToolStripMenuItem, Me.InfosToolStripMenuItem, Me.OpçõesToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip1.TabIndex = 12
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EstatisticasParaNerdsToolStripMenuItem
        '
        Me.EstatisticasParaNerdsToolStripMenuItem.Checked = True
        Me.EstatisticasParaNerdsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EstatisticasParaNerdsToolStripMenuItem.Name = "EstatisticasParaNerdsToolStripMenuItem"
        Me.EstatisticasParaNerdsToolStripMenuItem.Size = New System.Drawing.Size(114, 20)
        Me.EstatisticasParaNerdsToolStripMenuItem.Text = "Valores para nerds"
        '
        'CheatsToolStripMenuItem
        '
        Me.CheatsToolStripMenuItem.Name = "CheatsToolStripMenuItem"
        Me.CheatsToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.CheatsToolStripMenuItem.Text = "Cheats"
        '
        'InfosToolStripMenuItem
        '
        Me.InfosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ComoGanharToolStripMenuItem})
        Me.InfosToolStripMenuItem.Name = "InfosToolStripMenuItem"
        Me.InfosToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.InfosToolStripMenuItem.Text = "Mensagens"
        '
        'ComoGanharToolStripMenuItem
        '
        Me.ComoGanharToolStripMenuItem.Name = "ComoGanharToolStripMenuItem"
        Me.ComoGanharToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.ComoGanharToolStripMenuItem.Text = "Como ganhar"
        '
        'OpçõesToolStripMenuItem
        '
        Me.OpçõesToolStripMenuItem.Name = "OpçõesToolStripMenuItem"
        Me.OpçõesToolStripMenuItem.Size = New System.Drawing.Size(49, 20)
        Me.OpçõesToolStripMenuItem.Text = "Sobre"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 665)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label0)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PB_Habitat)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PB_Habitat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PB_Habitat As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label0 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents CheatsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EstatisticasParaNerdsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InfosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpçõesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ComoGanharToolStripMenuItem As ToolStripMenuItem
End Class

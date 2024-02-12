namespace ControlDeHorasCanellaRV
{
    partial class Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            menuStrip1 = new MenuStrip();
            guardarToolStripMenuItem = new ToolStripMenuItem();
            GenerarToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            label3 = new Label();
            txt_HorasRestantes = new TextBox();
            txt_Descripcion = new TextBox();
            lbl_Descripcion = new Label();
            label2 = new Label();
            txt_TotalHorasSemana = new TextBox();
            label1 = new Label();
            txt_TotalHorasDia = new TextBox();
            cb_TipoActividad = new ComboBox();
            cb_Etapa = new ComboBox();
            cb_Proyecto = new ComboBox();
            lbl_TipoActividad = new Label();
            lbl_Etapa = new Label();
            lbl_Proyecto = new Label();
            lbl_CantidadHoras = new Label();
            txt_CantidadHoras = new TextBox();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ControlLight;
            menuStrip1.Dock = DockStyle.Left;
            menuStrip1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            menuStrip1.GripMargin = new Padding(2, 2, 0, 5);
            menuStrip1.Items.AddRange(new ToolStripItem[] { guardarToolStripMenuItem, GenerarToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(103, 230);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // guardarToolStripMenuItem
            // 
            guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            guardarToolStripMenuItem.Size = new Size(90, 19);
            guardarToolStripMenuItem.Text = "Guardar";
            guardarToolStripMenuItem.Click += guardarToolStripMenuItem_Click;
            // 
            // GenerarToolStripMenuItem
            // 
            GenerarToolStripMenuItem.Name = "GenerarToolStripMenuItem";
            GenerarToolStripMenuItem.Size = new Size(90, 19);
            GenerarToolStripMenuItem.Text = "Generar Excel";
            GenerarToolStripMenuItem.Click += GenerarToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txt_HorasRestantes);
            panel1.Controls.Add(txt_Descripcion);
            panel1.Controls.Add(lbl_Descripcion);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txt_TotalHorasSemana);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txt_TotalHorasDia);
            panel1.Controls.Add(cb_TipoActividad);
            panel1.Controls.Add(cb_Etapa);
            panel1.Controls.Add(cb_Proyecto);
            panel1.Controls.Add(lbl_TipoActividad);
            panel1.Controls.Add(lbl_Etapa);
            panel1.Controls.Add(lbl_Proyecto);
            panel1.Controls.Add(lbl_CantidadHoras);
            panel1.Controls.Add(txt_CantidadHoras);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(103, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(986, 230);
            panel1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(638, 59);
            label3.Name = "label3";
            label3.Size = new Size(91, 15);
            label3.TabIndex = 15;
            label3.Text = "Horas Restantes";
            // 
            // txt_HorasRestantes
            // 
            txt_HorasRestantes.Enabled = false;
            txt_HorasRestantes.Location = new Point(744, 54);
            txt_HorasRestantes.Name = "txt_HorasRestantes";
            txt_HorasRestantes.Size = new Size(69, 23);
            txt_HorasRestantes.TabIndex = 14;
            // 
            // txt_Descripcion
            // 
            txt_Descripcion.Location = new Point(552, 116);
            txt_Descripcion.Multiline = true;
            txt_Descripcion.Name = "txt_Descripcion";
            txt_Descripcion.Size = new Size(375, 81);
            txt_Descripcion.TabIndex = 13;
            // 
            // lbl_Descripcion
            // 
            lbl_Descripcion.AutoSize = true;
            lbl_Descripcion.Location = new Point(696, 98);
            lbl_Descripcion.Name = "lbl_Descripcion";
            lbl_Descripcion.Size = new Size(69, 15);
            lbl_Descripcion.TabIndex = 12;
            lbl_Descripcion.Text = "Descripcion";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(725, 23);
            label2.Name = "label2";
            label2.Size = new Size(117, 15);
            label2.TabIndex = 11;
            label2.Text = "Cantidad Total Horas";
            // 
            // txt_TotalHorasSemana
            // 
            txt_TotalHorasSemana.Enabled = false;
            txt_TotalHorasSemana.Location = new Point(861, 20);
            txt_TotalHorasSemana.Name = "txt_TotalHorasSemana";
            txt_TotalHorasSemana.Size = new Size(69, 23);
            txt_TotalHorasSemana.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(502, 23);
            label1.Name = "label1";
            label1.Size = new Size(130, 15);
            label1.TabIndex = 9;
            label1.Text = "Cantidad de Horas Hoy";
            // 
            // txt_TotalHorasDia
            // 
            txt_TotalHorasDia.Enabled = false;
            txt_TotalHorasDia.Location = new Point(638, 20);
            txt_TotalHorasDia.Name = "txt_TotalHorasDia";
            txt_TotalHorasDia.Size = new Size(69, 23);
            txt_TotalHorasDia.TabIndex = 8;
            // 
            // cb_TipoActividad
            // 
            cb_TipoActividad.FormattingEnabled = true;
            cb_TipoActividad.Location = new Point(132, 135);
            cb_TipoActividad.Name = "cb_TipoActividad";
            cb_TipoActividad.Size = new Size(360, 23);
            cb_TipoActividad.TabIndex = 7;
            // 
            // cb_Etapa
            // 
            cb_Etapa.FormattingEnabled = true;
            cb_Etapa.Location = new Point(132, 98);
            cb_Etapa.Name = "cb_Etapa";
            cb_Etapa.Size = new Size(360, 23);
            cb_Etapa.TabIndex = 6;
            // 
            // cb_Proyecto
            // 
            cb_Proyecto.FormattingEnabled = true;
            cb_Proyecto.Location = new Point(132, 59);
            cb_Proyecto.Name = "cb_Proyecto";
            cb_Proyecto.Size = new Size(360, 23);
            cb_Proyecto.TabIndex = 5;
            cb_Proyecto.SelectedIndexChanged += cb_Proyecto_SelectedIndexChanged;
            // 
            // lbl_TipoActividad
            // 
            lbl_TipoActividad.AutoSize = true;
            lbl_TipoActividad.Location = new Point(21, 138);
            lbl_TipoActividad.Name = "lbl_TipoActividad";
            lbl_TipoActividad.Size = new Size(99, 15);
            lbl_TipoActividad.TabIndex = 4;
            lbl_TipoActividad.Text = "Tipo de Actividad";
            // 
            // lbl_Etapa
            // 
            lbl_Etapa.AutoSize = true;
            lbl_Etapa.Location = new Point(21, 106);
            lbl_Etapa.Name = "lbl_Etapa";
            lbl_Etapa.Size = new Size(36, 15);
            lbl_Etapa.TabIndex = 3;
            lbl_Etapa.Text = "Etapa";
            // 
            // lbl_Proyecto
            // 
            lbl_Proyecto.AutoSize = true;
            lbl_Proyecto.Location = new Point(21, 67);
            lbl_Proyecto.Name = "lbl_Proyecto";
            lbl_Proyecto.Size = new Size(54, 15);
            lbl_Proyecto.TabIndex = 2;
            lbl_Proyecto.Text = "Proyecto";
            // 
            // lbl_CantidadHoras
            // 
            lbl_CantidadHoras.AutoSize = true;
            lbl_CantidadHoras.Location = new Point(21, 26);
            lbl_CantidadHoras.Name = "lbl_CantidadHoras";
            lbl_CantidadHoras.Size = new Size(105, 15);
            lbl_CantidadHoras.TabIndex = 1;
            lbl_CantidadHoras.Text = "Cantidad de Horas";
            // 
            // txt_CantidadHoras
            // 
            txt_CantidadHoras.Location = new Point(132, 23);
            txt_CantidadHoras.Name = "txt_CantidadHoras";
            txt_CantidadHoras.Size = new Size(69, 23);
            txt_CantidadHoras.TabIndex = 0;
            txt_CantidadHoras.TextChanged += txt_CantidadHoras_TextChanged;
            // 
            // Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1089, 230);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Principal";
            Text = "Generador de Control de Horas RV";
            Load += Principal_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem guardarToolStripMenuItem;
        private ToolStripMenuItem GenerarToolStripMenuItem;
        private Panel panel1;
        private Label lbl_CantidadHoras;
        private TextBox txt_CantidadHoras;
        private ComboBox cb_TipoActividad;
        private ComboBox cb_Etapa;
        private ComboBox cb_Proyecto;
        private Label lbl_TipoActividad;
        private Label lbl_Etapa;
        private Label lbl_Proyecto;
        private Label label1;
        private TextBox txt_TotalHorasDia;
        private Label label2;
        private TextBox txt_TotalHorasSemana;
        private TextBox txt_Descripcion;
        private Label lbl_Descripcion;
        private Label label3;
        private TextBox txt_HorasRestantes;
    }
}
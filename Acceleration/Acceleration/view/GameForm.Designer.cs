namespace Acceleration
{
    partial class GameForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._newGameStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._pauseOrResumeStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._saveStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._loadStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._exit = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newGameStrip,
            this._pauseOrResumeStrip,
            this._saveStrip,
            this._loadStrip,
            this._exit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(582, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // _newGameStrip
            // 
            this._newGameStrip.Name = "_newGameStrip";
            this._newGameStrip.Size = new System.Drawing.Size(73, 24);
            this._newGameStrip.Text = "Új játék";
            // 
            // _pauseOrResumeStrip
            // 
            this._pauseOrResumeStrip.Name = "_pauseOrResumeStrip";
            this._pauseOrResumeStrip.Size = new System.Drawing.Size(111, 24);
            this._pauseOrResumeStrip.Text = "Szüneteltetés";
            // 
            // _saveStrip
            // 
            this._saveStrip.Name = "_saveStrip";
            this._saveStrip.Size = new System.Drawing.Size(71, 24);
            this._saveStrip.Text = "Mentés";
            // 
            // _loadStrip
            // 
            this._loadStrip.Name = "_loadStrip";
            this._loadStrip.Size = new System.Drawing.Size(77, 24);
            this._loadStrip.Text = "Betöltés";
            // 
            // _exit
            // 
            this._exit.Name = "_exit";
            this._exit.Size = new System.Drawing.Size(71, 24);
            this._exit.Text = "Kilépés";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.statusStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 825);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // statusStrip2
            // 
            this.statusStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel8,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel6});
            this.statusStrip2.Location = new System.Drawing.Point(0, 799);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(582, 26);
            this.statusStrip2.TabIndex = 0;
            this.statusStrip2.Text = "statusStrip1";
            this.statusStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip2_ItemClicked);
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(34, 20);
            this.toolStripStatusLabel8.Text = "Idő:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 20);
            this.toolStripStatusLabel1.Text = "0:00:00";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(139, 20);
            this.toolStripStatusLabel5.Text = "Benzin mennyisége:";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(55, 20);
            this.toolStripStatusLabel2.Text = "0:00:00";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(127, 20);
            this.toolStripStatusLabel3.Text = "Benzinmennyiség:";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(0, 20);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Motoros adatok (*.stl)|*.stl";
            this.saveFileDialog1.Title = "Motoros játék mentése";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Motoros játék (*.stl)|*.stl";
            this.openFileDialog1.Title = "Motoros játék betöltése";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 853);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameForm";
            this.Text = "Form1";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _newGameStrip;
        private System.Windows.Forms.ToolStripMenuItem _pauseOrResumeStrip;
        private System.Windows.Forms.ToolStripMenuItem _saveStrip;
        private System.Windows.Forms.ToolStripMenuItem _loadStrip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem _exit;
    }
}


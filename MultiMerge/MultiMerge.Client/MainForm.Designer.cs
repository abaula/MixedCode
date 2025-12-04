namespace MultiMerge.Client
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnMerge = new System.Windows.Forms.Button();
            this.btnVersion2File = new System.Windows.Forms.Button();
            this.btnVersion1File = new System.Windows.Forms.Button();
            this.btnEthalonFile = new System.Windows.Forms.Button();
            this.txtVersion2File = new System.Windows.Forms.TextBox();
            this.txtVersion1File = new System.Windows.Forms.TextBox();
            this.txtEthalonFile = new System.Windows.Forms.TextBox();
            this.lblVersion2File = new System.Windows.Forms.Label();
            this.lblVersion1File = new System.Windows.Forms.Label();
            this.lblEthalonFile = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Transparent;
            this.panelTop.BackgroundImage = global::MultiMerge.Client.Properties.Resources.background;
            this.panelTop.Controls.Add(this.btnMerge);
            this.panelTop.Controls.Add(this.btnVersion2File);
            this.panelTop.Controls.Add(this.btnVersion1File);
            this.panelTop.Controls.Add(this.btnEthalonFile);
            this.panelTop.Controls.Add(this.txtVersion2File);
            this.panelTop.Controls.Add(this.txtVersion1File);
            this.panelTop.Controls.Add(this.txtEthalonFile);
            this.panelTop.Controls.Add(this.lblVersion2File);
            this.panelTop.Controls.Add(this.lblVersion1File);
            this.panelTop.Controls.Add(this.lblEthalonFile);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(739, 139);
            this.panelTop.TabIndex = 0;
            // 
            // btnMerge
            // 
            this.btnMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMerge.Location = new System.Drawing.Point(572, 101);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(116, 23);
            this.btnMerge.TabIndex = 9;
            this.btnMerge.Text = "Выполнить слияние";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // btnVersion2File
            // 
            this.btnVersion2File.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVersion2File.Location = new System.Drawing.Point(694, 64);
            this.btnVersion2File.Name = "btnVersion2File";
            this.btnVersion2File.Size = new System.Drawing.Size(33, 20);
            this.btnVersion2File.TabIndex = 8;
            this.btnVersion2File.Text = "...";
            this.btnVersion2File.UseVisualStyleBackColor = true;
            this.btnVersion2File.Click += new System.EventHandler(this.btnVersion2File_Click);
            // 
            // btnVersion1File
            // 
            this.btnVersion1File.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVersion1File.Location = new System.Drawing.Point(694, 37);
            this.btnVersion1File.Name = "btnVersion1File";
            this.btnVersion1File.Size = new System.Drawing.Size(33, 20);
            this.btnVersion1File.TabIndex = 7;
            this.btnVersion1File.Text = "...";
            this.btnVersion1File.UseVisualStyleBackColor = true;
            this.btnVersion1File.Click += new System.EventHandler(this.btnVersion1File_Click);
            // 
            // btnEthalonFile
            // 
            this.btnEthalonFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEthalonFile.Location = new System.Drawing.Point(694, 10);
            this.btnEthalonFile.Name = "btnEthalonFile";
            this.btnEthalonFile.Size = new System.Drawing.Size(33, 20);
            this.btnEthalonFile.TabIndex = 6;
            this.btnEthalonFile.Text = "...";
            this.btnEthalonFile.UseVisualStyleBackColor = true;
            this.btnEthalonFile.Click += new System.EventHandler(this.btnEthalonFile_Click);
            // 
            // txtVersion2File
            // 
            this.txtVersion2File.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion2File.Location = new System.Drawing.Point(114, 65);
            this.txtVersion2File.Name = "txtVersion2File";
            this.txtVersion2File.ReadOnly = true;
            this.txtVersion2File.Size = new System.Drawing.Size(574, 20);
            this.txtVersion2File.TabIndex = 5;
            // 
            // txtVersion1File
            // 
            this.txtVersion1File.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion1File.Location = new System.Drawing.Point(114, 38);
            this.txtVersion1File.Name = "txtVersion1File";
            this.txtVersion1File.ReadOnly = true;
            this.txtVersion1File.Size = new System.Drawing.Size(574, 20);
            this.txtVersion1File.TabIndex = 4;
            // 
            // txtEthalonFile
            // 
            this.txtEthalonFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEthalonFile.Location = new System.Drawing.Point(114, 10);
            this.txtEthalonFile.Name = "txtEthalonFile";
            this.txtEthalonFile.ReadOnly = true;
            this.txtEthalonFile.Size = new System.Drawing.Size(574, 20);
            this.txtEthalonFile.TabIndex = 3;
            // 
            // lblVersion2File
            // 
            this.lblVersion2File.AutoSize = true;
            this.lblVersion2File.ForeColor = System.Drawing.Color.White;
            this.lblVersion2File.Location = new System.Drawing.Point(52, 68);
            this.lblVersion2File.Name = "lblVersion2File";
            this.lblVersion2File.Size = new System.Drawing.Size(56, 13);
            this.lblVersion2File.TabIndex = 2;
            this.lblVersion2File.Text = "Версия 2:";
            // 
            // lblVersion1File
            // 
            this.lblVersion1File.AutoSize = true;
            this.lblVersion1File.ForeColor = System.Drawing.Color.White;
            this.lblVersion1File.Location = new System.Drawing.Point(52, 41);
            this.lblVersion1File.Name = "lblVersion1File";
            this.lblVersion1File.Size = new System.Drawing.Size(56, 13);
            this.lblVersion1File.TabIndex = 1;
            this.lblVersion1File.Text = "Версия 1:";
            // 
            // lblEthalonFile
            // 
            this.lblEthalonFile.AutoSize = true;
            this.lblEthalonFile.ForeColor = System.Drawing.Color.White;
            this.lblEthalonFile.Location = new System.Drawing.Point(13, 13);
            this.lblEthalonFile.Name = "lblEthalonFile";
            this.lblEthalonFile.Size = new System.Drawing.Size(95, 13);
            this.lblEthalonFile.TabIndex = 0;
            this.lblEthalonFile.Text = "Эталонный файл:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Все файлы|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.Filter = "Текстовые файлы|*.txt";
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(0, 139);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(739, 284);
            this.txtResult.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 423);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.panelTop);
            this.Name = "MainForm";
            this.Text = "MultyMerge";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblVersion2File;
        private System.Windows.Forms.Label lblVersion1File;
        private System.Windows.Forms.Label lblEthalonFile;
        private System.Windows.Forms.TextBox txtVersion2File;
        private System.Windows.Forms.TextBox txtVersion1File;
        private System.Windows.Forms.TextBox txtEthalonFile;
        private System.Windows.Forms.Button btnEthalonFile;
        private System.Windows.Forms.Button btnVersion2File;
        private System.Windows.Forms.Button btnVersion1File;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox txtResult;

    }
}


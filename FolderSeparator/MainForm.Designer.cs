namespace FolderSeparator
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
            this.label1 = new System.Windows.Forms.Label();
            this.numUpDownSize = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSourceFolderInfo = new System.Windows.Forms.TextBox();
            this.txtTargetFolderInfo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max Size:";
            // 
            // numUpDownSize
            // 
            this.numUpDownSize.DecimalPlaces = 2;
            this.numUpDownSize.Location = new System.Drawing.Point(161, 50);
            this.numUpDownSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUpDownSize.Name = "numUpDownSize";
            this.numUpDownSize.Size = new System.Drawing.Size(120, 26);
            this.numUpDownSize.TabIndex = 1;
            this.numUpDownSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "GB";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(275, 448);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 37);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Source folder:";
            // 
            // txtSourceFolderInfo
            // 
            this.txtSourceFolderInfo.Location = new System.Drawing.Point(161, 102);
            this.txtSourceFolderInfo.Multiline = true;
            this.txtSourceFolderInfo.Name = "txtSourceFolderInfo";
            this.txtSourceFolderInfo.ReadOnly = true;
            this.txtSourceFolderInfo.Size = new System.Drawing.Size(189, 135);
            this.txtSourceFolderInfo.TabIndex = 6;
            this.txtSourceFolderInfo.DoubleClick += new System.EventHandler(this.txtSourceFolderInfo_DoubleClick);
            // 
            // txtTargetFolderInfo
            // 
            this.txtTargetFolderInfo.Location = new System.Drawing.Point(161, 272);
            this.txtTargetFolderInfo.Multiline = true;
            this.txtTargetFolderInfo.Name = "txtTargetFolderInfo";
            this.txtTargetFolderInfo.ReadOnly = true;
            this.txtTargetFolderInfo.Size = new System.Drawing.Size(189, 135);
            this.txtTargetFolderInfo.TabIndex = 8;
            this.txtTargetFolderInfo.DoubleClick += new System.EventHandler(this.txtTargetFolderInfo_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Target folder:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 519);
            this.Controls.Add(this.txtTargetFolderInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSourceFolderInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numUpDownSize);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Folder Separator";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numUpDownSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSourceFolderInfo;
        private System.Windows.Forms.TextBox txtTargetFolderInfo;
        private System.Windows.Forms.Label label4;
    }
}


namespace SkypeDBReader
{
    partial class about
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(about));
            this.label1 = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.CRLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MITLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "About";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(91, 52);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(44, 12);
            this.VersionLabel.TabIndex = 1;
            this.VersionLabel.Text = "Version";
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.Location = new System.Drawing.Point(22, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(242, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "本アプリケーションを利用したことによる損失について\r\n製作者はいかなる責任を負いかねます。\r\n";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(91, 30);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(34, 12);
            this.NameLabel.TabIndex = 3;
            this.NameLabel.Text = "Name";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SkypeDBReader.Properties.Resources.SDBR;
            this.pictureBox1.Location = new System.Drawing.Point(12, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(58, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // CRLabel
            // 
            this.CRLabel.AutoSize = true;
            this.CRLabel.Location = new System.Drawing.Point(91, 72);
            this.CRLabel.Name = "CRLabel";
            this.CRLabel.Size = new System.Drawing.Size(35, 12);
            this.CRLabel.TabIndex = 5;
            this.CRLabel.Text = "label5";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(19, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(250, 2);
            this.label6.TabIndex = 6;
            // 
            // MITLabel
            // 
            this.MITLabel.AutoSize = true;
            this.MITLabel.Location = new System.Drawing.Point(91, 84);
            this.MITLabel.Name = "MITLabel";
            this.MITLabel.Size = new System.Drawing.Size(56, 12);
            this.MITLabel.TabIndex = 7;
            this.MITLabel.TabStop = true;
            this.MITLabel.Text = "linkLabel1";
            this.MITLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MITLabel_LinkClicked);
            // 
            // about
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 150);
            this.Controls.Add(this.MITLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CRLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "about";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label CRLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel MITLabel;
    }
}
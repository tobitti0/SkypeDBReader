namespace SkypeDBReader
{
    partial class Filter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Filter));
            this.FilterCheck = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.FilterSkypeID = new System.Windows.Forms.TextBox();
            this.FSkypeCheck = new System.Windows.Forms.CheckBox();
            this.FStringCheck = new System.Windows.Forms.CheckBox();
            this.FilterString = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FilterCheck
            // 
            this.FilterCheck.AutoSize = true;
            this.FilterCheck.Location = new System.Drawing.Point(13, 13);
            this.FilterCheck.Name = "FilterCheck";
            this.FilterCheck.Size = new System.Drawing.Size(152, 16);
            this.FilterCheck.TabIndex = 0;
            this.FilterCheck.Text = "フィルター機能を有効にする";
            this.FilterCheck.UseVisualStyleBackColor = true;
            this.FilterCheck.CheckedChanged += new System.EventHandler(this.FilterCheck_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(187, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "適応";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(269, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "閉じる";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FilterSkypeID
            // 
            this.FilterSkypeID.Enabled = false;
            this.FilterSkypeID.Location = new System.Drawing.Point(105, 30);
            this.FilterSkypeID.Name = "FilterSkypeID";
            this.FilterSkypeID.Size = new System.Drawing.Size(133, 19);
            this.FilterSkypeID.TabIndex = 4;
            // 
            // FSkypeCheck
            // 
            this.FSkypeCheck.AutoSize = true;
            this.FSkypeCheck.Location = new System.Drawing.Point(33, 32);
            this.FSkypeCheck.Name = "FSkypeCheck";
            this.FSkypeCheck.Size = new System.Drawing.Size(66, 16);
            this.FSkypeCheck.TabIndex = 5;
            this.FSkypeCheck.Text = "SkypeID";
            this.FSkypeCheck.UseVisualStyleBackColor = true;
            // 
            // FStringCheck
            // 
            this.FStringCheck.AutoSize = true;
            this.FStringCheck.Location = new System.Drawing.Point(33, 55);
            this.FStringCheck.Name = "FStringCheck";
            this.FStringCheck.Size = new System.Drawing.Size(105, 16);
            this.FStringCheck.TabIndex = 6;
            this.FStringCheck.Text = "特定文字に反応";
            this.FStringCheck.UseVisualStyleBackColor = true;
            // 
            // FilterString
            // 
            this.FilterString.Location = new System.Drawing.Point(144, 53);
            this.FilterString.Name = "FilterString";
            this.FilterString.Size = new System.Drawing.Size(138, 19);
            this.FilterString.TabIndex = 7;
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 130);
            this.Controls.Add(this.FilterString);
            this.Controls.Add(this.FStringCheck);
            this.Controls.Add(this.FSkypeCheck);
            this.Controls.Add(this.FilterSkypeID);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FilterCheck);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Filter";
            this.Text = "Filter";
            this.Load += new System.EventHandler(this.Filter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox FilterCheck;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox FilterSkypeID;
        private System.Windows.Forms.CheckBox FSkypeCheck;
        private System.Windows.Forms.CheckBox FStringCheck;
        private System.Windows.Forms.TextBox FilterString;
    }
}
namespace SkypeDBReader
{
    partial class Option
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Option));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SkypeID = new System.Windows.Forms.TextBox();
            this.MessageRow = new System.Windows.Forms.TextBox();
            this.ChatID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MyColor_Check = new System.Windows.Forms.CheckBox();
            this.TimeDisplayCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "取得件数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "会話ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "Skype ID";
            // 
            // SkypeID
            // 
            this.SkypeID.Location = new System.Drawing.Point(83, 12);
            this.SkypeID.Name = "SkypeID";
            this.SkypeID.Size = new System.Drawing.Size(129, 19);
            this.SkypeID.TabIndex = 16;
            // 
            // MessageRow
            // 
            this.MessageRow.Location = new System.Drawing.Point(83, 62);
            this.MessageRow.Name = "MessageRow";
            this.MessageRow.Size = new System.Drawing.Size(53, 19);
            this.MessageRow.TabIndex = 15;
            // 
            // ChatID
            // 
            this.ChatID.Location = new System.Drawing.Point(83, 37);
            this.ChatID.Name = "ChatID";
            this.ChatID.Size = new System.Drawing.Size(417, 19);
            this.ChatID.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(343, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(424, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "閉じる";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "保存ボタンを押さないと保存されません。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "通常11件が画面にぴったりです。";
            // 
            // MyColor_Check
            // 
            this.MyColor_Check.AutoSize = true;
            this.MyColor_Check.Checked = true;
            this.MyColor_Check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MyColor_Check.Location = new System.Drawing.Point(83, 96);
            this.MyColor_Check.Name = "MyColor_Check";
            this.MyColor_Check.Size = new System.Drawing.Size(151, 16);
            this.MyColor_Check.TabIndex = 24;
            this.MyColor_Check.Text = "自分のチャットに色を付ける";
            this.MyColor_Check.UseVisualStyleBackColor = true;
            // 
            // TimeDisplayCheck
            // 
            this.TimeDisplayCheck.AutoSize = true;
            this.TimeDisplayCheck.Checked = true;
            this.TimeDisplayCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TimeDisplayCheck.Location = new System.Drawing.Point(83, 118);
            this.TimeDisplayCheck.Name = "TimeDisplayCheck";
            this.TimeDisplayCheck.Size = new System.Drawing.Size(142, 16);
            this.TimeDisplayCheck.TabIndex = 24;
            this.TimeDisplayCheck.Text = "チャットの時間を表示する";
            this.TimeDisplayCheck.UseVisualStyleBackColor = true;
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 195);
            this.Controls.Add(this.TimeDisplayCheck);
            this.Controls.Add(this.MyColor_Check);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SkypeID);
            this.Controls.Add(this.MessageRow);
            this.Controls.Add(this.ChatID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Option";
            this.Text = "Option";
            this.Load += new System.EventHandler(this.Option_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SkypeID;
        private System.Windows.Forms.TextBox MessageRow;
        private System.Windows.Forms.TextBox ChatID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox MyColor_Check;
        private System.Windows.Forms.CheckBox TimeDisplayCheck;
    }
}
namespace SkypeDBReader
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LoadButton = new System.Windows.Forms.Button();
            this.MonitoringStartButton = new System.Windows.Forms.Button();
            this.MonitoringEndButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.check_scroll = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.メインToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ロードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.そんな機能はないToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.フィルターToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.会話ID調査ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterStatusLabel = new System.Windows.Forms.Label();
            this.NewLink = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(102, 297);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(114, 23);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "読み込み";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // MonitoringStartButton
            // 
            this.MonitoringStartButton.Location = new System.Drawing.Point(222, 297);
            this.MonitoringStartButton.Name = "MonitoringStartButton";
            this.MonitoringStartButton.Size = new System.Drawing.Size(114, 23);
            this.MonitoringStartButton.TabIndex = 2;
            this.MonitoringStartButton.Text = "監視開始";
            this.MonitoringStartButton.UseVisualStyleBackColor = true;
            this.MonitoringStartButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MonitoringEndButton
            // 
            this.MonitoringEndButton.Enabled = false;
            this.MonitoringEndButton.Location = new System.Drawing.Point(342, 297);
            this.MonitoringEndButton.Name = "MonitoringEndButton";
            this.MonitoringEndButton.Size = new System.Drawing.Size(122, 23);
            this.MonitoringEndButton.TabIndex = 3;
            this.MonitoringEndButton.Text = "監視終了";
            this.MonitoringEndButton.UseVisualStyleBackColor = true;
            this.MonitoringEndButton.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(13, 34);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(101, 12);
            this.StatusLabel.TabIndex = 4;
            this.StatusLabel.Text = "たいきちゅう(´・ω・｀)";
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Aquamarine;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView.Location = new System.Drawing.Point(15, 49);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(683, 243);
            this.dataGridView.TabIndex = 12;
            this.dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting);
            this.dataGridView.MouseLeave += new System.EventHandler(this.dataGridView1_MouseLeave);
            // 
            // check_scroll
            // 
            this.check_scroll.AutoSize = true;
            this.check_scroll.Checked = true;
            this.check_scroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_scroll.Location = new System.Drawing.Point(470, 301);
            this.check_scroll.Name = "check_scroll";
            this.check_scroll.Size = new System.Drawing.Size(94, 16);
            this.check_scroll.TabIndex = 13;
            this.check_scroll.Text = "強制スクロール";
            this.check_scroll.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.メインToolStripMenuItem,
            this.フィルターToolStripMenuItem,
            this.設定ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(710, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // メインToolStripMenuItem
            // 
            this.メインToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ロードToolStripMenuItem,
            this.終了ToolStripMenuItem});
            this.メインToolStripMenuItem.Name = "メインToolStripMenuItem";
            this.メインToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.メインToolStripMenuItem.Text = "メイン";
            // 
            // ロードToolStripMenuItem
            // 
            this.ロードToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.そんな機能はないToolStripMenuItem});
            this.ロードToolStripMenuItem.Name = "ロードToolStripMenuItem";
            this.ロードToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.ロードToolStripMenuItem.Text = "ロード";
            // 
            // そんな機能はないToolStripMenuItem
            // 
            this.そんな機能はないToolStripMenuItem.Name = "そんな機能はないToolStripMenuItem";
            this.そんな機能はないToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.そんな機能はないToolStripMenuItem.Text = "そんな機能はない";
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
            // 
            // フィルターToolStripMenuItem
            // 
            this.フィルターToolStripMenuItem.Name = "フィルターToolStripMenuItem";
            this.フィルターToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.フィルターToolStripMenuItem.Text = "フィルター";
            this.フィルターToolStripMenuItem.Click += new System.EventHandler(this.フィルターToolStripMenuItem_Click);
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.会話ID調査ToolStripMenuItem,
            this.設定ToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.設定ToolStripMenuItem.Text = "その他";
            // 
            // 会話ID調査ToolStripMenuItem
            // 
            this.会話ID調査ToolStripMenuItem.Name = "会話ID調査ToolStripMenuItem";
            this.会話ID調査ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.会話ID調査ToolStripMenuItem.Text = "会話ID調査";
            this.会話ID調査ToolStripMenuItem.Click += new System.EventHandler(this.会話ID調査ToolStripMenuItem_Click);
            // 
            // 設定ToolStripMenuItem1
            // 
            this.設定ToolStripMenuItem1.Name = "設定ToolStripMenuItem1";
            this.設定ToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.設定ToolStripMenuItem1.Text = "設定";
            this.設定ToolStripMenuItem1.Click += new System.EventHandler(this.設定ToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // FilterStatusLabel
            // 
            this.FilterStatusLabel.AutoSize = true;
            this.FilterStatusLabel.Location = new System.Drawing.Point(234, 34);
            this.FilterStatusLabel.Name = "FilterStatusLabel";
            this.FilterStatusLabel.Size = new System.Drawing.Size(0, 12);
            this.FilterStatusLabel.TabIndex = 16;
            // 
            // NewLink
            // 
            this.NewLink.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewLink.Location = new System.Drawing.Point(470, 34);
            this.NewLink.Name = "NewLink";
            this.NewLink.Size = new System.Drawing.Size(228, 12);
            this.NewLink.TabIndex = 17;
            this.NewLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.NewLink_LinkClicked);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.AutoPopDelay = 1000000;
            this.toolTip1.InitialDelay = 0;
            this.toolTip1.ReshowDelay = 0;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(710, 326);
            this.Controls.Add(this.NewLink);
            this.Controls.Add(this.FilterStatusLabel);
            this.Controls.Add(this.check_scroll);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.MonitoringEndButton);
            this.Controls.Add(this.MonitoringStartButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SkypeDBReader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button MonitoringStartButton;
        private System.Windows.Forms.Button MonitoringEndButton;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.CheckBox check_scroll;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem メインToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ロードToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 会話ID調査ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem そんな機能はないToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem フィルターToolStripMenuItem;
        private System.Windows.Forms.Label FilterStatusLabel;
        private System.Windows.Forms.LinkLabel NewLink;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}


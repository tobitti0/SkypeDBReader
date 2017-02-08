using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;

namespace SkypeDBReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {//githubtest
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns.Add("ID", "なまえ");
            dataGridView1.Columns.Add("message", "ないよう");
            dataGridView1.Columns.Add("status", "ステータス");
            dataGridView1.Columns["status"].Visible = false;
            dataGridView1.Columns["message"].DefaultCellStyle.WrapMode =
            DataGridViewTriState.True;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        

        private System.IO.FileSystemWatcher watcher = null;

        private void button1_Click_1(object sender, EventArgs e)//ファイルを監視
        {
            copylog();
            readlog();
            string skypeID = Properties.Settings.Default.SkypeID;//DB探すのにIDいる&設定から持ってくる
            if (watcher != null) return;
            watcher = new System.IO.FileSystemWatcher();
            //監視するディレクトリを指定
            watcher.Path = @System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Skype\\" + skypeID;
            //最終アクセス日時、最終更新日時、ファイル、フォルダ名の変更を監視する
            watcher.NotifyFilter =
                (System.IO.NotifyFilters.LastAccess
                | System.IO.NotifyFilters.LastWrite);
            //すべてのファイルを監視
            watcher.Filter = "main.db";

            watcher.SynchronizingObject = this;

            //イベントハンドラの追加
            watcher.Changed += new System.IO.FileSystemEventHandler(watcher_Changed);

            //監視を開始する
            watcher.EnableRaisingEvents = true;
            label1.Text = "監視ちゅう(｀・ω・´)";

            button2.Enabled = true;
            button1.Enabled = false;
        }

        private void button2_Click_1(object sender, EventArgs e)//監視を終了
        {
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
            watcher = null;
            label1.Text = "監視おわり( ˘ω˘)";
            UnSelect();
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void watcher_Changed(System.Object source,System.IO.FileSystemEventArgs e)//ファイルの変更があった時
        {
            switch (e.ChangeType)
            {
                case System.IO.WatcherChangeTypes.Changed:
                    copylog();//copylogを呼び出し
                    readlog();//readlogを呼び出し
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)//手動呼び出し
        {
            copylog();//コピー
            readlog();//読み取り＆表示
        }


        public void readlog()//ログを開いて目的のものを抜き出してグリッドビューに表示
        {
            string target = Properties.Settings.Default.ChatID; ;//目当ての会話ID(main.dbみろ)&設定から持ってくる
            //mine鯖会議  19:6385c875730e47e885eede411c0f0f9b@thread.skype
            string line = Properties.Settings.Default.MessageRow;//ケツからn行指定&設定から持ってくる


            string filter = null;

            if (Properties.Settings.Default.FilterCheck)
            {
                FilterStatus.Text = "フィルターが有効(｀・д・´)";

                if (Properties.Settings.Default.FSkypeCheck)
                    filter = ("author = '" + Properties.Settings.Default.FilterSkypeID +"' AND ");
                else
                    filter = "";

            }
            else
            {
                filter = "";
                FilterStatus.Text="";
            }


            string databaseFilePath = "main.db"; // データベースファイル

            string commandText =
                "SELECT from_dispname,body_xml,chatmsg_status FROM Messages WHERE "+ filter +"(chatname = '" + target + "' OR dialog_partner = '" + target + "') ORDER BY id DESC limit " + line + ";";

            SQLiteConnection connection = null;
            SQLiteCommand command = null;
            SQLiteDataReader reader = null;

            try
            {
                connection = new SQLiteConnection();
                connection.ConnectionString = "Data Source=" + databaseFilePath + ";Version=3;";
                connection.Open();

                command = connection.CreateCommand();
                command.CommandText = commandText;
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "コマンドエラー");
            }
            finally
            {
                if (command != null)
                    command.Dispose();
            }

            if (reader == null)
                return;

            var sb2 = new StringBuilder();
            int n = 0;
            int Row = 0;
            var columnList = new List<string>();


            if (reader.HasRows) // コマンドの実行後、行データを持つとき
            {
                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    if (reader.FieldCount > n)
                    {
                        columnList.Add(reader.GetName(n)); // カラム名を取得する
                        ++n;
                    }
                    int System = 0;
                    for (int j = 0; j < reader.FieldCount; ++j) // 行データのフィールド数だけ繰り返す
                    {
                        object obj = reader.GetValue(j);
                        if (obj == null)
                            sb2.Append("null,");
                        else if (obj.ToString() == "")
                            sb2.Append(string.Format("<SDBR>削除されたようです。,")); 
                        else if (Regex.IsMatch(obj.ToString(), @"<URIObject\s+[^>]*type=""Picture.1""\s*uri\s*="))
                        {
                            sb2.Append(string.Format("<SDBR>画像のようです。,"));
                            System = 1;
                        }
                        else if (Regex.IsMatch(obj.ToString(), @"<URIObject\s+[^>]*type=""File.1""\s*uri\s*="))
                        {
                            sb2.Append(string.Format("<SDBR>なにかのファイルのようです。,"));
                            System = 2;
                        }
                        else if (Regex.IsMatch(obj.ToString(), @"<a\s+[^>]*href\s*=\s*(?:(?<quot>[""'])(?<url>.*?)\k<quot>|" +@"(?<url>[^\s>]+))[^>]*>(?<text>.*?)</a>"))
                        {
                            sb2.Append(string.Format("<SDBR>URLのようです。,"));
                            System = 3;
                        }
                        else if (obj is string)
                            sb2.Append(string.Format("{0},", reader.GetString(j)));
                        else
                            sb2.Append(String.Format("{0},", obj));
                    }


                    sb2.Replace("&lt;", "<");//Skypeに勝手に書き換えられるのを戻す1
                    sb2.Replace("&gt;", ">");//Skypeに勝手に書き換えられるのを戻す2
                    string linedate = sb2.ToString();
                    Regex replace1 = new Regex(@"<quote\sauthor=""[^""]+""\sauthorname=""[^""]+""\sconversation=""[^""]+""\sguid=""[^""]+""\stimestamp=""[^""]+"">");
                    Regex replace2 = new Regex(@"\r\n<<<\s</legacyquote></quote>");
                    Regex replace3 = new Regex(@"</legacyquote>");
                    Regex replace4 = new Regex(@"<legacyquote>");
                    linedate = replace1.Replace(linedate, "");//Skypeデフォの引用のｺﾞﾐを削除2-1
                    linedate = replace2.Replace(linedate, "");//Skypeデフォの引用のｺﾞﾐを削除2-2
                    linedate = replace3.Replace(linedate, "");//Skypeデフォの引用のｺﾞﾐを削除2-3
                    linedate = replace4.Replace(linedate, "");//Skypeデフォの引用のｺﾞﾐを削除2-4
                    string[] row = linedate.Split(',');//[,]で配列区切り
                    dataGridView1.Rows.Insert(0, row);//グリッドビューに挿入
                    if (Properties.Settings.Default.MyColor)//optionで自コメントに色設定がチェックついていたら
                    {
                        if (Convert.ToString(dataGridView1[2, Row].Value) == "2") dataGridView1.Rows[Row].DefaultCellStyle.BackColor = Color.LightGray;
                    }

                    if (System == 1) dataGridView1.Rows[Row].DefaultCellStyle.BackColor = Color.LightPink;
                    if (System == 2) dataGridView1.Rows[Row].DefaultCellStyle.BackColor = Color.Thistle;
                    if (System == 3) dataGridView1.Rows[Row].DefaultCellStyle.BackColor = Color.LightGreen;
                    if (Convert.ToString(dataGridView1[2, Row].Value) == "3") dataGridView1.Rows[Row].DefaultCellStyle.BackColor = Color.NavajoWhite;
                    CompareInfo ci = CultureInfo.CurrentCulture.CompareInfo;
                    if (Properties.Settings.Default.FilterCheck && Properties.Settings.Default.FStringCheck)//filterでチェックついていたら
                    {
                        string temp = Convert.ToString(dataGridView1[1, Row].Value);
                        string temp2 = Properties.Settings.Default.FilterString;
                        if (temp.IndexOf(temp2, StringComparison.OrdinalIgnoreCase) >= 0 || ci.IndexOf(temp, temp2, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType) >= 0)
                            dataGridView1.Rows[Row].DefaultCellStyle.BackColor = Color.Crimson;
                    }
                    sb2.Clear();
                    if (check_scroll.Checked == true)
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                }

                if (reader != null)
                    reader.Close();
            }

            // データベース接続を閉じる
            if (connection != null)
                connection.Close();
            //ChangeSelectColor();
            UnSelect();
        }

        private void copylog()//ログをSkypeのとこから手元にコピペ
        {
            string skypeID = Properties.Settings.Default.SkypeID;//DB探すのにIDいる&設定から持ってくる
            //string databaseFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Skype\\" + skypeID + "\\Main.db"; // データベースファイル
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(@System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Skype\\" + skypeID + "\\Main.db");
                System.IO.FileInfo copyFile = fi.CopyTo(@"main.db", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ファイル探査エラー");
            }
        }

        private Form2 _Form2 = null;//ウインド状況管理用
        private Option _Formoption = null;//ウインド状況管理用
        private Filter _Formfilter = null;//ウインド状況管理用


        private void 会話ID調査ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this._Form2 == null || this._Form2.IsDisposed)
                this._Form2 = new Form2();

            if (!this._Form2.Visible)
            {
                _Form2.Owner = this;
                _Form2.StartPosition = FormStartPosition.CenterParent;
                this._Form2.Show();
            }
            else
            {
                this._Form2.Activate();
            }
        }

        private void 設定ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this._Formoption == null || this._Formoption.IsDisposed)
                this._Formoption = new Option();

            if (!this._Formoption.Visible)
            {
                _Formoption.Owner = this;
                _Formoption.StartPosition = FormStartPosition.CenterParent;
                this._Formoption.Show();
            }
            else
            {
                this._Formoption.Activate();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about dlg = new about();
            dlg.StartPosition = FormStartPosition.CenterParent;
            DialogResult dlgRet = dlg.ShowDialog(this);
        }

        private void フィルターToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this._Formfilter == null || this._Formfilter.IsDisposed)
                this._Formfilter = new Filter();

            if (!this._Formfilter.Visible)
            {
                _Formfilter.Owner = this;
                _Formfilter.StartPosition = FormStartPosition.CenterParent;
                this._Formfilter.Show();
            }
            else
            {
                this._Formfilter.Activate();
            }
        }

        private void UnSelect()//選択を解除する
        {
            dataGridView1.CurrentCell = null;

        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)//グリッドビューからマウスが離れたら
        {
            UnSelect();
        }

        Color now;//選択した時の背景色を記録する用
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)//編集モードになった時
        {
            int Row = dataGridView1.CurrentCell.RowIndex;
            Color backcolor = dataGridView1.DefaultCellStyle.SelectionBackColor;
            now = dataGridView1.Rows[Row].InheritedStyle.BackColor;
            dataGridView1.Rows[Row].DefaultCellStyle.BackColor = backcolor;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)//編集モードから出た時
        {
            int Row = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[Row].DefaultCellStyle.BackColor = now;
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.FirstRun == true)
            {
                FirstSetUP FSUP = new FirstSetUP();
                FSUP.StartPosition = FormStartPosition.CenterParent;
                DialogResult dlgRet = FSUP.ShowDialog(this);
            }
        }


    }

}
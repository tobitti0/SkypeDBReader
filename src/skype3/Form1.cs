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
        {
            InitializeComponent();
            //---------Windowの設定----ここから
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //------------------------ここまで

            //---------DataGridViewの設定---ここから
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.Columns.Add("ID", "なまえ");
            dataGridView.Columns.Add("message", "ないよう");
            dataGridView.Columns.Add("timestamp", "");
            dataGridView.Columns.Add("status", "ステータス");
            dataGridView.Columns["status"].Visible = false;//statusの列は非表示に
            dataGridView.Columns["message"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.Columns["timestamp"].DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleRight;
            dataGridView.RowHeadersVisible = false;//一番左のはいらないので非表示
            dataGridView.ColumnHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;//データを追加する必要がないので追加させない
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DanySort();
            DataGridViewClear();
            //------------------------------ここまで

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.FirstRun == true)//初回起動の時の処理
            {
                FirstSetUP FSUP = new FirstSetUP();
                FSUP.StartPosition = FormStartPosition.CenterParent;
                DialogResult dlgRet = FSUP.ShowDialog(this);
            }
        }


        private System.IO.FileSystemWatcher watcher = null;//ファイル監視用

        private void button1_Click_1(object sender, EventArgs e)//ファイルを監視
        {
            //いったん読み込まないと画面が寂しい
            copylog();
            readlog();


            //DB探すのにIDいる&設定から持ってくる
            string skypeID = Properties.Settings.Default.SkypeID;

            if (watcher != null) return;

            watcher = new System.IO.FileSystemWatcher();

            //監視するディレクトリ
            watcher.Path = @System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Skype\\" + skypeID;

            //最終アクセス日時、最終更新日時、を監視
            watcher.NotifyFilter =
                (System.IO.NotifyFilters.LastAccess
                | System.IO.NotifyFilters.LastWrite);

            //main.dbファイルを監視
            watcher.Filter = "main.db";
            watcher.SynchronizingObject = this;

            //イベントハンドラの追加
            watcher.Changed += new System.IO.FileSystemEventHandler(watcher_Changed);

            //監視を開始する
            watcher.EnableRaisingEvents = true;
            //状態を表示
            StatusLabel.Text = "監視ちゅう(｀・ω・´)";

            //開始ボタンを押せなくして終了ボタンを押せるように
            MonitoringEndButton.Enabled = true;
            MonitoringStartButton.Enabled = false;
        }

        private void button2_Click_1(object sender, EventArgs e)//監視を終了
        {
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
            watcher = null;
            //状態を表示
            StatusLabel.Text = "監視おわり( ˘ω˘)";
            UnSelect();
            //終了を押せなくして開始を押せるように
            MonitoringEndButton.Enabled = false;
            MonitoringStartButton.Enabled = true;
        }

        private void watcher_Changed(System.Object source, System.IO.FileSystemEventArgs e)//ファイルの変更があった時
        {
            switch (e.ChangeType)
            {
                case System.IO.WatcherChangeTypes.Changed://なんか変わったら。
                    copylog();//copylogを呼び出し
                    readlog();//readlogを呼び出し
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)//ボタンで手動読み込み
        {
            copylog();//コピー
            readlog();//読み取り＆表示
        }


        public void readlog()//ログを開いて目的のものを抜き出してグリッドビューに表示
        {
            //UNIX-Timestampで現在のtimestampを得る
            uint _nowTimestamp = (uint)((DateTime.UtcNow.Ticks - DateTime.Parse("1970-01-01 00:00:00").Ticks) / 10000000);

            //目当ての会話IDを設定から持ってくる
            string target = Properties.Settings.Default.ChatID; ;

            //データの取り出し件数を設定から持ってくる
            string line = Properties.Settings.Default.MessageRow;

            //------------フィルターの処理---ここから
            string filter = null;

            if (Properties.Settings.Default.FilterCheck)//フィルターがONの時
            {
                //情報を表示
                FilterStatusLabel.Text = "フィルターが有効(｀・д・´)";

                if (Properties.Settings.Default.FSkypeCheck)//IDフィルターが有効の時
                    filter = ("author = '" + Properties.Settings.Default.FilterSkypeID + "' AND ");//検索条件を作成
                else
                    filter = "";//無効だったら検索条件は作らない
            }
            else
            {
                //フィルターがOFFの時
                filter = "";
                //情報を表示
                FilterStatusLabel.Text = "";
            }
            //------------------------------ここまで

            //データベースファイル名
            string databaseFilePath = "main.db";

            //データベースから抽出するためのSQL文
            string commandText =
                "SELECT from_dispname,body_xml,timestamp__ms,chatmsg_status FROM Messages WHERE " + filter + "(chatname = '" + target + "' OR dialog_partner = '" + target + "') ORDER BY id DESC limit " + line + ";";
            //from_dispname =ID[送信者]
            //body_xml      =massage[本文]
            //timestamp__ms =timestamp[時間]
            //chatmsg_status=status[メッセステータス（1=不明,2=自分メッセ,3=未読,4=既読）]

            SQLiteConnection connection = null;
            SQLiteCommand command = null;
            SQLiteDataReader reader = null;

            try//エラー発生の可能性あり
            {
                //データベースを開いてSQL文を実行
                connection = new SQLiteConnection();
                connection.ConnectionString = "Data Source=" + databaseFilePath + ";Version=3;";
                connection.Open();

                command = connection.CreateCommand();
                command.CommandText = commandText;
                reader = command.ExecuteReader();
            }
            catch (Exception ex)//エラー出たら
            {
                //エラーを見える形にしておく
                MessageBox.Show(ex.Message, "コマンドエラー");
            }
            finally
            {
                if (command != null)
                    command.Dispose();
            }

            if (reader == null)
                return;

            var DataString = new StringBuilder();
            int Row = 1;


            if (reader.HasRows) // コマンドの実行後、行データを持つとき
            {
                DataGridViewClear();


                while (reader.Read())
                {
                    int System = 0;
                    for (int j = 0; j < reader.FieldCount; ++j) // 行データのフィールド数だけ繰り返す
                    {
                        object obj = reader.GetValue(j);
                        if (obj == null)
                            DataString.Append("null,");
                        else if (obj.ToString() == "")
                            DataString.Append(string.Format("<SDBR>削除されたようです。,"));
                        else if (Regex.IsMatch(obj.ToString(), @"<URIObject\s+[^>]*type=""Picture.1""\s*uri\s*="))//この形は画像
                        {
                            DataString.Append(string.Format("<SDBR>画像のようです。,"));
                            System = 1;
                        }
                        else if (Regex.IsMatch(obj.ToString(), @"<URIObject\s+[^>]*type=""File.1""\s*uri\s*="))//この形はファイル
                        {
                            DataString.Append(string.Format("<SDBR>なにかのファイルのようです。,"));
                            System = 2;
                        }
                        else if (Regex.IsMatch(obj.ToString(), @"<a\s+[^>]*href\s*=\s*(?:(?<quot>[""'])(?<url>.*?)\k<quot>|" + @"(?<url>[^\s>]+))[^>]*>(?<text>.*?)</a>"))
                        {//この形はURL
                            DataString.Append(string.Format("<SDBR>URLのようです。,"));
                            System = 3;
                        }
                        else if (obj is string)
                            DataString.Append(string.Format("{0},", reader.GetString(j)));
                        else
                            DataString.Append(String.Format("{0},", obj));
                    }


                    DataString.Replace("&lt;", "<");//Skypeに勝手に書き換えられるのを戻す1
                    DataString.Replace("&gt;", ">");//Skypeに勝手に書き換えられるのを戻す2
                    string linedate = DataString.ToString();//stringに変換していれとく
                    //データ置換用正規表現---ここから
                    Regex replace1 = new Regex(@"<quote\sauthor=""[^""]+""\sauthorname=""[^""]+""\sconversation=""[^""]+""\sguid=""[^""]+""\stimestamp=""[^""]+"">");
                    Regex replace2 = new Regex(@"\r\n<<<\s</legacyquote></quote>");
                    Regex replace3 = new Regex(@"</legacyquote>");
                    Regex replace4 = new Regex(@"<legacyquote>");
                    //-----------------------ここまで
                    ////Skypeデフォの引用のゴミを正規表現で削除---ここから
                    linedate = replace1.Replace(linedate, "");
                    linedate = replace2.Replace(linedate, "");
                    linedate = replace3.Replace(linedate, "");
                    linedate = replace4.Replace(linedate, "");
                    //--------------------------------------------ここまで

                    string[] row = linedate.Split(',');//[,]で配列区切りにする


                    row[2] = TimestampDifference(_nowTimestamp, row[2]);//UNIXtimestampをhh:mmかss秒の形式に置き換え

                    dataGridView.Rows.Insert(1, row);//グリッドビューに配列で挿入


                    //-----------------------背景色を変える---ここから
                    if (Properties.Settings.Default.MyColor)//optionで自コメントに色設定がチェックついていたら
                    {

                        if (Convert.ToString(dataGridView[3, Row].Value) == "2") dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.LightGray;
                    }

                    if (System == 1) dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.LightPink;//画像の時
                    if (System == 2) dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.Thistle;//ファイルの時
                    if (System == 3) dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.LightGreen;//URLの時

                    //未読だったらstatus欄の値は3なので3だったら
                    if (Convert.ToString(dataGridView[3, Row].Value) == "3") dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.NavajoWhite;


                    //フィルター使用かつ文字強調がONの時
                    if (Properties.Settings.Default.FilterCheck && Properties.Settings.Default.FStringCheck)
                    {
                        CompareInfo ci = CultureInfo.CurrentCulture.CompareInfo;
                        string temp = Convert.ToString(dataGridView[1, Row].Value);//内容の取り出し
                        string temp2 = Properties.Settings.Default.FilterString;//調べたい文字の取り出し

                        //比較する
                        if (temp.IndexOf(temp2, StringComparison.OrdinalIgnoreCase) >= 0 || ci.IndexOf(temp, temp2, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType) >= 0)
                            dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.Crimson;
                    }
                    //-----------------------------------------ここまで

                    DataString.Clear();//データを入れたのでクリア

                    //スクロールするとき
                    if (check_scroll.Checked == true)
                        dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
                }

                if (reader != null)
                    reader.Close();
            }

            // データベース接続を閉じる
            if (connection != null)
                connection.Close();
            UnSelect();
            DanySort();
        }

        private void copylog()//ログをSkypeのとこから手元にコピペ
        {
            string skypeID = Properties.Settings.Default.SkypeID;//DB探すのにIDいる&設定から持ってくる
            try//エラー発生する可能性あり
            {
                //ファイルの場所
                System.IO.FileInfo fi = new System.IO.FileInfo(@System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Skype\\" + skypeID + "\\Main.db");
                //手元にコピー
                System.IO.FileInfo copyFile = fi.CopyTo(@"main.db", true);
            }
            catch (Exception ex)//エラー出たら
            {
                //表示
                MessageBox.Show(ex.Message, "ファイル探査エラー");
            }
        }

        private Form2 _Form2 = null;//ウインド状況管理用
        private Option _Formoption = null;//ウインド状況管理用
        private Filter _Formfilter = null;//ウインド状況管理用

        //-----------メニューバー設定---ここから
        private void 会話ID調査ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //他所起動防止
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
            //他所起動防止
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
            //閉じるまで他の画面をいじれない
            about dlg = new about();
            dlg.StartPosition = FormStartPosition.CenterParent;
            DialogResult dlgRet = dlg.ShowDialog(this);
        }

        private void フィルターToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //他所起動防止

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

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //閉じる
            Close();
        }

        //-----------------------------ここまで

        private void UnSelect()//選択を解除する,幅の調整
        {            
            dataGridView.Columns[2].Width = 35;//timestampの列の幅
            dataGridView.Columns[0].Width = 70;//IDの列の幅（設定で変えれるべきか？)
            dataGridView.CurrentCell = null;

        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)//グリッドビューからマウスが離れたら
        {
            UnSelect();
        }

        Color now;//選択した時の背景色を記録する用
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)//編集モードになった時
        {
            int Row = dataGridView.CurrentCell.RowIndex;
            Color backcolor = dataGridView.DefaultCellStyle.SelectionBackColor;
            now = dataGridView.Rows[Row].InheritedStyle.BackColor;
            dataGridView.Rows[Row].DefaultCellStyle.BackColor = backcolor;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)//編集モードから出た時
        {
            int Row = dataGridView.CurrentCell.RowIndex;
            dataGridView.Rows[Row].DefaultCellStyle.BackColor = now;
        }

        private void DanySort()//ソート機能の無効化
        {
            foreach (DataGridViewColumn Calumns in dataGridView.Columns)
            {
                Calumns.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private string TimestampDifference(uint now, string st)//UNIXTimestampをhh:mmかss秒の形式に置き換え
        {
            Int64 nowtime = Convert.ToInt64(now);//今のUNIXTimestamp
            Int64 messetime = Convert.ToInt64(st.Substring(0, 10));//データのUnixTimestanp
            Int64 defference = nowtime-messetime;//時差(秒)

            string output;

            if (defference >= 60)//一分以上ならhh:mm形式
            {
                DateTime UNIXBase=new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);//UNIXの定義時刻
                DateTime time = UNIXBase.AddSeconds(messetime).ToLocalTime();//日付とかの入った形に置換
                output = string.Format("{0}",time.ToShortTimeString());//hh:mm形式の部分だけ取り出し
                return output;

            }

            //一分未満ならss秒形式
            output = string.Format("{0}秒", defference);
            return output;


        }


        private void DataGridViewClear()//内容初期化と、ヘッダの作成
        {
            dataGridView.Rows.Clear();//表示されているものをすべて消す
            string[] header = new string[4]{//入れる文字の定義ヘッダから持ってくる
                dataGridView.Columns[0].HeaderText, dataGridView.Columns[1].HeaderText,
                dataGridView.Columns[2].HeaderText, dataGridView.Columns[3].HeaderText };
            dataGridView.Rows.Insert(0, header);//0行目に追加
            dataGridView.Rows[0].Frozen = true;//0行目を固定(スクロールしても常に表示)
            dataGridView.Rows[0].ReadOnly = true;//編集不可
            dataGridView.Columns[2].ReadOnly = true;//時間の列を編集不可
            dataGridView.Columns["timestamp"].Visible = Properties.Settings.Default.TimeDisplay;//timestampの列は設定に従って表示
            UnSelect();
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)//セルの設定
        {   //時間のセルの罫線を消す。ヘッダの色をうっすい灰色にする
            if (e.ColumnIndex == 2)
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (e.ColumnIndex == 1)
            {
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (e.RowIndex == 0)
            {
                DataGridView header = (DataGridView)sender;
                header[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.GhostWhite;
            }
        }
    }

}
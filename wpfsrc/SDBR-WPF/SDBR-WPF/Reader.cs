using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace SDBR_WPF
{
    public static class ReadWriter
    {
        public static void readwiter(List<DB> list, TextBlock FilterStatus, DataGrid dataGrid, TextBlock Log)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_Completed);
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync(Tuple.Create<List<DB>, TextBlock, DataGrid, TextBlock>(list, FilterStatus, dataGrid, Log));
            }
        }

        private static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            var tuple = (Tuple<List<DB>, TextBlock, DataGrid, TextBlock>)e.Argument;
            var list = tuple.Item1;
            var FilterStatus = tuple.Item2;
            var dataGrid = tuple.Item3;
            var Log = tuple.Item4;

            var dispatcher = Application.Current.Dispatcher;

                if (dispatcher.CheckAccess())
                {
                    copylog();
                    readlog(list, FilterStatus);
                }
                else
                {
                    dispatcher.Invoke(() => copylog());
                    dispatcher.Invoke(() => readlog(list, FilterStatus));
                }
                e.Result = tuple;

        }
        private static void bw_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            var tuple = (Tuple<List<DB>, TextBlock, DataGrid, TextBlock>)e.Result;
            var list = tuple.Item1;
            var FilterStatus = tuple.Item2;
            var dataGrid = tuple.Item3;
            var Log = tuple.Item4;

            if ((e.Cancelled == true))
            {
                MessageBox.Show(e.Error.ToString(), "Readerバックグラウンドエラー");
            }

            else if (!(e.Error == null))
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                var dispatcher = Application.Current.Dispatcher;
                if (dispatcher.CheckAccess())
                {
                    UpdateDispList(list, dataGrid, Log);
                    scrool(dataGrid);
                }
                else
                {
                    dispatcher.Invoke(() => UpdateDispList(list, dataGrid, Log));
                    dispatcher.Invoke(() => scrool(dataGrid));
                }
            }
        }

        private static void readlog(List<DB> list, TextBlock FilterStatus )//ログを開いて目的のものを抜き出してグリッドビューに表示
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
                FilterStatus.Text = "フィルター有効(｀・д・´)";

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
                FilterStatus.Text = "フィルター無効( ˘ω˘)";
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

            string[] row = new string[5];

            if (reader.HasRows) // コマンドの実行後、行データを持つとき
            {
               list.Clear();
                int System = 0;

                while (reader.Read())
                {
                    for (int j = 0; j < reader.FieldCount; ++j) // 行データのフィールド数だけ繰り返す
                    {
                        object obj = reader.GetValue(j);

                        if (obj == null)
                        {
                            row[j] = "null";
                        }
                        else if (obj.ToString() == "")
                        {
                            System = 0;
                            row[j] = "<SDBR>削除されたようです。";
                        }
                        else if (Regex.IsMatch(obj.ToString(), @"<URIObject\s+[^>]*type=""Picture.1""\s*uri\s*="))//この形は画像
                        {
                            System = 1;
                            row[j] = "<SDBR>画像のようです。";

                        }
                        else if (Regex.IsMatch(obj.ToString(), @"<URIObject\s+[^>]*type=""File.1""\s*uri\s*="))//この形はファイル
                        {
                            System = 2;
                            row[j] = "<SDBR>なにかのファイルのようです。";
                        }
                        else if (Regex.IsMatch(obj.ToString(), @"<a\s+[^>]*href\s*=\s*(?:(?<quot>[""'])(?<url>.*?)\k<quot>|" + @"(?<url>[^\s>]+))[^>]*>(?<text>.*?)</a>"))
                        {//この形はURL
                            System = 3;
                            row[j] = "<SDBR>URLのようです。";
                        }
                        else if (obj is string && j == 1)
                        {
                            System = 0;
                            row[j] = reader.GetString(j);
                        }
                        else if (obj is string)
                        {
                            row[j] = reader.GetString(j);
                        }
                        else
                        {
                            row[j] = obj.ToString();
                        }
                        row[j + 1] = System.ToString();

                    }

                    row[1]= HttpUtility.HtmlDecode(row[1]);//Skypeに勝手に[&○○;]に書き換えられたのを戻す

                    //データ置換用正規表現---ここから
                    Regex replace1 = new Regex(@"<quote\sauthor=""[^""]+""\sauthorname=""[^""]+""\sconversation=""[^""]+""\sguid=""[^""]+""\stimestamp=""[^""]+"">");
                    Regex replace2 = new Regex(@"\r\n<<<\s</legacyquote></quote>");
                    Regex replace3 = new Regex(@"</legacyquote>");
                    Regex replace4 = new Regex(@"<legacyquote>");
                    //-----------------------ここまで
                    ////Skypeデフォの引用のゴミを正規表現で削除---ここから
                    row[1] = replace1.Replace(row[1], "");
                    row[1] = replace2.Replace(row[1], "");
                    row[1] = replace3.Replace(row[1], "");
                    row[1] = replace4.Replace(row[1], "");
                    //--------------------------------------------ここまで

                    row[2] = TimestampDifference(_nowTimestamp, row[2]);//UNIXtimestampをhh:mmかss秒の形式に置き換え

                    list.Insert(0, new DB(row[0], row[1], row[2], row[3], row[4]));

                    //-----------------------背景色を変える---ここから
                    //if (Properties.Settings.Default.MyColor)//optionで自コメントに色設定がチェックついていたら
                    //{

                    //    if (Convert.ToString(dataGrid[3, Row].Value) == "2") dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.LightGray;
                    //}

                    //if (System == 1) dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.LightPink;//画像の時
                    //if (System == 2) dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.Thistle;//ファイルの時
                    //if (System == 3) dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.LightGreen;//URLの時

                    ////未読だったらstatus欄の値は3なので3だったら
                    //if (Convert.ToString(dataGridView[3, Row].Value) == "3") dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.NavajoWhite;


                    ////フィルター使用かつ文字強調がONの時
                    //if (Properties.Settings.Default.FilterCheck && Properties.Settings.Default.FStringCheck)
                    //{
                    //    CompareInfo ci = CultureInfo.CurrentCulture.CompareInfo;
                    //    string temp = Convert.ToString(dataGridView[1, Row].Value);//内容の取り出し
                    //    string temp2 = Properties.Settings.Default.FilterString;//調べたい文字の取り出し

                    //    //比較する
                    //    if (temp.IndexOf(temp2, StringComparison.OrdinalIgnoreCase) >= 0 || ci.IndexOf(temp, temp2, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType) >= 0)
                    //        dataGridView.Rows[Row].DefaultCellStyle.BackColor = Color.Crimson;
                    //}
                    //-----------------------------------------ここまで
                }
                if (reader != null)
                    reader.Close();
            }
            // データベース接続を閉じる
            if (connection != null)
                connection.Close();
        }
        private static void UpdateDispList(List<DB> list,DataGrid dataGrid, TextBlock Log)//Listをデータグリッドのアイテムにする
        {
            dataGrid.ItemsSource = new ReadOnlyCollection<DB>(list);
            DateTime dtNow = DateTime.Now;
            Log.Text = ("最終更新"+dtNow.ToShortTimeString());
        }

        private static string TimestampDifference(uint now, string st)//UNIXTimestampをhh:mmかss秒の形式に置き換え
        {
            Int64 nowtime = Convert.ToInt64(now);//今のUNIXTimestamp
            Int64 messetime = Convert.ToInt64(st.Substring(0, 10));//データのUnixTimestanp
            Int64 defference = nowtime - messetime;//時差(秒)

            string output;

            if (defference >= 60)//一分以上ならhh:mm形式
            {
                DateTime UNIXBase = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);//UNIXの定義時刻
                DateTime time = UNIXBase.AddSeconds(messetime).ToLocalTime();//日付とかの入った形に置換
                output = string.Format("{0}", time.ToShortTimeString());//hh:mm形式の部分だけ取り出し
                return output;

            }

            //一分未満ならss秒形式
            output = string.Format("{0}秒", defference);
            return output;
        }
        private static void copylog()//ログをSkypeのとこから手元にコピペ
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
                MessageBox.Show(ex.Message, "ファイル参照エラー");
            }
        }

        private static void scrool(DataGrid dataGrid)
        {
            dataGrid.ScrollIntoView(dataGrid.Items.GetItemAt(dataGrid.Items.Count - 1));

        }

    }
}

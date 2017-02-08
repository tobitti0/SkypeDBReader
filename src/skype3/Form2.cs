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

namespace SkypeDBReader
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.Columns.Add("ID", "会話ID");
            dataGridView.Columns.Add("name", "発言者");
            dataGridView.Columns.Add("message", "内容");
            dataGridView.Columns["ID"].DefaultCellStyle.WrapMode =
            DataGridViewTriState.True;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.Columns[0].Width = 120;
            dataGridView.Columns[1].Width = 70;

        }

        private void readlog()//ログを開いて目的のものを抜き出してグリッドビューに表示
        {
            {
                string databaseFilePath = "main.db";
                string commandText = "SELECT chatname,from_dispname,body_xml From Messages GROUP BY chatname ORDER BY id DESC limit 15;"; ;

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
                int n = 0;
                int Row = 0;


                if (reader.HasRows) // コマンドの実行後、行データを持つとき
                {
                    dataGridView.Rows.Clear();

                    while (reader.Read())
                    {
                        for (int j = 0; j < reader.FieldCount; ++j) // 行データのフィールド数だけ繰り返す
                        {
                            object obj = reader.GetValue(j);
                            if (obj == null)
                                DataString.Append("null,");
                            else if (obj.ToString() == "")
                                DataString.Append(string.Format("<SDBR>削除されたようです。,"));
                            else if (Regex.IsMatch(obj.ToString(), @"<URIObject\s+[^>]*type=""Picture.1""\s*uri\s*="))//この形は画像
                                DataString.Append(string.Format("<SDBR>画像のようです。,"));
                            else if (Regex.IsMatch(obj.ToString(), @"<URIObject\s+[^>]*type=""File.1""\s*uri\s*="))//この形はファイル
                                DataString.Append(string.Format("<SDBR>なにかのファイルのようです。,"));
                            else if (Regex.IsMatch(obj.ToString(), @"<a\s+[^>]*href\s*=\s*(?:(?<quot>[""'])(?<url>.*?)\k<quot>|" + @"(?<url>[^\s>]+))[^>]*>(?<text>.*?)</a>"))
                                DataString.Append(string.Format("<SDBR>URLのようです。,"));//この形はURL
                            else if (obj is string)
                                DataString.Append(string.Format("{0},", reader.GetString(j)));
                            else
                                DataString.Append(String.Format("{0},", obj));
                        }

                        //リスト項目の設定
                        string linedate = DataString.ToString();
                        string[] row = linedate.Split(',');
                        dataGridView.Rows.Add(row);
                        DataString.Clear();
                    }

                    if (reader != null)
                        reader.Close();
                }

                // データベース接続を閉じる
                if (connection != null)
                    connection.Close();
            }
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

        private void LoadButton_Click(object sender, EventArgs e)
        {
            copylog();
            readlog();
        }
    }
}

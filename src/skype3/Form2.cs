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

namespace SkypeDBReader
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns.Add("ID", "会話ID");
            dataGridView1.Columns.Add("name", "発言者");
            dataGridView1.Columns.Add("message", "内容");
            dataGridView1.Columns["ID"].DefaultCellStyle.WrapMode =
            DataGridViewTriState.True;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 70;

        }

        private void readlog()
        {
            {
                string databaseFilePath = "main.db";
                string commandText = "SELECT chatname,from_dispname,body_xml From Messages GROUP BY chatname ORDER BY id DESC limit 15;"; ;

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
                    MessageBox.Show(ex.Message, "コマンドの実行");
                }
                finally
                {
                    if (command != null)
                        command.Dispose();
                }

                if (reader == null)
                    return;

                var sb = new StringBuilder();
                var sb2 = new StringBuilder();
                int n = 0;
                var columnList = new List<string>();

                if (reader.HasRows)
                {
                    dataGridView1.Rows.Clear();

                    while (reader.Read())
                    {
                        if (reader.FieldCount > n)
                        {
                            columnList.Add(reader.GetName(n));
                            ++n;
                        }

                        for (int j = 0; j < reader.FieldCount; ++j)
                        {
                            object obj = reader.GetValue(j);

                            if (obj == null)
                                sb2.Append("null,");
                            else if (obj.ToString() == "")
                                sb2.Append("empty,");
                            else if (obj.ToString().StartsWith("<URIObject type="))
                                sb2.Append(string.Format("<System>なにかを送信したようです,"));
                            else if (obj is string)
                                sb2.Append(string.Format("{0},", reader.GetString(j)));
                            else
                                sb2.Append(String.Format("{0},", obj));
                        }

                        //リスト項目の設定
                        string linedate = sb2.ToString();
                        string[] row = linedate.Split(',');
                        dataGridView1.Rows.Add(row);
                        sb2.Clear();
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
        private void button1_Click(object sender, EventArgs e)
        {
            copylog();
            readlog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkypeDBReader
{
    public static class _Update
    {
        ///<summary>アップデートを既定のURLから確認し指定したLinkLabelとToolTipの情報を更新する</summary>
        public static void Check(LinkLabel LinkLabel,ToolTip ToolTip)//バックグラウンド処理の呼び出し
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_Completed);
            // バックグラウンド操作の実行
            bw.RunWorkerAsync(Tuple.Create<LinkLabel, ToolTip>(LinkLabel, ToolTip));
        }

        private static string VersionNet()//ネットのtxtから最新バージョンと変更内容を得る
        {
            string version = null;
            try
            {
                WebClient wc = new WebClient();
                Stream st = wc.OpenRead("https://raw.githubusercontent.com/tobitti0/SkypeDBReader/master/src/version.txt");
                StreamReader sr = new StreamReader(st);

                // URLからすべて読み取る
                var VersionNet = new StringBuilder();
                while (sr.Peek() >= 0)
                {
                    VersionNet.Append(string.Format("{0},", sr.ReadLine()));
                }
                version = VersionNet.ToString();

                // StreamとStreamReaderを閉じる
                sr.Close();
                st.Close();
            }
            catch (Exception)
            {
                // URLのファイルが見つからない等のエラーが発生
                version = "null";
            }

            return version;

        }

        private static void bw_DoWork(object sender, DoWorkEventArgs e)//バックグラウンドの処理
        {
            string result = VersionNet();//ネットのファイルを読んで文字列に収める
            var tuple = (Tuple<LinkLabel, ToolTip>)e.Argument;
            var LinkLabel = tuple.Item1;
            var tooltip = tuple.Item2;
            e.Result = Tuple.Create<LinkLabel,ToolTip, string>(LinkLabel, tooltip, result);//bw_Completedで受け取らせる
        }

        private static void bw_Completed(object sender, RunWorkerCompletedEventArgs e)//バック処理が完了した時
        {
            var tuple = (Tuple<LinkLabel, ToolTip, string>)e.Result;
            var LinkLabel = tuple.Item1;
            var tooltip = tuple.Item2;
            var Result = tuple.Item3;

            //e.Result = workのほうから受け取った値
            string[] version = Result.ToString().Split(',');//[,]で配列区切りにする

            if (e.Error != null)
            {
                // エラーが発生したらメッセ
                MessageBox.Show("エラーが発生しました\r\n" + e.Error, "バックグラウンドエラー");
            }

            if (version[0].ToString() == "null")
            {     //エラー
                LinkLabel.Text = ("NetWorkError");
            }
            else if (version[0].ToString() == Application.ProductVersion)
            {   //最新版
                LinkLabel.Text = ("");
            }
            else
            {//NewVersion
                LinkLabel.Text = ("新しいバージョンがあります" + version[0]);
                string message = null;
                for (int count = 0; count < version.Length - 1; count++)
                {//複数行あった場合改行してバルーンに表示する形式にする
                    if (count != 0) { message += "\r\n"; };
                    message += version[count];
                }
                //バルーンウィンドウにする
                tooltip.IsBalloon = true;
                //タイトル
                tooltip.ToolTipTitle = "更新内容";
                //アイコン
                tooltip.ToolTipIcon = ToolTipIcon.Info;
                //対象ととメッセージ
                tooltip.SetToolTip(LinkLabel, "Version " + message + "\r\n(クリックで配布ページへ移動します。)");
            }

        }



    }
}

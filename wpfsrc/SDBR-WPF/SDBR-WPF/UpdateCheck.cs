﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SkypeDBReader
{
    public static class _Update
    {
        ///<summary>
        ///アップデートを既定のURLから確認し指定したTextBllockに
        ///更新がなければ通常文字で
        ///更新があればLinkLabellとToolTipの情報を更新する
        /// </summary>
        public static void Check(TextBlock LinkLabel,TextBlock LinkLabel2, TextBlock ToolTip)//バックグラウンド処理の呼び出し
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_Completed);
            // バックグラウンド操作の実行
            bw.RunWorkerAsync(Tuple.Create<TextBlock,TextBlock, TextBlock>(LinkLabel, LinkLabel2, ToolTip));
        }

        private static string VersionNet()//ネットのtxtから最新バージョンと変更内容を得る
        {
            string version = null;
            try
            {
                WebClient wc = new WebClient();
                Stream st = wc.OpenRead("https://raw.githubusercontent.com/tobitti0/SkypeDBReader/master/wpfsrc/SDBR-WPF/version.txt");
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
            var tuple = (Tuple<TextBlock,TextBlock, TextBlock>)e.Argument;
            var LinkLabel = tuple.Item1;
            var LinkLabel2= tuple.Item2;
            var tooltip = tuple.Item3;
            e.Result = Tuple.Create<TextBlock, TextBlock,TextBlock, string>(LinkLabel, LinkLabel2, tooltip, result);//bw_Completedで受け取らせる
        }

        private static void bw_Completed(object sender, RunWorkerCompletedEventArgs e)//バック処理が完了した時
        {
            var tuple = (Tuple<TextBlock,TextBlock, TextBlock, string>)e.Result;
            var LinkLabel = tuple.Item1;
            var LinkLabel2 = tuple.Item2;
            var tooltip = tuple.Item3;
            var Result = tuple.Item4;
            var fullname = typeof(App).Assembly.Location;
            var info = System.Diagnostics.FileVersionInfo.GetVersionInfo(fullname);
            var ver = info.FileVersion;
            var nowversion = ver.ToString();
            ////e.Result = workのほうから受け取った値
            string[] version = Result.ToString().Split(',');//[,]で配列区切りにする

            if (e.Error != null)
            {
                // エラーが発生したらメッセ
                MessageBox.Show("エラーが発生しました\r\n" + e.Error, "バックグラウンドエラー");
            }

            if (version[0].ToString() == "null")
            {     //エラー
                LinkLabel.Text = ("NetWorkError");
                //メッセージ
                tooltip.Text = ("通信エラーが発生しました。");
            }
            else if (version[0].ToString() == nowversion)
            {   //最新版
                LinkLabel.Text = ("最新版です");
            }
            else
            {//NewVersion
                LinkLabel2.Text = ("新しいバージョンがあります");
                string message = null;
                for (int count = 0; count < version.Length - 1; count++)
                {//複数行あった場合改行してバルーンに表示する形式にする
                    if (count != 0) { message += "\r\n"; };
                    message += version[count];
                }

                //メッセージ
                tooltip.Text=("Version " + message + "\r\n(クリックで配布ページへ移動します。)");
             }

        }



    }
}

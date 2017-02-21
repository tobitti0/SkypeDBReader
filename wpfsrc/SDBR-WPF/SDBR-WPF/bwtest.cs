using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SkypeDBReader
{/*
    class bwtest
    {
        BackgroundWorker bw;

        public bwtest(Progressbar progressBar1)
        {
            progressBar1.Maximum = 100;

            bw = new BackgroundWorker();

            //BackgroundWorker が非同期のキャンセルをサポートしているかどうかを
            //示す値を取得または設定します。
            bw.WorkerSupportsCancellation = true;

            //プログレスバー関連
            //BackgroundWorker が進行状況の更新を報告できるかどうかを示す値を取得または設定します。
            bw.WorkerReportsProgress = true;

            //ReportProgress が呼び出されたときに発生します。
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);

            //RunWorkerAsync が呼び出されたときに発生します。 
            //実際にバックグラウンド処理を行うイベントハンドラ
            //イベントにフックする。
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);

            //バックグラウンド操作の完了時、キャンセル時、
            //またはバックグラウンド操作によって例外が発生したときに発生します。
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            //IsBusy - BackgroundWorker が非同期操作を実行中かどうかを示す値を取得します。
            if (!bw.IsBusy)
            {
                //バックグラウンド操作の実行を開始します。
                bw.RunWorkerAsync(100);
            }
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            int i = 0, count = (int)e.Argument;
            while (i < count)
            {
                Thread.Sleep(50);
                bw.ReportProgress(++i);
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string msg = string.Empty;

            if (e.Cancelled)
            {
                msg = "Cancelled";
            }
            else
            {
                msg = "Done";
            }

            MessageBox.Show(msg);

            progressBar1.Value = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                bw.CancelAsync();
            }
        }
    }
    */
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SkypeDBReader
{
    /// <summary>
    /// IdCheck.xaml の相互作用ロジック
    /// </summary>
    public partial class IdCheck : Window
    {
        readonly List<DB> SubDB;
        public IdCheck()
        {
            InitializeComponent();
            SubDB = new List<DB> { };
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CommandBinding_Executed_4(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        BackgroundWorker bw;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            LoadingAnimation.Visibility = Visibility.Visible;

            bw = new BackgroundWorker();

            bw.WorkerSupportsCancellation = false;

            bw.WorkerReportsProgress = false;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);

            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            ReadWriter.Subreader(SubDB);
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           ReadWriter.UpdateDispList(SubDB, dataGrid, Log);
            LoadingAnimation.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Activate();
        }
    }
}

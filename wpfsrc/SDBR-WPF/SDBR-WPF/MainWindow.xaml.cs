using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SkypeDBReader
{
    public partial class MainWindow : Window
    {
        readonly List<DB> DB;
        BackgroundWorker bw;
        public MainWindow()
        {
            InitializeComponent();
            DB = new List<DB> { };
            _Update.Check(VersionBoxNormal,VersionBox, VersionToolTip);//アップデート情報を取得および更新案内を設定

            if(Properties.Settings.Default.FirstRun==true) ContentRendered += (s, e) => { WindowControl.FirstSetUpStart(this); };

            bw = new BackgroundWorker();

            bw.WorkerSupportsCancellation = false;

            bw.WorkerReportsProgress = false;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);

            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

        }

        FileSystemWatcher watcher = null;
        private void MonitoringStartButton_Click(object sender, EventArgs e)
        {
            BWread();

            //DB探すのにIDいる&設定から持ってくる
            string skypeID = Properties.Settings.Default.SkypeID;

            if (watcher != null) return;

            watcher = new System.IO.FileSystemWatcher();
            //監視するディレクトリを指定
            watcher.Path = @System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Skype\" + skypeID;
            //最終アクセス日時、最終更新日時、ファイル、フォルダ名の変更を監視する
            watcher.NotifyFilter =
                (System.IO.NotifyFilters.LastAccess
                | System.IO.NotifyFilters.LastWrite);
            //すべてのファイルを監視
            watcher.Filter = "main.db";
            //UIのスレッドにマーシャリングする
            //コンソールアプリケーションでの使用では必要ない
            //watcher.SynchronizingObject = this;

            //イベントハンドラの追加
            watcher.Changed += new System.IO.FileSystemEventHandler(watcher_Changed);
            watcher.Created += new System.IO.FileSystemEventHandler(watcher_Changed);
            watcher.Deleted += new System.IO.FileSystemEventHandler(watcher_Changed);

            //監視を開始する
            watcher.EnableRaisingEvents = true;

            //状態を表示
            LeftButtom.Text = "監視ちゅう(｀・ω・´)";

            //開始ボタンを押せなくして終了ボタンを押せるように
            MonitoringEndButton.IsEnabled = true;
            MonitoringStartButton.IsEnabled = false;
        }

        private void MonitoringEndButton_Click(object sender, RoutedEventArgs e)//監視を終了
        {
            //監視を終了
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
            watcher = null;
            //状態を表示
            LeftButtom.Text = "監視おわり( ˘ω˘)";
            //終了を押せなくして開始を押せるように
            MonitoringEndButton.IsEnabled = false;
            MonitoringStartButton.IsEnabled = true;
        }

        private void watcher_Changed(System.Object source, System.IO.FileSystemEventArgs e)//ファイルの変更があった時
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                BWread();
            }));
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            BWread();
        }
        
        private void hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
        
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }
        private void CommandBinding_Executed_3(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }
        private void CommandBinding_Executed_4(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void Menu_Option_Click(object sender, RoutedEventArgs e)
        {
            WindowControl.OptionStart(this);
        }
        private void Menu_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Menu_Read_Click(object sender, RoutedEventArgs e)
        {
            BWread();
        }
        private void Menu_About_Click(object sender, RoutedEventArgs e)
        {
            WindowControl.AboutStart(this);
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.Columns[0].Width = Properties.Settings.Default.DataGridNameWidth;
            Left = Properties.Settings.Default.MainWindow_Left;
            Top = Properties.Settings.Default.MainWindow_Top;
            Width = Properties.Settings.Default.MainWindow_Width;
            Height = Properties.Settings.Default.MainWindow_Height;
            //設定の中からカラーを適用
            ((App)Application.Current).ChangeBase(Properties.Settings.Default.BaseColor);
            ((App)Application.Current).ChangeAccent(Properties.Settings.Default.AccentColor);

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            dataGrid.FontSize = Properties.Settings.Default.FontSize;
            Visibility VisibleStatus;

            if (Properties.Settings.Default.TimeDisplay)
            {
                VisibleStatus = Visibility.Visible;
            }else
            {
                VisibleStatus = Visibility.Hidden;
            }
            dataGrid.Columns[2].Visibility = VisibleStatus;
        }

        private void Menu_Filter_Click(object sender, RoutedEventArgs e)
        {
            WindowControl.FilterStart(this);
        }



        private void BWread()
        {
            LoadingAnimation.Visibility = Visibility.Visible;

            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            ReadWriter.readwiter(DB);
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Properties.Settings.Default.FilterCheck)//フィルターがONの時
            {
                //情報を表示
                FilterStatus.Text = "フィルター有効(｀・д・´)";
            }
            else
            {
                FilterStatus.Text = "フィルター無効( ˘ω˘)";
            }

            ReadWriter.UpdateDispList(DB, dataGrid, Log);
            ReadWriter.scrool(dataGrid);

            LoadingAnimation.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                // ウィンドウの値を Settings に格納
                Properties.Settings.Default.DataGridNameWidth =Convert.ToInt16( dataGrid.Columns[0].Width.ToString());
                Properties.Settings.Default.MainWindow_Left = Left;
                Properties.Settings.Default.MainWindow_Top = Top;
                Properties.Settings.Default.MainWindow_Width = Width;
                Properties.Settings.Default.MainWindow_Height = Height;
                Properties.Settings.Default.Save();
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SDBR_WPF
{
    public partial class MainWindow : Window
    {
        readonly List<DB> DB;
        public MainWindow()
        {
            InitializeComponent();
            DB = new List<DB> { };
            _Update.Check(VersionBox, VersionToolTip);//アップデート情報を取得および更新案内を設定
            for (int i = 0; i < 50; i++)
            {
                MenuItem newMI = new MenuItem();
                newMI.Header = "Item " + i.ToString();
                Scroll.Items.Add(newMI);
            }
        }


        FileSystemWatcher watcher = null;
        private void MonitoringStartButton_Click(object sender, EventArgs e)
        {
            ReadWriter.readwiter(DB, FilterStatus, dataGrid, Log);
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
            //UnSelect();
            //終了を押せなくして開始を押せるように
            MonitoringEndButton.IsEnabled = false;
            MonitoringStartButton.IsEnabled = true;
        }

        private void watcher_Changed(System.Object source, System.IO.FileSystemEventArgs e)//ファイルの変更があった時
        {
            ReadWriter.readwiter(DB, FilterStatus, dataGrid, Log);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ReadWriter.readwiter(DB, FilterStatus, dataGrid, Log);
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
            //Restore.Visibility = System.Windows.Visibility.Collapsed;
            //Maximize.Visibility = System.Windows.Visibility.Visible;
        }
        private void CommandBinding_Executed_3(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
            //Maximize.Visibility = System.Windows.Visibility.Collapsed;
            //Restore.Visibility = System.Windows.Visibility.Visible;
        }
        private void CommandBinding_Executed_4(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}
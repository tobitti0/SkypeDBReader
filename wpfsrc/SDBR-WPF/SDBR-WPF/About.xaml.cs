using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// About.xaml の相互作用ロジック
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();

            //NameLabel.Content = Application.ProductName;//NameLabelに製品名を入れる
            //VersionLabel.Content = ("Version:" + Application.ProductVersion);//VarsionLabelにバージョンを入れる

            //--------------情報を取得-----------ここから
            System.Reflection.AssemblyTitleAttribute asmttl =(System.Reflection.AssemblyTitleAttribute)Attribute.
                GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(),typeof(System.Reflection.AssemblyTitleAttribute));
            var fullname = typeof(App).Assembly.Location;
            var info = System.Diagnostics.FileVersionInfo.GetVersionInfo(fullname);
            var ver = info.FileVersion;
            System.Reflection.AssemblyCopyrightAttribute asmcpy = (System.Reflection.AssemblyCopyrightAttribute)Attribute.
                GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(), typeof(System.Reflection.AssemblyCopyrightAttribute));
            //-------------------------------------------ここまで
            NameLabel.Content = asmttl.Title;
            VersionLabel.Content = ("Version:" + ver);
            CRLabel.Content = asmcpy.Copyright.ToString();//CRLabelに取得したコピーライトを入れる
            MITLabel.Text = "Released under the MIT license";//MITLabelに定型文
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CommandBinding_Executed_4(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Activate();
        }
    }
}

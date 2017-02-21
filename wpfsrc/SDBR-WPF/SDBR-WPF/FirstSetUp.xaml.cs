using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// FirstSetUp.xaml の相互作用ロジック
    /// </summary>
    public partial class FirstSetUp : Window
    {
        public FirstSetUp()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SkypeID = SkypeID.Text;
            Properties.Settings.Default.Save();
            Label_SkypeID.Content = Properties.Settings.Default.SkypeID;
            button2.IsEnabled = true;
            button1.IsEnabled = true;
            button3.IsEnabled = true;
            ChatID.IsEnabled = true;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.FirstRun = false;
            Properties.Settings.Default.SkypeID = SkypeID.Text;
            Properties.Settings.Default.ChatID = ChatID.Text;
            Properties.Settings.Default.MessageRow = "11";
            Properties.Settings.Default.Save();
            Close();
            this.Owner.IsEnabled = true;
        }

        static private IdCheck _IdCheckWindow = null;
        public void IdCheckStart()
        {
            if (_IdCheckWindow == null)
            {
                _IdCheckWindow = new IdCheck();
                _IdCheckWindow.Closed += (s, e) => _IdCheckWindow = null;
                _IdCheckWindow.Owner = this;
                _IdCheckWindow.Show();
            }
            _IdCheckWindow.Activate();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            IdCheckStart();
        }
    }
}

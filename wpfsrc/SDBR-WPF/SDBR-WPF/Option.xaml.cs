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
    /// Option.xaml の相互作用ロジック
    /// </summary>
    public partial class Option : Window
    {
        public Option()
        {
            InitializeComponent();
        }

        private void BaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((App)Application.Current).ChangeBase(BaseComboBox.SelectedIndex);
        }

        private void AccentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((App)Application.Current).ChangeAccent(AccentComboBox.SelectedIndex);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CommandBinding_Executed_4(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SkypeIDBox.Text = Properties.Settings.Default.SkypeID;
            ChatIDBox.Text = Properties.Settings.Default.ChatID;
            MessageRowBox.Text = Properties.Settings.Default.MessageRow;
            FontSizeBox.Text = Properties.Settings.Default.FontSize.ToString();
            MyColorBox.IsChecked = Properties.Settings.Default.MyColor;
            TimeDisplayBox.IsChecked = Properties.Settings.Default.TimeDisplay;
            AccentComboBox.SelectedIndex= Properties.Settings.Default.AccentColor;
            BaseComboBox.SelectedIndex = Properties.Settings.Default.BaseColor;


        }

        private void ApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SkypeID = SkypeIDBox.Text;
            Properties.Settings.Default.ChatID = ChatIDBox.Text;
            Properties.Settings.Default.MessageRow = MessageRowBox.Text;
            int FontSize= int.Parse(FontSizeBox.Text);
            Properties.Settings.Default.FontSize= FontSize;
            Properties.Settings.Default.MyColor = (bool)MyColorBox.IsChecked;
            Properties.Settings.Default.TimeDisplay = (bool)TimeDisplayBox.IsChecked;
            Properties.Settings.Default.AccentColor= AccentComboBox.SelectedIndex;
            Properties.Settings.Default.BaseColor= BaseComboBox.SelectedIndex;

            Properties.Settings.Default.Save();
        }

        private void FontSizeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool yes_parse = false;
            {
                    float xx;
                    var tmp = FontSizeBox.Text + e.Text;
                    yes_parse = Single.TryParse(tmp, out xx);
                
            }

            e.Handled = !yes_parse;
        }

        private void MessageRowBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool yes_parse = false;
            {
                float xx;
                var tmp = FontSizeBox.Text + e.Text;
                yes_parse = Single.TryParse(tmp, out xx);

            }

            e.Handled = !yes_parse;
        }

        private void IdCheck_Click(object sender, RoutedEventArgs e)
        {
            WindowControl.IdCheckStart(this);
        }

    }
}


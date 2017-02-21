﻿using System;
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
    /// Filter.xaml の相互作用ロジック
    /// </summary>
    public partial class Filter : Window
    {
        public Filter()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FilterSkypeID.Text = Properties.Settings.Default.FilterSkypeID;
            FilterString.Text = Properties.Settings.Default.FilterString;
            FilterCheck.IsChecked = Properties.Settings.Default.FilterCheck;
            FSkypeCheck.IsChecked = Properties.Settings.Default.FSkypeCheck;
            FStringCheck.IsChecked = Properties.Settings.Default.FStringCheck;
            FSkypeCheck.IsEnabled = (bool)FilterCheck.IsChecked;
            FilterSkypeID.IsEnabled = (bool)FilterCheck.IsChecked;
            FStringCheck.IsEnabled = (bool)FilterCheck.IsChecked;
            FilterString.IsEnabled = (bool)FilterCheck.IsChecked;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.FilterSkypeID = FilterSkypeID.Text;
            Properties.Settings.Default.FilterString = FilterString.Text;
            Properties.Settings.Default.FilterCheck = (bool)FilterCheck.IsChecked;
            Properties.Settings.Default.FSkypeCheck = (bool)FSkypeCheck.IsChecked;
            Properties.Settings.Default.FStringCheck = (bool)FStringCheck.IsChecked;
            Properties.Settings.Default.Save();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FilterCheck_Checked(object sender, RoutedEventArgs e)
        {
            FSkypeCheck.IsEnabled = (bool)FilterCheck.IsChecked;
            FilterSkypeID.IsEnabled = (bool)FilterCheck.IsChecked;
            FStringCheck.IsEnabled = (bool)FilterCheck.IsChecked;
            FilterString.IsEnabled = (bool)FilterCheck.IsChecked;
        }
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CommandBinding_Executed_4(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Activate();
        }
    }
}

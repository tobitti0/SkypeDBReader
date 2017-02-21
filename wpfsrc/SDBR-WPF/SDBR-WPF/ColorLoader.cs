using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkypeDBReader
{
    public partial class App : Application
    {
        private ResourceDictionary Base;
        private ResourceDictionary Accent;


        public void ChangeBase(int num)
        {
            if (Base == null)
            {
                // 新しいリソース・ディクショナリを追加
                Base = new ResourceDictionary();
                Application.Current.Resources.MergedDictionaries.Add(Base);
            }
            string[] ColorBase = { "Light", "Dark"};
            // WPFテーマをリソース・ディクショナリのソースに指定
            string themeUri = String.Format(
              "pack://application:,,,/Generic/BaseColor/{0}.xaml", ColorBase[num]);
            Base.Source = new Uri(themeUri);
        }
        public void ChangeAccent(int num)
        {
            if (Accent == null)
            {
                // 新しいリソース・ディクショナリを追加
                Accent = new ResourceDictionary();
                Application.Current.Resources.MergedDictionaries.Add(Accent);
            }
            string[] ColorAccent = { "Blue", "Orange", "Purple" };
            // WPFテーマをリソース・ディクショナリのソースに指定
            string themeUri = String.Format(
              "pack://application:,,,/Generic/AccentColor/{0}.xaml", ColorAccent[num]);
            Accent.Source = new Uri(themeUri);
        }
    }
}
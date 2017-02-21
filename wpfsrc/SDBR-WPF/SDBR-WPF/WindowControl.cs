using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SkypeDBReader
{   /// <summary>
    /// Windoの起動状態を管理する
    /// </summary>
    class WindowControl
    {

        static private Option _OptionWindow = null;
        static private FirstSetUp _FirstSetUpWindow = null;
        static private Filter _FilterWindow = null;
        static private IdCheck _IdCheckWindow = null;
        static private About _AboutWindow = null;


        public static void OptionStart(Window Ownerwindow)
        {
            if (_OptionWindow == null)
            {
                _OptionWindow = new Option();
                _OptionWindow.Closed += (s, e) => _OptionWindow = null;
                _OptionWindow.Owner = Ownerwindow;
                _OptionWindow.Show();
            }
            _OptionWindow.Activate();
        }

        public static void FirstSetUpStart(Window Ownerwindow)
        {
            if (_FirstSetUpWindow == null)
            {
                _FirstSetUpWindow = new FirstSetUp();
                _FirstSetUpWindow.Closed += (s, e) => _FirstSetUpWindow = null;
                _FirstSetUpWindow.Owner = Ownerwindow;
                _FirstSetUpWindow.Show();
            }
            _FirstSetUpWindow.Activate();
        }

        public static void FilterStart(Window Ownerwindow)
        {
            if (_FilterWindow == null)
            {
                _FilterWindow = new Filter();
                _FilterWindow.Closed += (s, e) => _FilterWindow = null;
                _FilterWindow.Owner = Ownerwindow;
                _FilterWindow.Show();
            }
            _FilterWindow.Activate();
        }

        public static void IdCheckStart(Window Ownerwindow)
        {
            if (_IdCheckWindow == null)
            {
                _IdCheckWindow = new IdCheck();
                _IdCheckWindow.Closed += (s, e) => _IdCheckWindow = null;
                _IdCheckWindow.Owner = Ownerwindow;
                _IdCheckWindow.Show();
            }
            _IdCheckWindow.Activate();
        }

        public static void AboutStart(Window Ownerwindow)
        {
            if (_AboutWindow == null)
            {
                _AboutWindow = new About();
                _AboutWindow.Closed += (s, e) =>_AboutWindow = null;
                _AboutWindow.Owner = Ownerwindow;
                _AboutWindow.Show();
            }
            _AboutWindow.Activate();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace SkypeDBReader
{

    public class DB
    {
        public string Id { get; private set; }
        public string Message { get; private set; }
        public string System { get; private set; }
        public string Time { get; private set; }
        public string Status { get; private set; }
        public string ChatID { get; private set; }
        

        public DB(string id, string message, string time,string status, string system,string chatid)
        {
            this.Id = id;
            this.Message = message;
            this.System = system;
            this.Time = time;
            this.Status = status;
            this.ChatID = chatid;

        }

        public int StatusNumber
        {
            get
            {
                if (Properties.Settings.Default.MyColor)
                {
                    if (Status == "2") return 1;
                }
                if (Status == "3") return 2;
                return 0;
            }
        }
        public int SystemNumber
        {
            get
            {
                if (System == "1") return 1;

                if (System == "2") return 2;

                if (System == "3") return 3;

                if (System == "4") return 4;
                return 0;
            }
        }
        public int FilterNumber
        {
            get
            {
                if (Properties.Settings.Default.FilterCheck && Properties.Settings.Default.FStringCheck)
                {
                    CompareInfo ci = CultureInfo.CurrentCulture.CompareInfo;
                    string temp2 = Properties.Settings.Default.FilterString;//調べたい文字の取り出し
                    //比較する

                    if (Message.IndexOf(temp2, StringComparison.OrdinalIgnoreCase) >= 0 || ci.IndexOf(Message, temp2, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType) >= 0)
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }
         //-----------------------------------------ここまで
        }
}

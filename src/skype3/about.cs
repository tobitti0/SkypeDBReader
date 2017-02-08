using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkypeDBReader
{
    public partial class about : Form
    {

        public about()
        {
            InitializeComponent();
            //-----Windowの設定--------ここから
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //-------------------------ここまで

            NameLabel.Text = Application.ProductName;//NameLabelに製品名を入れる
            VersionLabel.Text = ("Version:" + Application.ProductVersion );//VarsionLabelにバージョンを入れる

            //--------------コピーライトを取得-----------ここから
            System.Reflection.AssemblyCopyrightAttribute asmcpy =(System.Reflection.AssemblyCopyrightAttribute)Attribute.
                GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(),typeof(System.Reflection.AssemblyCopyrightAttribute));
            //-------------------------------------------ここまで

            CRLabel.Text = asmcpy.Copyright.ToString();//CRLabelに取得したコピーライトを入れる
            MITLabel.Text="Released under the MIT license";//MITLabelに定型文
        }

        private void MITLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//MITLabelがクリックされたとき
        {
            System.Diagnostics.Process.Start("https://github.com/tobitti0/SkypeDBReader/blob/master/LICENSE");//LICENSE開く

        }
    }
}

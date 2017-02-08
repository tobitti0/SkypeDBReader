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
    public partial class FirstSetUP : Form
    {
        public FirstSetUP()
        {
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            button2.Enabled = false;
            button3.Enabled = false;
            label12.Text = (Application.ProductName + "  Version:" + Application.ProductVersion);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SkypeID = SkypeID.Text;
            Properties.Settings.Default.Save();
            Label_SkypeID.Text = Properties.Settings.Default.SkypeID;
            button2.Enabled = true;
            button3.Enabled = true;
        }
        private Form2 _Form2 = null;//ウインド状況管理用
        private void button2_Click(object sender, EventArgs e)
        {
            if (this._Form2 == null || this._Form2.IsDisposed)
                this._Form2 = new Form2();

            if (!this._Form2.Visible)
            {
                _Form2.Owner = this;
                _Form2.StartPosition = FormStartPosition.CenterParent;
                this._Form2.Show();
            }
            else
            {
                this._Form2.Activate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SkypeID = SkypeID.Text;
            Properties.Settings.Default.ChatID = ChatID.Text;
            Properties.Settings.Default.FirstRun = false;
            Properties.Settings.Default.MessageRow = "11";
            Properties.Settings.Default.Save();

            Close();

        }

        private void FirstSetUP_Load(object sender, EventArgs e)
        {
            SkypeID.Select();
        }
    }
}

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
    public partial class Option : Form
    {
        public Option()
        {
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Option_Load(object sender, EventArgs e)
        {
            SkypeID.Text = Properties.Settings.Default.SkypeID;
            ChatID.Text = Properties.Settings.Default.ChatID;
            MessageRow.Text = Properties.Settings.Default.MessageRow;
            MyColor_Check.Checked = Properties.Settings.Default.MyColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SkypeID = SkypeID.Text;
            Properties.Settings.Default.ChatID = ChatID.Text;
            Properties.Settings.Default.MessageRow = MessageRow.Text;
            Properties.Settings.Default.MyColor = MyColor_Check.Checked;
            Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

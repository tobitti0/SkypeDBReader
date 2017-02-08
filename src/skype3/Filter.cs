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
    public partial class Filter : Form
    {
        public Filter()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            FilterSkypeID.Text = Properties.Settings.Default.FilterSkypeID;
            FilterString.Text = Properties.Settings.Default.FilterString;
            FilterCheck.Checked = Properties.Settings.Default.FilterCheck;
            FSkypeCheck.Checked = Properties.Settings.Default.FSkypeCheck;
            FStringCheck.Checked = Properties.Settings.Default.FStringCheck;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FilterSkypeID = FilterSkypeID.Text;
            Properties.Settings.Default.FilterString = FilterString.Text;
            Properties.Settings.Default.FilterCheck = FilterCheck.Checked;
            Properties.Settings.Default.FSkypeCheck = FSkypeCheck.Checked;
            Properties.Settings.Default.FStringCheck = FStringCheck.Checked;
            Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FilterCheck_CheckedChanged(object sender, EventArgs e)
        {
            FSkypeCheck.Enabled = FilterCheck.Checked;
            FilterSkypeID.Enabled = FilterCheck.Checked;
            FStringCheck.Enabled = FilterCheck.Checked;
            FilterString.Enabled = FilterCheck.Checked;

        }
    }
}

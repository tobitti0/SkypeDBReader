﻿using System;
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
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            label4.Text = Application.ProductName;
            label2.Text = ("Version:" + Application.ProductVersion );

            System.Reflection.AssemblyCopyrightAttribute asmcpy =(System.Reflection.AssemblyCopyrightAttribute)Attribute.
                GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(),typeof(System.Reflection.AssemblyCopyrightAttribute));


        }
    }
}

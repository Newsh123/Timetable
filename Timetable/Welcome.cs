﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timetable
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CreateOrLoad fm = new CreateOrLoad();
            this.Hide();
            fm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

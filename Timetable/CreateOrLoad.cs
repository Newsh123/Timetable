using System;
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
    public partial class CreateOrLoad : Form
    {
        public CreateOrLoad()
        {
            InitializeComponent();
        }

        private void CreateTimetable_Click(object sender, EventArgs e)
        {
            InsertFiles fm = new InsertFiles(this);
            this.Hide();
            fm.Show();
        }

        private void LoadTimetable_Click(object sender, EventArgs e)
        {
            LoadFile fm = new LoadFile(this);
            this.Hide();
            fm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

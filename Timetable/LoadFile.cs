using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Timetable
{
    public partial class LoadFile : Form
    {
        private string filePath;
        private CreateOrLoad prev;
        public LoadFile(CreateOrLoad previous)
        {
            prev = previous;
            InitializeComponent();
        }

        private void Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = Path.Combine(@"~\database");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = Path.GetFileName(dialog.FileName);
                path = path + fileName;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.Copy(dialog.FileName, path);
                filePath = path;
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            InputChecker input = new InputChecker(filePath, Days.Text, Periods.Text);
            if (input.valid())
            {
                Reader timetable = new Reader(filePath);
                int periods = 0;
                Dictionary<string, List<string[]>> result = timetable.readTimetable(ref periods);
                DatabaseConnection database = new DatabaseConnection(result, periods + 1);
                OutputFile fm = new OutputFile(Convert.ToInt32(Days.Text), Convert.ToInt32(Periods.Text));
                this.Hide();
                fm.Show();
            }
            else
            {
                MessageBox.Show("You have not entered a valid input.\nPlease try again.", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }
    }
}

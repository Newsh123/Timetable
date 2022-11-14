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
    public partial class ShowLesson : Form
    {
        private OutputFile prev;
        public ShowLesson(List<string> studentNames, string teacherName, OutputFile previous)
        {
            prev = previous;
            InitializeComponent();
            teacher.Text = teacherName;
            students.Text = "";
            foreach (string studentName in studentNames)
            {
                students.Text = students.Text + $"{studentName}, ";
            }
            students.Text = students.Text.Substring(0, students.Text.Length - 2);
        }

        private void teacher_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }
    }
}

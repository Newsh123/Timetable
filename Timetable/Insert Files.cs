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
    public partial class InsertFiles : Form
    {
        private string studentPath = null, teacherPath = null;
        private CreateOrLoad prev;
        public InsertFiles(CreateOrLoad previous)
        {
            prev = previous;
            InitializeComponent();
        }


        private void InsertFiles_Load(object sender, EventArgs e)
        {
            

        }

        private string createFile()
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
                return path;
            }
            return null;
        }

        private void studentButton_Click(object sender, EventArgs e)
        {
            studentPath = createFile();
        }
        private void teacherButton_Click(object sender, EventArgs e)
        {
            teacherPath = createFile();
        }

        private void submit_Click(object sender, EventArgs e)
        {
        InputChecker input = new InputChecker(studentPath, teacherPath, Days.Text, Periods.Text, classSize.Text);    
            if (input.valid())
            {
                Reader students = new Reader(studentPath);
                Reader teachers = new Reader(teacherPath);
                students.ReadStudents();
                teachers.ReadTeachers();
                List<string[]> studentList = students.getTable();
                List<string[]> teacherList = teachers.getTable();
                AddHours fm = new AddHours(studentList, teacherList, Convert.ToInt32(Days.Text), Convert.ToInt32(Periods.Text), Convert.ToInt32(classSize.Text), this);
                this.Hide();
                fm.Show();
            }
            else
            {
                MessageBox.Show("You have not entered a valid input.\nPlease try again.", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }
    }
}

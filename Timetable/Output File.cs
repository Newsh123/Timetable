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
    public partial class OutputFile : Form
    {
        DatabaseConnection database = new DatabaseConnection();
        int schoolDays, schoolPeriods;
        public OutputFile(int days, int periods)
        {
            schoolDays = days;
            schoolPeriods = periods;
            InitializeComponent();
        }

        private void Output_File_Load(object sender, EventArgs e)
        {

        }

        private void download_Click(object sender, EventArgs e)
        {
            XMLWriter writer = new XMLWriter(database);
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                writer.writeFile(dialog.SelectedPath);
            }
        }

        private void SearchLesson_Click(object sender, EventArgs e)
        {
            InputChecker input = new InputChecker(database, LessonCode.Text);
            if (input.valid())
            {
                string id = LessonCode.Text;
                List<string> students = database.getStudents(id, schoolDays * schoolPeriods);
                string teacher = database.getTeacher(id);
                ShowLesson fm = new ShowLesson(students, teacher, this);
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
            CreateOrLoad home = new CreateOrLoad();
            this.Hide();
            home.Show();
        }

        private void SearchPerson_Click(object sender, EventArgs e)
        {
            InputChecker input = new InputChecker(database, FirstName.Text, LastName.Text, Type.Text);
            if (input.valid())
            {
                int id;
                if (Type.Text.ToLower() == "student")
                {
                    id = database.findStudent(FirstName.Text, LastName.Text);
                }
                else
                {
                    id = database.findTeacher(FirstName.Text, LastName.Text);
                }
                string[] timetable = new string[schoolDays * schoolPeriods];
                for (int i = 0; i < timetable.Length; i++)
                {
                    string command = $"SELECT period_{i} FROM {Type.Text.ToLower()}_timetable WHERE {Type.Text.ToLower()}_id={id}";
                    string lesson = database.executeReadCommand(command);
                    if (lesson == null)
                    {
                        timetable[i] = "";
                    }
                    else
                    {
                        timetable[i] = lesson;
                    }
                }
                ShowTimetable fm = new ShowTimetable(FirstName.Text, LastName.Text, timetable, schoolDays, schoolPeriods, this);
                this.Hide();
                fm.Show();
            }
            else
            {
                MessageBox.Show("You have not entered a valid input.\nPlease try again.", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }
    }
}

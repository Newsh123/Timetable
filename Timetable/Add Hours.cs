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
    public partial class AddHours : Form
    {
        private List<string[]> studentList, teacherList;
        private int schoolDays, schoolPeriods, maxClassSize;
        private InsertFiles prev;

        public AddHours(List<string[]> students, List<string[]> teachers, int days, int periods, int classSize, InsertFiles previous)
        {
            prev = previous;
            studentList = students;
            teacherList = teachers;
            schoolDays = days;
            schoolPeriods = periods;
            maxClassSize = classSize;
            InitializeComponent();
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void SumbitButton_Click(object sender, EventArgs e)
        {
            InputChecker input = new InputChecker(addPE.Checked, Maths.Text, English.Text, Science.Text, Language.Text, Humanities.Text, Option_1.Text, Option_2.Text, PE.Text, schoolDays * schoolPeriods);
            if (input.valid())
            {
                Dictionary<string, int> subjectGroups = new Dictionary<string, int>
                {
                    {"Maths", Convert.ToInt32(Maths.Text) },
                    {"English", Convert.ToInt32(English.Text) },
                    {"Science", Convert.ToInt32(Science.Text) },
                    {"Language", Convert.ToInt32(Language.Text) },
                    {"Humanities", Convert.ToInt32(Humanities.Text) },
                    {"Option_1", Convert.ToInt32(Option_1.Text) },
                    {"Option_2", Convert.ToInt32(Option_2.Text) },
                };
                if (addPE.Checked)
                {
                    subjectGroups.Add("PE", Convert.ToInt32(PE.Text));
                }
                DatabaseConnection database = new DatabaseConnection(studentList, teacherList, schoolDays * schoolPeriods);
                Timetable timetable = new Timetable(database, maxClassSize, subjectGroups);
                if (timetable.wasSuccess())
                {
                    timetable.createTimetables();
                    this.Hide();
                    OutputFile fm = new OutputFile(schoolDays, schoolPeriods);
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("The data that you have inputted did not produce a valid timetable.\nPlease make sure that there are enough teachers to be able to teach the number of lessons you would require.", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not entered a valid input.\nPlease try again.", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
    }
}

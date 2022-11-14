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
    public partial class ShowTimetable : Form
    {
        private OutputFile prev;

        public ShowTimetable(string firstName, string lastName, string[] lessons, int days, int periods, OutputFile previous)
        {
            prev = previous;
            InitializeComponent();
            intro.Text = $"{char.ToUpper(firstName[0]) + firstName.Substring(1)} {char.ToUpper(lastName[0]) + lastName.Substring(1)}'s Timetable";
            table.ColumnCount = days;
            table.RowCount = periods;
            table.Update();
            for (int i = 2; i < days; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                table.ColumnStyles[i].Width = 50;
            }
            for (int i = 2; i < periods; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent));
                table.RowStyles[i].Height = 50;
            }
            for (int i = 0; i < days; i++)
            {
                for (int j = 0; j < periods; j++)
                {
                    Label temp = new Label();
                    temp.Name = $"{i} {j}";
                    temp.Dock = DockStyle.Fill;
                    temp.AutoSize = false;
                    temp.TextAlign = ContentAlignment.MiddleCenter;
                    temp.Font = new Font("Segoe UI", 14);
                    if (lessons[i * 5 + j] != null)
                    {
                        temp.Text = lessons[i * periods + j];
                    }
                    else
                    {
                        temp.Text = "No Lesson";
                    }
                    table.Controls.Add(temp, i, j);
                }
            }

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }
    }
}

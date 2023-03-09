using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    class Student : Person
    {
        Dictionary<string, int> subjects;

        public Student(string[] details, DatabaseConnection databaseConnection) : base(details, databaseConnection)
        {
            subjects = new Dictionary<string, int>
            {
                { "Maths", lessonGroup(details[3]) },
                { "English", lessonGroup(details[5])},
                { details[6], lessonGroup(details[6]) },
                { details[7], lessonGroup(details[7]) },
                { details[8], lessonGroup(details[8]) },
                { details[9], lessonGroup(details[9]) }
            };
            addScience(details[4]);
        }
        private int lessonGroup(string lesson)
        {
            int group;
            bool endsWithNumber = int.TryParse(lesson.Substring(lesson.Length - 1), out group);
            if (endsWithNumber)
            {
                return group;
            }
            else
            {
                return 0;
            }
        }

        private void addScience(string science)
        {
            string level = science.Substring(0, science.Length - 2);
            int group = Convert.ToInt32(science.Substring(science.Length - 1, 1));
            if (level == "Combined")
            {
                subjects.Add(level, group);
            }
            else if (level == "Triple")
            {
                subjects.Add("Physics", group);
                subjects.Add("Biology", group);
                subjects.Add("Chemistry", group);
            }
        }

        public string createTimetable(List<Lesson>[] masterTimetable)
        {
            int i = 0;
            foreach (List<Lesson> lessons in masterTimetable)
            {
                foreach (Lesson lesson in lessons)
                {
                    if (lesson.getStudentList().Contains(this))
                    {
                        timetable[i] = lesson.getCode();
                    }
                }
                i++;
            }

            string command = "UPDATE student_timetable SET ";
            int periods = database.getPeriods();
            for (int j = 0; j < periods - 1; j++)
            {
                command = command + $"period_{j} = '{timetable[j]}', ";
            }
            command = command + $"period_{periods - 1} = '{timetable[periods - 1]}' WHERE student_id = {id};";
            return command;
        }

        public Dictionary<string, int> getSubjects()
        {
            return subjects;
        }
    }
}

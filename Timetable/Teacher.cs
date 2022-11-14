using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    class Teacher : Person
    {
        private string subject;
        private int partTime, scienceCount = 0, capacity;
        private List<string> groups = new List<string>();
        public Teacher(string[] details, DatabaseConnection databaseConnection) : base(details, databaseConnection)
        {
            subject = details[3];
            partTime = Convert.ToInt32(details[4]);
            capacity = (database.getPeriods() * 100) / partTime;
        }

        public void createTimetable(int period, string code)
        {
            timetable[period] = code;
        }

        public string getInsertString()
        {
            string command = "UPDATE teacher_timetable SET ";
            for (int i = 0; i < timetable.Length; i++)
            {
                if (timetable[i] != null)
                {
                    command = command + $"period_{i} = '{timetable[i]}', ";
                }
            }
            command = command.Substring(0, command.Length - 2) + $" WHERE teacher_id = {id};";
            if (command.Length < 55)
            {
                command = "";
            }
            return command;
        }

        public void nowTeaching(string subjectGroup)
        {
            groups.Add(subjectGroup);
        }

        public bool alreadyTeaching(string subjectGroup)
        {
            if (groups.Contains(subjectGroup))
            {
                return true;
            }
            return false;
        }

        public bool hasCapacity(int hours)
        {
            if (capacity - hours >= 0)
            {
                capacity -= hours;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addToTally()
        {
            scienceCount++;
        }

        public bool checkTally()
        {
            if (scienceCount == 3)
            {
                return false;
            }
            return true;
        }

        public int getTally()
        {
            return scienceCount;
        }

        public string getSubject()
        {
            return subject;
        }
    }
}

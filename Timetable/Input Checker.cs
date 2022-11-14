using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    class InputChecker
    {
        private bool validInputs = true;
        private int total = 0;

        // Insert Files
        public InputChecker(string studentPath, string teacherPath, string days, string periods, string classSize)
        { 
            if (studentPath == null || teacherPath == null)
            {
                validInputs = false;
            }
            checkDays(days);
            checkPeriods(periods);
            checkClassSize(classSize);
            if (validInputs)
            {
                checkStudentPath(studentPath);
                checkTeacherPath(teacherPath);
            }
        }

        // Add Hours
        public InputChecker(bool hasPE, string maths, string english, string science, string language, string humanities, string option1, string option2, string PE, int totalPeriods)
        {
            checkMaths(maths);
            checkEnglish(english);
            checkScience(science);
            checkLanguage(language);
            checkHumanities(humanities);
            checkOption1(option1);
            checkOption2(option2);
            if (hasPE)
            {
                checkPE(PE);
            }
            checkTotal(totalPeriods);
        }

        // Load File
        public InputChecker(string timetablePath, string days, string periods)
        {
            if (timetablePath == null)
            {
                validInputs = false;
            }
            checkDays(days);
            checkPeriods(periods);
            if (validInputs)
            {
                checkTimetablePath(timetablePath, Convert.ToInt32(days) * Convert.ToInt32(periods));
            }
        }

        // Output Person
        public InputChecker(DatabaseConnection database, string firstName, string lastName, string type)
        {
            string command = $"SELECT {type.ToLower()}_id FROM {type.ToLower()}_list WHERE first_name='{firstName}' AND last_name='{lastName}';";
            string result = database.executeReadCommand(command);
            if (result == null)
            {
                validInputs = false;
            }
        }

        // Output Lesson
        public InputChecker(DatabaseConnection database, string lessonCode)
        {
            string command = $"SELECT student_count FROM lessons WHERE lesson_id='{lessonCode}';";
            string result = database.executeReadCommand(command);
            if (result != null)
            {
                if (Convert.ToInt32(result) == 0)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkStudentPath (string check)
        {
            Reader students = new Reader(check);
            bool validFile = students.checkStudents();
            if (!validFile)
            {
                validInputs = false;
            }
        }

        private void checkTeacherPath(string check)
        {
            Reader teachers = new Reader(check);
            bool validFile = teachers.checkTeachers();
            if (!validFile)
            {
                validInputs = false;
            }
        }

        private void checkTimetablePath(string check, int totalPeriods)
        {
            Reader timetable = new Reader(check);
            bool validFile = timetable.checkTimetable(totalPeriods);
            if (!validFile)
            {
                validInputs = false;
            }
        }

        private void checkDays(string check)
        {
            int days;
            bool isInt = int.TryParse(check, out days);
            if (isInt)
            {
                if (days < 2 || days > 10)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkPeriods(string check)
        {
            int periods;
            bool isInt = int.TryParse(check, out periods);
            if (isInt)
            {
                if (periods < 2 || periods > 10)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkClassSize(string check)
        {
            int classSize;
            bool isInt = int.TryParse(check, out classSize);
            if (isInt)
            {
                if (classSize < 15 || classSize > 50)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkMaths(string check)
        {
            int maths;
            bool isInt = int.TryParse(check, out maths);
            if (isInt)
            {
                total = total + maths;
                if (maths < 0)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkEnglish(string check)
        {
            int english;
            bool isInt = int.TryParse(check, out english);
            if (isInt)
            {
                total = total + english;
                if (english < 0)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkScience(string check)
        {
            int science;
            bool isInt = int.TryParse(check, out science);
            if (isInt)
            {
                total = total + science;
                if (science < 0 || science % 3 != 0)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkLanguage(string check)
        {
            int language;
            bool isInt = int.TryParse(check, out language);
            if (isInt)
            {
                total = total + language;
                if (language < 0)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkHumanities(string check)
        {
            int humanities;
            bool isInt = int.TryParse(check, out humanities);
            if (isInt)
            {
                total = total + humanities;
                if (humanities < 0)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkOption1(string check)
        {
            int option1;
            bool isInt = int.TryParse(check, out option1);
            if (isInt)
            {
                total = total + option1;
                if (option1 < 0)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkOption2(string check)
        {
            int option2;
            bool isInt = int.TryParse(check, out option2);
            if (isInt)
            {
                total = total + option2;
                if (option2 < 0)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkPE(string check)
        {
            int PE;
            bool isInt = int.TryParse(check, out PE);
            if (isInt)
            {
                total = total + PE;
                if (PE < 0 || PE > 4)
                {
                    validInputs = false;
                }
            }
            else
            {
                validInputs = false;
            }
        }

        private void checkTotal(int totalPeriods)
        {
            if (totalPeriods != total)
            {
                validInputs = false;
            }
        }

        public bool valid()
        {
            return validInputs;
        }
    }
}

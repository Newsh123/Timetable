using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    class Lesson
    {
        private DatabaseConnection database;
        private int teachingGroup;
        private string subject, subjectGroup, lessonCode;
        private Teacher teacher;
        private List<Student> studentList;

        public Lesson(string[] details, DatabaseConnection databaseConnection)
        {
            database = databaseConnection;
            setSubject(details[1]);
            subjectGroup = details[2];
            studentList = new List<Student>();
        }

        public Lesson(string code, string subjectName, string group, List<Student> students, DatabaseConnection databaseConnection)
        {
            database = databaseConnection;
            lessonCode = code;
            subject = subjectName;
            subjectGroup = group;
            studentList = students;
        }

        private void setSubject(string subj)
        {
            if (Convert.ToInt32(subj[subj.Length - 1]) >= 48 && Convert.ToInt32(subj[subj.Length - 1]) <= 57)
            {
                subject = subj.Substring(0, subj.Length - 2);
                teachingGroup = Convert.ToInt32(subj.Substring(subj.Length - 1));
            }
            else
            {
                subject = subj;
                teachingGroup = 0;
            }
        }

        public void addStudent(Student student)
        {
            studentList.Add(student);
        }

        public void addTeacher(Teacher teacherToAdd)
        {
            teacher = teacherToAdd;
        }

        public string getSuffix(string grouping)
        {
            Dictionary<string, string> subjectGroups = new Dictionary<string, string>
            {
                {"Maths", "a" },
                {"Science", "b" },
                {"English", "c" },
                {"Language", "d" },
                {"Humanities", "e" },
                {"Option_1", "f" },
                {"Option_2", "g" }
            };
            string suffix;
            if (subjectGroups.ContainsKey(grouping))
            {
                suffix = subjectGroups[grouping];
            }
            else
            {
                suffix = "h";
            }
            return suffix;
        }

        public void incrementCode()
        {
            lessonCode = $"{lessonCode.Substring(0, 4)}{Convert.ToInt32(lessonCode.Substring(4, 1)) + 1}{lessonCode.Substring(5, lessonCode.Length - 5)}";
        }

        public List<Lesson> split(int i)
        {
            List<Student>[] newStudentLists = new List<Student>[i];
            for (int j = 0; j < i; j++)
            {
                newStudentLists[j] = new List<Student>();
            }
            int k = 0;
            foreach (Student student in studentList)
            {
                newStudentLists[k % i].Add(student);
                k++;
            }
            List<Lesson> newLessons = new List<Lesson>();
            for (int j = 0; j < i; j++)
            {
                newLessons.Add(new Lesson($"{subject.Substring(0, 3)}{teachingGroup}{j}{getSuffix(subjectGroup)}", subject, subjectGroup, newStudentLists[j], database));
            }
            return newLessons;
        }

        public List<Student> removeStudents(List<Student> students) 
        {
            List<Student> removedStudents = new List<Student>();
            foreach (Student student in students)
            {
                if (studentList.Contains(student))
                {
                    removedStudents.Add(student);
                    studentList.Remove(student);
                }
            }
            return removedStudents;
        }

        public void merge(Lesson lesson)
        {
            foreach (Student student in lesson.getStudentList())
            {
                addStudent(student);
            }
        }

        public string getInsertString()
        {
           return $"INSERT INTO lessons VALUES ('{lessonCode}', '{subject}', '{subjectGroup}', {teacher.getId()}, {studentList.Count});";
        }

        public int getStudentCount()
        {

            return studentList.Count;
        }

        public string getSubject()
        {
            return subject;
        }

        public string getSubjectGroup()
        {
            return subjectGroup;
        }

        public List<Student> getStudentList()
        {
            List<Student> newList = new List<Student>();
            foreach (Student student in studentList)
            {
                newList.Add(student);
            }
            return newList;
        }

        public string getCode()
        {
            return lessonCode;
        }

        public int getTeacherId()
        {
            return teacher.getId();
        }

        public int getTeachingGroup()
        {
            return teachingGroup;
        }
    }
}

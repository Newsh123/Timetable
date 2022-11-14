using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Timetable
{
    class XMLWriter
    {
        private string xmlFile = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
        public XMLWriter(DatabaseConnection database)
        {
            xmlFile = xmlFile + "<Timetable>\n";
            List<string[]> timetable = database.getTimetable();
            List<int> listStarts = new List<int>();
            for (int i = 0; i < timetable.Count; i++)
            {
                if (timetable[i][0] == "0")
                {
                    listStarts.Add(i);
                }
                else if (!int.TryParse(timetable[i][0], out _))
                {
                    listStarts.Add(i);
                    break;
                }
            }
            for (int i = 0; i < listStarts[1]; i++)
            {
                string[] student = timetable[i];
                xmlFile = xmlFile + $"<Student>\n<ID>{student[0]}</ID>\n<FirstName>{student[1]}</FirstName>\n<LastName>{student[2]}</LastName>\n<Lessons>\n";
                for (int j = 3; j < student.Length; j++)
                {
                    xmlFile = xmlFile + $"<Period_{j - 3}>{student[j]}</Period_{j - 3}>\n";
                }
                xmlFile = xmlFile + $"</Lessons>\n</Student>\n";
            }
            for (int i = listStarts[1]; i < listStarts[2]; i++)
            {
                string[] teacher = timetable[i];
                xmlFile = xmlFile + $"<Teacher>\n<ID>{teacher[0]}</ID>\n<FirstName>{teacher[1]}</FirstName>\n<LastName>{teacher[2]}</LastName>\n<Lessons>\n";
                for (int j = 3; j < teacher.Length; j++)
                {
                    if (teacher[j] != "")
                    {
                        xmlFile = xmlFile + $"<Period_{j - 3}>{teacher[j]}</Period_{j - 3}>\n";
                    }
                }
                xmlFile = xmlFile + $"</Lessons>\n</Teacher>\n";
            }
            for (int i = listStarts[2]; i < timetable.Count; i++)
            {
                string[] lesson = timetable[i];
                xmlFile = xmlFile + $"<Lesson>\n<ID>{lesson[0]}</ID>\n<Subject>{lesson[1]}</Subject>\n<Subject_Group>{lesson[2]}</Subject_Group>\n<Teacher_ID>{lesson[3]}</Teacher_ID>\n<Student_Count>{lesson[4]}</Student_Count>\n</Lesson>\n";
            }
            xmlFile = xmlFile + "</Timetable>";
        }

        public void writeFile(string path)
        {
            string fileName = path + "/timetable.xml";
            File.WriteAllText(fileName, xmlFile);
        }
    }
}

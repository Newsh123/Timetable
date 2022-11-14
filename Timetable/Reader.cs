using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Timetable
{
    class Reader
    {
        private List<string[]> table;
        private string path;

        public Reader(string filePath)
        {
            path = filePath;
        }

        public bool checkStudents()
        {
            XmlTextReader students = new XmlTextReader(path);
            List<string> temp = new List<string>();
            string content;
            while (students.Read())
            {
                if (students.NodeType == XmlNodeType.Element)
                {
                    switch (students.Name)
                    {
                        case "FirstName":
                            if (temp.Count > 0)
                            {
                                if (temp.Count != 9)
                                {
                                    students.Close();
                                    return false;
                                }
                            }
                            temp = new List<string>();
                            content = students.ReadElementContentAsString();
                            if (!content.Contains(" "))
                            {
                                temp.Add(content);
                            }
                            else
                            {
                                students.Close();
                                return false;
                            }
                            break;
                        case "LastName":
                            content = students.ReadElementContentAsString();
                            if(!content.Contains(" "))
                            {
                                temp.Add(content);
                            }
                            else
                            {
                                students.Close();
                                return false;
                            }
                            break;
                        case "Maths":
                            content = students.ReadElementContentAsString();
                            if (content.Substring(0, content.Length - 2) == "Maths")
                            {
                                int group;
                                bool isInt = int.TryParse(content.Substring(content.Length - 1), out group);
                                if (isInt)
                                {
                                    temp.Add(content);
                                }
                                else 
                                {
                                    students.Close();
                                    return false;
                                }
                            }
                            else
                            {
                                students.Close();
                                return false;
                            }
                            break;
                        case "English":
                            content = students.ReadElementContentAsString();
                            if (content.Substring(0, content.Length - 2) == "English")
                            {
                                int group;
                                bool isInt = int.TryParse(content.Substring(content.Length - 1), out group);
                                if (isInt)
                                {
                                    temp.Add(content);
                                }
                                else
                                {
                                    students.Close();
                                    return false;
                                }
                            }
                            else
                            {
                                students.Close();
                                return false;
                            }
                            break;
                        case "Science":
                            content = students.ReadElementContentAsString();
                            if (content.Substring(0, content.Length - 2) == "Triple" || content.Substring(0, content.Length - 2) == "Combined")
                            {
                                int group;
                                bool isInt = int.TryParse(content.Substring(content.Length - 1), out group);
                                if (isInt)
                                {
                                    temp.Add(content);
                                }
                                else
                                {
                                    students.Close();
                                    return false;
                                }
                            }
                            else
                            {
                                students.Close();
                                return false;
                            }
                            break;
                        case "Language":
                            content = students.ReadElementContentAsString();
                            temp.Add(content);
                            break;
                        case "Humanities":
                            content = students.ReadElementContentAsString();
                            if (content == "Geography" || content == "History")
                            {
                                temp.Add(content);
                            }
                            else
                            {
                                students.Close();
                                return false;
                            }
                            break;
                        case "Option_1":
                            content = students.ReadElementContentAsString();
                            temp.Add(content);
                            break;
                        case "Option_2":
                            content = students.ReadElementContentAsString();
                            temp.Add(content);
                            break;
                    }
                }
            }
            students.Close();
            return true;
        }

        public void ReadStudents()
        {
            table = new List<string[]>();
            XmlTextReader students = new XmlTextReader(path);
            int count = -1;
            while (students.Read())
            {
                if (students.NodeType == XmlNodeType.Element)
                {
                    switch (students.Name)
                    {
                        case "FirstName":
                            table.Add(new string[9]);
                            count++;
                            table[count][0] = students.ReadElementContentAsString();
                            break;
                        case "LastName":
                            table[count][1] = students.ReadElementContentAsString();
                            break;
                        case "Maths":
                            table[count][2] = students.ReadElementContentAsString();
                            break;
                        case "Science":
                            table[count][3] = students.ReadElementContentAsString();
                            break;
                        case "English":
                            table[count][4] = students.ReadElementContentAsString();
                            break;
                        case "Language":
                            table[count][5] = students.ReadElementContentAsString();
                            break;
                        case "Humanities":
                            table[count][6] = students.ReadElementContentAsString();
                            break;
                        case "Option_1":
                            table[count][7] = students.ReadElementContentAsString();
                            break;
                        case "Option_2":
                            table[count][8] = students.ReadElementContentAsString();
                            break;
                    }
                }
            }
            students.Close();
        }

        public bool checkTeachers()
        {
            XmlTextReader teachers = new XmlTextReader(path);
            List<string> temp = new List<string>();
            string content;
            while (teachers.Read())
            {
                if (teachers.NodeType == XmlNodeType.Element)
                {
                    switch (teachers.Name)
                    {
                        case "FirstName":
                            if (temp.Count > 0)
                            {
                                if (temp.Count != 4)
                                {
                                    teachers.Close();
                                    return false;
                                }
                            }
                            temp = new List<string>();
                            content = teachers.ReadElementContentAsString();
                            if (!content.Contains(" "))
                            {
                                temp.Add(content);
                            }
                            else
                            {
                                teachers.Close();
                                return false;
                            }
                            break;
                        case "LastName":
                            content = teachers.ReadElementContentAsString();
                            if (!content.Contains(" "))
                            {
                                temp.Add(content);
                            }
                            else
                            {
                                teachers.Close();
                                return false;
                            }
                            break;
                        case "Subject":
                            content = teachers.ReadElementContentAsString();
                            temp.Add(content);
                            break;
                        case "PartTime":
                            content = teachers.ReadElementContentAsString();
                            int partTime;
                            bool isInt = int.TryParse(content, out partTime);
                            if (isInt)
                            {
                                if (partTime >= 0 && partTime <= 100)
                                {
                                    temp.Add(content);
                                }
                                else
                                {
                                    teachers.Close();
                                    return false;
                                }
                            }
                            else
                            {
                                teachers.Close();
                                return false;
                            }
                            break;
                    }
                }
            }
            teachers.Close();            
            return true;
        }

        public void ReadTeachers()
        {
            table = new List<string[]>();
            XmlTextReader teachers = new XmlTextReader(path);
            int count = -1;
            while (teachers.Read())
            {
                if (teachers.NodeType == XmlNodeType.Element)
                {
                    switch (teachers.Name)
                    {
                        case "FirstName":
                            table.Add(new string[5]);
                            count++;
                            table[count][0] = teachers.ReadElementContentAsString();
                            break;
                        case "LastName":
                            table[count][1] = teachers.ReadElementContentAsString();
                            break;
                        case "Subject":
                            table[count][2] = teachers.ReadElementContentAsString();
                            break;
                        case "PartTime":
                            table[count][3] = teachers.ReadElementContentAsString();
                            break;
                    }
                }
            }
            teachers.Close();
        }

        public bool checkTimetable(int periods)
        {
            string[] parts = new string[] { "Student", "Teacher", "Lesson" };
            string[] acceptedNames = new string[] { "FirstName", "LastName", "Subject", "Subject_Group", "Teacher_ID", "Student_Count" };
            int counter = -1;
            XmlTextReader timetable = new XmlTextReader(path);
            List<string> temp = new List<string>();
            string content;
            while (timetable.Read())
            {
                if (timetable.NodeType == XmlNodeType.Element)
                {

                    if (timetable.Name == "ID")
                    {
                        content = timetable.ReadElementContentAsString();
                        if (content == "0" || (!int.TryParse(content, out _) && counter < 2))
                        {
                            counter++;
                        }
                        if (temp.Count > 0)
                        {
                            if (parts[counter] == "Student" && temp.Count != 3 + periods)
                            {
                                timetable.Close();
                                return false;
                            }
                            else if (parts[counter] == "Teacher" && temp.Count < 3)
                            {
                                timetable.Close();
                                return false;
                            }
                            else if (parts[counter] == "Lesson" && temp.Count != 5 && !int.TryParse(temp[0], out _))
                            {
                                timetable.Close();
                                return false;
                            }
                        }
                        temp = new List<string>();
                        temp.Add(content);
                    }
                    else if (acceptedNames.Contains(timetable.Name))
                    {
                        content = timetable.ReadElementContentAsString();
                        temp.Add(content);
                    }
                    else if (timetable.Name.Length > 7)
                    {
                        if (timetable.Name.Substring(0, 7) == "Period_")
                        {
                            int periodNum = Convert.ToInt32(timetable.Name.Substring(7, timetable.Name.Length - 7));
                            if (periodNum > periods)
                            {
                                periods = periodNum;
                            }
                            while (temp.Count < periodNum + 3)
                            {
                                temp.Add(null);
                            }
                            temp.Add(timetable.ReadElementContentAsString());
                        }
                    }
                }
            }
            timetable.Close();
            return true;
        }

        public Dictionary<string, List<string[]>> readTimetable(ref int periods)
        {
            Dictionary<string, List<string[]>> result = new Dictionary<string, List<string[]>>()
            {
                {"Students", new List<string[]>() },
                {"Teachers", new List<string[]>() },
                {"Lessons", new List<string[]>() }
            };
            List<string> acceptedNames = new List<string>() { "FirstName", "LastName", "Subject", "Subject_Group", "Teacher_ID", "Student_Count" };
            int counter = -1;
            XmlTextReader timetable = new XmlTextReader(path);
            List<string> temp = null;
            while (timetable.Read())
            {
                if (timetable.NodeType == XmlNodeType.Element)
                {

                    if (timetable.Name == "ID")
                    {
                        if (temp != null)
                        {
                            result[result.Keys.ToList()[counter]].Add(temp.ToArray());
                        }
                        string id = timetable.ReadElementContentAsString();
                        if (id == "0" || (!int.TryParse(id, out _) && counter < 2))
                        {
                            counter++;
                        }
                        temp = new List<string>();
                        temp.Add(id);
                    }
                    else if (acceptedNames.Contains(timetable.Name))
                    {
                        temp.Add(timetable.ReadElementContentAsString());
                    }
                    else if (timetable.Name.Length > 7)
                    {
                        if (timetable.Name.Substring(0, 7) == "Period_")
                        {
                            int periodNum = Convert.ToInt32(timetable.Name.Substring(7, timetable.Name.Length - 7));
                            if (periodNum > periods)
                            {
                                periods = periodNum;
                            }
                            while (temp.Count < periodNum + 3)
                            {
                                temp.Add(null);
                            }
                            temp.Add(timetable.ReadElementContentAsString());
                        }
                    }
                }
            }
            result["Lessons"].Add(temp.ToArray());
            timetable.Close();
            return result;
        }

        public List<string[]> getTable()
        {
            return table;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace Timetable
{
    class DatabaseConnection
    {
        private int numPeriods;
        private string createTimetables;
        private MySqlConnection database = new MySqlConnection();
        private MySqlCommand cmd = new MySqlCommand();
        private MySqlDataReader result;

        public DatabaseConnection(List<string[]> students, List<string[]> teachers, int periods)
        {
            numPeriods = periods;
            database.ConnectionString = "server=localhost;user id=root;password=Er1414Er;persistsecurityinfo=True;database=\"TimetableCreator\"";
            openDatabase();
            cmd.Connection = database;
            if (checkIfExists())
            {
                dropTables();
            }
            else
            {
                createTables();
            }
            insertStudents(students);
            insertTeachers(teachers);
            createLessons();
            closeDatabase();
        }

        public DatabaseConnection(Dictionary<string, List<string[]>> timetable, int periodNum)
        {
            numPeriods = periodNum;
            database.ConnectionString = "server=localhost;user id=root;password=Er1414Er;persistsecurityinfo=True;database=\"TimetableCreator\"";
            openDatabase();
            cmd.Connection = database;
            if (checkIfExists())
            {
                dropTables();
            }
            else
            {
                createTables();
            }
            cmd.CommandText = "ALTER TABLE lessons MODIFY COLUMN lesson_id varchar(255);";
            cmd.ExecuteNonQuery();
            closeDatabase();
            createNewTables();
            string sListCommand = "";
            string sTimeCommand = "";
            foreach (string[] student in timetable["Students"])
            {
                sListCommand = sListCommand + $"INSERT INTO student_list(student_id, first_name, last_name) VALUES ({student[0]}, '{student[1]}', '{student[2]}');";
                sTimeCommand = sTimeCommand + "INSERT INTO student_timetable(student_id{0}) VALUES ({1}{2});";
                string periods = ", ";
                string lessons = ", ";
                for (int i = 3; i < student.Length; i++)
                {
                    periods = periods + $"period_{i - 3},";
                    lessons = lessons + $"'{student[i]}',";
                }
                periods = periods.Substring(0, periods.Length - 1);
                lessons = lessons.Substring(0, lessons.Length - 1);
                sTimeCommand = string.Format(sTimeCommand, periods, student[0], lessons);
            }
            string tListCommand = "";
            string tTimeCommand = "";
            foreach (string[] teacher in timetable["Teachers"])
            {
                tListCommand = tListCommand + $"INSERT INTO teacher_list(teacher_id, first_name, last_name) VALUES ({teacher[0]}, '{teacher[1]}', '{teacher[2]}');";
                tTimeCommand = tTimeCommand + "INSERT INTO teacher_timetable(teacher_id{0}) VALUES ({1}{2});";
                string periods = ", ";
                string lessons = ", ";
                for (int i = 3; i < teacher.Length; i++)
                {
                    if (teacher[i] != null)
                    {
                        periods = periods + $"period_{i - 3}, ";
                        lessons = lessons + $"'{teacher[i]}', ";
                    }
                }
                periods = periods.Substring(0, periods.Length - 2);
                lessons = lessons.Substring(0, lessons.Length - 2);
                tTimeCommand = string.Format(tTimeCommand, periods, teacher[0], lessons);
            }
            string lCommand = "";
            foreach (string[] lesson in timetable["Lessons"])
            {
                lCommand = lCommand + $"INSERT INTO lessons(lesson_id, subject_name, subject_group, teacher_id, student_count) VALUES ('{lesson[0]}', '{lesson[1]}', '{lesson[2]}', {lesson[3]}, {lesson[4]});";
            }
            openDatabase();
            cmd.CommandText = sListCommand + tListCommand + lCommand + sTimeCommand + tTimeCommand;
            cmd.ExecuteNonQuery();
            closeDatabase();
        }

        public DatabaseConnection()
        {
            database.ConnectionString = "server=localhost;user id=root;password=Er1414Er;persistsecurityinfo=True;database=\"TimetableCreator\"";
            cmd.Connection = database;
        }

        private bool checkIfExists()
        {
            cmd.CommandText = "SHOW TABLES LIKE \"student_list\"";
            result = cmd.ExecuteReader();
            if (result.Read())
            {
                result.Close();
                return true;
            }
            else
            {
                result.Close();
                return false;
            }
        }

        private void dropTables()
        {
            cmd.CommandText = "SHOW TABLES LIKE \"student_timetable\"";
            result = cmd.ExecuteReader();
            if (result.Read())
            {
                result.Close();
                cmd.CommandText = "DROP TABLE student_timetable; DROP TABLE teacher_timetable;DROP TABLE lessons;DROP TABLE student_list;DROP TABLE teacher_list;";
                cmd.ExecuteNonQuery();
            }
            else
            {
                result.Close();
                cmd.CommandText = "DROP TABLE lessons;DROP TABLE student_list;DROP TABLE teacher_list;";
                cmd.ExecuteNonQuery();
            }
            createTables();
        }

        private void createTables()
        {
            string command;
            string fkString = "";
            cmd.CommandText = "CREATE TABLE student_list (" +
                "student_id int NOT NULL," +
                "first_name varchar(255) NOT NULL," +
                "last_name varchar(255) NOT NULL," +
                "maths varchar(255)," +
                "science varchar(255)," +
                "english varchar(255)," +
                "language varchar(255)," +
                "humanities varchar(255)," +
                "option_1 varchar(255)," +
                "option_2 varchar(255)," +
                "PRIMARY KEY (student_id)" +
                ");";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "CREATE TABLE teacher_list (" +
                "teacher_id int NOT NULL," +
                "first_name varchar(255) NOT NULL," +
                "last_name varchar(255) NOT NULL," +
                "subject varchar(255)," +
                "part_time int," +
                "PRIMARY KEY (teacher_id)" +
                ");";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "CREATE TABLE lessons (" +
                "lesson_id int NOT NULL," +
                "subject_name varchar(255) NOT NULL," +
                "subject_group varchar(255)," +
                "teacher_id int," +
                "student_count int," +
                "PRIMARY KEY (lesson_id)," +
                "FOREIGN KEY (teacher_id) REFERENCES teacher_list(teacher_id)" +
                ");";
            cmd.ExecuteNonQuery();

            command = "CREATE TABLE student_timetable (" +
                "student_id int NOT NULL,";
            for (int i = 0; i < numPeriods; i++)
            {
                command = command + $"period_{i} varchar(255),";
                fkString = fkString + $"FOREIGN KEY (period_{i}) REFERENCES lessons(lesson_id),";
            }
            createTimetables = command + fkString + "FOREIGN KEY (student_id) REFERENCES student_list(student_id) );";

            command = "CREATE TABLE teacher_timetable (" +
                "teacher_id int NOT NULL,";
            fkString = "";
            for (int i = 0; i < numPeriods; i++)
            {
                command = command + $"period_{i} varchar(255),";
                fkString = fkString + $"FOREIGN KEY (period_{i}) REFERENCES lessons(lesson_id),";
            }
            createTimetables = createTimetables + command + fkString + "FOREIGN KEY (teacher_id) REFERENCES teacher_list(teacher_id) );";
        }

        private void insertStudents(List<string[]> students)
        {
            string command = "";
            int count = 0;
            foreach (string[] student in students)
            {
                command = command + "INSERT INTO student_list " +
                    $"VALUES ({count}, '{student[0]}', '{student[1]}', '{student[2]}', '{student[3]}', '{student[4]}', '{student[5]}', '{student[6]}', '{student[7]}', '{student[8]}');";
                count++;
            }
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();

        }
        private void insertTeachers(List<string[]> teachers)
        {
            string command = "";
            int count = 0;
            foreach (string[] teacher in teachers)
            {
                command = command + "INSERT INTO teacher_list " +
                    $"VALUES ({count}, '{teacher[0]}', '{teacher[1]}', '{teacher[2]}', '{teacher[3]}');";
                count++;
            }
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
        }

        private void createLessons()
        {
            List<string> classes = new List<string>();
            List<string> subjectGroups = new List<string>();
            string[] commands = new string[] {
                "SELECT DISTINCT Maths FROM student_list;",
                "SELECT DISTINCT Science FROM student_list;",
                "SELECT DISTINCT English FROM student_List;",
                "SELECT DISTINCT Language FROM student_list;",
                "SELECT DISTINCT Humanities FROM student_list;",
                "SELECT DISTINCT Option_1 FROM student_list;",
                "SELECT DISTINCT Option_2 FROM student_list;",
            };
            foreach (string command in commands)
            {
                cmd.CommandText = command;
                result = cmd.ExecuteReader();
                while (result.Read())
                {
                    classes.Add(result.GetString(0));
                    subjectGroups.Add(command.Substring(16, command.Length - 35));
                }
                result.Close();
            }
            for (int i = 0; i < classes.Count; i++)
            {
                if (classes[i].Substring(0, classes[i].Length - 2) == "Triple")
                {
                    classes[i] = $"Physics {classes[i][classes[i].Length - 1]}";
                    classes.Add($"Biology {classes[i][classes[i].Length - 1]}");
                    classes.Add($"Chemistry {classes[i][classes[i].Length - 1]}");
                    subjectGroups.Add("Science");
                    subjectGroups.Add("Science");
                }
            }
            string com = "";
            for (int i = 0; i < classes.Count; i++)
            {
                com = com + $"INSERT INTO lessons (lesson_id, subject_name, subject_group) VALUES ({i}, '{classes[i]}', '{subjectGroups[i]}');";
            }
            cmd.CommandText = com;
            cmd.ExecuteNonQuery();
        }

        public void executeCommand(string command)
        {
            openDatabase();
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
            closeDatabase();
        }

        public string executeReadCommand(string command)
        {
            openDatabase();
            cmd.CommandText = command;
            result = cmd.ExecuteReader();
            result.Read();
            try
            {
                if (result.IsDBNull(0))
                {
                    closeDatabase();
                    return null;
                }
                string res = result.GetString(0);
                closeDatabase();
                return res;
            }
            catch (MySqlException)
            {
                closeDatabase();
                return null;
            }
                
        }

        public void createNewTables()
        {
            openDatabase();
            cmd.CommandText = createTimetables;
            cmd.ExecuteNonQuery();
            closeDatabase();
        }

        public void openDatabase()
        {
            database.Open();
        }

        public void closeDatabase()
        {
            database.Close();
        }

        public int findStudent(string firstName, string lastName)
        {
            openDatabase();
            cmd.CommandText = $"SELECT student_id FROM student_list WHERE first_name='{firstName}' AND last_name='{lastName}';";
            result = cmd.ExecuteReader();
            result.Read();
            int id = result.GetInt32(0);
            result.Close();
            closeDatabase();
            return id;
        }

        public int findTeacher(string firstName, string lastName)
        {
            openDatabase();
            cmd.CommandText = $"SELECT teacher_id FROM teacher_list WHERE first_name='{firstName}' AND last_name='{lastName}';";
            result = cmd.ExecuteReader();
            result.Read();
            int id = result.GetInt32(0);
            result.Close();
            closeDatabase();
            return id;
        }

        public List<string[]> getStudents()
        {
            List<string[]> studentList = new List<string[]>();
            cmd.CommandText = "SELECT * FROM student_list";
            result = cmd.ExecuteReader();
            while (result.Read())
            {
                string[] temp = new string[]
                {
                    result.GetString(0),
                    result.GetString(1),
                    result.GetString(2),
                    result.GetString(3),
                    result.GetString(4),
                    result.GetString(5),
                    result.GetString(6),
                    result.GetString(7),
                    result.GetString(8),
                    result.GetString(9)
                };
                studentList.Add(temp);
            }
            result.Close();
            return studentList;
        }

        public List<string> getStudents(string lessonId, int periods)
        {
            openDatabase();
            List<string> studentNames = new List<string>();
            for (int i = 0; i < periods; i++)
            {
                cmd.CommandText = $"SELECT first_name, last_name FROM student_list LEFT JOIN student_timetable ON student_list.student_id=student_timetable.student_id WHERE period_{i}='{lessonId}';";
                result = cmd.ExecuteReader();
                while (result.Read())
                {
                    if (!result.IsDBNull(0))
                    {
                        studentNames.Add($"{result.GetString(0)} {result.GetString(1)}");
                    }
                }
                result.Close();
                if (studentNames.Count > 0)
                {
                    break;
                }
            }
            closeDatabase();
            return studentNames;
        }

        public string getTeacher(string lessonId)
        {
            openDatabase();
            cmd.CommandText = $"SELECT first_name, last_name FROM teacher_list RIGHT JOIN lessons ON teacher_list.teacher_id=lessons.teacher_id WHERE lesson_id='{lessonId}';";
            result = cmd.ExecuteReader();
            result.Read();
            string name =  $"{result.GetString(0)}  {result.GetString(1)}";
            result.Close();
            closeDatabase();
            return name;
        }

        public List<string[]> getTeachers()
        {
            List<string[]> teacherList = new List<string[]>();
            cmd.CommandText = "SELECT * FROM teacher_list";
            result = cmd.ExecuteReader();
            while (result.Read())
            {
                string[] temp = new string[]
                {
                    result.GetString(0),
                    result.GetString(1),
                    result.GetString(2),
                    result.GetString(3),
                    result.GetString(4),
                };
                teacherList.Add(temp);
            }
            result.Close();
            return teacherList;
        }

        public List<string[]> getLessons()
        {
            List<string[]> lessonList = new List<string[]>();
            cmd.CommandText = "SELECT * FROM lessons";
            result = cmd.ExecuteReader();
            while (result.Read())
            {
                string[] temp = new string[]
                {
                    result.GetString(0),
                    result.GetString(1),
                    result.GetString(2)
                };
                lessonList.Add(temp);
            }
            result.Close();
            return lessonList;
        }

        public List<string[]> getTimetable()
        {
            openDatabase();
            Dictionary<string, List<string>> students = new Dictionary<string, List<string>>();
            cmd.CommandText = "SELECT * FROM student_list;";
            result = cmd.ExecuteReader();
            while (result.Read())
            {
                students[result.GetString(0)] = new List<string>() { result.GetString(1), result.GetString(2) };
            }
            result.Close();
            cmd.CommandText = "SELECT * FROM student_timetable;";
            result = cmd.ExecuteReader();
            while (result.Read())
            {
                for (int i = 1; i < result.FieldCount; i++)
                {
                    students[result.GetString(0)].Add(result.GetString(i));
                }
            }
            result.Close();
            Dictionary<string, List<string>> teachers = new Dictionary<string, List<string>>();
            cmd.CommandText = "SELECT * FROM teacher_list;";
            result = cmd.ExecuteReader();
            while (result.Read())
            {
                teachers[result.GetString(0)] = new List<string>() { result.GetString(1), result.GetString(2) };
            }
            result.Close();
            cmd.CommandText = "SELECT * FROM teacher_timetable;";
            result = cmd.ExecuteReader();
            while (result.Read())
            {
                for (int i = 1; i < result.FieldCount; i++)
                {
                    bool isNull = result.IsDBNull(i);
                    if (isNull)
                    {
                        teachers[result.GetString(0)].Add("");
                    }
                    else
                    {
                        teachers[result.GetString(0)].Add(result.GetString(i));
                    }
                }
            }
            result.Close();
            List<string[]> lessons = new List<string[]>();
            cmd.CommandText = "SELECT * FROM lessons;";
            result = cmd.ExecuteReader();
            while (result.Read())
            {
                lessons.Add(new string[] { result.GetString(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetString(4) });
            }
            List<string[]> timetable = new List<string[]>();
            foreach (var student in students)
            {
                string[] temp = new string[student.Value.Count + 1];
                temp[0] = student.Key;
                for (int i = 0; i < student.Value.Count; i++)
                {
                    temp[i + 1] = student.Value[i];
                }
                timetable.Add(temp);
            }
            foreach (var teacher in teachers)
            {
                string[] temp = new string[teacher.Value.Count + 1];
                temp[0] = teacher.Key;
                for (int i = 0; i < teacher.Value.Count; i++)
                {
                    temp[i + 1] = teacher.Value[i];
                }
                timetable.Add(temp);
            }
            foreach (string[] lesson in lessons)
            {
                timetable.Add(lesson);
            }
            closeDatabase();
            return timetable;
        }

        public int getPeriods()
        {
            return numPeriods;
        }
    }
}

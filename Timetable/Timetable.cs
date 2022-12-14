using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    class Timetable
    {
        private bool PEFlag = false, success = false;
        private int maxClassSize, periods;
        private DatabaseConnection database;
        private string[] groupTimetable;
        private List<Lesson>[] masterTimetable;
        private List<Student> students = new List<Student>();
        private List<Teacher> teachers = new List<Teacher>();
        private List<Lesson> lessons = new List<Lesson>();
        private List<List<Lesson>[]> PETable;
        private string[] PESubjects = new string[] { "Language", "Humanities", "Option_1", "Option_2" };
        private Dictionary<string, List<Teacher>> subjectTeachers = new Dictionary<string, List<Teacher>>();
        private Dictionary<string, int> subjectGroups = new Dictionary<string, int>();

        public Timetable(DatabaseConnection databaseConnector, int classSize, Dictionary<string, int> subjects)
        {
            while (!success)
            {
                database = databaseConnector;
                maxClassSize = classSize;
                periods = database.getPeriods();
                subjectGroups = subjects;
                if (subjectGroups.ContainsKey("PE"))
                {
                    string[] temp = new string[subjectGroups["PE"]];
                    for (int i = 0; i < temp.Length; i++)
                    {
                        temp[i] = PESubjects[i];
                    }
                    PESubjects = temp;
                    PEFlag = true;
                }
                PETable = new List<List<Lesson>[]>();
                groupTimetable = new string[periods];
                masterTimetable = new List<Lesson>[periods];
                for (int i = 0; i < periods; i++)
                {
                    masterTimetable[i] = new List<Lesson>();
                }
                database.openDatabase();
                createStudents(database.getStudents());
                createTeachers(database.getTeachers());
                createLessons(database.getLessons());
                database.closeDatabase();
                insertStudents();
                checkLessons();
                if (!insertTeachers())
                {
                    break;
                }
                if (PEFlag)
                {
                    if (!subjectTeachers.ContainsKey("PE"))
                    {
                        break;
                    }
                    for (int i = 0; i < PESubjects.Length; i++)
                    {
                        createPE(i);
                    }
                }
                remakeLessons();
                success = true;
            }
        }

        private void createStudents(List<string[]> studentList)
        {
            foreach (string[] student in studentList)
            {
                students.Add(new Student(student, database));
            }
        }

        private void createTeachers(List<string[]> teacherList)
        {
            foreach (string[] teacher in teacherList)
            {
                Teacher newTeacher = new Teacher(teacher, database);
                teachers.Add(newTeacher);
                if (subjectTeachers.ContainsKey(newTeacher.getSubject()))
                {
                    subjectTeachers[newTeacher.getSubject()].Add(newTeacher);
                }
                else
                {
                    subjectTeachers.Add(newTeacher.getSubject(), new List<Teacher>() { newTeacher });
                }
            }
        }

        private void createLessons(List<string[]> lessonList)
        {
            foreach (string[] lesson in lessonList)
            {
                lessons.Add(new Lesson(lesson, database));
            }
        }

        private void insertStudents()
        {
            foreach (Student student in students)
            {
                Dictionary<string, int> subjects = student.getSubjects();
                int i = 0;
                foreach (string subject in subjects.Keys)
                {
                    string lessonName = subject;
                    if (subjects[subject] != 0)
                    {
                        lessonName = $"{lessonName} {subjects[subject]}";
                    }
                    string[] subjectGroups = new string[] { "Maths", "English", "Language", "Humanities", "Option_1", "Option_2" };
                    string subjectGroup;
                    if (i < 6)
                    {
                        subjectGroup = subjectGroups[i];
                        i++;
                    }
                    else
                    {
                        subjectGroup = "Science";
                    }
                    string getter = $"SELECT lesson_id FROM lessons WHERE subject_name='{lessonName}' AND subject_group='{subjectGroup}';";
                    int lessonId = Convert.ToInt32(database.executeReadCommand(getter));
                    lessons[lessonId].addStudent(student);
                }
            }
        }

        private void checkLessons()
        {
            List<Lesson> newLessons = new List<Lesson>();
            foreach (Lesson lesson in lessons)
            {
                int classSize = lesson.getStudentCount();
                int i = 1;
                while (classSize / i + (classSize % i == 0 ? 0 : 1) > maxClassSize)
                {
                    i++;
                }
                foreach (Lesson newLesson in lesson.split(i))
                {
                    newLessons.Add(newLesson);
                }
            }
            lessons = newLessons;
        }

        private List<Lesson> checkLessons(List<Lesson> oldLessons)
        {
            List<Lesson> newLessons = new List<Lesson>();
            foreach (Lesson lesson in oldLessons)
            {
                int classSize = lesson.getStudentCount();
                int i = 1;
                while (classSize / i + (classSize % i == 0 ? 0 : 1) > maxClassSize)
                {
                    i++;
                }
                foreach (Lesson newLesson in lesson.split(i))
                {
                    newLessons.Add(newLesson);
                }
            }
            return newLessons;
        }

        private bool insertTeachers()
        {
            List<int> combinedIndexes = new List<int>();
            List<int> clashingLessons = new List<int>();
            for (int i = 0; i < lessons.Count; i++)
            {
                if (lessons[i].getSubject() != "Combined")
                {
                    if (!subjectTeachers.ContainsKey(lessons[i].getSubject()))
                    {
                        return false;
                    }
                    bool added = false;
                    for (int j = 0; j < subjectTeachers[lessons[i].getSubject()].Count; j++)
                    {
                        Teacher teacher = subjectTeachers[lessons[i].getSubject()][j];
                        if (!teacher.alreadyTeaching(lessons[i].getSubjectGroup()))
                        {
                            if (teacher.hasCapacity(subjectGroups[lessons[i].getSubjectGroup()]))
                            {
                                lessons[i].addTeacher(teacher);
                                if (lessons[i].getSubjectGroup() == "Science")
                                {
                                    subjectTeachers[lessons[i].getSubject()][j].addToTally();
                                    if (!subjectTeachers[lessons[i].getSubject()][j].checkTally())
                                    {
                                        teacher.nowTeaching(lessons[i].getSubjectGroup());
                                    }
                                }
                                else
                                {
                                    teacher.nowTeaching(lessons[i].getSubjectGroup());
                                }
                                added = true;
                                break;
                            }
                        }
                    }
                    if (!added)
                    {
                        clashingLessons.Add(i);
                    }
                }
                else
                {
                    combinedIndexes.Add(i);
                }
            }
            foreach (int i in combinedIndexes)
            {
                bool added = false;
                for (int j = 0; j < subjectTeachers["Physics"].Count; j++)
                {
                    Teacher teacher = subjectTeachers["Physics"][j];
                    if (teacher.getTally() == 0)
                    {
                        if (teacher.hasCapacity(subjectGroups["Science"]))
                        {
                            lessons[i].addTeacher(teacher);
                            teacher.nowTeaching(lessons[i].getSubjectGroup());
                            added = true;
                            break;
                        }
                    }
                }
                if (!added)
                {
                    for (int j = 0; j < subjectTeachers["Biology"].Count; j++)
                    {
                        Teacher teacher = subjectTeachers["Biology"][j];
                        if (teacher.getTally() == 0)
                        {
                            if (teacher.hasCapacity(subjectGroups["Science"]))
                            {
                                lessons[i].addTeacher(teacher);
                                teacher.nowTeaching(lessons[i].getSubjectGroup());
                                added = true;
                                break;
                            }
                        }
                    }
                }
                if (!added)
                {
                    for (int j = 0; j < subjectTeachers["Chemistry"].Count; j++)
                    {
                        Teacher teacher = subjectTeachers["Chemistry"][j];
                        if (teacher.getTally() == 0)
                        {
                            if (teacher.hasCapacity(subjectGroups["Science"]))
                            {
                                lessons[i].addTeacher(teacher);
                                teacher.nowTeaching(lessons[i].getSubjectGroup());
                                added = true;
                                break;
                            }
                        }
                    }
                }
                if (!added)
                {
                    return false;
                }
            }
            if (clashingLessons.Count > 0)
            {
                foreach (var thing in clashingLessons)
                {
                }
                if (!moveStudents(clashingLessons))
                {
                    return false;
                }
                List<Lesson> removeLessons = new List<Lesson>();
                foreach (int index in clashingLessons)
                {
                    removeLessons.Add(lessons[index]);
                }
                foreach (Lesson lesson in removeLessons)
                {
                    lessons.Remove(lesson);
                }
            }
            return true;
        }

        private bool insertTeachers(ref List<Lesson> newLessons)
        {
            List<int> clashingLessons = new List<int>();
            for (int i = 0; i < newLessons.Count; i++)
            {
                bool added = false;
                for (int j = 0; j < subjectTeachers[newLessons[i].getSubject()].Count; j++)
                {
                    Teacher teacher = subjectTeachers[newLessons[i].getSubject()][j];
                    if (!teacher.alreadyTeaching(newLessons[i].getSubjectGroup()))
                    {
                        if (teacher.hasCapacity(subjectGroups[newLessons[i].getSubjectGroup()]))
                        {
                            newLessons[i].addTeacher(teacher);
                            teacher.nowTeaching(newLessons[i].getSubjectGroup());
                            added = true;
                            break;
                        }
                    }
                }
                if (!added)
                {
                    clashingLessons.Add(i);
                }
            }
            if (clashingLessons.Count > 0)
            {
                return false;
            }
            return true;
        }

        private bool moveStudents(List<int> lessonClashes)
        {
            List<string> groupClashes = new List<string>();
            foreach (int i in lessonClashes)
            {
                if (!groupClashes.Contains(lessons[i].getSubjectGroup()))
                {
                    groupClashes.Add(lessons[i].getSubjectGroup());
                }
            }
            int[] groupAppearences = new int[groupClashes.Count];
            for (int i = 0; i < groupClashes.Count; i++)
            {
                groupAppearences[i] = subjectGroups[groupClashes[i]];
            }
            Dictionary<string, List<Lesson>> orderedGroupAppearences = new Dictionary<string, List<Lesson>>();
            bool containsUnique = false;
            for (int i = 0; i < groupAppearences.Length; i++)
            {
                bool currentUnique = true;
                for (int j = 0; j < subjectGroups.Keys.Count; j++)
                {
                    if (groupClashes[i] != subjectGroups.Keys.ToList()[j] && groupAppearences[i] == subjectGroups[subjectGroups.Keys.ToList()[j]])
                    {
                        foreach (int k in lessonClashes)
                        {
                            if (groupClashes[i] == lessons[k].getSubjectGroup())
                            {
                                if (!orderedGroupAppearences.ContainsKey(subjectGroups.Keys.ToList()[j]))
                                {
                                    orderedGroupAppearences[subjectGroups.Keys.ToList()[j]] = new List<Lesson> { lessons[k] };
                                }
                                else
                                {
                                    orderedGroupAppearences[subjectGroups.Keys.ToList()[j]].Add(lessons[k]);
                                }
                            }
                        }
                        currentUnique = false;
                        break;
                    }
                }
                if (currentUnique)
                {
                    containsUnique = true;
                }
            }
            if (containsUnique)
            {
                return false;
            }
            else
            {
                List<Lesson> newLessons = new List<Lesson>();
                foreach (string newGroup in orderedGroupAppearences.Keys)
                {
                    List<Student> affectedStudents = new List<Student>();
                    Lesson tempLesson;
                    foreach (Lesson lesson in orderedGroupAppearences[newGroup])
                    {
                        tempLesson = new Lesson(new string[] { "0", $"{lesson.getSubject()} {lesson.getCode().Substring(3, 1)}", newGroup }, database);
                        foreach (Student student in lesson.getStudentList())
                        {
                            tempLesson.addStudent(student);
                            affectedStudents.Add(student);
                        }
                        newLessons.Add(tempLesson);
                    }
                    foreach (Lesson lesson in lessons)
                    {
                        if (lesson.getSubjectGroup() == newGroup)
                        {
                            List<Student> removedStudents = lesson.removeStudents(affectedStudents);
                            tempLesson = new Lesson(new string[] { "0", $"{lesson.getSubject()} {lesson.getCode().Substring(3, 1)}", orderedGroupAppearences[newGroup][0].getSubjectGroup() }, database);
                            foreach (Student student in removedStudents)
                            {
                                tempLesson.addStudent(student);
                            }
                            newLessons.Add(tempLesson);
                        }
                    }
                }
                newLessons = checkLessons(newLessons);
                List<Lesson> removeList = new List<Lesson>();
                for (int i = 0; i < newLessons.Count; i++)
                {
                    for (int j = 0; j < newLessons.Count; j++)
                    {
                        if (i != j)
                        {
                            if (newLessons[i].getCode() == newLessons[j].getCode())
                            {
                                newLessons[j].incrementCode();
                            }
                            if (newLessons[i].getSubject() == newLessons[j].getSubject() && newLessons[i].getSubjectGroup() == newLessons[j].getSubjectGroup() && newLessons[i].getTeachingGroup() == newLessons[j].getTeachingGroup() && newLessons[i].getStudentCount() + newLessons[j].getStudentCount() <= maxClassSize)
                            {
                                newLessons[i].merge(newLessons[j]);
                                newLessons.Remove(newLessons[j]);
                            }
                        }
                    }
                }
                if (!insertTeachers(ref newLessons))
                {
                    return false;
                }
                List<string> lessonCodes = new List<string>();
                foreach (Lesson lesson in lessons)
                {
                    lessonCodes.Add(lesson.getCode());
                }
                foreach (Lesson lesson in newLessons)
                {
                    while (lessonCodes.Contains(lesson.getCode()))
                    {
                        lesson.incrementCode();
                    }
                    lessons.Add(lesson);
                }
            }
            return true;
        }

        private void createPE(int pass)
        {
            string group = PESubjects[pass];
            int subjecthours = subjectGroups[group] + 1;
            Dictionary<int, Lesson> orderedLessons = new Dictionary<int, Lesson>();
            PETable.Add(new List<Lesson>[subjecthours]);
            for (int i = 0; i < subjecthours; i++)
            {
                PETable[pass][i] = new List<Lesson>();
            }
            foreach (Lesson lesson in lessons)
            {
                if (lesson.getSubjectGroup() == group)
                {
                    int studentCount = lesson.getStudentCount();
                    bool added = false;
                    while (!added)
                    {
                        if (!orderedLessons.ContainsKey(studentCount))
                        {
                            orderedLessons[studentCount] = lesson;
                            added = true;
                        }
                        else
                        {
                            studentCount++;
                        }
                    }
                }
            }
            bool add = true;
            int j = 0;
            for (int i = orderedLessons.Keys.ToList().Max(); i >= orderedLessons.Keys.ToList().Min(); i--)
            {
                if (orderedLessons.ContainsKey(i))
                {
                    if (add)
                    {
                        PETable[pass][j % subjecthours].Add(orderedLessons[i]);
                    }
                    else
                    {
                        PETable[pass][subjecthours - 1 - (j % subjecthours)].Add(orderedLessons[i]);
                    }
                    if (j % subjecthours == subjecthours - 1)
                    {
                        add = !add;
                    }
                    j++;
                }
            }
            foreach (Lesson lesson in orderedLessons.Values)
            {
                for (int i = 0; i < PETable[pass].Length; i++)
                {
                    if (PETable[pass][i].Contains(lesson))
                    {
                        string lessonCode = $"P.E{i}0{lesson.getSuffix(group)}";
                        bool exists = false;
                        foreach (Lesson PE in PETable[pass][i])
                        {
                            if (PE.getCode() == lessonCode)
                            {
                                foreach (Student student in lesson.getStudentList())
                                {
                                    PE.addStudent(student);
                                }
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            PETable[pass][i].Add(new Lesson(lessonCode, "PE", group, lesson.getStudentList(), database));
                            lessons.Add(PETable[pass][i][PETable[pass][i].Count - 1]);
                            PETable[pass][i][PETable[pass][i].Count - 1].addTeacher(subjectTeachers["PE"][0]);
                        }
                        PETable[pass][i].Remove(lesson);
                    }
                    else
                    {
                        PETable[pass][i].Add(lesson);
                    }
                }
            }
            subjectGroups.Remove("PE");
            subjectGroups[group]++;
        }

        private void remakeLessons()
        {
            string command = "TRUNCATE TABLE lessons; ALTER TABLE lessons MODIFY COLUMN lesson_id varchar(255);";
            foreach (Lesson lesson in lessons)
            {
                command = command + lesson.getInsertString();
            }
            database.executeCommand(command);
        }

        public bool wasSuccess()
        {
            return success;
        }

        public void createTimetables()
        {
            database.createNewTables();
            addIds();
            foreach (string subject in subjectGroups.Keys)
            {
                int leftToAdd = subjectGroups[subject];
                int increment = periods / (leftToAdd);
                int i = 0;
                while (leftToAdd > 0)
                {
                    i = i < periods ? i : 0;
                    while (groupTimetable[i] != null)
                    {
                        i = i < periods - 1 ? i + 1 : 0;
                    }
                    groupTimetable[i] = subject;
                    i += increment;
                    leftToAdd--;
                }
            }
            string[] sciences = new string[] { "Physics", "Biology", "Chemistry" };
            Dictionary<string, int> vistitedClasses = new Dictionary<string, int>();
            int nextNumber = 0;
            int[] PECounter = new int[PESubjects.Length];
            for (int i = 0; i < periods; i++)
            {
                foreach (Lesson lesson in lessons)
                {
                    if (groupTimetable[i] == lesson.getSubjectGroup())
                    {
                        if (groupTimetable[i] == "Science" && sciences.Contains(lesson.getSubject()))
                        {
                            string code = lesson.getCode().Substring(3);
                            if (!vistitedClasses.ContainsKey(code))
                            {
                                vistitedClasses.Add(code, nextNumber);
                                nextNumber = nextNumber < 2 ? nextNumber + 1 : 0;
                            }
                            if (sciences[vistitedClasses[code]] == lesson.getSubject())
                            {
                                masterTimetable[i].Add(lesson);
                            }
                        }
                        else if (PESubjects.Contains(groupTimetable[i]) && PEFlag)
                        {
                            int counterGroup = Array.IndexOf(PESubjects, groupTimetable[i]);
                            if (PETable[counterGroup][PECounter[counterGroup]].Contains(lesson))
                            {
                                masterTimetable[i].Add(lesson);
                            }
                        }
                        else
                        {
                            masterTimetable[i].Add(lesson);
                        }
                    }
                }
                if (groupTimetable[i] == "Science")
                {
                    foreach (string code in vistitedClasses.Keys.ToList())
                    {
                        vistitedClasses[code]++;
                        vistitedClasses[code] = vistitedClasses[code] > 2 ? 0 : vistitedClasses[code];
                    }
                }
                else if (PESubjects.Contains(groupTimetable[i]) && PEFlag)
                {
                    PECounter[Array.IndexOf(PESubjects, groupTimetable[i])]++;
                }
            }
            string command = "";
            foreach (Student student in students)
            {
                command = command + student.createTimetable(masterTimetable);
            }
            for (int i = 0; i < masterTimetable.Length; i++)
            {
                foreach (Lesson lesson in masterTimetable[i])
                {
                    teachers[lesson.getTeacherId()].createTimetable(i, lesson.getCode());
                }
            }
            foreach (Teacher teacher in teachers)
            {
                command = command + teacher.getInsertString();
            }
            database.executeCommand(command);
        }

        private void addIds()
        {
            string command = "";
            foreach (Student student in students)
            {
                command = command + $"INSERT INTO student_timetable(student_id) VALUES ({student.getId()});";
            }
            foreach (Teacher teacher in teachers)
            {
                command = command + $"INSERT INTO teacher_timetable(teacher_id) VALUES ({teacher.getId()});";
            }
            database.executeCommand(command);
        }
    }
}

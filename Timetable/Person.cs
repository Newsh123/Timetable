using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    class Person
    {
        protected DatabaseConnection database;
        protected int id;
        protected string firstName;
        protected string lastName;
        protected string[] timetable;

        public Person(string[] details, DatabaseConnection databaseConnection)
        {
            database = databaseConnection;
            id = Convert.ToInt32(details[0]);
            firstName = details[1];
            lastName = details[2];
            timetable = new string[database.getPeriods()];
        }

        public int getId()
        {
            return id;
        }

        public string getName()
        {
            return $"{firstName} {lastName}";
        }
    }
}

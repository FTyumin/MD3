using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3.Entities
{
    public class Student
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public int StudentIdNumber { get; set; }
        //public virtual ICollection<Submission> Submissions { get; set; }

        [Ignore] // Prevent this property from being treated as a column in SQLite
        public List<Submission> Submissions { get; set; }
        public Student(int id, string name, string surname, string gender, int studentIdNumber)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Gender = gender;
            StudentIdNumber = studentIdNumber;
            Submissions = new List<Submission>();
        }

        public Student() 
        {
            Submissions = new List<Submission>();
        }
    }
}

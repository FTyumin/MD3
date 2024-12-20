﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3.Entities
{
    public class Teacher
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime ContractDate { get; set; }

        [Ignore]
        public List<Course> Courses { get; set; }

        public Teacher(int id, string name, string surname)
        {
            ID=id;
            Name = name;
            Surname = surname;
            Courses = new List<Course>();
        }

        public Teacher() { }

        //public virtual ICollection<Course> Courses { get; set; }
    }
}

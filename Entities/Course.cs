using MD1;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3.Entities
{
    public class Course
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        //public virtual Teacher Teacher { get; set; }
        //public virtual ICollection<Assignment> Assignments { get; set; }
    }
}

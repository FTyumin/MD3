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
        public string Gender { get; set; }
        public int StudentIdNumber { get; set; }
        //public virtual ICollection<Submission> Submissions { get; set; }
    }
}

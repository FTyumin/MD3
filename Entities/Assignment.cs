using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3.Entities
{
    public class Assignment
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public int CourseId { get; set; }
        public string Description { get; set; }

        [Ignore] // Prevent this property from being treated as a column in SQLite
        public Course course { get; set; }
        [Ignore]
        public List<Submission> Submissions { get; set; }

        public Assignment(int id, DateTime deadline, int courseId, string description)
        {
            Id = id;
            Deadline = deadline;
            CourseId = courseId;
            Description = description;
        }


        public Assignment() { }

        //public virtual Course Course { get; set; }
        //public virtual ICollection<Submission> Submissions { get; set; }
    }
}

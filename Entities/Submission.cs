using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3.Entities
{
    public class Submission
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int Score { get; set; }

        //public virtual Assignment Assignment { get; set; }
        //public virtual Student Student { get; set; }
    }
}

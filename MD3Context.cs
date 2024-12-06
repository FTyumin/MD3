//using MD3.Entities;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MD3
//{
//    public class MD3Context:DbContext
//    {
//        public DbSet<Course> Courses { get; set; }
//        public DbSet<Assignment> Assignments { get; set; }
//        public DbSet<Student> Students { get; set; }
//        public DbSet<Submission> Submissions { get; set; }
//        public DbSet<Teacher> Teachers { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Data Source=MD3.db");
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Course>()
//                .HasOne(c => c.Teacher)
//                .WithMany(t => t.Courses)
//                .HasForeignKey(c => c.TeacherId);

//            modelBuilder.Entity<Assignment>()
//                .HasOne(a => a.Course)
//                .WithMany(c => c.Assignments)
//                .HasForeignKey(a => a.CourseId);

//            modelBuilder.Entity<Submission>()
//                .HasOne(s => s.Assignment)
//                .WithMany(a => a.Submissions)
//                .HasForeignKey(s => s.AssignmentId);

//            modelBuilder.Entity<Submission>()
//                .HasOne(s => s.Student)
//                .WithMany(s => s.Submissions)
//                .HasForeignKey(s => s.StudentId);
//        }
//    }
//}

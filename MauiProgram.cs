using Microsoft.Extensions.Logging;
using MD3.Entities;
namespace MD3
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<SqliteConnectionFactory>();
            CreateDefaultData();

            return builder.Build();
        }
        // fja,kas ievieto noklusējuma datus
        private static void CreateDefaultData()
        {
            var connectionFactory = new SqliteConnectionFactory();
            var database = connectionFactory.CreateConnection();

            // Pārbauda, vai tabulas eksistē
            database.CreateTableAsync<Course>().Wait();
            database.CreateTableAsync<Assignment>().Wait();
            database.CreateTableAsync<Student>().Wait();
            database.CreateTableAsync<Submission>().Wait();
            database.CreateTableAsync<Teacher>().Wait();

            var existingTeachers = database.Table<Teacher>().ToListAsync().Result;
            if (!existingTeachers.Any())
            {
                database.InsertAsync(new Teacher { Name = "Gunars", Surname = "Berzins" }).Wait();
                database.InsertAsync(new Teacher { Name = "Alex", Surname = "Smith" }).Wait();
            }

            // Insert default courses
            var existingCourses = database.Table<Course>().ToListAsync().Result;
            if (!existingCourses.Any())
            {
                database.InsertAsync(new Course { Name = "Math 101", TeacherId = 1 }).Wait();
                database.InsertAsync(new Course { Name = "Physics 101", TeacherId = 2 }).Wait();
            }

            // Insert default students
            var existingStudents = database.Table<Student>().ToListAsync().Result;
            if (!existingStudents.Any())
            {
                database.InsertAsync(new Student { Name = "John", Surname = "Doe", Gender = "Male", StudentIdNumber = 12345 }).Wait();
                database.InsertAsync(new Student { Name = "Jane", Surname = "Smith", Gender = "Female", StudentIdNumber = 67890 }).Wait();
            }

            // Insert default assignments
            var existingAssignments = database.Table<Assignment>().ToListAsync().Result;
            if (!existingAssignments.Any())
            {
                database.InsertAsync(new Assignment { CourseId = 1, Description = "Homework 1", Deadline = DateTime.Now.AddDays(7) }).Wait();
                database.InsertAsync(new Assignment { CourseId = 2, Description = "Lab 1", Deadline = DateTime.Now.AddDays(10) }).Wait();
            }
        }
    }
}

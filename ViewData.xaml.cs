using MD3.CreatePages;
using SQLite;
using MD3.Entities;
using System.Collections.ObjectModel;


namespace MD3;

public partial class ViewData : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    public ObservableCollection<Student> Students { get; private set; }
    public ObservableCollection<Assignment> Assignments { get; private set; }
    public ObservableCollection<Course> Courses { get; private set; }
    public ObservableCollection<Submission> Submissions { get; private set; }
    public ObservableCollection<Teacher> Teachers { get; private set; }
    public ViewData(SqliteConnectionFactory connectionFactory)
    {
        InitializeComponent();
        _connectionFactory = connectionFactory;
        Students = new ObservableCollection<Student>();
        Assignments = new ObservableCollection<Assignment>();
        Courses = new ObservableCollection<Course>();
        Submissions = new ObservableCollection<Submission>();
        Teachers = new ObservableCollection<Teacher>();
    }



    async void TeacherBtn_Clicked(object sender, EventArgs e)
    {
        try
        {

            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            List<Teacher> teachers = await database.Table<Teacher>().ToListAsync();

            Teachers.Clear();

            foreach (var teacher in teachers)
            {
                Teachers.Add(new Teacher(teacher.ID, teacher.Name, teacher.Surname));
            }
            data.Text = string.Join("\n", Teachers.Select(teacher =>
                 $"{teacher.Name} {teacher.Surname}"));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load Teachers: {ex.Message}", "OK");
        }
    }


    async void StudentBtn_Clicked(object sender, EventArgs e)
    {


        try
        {

            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            List<Student> students = await database.Table<Student>().ToListAsync();

            Students.Clear();

            foreach (var student in students)
            {
                Students.Add(new Student(student.Id, student.Name, student.Surname,
                    student.Gender, student.StudentIdNumber));
            }
            data.Text = string.Join("\n", Students.Select(student =>
                 $"{student.Name} {student.Surname}, Gender: {student.Gender}, ID: {student.StudentIdNumber}"));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load students: {ex.Message}", "OK");
        }
    }




    async void CourseBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            List<Course> courses = await database.Table<Course>().ToListAsync();

            Courses.Clear();

            foreach (var course in courses)
            {
                Courses.Add(new Course(course.Id, course.Name, course.TeacherId));
            }

            data.Text = string.Join("\n", Courses.Select(course =>
                 $"Id:{course.Id} Name: {course.Name}, Teacher ID: {course.TeacherId}"));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load courses: {ex.Message}", "OK");
        }
    }

    async void AssignmentBtn_Clicked(object sender, EventArgs e)
    {
        try
        {

            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            List<Assignment> assignments = await database.Table<Assignment>().ToListAsync();

            Assignments.Clear();

            foreach (var assignment in assignments)
            {
                Assignments.Add(new Assignment(assignment.Id, assignment.Deadline,
                    assignment.CourseId, assignment.Description));
            }
            data.Text = string.Join("\n", Assignments.Select(assignment =>
                 $" Deadline:{assignment.Deadline},Course ID:{assignment.CourseId}, Description: {assignment.Description}, "));
        }
        catch (Exception ex)
        {
            data.Text = "Couldn't load assignments";
        }
    }

    async void SubmissionBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            List<Submission> submissions = await database.Table<Submission>().ToListAsync();

            Submissions.Clear();

            foreach (var submission in submissions)
            {
                Submissions.Add(new Submission(submission.Id, submission.AssignmentId, submission.StudentId, submission.SubmissionTime, submission.Score));
            }

            data.Text = string.Join("\n", Submissions.Select(submission =>
                 $"Assignment ID: {submission.AssignmentId}, Student ID: {submission.StudentId}, Submission Time: {submission.SubmissionTime}, Score: {submission.Score}"));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load submissions: {ex.Message}", "OK");
        }
    }

}


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
    public ViewData(SqliteConnectionFactory connectionFactory)
	{
		InitializeComponent();
        _connectionFactory = connectionFactory;
        Students = new ObservableCollection<Student>();
        Assignments = new ObservableCollection<Assignment>();
    }



	private void TeacherBtn_Clicked(object sender, EventArgs e)
	{

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
                
            }
        }
        

    

    private void CourseBtn_Clicked(object sender, EventArgs e)
    {

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
                Assignments.Add(new Assignment(assignment.Id,assignment.Deadline,
                    assignment.CourseId, assignment.Description));
            }
            data.Text = string.Join("\n", Assignments.Select(assignment =>
                 $" Deadline:{assignment.Deadline},Course ID:{assignment.CourseId}, Description: {assignment.Description}, "));
        }
        catch (Exception ex)
        {
            data.Text = "Dirsaa ir,vecit";
        }
    }

    private void SubmissionBtn_Clicked(object sender, EventArgs e)
    {

    }

	private async void LoadBtn_Clicked(object sender, EventArgs e)
	{

	}

}
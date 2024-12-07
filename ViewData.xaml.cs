using MD3.CreatePages;
using SQLite;
using MD3.Entities;
using System.Collections.ObjectModel;

namespace MD3;

public partial class ViewData : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    public ObservableCollection<Student> Students { get; private set; }
    public ViewData(SqliteConnectionFactory connectionFactory)
	{
		InitializeComponent();
        _connectionFactory = connectionFactory;
        Students = new ObservableCollection<Student>();
    }

	private void TeacherBtn_Clicked(object sender, EventArgs e)
	{

	}

	async void StudentBtn_Clicked(object sender, EventArgs e)
	{
        

        try
            {
            if (_connectionFactory == null)
            {
                await DisplayAlert("Error", "Connection factory is null. Ensure it is passed correctly.", "OK");
                return;
            }
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

    private void AssignmentBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void SubmissionBtn_Clicked(object sender, EventArgs e)
    {

    }

	private async void LoadBtn_Clicked(object sender, EventArgs e)
	{

	}

}
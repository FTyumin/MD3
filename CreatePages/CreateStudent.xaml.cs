using MD3.Entities;
using Microsoft.Data.Sqlite;
using SQLite;
using System.Collections.ObjectModel;

namespace MD3.CreatePages;

public partial class CreateStudent : ContentPage
{
	private readonly SqliteConnectionFactory _connectionFactory;
	public ObservableCollection<Student> Students { get; private set; }

	public CreateStudent(SqliteConnectionFactory connectionFactory)
	{
		InitializeComponent();
		_connectionFactory = connectionFactory;
		Students = new ObservableCollection<Student>();
		LoadStudents();
	}

	private async void LoadStudents()
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
		
		}
		catch(Exception ex)
		{
			MessageLabel.Text = $"Error loading tickets: {ex.Message}";
		}
	}

	private async void CreateStudentCommand(object sender, EventArgs e)
	{
		string name = StudentNameEntry.Text;
		string surname = StudentSurnameEntry.Text;


	}
}
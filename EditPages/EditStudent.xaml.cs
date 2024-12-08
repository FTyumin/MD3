using System.Collections.ObjectModel;
using MD3.Entities;
using SQLite;
namespace MD3.EditPages;

public partial class EditStudent : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    public ObservableCollection<Student> Students { get; private set; }
    public EditStudent(SqliteConnectionFactory connectionFactory)
	{
		InitializeComponent();
        _connectionFactory = connectionFactory;
        Students = new ObservableCollection<Student>();
        LoadStudents();
    }

    public async void LoadStudents()
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
        catch (Exception ex)
        {
            MessageLabel.Text = $"Error loading students: {ex.Message}";
        }
    }

    private async void EditStudentCommand(object sender, EventArgs e)
    {
        string name = StudentNameEntry.Text?.Trim();
        string surname = StudentSurnameEntry.Text?.Trim();
        string gender = GenderPicker.SelectedItem?.ToString();



    }

}
using System.Collections.ObjectModel;
using MD3.Entities;
using SQLite;
namespace MD3;

public partial class EditDelete : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    public ObservableCollection<Student> Students { get; private set; }
    public EditDelete(SqliteConnectionFactory connectionFactory)
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
        } catch (Exception ex)
        {
            //MessageLabel.Text = $"Error loading students: {ex.Message}";
        }
        
   }

	private async void EditStudentCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditPages.EditStudent(_connectionFactory));
    }

	//private async void EditAssignmentCommand(object sender, EventArgs e)
 //   {
 //       await Navigation.PushAsync(new EditPages.EditAssignment(_connectionFactory));
 //   }

    //private async void DeleteStudentCommand(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new DeletePages.DeleteStudent());
    //}

    //private async void DeleteAssignmentCommand(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new DeletePages.DeleteAssignment());
    //}
}
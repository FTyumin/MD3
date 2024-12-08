using MD3.Entities;
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

        if (connectionFactory == null)
        {
            throw new ArgumentNullException(nameof(connectionFactory), "Connection factory cannot be null.");
        }

        _connectionFactory = connectionFactory;
        Students = new ObservableCollection<Student>();
        LoadData();

    }

    public async void LoadData()
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

    private async void CreateStudentCommand(object sender, EventArgs e)
    {
        string name = StudentNameEntry.Text?.Trim();
        string surname = StudentSurnameEntry.Text?.Trim();
        string gender = GenderPicker.SelectedItem?.ToString();

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(gender))
        {
            MessageLabel.Text = "All fields are required.";
            return;
        }

        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            // Create a new student object
            Student newStudent = new Student
            {
                Name = name,
                Surname = surname,
                Gender = gender,
                StudentIdNumber = new Random().Next(100000, 999999) // Ģenerēt 6 ciparu studentu ID
            };

            // Ievietojam datubazē
            await database.InsertAsync(newStudent);

            // Add to the observable collection for UI update
            Students.Add(newStudent);

            // Clear input fields
            StudentNameEntry.Text = string.Empty;
            StudentSurnameEntry.Text = string.Empty;
            GenderPicker.SelectedIndex = -1;

            MessageLabel.Text = "Student created successfully!";
        }
        catch (Exception ex)
        {
            MessageLabel.Text = $"Error creating student: {ex.Message}";
        }
    }
}

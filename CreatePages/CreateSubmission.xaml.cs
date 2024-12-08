using System.Collections.ObjectModel;
using MD3.Entities;
using SQLite;

namespace MD3.CreatePages;

public partial class CreateSubmission : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    public ObservableCollection<Submission> Submissions { get; private set; }
    public ObservableCollection<Assignment> Assignments { get; private set; }
    public ObservableCollection<Student> Students { get; private set; }

    public CreateSubmission(SqliteConnectionFactory connectionFactory)
    {
        InitializeComponent();

        _connectionFactory = connectionFactory;
        Submissions = new ObservableCollection<Submission>();
        Assignments = new ObservableCollection<Assignment>();
        Students = new ObservableCollection<Student>();

        LoadData();
    }

    public async void LoadData()
    {
        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            // Load assignments and students from the database
            List<Assignment> assignments = await database.Table<Assignment>().ToListAsync();
            List<Student> students = await database.Table<Student>().ToListAsync();

            // Populate Pickers
            Assignments.Clear();
            foreach (var assignment in assignments)
            {
                Assignments.Add(assignment);
            }
            AssignmentPicker.ItemsSource = Assignments;

            Students.Clear();
            foreach (var student in students)
            {
                Students.Add(student);
            }
            StudentPicker.ItemsSource = Students;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
        }
    }

    private async void CreateSubmissionCommand(object sender, EventArgs e)
    {
        // Get selected Assignment and Student from Pickers
        var selectedAssignment = AssignmentPicker.SelectedItem as Assignment;
        var selectedStudent = StudentPicker.SelectedItem as Student;

        // Validate inputs
        if (selectedAssignment == null || selectedStudent == null || string.IsNullOrWhiteSpace(GradeEntry.Text))
        {
            MessageLabel.Text = "Please fill all fields and select Assignment and Student.";
            MessageLabel.IsVisible = true;
            return;
        }

        // Parse grade
        if (!int.TryParse(GradeEntry.Text, out int score))
        {
            MessageLabel.Text = "Please enter a valid grade.";
            MessageLabel.IsVisible = true;
            return;
        }

        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            // Create a new submission
            Submission newSubmission = new Submission
            {
                AssignmentId = selectedAssignment.Id,
                StudentId = selectedStudent.Id,
                SubmissionTime = DateTime.Now, // Set current time
                Score = score
            };

            // Save submission to the database
            await database.InsertAsync(newSubmission);

            // Add to local collection
            Submissions.Add(newSubmission);

            // Success message
            MessageLabel.Text = "Submission created successfully.";
            MessageLabel.TextColor = Colors.Green;
            MessageLabel.IsVisible = true;

            // Clear inputs
            AssignmentPicker.SelectedIndex = -1;
            StudentPicker.SelectedIndex = -1;
            GradeEntry.Text = string.Empty;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to create submission: {ex.Message}", "OK");
        }
    }
}

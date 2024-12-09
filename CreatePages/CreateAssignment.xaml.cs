using System.Collections.ObjectModel;
using MD3.Entities;
using SQLite;
namespace MD3.CreatePages;

public partial class CreateAssignment : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    public ObservableCollection<Assignment> Assignments { get; private set; }
    private readonly ObservableCollection<Course> _courses;
    public CreateAssignment(SqliteConnectionFactory connectionFactory)
	{
		InitializeComponent();

        // bija šāda kļūda, kuru nevarēju ilgi saprast
        if (connectionFactory == null)
        {
            throw new ArgumentNullException(nameof(connectionFactory), "Connection factory cannot be null.");
        }

        _connectionFactory = connectionFactory;
        Assignments = new ObservableCollection<Assignment>();
        _courses = new ObservableCollection<Course>();

        LoadAssignments();
    }

    public async void LoadAssignments()
    {
        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
            List<Assignment> assignments = await database.Table<Assignment>().ToListAsync();

            Assignments.Clear();

            foreach (var assignment in assignments)
            {
                //Assignments.Add(new Assignment(assignment.Id, assignment.Deadline, assignment.Description,
                //    assignment.CourseId, assignment.Description));
            }
            List<Course> courses = await database.Table<Course>().ToListAsync();
            foreach(var course in courses)
            {
                _courses.Add(course);
            }
            CoursePicker.ItemsSource = _courses;
        } catch (Exception ex) {
            MessageLabel.Text = $"Error loading students: {ex.Message}";
        }
    }

    private async void CreateAssignmentCommand(object sender, EventArgs e)
    {
        DateTime date = DueDate.Date;
        string title = AssignmentNameEntry.Text;
        string description = AssignmentDescription.Text;
        var selectedCourse = CoursePicker.SelectedItem as Course;
        int course_id = selectedCourse?.Id ?? 0;

        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            Assignment newAssignment = new Assignment
            {
                Deadline = date,
                CourseId = course_id,
                Description = description,
            };

            await database.InsertAsync(newAssignment);

            Assignments.Add(newAssignment);

            DueDate.Date = DateTime.Today;
            AssignmentNameEntry.Text = string.Empty;
            AssignmentDescription.Text = string.Empty;

            MessageLabel.Text = "Assignment created successfully!";
        }
        catch (Exception ex)
        {
            MessageLabel.Text = $"Error creating student: {ex.Message}";
        }
    }
}
using MD3.Entities;
using SQLite;
using System.Collections.ObjectModel;

namespace MD3.EditPages;

public partial class EditAssignment : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    private readonly Assignment _assignment;
    private readonly ObservableCollection<Course> _courses;

    public EditAssignment(SqliteConnectionFactory connectionFactory, Assignment assignment)
    {
        InitializeComponent();
        _connectionFactory = connectionFactory;
        _assignment = assignment;

        _courses = new ObservableCollection<Course>();

        LoadCourses();

        // Pre-fill fields with assignment data
        AssignmentNameEntry.Text = _assignment.Description;
        DueDate.Date = _assignment.Deadline;
        CoursePicker.ItemsSource = _courses;
    }

    private async void LoadCourses()
    {
        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
            var courses = await database.Table<Course>().ToListAsync();

            _courses.Clear();
            foreach (var course in courses)
            {
                _courses.Add(course);
            }

            CoursePicker.ItemsSource = _courses;
            CoursePicker.SelectedItem = _courses.FirstOrDefault(c => c.Id == _assignment.CourseId);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load courses: {ex.Message}", "OK");
        }
    }

    private async void SaveAssignmentCommand(object sender, EventArgs e)
    {
        // Validate fields
        if (string.IsNullOrWhiteSpace(AssignmentNameEntry.Text) || CoursePicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please fill all fields.", "OK");
            return;
        }

        // Update the assignment object
        _assignment.Description = AssignmentNameEntry.Text.Trim();
        _assignment.Deadline = DueDate.Date;
        var selectedCourse = CoursePicker.SelectedItem as Course;
        _assignment.CourseId = selectedCourse?.Id ?? 0;

        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
            await database.UpdateAsync(_assignment);

            await DisplayAlert("Success", "Assignment updated successfully.", "OK");

            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to update assignment: {ex.Message}", "OK");
        }
    }
}

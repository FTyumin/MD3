using MD3.Entities;
using SQLite;

namespace MD3.EditPages;

public partial class EditStudent : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    private readonly Student _student;

    public EditStudent(SqliteConnectionFactory connectionFactory, Student student)
    {
        InitializeComponent();
        _connectionFactory = connectionFactory;
        _student = student;

        // Populate fields with existing student data
        StudentNameEntry.Text = _student.Name;
        StudentSurnameEntry.Text = _student.Surname;
        GenderPicker.SelectedItem = _student.Gender;
    }

    private async void SaveStudentCommand(object sender, EventArgs e)
    {
        try
        {
            // Update student properties from input fields
            _student.Name = StudentNameEntry.Text;
            _student.Surname = StudentSurnameEntry.Text;
            _student.Gender = GenderPicker.SelectedItem?.ToString();

            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            // Update the student in the database
            await database.UpdateAsync(_student);

            await DisplayAlert("Success", "Student updated successfully.", "OK");

            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save student: {ex.Message}", "OK");
        }
    }
}

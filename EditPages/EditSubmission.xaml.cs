namespace MD3.EditPages;
using MD3.Entities;
using Microsoft.VisualBasic;
using SQLite;
using System.Collections.ObjectModel;
public partial class EditSubmission : ContentPage
{
	private readonly SqliteConnectionFactory _connectionFactory;
	private readonly Submission _submission;
	private readonly ObservableCollection<Student> _students;
	private readonly ObservableCollection<Assignment> _assignments;

	public EditSubmission(SqliteConnectionFactory connectionFactory, Submission submission)
	{
		InitializeComponent();
		_connectionFactory = connectionFactory;
		_submission = submission;

		_students = new ObservableCollection<Student>();
		_assignments = new ObservableCollection<Assignment>();

		LoadData();

		AssignmentPicker.ItemsSource = _assignments;
		StudentPicker.ItemsSource = _students;

	}

	private async void LoadData()
	{
		try
		{
			ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
			List<Assignment> assignments = await database.Table<Assignment>().ToListAsync();

			_assignments.Clear();
			foreach (var assignment in assignments)
			{
				_assignments.Add(assignment);
			}

			List<Student> students = await database.Table<Student>().ToListAsync();
			_students.Clear();
			foreach (var student in students)
			{
				_students.Add(student);
			}


			// copilot rindina, kas izvēlas studentu un uzdevumu, kas ir pie iesnieguma
			StudentPicker.SelectedItem = _students.FirstOrDefault(s => s.Id == _submission.StudentId);
			AssignmentPicker.SelectedItem = _assignments.FirstOrDefault(a => a.Id == _submission.AssignmentId);
			ScoreEntry.Text = _submission.Score.ToString();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
		}

	}

    private async void SaveSubmissionCommand(object sender, EventArgs e)
    {
        int.TryParse(ScoreEntry.Text, out int score);
        // Validate fields
        if (AssignmentPicker.SelectedItem==null || StudentPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please fill all fields.", "OK");
            return;
        } else if(score < 0 || score > 100)
        {
            await DisplayAlert("Error", "Score must be between 0 and 100.", "OK");
            return;
        }

        // Update the assignment object
        _submission.StudentId = (StudentPicker.SelectedItem as Student).Id;
		_submission.AssignmentId = (AssignmentPicker.SelectedItem as Assignment).Id;
		_submission.Score = score;


        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
            await database.UpdateAsync(_submission);

            await DisplayAlert("Success", "Submission updated successfully.", "OK");

            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to update submission: {ex.Message}", "OK");
        }
    }
}
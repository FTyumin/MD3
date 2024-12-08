using System.Collections.ObjectModel;
using MD3.Entities;
using SQLite;
namespace MD3;

public partial class EditDelete : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    public ObservableCollection<Student> Students { get; private set; }
    public ObservableCollection<Assignment> Assignments { get; private set; }
    public ObservableCollection<Submission> Submissions { get; private set; }
    public EditDelete(SqliteConnectionFactory connectionFactory)
    {
        InitializeComponent();
        _connectionFactory = connectionFactory;
        Students = new ObservableCollection<Student>();
        Assignments = new ObservableCollection<Assignment>();
        Submissions = new ObservableCollection<Submission>();
        LoadData();

        StudentListView.ItemsSource = Students;
        AssignmentListView.ItemsSource = Assignments;
        SubmissionListView.ItemsSource = Submissions;
    }



    public async void LoadData()
    {

        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
            List<Student> students = await database.Table<Student>().ToListAsync();
            List<Assignment> assignments = await database.Table<Assignment>().ToListAsync();
            List<Submission> submissions = await database.Table<Submission>().ToListAsync();


            Students.Clear();

            foreach (var student in students)
            {
                Students.Add(new Student(student.Id, student.Name, student.Surname, student.Gender, student.StudentIdNumber));
            };


            foreach (var assignment in assignments)
            {
                Assignments.Add(new Assignment(assignment.Id, assignment.Deadline, assignment.CourseId, assignment.Description));
            };

            foreach (var submission in submissions)
            {
                Submissions.Add(new Submission(submission.Id, submission.AssignmentId, submission.StudentId,
                    submission.SubmissionTime, submission.Score));
            };



        }
        catch (Exception ex)
        {
            //MessageLabel.Text = $"Error loading students: {ex.Message}";
        }
    }

    private async void EditStudentCommand(object sender, EventArgs e)
    {
        // Get the student passed via CommandParameter
        var button = sender as Button;
        var student = button?.CommandParameter as Student;

        if (student == null)
            return;

        // Navigate to the EditStudent page, passing the selected student
        await Navigation.PushAsync(new EditPages.EditStudent(_connectionFactory, student));
    }

    private async void DeleteStudentCommand(object sender, EventArgs e)
    {
        // Get the student passed via CommandParameter
        var button = sender as Button;
        var student = button?.CommandParameter as Student;

        if (student == null)
            return;

        bool confirmDelete = await DisplayAlert(
            "Delete Student",
            $"Are you sure you want to delete {student.Name} {student.Surname}?",
            "Yes",
            "No");

        if (!confirmDelete)
            return;

        try
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            // Delete the student from the database
            await database.DeleteAsync(student);

            // Remove the student from the ObservableCollection to update the UI
            Students.Remove(student);

            await DisplayAlert("Success", "Student deleted successfully.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to delete student: {ex.Message}", "OK");
        }
    }

    private async void EditAssignmentCommand(object sender, EventArgs e)
    {
        var button = sender as Button;
        var assignment = button?.CommandParameter as Assignment;
        await Navigation.PushAsync(new EditPages.EditAssignment(_connectionFactory, assignment));
    }

    private async void DeleteAssignmentCommand(object sender, EventArgs e)
    {
        // Cast the sender to a Button
        var button = sender as Button;

        // Access the CommandParameter of the Button to get the Assignment
        var assignment = button?.CommandParameter as Assignment;

        if (assignment == null)
        {
            await DisplayAlert("Error", "Assignment not found.", "OK");
            return;
        }

        // Ask the user to confirm the deletion
        bool confirmDelete = await DisplayAlert(
            "Delete Assignment",
            $"Are you sure you want to delete the assignment: {assignment.Description}?",
            "Yes",
            "No");

        if (!confirmDelete)
            return;

        try
        {
            // Delete the assignment from the database
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
            await database.DeleteAsync(assignment);

            // Remove the assignment from the ObservableCollection to update the UI
            Assignments.Remove(assignment);

            await DisplayAlert("Success", "Assignment deleted successfully.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to delete assignment: {ex.Message}", "OK");
        }
    }

    private async void DeleteSubmissionCommand(object sender, EventArgs e)
    {
        var button = sender as Button;
        var submission = button?.CommandParameter as Submission;

        if (submission == null)
        {
            await DisplayAlert("Error", "Submission not found.", "OK");
        }

        bool confirmDelete = await DisplayAlert(
           "Delete Submission",
           $"Are you sure you want to delete the submission?",
           "Yes",
           "No");


        if (!confirmDelete)
            return;

        try
        {
            // izdzēst studentu no datubāzes
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
            await database.DeleteAsync(submission);

            // izdzēst studentu no saskarnes
            Submissions.Remove(submission);

            await DisplayAlert("Success", "Assignment deleted successfully.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to delete submission: {ex.Message}", "OK");
        }
    }






    private async void EditSubmissionCommand(object sender, EventArgs e)
    {
        var button = sender as Button;
        var submission = button?.CommandParameter as Submission;
        await Navigation.PushAsync(new EditPages.EditSubmission(_connectionFactory, submission));
    }

}
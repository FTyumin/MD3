using MD3.CreatePages;

namespace MD3;

public partial class CreateData : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public CreateData(SqliteConnectionFactory connectionFactory)
    {
        InitializeComponent();
        _connectionFactory = connectionFactory;
    }

    async void StudentBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateStudent(_connectionFactory));
    }

    async void AssignmentBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAssignment(_connectionFactory));
    }
}

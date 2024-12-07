using Microsoft.Maui.Storage;

namespace MD3
{
    public partial class MainPage : ContentPage
    {
        private readonly SqliteConnectionFactory _connectionFactory;

        public MainPage()
        {
            InitializeComponent();
            _connectionFactory = new SqliteConnectionFactory();
        }

       

        async void DataView_Clicked(object sender, EventArgs args)
        {
         
            await Navigation.PushAsync(new ViewData(_connectionFactory));

        }

        async void DataFile_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new FilePage());
        }

        async void CreateData_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CreateData());
        }

        async void EditDelete_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new EditDelete());
        }
    }

}

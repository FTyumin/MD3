using Microsoft.Maui.Storage;

namespace MD3
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        async void DataView_Clicked(object sender, EventArgs args)
        {

            //await Navigation.PushAsync(new ViewData());

        }

        async void DataFile_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new FilePage());
        }

        async void CreateFile_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CreateData());
        }

        async void EditDelete_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new EditDelete());
        }
    }

}

using Frontend_LeLire.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_LeLire.Views.ContentPages;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private async void refreshCv_Refreshing(object sender, EventArgs e)
    {
        refreshCv.IsRefreshing = true;
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{App.conString}libraryview/get/{App.libraryId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var libraryData = JsonConvert.DeserializeObject<LibraryView[]>(content);
            myBooksCv.ItemsSource = libraryData.ToList();
        }
        refreshCv.IsRefreshing = false;
    }

    private async void myBooksCv_Loaded(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{App.conString}libraryview/get/{App.libraryId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var libraryData = JsonConvert.DeserializeObject<LibraryView[]>(content);
            myBooksCv.ItemsSource = libraryData.ToList();
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Border border = (Border)sender;
        var bookId = int.Parse(border.AutomationId);
        ReaderPage readerPage = new ReaderPage();
        if (App.russianPagesSource.Count != 0 && App.frenchPagesSource.Count != 0)
        {
            Navigation.PushModalAsync(readerPage);
        }
        else
        {
            readerPage = new ReaderPage(bookId);
            Navigation.PushModalAsync(readerPage);
        }
    }
}
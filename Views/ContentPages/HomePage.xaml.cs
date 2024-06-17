using Frontend_LeLire.ApplicationData;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Frontend_LeLire.Views.ContentPages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void showcaseBooksCv_Loaded(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{App.conString}showcaseview/get/popular");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var showCaseBooks = JsonConvert.DeserializeObject<ShowcaseView[]>(content);
            showcaseBooksCv.ItemsSource = showCaseBooks.ToList();
        }
    }

    private async void bookPopularBtn_Clicked(object sender, EventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;
        var imageButtonId = int.Parse(imageButton.AutomationId);
        App.selectedBook = imageButtonId;
        await Navigation.PushModalAsync(new BookPage());
    }
    
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{App.conString}libraries/get/{App.enteredUser.UserId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var libraryData = JsonConvert.DeserializeObject<Library>(content);
            App.libraryId = libraryData.LibraryId;
        }

        HttpClient allClient = new HttpClient();
        var allResponse = await allClient.GetAsync($"{App.conString}showcaseview/get/all");
        if (allResponse.IsSuccessStatusCode)
        {
            string allContent = await allResponse.Content.ReadAsStringAsync();
            var allItems = JsonConvert.DeserializeObject<ShowcaseView[]>(allContent);
            allItemsCv.ItemsSource = allItems.ToList();
        }
    }

    private async void allBooksCv_Loaded(object sender, EventArgs e)
    {
        //HttpClient client = new HttpClient();
        //var response = await client.GetAsync($"{App.conString}showcaseview/get/all");
        //if (response.IsSuccessStatusCode)
        //{
        //    string content = await response.Content.ReadAsStringAsync();
        //    var allBooks = JsonConvert.DeserializeObject<ShowcaseView[]>(content);
        //    allBooksCv.ItemsSource = allBooks.ToList();
        //}
    }

    private async void refresPage_Refreshing(object sender, EventArgs e)
    {
        refresPage.IsRefreshing = true;
        try
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{App.conString}showcaseview/get/popular");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var showCaseBooks = JsonConvert.DeserializeObject<ShowcaseView[]>(content);
                showcaseBooksCv.ItemsSource = showCaseBooks.ToList();
            }
        }
        catch (Exception)
        {

        }
        try
        {
            HttpClient allClient = new HttpClient();
            var allResponse = await allClient.GetAsync($"{App.conString}showcaseview/get/all");
            if (allResponse.IsSuccessStatusCode)
            {
                string allContent = await allResponse.Content.ReadAsStringAsync();
                var allBooks = JsonConvert.DeserializeObject<ShowcaseView[]>(allContent);
                allItemsCv.ItemsSource = allBooks.ToList();
            }
        }
        catch (Exception)
        {

        }
        refresPage.IsRefreshing = false;
    }
}
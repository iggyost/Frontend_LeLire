using Frontend_LeLire.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_LeLire.Views.ContentPages;

public partial class BookPage : ContentPage
{
	public BookPage()
	{
		InitializeComponent();
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		HttpClient client = new HttpClient();
		var response = await client.GetAsync($"{App.conString}books/get/{App.selectedBook}");
		if (response.IsSuccessStatusCode)
		{
			string content = await response.Content.ReadAsStringAsync();
			var data = JsonConvert.DeserializeObject<Book>(content);
			this.BindingContext = data;
        }
    }

    private async void buyBookBtn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync(new BuyBookPage());
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Home");
    }
}
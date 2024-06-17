using Frontend_LeLire.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_LeLire.Views.ContentPages;

public partial class BuyBookPage : ContentPage
{
    public static Book bookData;
    public BuyBookPage()
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
            bookData = JsonConvert.DeserializeObject<Book>(content);
            this.BindingContext = bookData;
        }
    }

    private async void payBtn_Clicked(object sender, EventArgs e)
    {
        if (cvvCodeEntry.Text == null && cardNumEntry.Text == null)
        {
            await DisplayAlert("Ошибка!", "Недопустимы пустые поля для ввода!", "Закрыть");
        }
        else
        {
            if (cardNumEntry.Text.Length == 16)
            {
                if (cvvCodeEntry.Text.Length == 3)
                {
                    await DisplayAlert("Оплата прошла успешно", string.Empty, "Закрыть");

                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync($"{App.conString}librariesbooks/put/{App.libraryId}/{bookData.BookId}");
                    if (response.IsSuccessStatusCode)
                    {
                        this.BindingContext = bookData;
                        await Shell.Current.GoToAsync("//Home");
                    }

                }
                else
                {
                    await DisplayAlert("Ошибка", "CVV-код должен состоять из 3 цифр", "Закрыть");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Номер карты должен состоять из 16 цифры", "Закрыть");
            }
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Home");
    }
}
using Frontend_LeLire.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_LeLire.Views.ContentPages;

public partial class PasswordEnterPage : ContentPage
{
    public PasswordEnterPage()
    {
        InitializeComponent();
    }

    private async void passwordEntry_Completed(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{App.conString}users/enter/{App.enteredPhone}/{passwordEntry.Text}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var dataUser = JsonConvert.DeserializeObject<User>(content);
            App.enteredUser = dataUser;

            App.Current.MainPage = new AppShell();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            await DisplayAlert("Ошибка!", "Неверный пароль!", "Закрыть");
        }
        else
        {
            await DisplayAlert("Ошибка!", "Ошибка сервера!", "Закрыть");
        }
    }
}
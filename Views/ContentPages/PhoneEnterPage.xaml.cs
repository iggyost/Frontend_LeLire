using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Frontend_LeLire.Views.ContentPages;

public partial class PhoneEnterPage : ContentPage
{
	public PhoneEnterPage()
	{
		InitializeComponent();
	}

	private async void phoneEntry_Completed(object sender, EventArgs e)
    {
        if (phoneEntry.Text == string.Empty || phoneEntry.Text == null)
        {
            await DisplayAlert("Ошибка!", "Заполните поля для ввода!", "Закрыть");
        }
        else
        {
            var regex = new Regex("^((\\+7|7|8)+([0-9]){10})$");
            if (regex.IsMatch(phoneEntry.Text))
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync($"{App.conString}users/get/{phoneEntry.Text}");

                if (response.IsSuccessStatusCode)
                {
                    App.enteredPhone = phoneEntry.Text;
                    Application.Current.MainPage = new PasswordEnterPage();
                }
                else
                {
                    App.enteredPhone = phoneEntry.Text;
                    Application.Current.MainPage = new RegistrationPage();
                }

            }
            else
            {
                await DisplayAlert("Ошибка!", "Номер не соответствует формату!", "Закрыть");
            }
        }
    }
}
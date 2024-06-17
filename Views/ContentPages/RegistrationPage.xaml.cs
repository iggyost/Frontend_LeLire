using Frontend_LeLire.ApplicationData;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Frontend_LeLire.Views.ContentPages;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
        phoneEntry.Text = App.enteredPhone;
        phoneEntry.IsEnabled = false;
    }

    private async void passwordEntry_Completed(object sender, EventArgs e)
    {
        if (passwordEntry.Text == string.Empty || passwordEntry.Text == null || passwordEntry.Placeholder != null && passwordEntry.Text.Length <= 3)
        {
            await DisplayAlert("������!", "������� ������ (�� ����� 4 ��������)", "�������");
        }
        else
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{App.conString}users/reg/{phoneEntry.Text}/{passwordEntry.Text}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var userData = JsonConvert.DeserializeObject<User>(content);

                App.enteredUser = userData;

                HttpClient libClient = new HttpClient();
                var libResponse = await libClient.GetAsync($"{App.conString}libraries/create/{App.enteredUser.UserId}");
                if (libResponse.IsSuccessStatusCode)
                {
                    App.Current.MainPage = new AppShell();
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await DisplayAlert("������!", "������ ��� ����� ������! ", "�������");
            }
            else
            {
                await DisplayAlert("������!", "������ �������!", "�������");
            }
        }
    }
}
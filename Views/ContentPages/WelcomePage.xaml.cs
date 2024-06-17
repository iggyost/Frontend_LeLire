namespace Frontend_LeLire.Views.ContentPages;

public partial class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        InitializeComponent();

    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        for (; ; )
        {
            if (spanWelcome.Text == "����� ���������� ")
            {
                await Task.Delay(5000);
                spanWelcome.Text = "Bienvenue ";
                spanIn.Text = "dans ";
            }
            else
            {
                await Task.Delay(5000);
                spanWelcome.Text = "����� ���������� ";
                spanIn.Text = "� ";
            }
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PhoneEnterPage();
    }
}
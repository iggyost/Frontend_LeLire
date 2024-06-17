using Frontend_LeLire.ApplicationData;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.IO;
using System.Net;

namespace Frontend_LeLire.Views.ContentPages;

public partial class ReaderPage : ContentPage
{
    public ReaderPage()
    {
        InitializeComponent();

    }
    public ReaderPage(int book)
    {
        InitializeComponent();
        bookId = book;
    }
    public static int bookId;
    public static List<byte[]> russianBookByteData = new List<byte[]>();
    public static List<byte[]> frenchBookByteData = new List<byte[]>();
    public static int pageNum_ru = 1;
    public static int pageNum_fr = 1;
    public static bool isRuBookLoaded = false;
    public static bool isFrBookLoaded = false;

    public void LoadingAnimation(bool turnOn)
    {
        if (turnOn)
        {
            isBusyIndicator.IsRunning = true;
            isBusyText.IsVisible = true;
        }
        else
        {
            isBusyIndicator.IsRunning = false;
            isBusyText.IsVisible = false;
        }
    }
    public async void GetRussianBook(int bookId)
    {
        try
        {
            LoadingAnimation(true);
            isBusyText.Text = "Загрузка книги на русском...";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{App.conString}bookssources/get/{bookId}/{1}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var bookByteArray = JsonConvert.DeserializeObject<List<byte[]>>(content);
                russianBookByteData = bookByteArray;
            }
            foreach (var item in russianBookByteData)
            {
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(item));
                App.russianPagesSource.Add(imageSource);
            }
            LoadingAnimation(false);
            isRuBookLoaded = true;
        }
        catch (Exception)
        {

        }
    }
    public async void GetFrenchBook(int bookId)
    {
        try
        {
            isBusyText.Text = "Загрузка книги на французском...";
            LoadingAnimation(true);

            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{App.conString}bookssources/get/{bookId}/{2}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var bookByteArray = JsonConvert.DeserializeObject<List<byte[]>>(content);
                frenchBookByteData = bookByteArray;
            }
            foreach (var item in frenchBookByteData)
            {
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(item));
                App.frenchPagesSource.Add(imageSource);
            }
            LoadingAnimation(false);
            isFrBookLoaded = true;
        }
        catch (Exception)
        {

        }
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        try
        {
            GetRussianBook(bookId);
            await Task.Delay(4000);
            GetFrenchBook(bookId);
            while (true)
            {
                await Task.Delay(1000);
                if (isRuBookLoaded == true && isFrBookLoaded == true)
                {
                    SwitchToRussian();
                    pageImageViewRussian.Source = App.russianPagesSource[pageNum_ru - 1];
                    pageImageViewFrench.Source = App.frenchPagesSource[pageNum_fr - 1];
                    pageCounterBorder.IsVisible = true;

                    break;
                }
            }
        }
        catch (Exception)
        {

        }
    }
    public async void SwitchToRussian()
    {
        rusBtn.IsEnabled = false;
        frBtn.IsEnabled = false;

        backBorder.IsEnabled = true;
        forwardBorder.IsEnabled = true;
        backBorder.IsVisible = true;
        forwardBorder.IsVisible = true;
        backFrenchBorder.IsEnabled = false;
        backFrenchBorder.IsVisible = false;
        forwardFrenchBorder.IsEnabled = false;
        forwardFrenchBorder.IsVisible = false;

        currentPageLabel.Text = pageNum_ru.ToString();
        totalPageLabel.Text = russianBookByteData.Count.ToString();
        pageImageViewRussian.IsVisible = true;
        pageImageViewFrench.IsVisible = false;
        currentPageLabel.Text = pageNum_ru.ToString();

        await Task.Delay(500);
        rusBtn.IsEnabled = true;
        frBtn.IsEnabled = true;
    }
    public async void SwitchToFrench()
    {
        rusBtn.IsEnabled = false;
        frBtn.IsEnabled = false;

        backBorder.IsEnabled = false;
        forwardBorder.IsEnabled = false;
        backBorder.IsVisible = false;
        forwardBorder.IsVisible = false;
        backFrenchBorder.IsEnabled = true;
        backFrenchBorder.IsVisible = true;
        forwardFrenchBorder.IsEnabled = true;
        forwardFrenchBorder.IsVisible = true;

        currentPageLabel.Text = pageNum_fr.ToString();
        totalPageLabel.Text = frenchBookByteData.Count.ToString();
        pageImageViewRussian.IsVisible = false;
        pageImageViewFrench.IsVisible = true;

        await Task.Delay(500);
        rusBtn.IsEnabled = true;
        frBtn.IsEnabled = true;
    }
    private async void rusBtn_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (rusBtn.IsChecked == true)
        {
            SwitchToRussian();
        }
    }

    private async void frBtn_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (frBtn.IsChecked == true)
        {
            SwitchToFrench();
        }
    }
    public async void RuPageForward()
    {
        if (pageNum_ru < App.russianPagesSource.Count)
        {
            forwardBorder.IsEnabled = false;
            pageNum_ru++;
            pageImageViewRussian.Source = App.russianPagesSource[pageNum_ru - 1];
            currentPageLabel.Text = pageNum_ru.ToString();
            await Task.Delay(300);
            forwardBorder.IsEnabled = true;
        }
    }
    public async void RuPageBack()
    {
        if (pageNum_ru > 1)
        {
            backBorder.IsEnabled = false;
            pageNum_ru--;
            pageImageViewRussian.Source = App.russianPagesSource[pageNum_ru - 1];
            currentPageLabel.Text = pageNum_ru.ToString();
            await Task.Delay(300);
            backBorder.IsEnabled = true;
        }
    }
    public async void FrPageForward()
    {
        if (pageNum_fr < App.frenchPagesSource.Count)
        {
            forwardFrenchBorder.IsEnabled = false;
            pageNum_fr++;
            pageImageViewFrench.Source = App.frenchPagesSource[pageNum_fr - 1];
            currentPageLabel.Text = pageNum_fr.ToString();
            await Task.Delay(300);
            forwardFrenchBorder.IsEnabled = true;
        }
    }
    public async void FrPageBack()
    {
        if (pageNum_fr > 1)
        {
            backFrenchBorder.IsEnabled = false;
            pageNum_fr--;
            pageImageViewFrench.Source = App.frenchPagesSource[pageNum_fr - 1];
            currentPageLabel.Text = pageNum_fr.ToString();
            await Task.Delay(300);
            backFrenchBorder.IsEnabled = true;
        }
    }

    private void backBorder_Clicked(object sender, EventArgs e)
    {
        RuPageBack();
    }
    private void forwardBorder_Clicked(object sender, EventArgs e)
    {
        RuPageForward();
    }

    private void backFrenchBorder_Clicked(object sender, EventArgs e)
    {
        FrPageBack();
    }

    private void forwardFrenchBorder_Clicked(object sender, EventArgs e)
    {
        FrPageForward();
    }
}
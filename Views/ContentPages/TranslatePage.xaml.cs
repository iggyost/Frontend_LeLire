using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Frontend_LeLire.Views.ContentPages;

public partial class TranslatePage : ContentPage
{
    public TranslatePage()
    {
        InitializeComponent();
    }
    private static string HtmlToPlainText(string html)
    {
        const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
        const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
        const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
        var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
        var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
        var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

        var text = html;
        //Decode html specific characters
        text = System.Net.WebUtility.HtmlDecode(text);
        //Remove tag whitespace/line breaks
        text = tagWhiteSpaceRegex.Replace(text, "><");
        //Replace <br /> with line breaks
        text = lineBreakRegex.Replace(text, Environment.NewLine);
        //Strip formatting
        text = stripFormattingRegex.Replace(text, string.Empty);

        return text;
    }
    public class TranslationData
    {
        public string trans { get; set; }
    }

    public async void GetTranslate(string text)
    {
        loadingTranslate.IsRunning = true;
        languageEditor.IsEnabled = false;

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://google-translate113.p.rapidapi.com/api/v1/translator/text"),
            Headers =
    {
        { "X-RapidAPI-Key", "960af32fa6mshedf964ffa205e1cp1bed85jsn89b3019cedc2" },
        { "X-RapidAPI-Host", "google-translate113.p.rapidapi.com" },
    },
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
    {
        { "from", $"{fromLanguage}" },
        { "to", $"{toLanguage}" },
        { "text", $"{text}" },
    }),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var translate = JsonConvert.DeserializeObject<TranslationData>(body);
            translateReadOnlyEditor.Text = translate.trans;
        }

        languageEditor.IsEnabled = true;
        loadingTranslate.IsRunning = false;
    }
    public static bool isCurrentLanguageRu = true;
    public static string french = "Французский";
    public static string russian = "Русский";
    public static string fromLanguage = "ru";
    public static string toLanguage = "fr";
    private async void swapLanguagesButton_Clicked(object sender, EventArgs e)
    {
        swapLanguagesButton.IsEnabled = false;
        if (isCurrentLanguageRu)
        {
            fromLanguageBorder.Text = french;
            topEditorLanguageLabel.Text = french;
            toLanguageBorder.Text = russian;
            bottomEditorLanguageLabel.Text = russian;

            fromLanguage = "fr";
            toLanguage = "ru";

            isCurrentLanguageRu = false;
        }
        else
        {
            fromLanguageBorder.Text = russian;
            topEditorLanguageLabel.Text = russian;
            toLanguageBorder.Text = french;
            bottomEditorLanguageLabel.Text = french;

            fromLanguage = "ru";
            toLanguage = "fr";

            isCurrentLanguageRu = true;
        }
        await Task.Delay(500);
        swapLanguagesButton.IsEnabled = true;
    }

    private void languageEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        charsCountLabel.Text = languageEditor.Text.Length.ToString();
    }

    private void languageEditor_Completed(object sender, EventArgs e)
    {
        if (languageEditor.Text.Length != 0)
        {
            GetTranslate(languageEditor.Text);
        }
    }
}
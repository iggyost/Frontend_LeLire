using Frontend_LeLire.ApplicationData;
using Frontend_LeLire.Views.ContentPages;

namespace Frontend_LeLire;

public partial class App : Application
{
    public static string conString = "http://192.168.0.10:45455/api/";
	public static string enteredPhone;
	public static User enteredUser;
	public static int selectedBook;
	public static int libraryId;
    public static List<ImageSource> russianPagesSource = new List<ImageSource>();
    public static List<ImageSource> frenchPagesSource = new List<ImageSource>();
    public App()
	{
		InitializeComponent();

		MainPage = new WelcomePage();
	}
}

using Frontend_LeLire.Views.ContentPages;

namespace Frontend_LeLire;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("Home", typeof(HomePage));
    }
}

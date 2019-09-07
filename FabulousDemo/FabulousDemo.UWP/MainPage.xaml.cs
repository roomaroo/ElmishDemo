using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace FabulousDemo.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new FabulousDemo.App());
        }
    }
}

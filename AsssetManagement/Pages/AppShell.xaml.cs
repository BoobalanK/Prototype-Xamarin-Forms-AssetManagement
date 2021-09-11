using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsssetManagement.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }
        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(AssetsPage), typeof(AssetsPage));
            Routing.RegisterRoute(nameof(NewAssetPage), typeof(NewAssetPage));
            Routing.RegisterRoute(nameof(EditAssetPage), typeof(EditAssetPage));
            Routing.RegisterRoute(nameof(ReleaseAssetPage), typeof(ReleaseAssetPage));
            Routing.RegisterRoute(nameof(RequestAssetPage), typeof(RequestAssetPage));
            Routing.RegisterRoute(nameof(AssignAssetPage), typeof(AssignAssetPage));
        }
        private void OnLogOutClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Login");
        }
        private void FlyoutLogoClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Login");
        }
    }
}
using AsssetManagement.PageModels;
using AsssetManagement.Pages;
using AsssetManagement.Repositories;
using AsssetManagement.Services;

using FreshMvvm;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsssetManagement
{
    public partial class App : Application
    {
        public App()
        {
            XF.Material.Forms.Material.Init(this);
            InitializeComponent();
            XF.Material.Forms.Material.Use("Material.Style");
            InitContainer();
            MainPage = new AppShell();
        }

        private void InitContainer()
        {
            FreshIOC.Container.Register<LoginPageModel,LoginPageModel>();
            FreshIOC.Container.Register<AssetsPageModel,AssetsPageModel>();
            //
            FreshIOC.Container.Register<IUserRepository,UserRepository>();
            FreshIOC.Container.Register<IAssetRepository, AssetRepository>();
            //
            FreshIOC.Container.Register<IUserService, UserService>();
            FreshIOC.Container.Register<IAssetService, AssetService>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

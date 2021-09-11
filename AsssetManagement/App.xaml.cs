using AsssetManagement.ViewModels;
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
            InitContainer();
            XF.Material.Forms.Material.Use("Material.Style");
            MainPage = new AppShell();
        }

        private void InitContainer()
        {
            DependencyService.Register<IAssetRepository, AssetRepository>();
            DependencyService.Register<IUserRepository, UserRepository>();
            //
            DependencyService.Register<IAssetService, AssetService>();
            DependencyService.Register<IUserService, UserService>();
            //
            DependencyService.Register<AppShellViewModel>();
            DependencyService.Register<LoginViewModel>();
            DependencyService.Register<AssetsViewModel>();
            DependencyService.Register<ReleaseAssetViewModel>();
            DependencyService.Register<RequestAssetViewModel>();
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

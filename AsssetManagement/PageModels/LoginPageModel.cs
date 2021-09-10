using AsssetManagement.Models;
using AsssetManagement.Pages;
using AsssetManagement.StaticClasses;

using FreshMvvm;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AsssetManagement.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        private User _user;

        public User User
        {
            get => _user;
            set
            {
                if (_user != value)
                {
                    _user = value;
                    RaisePropertyChanged(nameof(User));
                }
            }
        }

        private AppShellPageModel _appShellPageModel;

        public FreshAwaitCommand OnLoginCommand { get; }
        public LoginPageModel(AppShellPageModel appShellPageModel)
        {
            _appShellPageModel = appShellPageModel;
            OnLoginCommand = new FreshAwaitCommand(OnLogin);
            //
            User = new User();
            //User.EmailAddress = "prasanthk@dsrc.co.in";
            //User.Password = "Admin@123";
            //User.Type = UserType.StoreManager;
            //
            User.EmailAddress = "boobalan.k@dsrc.co.in";
            User.Password = "Dsrc@123";
            User.Type = UserType.Employee;
        }

        private void OnLogin(object arg1, TaskCompletionSource<bool> arg2)
        {
            AppData.User = User;
            (Shell.Current.BindingContext as AppShellPageModel).IsEmployee = AppData.User.Type is Models.UserType.Employee;
            (Shell.Current.BindingContext as AppShellPageModel).IsStoreManager = AppData.User.Type is Models.UserType.StoreManager;
            Shell.Current.GoToAsync("//Home");
        }
    }
}

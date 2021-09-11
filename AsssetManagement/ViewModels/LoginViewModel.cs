using AsssetManagement.Models;
using AsssetManagement.Pages;
using AsssetManagement.StaticClasses;

using FreshMvvm;

using MvvmHelpers;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AsssetManagement.ViewModels
{
    public class LoginViewModel : BaseViewModel
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
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        public FreshAwaitCommand OnLoginCommand { get; }
        public LoginViewModel()
        {
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
            (Shell.Current.BindingContext as AppShellViewModel).IsEmployee = AppData.User.Type is Models.UserType.Employee;
            (Shell.Current.BindingContext as AppShellViewModel).IsStoreManager = AppData.User.Type is Models.UserType.StoreManager;
            Shell.Current.GoToAsync("//Home");
        }
    }
}

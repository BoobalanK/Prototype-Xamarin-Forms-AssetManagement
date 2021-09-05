using AsssetManagement.Models;
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
        public FreshAwaitCommand OnLoginCommand { get; }
        public LoginPageModel()
        {
            OnLoginCommand = new FreshAwaitCommand(OnLogin);
        }

        private void OnLogin(object arg1, TaskCompletionSource<bool> arg2)
        {
            User = new User();
            User.EmailAddress = "boobalan.k@dsrc.co.in";
            User.Password = "Admin@123";
            AppData.User = User;
            Shell.Current.GoToAsync("//Home");
        }
    }
}

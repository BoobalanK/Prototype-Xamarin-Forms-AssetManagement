using AsssetManagement.Pages;
using AsssetManagement.StaticClasses;

using FreshMvvm;

using MvvmHelpers;

using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace AsssetManagement.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        private bool _isEmployee;
        private bool _isStoreManager;

        public bool IsEmployee
        {
            get => _isEmployee;
            set
            {
                if (_isEmployee != value)
                {
                    OnPropertyChanged(nameof(IsEmployee));
                }
                _isEmployee = value;
            }
        }
        public bool IsStoreManager
        {
            get => _isStoreManager;
            set
            {
                if (_isStoreManager != value)
                {
                    OnPropertyChanged(nameof(IsStoreManager));
                }
                _isStoreManager = value;
            }
        }

        public AppShellViewModel()
        {
            IsEmployee = false;
            IsStoreManager= false;
        }

        public void UpdateShellContent()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (AppData.User != null)
                {
                    IsEmployee = AppData.User.Type is Models.UserType.Employee;
                    IsStoreManager = AppData.User.Type is Models.UserType.StoreManager;
                    (Shell.Current as AppShell).BindingContext = null;
                    (Shell.Current as AppShell).BindingContext = this;
                }
            });
        }
    }
}

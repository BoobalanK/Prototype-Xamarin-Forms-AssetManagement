using AsssetManagement.StaticClasses;

using FreshMvvm;

using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace AsssetManagement.PageModels
{
    public class AppShellPageModel : FreshBasePageModel
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
                    RaisePropertyChanged(nameof(IsEmployee));
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
                    RaisePropertyChanged(nameof(IsStoreManager));
                }
                _isStoreManager = value;
            }
        }

        public AppShellPageModel()
        {
            IsEmployee = true;
            IsStoreManager= true;
        }

        public void UpdateShellContent()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (AppData.User != null)
                {
                    IsEmployee = AppData.User.Type is Models.UserType.Employee;
                    IsStoreManager = AppData.User.Type is Models.UserType.StoreManager;
                }
            });
        }
    }
}

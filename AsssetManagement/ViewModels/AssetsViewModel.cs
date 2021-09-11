using AsssetManagement.Models;
using AsssetManagement.Services;
using AsssetManagement.StaticClasses;

using FreshMvvm;

using MvvmHelpers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace AsssetManagement.ViewModels
{
    public class AssetsViewModel : BaseViewModel
    {
        private ObservableCollection<Asset> _assets;
        private IAssetService _assetService;

        public ICommand OnAppearingCommand { get; }
        public ICommand OnDisappearingCommand { get; }

        public ObservableCollection<Asset> Assets
        {
            get => _assets;
            set
            {
                if (_assets != value)
                {
                    _assets = value;
                    OnPropertyChanged(nameof(Assets));
                }
            }
        }
        public AssetsViewModel()
        {
            _assetService = DependencyService.Get<IAssetService>();
            OnAppearingCommand = CommandFactory.Create(OnAppearing);
            OnDisappearingCommand = CommandFactory.Create(OnDisappearing);
            Assets = new ObservableCollection<Asset>(_assetService.Assets);
        }

        private void OnDisappearing()
        {
            
        }

        public void OnAppearing()
        {
            Assets = new ObservableCollection<Asset>(_assetService.Assets);
            (Shell.Current.BindingContext as AppShellViewModel).UpdateShellContent();
        }
    }
}

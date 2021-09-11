using AsssetManagement.Models;
using AsssetManagement.Services;

using MvvmHelpers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace AsssetManagement.ViewModels
{
    public class ReleaseAssetViewModel : BaseViewModel
    {
        private ObservableCollection<Asset> _assets;
        private IAssetService _assetService;

        public Command OnReleaseClicked { get; }
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

        public ReleaseAssetViewModel()
        {
            _assetService = DependencyService.Get<IAssetService>();
            OnReleaseClicked = new Command(OnRelease);
            OnAppearingCommand = CommandFactory.Create(OnAppearing);
            OnDisappearingCommand = CommandFactory.Create(OnDisappearing);
            //
            Title = "Release an Asset";
            SetAssets();
        }

        private void OnDisappearing()
        {
            
        }

        private void OnAppearing()
        {
            SetAssets();
        }

        private void SetAssets()
        {
            Assets = new ObservableCollection<Asset>(_assetService.GetRequested());
        }

        private void OnRelease(object obj)
        {
            ((Asset)obj).IsSearched = false;
            ((Asset)obj).IsRequested = false;
            SetAssets();
        }
    }
}

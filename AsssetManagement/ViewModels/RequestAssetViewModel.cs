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
    public class RequestAssetViewModel : BaseViewModel
    {
        private ObservableCollection<Asset> _assets;
        private IAssetService _assetService;
        public ICommand OnRequestClicked { get; set; }
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

        public RequestAssetViewModel()
        {
            _assetService = DependencyService.Get<IAssetService>();
            OnRequestClicked = CommandFactory.Create(OnRequest);
            OnAppearingCommand = CommandFactory.Create(OnAppearing);
            OnDisappearingCommand = CommandFactory.Create(OnDisappearing);
            Title = "Request an Asset";
            SetAssets();
        }

        private void OnDisappearing()
        {
            
        }

        private void OnAppearing()
        {
            SetAssets();
        }

        public void SetAssets()
        {
            Assets = new ObservableCollection<Asset>(_assetService.GetSearched());
        }

        private void OnRequest(object obj)
        {
            ((Asset)obj).IsSearched = false;
            ((Asset)obj).IsRequested = true;
            Assets = new ObservableCollection<Asset>(_assetService.GetSearched());
        }
    }
}

using AsssetManagement.Models;
using AsssetManagement.Pages;
using AsssetManagement.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AsssetManagement.ViewModels
{
    public class AppSearchHandler : SearchHandler
    {
        private IAssetService _assetService;
        private RequestAssetViewModel _requestAssetViewModel;

        public IList<Asset> Assets { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }
        public AppSearchHandler()
        {
            _assetService = DependencyService.Get<IAssetService>();
            _requestAssetViewModel = DependencyService.Get<RequestAssetViewModel>();
            Assets = new ObservableCollection<Asset>(_assetService.GetAssetItems(string.Empty));
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = _assetService.GetAssetItems(newValue);
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Let the animation complete
            await Task.Delay(1000);

            //ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            //// The following route works because route names are unique in this application.
            //await Shell.Current.GoToAsync($"{GetNavigationTarget()}?name={((Asset)item).Name}");

            ((Asset)item).IsSearched = true;
            Device.BeginInvokeOnMainThread(() =>{
                _requestAssetViewModel.SetAssets();
            });
        }

    }
}

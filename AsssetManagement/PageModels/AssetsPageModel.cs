using AsssetManagement.Models;
using AsssetManagement.Services;

using FreshMvvm;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AsssetManagement.PageModels
{
    public class AssetsPageModel: FreshBasePageModel
    {
        private ObservableCollection<Asset> _assets;

        public ObservableCollection<Asset> Assets
        {
            get => _assets;
            set
            {
                if (_assets != value)
                {
                    _assets = value;
                    RaisePropertyChanged(nameof(Assets));
                }
            }
        }

        public AssetsPageModel(IAssetService assetService)
        {
            Assets = new ObservableCollection<Asset>((IEnumerable<Asset>)assetService.GetAssetItems());
        }
    }
}

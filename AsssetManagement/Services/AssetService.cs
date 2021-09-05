using AsssetManagement.Models;
using AsssetManagement.Repositories;

using Couchbase.Lite;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Xamarin.Forms;

namespace AsssetManagement.Services
{
    public interface IAssetService
    {
        List<Asset> Assets { get; set; }
        bool AddAssetItem(Asset newAsset);
        bool DeleteAssetItem(Asset removedAsset);
        Asset GetAssetItem(Guid id);
        IList<Asset> GetAssetItems(string searchText);
        bool UpdateAssetItem(Asset updatedAssetItem);
    }
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;

        public List<Asset> Assets { get; set; }
        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
            //
            Assets = new List<Asset>
            {
                new Asset() { Id = Guid.NewGuid(), Type = AssetType.Mobile, AssetTag = "DSRCHS001", Model = "M11", Manufacturer = "Samsung", Name = "Samsung M11", Description = "Samsung M11", Project = "HP", Status = AssetStatus.InStore },
                new Asset() { Id = Guid.NewGuid(), Type = AssetType.Mobile, AssetTag = "DSRCHS002", Model = "G5", Manufacturer = "Motorola", Name = "Motorola G5", Description = "Motorola G5", Project = "HP", Status = AssetStatus.InStore },
                new Asset() { Id = Guid.NewGuid(), Type = AssetType.Tablet, AssetTag = "DSRCTAB001", Model = "M10", Manufacturer = "Lenova", Name = "Lenova Tab M10", Description = "Lenova Tab M10", Project = "HP", Status = AssetStatus.InStore },
                new Asset() { Id = Guid.NewGuid(), Type = AssetType.Tablet, AssetTag = "DSRCTAB002", Model = "A7", Manufacturer = "Samsung", Name = "Samsung A7", Description = "Samsung A7", Project = "HP", Status = AssetStatus.InStore },
                new Asset() { Id = Guid.NewGuid(), Type = AssetType.Laptop, AssetTag = "DSRCLAPTOP001", Model = "Vostro 3501", Manufacturer = "Dell", Name = "Dell Vostro 3501", Description = "Dell Vostro 3501", Project = "HP", Status = AssetStatus.InStore },
                new Asset() { Id = Guid.NewGuid(), Type = AssetType.Laptop, AssetTag = "DSRCLAPTOP002", Model = "IdeaPad Slim 5 Pro", Manufacturer = "Lenova", Name = "Lenova IdeaPad Slim 5 Pro", Description = "Lenova IdeaPad Slim 5 Pro", Project = "HP", Status = AssetStatus.InStore }
            };
            //
            foreach (var asset in Assets)
            {
                assetRepository.Save(asset);
            }
        }
        public bool AddAssetItem(Asset newAsset)
        {
            return _assetRepository.Save(newAsset);
        }

        public bool DeleteAssetItem(Asset removedAsset)
        {
            return _assetRepository.Remove(removedAsset);
        }

        public Asset GetAssetItem(Guid id)
        {
            return _assetRepository.Get(id.ToString());
        }

        public IList<Asset> GetAssetItems(string searchText)
        {
            return _assetRepository.GetAll(searchText);
        }

        public bool UpdateAssetItem(Asset updatedAssetItem)
        {
            return _assetRepository.Update(updatedAssetItem);
        }
    }
}

using AsssetManagement.Models;
using AsssetManagement.StaticClasses;

using Couchbase.Lite;
using Couchbase.Lite.Query;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

using Xamarin.Forms;

namespace AsssetManagement.Repositories
{
    public interface IAssetRepository : IRepository<Asset, string>
    {
        new IList<Asset> GetAll(string searchText);
        new Asset Get(string assetId);
        new bool Save(Asset asset);
        new bool Update(Asset asset);
        new bool Remove(Asset asset);
    }
    public class AssetRepository : BaseRepository<Asset, string>, IAssetRepository
    {
        DatabaseConfiguration _databaseConfig;
        protected override DatabaseConfiguration DatabaseConfig
        {
            get
            {
                if (_databaseConfig == null)
                {
                    if (AppData.User?.EmailAddress == null)
                    {
                        throw new Exception($"Repository Exception: A valid user is required!");
                    }

                    _databaseConfig = new DatabaseConfiguration
                    {
                        Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                        AppData.User.EmailAddress)
                    };
                }

                return _databaseConfig;
            }
            set => _databaseConfig = value;
        }
        public AssetRepository() : base("asset")
        {
            var index = IndexBuilder.FullTextIndex(FullTextIndexItem.Property("Name")
                , FullTextIndexItem.Property("Description")
                , FullTextIndexItem.Property("Type")
                , FullTextIndexItem.Property("AssetTag")
                , FullTextIndexItem.Property("Model")
                , FullTextIndexItem.Property("Manufacturer")
                , FullTextIndexItem.Property("Status")
                , FullTextIndexItem.Property("Project")).IgnoreAccents(false);
            Database.CreateIndex("assetFTSIndex", index);
        }
        public override Asset Get(string id)
        {
            Asset asset = null;

            try
            {
                var document = Database.GetDocument(id);

                if (document != null)
                {
                    asset = new Asset
                    {
                        Id = Guid.Parse(document.Id),
                        Name = document.GetString("Name"),
                        AssetTag = document.GetString("AssetTag"),
                        Description = document.GetString("Description"),
                        Manufacturer = document.GetString("Manufacturer"),
                        Model = document.GetString("Model"),
                        Project = document.GetString("Project"),
                        Status = (AssetStatus)Enum.Parse(typeof(AssetStatus), document.GetString("Status").ToString()),
                        Type = (AssetType)Enum.Parse(typeof(AssetType), document.GetString("Type").ToString())
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserProfileRepository Exception: {ex.Message}");
            }

            return asset;
        }

        public override bool Save(Asset asset)
        {
            try
            {
                if (asset != null)
                {
                    // tag::docSet[]
                    var mutableDocument = new MutableDocument(asset.Id.ToString());
                    mutableDocument.SetString(nameof(asset.Name), asset.Name);
                    mutableDocument.SetString(nameof(asset.Description), asset.Description);
                    mutableDocument.SetString(nameof(asset.Type), asset.Type.ToString());
                    mutableDocument.SetString(nameof(asset.AssetTag), asset.AssetTag);
                    mutableDocument.SetString(nameof(asset.Model), asset.Model);
                    mutableDocument.SetString(nameof(asset.Manufacturer), asset.Manufacturer);
                    mutableDocument.SetString(nameof(asset.Status), asset.Status.ToString());
                    mutableDocument.SetString(nameof(asset.Project), asset.Project);
                    mutableDocument.SetString("type", "user");
                    //
                    Database.Save(mutableDocument);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserProfileRepository Exception: {ex.Message}");
            }

            return false;
        }
        public override bool Remove(Asset asset)
        {
            try
            {
                var document = Database.GetDocument(asset.Id.ToString());
                //
                if (asset != null)
                {
                    Database.Delete(document);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserProfileRepository Exception: {ex.Message}");
            }

            return false;
        }

        public override IList<Asset> GetAll(string searchText)
        {
            var assets = new List<Asset>();
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    var fromQuery = QueryBuilder.Select(SelectResult.Expression(Meta.ID)).From(DataSource.Database(Database));
                    foreach (var result in fromQuery.Execute())
                    {
                        var documentId = result.ToDictionary().Values.FirstOrDefault().ToString();
                        Asset asset = Get(documentId);
                        assets.Add(asset);
                    }
                }
                else
                {
                    var whereClause = FullTextExpression.Index("assetFTSIndex").Match($"'{searchText}'");
                    using (var whereQuery = QueryBuilder.Select(SelectResult.Expression(Meta.ID))
                        .From(DataSource.Database(Database))
                        .Where(whereClause))
                    {
                        foreach (var result in whereQuery.Execute())
                        {
                            var documentId = result.ToDictionary().Values.FirstOrDefault().ToString();
                            Asset asset = Get(documentId);
                            assets.Add(asset);
                        }
                    }
                }
                //
                return assets;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserProfileRepository Exception: {ex.Message}");
            }

            return assets;
        }

        public override bool Update(Asset asset)
        {
            try
            {
                var document = Database.GetDocument(asset.Id.ToString());

                if (document != null)
                {
                    var mutableDocument = document.ToMutable();
                    mutableDocument.SetString(nameof(asset.Name), asset.Name);
                    mutableDocument.SetString(nameof(asset.Description), asset.Description);
                    mutableDocument.SetString(nameof(asset.Type), asset.Type.ToString());
                    mutableDocument.SetString(nameof(asset.AssetTag), asset.AssetTag);
                    mutableDocument.SetString(nameof(asset.Model), asset.Model);
                    mutableDocument.SetString(nameof(asset.Manufacturer), asset.Manufacturer);
                    mutableDocument.SetString(nameof(asset.Status), asset.Status.ToString());
                    mutableDocument.SetString(nameof(asset.Project), asset.Project);
                    //
                    Database.Save(mutableDocument);
                    //
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserProfileRepository Exception: {ex.Message}");
            }

            return false;
        }
    }
}

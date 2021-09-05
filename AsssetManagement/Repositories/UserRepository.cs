using AsssetManagement.Models;
using AsssetManagement.StaticClasses;

using Couchbase.Lite;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Xamarin.Forms;

namespace AsssetManagement.Repositories
{
    public interface IUserRepository : IRepository<User, string>
    {
        new IList<User> GetAll(string searchText);
        new User Get(string userId);
        new bool Update(User user);
        new bool Save(User user);
        new bool Remove(User user);
    }
    public class UserRepository : BaseRepository<User, string>, IUserRepository
    {
        DatabaseConfiguration _databaseConfig;
        protected override DatabaseConfiguration DatabaseConfig
        {
            get
            {
                if (_databaseConfig == null)
                {
                    if (AppData.User.EmailAddress == null)
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
        public UserRepository() : base("user")
        { }
        public override User Get(string id)
        {
            User user = null;

            try
            {
                var document = Database.GetDocument(id);

                if (document != null)
                {
                    user = new User
                    {
                        Id = Guid.Parse(document.Id),
                        Name = document.GetString("Name"),
                        EmailAddress = document.GetString("EmailAddress"),
                    };
                    switch (int.Parse(document.GetString("Type")))
                    {
                        case 1:
                            user.Type = UserType.Employee;
                            break;
                        case 2:
                            user.Type = UserType.StoreManager;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserProfileRepository Exception: {ex.Message}");
            }

            return user;
        }

        public override bool Save(User user)
        {
            try
            {
                if (user != null)
                {
                    var mutableDocument = new MutableDocument(user.Id.ToString());
                    mutableDocument.SetString(nameof(user.Name), user.Name);
                    mutableDocument.SetString(nameof(user.EmailAddress), user.EmailAddress);
                    mutableDocument.SetString(nameof(user.Type), user.Type.ToString());
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
        public override bool Remove(User user)
        {
            try
            {
                var document = Database.GetDocument(user.Id.ToString());
                //
                if (user != null)
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

        public override IList<User> GetAll(string searchText)
        {
            var users = new List<User>();
            try
            {
                var indexes = Database.GetIndexes();
                //
                foreach (var index in indexes)
                {
                    User user = Get(index);
                    users.Add(user);
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserProfileRepository Exception: {ex.Message}");
            }

            return users;
        }
        public override bool Update(User user)
        {
            try
            {
                var document = Database.GetDocument(user.Id.ToString());

                if (document != null)
                {
                    var mutableDocument = document.ToMutable();
                    mutableDocument.SetString(nameof(user.Name), user.Name);
                    mutableDocument.SetString(nameof(user.EmailAddress), user.EmailAddress);
                    mutableDocument.SetString(nameof(user.Type), user.Type.ToString());
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

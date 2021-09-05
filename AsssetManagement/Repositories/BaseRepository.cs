using System;
using System.Collections.Generic;

using Couchbase.Lite;

namespace AsssetManagement.Repositories
{
    public interface IRepository<T, K> : IDisposable
    {
        IList<T> GetAll();
        T Get(K id);
        bool Save(T obj);
        bool Update(T obj);
        bool Remove(T obj);
    }
    public abstract class BaseRepository<T,K> : IRepository<T,K> where T : class
    {
        string DatabaseName { get; set; }
        ListenerToken DatabaseListenerToken { get; set; }

        protected virtual DatabaseConfiguration DatabaseConfig { get; set; }

        // tag::database[]
        Database _database;
        protected Database Database
        {
            get
            {
                if (_database == null)
                {
                    // tag::databaseCreate[]
                    _database = new Database(DatabaseName, DatabaseConfig);
                    // end::databaseCreate[]
                }

                return _database;
            }
            private set => _database = value;
        }
        // end::database[]

        protected BaseRepository(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new Exception($"Repository Exception: Database name cannot be null or empty!");
            }

            DatabaseName = databaseName;

            // tag::registerForDatabaseChanges[]
            DatabaseListenerToken = Database.AddChangeListener(OnDatabaseChangeEvent);
            // end::registerForDatabaseChanges[]
        }

        // tag::addChangeListener[]
        void OnDatabaseChangeEvent(object sender, DatabaseChangedEventArgs e)
        {
            foreach (var documentId in e.DocumentIDs)
            {
                var document = Database?.GetDocument(documentId);

                string message = $"Document (id={documentId}) was ";

                if (document == null)
                {
                    message += "deleted";
                }
                else
                {
                    message += "added/updaAted";
                }

                Console.WriteLine(message);
            }
        }
        // end::addChangeListener[]

        // tag::databaseClose[]
        public void Dispose()
        {
            DatabaseConfig = null;

            Database.RemoveChangeListener(DatabaseListenerToken);
            Database.Close();
            Database = null;
        }
        // end::databaseClose[]

        public abstract T Get(K id);
        public abstract bool Save(T obj);
        public abstract bool Remove(T obj);
        public abstract IList<T> GetAll();
        public abstract bool Update(T obj);
    }
}

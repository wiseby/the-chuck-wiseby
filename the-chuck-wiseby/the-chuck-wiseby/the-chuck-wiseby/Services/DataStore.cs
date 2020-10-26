using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using the_chuck_wiseby.Models;

namespace the_chuck_wiseby.Services
{
    public class DataStore : IDataStore<ChuckJoke>
    {
        private SQLiteAsyncConnection connection;

        public void Initialize(string dbName)
        {
            // Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3")
            var databasePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(databasePath, dbName);
            var connection = new SQLiteAsyncConnection(path);
            connection.CreateTableAsync<ChuckJoke>().Wait();
            this.connection = connection;
        }

        public async Task<IEnumerable<ChuckJoke>> Load()
        {
            return await connection.Table<ChuckJoke>().ToListAsync();
        }

        public async Task<int> Save(ChuckJoke data)
        {
            return await connection.InsertAsync(data);
        }

        public async Task<int> Delete(ChuckJoke data)
        {
            return await connection.DeleteAsync(data);
        }
    }
}

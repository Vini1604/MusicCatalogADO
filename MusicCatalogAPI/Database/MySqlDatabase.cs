using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Database
{
    public class MySqlDatabase : IDatabase
    {

        public readonly MySqlConnection _connection;
        public MySqlDatabase(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}

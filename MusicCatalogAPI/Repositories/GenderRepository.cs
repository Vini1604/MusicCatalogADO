using MusicCatalogAPI.Database;
using MusicCatalogAPI.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public class GenderRepository : IRepository<Gender>
    {
        private readonly MySqlDatabase _db;

        public GenderRepository(IDatabase db)
        {
            _db = db as MySqlDatabase;
        }

        public async Task Delete(Guid id)
        {
            string query = "DELETE FROM gender WHERE GenderId = @GenderId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@GenderId", id.ToString());
                try
                {
                    await _db._connection.OpenAsync();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task<List<Gender>> GetAll()
        {
            string query = "SELECT * FROM gender ORDER BY GenderDescription";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                List<Gender> genders = new List<Gender>();
                try
                {
                    await _db._connection.OpenAsync();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            genders.Add(new Gender { GenderId = Guid.Parse(reader["GenderId"].ToString()), GenderDescription = reader["GenderDescription"].ToString() });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return genders;
            }
        }

        public async Task<Gender> GetById(Guid id)
        {
            string query = "SELECT * FROM gender WHERE GenderId = @GenderId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@GenderId", id);
                Gender gender = null;
                try
                {
                    await _db._connection.OpenAsync();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                gender = new Gender
                                {
                                    GenderId = Guid.Parse(reader["GenderId"].ToString()),
                                    GenderDescription = reader["GenderDescription"].ToString(),
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return gender;
            }
        }

        public async Task Insert(Gender entity)
        {
            string query = "INSERT INTO gender (GenderId, GenderDescription) VALUES (@GenderId, @GenderDescription)";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@GenderId", entity.GenderId.ToString());
                cmd.Parameters.AddWithValue("@GenderDescription", entity.GenderDescription);
                try
                {
                    await _db._connection.OpenAsync();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public async Task Update(Gender entity)
        {
            var connection = new MySqlDatabase("Server=localhost;Database=musiccatalog;User=root;Password=1234");
            string query = "UPDATE gender SET GenderDescription = @GenderDescription WHERE GenderId = @GenderId";
            var cmd = new MySqlCommand(query, connection._connection);
            cmd.Parameters.AddWithValue("@GenderId", entity.GenderId);
            cmd.Parameters.AddWithValue("@GenderDescription", entity.GenderDescription);
            try
            {
                await connection._connection.OpenAsync();
                cmd.ExecuteNonQuery();
                connection._connection.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

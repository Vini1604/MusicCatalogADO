using MusicCatalogAPI.Database;
using MusicCatalogAPI.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly MySqlDatabase _db;

        public ArtistRepository(IDatabase db)
        {
            _db = db as MySqlDatabase;
        }

        public async Task Delete(Guid id)
        {
            string query = "DELETE FROM ARTIST WHERE ArtistId = @ArtistId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@ArtistId", id.ToString());
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

        public async Task<List<Artist>> GetAll()
        {
            string query = "SELECT * FROM ARTIST ORDER BY ArtistName";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                List<Artist> artists = new List<Artist>();
                try
                {
                    await _db._connection.OpenAsync();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            artists.Add(new Artist { ArtistId = Guid.Parse(reader["ArtistId"].ToString()), ArtistName = reader["ArtistName"].ToString() });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return artists;
            }
        }

        public async Task<Artist> GetById(Guid id)
        {
            string query = "SELECT * FROM ARTIST WHERE ArtistId = @ArtistId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@ArtistId", id);
                Artist artist = null;
                try
                {
                    await _db._connection.OpenAsync();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                artist = new Artist
                                {
                                    ArtistId = Guid.Parse(reader["ArtistId"].ToString()),
                                    ArtistName = reader["ArtistName"].ToString(),
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return artist;
            }
        }

        public async Task Insert(Artist entity)
        {
            string query = "INSERT INTO artist (ArtistId, ArtistName) VALUES (@ArtistId, @ArtistName)";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@ArtistId", entity.ArtistId.ToString());
                cmd.Parameters.AddWithValue("@ArtistName", entity.ArtistName);
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

        public async Task Update(Artist entity)
        {
            var connection = new MySqlDatabase("Server=localhost;Database=musiccatalog;User=root;Password=1234");
            string query = "UPDATE artist SET ArtistName = @ArtistName WHERE ArtistId = @ArtistId";
            var cmd = new MySqlCommand(query, connection._connection);
            cmd.Parameters.AddWithValue("@ArtistId", entity.ArtistId);
            cmd.Parameters.AddWithValue("@ArtistName", entity.ArtistName);
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

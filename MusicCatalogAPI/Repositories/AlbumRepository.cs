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
    public class AlbumRepository : IRepository<Album>
    {
        private readonly MySqlDatabase _db;

        public AlbumRepository(IDatabase db)
        {
            _db = db as MySqlDatabase;
        }

        public async Task Delete(Guid id)
        {
            string query = "DELETE FROM album WHERE AlbumId = @AlbumId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@AlbumId", id.ToString());
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

        public async Task<List<Album>> GetAll()
        {
            string query = "SELECT alb.AlbumId, alb.AlbumName, alb.ReleasedYear, art.ArtistId, art.ArtistName, g.GenderId, g.GenderDescription FROM album alb " +
                "inner join artist art " +
                "on alb.ArtistId = art.ArtistId " +
                "inner join gender g " +
                "on alb.GenderId = g.GenderId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                List<Album> albuns = new List<Album>();
                try
                {
                    await _db._connection.OpenAsync();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            albuns.Add(new Album
                            {
                                AlbumId = Guid.Parse(reader["AlbumId"].ToString()),
                                AlbumName = reader["AlbumName"].ToString(),
                                Year = reader["ReleasedYear"].ToString(),
                                ArtistId = Guid.Parse(reader["ArtistId"].ToString()),
                                ArtistName = reader["ArtistName"].ToString(),
                                GenderId = Guid.Parse(reader["GenderId"].ToString()),
                                GenderDescription = reader["GenderDescription"].ToString()

                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return albuns;
            }
        }

        public async Task<Album> GetById(Guid id)
        {
            string query = "SELECT alb.AlbumId, alb.AlbumName, alb.ReleasedYear, art.ArtistId, art.ArtistName, g.GenderId, g.GenderDescription FROM album alb " +
                "inner join artist art " +
                "on alb.ArtistId = art.ArtistId " +
                "inner join gender g " +
                "on alb.GenderId = g.GenderId " +
                "where alb.AlbumId = @AlbumId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@AlbumId", id);
                Album album = null;
                try
                {
                    await _db._connection.OpenAsync();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                album = new Album
                                {
                                    AlbumId = Guid.Parse(reader["AlbumId"].ToString()),
                                    AlbumName = reader["AlbumName"].ToString(),
                                    Year = reader["ReleasedYear"].ToString(),
                                    ArtistId = Guid.Parse(reader["ArtistId"].ToString()),
                                    ArtistName = reader["ArtistName"].ToString(),
                                    GenderId = Guid.Parse(reader["GenderId"].ToString()),
                                    GenderDescription = reader["GenderDescription"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return album;
            }
        }

        public async Task Insert(Album entity)
        {
       
            string query = "Insert into album(AlbumId, AlbumName, ReleasedYear, ArtistId, GenderId) values(@AlbumId, @AlbumName, @ReleasedYear, @ArtistId, @GenderId)";
            
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@AlbumId", entity.AlbumId.ToString());
                cmd.Parameters.AddWithValue("@AlbumName", entity.AlbumName.ToString());
                cmd.Parameters.AddWithValue("@ReleasedYear", entity.Year.ToString());
                cmd.Parameters.AddWithValue("@ArtistId", entity.ArtistId.ToString());
                cmd.Parameters.AddWithValue("@GenderId", entity.GenderId.ToString());
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

        public async Task Update(Album entity)
        {
            var connection = new MySqlDatabase("Server=localhost;Database=musiccatalog;User=root;Password=1234");
            string query = "UPDATE Album SET AlbumName = @AlbumName, ReleasedYear = @ReleasedYear WHERE AlbumId = @AlbumId";
            var cmd = new MySqlCommand(query, connection._connection);
            cmd.Parameters.AddWithValue("@AlbumId", entity.AlbumId);
            cmd.Parameters.AddWithValue("@AlbumName", entity.AlbumName);
            cmd.Parameters.AddWithValue("@ReleasedYear", entity.Year);
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

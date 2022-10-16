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
    public class MusicRepository : IRepository<Music>
    {
        private readonly MySqlDatabase _db;

        public MusicRepository(IDatabase db)
        {
            _db = db as MySqlDatabase;
        }

        public async Task Delete(Guid id)
        {
            string query = "DELETE FROM music WHERE MusicId = @MusicId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@MusicId", id.ToString());
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

        public async Task<List<Music>> GetAll()
        {
            string query = "SELECT * FROM Music ORDER BY MusicName";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                List<Music> musics = new List<Music>();
                try
                {
                    await _db._connection.OpenAsync();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            musics.Add(new Music { MusicId = Guid.Parse(reader["MusicId"].ToString()), MusicName = reader["MusicName"].ToString(), Duration = reader["Duration"].ToString() });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return musics;
            }
        }

        public async Task<Music> GetById(Guid id)
        {
            string query = "SELECT * FROM music WHERE MusicId = @MusicId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@MusicId", id);
                Music music = null;
                try
                {
                    await _db._connection.OpenAsync();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                music = new Music
                                {
                                    MusicId = Guid.Parse(reader["MusicId"].ToString()),
                                    MusicName = reader["MusicName"].ToString(),
                                    Duration = reader["Duration"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return music;
            }
        }

        public async Task Insert(Music entity)
        {
            string query = "INSERT INTO music (MusicId, MusicName, Duration) VALUES (@MusicId, @MusicName, @Duration)";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@MusicId", entity.MusicId.ToString());
                cmd.Parameters.AddWithValue("@MusicName", entity.MusicName);
                cmd.Parameters.AddWithValue("@Duration", entity.Duration.ToString());
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

        public async Task Update(Music entity)
        {
            var connection = new MySqlDatabase("Server=localhost;Database=musiccatalog;User=root;Password=1234");
            string query = "UPDATE music SET MusicName = @MusicName, Duration = @Duration WHERE MusicId = @MusicId";
            var cmd = new MySqlCommand(query, connection._connection);
            cmd.Parameters.AddWithValue("@MusicId", entity.MusicId);
            cmd.Parameters.AddWithValue("@MusicName", entity.MusicName);
            cmd.Parameters.AddWithValue("@Duration", entity.Duration);
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

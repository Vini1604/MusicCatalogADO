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
    public class AlbumMusicRepository : IAlbumMusicRepository
    {
        private readonly MySqlDatabase _db;

        public AlbumMusicRepository(IDatabase db)
        {
            _db = db as MySqlDatabase;
        }

        public async Task Delete(Guid idAlbum, Guid idMusic)
        {
            string query = "DELETE FROM album_music WHERE AlbumId = @AlbumId and MusicId = @MusicId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@AlbumId", idAlbum.ToString());
                cmd.Parameters.AddWithValue("@MusicId", idMusic.ToString());
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

        public async Task<AlbumMusic> GetAlbumMusic(Guid idAlbum, Guid idMusic)
        {
            string query = "SELECT albM.AlbumId, alb.AlbumName, albM.MusicId, m.MusicName FROM album_music albM " +
                "inner join album alb " +
                "on albM.AlbumId = alb.AlbumId " +
                "inner join music m " +
                "on albM.MusicId = m.MusicId " +
                "where albM.AlbumId = @AlbumId and albM.MusicId = @MusicId";
            var cmd = new MySqlCommand(query, _db._connection);
            cmd.Parameters.AddWithValue("@AlbumId", idAlbum);
            cmd.Parameters.AddWithValue("@MusicId", idMusic);
            AlbumMusic albunsMusics = null;
            try
            {
                await _db._connection.OpenAsync();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            albunsMusics = new AlbumMusic
                            {
                                AlbumId = Guid.Parse(reader["AlbumId"].ToString()),
                                AlbumName = reader["AlbumName"].ToString(),
                                MusicId = Guid.Parse(reader["MusicId"].ToString()),
                                MusicName = reader["MusicName"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return albunsMusics;
        }

        public async Task<List<AlbumMusic>> GetAll()
        {
            string query = "SELECT albM.AlbumId, alb.AlbumName, albM.MusicId, m.MusicName FROM album_music albM " +
                "inner join album alb " +
                "on albM.AlbumId = alb.AlbumId " +
                "inner join music m " +
                "on albM.MusicId = m.MusicId " +
                "order by alb.AlbumName, albM.MusicId";
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                List<AlbumMusic> albunsMusics = new List<AlbumMusic>();
                try
                {
                    await _db._connection.OpenAsync();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            albunsMusics.Add(new AlbumMusic
                            {
                                AlbumId = Guid.Parse(reader["AlbumId"].ToString()),
                                AlbumName = reader["AlbumName"].ToString(),
                                MusicId = Guid.Parse(reader["MusicId"].ToString()),
                                MusicName = reader["MusicName"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return albunsMusics;
            }
        }

        public async Task<List<AlbumMusic>> GetByAlbumId(Guid id)
        {
            string query = "SELECT albM.AlbumId, alb.AlbumName, albM.MusicId, m.MusicName FROM album_music albM " +
                "inner join album alb " +
                "on albM.AlbumId = alb.AlbumId " +
                "inner join music m " +
                "on albM.MusicId = m.MusicId " +
                "where albM.AlbumId = @AlbumId " +
                "order by albM.MusicId";
            var cmd = new MySqlCommand(query, _db._connection);
            cmd.Parameters.AddWithValue("@AlbumId", id);
            List<AlbumMusic> albunsMusics = new List<AlbumMusic>();
            try
            {
                await _db._connection.OpenAsync();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        albunsMusics.Add(new AlbumMusic
                        {
                            AlbumId = Guid.Parse(reader["AlbumId"].ToString()),
                            AlbumName = reader["AlbumName"].ToString(),
                            MusicId = Guid.Parse(reader["MusicId"].ToString()),
                            MusicName = reader["MusicName"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return albunsMusics;
        }

        public async Task Insert(AlbumMusic entity)
        {
       
            string query = "Insert into album_music(AlbumId, MusicId) values(@AlbumId, @MusicId)";
            
            using (_db)
            {
                var cmd = new MySqlCommand(query, _db._connection);
                cmd.Parameters.AddWithValue("@AlbumId", entity.AlbumId.ToString());
                cmd.Parameters.AddWithValue("@MusicId", entity.MusicId.ToString());
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

        public async Task Update(AlbumMusic entity)
        {
            var connection = new MySqlDatabase("Server=localhost;Database=musiccatalog;User=root;Password=1234");
            string query = "UPDATE Album SET AlbumId = @AlbumId, MusicId = @MusicId WHERE AlbumId = @AlbumId";
            var cmd = new MySqlCommand(query, connection._connection);
            cmd.Parameters.AddWithValue("@AlbumId", entity.AlbumId);
            cmd.Parameters.AddWithValue("@MusicId", entity.MusicId);
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

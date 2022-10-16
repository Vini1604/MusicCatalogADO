using MusicCatalogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public interface IAlbumMusicRepository
    {
        Task<List<AlbumMusic>> GetAll();
        Task<List<AlbumMusic>> GetByAlbumId(Guid id);
        Task<AlbumMusic>GetAlbumMusic(Guid idAlbum, Guid idMusic);
        Task Insert(AlbumMusic entity);
        Task Update(AlbumMusic entity);
        Task Delete(Guid idAlbum, Guid idMusic);
    }
}

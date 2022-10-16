using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Models;
using MusicCatalogAPI.Models.Dtos;
using MusicCatalogAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumMusicController : ControllerBase
    {

        private readonly IAlbumMusicRepository _repo;

        public AlbumMusicController(IAlbumMusicRepository repository)
        {
            _repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<AlbumMusic>>> GetAlbunsMusics()
        {
            var albunsMusics = await _repo.GetAll();
            if (albunsMusics == null)
            {
                return NoContent();
            };
            return Ok(albunsMusics);
        }
        [HttpGet("{idAlbum:guid}")]
        public async Task<ActionResult<List<AlbumMusic>>> GetByAlbumId(Guid idAlbum)
        {
            var albumMusics = await _repo.GetByAlbumId(idAlbum);
            if (albumMusics == null)
            {
                return NotFound();
            };
            return Ok(albumMusics);
        }
        [HttpGet("{idAlbum}/{idMusic}")]
        public async Task<ActionResult<List<AlbumMusic>>> GetMusicAlbum(Guid idAlbum, Guid idMusic)
        {
            var albumMusic = await _repo.GetAlbumMusic(idAlbum, idMusic);
            if (albumMusic == null)
            {
                return NotFound();
            };
            return Ok(albumMusic);
        }
        [HttpPost]
        public async Task<ActionResult<AlbumMusic>> InsertMusic(AlbumMusicDTO albumMusic)
        {
            AlbumMusic newAlbumMusic = new AlbumMusic
            {
                AlbumId = albumMusic.AlbumId,
                MusicId = albumMusic.MusicId
            };
            await _repo.Insert(newAlbumMusic);

            return newAlbumMusic;
        }

        [HttpPut("{idAlbum}/{idMusic}")]
        public async Task<ActionResult> UpdateAlbum(Guid idAlbum, Guid idMusic, AlbumMusicDTO albumMusicUpdated)
        {
            var existingAlbumMusic = await _repo.GetAlbumMusic(idAlbum, idMusic);
            if (existingAlbumMusic == null)
            {
                return NotFound();
            }
            existingAlbumMusic.AlbumId = albumMusicUpdated.AlbumId;
            existingAlbumMusic.MusicId = albumMusicUpdated.MusicId;
            await _repo.Update(existingAlbumMusic);
            return NoContent();

        }
        [HttpDelete("{idAlbum}/{idMusic}")]
        public async Task<ActionResult<Album>> DeleteAlbum(Guid idAlbum, Guid idMusic)
        {
            await _repo.Delete(idAlbum, idMusic);
            return NoContent();
        }

    }
}

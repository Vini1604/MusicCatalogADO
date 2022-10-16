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
    public class AlbumController : ControllerBase
    {

        private readonly IRepository<Album> _repo; 
        
        public AlbumController(IRepository<Album> repository)
        {
            _repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Album>>> GetAlbuns()
        {
            var albuns =  await _repo.GetAll();
            if (albuns == null)
            {
                return NoContent();
            };
            return Ok(albuns);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Music>> GetAlbum(Guid id)
        {
            var album = await _repo.GetById(id);
            if (album == null)
            {
                return NotFound();
            };
            return Ok(album);
        }
        [HttpPost]
        public async Task<ActionResult<Album>> InsertMusic(AlbumDTO album)
        {
            Album newAlbum = new Album
            {
                AlbumId = Guid.NewGuid(),
                AlbumName = album.AlbumName,
                Year = album.Year,
                ArtistId = album.ArtistId,
                GenderId = album.GenderId
            };
            await _repo.Insert(newAlbum);

            return newAlbum;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAlbum(Guid id, AlbumDTO albumUpdated)
        {
            var existingAlbum = await _repo.GetById(id);
            if (existingAlbum == null)
            {
                return NotFound();
            }
            existingAlbum.AlbumName = albumUpdated.AlbumName;
            existingAlbum.Year = albumUpdated.Year;
            await _repo.Update(existingAlbum);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Album>> DeleteAlbum(Guid id)
        {
            await _repo.Delete(id);
            return NoContent();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Models;
using MusicCatalogAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {

        private readonly IRepository<Artist> _repo; 
        
        public ArtistController(IRepository<Artist> repository)
        {
            _repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Artist>>> GetArtists()
        {
            var artists =  await _repo.GetAll();
            if (artists == null)
            {
                return NoContent();
            };
            return Ok(artists);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<Artist>>> GetArtist(Guid id)
        {
            var artist = await _repo.GetById(id);
            if (artist == null)
            {
                return NotFound();
            };
            return Ok(artist);
        }
        [HttpPost]
        public async Task<ActionResult<Artist>> InsertArtist(ArtistDTO artist)
        {
            Artist newArtist = new Artist
            {
                ArtistId = Guid.NewGuid(),
                ArtistName = artist.ArtistName
            };
            await _repo.Insert(newArtist);

            return newArtist;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArtist(Guid id, ArtistDTO artistUpdated)
        {
            var existingArtist = await _repo.GetById(id);
            if (existingArtist == null)
            {
                return NotFound();
            }
            existingArtist.ArtistName = artistUpdated.ArtistName;
            await _repo.Update(existingArtist);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> DeleteArtist(Guid id)
        {
            await _repo.Delete(id);
            return NoContent();
        }

    }
}

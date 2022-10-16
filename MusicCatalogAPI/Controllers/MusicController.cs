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
    public class MusicController : ControllerBase
    {

        private readonly IRepository<Music> _repo; 
        
        public MusicController(IRepository<Music> repository)
        {
            _repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Music>>> GetMusics()
        {
            var musics =  await _repo.GetAll();
            if (musics == null)
            {
                return NoContent();
            };
            return Ok(musics);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Music>> GetMusic(Guid id)
        {
            var music = await _repo.GetById(id);
            if (music == null)
            {
                return NotFound();
            };
            return Ok(music);
        }
        [HttpPost]
        public async Task<ActionResult<Music>> InsertMusic(MusicDTO music)
        {
            Music newMusic = new Music
            {
                MusicId = Guid.NewGuid(),
                MusicName = music.MusicName,
                Duration = music.Duration
            };
            await _repo.Insert(newMusic);

            return newMusic;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMusic(Guid id, MusicDTO musicUpdated)
        {
            var existingMusic = await _repo.GetById(id);
            if (existingMusic == null)
            {
                return NotFound();
            }
            existingMusic.MusicName = musicUpdated.MusicName;
            existingMusic.Duration = musicUpdated.Duration;
            await _repo.Update(existingMusic);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Music>> DeleteMusic(Guid id)
        {
            await _repo.Delete(id);
            return NoContent();
        }

    }
}

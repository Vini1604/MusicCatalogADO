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
    public class GenderController : ControllerBase
    {

        private readonly IRepository<Gender> _repo; 
        
        public GenderController(IRepository<Gender> repository)
        {
            _repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Gender>>> GetGenders()
        {
            var genders =  await _repo.GetAll();
            if (genders == null)
            {
                return NoContent();
            };
            return Ok(genders);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Gender>> GetGender(Guid id)
        {
            var gender = await _repo.GetById(id);
            if (gender == null)
            {
                return NotFound();
            };
            return Ok(gender);
        }
        [HttpPost]
        public async Task<ActionResult<Gender>> InsertArtist(GenderDTO gender)
        {
            Gender newGender = new Gender
            {
                GenderId = Guid.NewGuid(),
                GenderDescription = gender.GenderDescription
            };
            await _repo.Insert(newGender);

            return newGender;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGender(Guid id, GenderDTO genderUpdated)
        {
            var existingGender = await _repo.GetById(id);
            if (existingGender == null)
            {
                return NotFound();
            }
            existingGender.GenderDescription = genderUpdated.GenderDescription;
            await _repo.Update(existingGender);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gender>> DeleteGender(Guid id)
        {
            await _repo.Delete(id);
            return NoContent();
        }

    }
}

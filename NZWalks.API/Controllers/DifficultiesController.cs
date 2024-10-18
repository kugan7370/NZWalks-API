using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;
        

        public DifficultiesController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var difficulties = dbContext.Difficulties.ToList();
            return Ok(difficulties);
        }

        [HttpPost]
        public IActionResult AddDifficulty(Difficulty difficulty)
        {
            dbContext.Difficulties.Add(difficulty);
            dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDifficultyById(Guid id)
        {
            var difficulty = dbContext.Difficulties.Find(id);
            if (difficulty == null)
            {
                return NotFound();
            }
            return Ok(difficulty);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateDifficulty(Guid id, Difficulty updatedDifficulty)
        {
            var existingDifficulty = dbContext.Difficulties.Find(id);
            if (existingDifficulty == null)
            {
                return NotFound();
            }

            existingDifficulty.Name = updatedDifficulty.Name;
            dbContext.SaveChanges();
            return Ok(existingDifficulty);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteDifficulty(Guid id)
        {
            var difficulty = dbContext.Difficulties.Find(id);
            if (difficulty == null)
            {
                return NotFound();
            }

            dbContext.Difficulties.Remove(difficulty);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}

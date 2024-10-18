using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public WalksController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Include region and difficulty in the query to get the related data
            var walks = dbContext.Walks.Include(w => w.Region).Include(w => w.Difficulty).ToList();
            return Ok(walks);
        }

        [HttpPost]
        public IActionResult AddWalk(Walk walk)
        {
            dbContext.Walks.Add(walk);
            dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetWalkById(Guid id)
        {
            var walk = dbContext.Walks.Find(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(walk);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateWalk(Guid id, Walk updatedWalk)
        {
            var existingWalk = dbContext.Walks.Find(id);
            if (existingWalk == null)
            {
                return NotFound();
            }

            existingWalk.Name = updatedWalk.Name;
            existingWalk.Description = updatedWalk.Description;
            existingWalk.LenthInKm = updatedWalk.LenthInKm;
            existingWalk.WalkImageUrl = updatedWalk.WalkImageUrl;
            existingWalk.RegionId = updatedWalk.RegionId;
            existingWalk.DifficultyId = updatedWalk.DifficultyId;

            dbContext.SaveChanges();
            return Ok(existingWalk);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteWalk(Guid id)
        {
            var walk = dbContext.Walks.Find(id);
            if (walk == null)
            {
                return NotFound();
            }

            dbContext.Walks.Remove(walk);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}

using Microsoft.AspNetCore.Http; // Provides HTTP-specific features
using Microsoft.AspNetCore.Mvc; // Enables MVC functionalities like controllers and actions
using Microsoft.EntityFrameworkCore; // ORM for database operations
using NZWalks.API.Data; // Includes DbContext for database access
using NZWalks.API.Models.Domains; // Includes domain models (e.g., Region)

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")] // Defines route as "api/Regions"
    [ApiController] // Marks this class as an API controller
    public class RegionsController : ControllerBase
    {
        //A readonly field can only be assigned a value once, either at the time of declaration or within a constructor.
        private readonly NZWalksDbContext dbContext;

        // Constructor injects DbContext for database operations
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet] // HTTP GET method for fetching data
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList(); // Fetch all regions from the database
            return Ok(regions); // Return regions in HTTP 200 OK response
        }

        [HttpPost]
        public IActionResult AddRegion(Region region)
        {
            dbContext.Regions.Add(region); // Add new region to the database
            dbContext.SaveChanges(); // Save changes to the database
            return StatusCode(StatusCodes.Status201Created); // Return HTTP 201 Created status
        }

        [HttpGet]
        [Route("{id}")] // Route parameter for region ID
        public IActionResult GetRegionById(Guid id)
        {
            var region = dbContext.Regions.Find(id); // Find region by ID
            if (region == null)
            {
                return NotFound(); // Return HTTP 404 Not Found if region not found
            }
            return Ok(region); // Return region in HTTP 200 OK response
        }

        [HttpPut]
        [Route("{id}")] // Route parameter for region ID
        public IActionResult UpdateRegion(Guid id, Region updatedRegion)
        {
            var existingRegion = dbContext.Regions.Find(id); // Find existing region by ID
            if (existingRegion == null)
            {
                return NotFound(); // Return HTTP 404 Not Found if region not found
            }

            existingRegion.Name = updatedRegion.Name; // Update region name
            existingRegion.Code = updatedRegion.Code; // Update region code

            dbContext.SaveChanges(); // Save changes to the database
            return Ok(existingRegion); // Return updated region in HTTP 200 OK response
        }

        [HttpDelete]
        [Route("{id}")] // Route parameter for region ID
        public IActionResult DeleteRegion(Guid id)
        {
            var region = dbContext.Regions.Find(id); // Find region by ID
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound(); // Return HTTP 404 Not Found if region not found
            }

            dbContext.Regions.Remove(region); // Remove region from the database
            dbContext.SaveChanges(); // Save changes to the database
            return Ok(); // Return HTTP 200 OK response
        }

        [HttpGet]
        [Route("search")] // Route for searching regions by name
        public IActionResult SearchRegionsByName([FromQuery] string name)
        {
            var regions = dbContext.Regions.Where(r => r.Name.Contains(name)).ToList(); // Filter regions by name
            return Ok(regions); // Return filtered regions in HTTP 200 OK response
        }

        //get region count
        [HttpGet]
        [Route("count")] // Route for counting regions
        public IActionResult GetRegionCount()
        {
            var count = dbContext.Regions.Count(); // Count the number of regions
            return Ok(count); // Return region count in HTTP 200 OK response
        }




    }
}

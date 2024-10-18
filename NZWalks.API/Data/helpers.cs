using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Data
{
    public class Helpers
    {
        private readonly NZWalksDbContext dbContext;

        // Constructor to inject the DbContext
        public Helpers(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Fetch all records from the Regions table
        public List<Region> GetAllRegions()
        {
            var regions = dbContext.Regions.ToList();
            return regions;
        }

        // Find a single region by primary key (id)
        public Region GetRegionById(Guid id)
        {
            var region = dbContext.Regions.Find(id);
            return region;
        }

        // Filter regions by name containing a specific string
        public List<Region> GetRegionsByName(string name)
        {
            var regions = dbContext.Regions.Where(r => r.Name.Contains(name)).ToList();
            return regions;
        }

        // Add a new region
        public Region AddRegion(Region region)
        {
            dbContext.Regions.Add(region);
            dbContext.SaveChanges();
            return region;
        }

        // Update an existing region
        public Region UpdateRegion(Guid id, Region updatedRegion)
        {
            var existingRegion = dbContext.Regions.Find(id);
            if (existingRegion != null)
            {
                existingRegion.Name = updatedRegion.Name;
                existingRegion.Code = updatedRegion.Code;

                dbContext.SaveChanges();
            }
            return existingRegion;
        }

        // Delete a region by ID
        public bool DeleteRegion(Guid id)
        {
            var region = dbContext.Regions.Find(id);
            if (region != null)
            {
                dbContext.Regions.Remove(region);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        // Fetch regions and include related Walks (eager loading)
        //public List<Region> GetRegionsWithWalks()
        //{
        //    var regionsWithWalks = dbContext.Regions.Include(r => r.Walks).ToList();
        //    return regionsWithWalks;
        //}

        // Check if a region exists by name
        public bool RegionExists(string name)
        {
            return dbContext.Regions.Any(r => r.Name == name);
        }

        // Count the total number of regions
        public int GetRegionCount()
        {
            return dbContext.Regions.Count();
        }

        // Paginate results (skip and take)
        public List<Region> GetRegionsPaged(int skip, int take)
        {
            var pagedRegions = dbContext.Regions.Skip(skip).Take(take).ToList();
            return pagedRegions;
        }

        // Raw SQL query to get all regions
        public List<Region> GetRegionsRawSQL()
        {
            var regions = dbContext.Regions.FromSqlRaw("SELECT * FROM Regions").ToList();
            return regions;
        }
    }
}

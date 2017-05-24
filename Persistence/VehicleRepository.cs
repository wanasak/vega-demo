using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega_demo.Models;

namespace vega_demo.Persistence
{
    public class VehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> GetVehicle(int id)
        {
            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .SingleOrDefaultAsync(v => v.Id == id);
        }
    }
}
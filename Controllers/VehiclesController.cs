using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega_demo.Controllers.Resources;
using vega_demo.Models;
using vega_demo.Persistence;

namespace vega_demo.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext context;
        public VehiclesController(IMapper mapper, VegaDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]SaveVehicleResource SaveVehicleResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Low priority
            // var model = await context.Models.FindAsync(SaveVehicleResource.ModelId);
            // if (model == null)
            // {
            //     ModelState.AddModelError("ModelId", "Invalid modelId.");
            //     return BadRequest(ModelState);
            // }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(SaveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]SaveVehicleResource SaveVehicleResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehicle = await context.Vehicles.Include(m => m.Features).SingleOrDefaultAsync(v => v.Id == SaveVehicleResource.Id);
            
            if (vehicle == null) return NotFound();

            vehicle = mapper.Map<SaveVehicleResource, Vehicle>(SaveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await context.Vehicles.FindAsync(id);

            if (vehicle == null) return NotFound();

            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
            
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle == null) return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega_demo.Controllers.Resources;
using vega_demo.Models;

namespace vega_demo.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        public VehiclesController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody]VehicleResource vehicleResource)
        {
            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            return Ok(vehicle);
        }
    }
}
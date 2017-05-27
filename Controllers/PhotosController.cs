using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vega_demo.Controllers.Resources;
using vega_demo.Models;
using vega_demo.Persistence;

namespace vega_demo.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PhotosController(
            IHostingEnvironment host,
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.vehicleRepository = vehicleRepository;
            this.host = host;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await vehicleRepository.GetVehicle(vehicleId, isIncludeRelated: false);
            if (vehicle == null) return NotFound();

            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
    }
}
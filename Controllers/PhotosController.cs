using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vega_demo.Controllers.Resources;
using vega_demo.Core.Services;
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
        // private readonly int MAX_BYTES = 1 * 1024 * 1024;
        // private readonly string[] ACCEPTED_FILE_TYPES = new string[] { ".jpg", ".jpeg", ".png" };
        private readonly PhotoSetting photoSettings;
        private readonly IPhotoRepository photoRepository;
        private readonly IPhotoService photoService;

        public PhotosController(
            IHostingEnvironment host,
            IVehicleRepository vehicleRepository,
            IPhotoRepository photoRepository,
            IPhotoService photoService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsSnapshot<PhotoSetting> options)
        {
            this.vehicleRepository = vehicleRepository;
            this.photoRepository = photoRepository;
            this.photoService = photoService;
            this.host = host;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.photoSettings = options.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int vehicleId)
        {
            var photos = await this.photoRepository.GetPhotos(vehicleId);
            return Ok(mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos));
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await vehicleRepository.GetVehicle(vehicleId, isIncludeRelated: false);
            if (vehicle == null) return NotFound();

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size");
            if (!photoSettings.IsSupportedFileType(file.FileName)) return BadRequest("Invalid file type.");

            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
            // if (!Directory.Exists(uploadFolderPath))
            //     Directory.CreateDirectory(uploadFolderPath);
            // var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            // var filePath = Path.Combine(uploadFolderPath, fileName);
            // using (var stream = new FileStream(filePath, FileMode.Create))
            // {
            //     await file.CopyToAsync(stream);
            // }
            // var photo = new Photo { FileName = fileName };
            // vehicle.Photos.Add(photo);
            // await unitOfWork.CompleteAsync();
            var photo = await photoService.UploadPhotoAsync(vehicle, file, uploadFolderPath);

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
    }
}
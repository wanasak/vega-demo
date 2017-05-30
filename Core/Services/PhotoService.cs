using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using vega_demo.Models;
using vega_demo.Persistence;

namespace vega_demo.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPhotoStorage photoStorage;
        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            this.photoStorage = photoStorage;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Photo> UploadPhotoAsync(Vehicle vehicle, IFormFile file, string uploadFolderPath)
        {
            // if (!Directory.Exists(uploadFolderPath))
            //     Directory.CreateDirectory(uploadFolderPath);

            // var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            // var filePath = Path.Combine(uploadFolderPath, fileName);

            // using (var stream = new FileStream(filePath, FileMode.Create))
            // {
            //     await file.CopyToAsync(stream);
            // }

            var fileName = await photoStorage.StorePhotoAsync(file, uploadFolderPath);

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return photo;
        }
    }
}
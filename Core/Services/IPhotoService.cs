using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using vega_demo.Models;

namespace vega_demo.Core.Services
{
    public interface IPhotoService
    {
         Task<Photo> UploadPhotoAsync(Vehicle vehicle, IFormFile file, string uploadFolderPath);
    }
}
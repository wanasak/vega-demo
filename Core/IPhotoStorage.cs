using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace vega_demo.Core
{
    public interface IPhotoStorage
    {
         Task<string> StorePhotoAsync(IFormFile file, string uploadFolderPath);
    }
}
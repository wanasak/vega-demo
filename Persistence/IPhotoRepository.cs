using System.Collections.Generic;
using System.Threading.Tasks;
using vega_demo.Models;

namespace vega_demo.Persistence
{
    public interface IPhotoRepository
    {
         Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}
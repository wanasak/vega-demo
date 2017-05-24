using System.Threading.Tasks;
using vega_demo.Models;

namespace vega_demo.Persistence
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id);
    }
}
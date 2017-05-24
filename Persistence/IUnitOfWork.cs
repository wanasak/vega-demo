using System.Threading.Tasks;

namespace vega_demo.Persistence
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}
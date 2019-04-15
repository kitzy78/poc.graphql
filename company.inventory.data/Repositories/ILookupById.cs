using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace company.inventory.data.Repositories
{
    public interface ILookupById<T>
    {
        Task<ILookup<int, T>> GetById(IEnumerable<int> ids);
    }
}
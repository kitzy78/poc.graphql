using System.Collections.Generic;
using System.Threading.Tasks;

namespace company.inventory.data.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Get(int id);
        Task<ICollection<T>> Get(T criteria);
        Task<T> Set(T value);
        Task<T> Create(T value);
    }
}
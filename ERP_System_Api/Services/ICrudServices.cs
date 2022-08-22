using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_System_Api
{
    public interface ICrudServices<T> where T : class
    {
        Task<T> Create(T t);

        Task<List<T>> Get();

        Task<T> GetById(long id);

        Task<T> Update(T t);

        Task<bool> Delete(long id);
    }
}

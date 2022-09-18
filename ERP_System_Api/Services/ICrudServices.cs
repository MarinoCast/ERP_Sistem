using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_System_Api
{
    public interface ICrudServices<T, R> where T : class
    {
        Task<T> Create(R request);

        Task<List<T>> Get();

        Task<T> GetById(int id);

        Task<T> Update(R request,int id);

        Task<bool> Delete(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_System_Api
{
    public interface ISearchingServices<T> where T : class
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> SeachByName(string name);
    }
}

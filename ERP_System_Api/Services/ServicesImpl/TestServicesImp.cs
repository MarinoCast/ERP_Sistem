using ERP_System_Api.Model;

namespace ERP_System_Api.Services.ServicesImpl
{
    public class TestServicesImp : ICrudServices<Test>
    {
        public Task<Test> Create(Test t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Test>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Test> Update(Test t)
        {
            throw new NotImplementedException();
        }
    }
}

using ERP_System_Api.DataBase;
using ERP_System_Api.Helpers.Middleware;
using ERP_System_Api.Model;

namespace ERP_System_Api.Services.ServicesImpl
{
    public class TestServicesImpl : ICrudServices<Test>
    {
        private readonly DataContext DbSvc;
        private readonly RuleEngine<Test> WEngine;

        public TestServicesImpl(DataContext dbSvc, RuleEngine<Test> ruleEngine)
        {
            DbSvc = dbSvc;
            WEngine = ruleEngine;
        }

        public async Task<Test> Create(Test request)
        {
           var rule = await WEngine.Validate(request);


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

        public Task<Test> Update(Test request)
        {
            throw new NotImplementedException();
        }
    }
}

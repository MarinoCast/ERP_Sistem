using ERP_System_Api.DataBase;
using ERP_System_Api.Helpers.Middleware;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using Microsoft.EntityFrameworkCore;

namespace ERP_System_Api.Services.ServicesImpl
{
    public class TestServicesImpl : ICrudServices<Test, TestRequest>
    {
        private readonly DataContext DbSvc;
        private readonly RuleEngine<Test> WEngine;
        private readonly Test test = new Test();
        public TestServicesImpl(DataContext dbSvc, RuleEngine<Test> ruleEngine)
        {
            DbSvc = dbSvc;
            WEngine = ruleEngine;
        }

        public async Task<Test> Create(TestRequest request)
        {
            test.name = request.name;
            DbSvc.Test.AddAsync(test);
            var create = await DbSvc.SaveChangesAsync();

            return test;
        }

     
        public Task<List<Test>> Get()
        {
            return DbSvc.Test.ToListAsync();
        }

        public async Task<Test> GetById(int id)
        {
            return await DbSvc.Test.SingleOrDefaultAsync(x => x.id == id);

        }

        public async Task<Test> Update(TestRequest request, int id)
        {
            test.name = request.name;
            test.id = id;
            
            DbSvc.Test.Update(test);
            var updated = await DbSvc.SaveChangesAsync();

            return test;
        }
        public async Task<bool> Delete(int id)
        {

            var value = await GetById(id);
            DbSvc.Test.Remove(value);
            var delete = await DbSvc.SaveChangesAsync();

            return delete > 0;
        }

    }
}

using ERP_System_Api.DataBase;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using Microsoft.EntityFrameworkCore;

namespace ERP_System_Api.Services.ServicesImpl
{
    public class ProcessingServiceImpl : ICrudServices<Processing, ProcessingsRequest>
    {
        private readonly DataContext DbSvc;
        // private readonly RuleEngine<Client> WEngine;
        private readonly Processing proces = new Processing();

        public ProcessingServiceImpl(DataContext Svc)
        {
            DbSvc = Svc;
          
        }

        public async Task<Processing> Create(ProcessingsRequest request)
        {
            proces.Name = request.Name;
            proces.Description = request.Description;
            proces.Price = request.Price;
           
            DbSvc.Processing.AddAsync(proces);
            var create = DbSvc.SaveChangesAsync();

            return proces;
        }

       

        public async Task<List<Processing>> Get()
        {
            return await DbSvc.Processing.ToListAsync();
        }

        public async Task<Processing> GetById(int id)
        {
            return await DbSvc.Processing.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Processing> Update(ProcessingsRequest request, int id)
        {

            proces.Name = request.Name;
            proces.Description = request.Description;
            proces.Price = request.Price;
            proces.Id = id;
            DbSvc.Processing.Update(proces);
            var update = await DbSvc.SaveChangesAsync();

            return proces;
        }
        public async Task<bool> Delete(int id)
        {
            var value = await GetById(id);
            DbSvc.Remove(value);
            var delete = await DbSvc.SaveChangesAsync();

            return delete > 0;
        }
    }
}

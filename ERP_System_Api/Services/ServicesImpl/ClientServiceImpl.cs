using ERP_System_Api.Payloads.Request;
using ERP_System_Api.Model;
using ERP_System_Api.DataBase;
using ERP_System_Api.Helpers.Middleware;
using ERP_System_Api.Payloads.Response;
using Microsoft.EntityFrameworkCore;

namespace ERP_System_Api.Services.ServicesImpl
{
    public class ClientServiceImpl : ICrudServices<Client, ClientRequest>
    {
        private readonly DataContext DbSvc;
       // private readonly RuleEngine<Client> WEngine;
        private readonly Client client = new Client();

        public ClientServiceImpl(DataContext Svc)
        {
            DbSvc = Svc;
            
        }
        public async Task<Client> Create(ClientRequest request)
        {
            client.name = request.name;
            client.lastName = request.lastName;
            client.address = request.address;
            client.phoneNumber = request.phoneNumber;
            client.age = request.age;
            client.personalId = request.personalId;
            client.sex = request.sex;

            DbSvc.Clients.AddAsync(client);
            var clientCreate = await DbSvc.SaveChangesAsync();
            return client;

        }

        public Task<List<Client>> Get()
        {
            return DbSvc.Clients.ToListAsync();
        }

        public async Task<Client> GetById(int id)
        {

            return await DbSvc.Clients.SingleOrDefaultAsync(x => x.id == id);


        }

        public async Task<Client> Update(ClientRequest request, int id)
        {
           
            client.name = request.name;
            client.lastName = request.lastName;
            client.address = request.address;
            client.phoneNumber = request.phoneNumber;
            client.sex = request.sex;
            client.age = request.age;
            client.personalId = request.personalId;
            client.id = id;

            DbSvc.Clients.Update(client);
            var updatedClient = await DbSvc.SaveChangesAsync();

            return client;

        }
        public async Task<bool> Delete(int id)
        {
            var clientId = await GetById(id);
            DbSvc.Clients.Remove(clientId);
            var delete = await DbSvc.SaveChangesAsync();

            return delete > 0;
        }
    }
}

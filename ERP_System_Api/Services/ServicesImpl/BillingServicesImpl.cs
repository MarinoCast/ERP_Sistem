using ERP_System_Api.DataBase;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;

namespace ERP_System_Api.Services.ServicesImpl
{

    public class BillingServicesImpl : ICrudServices<Billing, BillingRequest>
    {
        private readonly Billing billing = new Billing();
        private readonly DataContext DbSvc;
        private readonly Client client = new Client();
        private readonly ClientServiceImpl clientSvc;
        private readonly ProcessingServiceImpl processSvc;
        private readonly Processing processing = new Processing();

        public BillingServicesImpl(DataContext Svc)
        {
            DbSvc = Svc;

        }

        public async Task<Billing> Create(BillingRequest request)
        {



            var clientInfo = await clientSvc.GetById(request.clientInfo.id);
            var processingInfo = await processSvc.GetById(request.processingInfo.Id);


            billing.clientInfo = clientInfo;
            billing.processingInfo = processingInfo;

            billing.Nota = request.Nota;
            billing.nextAppointment = request.nextAppointment;

            DbSvc.Billings.AddAsync(billing);
            var billingCreate = await DbSvc.SaveChangesAsync();
            return billing;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Billing>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Billing> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Billing> Update(BillingRequest request, int id)
        {
            throw new NotImplementedException();
        }
    }
}

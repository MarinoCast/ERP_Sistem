using ERP_System_Api.Services.OAuthServ;

namespace ERP_System_Api
{
    public class IPruebaImp : ICreate
    {
        public Task<ErrorResult> create(string name)
        {
            var resp = new ErrorResult
            {
                Message = name
            };

            return Task.FromResult(resp);

        }

       
    }
}


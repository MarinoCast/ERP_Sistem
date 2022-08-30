using ERP_System_Api.Services.OAuthServ;


namespace ERP_System_Api
{
    public interface ICreate
    {
       Task<ErrorResult> create(string name);
    }
}

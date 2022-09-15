namespace ERP_System_Api.Helpers.Middleware
{
    public interface RuleEngine<C>  where C  : class
    {

        Task<C> Validate(C request);

    }
}

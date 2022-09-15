using ERP_System_Api.Model;

namespace ERP_System_Api.Helpers.Middleware
{
    public class WorkFlowEngine : RuleEngine<Test>
    {
        private readonly Test test = new Test();

        public Task<Test> Validate(Test request)
        {
            /*
            Switch -> valid
            case request --> registro
            {
                operaciones
            }
            case validation --> bool
            {
                operaciones
            }
            

            
             * 
             */

            throw new Exception();
           
       
        }

            
    }
}

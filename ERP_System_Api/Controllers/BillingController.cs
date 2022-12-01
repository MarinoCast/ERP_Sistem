using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System_Api.Controllers
{
    public class BillingController : BaseApiController
    {
        private readonly ICrudServices<Billing, BillingRequest> BillingSvc;

        public BillingController(ICrudServices<Billing, BillingRequest> Svc)
        {
            BillingSvc = Svc;
        }

        [HttpPost("/CreateBilling"), Authorize(Roles = "employee")]
        public async Task<IActionResult> Create([FromBody] BillingRequest request)
        {
            var response = await BillingSvc.Create(request);
            if (response == null)
            {
                throw new Exception();
            }
            return Ok(response);
        }
    }
}

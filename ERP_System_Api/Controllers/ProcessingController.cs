using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.DataBase;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System_Api.Controllers
{
    public class ProcessingController : BaseApiController
    {
        private readonly ICrudServices<Processing, ProcessingsRequest> ProSvc;

        public ProcessingController(ICrudServices<Processing, ProcessingsRequest> Svc)
        {
            ProSvc = Svc;
        }
        [HttpPost("/AddProcess"), Authorize(Roles = "employee")]
        public async Task<IActionResult> Create([FromBody] ProcessingsRequest request)
        {
            var response = await ProSvc.Create(request);
            if (response == null)
            {
                throw new Exception();
            }
            return Ok(response);
        }
        [HttpGet("/GetByProcess{id}"), Authorize(Roles = "employee")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await ProSvc.GetById(id);
            if (response == null)
            {
                throw new Exception();
            }
            return Ok(response);
        }

        [HttpGet("/GetAllProcess"), Authorize(Roles = "employee")]
        public async Task<IActionResult> GetAll()
        {
            var response = await ProSvc.Get();
            if (response == null)
            {
                throw new Exception();
            }
            return Ok(response);
        }

        [HttpPut("/UpdatedProcess"), Authorize(Roles = "employee")]
        public async Task<IActionResult> Updated(int id, [FromBody] ProcessingsRequest request)
        {
            var reponse = await ProSvc.Update(request, id);
            if (reponse == null)
            {
                throw new Exception();
            }
            return Ok(reponse);
        }
        [HttpDelete("/DeleteByProcess{id}"), Authorize(Roles = "employee")]
        public async Task<IActionResult>Delete(int id)
        {
            var response = await ProSvc.Delete(id);
            if (response == null)
            {
                throw new Exception();
            }
            return Ok(response);
        }
    }
}

using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System_Api.Controllers
{
    public class ClientController : BaseApiController
    {

        private readonly ICrudServices<Client, ClientRequest> ClientSvc;

        public ClientController(ICrudServices<Client, ClientRequest> Svc)
        {
            ClientSvc = Svc;
        }

        [HttpPost("/AddClient"), Authorize(Roles = "employee")]
        public async Task<IActionResult> Create([FromBody] ClientRequest request)
        {
            var response = await ClientSvc.Create(request);
            if (response == null)
            {
                throw new Exception();
            }
            return Ok(response);
        }
        [HttpGet("/GetByClient{id}"), Authorize(Roles = "employee")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await ClientSvc.GetById(id);
            if(response == null)
            {
                throw new Exception();
            }
            return Ok(response);
        }

        [HttpGet("/GetAllClient"), Authorize(Roles = "employee")]
        public async Task<IActionResult> GetAll()
        {
            var response = await ClientSvc.Get();
            if (response == null)
            {
                throw new Exception();
            }
            return Ok(response);
        }
      
        [HttpPut("/UpdatedClient"), Authorize(Roles = "employee")]
        public async Task<IActionResult> Updated(int id, [FromBody] ClientRequest request)
        {
            var reponse = await ClientSvc.Update(request, id);
            if (reponse == null)
            {
                throw new Exception();
            }
            return Ok(reponse);
        }
        [HttpDelete("/DeleteClient{id}"), Authorize(Roles = "employee")]
        public async Task<IActionResult> Delete( int id)
        {
            var response = await ClientSvc.Delete(id);
            if (response == null)
            {
                throw new Exception();
            }
            return Ok(response);
        }
    }
}

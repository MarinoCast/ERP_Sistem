using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System_Api.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly ICrudServices<Test, TestRequest> TestSvc;

        public TestController(ICrudServices<Test, TestRequest> Svc)
        {
            TestSvc = Svc;
        }


        [HttpPost("/Test")]
        public async Task<IActionResult> Create([FromBody] TestRequest request)
        {
            var reponse = await TestSvc.Create(request);
            if(reponse == null)
            {
                throw new Exception();
            }
            return Ok(reponse);

        }
        [HttpGet("/Get{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var reponse = await TestSvc.GetById(id);
            if (reponse == null)
            {
                throw new Exception();
            }
            return Ok(reponse);

        }

        [HttpGet("/GetAll")]
        public async Task<IActionResult> GetAll([FromRoute] int id)
        {
            var reponse = await TestSvc.Get();
            if (reponse == null)
            {
                throw new Exception();
            }
            return Ok(reponse);

        }

        [HttpPut("/Update{id}")]
        public async Task<IActionResult> Updated([FromRoute] int id,[FromBody] TestRequest request)
        {
            var reponse = await TestSvc.Update(request, id);
            if (reponse == null)
            {
                throw new Exception();
            }
            return Ok(reponse);

        }
        [HttpDelete("/Delete{id}")]
        public async Task<IActionResult> D([FromRoute] int id)
        {
            var reponse = await TestSvc.Delete(id);
            if (!reponse)
            {
                throw new Exception();
            }
            return Ok(reponse);

        }

    }
}

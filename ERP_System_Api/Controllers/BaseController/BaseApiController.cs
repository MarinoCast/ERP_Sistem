using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System_Api.Controllers.BaseController
{
    [ApiController]
    [Route("[api/controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class BaseApiController : ControllerBase
    {
       
    }
}

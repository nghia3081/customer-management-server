using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace CusomterManager.Controllers.Base
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ODataRouteComponent("odata")]
    public class BaseController : ODataController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetUser()
        {
            var a = User;
            return string.Empty;
        }
    }
}

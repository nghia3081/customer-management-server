using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CusomterManager.Controllers.Base
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ExceptionController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            var error = exceptionHandlerFeature.Error;
            if (error is CustomerManagementException theaterException)
            {
                if (theaterException.ExceptionCode == 4010) return Unauthorized();
                return StatusCode(500, theaterException.Message);
            }
            return StatusCode(500, "An error has occured");
        }
    }
}

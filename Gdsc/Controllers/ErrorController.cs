using Gdsc.Controllers;
using Gdsc.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gdsc.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseController
    {
        public ActionResult Error(int code)
        {
            return new ObjectResult(new ApiErroeResponse(code));
        }
    }
}

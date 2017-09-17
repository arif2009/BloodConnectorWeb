using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BloodConnector.WebAPI.Filters
{
    //https://stackoverflow.com/a/21654903/3835843
    public class ValidateModelStateAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse((HttpStatusCode)422, actionContext.ModelState);
            }
        }
    }
}
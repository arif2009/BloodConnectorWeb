using System.Threading.Tasks;
using System.Web.Http;
using BloodConnector.WebAPI.Services;

namespace BloodConnector.WebAPI.Controllers.Api
{
    [RoutePrefix("api/developers")]
    public class DevelopersController : ApiController
    {
        private readonly UserServices _userServices;

        public DevelopersController(UserServices userServices)
        {
            _userServices = userServices;
        }

        public IHttpActionResult Get()
        {
            return Ok(_userServices.GetDevelopers());
        }
    }
}
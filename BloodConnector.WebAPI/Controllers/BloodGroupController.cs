using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using BloodConnector.WebAPI.Interface;
using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.Repository;

namespace BloodConnector.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/BloodGroup")]
    //[EnableCors("http://localhost:14717,http://localhost:2102", "*", "*")]
    public class BloodGroupController: ApiController
    {
        private readonly IBloodGroupRepository _bloodGroupRepository;
        public BloodGroupController(IBloodGroupRepository bloodGroupRepository)
        {
            //_bloodGroupRepository = new BaseRepository<BloodGroup>();
            _bloodGroupRepository = bloodGroupRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            var sdf = _bloodGroupRepository.FindAll();
            return Ok(sdf);
        } 
    }
}
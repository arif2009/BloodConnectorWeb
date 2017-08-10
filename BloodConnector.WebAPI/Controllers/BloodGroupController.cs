using System.Web.Http;
using BloodConnector.WebAPI.Interface;

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
            return Ok(_bloodGroupRepository.FindAll());
        } 
    }
}
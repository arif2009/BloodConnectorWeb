using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using BloodConnector.WebAPI.Interface;
using Microsoft.AspNet.Identity.Owin;

namespace BloodConnector.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/BloodGroup")]
    //[EnableCors("http://localhost:14717,http://localhost:2102", "*", "*")]
    public class BloodGroupController: ApiController
    {
        private readonly IBloodGroupRepository _bloodGroupRepository;
        private ApplicationUserManager _userManager;
        public BloodGroupController(IBloodGroupRepository bloodGroupRepository)
        {
            //_bloodGroupRepository = new BaseRepository<BloodGroup>();
            _bloodGroupRepository = bloodGroupRepository;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            return Ok(_bloodGroupRepository.FindAll());
        }

        [AllowAnonymous]
        [Route("TestSmtpMail")]
        [HttpGet]
        public async Task<IHttpActionResult> TestSmtpMail()
        {
            var subject = "Your subject";
            var body = "Your email body it can be html also";
            var user = await UserManager.FindByEmailAsync("arif.rahman2009@gmail.com");
            await UserManager.SendEmailAsync(user.Id, subject, body);
            return Ok();
        }
    }
}
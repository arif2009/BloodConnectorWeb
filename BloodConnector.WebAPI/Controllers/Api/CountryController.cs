using System.Web.Http;
using BloodConnector.WebAPI.Interface;

namespace BloodConnector.WebAPI.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/country")]
    public class CountryController : ApiController
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        // GET api/country
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_countryRepository.FindAll());
        }

        // GET api/country/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/country
        public void Post([FromBody]string value)
        {
        }

        // PUT api/country/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/country/5
        public void Delete(int id)
        {
        }
    }
}
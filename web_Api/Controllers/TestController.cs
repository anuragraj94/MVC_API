using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace web_Api.Controllers
{
    public class TestController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Test/forall")]
        public IHttpActionResult Get()
        {
            return Ok("Curren DateTime : " + DateTime.Now.ToString());
        }


        [Authorize]
        [HttpGet]
        [Route("api/Test/Name")]
        public IHttpActionResult Name()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello "+identity.Name);
        }


        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("api/Test/auth")]
        public IHttpActionResult GetAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                                .Where(c => c.Type == ClaimTypes.Role)
                                .Select(c => c.Value);
            return Ok("Hello " + identity.Name +" Role: "+ string.Join(",", roles.ToList()));
        }

    }
}

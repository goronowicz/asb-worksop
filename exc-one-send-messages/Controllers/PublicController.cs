using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace exc_one_send_messages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Route("ping")]
        public ActionResult<string> Get()
        {
            return "OK";
        }
    }
}

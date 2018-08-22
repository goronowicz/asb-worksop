using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exc_one_send_messages.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exc_one_send_messages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public async Task<ActionResult> Post([FromBody]Message message)
        {

            return Ok();
        }
    }
}
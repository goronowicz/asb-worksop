using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace exc_one_send_messages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IQueueClient queueClient;
        private readonly ITopicClient topicClient;

        public MessageController(IQueueClient queueClient, ITopicClient topicClient)
        {
            this.queueClient = queueClient;
            this.topicClient = topicClient;
        }
        public async Task<ActionResult> Post([FromBody]Commons.Message message)
        {
            var messageEnvelop = new Microsoft.Azure.ServiceBus.Message
            {
                MessageId = message.ID.ToString(),
                ContentType = "application/json",
                Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))
            };

            await queueClient.SendAsync(messageEnvelop);
            return Ok();
        }

        [Route("broadcast")]
        public async Task<ActionResult> Broadcast([FromBody]Commons.Message message)
        {
            var messageEnvelop = new Message
            {
                MessageId = message.ID.ToString(),
                ContentType = "application/json",
                Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)),
            };

            messageEnvelop.UserProperties["domain"] = new MailAddress(message.Receipient).Host;

            await topicClient.SendAsync(messageEnvelop);
            return Ok();
        }
    }
}
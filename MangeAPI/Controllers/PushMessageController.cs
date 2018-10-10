using isRock.LineBot;
using MangeAPI.Repository.Repo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MangeAPI.Controllers
{
    [EnableCors(origins: "http://example.com", headers: "*", methods: "*")]
    [Route("api/QueueNumber")]
    public class PushMessageController : ApiController
    {
        private readonly Repo_CheckIn _repo;
        string channelAccessToken = ConfigurationManager.AppSettings["ChannelAccessToken"];
        string AdminUserId = ConfigurationManager.AppSettings["AdminUserID"];
   
        public PushMessageController()
        {
            _repo = new Repo_CheckIn();
        }

        // GET api/<controller>
        public string Get()
        {
            return "value6";
        }

        public class getQueue
        {
            public string shop_id { get; set; }
            public string counter_id { get; set; }
        }

        [Route("api/PushMessage/QueueNotify")]
        [HttpPost]
        public void QueueNotify([FromBody]Utility.flexInf queueObj)
        {
            Bot bot = new Bot(channelAccessToken);
            bot.PushMessageWithJSON(queueObj.user_id, Utility.createFlexMessage(queueObj));
        }

       

        //public class AddInQueue
        //{
        //    public string user_id { get; set; }
        //    public string shop_id { get; set; }
        //    public string counter_id { get; set; }
        //}
        //[Route("api/QueueNumber/AddCustomerInQueue")]
        //[HttpPost]
        //public async Task<int> AddCustomerInQueue([FromBody]AddInQueue addInQueue)
        //{
        //    var task = await _repo.SetReservation(addInQueue.user_id, addInQueue.shop_id, addInQueue.counter_id);
        //    return task;
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
using MangeAPI.Repository.Repo;
using MangeAPI.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using isRock.LineBot;
using System.Configuration;

namespace MangeAPI.Controllers
{
    public class QueueManageController : ApiController
    {

        string channelAccessToken = ConfigurationManager.AppSettings["ChannelAccessToken"];
        private readonly Repo_QueueManage _repo;

        protected QueueManageController()
        {
            _repo = new Repo_QueueManage();
        }

        [Route("api/QueueManage/getQueueList")]
        [HttpPost]
        public async Task<IEnumerable<getQueueList>> getQueueList([FromBody]QueueList queueList)
        {
            var list = await _repo.GetQueueList(queueList);
           
            //var list = new List<getQueueList>();
            //list.Add(new getQueueList()
            //{
            //    user_id = "userid1",
            //    queue_number = "01",
            //    display_name = "wayne"
            //});
            //list.Add(new getQueueList()
            //{
            //    user_id = "userid2",
            //    queue_number = "02",
            //    display_name = "wayne2"
            //});
            //list.Add(new getQueueList()
            //{
            //    user_id = "userid3",
            //    queue_number = "03",
            //    display_name = "wayne3"
            //});

            return list;
        }

        [Route("api/QueueManage/UpdateQueueList")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateQueueList([FromBody]QueueListItem queueListItem)
        {
            var result = await _repo.UpdateQueueList(queueListItem);

            Bot bot = new Bot(channelAccessToken);
            bot.PushMessageWithJSON(queueListItem.user_id, Utility.CompleteFlexMessage(queueListItem));

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/QueueManage/NotifyComplete")]
        [HttpPost]
        public void NotifyComplete([FromBody]QueueListItem queueListItem)
        {
            Bot bot = new Bot(channelAccessToken);
            bot.PushMessageWithJSON(queueListItem.user_id, Utility.CompleteFlexMessage(queueListItem));
        }

        [Route("api/QueueManage/ReceiveNotify")]
        [HttpPost]
        public void ReceiveNotify([FromBody]QueueListItem queueListItem)
        {
            Bot bot = new Bot(channelAccessToken);
            bot.PushMessageWithJSON(queueListItem.user_id, Utility.ReceiveNotifyFlexMessage(queueListItem));
        }
    }
}
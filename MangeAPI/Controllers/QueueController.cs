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
    public class QueueController : ApiController
    {
        string channelAccessToken = ConfigurationManager.AppSettings["ChannelAccessToken"];
        private readonly Repo_Queue _repo;

        protected QueueController()
        {
            _repo = new Repo_Queue();
        }

        //管理介面拿到前二十店裡排隊名清單
        [Route("api/Queue/getQueueList")]
        [HttpPost]
        public async Task<IEnumerable<getQueueList>> getQueueList([FromBody]QueueList queueList)
        {
            var list = await _repo.GetQueueList(queueList);

            return list;
        }

        // 任務完成發送訊息給客戶
        [Route("api/Queue/UpdateQueueList")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateQueueList([FromBody]TaskComplete queueListItem)
        {
            var result = await _repo.UpdateQueueList(queueListItem);

            Bot bot = new Bot(channelAccessToken);
            bot.PushMessageWithJSON(queueListItem.user_id, Utility.CompleteFlexMessageTwo(queueListItem));

            return StatusCode(HttpStatusCode.NoContent);
        }

        //取得店裡
        [Route("api/Queue/GetUserQueueNumber")]
        [HttpPost]
        public async Task<getQueueOutput> GetUserQueueNumber([FromBody]getQueueInput Input)
        {
            var result = await _repo.GetUserQueueNumber( Input.user_id);

            return result;
        }

        [Route("api/Queue/AddCustomerInQueue")]
        [HttpPost]
        public async Task<int> AddCustomerInQueue([FromBody]AddInQueueInput addInQueue)
        {
            var task = await _repo.SetReservation(addInQueue.user_id, addInQueue.shop_id);
            return task;
        }


        // 發送完成通知
        [Route("api/Queue/NotifyComplete")]
        [HttpPost]
        public void NotifyComplete([FromBody]QueueListItem queueListItem)
        {
            Bot bot = new Bot(channelAccessToken);
            bot.PushMessageWithJSON(queueListItem.user_id, Utility.CompleteFlexMessage(queueListItem));
        }

        // 客戶抽完號後發送通知
        [Route("api/Queue/ReceiveNotify")]
        [HttpPost]
        public void ReceiveNotify([FromBody]QueueListItem queueListItem)
        {
            Bot bot = new Bot(channelAccessToken);
            bot.PushMessageWithJSON(queueListItem.user_id, Utility.ReceiveNotifyFlexMessage(queueListItem));
        }
    }
}
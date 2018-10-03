using MangeAPI.Repository.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MangeAPI.Controllers
{
    public class QueueNumberController : ApiController
    {

        private readonly Repo_CheckIn _repo;

        public QueueNumberController()
        {
            _repo = new Repo_CheckIn();
        }

        // GET api/<controller>
        public string Get()
        {
            return "value6";
        }
        
        [Route("api/QueueNumber/GetQueueNumber")]
        [HttpPost]
        public async Task<int> GetQueueNumber([FromBody]string _shop_id, string _counter_id)
        {
            var result = await _repo.GetQueueNumber(_shop_id, _counter_id);

            return result;
        }

        [Route("api/QueueNumber/AddCustomerInQueue")]
        [HttpPost]
        public async Task<int> AddCustomerInQueue(string _user_id,string _shop_id,string _counter_id)
        {
            var task =  await _repo.SetReservation(_user_id,  _shop_id, _counter_id);
            return task;
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
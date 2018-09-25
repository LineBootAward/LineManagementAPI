using MangeAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MangeAPI.Repository.Repo;
using System.Threading.Tasks;

namespace MangeAPI.Controllers
{
    public class LiffTestController : ApiController
    {
        private readonly Repo_LiffTest _repo;

        public LiffTestController()
        {
            _repo = new Repo_LiffTest();
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public async Task<int> Post([FromBody]LiffTest liffId)
        {
            var result = await _repo.InsertUserid(liffId.user_id.ToString());

            return result;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
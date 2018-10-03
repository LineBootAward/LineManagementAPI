using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangeAPI.Repository.IRepo
{
    interface ICheckIn
    {
        Task<int> GetQueueNumber(string _shop_id, string _counter_id);
        Task<int> AddCustomerInQueue(int now_queue_number, string _user_id, string _shop_id, string _counter_id);
    }
}

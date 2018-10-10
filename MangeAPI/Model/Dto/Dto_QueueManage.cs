using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangeAPI.Model.Dto
{
    public class QueueList
    {
        public string shop_id { get; set; }
        public string counter_id { get; set; }
    }
    public class getQueueList
    {
        public int idx { get; set; }
        public string user_id { get; set; }
        public string queue_number { get; set; }
        public string display_name { get; set; }
    }
    public class QueueListItem
    {
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string shop_name{ get; set; }
        public string queue_number { get; set; }
        public string display_name { get; set; }
        public int idx { get; set; }
    }
}
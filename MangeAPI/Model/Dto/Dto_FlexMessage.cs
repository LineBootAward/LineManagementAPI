using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangeAPI.Model.Dto
{
    public class companyInf
    {
        public string name;
        public int number;
        public string wait_time;
        public string foot_print;
        public string img_url;
        public string id;

        public companyInf(string store_id, string store_name, string store_img_url, string line_wait_time, int line_number, string foot_print_text)
        {
            name = store_name;
            img_url = store_img_url;
            wait_time = line_wait_time;
            number = line_number;
            foot_print = foot_print_text;
            id = store_id;
        }

    }
}
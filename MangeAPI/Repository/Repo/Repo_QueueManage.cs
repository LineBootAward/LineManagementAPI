using Dapper;
using MangeAPI.Repository.DbConnFactory;
using MangeAPI.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MangeAPI.Repository.Repo
{
    public class Repo_QueueManage : DbConnConfig
    {

        public async Task<List<getQueueList>> GetQueueList(QueueList queue_list)
        {
            using (var conn = ConnFactory.Create(_DbType, _azConnStr))
            {
                conn.Open();

                string sqlCmd = @"SELECT TOP 20
                                    user_id, queue_number, display_name,idx
                                  FROM 
                                    QueueList 
                                  WHERE 
                                        shop_id = @shop_id 
                                    AND 
                                        counter_id = @counter_id
                                    AND 
                                        status = @status
                                  Order By 
                                    queue_number ASC";

                var result = await conn.QueryAsync<getQueueList>(sqlCmd, new {
                    shop_id = queue_list.shop_id,
                    counter_id = queue_list.counter_id,
                    status = QueueTask_Status.Init.ToString()
                });
                var res = result.ToList();
                return res;
            }
        }

        public async Task<int> UpdateQueueList(QueueListItem queue_list_item)
        {
            using (var conn = ConnFactory.Create(_DbType, _azConnStr))
            {
                conn.Open();
                string sqlCmd = @"UPDATE 
                                    QueueList
                                SET 
                                    status = @status
                                WHERE 
                                    shop_id = @shop_id 
                                AND 
                                    user_id = @user_id
                                AND 
                                    idx = @idx
                                ";

                var result = await conn.ExecuteAsync(sqlCmd, new
                {
                    idx = queue_list_item.idx,
                    shop_id = queue_list_item.shop_id,
                    user_id = queue_list_item.user_id,
                    status = QueueTask_Status.Complete.ToString()
                });
       
                return result;
            }
        }
    }
}
using Dapper;
using MangeAPI.Repository.DbConnFactory;
using MangeAPI.Repository.IRepo;
using MangeAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Transactions;

namespace MangeAPI.Repository.Repo
{
    public class Repo_CheckIn : DbConnConfig
    {
        
        public async Task<int> GetQueueNumber(string _shop_id ,string _counter_id)
        {
            using(var conn = ConnFactory.Create(_DbType, _azConnStr))
            {
                conn.Open();

                string sqlCmd = @"SELECT 
                                    now_queue_number 
                                  FROM 
                                    shop_queue_status 
                                  Where 
                                    shop_id = @shop_id AND counter_id = @counter_id";
                
                var result = await conn.QuerySingleAsync<GetQueue>(sqlCmd, new { shop_id = _shop_id , counter_id = _counter_id });
                int res = result.now_queue_number;
                return res;
            }
        }

        public async Task<int> SetReservation(
            string _user_id, string _shop_id, string _counter_id
        )
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var cmd1 = new
                {
                    user_id = _user_id,
                    shop_id = _shop_id,
                    status = QueueTask_Status.Init.ToString(),
                    counter_id = _counter_id
                };

                var cmd2 = new
                {
                    shop_id = _shop_id,
                    counter_id = _counter_id
                };
                
                string sqlCommand1 = @"INSERT INTO queueList
                                            (user_id,shop_id,status,counter_id) 
                                        VALUES
                                            (@user_id,@shop_id,@status,@counter_id);";

                string sqlCommand2 = @"UPDATE 
                                           shop_queue_status
                                       SET 
                                           now_queue_number = now_queue_number + 1
                                       WHERE 
                                           shop_id = @shop_id 
                                        AND 
                                           counter_id = @counter_id
                                        ";

                string queueNum = @"SELECT 
                                        now_queue_number 
                                      FROM 
                                        shop_queue_status 
                                      Where 
                                            shop_id = @shop_id 
                                        AND 
                                            counter_id = @counter_id";

                string setQueueNum = @"UPDATE 
                                           queueList
                                       SET 
                                           queue_number  =  @queue_number
                                       WHERE 
                                           shop_id = @shop_id 
                                        AND 
                                           user_id = @user_id
                                        ";
                int cmdResult;
                using (var conn = ConnFactory.Create(_DbType, _azConnStr))
                {
                    conn.Open();

                    cmdResult = await conn.ExecuteAsync(sqlCommand1, cmd1);
                    var updateResult = await conn.ExecuteAsync(sqlCommand2, cmd2);

                    if(updateResult == 1)
                    {
                        var result = await conn.QuerySingleAsync<GetQueue>(queueNum , new {
                            shop_id = _shop_id,
                            counter_id = _counter_id
                        });

                        await conn.ExecuteAsync(setQueueNum, new {
                            queue_number = result.now_queue_number,
                            shop_id = _shop_id,
                            user_id = _user_id
                        });

                        transactionScope.Complete();
                        return result.now_queue_number;
                    }

                    transactionScope.Complete();
                    return cmdResult;
                }
                
            }
           
        }
    }
}
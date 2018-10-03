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
            using(var conn = ConnFactory.Create(_DbType, _ConnStr))
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
                var Res = new
                {
                    shop_id = _shop_id,
                    counter_id = _counter_id
                };

                var Res_Ev = new
                {
                    user_id = _user_id,
                    shop_id = _shop_id,
                    status = QueueTask_Status.Init.ToString()
                };
                
                string sqlCommand1 = @"INSERT INTO 
                                        queueList
                                            (user_id,shop_id,status) 
                                        VALUES(@user_id,@shop_id,@status);";

                string sqlCommand2 = @"UPDATE shop_queue_status
                                            SET now_queue_number = now_queue_number + 1
                                       WHERE 
                                           shop_id = @shop_id 
                                       AND 
                                           counter_id = @counter_id
                                        ";

                string queryNumber = @"SELECT 
                                        now_queue_number 
                                      FROM 
                                        shop_queue_status 
                                      Where shop_id = @shop_id AND counter_id = @counter_id";
                int cmdResult;
                using (var conn = ConnFactory.Create(_DbType, _ConnStr))
                {
                    conn.Open();

                    cmdResult = await conn.ExecuteAsync(sqlCommand1,Res_Ev);
                    var updateResult = await conn.ExecuteAsync(sqlCommand2, Res);

                    if(updateResult == 1)
                    {
                        var result = await conn.QuerySingleAsync<GetQueue>(queryNumber,new {
                            shop_id = _shop_id,
                            counter_id = _counter_id
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
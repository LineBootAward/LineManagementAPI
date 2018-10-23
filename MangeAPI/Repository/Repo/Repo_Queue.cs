using Dapper;
using MangeAPI.Repository.DbConnFactory;
using MangeAPI.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MangeAPI.Model;
using System.Transactions;

namespace MangeAPI.Repository.Repo
{
    public class Repo_Queue : DbConnConfig
    {
        public async Task<List<getQueueList>> GetQueueList(QueueList queue_list)
        {
            using (var conn = ConnFactory.Create(_DbType, _ConnStr))
            {
                conn.Open();

                string sqlCmd = @"SELECT TOP 20
                                    user_id, queue_number, display_name,idx
                                  FROM 
                                    QueueList 
                                  WHERE 
                                        shop_id = @shop_id 
                                    AND 
                                        status = @status
                                  Order By 
                                    queue_number ASC";

                var result = await conn.QueryAsync<getQueueList>(sqlCmd, new
                {
                    shop_id = queue_list.shop_id,
                    counter_id = queue_list.counter_id,
                    status = QueueTask_Status.Init.ToString()
                });
                var res = result.ToList();
                return res;
            }
        }

        public async Task<int> UpdateQueueList(TaskComplete queue_list_item)
        {
            using (var conn = ConnFactory.Create(_DbType, _ConnStr))
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

                string updateCurrentNumber = @"UPDATE 
                                                  shop_queue_status
                                            SET 
                                                current_number = @current_number
                                            WHERE 
                                                shop_id = @shop_id 
                                            ";

                var result = await conn.ExecuteAsync(sqlCmd, new
                {
                    idx = queue_list_item.idx,
                    shop_id = queue_list_item.shop_id,
                    user_id = queue_list_item.user_id,
                    status = QueueTask_Status.Complete.ToString()
                });

                var result2 = await conn.ExecuteAsync(updateCurrentNumber, new
                {
                    shop_id = queue_list_item.shop_id,
                    current_number = queue_list_item.queue_number
                });

                return result;
            }
        }

        public async Task<bool> LiffUserExists(string _liff_user_id)
        {
            using (var conn = ConnFactory.Create(_DbType, _ConnStr))
            {
                conn.Open();

                string sqlCmd = "SELECT username FROM account Where user_id = @user_id";

                var result = await conn.QueryAsync<User>(sqlCmd, new { user_id = _liff_user_id });

                if (result.Any())
                    return true;
            }

            return false;
        }
        
        //public async Task<getQueueOutput> GetQueueNumber(string _shop_id)
        //{
        //    using (var conn = ConnFactory.Create(_DbType, _ConnStr))
        //    {
        //        conn.Open();

        //        string sqlCmd = @"SELECT 
        //                            now_queue_number, current_number 
        //                          FROM 
        //                            shop_queue_status 
        //                          Where 
        //                            shop_id = @shop_id";

        //        var result = await conn.QuerySingleAsync<getQueueOutput>(sqlCmd, new { shop_id = _shop_id });
        //        return result;
        //    }
        //}
        
        public async Task<getQueueOutput> GetUserQueueNumber(string _user_id)
        {
            using (var conn = ConnFactory.Create(_DbType, _ConnStr))
            {
                conn.Open();

                string sqlcmd = @"SELECT 
                                        a.current_number , b.queue_number
                                    FROM
                                        shop_queue_status a
                                    INNER JOIN 
                                            QueueList b
                                        ON 
                                            a.shop_id = b.shop_id
                                    WHERE
                                            b.user_id = @user_id";

                var result = await conn.QuerySingleAsync<getQueueOutput>(sqlcmd, new
                {
                    user_id = _user_id
                });
                return result;
            }
        }
        public async Task<int> SetReservation(
            string _user_id, string _shop_id
        )
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                string queueNum = @"SELECT 
                                        now_queue_number 
                                      FROM 
                                        shop_queue_status 
                                      Where 
                                            shop_id = @shop_id ";

                string newQueueList = @"INSERT INTO queueList
                                            (user_id,shop_id,queue_number,status) 
                                        VALUES
                                            (@user_id,@shop_id,@queue_number,@status);";

                string updateQueue_Number = @"UPDATE 
                                           shop_queue_status
                                       SET 
                                           now_queue_number = now_queue_number + 1
                                       WHERE 
                                           shop_id = @shop_id 
                                        ";

                int InsertResult;
                using (var conn = ConnFactory.Create(_DbType, _ConnStr))
                {
                    conn.Open();

                    var result = await conn.QuerySingleOrDefaultAsync<GetQueue>(queueNum, new
                    {
                        shop_id = _shop_id
                    });

                    if (result.now_queue_number != 0)
                    {
                       
                        InsertResult = await conn.ExecuteAsync(newQueueList, new
                        {
                            user_id = _user_id,
                            shop_id = _shop_id,
                            status = QueueTask_Status.Init.ToString(),
                            queue_number = result.now_queue_number + 1
                        });

                        var updateResult = await conn.ExecuteAsync(updateQueue_Number, new
                        {
                            shop_id = _shop_id
                        });

                        transactionScope.Complete();
                        return InsertResult;
                    }
                    else
                    {
                        InsertResult = 0;
                    }
                    transactionScope.Complete();
                    return InsertResult;
                }

            }

        }
    }
}

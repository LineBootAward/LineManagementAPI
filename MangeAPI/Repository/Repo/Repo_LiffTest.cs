using Dapper;
using MangeAPI.Model.Auth;
using MangeAPI.Repository.DbConnFactory;
using System.Linq;
using System.Threading.Tasks;

namespace MangeAPI.Repository.Repo
{
    public class Repo_LiffTest : DbConnConfig
    {
        public async Task<int> InsertUserid(string _user_id)
        {
            using (var conn = ConnFactory.Create(_DbType,_azConnStr))
            {
                conn.Open();

                string sqlCmd = @"INSERT INTO 
                                    _LiffTest
                                        (user_id,test) 
                                    VALUES(@user_id,@test);";
                
                var result = await conn.ExecuteAsync(sqlCmd, new { user_id = _user_id , test = "test" });
               
                return result;
            }
        }
    }
}
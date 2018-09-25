using Dapper;
using MangeAPI.Model.Auth;
using MangeAPI.Repository.DbConnFactory;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangeAPI.Repository.Repo
{
    public class Repo_Auth : DbConnConfig
    {
        public async Task<User> Login(string _username, string password)
        {
            using (var conn = ConnFactory.Create( _DbType, _ConnStr))
            {
                conn.Open();

                string sqlCmd = @"SELECT 
                                    user_id,username,password_hash,password_salt 
                                  FROM 
                                    account 
                                  Where username = @username";

                var result = await conn.QueryAsync<User>(sqlCmd, new { username = _username });
                var user = result.FirstOrDefault();

                if (user == null)
                    return null;
                if (!VerifyPasswordHash(password, user.password_hash, user.password_salt))
                    return null;

                return user;
            }
        }

        public async Task<int> Register(User user, string password)
        {
            byte[] password_hash, password_salt;

            CreatePasswordHash(password, out password_hash, out password_salt);

            user.password_hash = password_hash.ToString();
            user.password_salt = password_salt.ToString();
            user.user_id = Guid.NewGuid().ToString();
            using (var conn = ConnFactory.Create(_DbType, _ConnStr))
            {
                conn.Open();
                string sqlCmd = @"INSERT INTO 
                                    account
                                        (user_id,username,password_hash,password_salt) 
                                    VALUES
                                        (@user_id,@username,@password_hash,@password_salt);";

                var result = await conn.ExecuteAsync(sqlCmd, user);

                return result;
            }
        }

        public async Task<bool> UserExists(string _username)
        {
            using (var conn = ConnFactory.Create( _DbType, _ConnStr))
            {
                conn.Open();

                string sqlCmd = "SELECT username FROM account Where username = @username";

                var result = await conn.QueryAsync<User>(sqlCmd, new { username = _username });

                if (result.Any())
                    return true;
            }

            return false;
        }

        private bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            byte[] salt = Encoding.UTF8.GetBytes(passwordSalt);
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computehash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computehash.Length; i++)
                {
                    if (computehash[i] != passwordHash[i]) return false;
                }

                return true;
            }
        }

        private void CreatePasswordHash(string password, out byte[] password_hash, out byte[] password_salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                password_salt = hmac.Key;
                password_hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
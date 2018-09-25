using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MangeAPI.Repository.DbConnFactory
{
    public enum DbType
    {
        MySql,
        MsSql
    }

    public class DbConnConfig
    {
        public DbType _DbType = DbType.MsSql;
        public string _ConnStr = WebConfigurationManager.ConnectionStrings["connStr"].ToString();
        public string _azConnStr = WebConfigurationManager.ConnectionStrings["azConnStr"].ToString();
    }
    
    public class ConnFactory
    {
        public static IDbConnection Create(DbType database, string connection)
        {
            IDbConnection conn = null;
            switch (database)
            {
                case DbType.MsSql:
                    conn = new SqlConnection(connection);
                    break;
                case DbType.MySql:
                    conn = new MySqlConnection(connection);
                    break;
                default:
                    conn = new SqlConnection(connection);
                    break;
            }
            return conn;
        }
    }
}
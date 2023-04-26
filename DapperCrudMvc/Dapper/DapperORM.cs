using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DapperCrudMvc.Dapper
{
    public static class DapperORM
    {

        //dapper da önce , db bağlantısı, sql connect, sql query , sql prodecure
        private static string connectionString = @"Data Source=DESKTOP-T21U4SI\SQLEXPRESS;Initial Catalog=DapperDb;Integrated Security=True;";

        public static void ExecuteWithoutReturn(string procedureName, DynamicParameters param=null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                sqlcon.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public static T ExecuteReturnSalary<T>(string procedureName, DynamicParameters param=null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                return (T)Convert.ChangeType(sqlcon.ExecuteScalar(procedureName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }

        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param=null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                return sqlcon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Models;
using Dapper;
using MiniProfiler.Integrations;

namespace Season02Query
{
    internal class QueryFirst
    {
        static string sql = "select * from Students where id=@id";
        static string sql2 = "select * from Students";

        public static void QueryFirstAnonymous()
        {
            using (var connection = DapperHelper.DbConnection())
            {
                var res = connection.QueryFirst(sql, new { Id = 3 });

                Console.WriteLine(res.FirstName);
                Console.WriteLine(res.LastName);
            }
        }

        public static void QueryFirstStronglyTyped()
        {
            using (var connection = DapperHelper.DbConnection())
            {
                var res = connection.QueryFirst<Student>(sql2);
                Console.WriteLine(res.ToString());
            }
        }

        public static void QueryFirstOrDefaultAnonymous()
        {
            using (var connection = DapperHelper.DbConnection())
            {
                var res = connection.QueryFirstOrDefault(sql, new { Id = 1 });

                Console.WriteLine(res?.FirstName);
                Console.WriteLine(res?.LastName);
            }
        }

        public static void QueryFirstOrDefaultStronglyTyped()
        {
            using (var connection = DapperHelper.DbConnection())
            {
                var res = connection.QueryFirstOrDefault<Student>(sql, new { Id = 1 });
                Console.WriteLine(res?.ToString());
            }
        }
    }
}

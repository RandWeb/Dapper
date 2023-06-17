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
    internal class QuerySingle
    {
        static string sql = "select * from Students where id=@id";
        static string sql2 = "select * from Students";
        public static void QuerySingleAnonymous()
        {
            using (var connection = DapperHelper.DbConnection())
            {
                var res = connection.QuerySingle(sql, new { Id = 4 });
                
                    Console.WriteLine(res.FirstName);
                    Console.WriteLine(res.LastName);
            }
        }

        public static void QuerySingleStronglyTyped()
        {
            using (var connection = DapperHelper.DbConnection())
            {
                var res = connection.QuerySingle<Student>(sql2);
                Console.WriteLine(res.ToString());
            }
        }

        public static void QuerySingleOrDefaultAnonymous()
        {
            using (var connection = DapperHelper.DbConnection())
            {
                var res = connection.QuerySingleOrDefault(sql, new { Id = 1 });

                Console.WriteLine(res?.FirstName);
                Console.WriteLine(res?.LastName);
            }
        }

        public static void QuerySingleOrDefaultStronglyTyped()
        {
            using (var connection = DapperHelper.DbConnection())
            {
                var res = connection.QuerySingleOrDefault<Student>(sql, new { Id = 1 });
                Console.WriteLine(res?.ToString());
            }
        }
    }
}

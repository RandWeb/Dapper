using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Dapper;

namespace Season02Execute
{
    internal class ExecuteScalarSample
    {
        private static string query = "SELECT FirstName,LastName FROM Students;";

        public static void Sample()
        {
            using (var cnn=DapperHelper.DbConnection())
            {
                var firstname = cnn.ExecuteScalar<string>(query);
                Console.WriteLine(firstname);
            }
        }

    }
}

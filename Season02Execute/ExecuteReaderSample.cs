using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Dapper;

namespace Season02Execute
{
    internal class ExecuteReaderSample
    {
        private static string query01 = "SELECT * FROM Students;";

        public static void Sample()
        {
            using (var cnn=DapperHelper.DbConnection())
            {
                var reader = cnn.ExecuteReader(query01);

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write(item+ "  ");
                    }
                    Console.WriteLine();
                }

            }
        }
    }
}

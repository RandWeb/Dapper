using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Models;
using Dapper;

namespace Season02Query
{
    internal class QueryMultiple
    {
        static string sql = "select * from Students where Id=@Id; Select * from Courses;";


        public static void Sample()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                using (var multi = cnn.QueryMultiple(sql, new { Id = 3 }))
                {
                    var student = multi.Read<Student>().FirstOrDefault();
                    var courses = multi.Read<Courses>().ToList();

                    Console.WriteLine(student);

                    foreach (var course in courses)
                    {
                        Console.WriteLine(course);

                    }

                }


            }
        }
    }
}

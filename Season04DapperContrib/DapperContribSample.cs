using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Season04DapperContrib.Models;

namespace Season04DapperContrib
{
    internal class DapperContribSample
    {

        public static void Get()
        {
            using (var cnn=DapperHelper.DbConnection())
            {
                cnn.Open();
                var student = cnn.Get<Student>(3);
                Console.WriteLine(student);
            }
        }
        public static void GetAll()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                cnn.Open();
                var students = cnn.GetAll<Student>();
                foreach (var student in students)
                {
                    Console.WriteLine(student);
                }
            }
        }

        public static void InsertSingle()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                cnn.Open();
                var res = cnn.Insert<Student>(new Student()
                {
                    FirstName = "sina",
                    LastName = "sinaei",
                    BirthDate = DateTime.Now.AddYears(-19),
                    NationalCode = "0123456789"
                });
                Console.WriteLine(res);
            }
        }

        public static void InsertMultiple()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                cnn.Open();
                var students = new List<Student>()
                {
                    new Student()
                    {
                        FirstName = "mina",
                        LastName = "sinaei",
                        BirthDate = DateTime.Now.AddYears(-19),
                        NationalCode = "0123456789"
                    },
                    new Student()
                    {
                        FirstName = "akbar",
                        LastName = "sinaei",
                        BirthDate = DateTime.Now.AddYears(-19),
                        NationalCode = "0123456789"
                    }
                };
                var res = cnn.Insert(students);
                Console.WriteLine(res);
            }
        }


        public static void UpdateSingle()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                cnn.Open();
                var res = cnn.Update(new Student()
                {
                    Id=3,
                    FirstName = "sina",
                    LastName = "akbari",
                    BirthDate = DateTime.Now.AddYears(-19),
                    NationalCode = "9876543210"
                });
                Console.WriteLine(res);
            }
        }

        public static void UpdateMultiple()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                cnn.Open();
                var students = new List<Student>()
                {
                    new Student()
                    {
                        Id=14,
                        FirstName = "mina2",
                        LastName = "sinaei",
                        BirthDate = DateTime.Now.AddYears(-19),
                        NationalCode = "0123456789"
                    },
                    new Student()
                    {
                        Id=15,
                        FirstName = "akbar2",
                        LastName = "sinaei",
                        BirthDate = DateTime.Now.AddYears(-19),
                        NationalCode = "0123456789"
                    }
                };
                var res = cnn.Update(students);
                Console.WriteLine(res);
            }
        }

        public static void DeleteSingle()
        {
            using (var cnn=DapperHelper.DbConnection())
            {
                cnn.Open();
                var res = cnn.Delete(new Student()
                {
                    Id = 14
                });
                Console.WriteLine(res);
            }
        }
        public static void DeleteMultiple()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                cnn.Open();
                var students = new List<Student>()
                {
                    new Student()
                    {
                        Id = 13
                    },
                    new Student()
                    {
                        Id = 15
                    }

                };

                var res = cnn.Delete(students);
                Console.WriteLine(res);
            }
        }

        public static void DeleteAll()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                cnn.Open();
                var res = cnn.DeleteAll<Student>();
                Console.WriteLine(res);
            }
        }
    }
}

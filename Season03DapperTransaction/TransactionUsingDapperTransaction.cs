using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Models;
using Dapper;
using Dapper.Transaction;

namespace Season03DapperTransaction
{
    internal class TransactionUsingDapperTransaction
    {
        static string insertStudent = @"INSERT INTO Students (FirstName,LastName,NationalCode,BirthDate) 
                                                Values (@FirstName,@LastName,@NationalCode,@BirthDate);";

        static string insertCourse = @"INSERT INTO Courses (Title,TeacherFullname,Capacity) 
                                                Values (@Title,@TeacherFullname,@Capacity);";


        public static void TransactionSample()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                cnn.Open();
                using (var transaction=cnn.BeginTransaction())
                {

                    var addedCourseCount = transaction.Execute(insertCourse,
                        new[]
                        {
                            new { Title = "Biology", TeacherFullname = "ali akbari", Capacity = 15 },
                            new { Title = "chemistry", TeacherFullname = "amir akbari", Capacity = 15 },
                            new { Title = "Physics", TeacherFullname = "iman akbari", Capacity = 15 }
                        });
                    var addedStudentsCount = transaction.Execute(insertStudent, new Student[]
                    {
                        new Student()
                        {
                            FirstName = "mina",
                            LastName = "minaei",
                            NationalCode = "0123456789",
                            BirthDate = DateTime.Now.AddYears(-30)
                        },
                        new Student()
                        {
                            FirstName = "sina",
                            LastName = "minaei",
                            NationalCode = "0123456789123",
                            BirthDate = DateTime.Now.AddYears(-30)
                        }
                    });

                    Console.WriteLine($"Courses Count:{addedCourseCount}");
                    Console.WriteLine($"students Count:{addedStudentsCount}");
                }
               
            }
        }
    }
}

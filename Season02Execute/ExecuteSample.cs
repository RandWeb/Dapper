using System.Data;
using Common;
using Common.Models;
using Dapper;
namespace Season02Execute
{
    internal class ExecuteSample
    {
        static string sp_CreateCourse = "CreateCourse";
        static string _insertCommand = @"INSERT INTO Students (FirstName,LastName,NationalCode,BirthDate) 
                                                Values (@FirstName,@LastName,@NationalCode,@BirthDate);";
        static string _updateCommand = "UPDATE Students SET FirstName = @FirstName WHERE Id = @Id;";
        static string _deleteCommand = "DELETE FROM Students WHERE Id = @Id;";
        static string _deleteCommand2 = "DELETE FROM Students WHERE FirstName = @FirstName;";


        public static void CallingStoreProcedure()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                //Single
                //Anonymous Parameter
                var affectedRows = cnn.Execute(sp_CreateCourse,
                    new
                    {
                        Title = "Math",
                        TeacherName = "Reza akbari",
                        Capacity = 15
                    },
                    commandType: CommandType.StoredProcedure);
                Console.WriteLine($"Affected rows: {affectedRows}");
                Console.ReadLine();

                //Multiple
                var affectedRows2 = cnn.Execute(sp_CreateCourse,
                   new[]
                   {
                       new
                       {
                           Title = "Math2",
                           TeacherName = "Reza akbari",
                           Capacity = 15
                       },
                       new
                       {
                           Title = "Math3",
                           TeacherName = "Reza akbari",
                           Capacity = 15
                       },
                       new
                       {
                           Title = "Math4",
                           TeacherName = "Reza akbari",
                           Capacity = 15
                       }
                   },
                    commandType: CommandType.StoredProcedure);
                Console.WriteLine($"Affected rows: {affectedRows2}");
            }
        }
        public static void CallingStoreProcedureWithDynamicParameters()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Title", "Sport", DbType.String, ParameterDirection.Input);
                parameter.Add("@TeacherName", "Pejman jamshidi");
                parameter.Add("@Capacity", 15);
                parameter.Add("@OutPam",dbType:DbType.Int32, direction:ParameterDirection.Output);

                parameter.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                cnn.Execute(sp_CreateCourse, parameter, commandType: CommandType.StoredProcedure);

                Console.WriteLine($"Rows Count: {parameter.Get<int>("@RowCount")}");
                Console.WriteLine($"OutPam: {parameter.Get<int>("@OutPam")}");

            }
        }
        public static void CallingStoreProcedureWithMultipleDynamicParameters()
        {
            using (var cnn = DapperHelper.DbConnection())
            {
                var parameters = new List<DynamicParameters>();
                DynamicParameters parameter1 = new DynamicParameters();
                parameter1.Add("@Title", "Sport", DbType.String, ParameterDirection.Input);
                parameter1.Add("@TeacherName", "Pejman jamshidi");
                parameter1.Add("@Capacity", 15);
                parameter1.Add("@OutPam", dbType: DbType.Int32, direction: ParameterDirection.Output);

                parameter1.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


                DynamicParameters parameter2 = new DynamicParameters();
                parameter2.Add("@Title", "Sport", DbType.String, ParameterDirection.Input);
                parameter2.Add("@TeacherName", "Pejman jamshidi");
                parameter2.Add("@Capacity", 15);
                parameter2.Add("@OutPam", dbType: DbType.Int32, direction: ParameterDirection.Output);

                parameter2.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


                parameters.Add(parameter1);
                parameters.Add(parameter2);
                cnn.Execute(sp_CreateCourse, parameters.ToArray(), commandType: CommandType.StoredProcedure);
                var sumRowCount = parameters.Sum(c => c.Get<int>("@RowCount"));
                var sumOutPam = parameters.Sum(c => c.Get<int>("@OutPam"));

                Console.WriteLine($"Rows Count: {sumRowCount}");
                Console.WriteLine($"OutPam: {sumOutPam}");

            }
        }

        public static void InsertCommand()
        {
            using (var cnn=DapperHelper.ProfilerDbConnection())
            {
                var affectedRows = cnn.Execute(_insertCommand, new Student()
                {
                    FirstName = "milad",
                    LastName = "akbari",
                    NationalCode = "0123456789",
                    BirthDate = DateTime.Now.AddYears(-30)
                });

                Console.WriteLine($"rows count: {affectedRows}");


                var log = DapperHelper.GetExecutedCommands();
                Console.WriteLine(log);
            }
        }

        public static void UpdateCommand()
        {
            using (var cnn = DapperHelper.ProfilerDbConnection())
            {
                var affectedRows = cnn.Execute(_updateCommand, new Student()
                {
                    FirstName = "ali",
                   Id=1
                });

                Console.WriteLine($"rows count: {affectedRows}");


                var log = DapperHelper.GetExecutedCommands();
                Console.WriteLine(log);
            }
        }

        public static void DeleteCommand()
        {
            using (var cnn = DapperHelper.ProfilerDbConnection())
            {
                var affectedRows = cnn.Execute(_deleteCommand, new
                {
                    Id = 1
                });
                var affectedRows2 = cnn.Execute(_deleteCommand2, new
                {
                    FirstName = "milad"
                });

                Console.WriteLine($"rows count: {affectedRows}");


                var log = DapperHelper.GetExecutedCommands();
                Console.WriteLine(log);
            }
        }
    }
}

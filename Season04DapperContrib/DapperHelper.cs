using System.Data;
using System.Data.SqlClient;
using MiniProfiler.Integrations;

namespace Season04DapperContrib
{
    public class DapperHelper
    {
        public static string ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MyDapperCourse;Data Source=.";

        public static IDbConnection DbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static CustomDbProfiler GetCustomDbProfiler()
        {
            return CustomDbProfiler.Current;
        }

        public static IDbConnection ProfilerDbConnection()
        {
            var factory = new SqlServerDbConnectionFactory(ConnectionString);
            return ProfiledDbConnectionFactory.New(factory, GetCustomDbProfiler());
        }

        public static string GetExecutedCommands()
        {
            var res = GetCustomDbProfiler().ProfilerContext.GetExecutedCommands();
            GetCustomDbProfiler().ProfilerContext.Reset();
            return res;
        }
    }
}

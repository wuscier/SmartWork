using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace SmartWork.Wpf
{
    public class SqliteDataAccess
    {
        public static List<Job> LoadJobs()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Job>("select * from Job");

                return output.ToList();
            }
        }

        public static List<Job> LoadJobs(int limit)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Job>($"select * from Job LIMIT {limit}");

                return output.ToList();
            }
        }

        public static List<Job> LoadJobs(int limit, int offset)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Job>($"select * from Job LIMIT {limit} OFFSET {offset}");

                return output.ToList();
            }
        }

        public static int CountJobs()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<int>("select count(Id) from Job", new DynamicParameters());

                return output.FirstOrDefault();
            }
        }

        public static int SaveJob(Job job)
        {
            int result = 0;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                result = cnn.Execute("insert into Job (Description, Script) values (@Description,@Script)", job);
            }

            return result;
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}

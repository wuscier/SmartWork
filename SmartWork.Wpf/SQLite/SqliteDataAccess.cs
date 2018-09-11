using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace SmartWork.Wpf.SQLite
{
    public class SqliteDataAccess
    {
        public static List<Job> LoadJobs()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Job>("select * from Job", new DynamicParameters());

                return output.ToList();
            }
        }

        public static void SaveJob(Job job)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Job (Name, CreateTime) values (@Name,@CreateTime)", job);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}

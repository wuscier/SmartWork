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
                var output = cnn.Query<Job>("select * from Job", new DynamicParameters());

                return output.ToList();
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

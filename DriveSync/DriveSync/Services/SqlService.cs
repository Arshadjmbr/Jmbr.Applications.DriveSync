using DriveSync.Services.IServices;
using Microsoft.Data.SqlClient;

namespace DriveSync.Services
{
    public class SqlService :ISqlService
    {
        private string mConfiguration;

        public SqlService(IConfiguration configuration)
        {
            mConfiguration = configuration["ConnectionStrings:DbConnection"];
        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(mConfiguration);
        }
    }
}

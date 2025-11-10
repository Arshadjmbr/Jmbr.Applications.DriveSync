using Microsoft.Data.SqlClient;

namespace DriveSync.Services.IServices
{
    public interface ISqlService
    {
        SqlConnection GetSqlConnection();
    }
}

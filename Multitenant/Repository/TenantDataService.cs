using Microsoft.Data.SqlClient;
using System.Data;

namespace Multitenant.Repository
{
    public class TenantDataService : ITenantDataService
    {
        string connectionString = "Server=SHAHED\\SQLEXPRESS;Database=MultiTenant;Trusted_Connection=True;MultipleActiveResultSets=true";
        public DataTable CreateDatabaseAndFileTables(string DBNO, string DBName)
        {
            try
            {
                DataTable dt2 = new DataTable();
                using (var connection = new SqlConnection(connectionString))
                {
                    string sql = "dbo.sprCreateDatabaseAndBaseTables";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, connection))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@DBNO", DBNO);
                        sqlCmd.Parameters.AddWithValue("@DBName", DBName);
                        connection.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt2);
                        }
                        connection.Close();
                    }
                }
                return dt2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public DataTable CreateFile(decimal AllocateStoreSize, long TenantId, bool IsLocked, FileTypeId, FileSize)
        //{
        //    try
        //    {
        //        DataTable dt2 = new DataTable();
        //        using (var connection = new SqlConnection(connectionString))
        //        {
        //            string sql = "dbo.sprCreateDatabaseAndBaseTables";
        //            using (SqlCommand sqlCmd = new SqlCommand(sql, connection))
        //            {
        //                sqlCmd.CommandType = CommandType.StoredProcedure;
        //                sqlCmd.Parameters.AddWithValue("@DBNO", DBNO);
        //                sqlCmd.Parameters.AddWithValue("@DBName", DBName);
        //                connection.Open();
        //                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
        //                {
        //                    sqlAdapter.Fill(dt2);
        //                }
        //                connection.Close();
        //            }
        //        }
        //        return dt2;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Task<object> 
        //{


        //}
    }
}

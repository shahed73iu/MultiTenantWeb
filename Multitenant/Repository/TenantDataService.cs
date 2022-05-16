using Microsoft.Data.SqlClient;
using Multitenant.Data;
using Multitenant.Models;
using MultiTenant.Data;
using System.Data;

namespace Multitenant.Repository
{
    public class TenantDataService : ITenantDataService
    {
        private readonly ApplicationDbContext _context;
        public TenantDataService(ApplicationDbContext context)
        {
            _context = context;
        }
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

        public string InsertTanentInfoIntoDynamicDB(TenantDataViewModel model)
        {
            try
            {
                //string dynamicConnectionString = "Server=SHAHED\\SQLEXPRESS;Database=" + model.DatabaseName + ";Trusted_Connection=True;MultipleActiveResultSets=true";
                DataTable dt2 = new DataTable();
                using (var connection = new SqlConnection(connectionString))
                {
                    string sql = "dbo.sprDynamicTenantInsert";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, connection))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@Name", model.TenantName);
                        sqlCmd.Parameters.AddWithValue("@ContactNo", model.ContactNo);
                        sqlCmd.Parameters.AddWithValue("@Email", model.Email);
                        sqlCmd.Parameters.AddWithValue("@Address", model.Address);
                        sqlCmd.Parameters.AddWithValue("@DatabaseName", model.DatabaseName);

                        connection.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt2);
                        }
                        connection.Close();
                    }
                }
                return "";
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
        public async Task<MessageHelper> CreateApi(TenantDataViewModel model)
        {
            try
            {
                var countDbInfo = _context.TenantInfo.Count();

                var tenantModel = new TenantInfo()
                {
                    Name = model.TenantName,
                    Address = model.Address,
                    ContactNo = model.ContactNo,
                    Email = model.Email,
                    DatabaseName = model.DatabaseName,
                };
                await _context.TenantInfo.AddAsync(tenantModel);
                await _context.SaveChangesAsync();

                return new MessageHelper
                {
                    Message = "Created Successfully",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Data.FileType>> GetFileTypes()
        {
            try
            {

                List<Data.FileType> fileDDL = _context.FileType.ToList();
                return fileDDL;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<MultiTenant.Data.TenantInfo>> GetTenants()
        {
            try
            {

                List<MultiTenant.Data.TenantInfo> TenantDDL = _context.TenantInfo.ToList();
                return TenantDDL;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

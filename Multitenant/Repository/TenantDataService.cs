﻿using Microsoft.Data.SqlClient;
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
                    ContactNo = model.Emial,
                    Email = model.Emial,
                    DatabaseName = "",
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
    }
}

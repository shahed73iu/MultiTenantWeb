using Multitenant.Models;
using System.Data;

namespace Multitenant.Repository
{
    public interface ITenantDataService
    {
        public DataTable CreateDatabaseAndFileTables(string DBNO, string DBName);
        public Task<MessageHelper> CreateApi(TenantDataViewModel model);
        public string InsertTanentInfoIntoDynamicDB(TenantDataViewModel model);
        public Task<List<Data.FileType>> GetFileTypes();
        public Task<List<MultiTenant.Data.TenantInfo>> GetTenants();
    }
}

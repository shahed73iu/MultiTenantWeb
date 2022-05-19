using Multitenant.Models;
using System.Data;

namespace Multitenant.Repository
{
    public interface ITenantDataService
    {
        public DataTable CreateDatabaseAndFileTables(string DBNO, string DBName);
        public DataTable CreateFile(long FileTypeId, decimal FileSize, long TenantId, string DatabaseName, decimal AllocateStoreSize, bool IsLocked);

        public DataTable DatabaseNameListWithAllocatedSize(string? DbName);
        public Task<MessageHelper> CreateApi(TenantDataViewModel model);
        public string InsertTanentInfoIntoDynamicDB(TenantDataViewModel model);
        public Task<List<Data.FileType>> GetFileTypes();
        public Task<List<MultiTenant.Data.TenantInfo>> GetTenants();
        public Task<string> GetDbName(long TenantId);
    }
}

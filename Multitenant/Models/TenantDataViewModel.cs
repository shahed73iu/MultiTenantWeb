namespace Multitenant.Models
{
    public class TenantDataViewModel
    {
        public long TenantId { get; set; }
        public IFormFile inputFile { get; set; }

        public long? FileTypeId { get; set; }
        public List<Data.FileType> fileDDL { get; set; }
        public List<MultiTenant.Data.TenantInfo> TenantDDL { get; set; }
    }
}

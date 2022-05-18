namespace Multitenant.Models
{
    public class TenantDataViewModel
    {
        public string TenantName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DatabaseName { get; set; }
        public long TenantId { get; set; }
        public IFormFile inputFile { get; set; }

        public long FileTypeId { get; set; }
        public List<Data.FileType> fileDDL { get; set; }
        public List<MultiTenant.Data.TenantInfo> TenantDDL { get; set; }
    }
}

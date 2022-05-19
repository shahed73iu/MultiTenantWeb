namespace Multitenant.Models
{
    public class GetDbViewModel
    {
        public long TempAutoId { get; set; }
        public long TenantId { get; set; }
        public string DatabaseName { get; set; }
        public string TotalStorage { get; set; }
        public bool IsProcessed { get; set; }
    }
}

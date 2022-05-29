using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiTenant.Data;

namespace Multitenant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TenantInfo> TenantInfo { get; set; }
        public DbSet<FileType> FileType { get; set; }
    }
    //public class ApplicationDbContext : IdentityDbContext
    //{
    //    public ApplicationDbContext(string connectionString)
    //        : base(GetOptions(connectionString))
    //    {

    //    }
    //    private static DbContextOptions GetOptions(string connectionString)
    //    {
    //        return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
    //    }

    //    public DbSet<TenantInfo> TenantInfo { get; set; }
    //    public DbSet<FileType> FileType { get; set; }
    //}
}
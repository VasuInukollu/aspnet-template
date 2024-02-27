using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Extensions;
using Project.Models;
using Project.Models.Tenancy;

namespace Project.Data;

public class TenantDbContext : EFCoreStoreDbContext<Tenant>
{
    private readonly IOptions<AppSettings> _appSettings;

    public TenantDbContext(DbContextOptions<TenantDbContext> options, IOptions<AppSettings> appSettings) : base(options)
    {
        _appSettings = appSettings;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var appSettings = _appSettings.Value;

        var connectionString = appSettings.ToConnectionString();

        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        //optionsBuilder.EnableSensitiveDataLogging();
        //optionsBuilder.EnableDetailedErrors();
        //optionsBuilder.EnableServiceProviderCaching();


    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tenant>().ToTable("Tenants");
    }
}

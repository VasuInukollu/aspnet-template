using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Extensions;
using Project.Models;
using Project.Models.Tenancy;

namespace Project.Data;

// dotnet ef migrations add init --context AppDbContext

public class AppDbContext : MultiTenantIdentityDbContext<IdentityUser, IdentityRole, string>
{
    private readonly IOptions<AppSettings> _appSettings;
    private readonly ITenantInfo _tenantInfo;

    public AppDbContext(IOptions<AppSettings> appSettings, ITenantInfo tenantInfo) : base(tenantInfo)
    {
        _appSettings = appSettings;
        _tenantInfo = tenantInfo;
    }

    public AppDbContext(IOptions<AppSettings> appSettings, ITenantInfo tenantInfo, DbContextOptions<AppDbContext> options) : base(tenantInfo, options)
    {
        _appSettings = appSettings;
        _tenantInfo = tenantInfo;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var appSettings = _appSettings.Value;

        var connectionString = appSettings.ToConnectionString(_tenantInfo as Tenant);

        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tenant>().ToTable("Tenants");
    }

    public DbSet<Tenant> Tenants { get; set; } = default!;

}

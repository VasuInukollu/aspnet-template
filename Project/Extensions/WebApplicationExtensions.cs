using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models.Tenancy;
using System.Diagnostics;

namespace Project.Extensions;

public static class WebApplicationExtensions
{

    public static void ApplyDbMigrations(this WebApplication app, string[] args)
    {
        using var scope = app.Services.CreateScope();

        {
            var db = new DesignTimeAppDbContextFactory().CreateDbContext(args);
            db.Database.Migrate();
        }

    }


    public static void SeedDefaultData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<TenantDbContext>();

        db.Database.EnsureCreated();

        if (!db.TenantInfo.Any(x => x.NormalizedIdentifier == "UNKNOWN"))
            db.TenantInfo.Add(new Tenant { Id = "unknown", Identifier = "unknown", NormalizedIdentifier = "UNKNOWN", Name = "Unknown" });

        db.SaveChanges();

        //var store = scope.ServiceProvider.GetRequiredService<IMultiTenantStore<Tenant>>();
        //store.TryAddAsync(new Tenant { Id = "unknown", Identifier = "unknown", NormalizedIdentifier= "UNKNOWN", Name = "Unknown" }).Wait();


    }


}

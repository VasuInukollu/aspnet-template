using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using Project.Extensions;
using Project.Models;
using Project.Models.Tenancy;
namespace Project.Data;

public class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{

    public AppDbContext CreateDbContext(string[] args)
    {
        var _settings = AppSettings.Get(args);

        var connectionString = _settings.ToConnectionString();

        Console.WriteLine(connectionString);

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        var _options = Options.Create(_settings);

        return new AppDbContext(_options, new Tenant() /*{ ConnectionString = connectionString }*/, optionsBuilder.Options);
    }
}

using System.Diagnostics;

namespace Project.Models;


public class AppSettings
{
    public DbSettings Db { get; set; } = default!;


    public static AppSettings Get(string[] args)
    {
        IConfigurationRoot configuration = WebApplication.CreateBuilder(args).Configuration;

        var _settings = configuration.GetSection("App").Get<AppSettings>();
        Debug.Assert(_settings != null);
        return _settings;
    }

    public class DbSettings
    {
        public string Server { get; set; } = default!;

        public string? UserId { get; set; }

        public string? Password { get; set; }
    }

}

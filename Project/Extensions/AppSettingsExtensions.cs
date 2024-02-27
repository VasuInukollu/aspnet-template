using Project.Models;
using Project.Models.Tenancy;

namespace Project.Extensions;

public static class AppSettingsExtensions
{
    public static string? ToConnectionString(this AppSettings mySql)
    {
        return mySql.ToConnectionString("ProjectDb");
    }

    public static string? ToConnectionString(this AppSettings mySql, Tenant? tenant)
    {
        if (tenant is null) return null;

        switch (tenant.IsolationLevel)
        {
            case IsolationLevel.None:
                return mySql.ToConnectionString();
            case IsolationLevel.DifferentDb:
                return mySql.ToConnectionString(tenant.DbName);
            case IsolationLevel.DifferentServer:
                return tenant.ConnectionString;
            default:
                return null;
        }
    }


    public static string? ToConnectionString(this AppSettings appSettings, string? dbName)
    {
        return $"Server={appSettings.Db.Server};UserID={appSettings.Db.UserId};Password={appSettings.Db.Password};Database={dbName};";
    }

}

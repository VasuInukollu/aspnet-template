using Microsoft.AspNetCore.Identity;
using Project.Data;
using Project.Extensions;
using Project.Models;
using Project.Models.Tenancy;

var builder = WebApplication.CreateBuilder(args);


// 1. Add services to the container.

// appsettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("App"));

// database
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// multi tenancy
builder.Services.AddMultiTenant<Tenant>()
    .WithEFCoreStore<TenantDbContext, Tenant>()
    .WithHostStrategy()
    .WithBasePathStrategy(options => options.RebaseAspNetCorePathBase = true)
    .WithHeaderStrategy("TenantId")
    .WithClaimStrategy("TenantId")
    .WithStaticStrategy("unknown")
    .WithPerTenantAuthentication();

// controllers and pages
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// api
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// 2. Configure the pipiline order

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMultiTenant();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();


// 3. Seed the database

app.ApplyDbMigrations(args);
app.SeedDefaultData();


// 4. Run the application

app.Run();

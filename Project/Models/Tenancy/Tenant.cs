using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Tenancy;


[Index(nameof(NormalizedIdentifier), IsUnique = true)]
public class Tenant : ITenantInfo
{
    // id and name
    [StringLength(64)]
    public string? Id { get; set; }

    [Required]
    [StringLength(256)]
    public string? Identifier { get; set; }
    [Required]
    [StringLength(256)]
    public string? NormalizedIdentifier { get; set; }

    [Required]
    [StringLength(256)]
    public string? Name { get; set; }

    // database setup
    public IsolationLevel IsolationLevel { get; set; }
    [StringLength(256)]
    public string? DbName { get; set; }
    [StringLength(1024)]
    public string? ConnectionString { get; set; }

    // external authentication
    [StringLength(512)]
    public string? OpenIdConnectAuthority { get; set; }
    [StringLength(256)]
    public string? OpenIdConnectClientId { get; set; }
    [StringLength(1024)]
    public string? OpenIdConnectClientSecret { get; set; }

}

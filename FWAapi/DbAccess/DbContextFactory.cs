using Microsoft.Extensions.Configuration;

namespace FWAapi.DbAccess;

public class DbContextFactory
{
    private readonly IConfiguration _configuration;

    public int UserId { get; set; }

    public DbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbContext CreateInstance()
    {
        var cs = _configuration?.GetConnectionString("FWA");
        if (cs is null) throw new Exception($"No connection string defined");
        return new DbContext(cs, UserId);
    }
}

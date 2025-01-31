using Microsoft.EntityFrameworkCore;

namespace Rony.Store.Infrastructure.Database;
public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

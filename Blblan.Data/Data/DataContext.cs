using Blblan.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blblan.Data;

public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Conversation> Conversations { get; set; }

    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conversation>()
            .HasQueryFilter(x => x.IsDeleted);

        modelBuilder.Entity<Subscription>()
            .HasQueryFilter(x => x.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection =
            "server=localhost;Port=3309;database=SoftwareEngineerDb;user=root;password=sqlpass1234;old guids=true;";

        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
    }
}
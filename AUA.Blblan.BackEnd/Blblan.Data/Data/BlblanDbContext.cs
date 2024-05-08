using Blblan.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blblan.Data;

public class BlblanDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public BlblanDbContext(DbContextOptions<BlblanDbContext> options) : base(options)
    {
    }

    public DbSet<Conversation> Conversations { get; set; }

    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conversation>()
            .HasOne<User>(s => s.User)
            .WithMany(s => s.Conversations)
            .HasForeignKey(s => s.UserId);

        modelBuilder.Entity<Message>()
            .HasOne(x => x.Conversation)
            .WithMany(s => s.Messages)
            .HasForeignKey(s => s.ConversationId);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = "server=localhost;Port=3309;database=BlblanDb;user=root;password=sqlpass1234;old guids=true;";

        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
    }
}
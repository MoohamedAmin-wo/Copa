using Cupa.Infrastructure.Persistence.Configurations;
namespace Cupa.Infrastructure.Persistence;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    // Support schema
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Position> Position { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<SuccessStories> SuccessStories { get; set; }

    // Main schema
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<ClubPlayer> ClubPlayers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfigurations).Assembly);
    }
}

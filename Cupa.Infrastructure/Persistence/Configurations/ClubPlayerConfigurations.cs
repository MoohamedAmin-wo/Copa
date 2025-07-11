namespace Cupa.Infrastructure.Persistence.Configurations;

public class ClubPlayerConfigurations : IEntityTypeConfiguration<ClubPlayer>
{
    public void Configure(EntityTypeBuilder<ClubPlayer> builder)
    {
        builder.ToTable("ClubPlayers", schema: "Main");
        builder.HasIndex(x => new { x.ClubId, x.Number });
    }
}
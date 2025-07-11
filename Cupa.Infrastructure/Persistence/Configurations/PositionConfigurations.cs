namespace Cupa.Infrastructure.Persistence.Configurations;

public class PositionConfigurations : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Positions", schema: "Support");
        builder.HasIndex(ix => ix.PositionName);
        builder.HasIndex(ix => ix.PositionAPPR);
        builder.HasIndex(ix => new { ix.PositionAPPR, ix.PositionName });

        builder.Property(prop => prop.PositionName).HasMaxLength(50).IsRequired();
        builder.Property(prop => prop.PositionAPPR).HasMaxLength(4).IsRequired();
    }
}


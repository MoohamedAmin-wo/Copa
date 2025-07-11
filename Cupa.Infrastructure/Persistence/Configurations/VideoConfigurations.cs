namespace Cupa.Infrastructure.Persistence.Configurations;

public class VideoConfigurations : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Videos", schema: "Support");
        builder.HasIndex(ix => ix.Url);
        builder.HasIndex(ix => new { ix.Id, ix.Url });

        builder.Property(prop => prop.Url).HasMaxLength(500).IsRequired();
        builder.Property(prop => prop.IsDeleted).HasDefaultValue(false);
    }
}


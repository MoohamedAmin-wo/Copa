namespace Cupa.Infrastructure.Persistence.Configurations;

public class PicturesConfigurations : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.ToTable("Pictures", schema: "Support");
        builder.Property(prop => prop.Url).HasMaxLength(500);
        builder.Property(prop => prop.ThumbnailUrl).HasMaxLength(500);

        builder.Property(prop => prop.IsDeleted).HasDefaultValue(false);
    }
}

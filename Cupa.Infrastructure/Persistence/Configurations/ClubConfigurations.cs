namespace Cupa.Infrastructure.Persistence.Configurations;

internal class ClubConfigurations : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.ToTable("Clubs", schema: "Main");

        builder.HasIndex(ix => ix.ClubName);
        builder.Property(prop => prop.ClubName).HasMaxLength(256).IsRequired();
        builder.Property(prop => prop.Story).HasMaxLength(5000).IsRequired();
        builder.Property(prop => prop.About).HasMaxLength(500).IsRequired();
        builder.Property(prop => prop.LogoUrl).HasMaxLength(500).IsRequired();
        builder.Property(prop => prop.MainShirtUrl).HasMaxLength(500).IsRequired();
        builder.Property(prop => prop.ClubPictureUrl).HasMaxLength(500).IsRequired();
        builder.Property(prop => prop.IsDeleted).HasDefaultValue(false);
    }
}

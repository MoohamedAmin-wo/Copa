namespace Cupa.Infrastructure.Persistence.Configurations;

internal class ManagerConfigurations : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.ToTable(name: "Managers", schema: "Main");
        builder.HasKey(pk => pk.Id);
        builder.HasIndex(ix => ix.UserId);
        builder.HasOne(x => x.User).WithOne(a => a.Manager).HasForeignKey<Manager>(f => f.UserId);
        builder.Property(prop => prop.StoryAbout).HasMaxLength(5000);
        builder.Property(prop => prop.StoryWithClub).HasMaxLength(5000);
    }
}

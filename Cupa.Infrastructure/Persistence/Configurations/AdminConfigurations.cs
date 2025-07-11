namespace Cupa.Infrastructure.Persistence.Configurations;

internal class AdminConfigurations : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable("Admins", schema: "Support");
        builder.HasIndex(ix => ix.UserId);
        builder.HasOne(x => x.User).WithOne(a => a.Admin).HasForeignKey<Admin>(f => f.UserId);
    }
}
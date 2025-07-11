namespace Cupa.Infrastructure.Persistence.Configurations;
public class AddressConfigurations : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses", schema: "Support");
        builder.HasIndex(ix => ix.UserId);
        builder.HasOne(x => x.User).WithOne(a => a.Address).HasForeignKey<Address>(f => f.UserId);
        builder.Property(prop => prop.Country).HasMaxLength(64).IsRequired();
        builder.Property(prop => prop.Governrate).HasMaxLength(64).IsRequired();
        builder.Property(prop => prop.City).HasMaxLength(64).IsRequired();
        builder.Property(prop => prop.Regoin).HasMaxLength(64).IsRequired();
        builder.Property(prop => prop.Street).HasMaxLength(500).IsRequired();
    }
}
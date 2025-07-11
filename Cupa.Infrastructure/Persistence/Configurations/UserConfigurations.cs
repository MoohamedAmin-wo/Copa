namespace Cupa.Infrastructure.Persistence.Configurations;
public class UserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users", schema: "Idn");
        builder.HasIndex(prop => prop.Email);
        builder.HasIndex(prop => prop.UserName);

        builder.Property(prop => prop.FirstName).HasMaxLength(60).IsRequired();
        builder.Property(prop => prop.LastName).HasMaxLength(256).IsRequired();
        builder.Property(prop => prop.UserName).HasMaxLength(128).IsRequired();
        builder.Property(prop => prop.Email).HasMaxLength(256).IsRequired();
        builder.Property(prop => prop.IsDeleted).HasDefaultValue(false);
        builder.Property(prop => prop.IsBlocked).HasDefaultValue(false);
    }
}


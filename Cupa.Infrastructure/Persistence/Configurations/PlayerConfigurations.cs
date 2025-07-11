namespace Cupa.Infrastructure.Persistence.Configurations;
public class PlayerConfigurations : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Palyers", schema: "Main");
        builder.HasIndex(x => x.UserId);
        builder.HasOne(x => x.User).WithOne(a => a.Player).HasForeignKey<Player>(f => f.UserId);
        builder.Property(prop => prop.NickName).HasMaxLength(60).IsRequired();
        builder.Property(prop => prop.IsDeleted).HasDefaultValue(false);
        builder.Property(prop => prop.IsFree).HasDefaultValue(false);
        builder.Property(prop => prop.Views).HasDefaultValue(0);
        builder.Property(prop => prop.Rate).HasDefaultValue(1);

    }
}
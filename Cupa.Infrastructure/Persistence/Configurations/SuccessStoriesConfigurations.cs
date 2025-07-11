namespace Cupa.Infrastructure.Persistence.Configurations
{
    class SuccessStoriesConfigurations : IEntityTypeConfiguration<SuccessStories>
    {
        public void Configure(EntityTypeBuilder<SuccessStories> builder)
        {
            builder.ToTable("SuccessStories", schema: "Main");
            builder.Property(prop => prop.Fullname).HasMaxLength(256).IsRequired();
            builder.Property(prop => prop.PictureUrl).HasMaxLength(500);
            builder.Property(prop => prop.VideoUrl).HasMaxLength(500);
        }
    }
}

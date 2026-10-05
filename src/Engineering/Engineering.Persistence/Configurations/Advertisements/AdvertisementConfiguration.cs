using Engineering.Domain.Entities.Advertisements;

namespace Engineering.Persistence.Configurations.Advertisements;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    private const string TableName = "Advertisements";
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.MetaActiveConfiguration<Advertisement, long>(TableName);

        builder.Property(oo => oo.TitleFa)
            .HasComment(AdvertisementCmts.TitleFa)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.TitleEn)
            .HasComment(AdvertisementCmts.TitleEn)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.DescriptionFa)
            .HasComment(AdvertisementCmts.DescriptionFa)
            .HasColumnType("nvarchar(1500)")
            .IsRequired();

        builder.Property(oo => oo.DescriptionEn)
            .HasComment(AdvertisementCmts.DescriptionEn)
            .HasColumnType("nvarchar(1500)")
            .IsRequired();

        builder.Property(oo => oo.TechnicalCode)
            .HasComment(AdvertisementCmts.TechnicalCode)
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();
    }
}
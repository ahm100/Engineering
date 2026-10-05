using Engineering.Domain.Entities.ServiceInfos;
using Engineering.Domain.Entities.ServiceInfos.Enums;

namespace Engineering.Persistence.Configurations.ServiceInfos;

public class ServiceConfiguration : IEntityTypeConfiguration<ServiceInfo>
{
    private const string _tableName = "EngineeringServices";
    public void Configure(EntityTypeBuilder<ServiceInfo> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ServiceInfoName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.ServiceInfoEnName)
            .HasColumnType("nvarchar(250)")
            .HasComment(AdvertisementCmts.TitleEn)
            .HasMaxLength(250);

        builder.Property(oo => oo.DescriptionFa)
            .HasColumnType("nvarchar(1500)")
            .HasComment(AdvertisementCmts.DescriptionFa)
            .HasMaxLength(1500);

        builder.Property(oo => oo.DescriptionEn)
            .HasColumnType("nvarchar(1500)")
            .HasComment(AdvertisementCmts.DescriptionEn)
            .HasMaxLength(1500);

        builder.Property(oo => oo.ServiceInfoCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .HasDefaultValue(ServiceInfoType.Engineering)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.UnitOfMeasurementId)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.PreferentialReferenceCode);
    }
}
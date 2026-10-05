using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Persistence.Configurations.OperationInfos;

public class OperationInfoServiceConfiguration : IEntityTypeConfiguration<OperationInfoService>
{
    private const string _tableName = "OperationInfoServices";
    public void Configure(EntityTypeBuilder<OperationInfoService> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.TimeSpant)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.OperationInfo)
            .WithMany(oo => oo.OperationInfoServices)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(nameof(OperationInfo.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ServiceInfo)
            .WithMany(oo => oo.OperationInfoServices)
            .HasForeignKey("ServiceInfoId").HasPrincipalKey(nameof(ServiceInfo.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
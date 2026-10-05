using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;

namespace Engineering.Persistence.Configurations.OperationInfos.ConsumptionStandards;

public class ConsumptionStandardExpertConfiguration : IEntityTypeConfiguration<ConsumptionStandardExpert>
{
    private const string _tableName = "EngineeringStandardExperts";
    public void Configure(EntityTypeBuilder<ConsumptionStandardExpert> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ExpertUnitId)
            .IsRequired();

        builder.Property(oo => oo.ExpertNumber)
                .HasColumnType("decimal(18,5)");

        builder.Property(oo => oo.TimeSpant)
            .IsRequired();

        builder.Property(oo => oo.UnusedPercentage)
            .HasColumnType("decimal(18,5)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.OperationInfo)
            .WithMany(oo => oo.ConsumptionStandardExperts)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(nameof(OperationInfo.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
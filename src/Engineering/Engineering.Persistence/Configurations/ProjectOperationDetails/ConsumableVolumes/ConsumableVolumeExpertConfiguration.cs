using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails.ConsumableVolumes;

public class ConsumableVolumeExpertConfiguration : IEntityTypeConfiguration<ConsumableVolumeExpert>
{
    private const string _tableName = "ProjectOperationDetailConsumableVolumeExperts";
    public void Configure(EntityTypeBuilder<ConsumableVolumeExpert> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ExpertId)
            .IsRequired();

        builder.Property(oo => oo.Number)
            .IsRequired();

        builder.Property(oo => oo.IsStandard)
            .IsRequired();

        builder.Property(oo => oo.StandardValue)
            .HasColumnType("bigint");

        builder.Property(oo => oo.FinalValue)
            .HasColumnType("bigint")
            .HasDefaultValue(36000000000)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.ConsumableVolumeExperts)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
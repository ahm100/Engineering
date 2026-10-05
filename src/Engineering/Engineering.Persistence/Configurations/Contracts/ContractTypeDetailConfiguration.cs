using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractTypeDetailConfiguration
    : IEntityTypeConfiguration<ContractTypeDetail>
{
    private const string TableName = "ContractTypeDetails";

    public void Configure(EntityTypeBuilder<ContractTypeDetail> builder)
    {
        builder.MetaConfiguration<ContractTypeDetail, long>(TableName);

        builder.Property(oo => oo.ContractTypeId)
            .HasComment(ContractCmts.ContractTypeId)
            .IsRequired();

        builder.Property(oo => oo.ConsumableVolumeProductId)
            .HasComment(ContractCmts.ConsumableVolumeProductId);

        builder.Property(oo => oo.ProjectOperationDetailId)
            .HasComment(GlobalCmts.ProjectOperationDetailId);

        builder.Property(oo => oo.ProjectOperationDetailContractorServiceId)
            .HasComment(ContractCmts.ProjectOperationDetailContractorServiceId);

        builder.Property(oo => oo.Quantity)
            .HasColumnType("decimal(18,5)")
            .HasComment(ContractCmts.Quantity)
            .IsRequired();

        builder.Property(oo => oo.UnitOfMeasurementId)
            .HasComment(ContractCmts.UnitOfMeasurementId);

        builder.Property(oo => oo.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.UnitPrice);

        builder.Property(oo => oo.FixedAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.FixedAmount);

        builder.Property(oo => oo.TechnicalSpecifications)
            .HasComment(ContractCmts.TechnicalSpecifications);

        builder.Property(oo => oo.ExpectedDeliverables)
            .HasComment(ContractCmts.ExpectedDeliverables);

        builder.Property(oo => oo.Duration)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.DetailDuration);

        builder.Property(oo => oo.DurationUnit)
            .HasComment(ContractCmts.DurationUnit)
            .IsRequired(false);

        builder.Property(oo => oo.IsSubjectToAdjustment)
            .HasComment(ContractCmts.IsSubjectToAdjustment)
            .IsRequired();

        builder.HasIndex(oo => new
        {
            oo.ContractTypeId,
            oo.ConsumableVolumeProductId
        })
            .IsUnique()
            .HasFilter(
                "[IsDeleted] = 0 AND [ConsumableVolumeProductId] IS NOT NULL");

        builder.HasIndex(oo => new
        {
            oo.ContractTypeId,
            oo.ProjectOperationDetailId
        })
            .IsUnique()
            .HasFilter(
                "[IsDeleted] = 0 AND [ProjectOperationDetailId] IS NOT NULL");

        builder.HasIndex(oo => new
        {
            oo.ContractTypeId,
            oo.ProjectOperationDetailContractorServiceId
        })
            .IsUnique()
            .HasFilter(
                "[IsDeleted] = 0 AND [ProjectOperationDetailContractorServiceId] IS NOT NULL");

        builder.HasOne(oo => oo.ContractType)
            .WithMany(oo => oo.ContractTypeDetails)
            .HasForeignKey(oo => oo.ContractTypeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne<ConsumableVolumeProduct>()
            .WithMany()
            .HasForeignKey(oo => oo.ConsumableVolumeProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<ProjectOperationDetail>()
            .WithMany()
            .HasForeignKey(oo => oo.ProjectOperationDetailId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<ProjectOperationDetailContractorService>()
            .WithMany()
            .HasForeignKey(oo => oo.ProjectOperationDetailContractorServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable(TableName, tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "CK_ContractTypeDetails_ExactlyOneSource",
                "(CASE WHEN [ConsumableVolumeProductId] IS NULL THEN 0 ELSE 1 END + " +
                "CASE WHEN [ProjectOperationDetailId] IS NULL THEN 0 ELSE 1 END + " +
                "CASE WHEN [ProjectOperationDetailContractorServiceId] IS NULL THEN 0 ELSE 1 END) = 1");
        });
    }
}

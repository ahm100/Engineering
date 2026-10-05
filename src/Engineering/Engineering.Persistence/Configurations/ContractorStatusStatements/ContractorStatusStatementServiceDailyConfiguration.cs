using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementServiceDailyConfiguration : IEntityTypeConfiguration<ContractorStatusStatementServiceDaily>
{
    private const string _tableName = "ContractorStatusStatementServiceDailies";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementServiceDaily> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.TimeSpant);

        builder.Property(oo => oo.Volume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.AcceptablePercentage)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.AcceptableAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.AcceptableDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ProjectManagementApprovalPercentage)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.ProjectManagementApprovedPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ProjectManagementApprovedDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ManagementApprovalPercentage)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.ApprovedPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ApprovedDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder
            .HasOne(oo => oo.ContractorStatusStatementService)
            .WithMany(oo => oo.ContractorStatusStatementServiceDailies)
            .HasForeignKey("ContractorStatusStatementServiceId")
            .HasPrincipalKey(nameof(ContractorStatusStatementService.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(oo => oo.DailyProjectOperationService)
            .WithMany(oo => oo.ContractorStatusStatementServiceDailies)
            .HasForeignKey("DailyProjectOperationServiceId")
            .HasPrincipalKey(nameof(DailyProjectOperationService.Id))
            .OnDelete(DeleteBehavior.NoAction);


    }
}

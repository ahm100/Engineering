using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Configurations.EmployerStatusStatements;

public class EmployerStatusStatementProjectOperationConfiguration : IEntityTypeConfiguration<EmployerStatusStatementProjectOperation>
{
    private const string _tableName = "EmployerStatusStatementProjectOperations";
    public void Configure(EntityTypeBuilder<EmployerStatusStatementProjectOperation> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.TotalWorkVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalDetailWorkVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalDailyWorkVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorWorkVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.SupervisorWorkVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ConsultantWorkVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerRepresentativeWorkVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorUnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorTotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorConfirmeTotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerCommercialUnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerCommercialTotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerCommercialConfirmeTotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.EmployerStatusStatement)
            .WithMany(oo => oo.EmployerStatusStatementProjectOperations)
            .HasForeignKey("EmployerStatusStatementId").HasPrincipalKey(nameof(EmployerStatusStatement.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.EmployerStatusStatementProjectOperations)
            .HasForeignKey("ProjectOperationId").HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}


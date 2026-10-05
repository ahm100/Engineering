using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Persistence.Configurations.EmployerStatusStatements;

public class EmployerStatusStatementProjectOperationDetailDailyConfiguration : IEntityTypeConfiguration<EmployerStatusStatementProjectOperationDetailDaily>
{
    private const string _tableName = "EmployerStatusStatementProjectOperationDetailDailies";
    public void Configure(EntityTypeBuilder<EmployerStatusStatementProjectOperationDetailDaily> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ContractorLength)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorWidth)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorHeight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorWeight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorNumber)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.SupervisorLength)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.SupervisorWidth)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.SupervisorHeight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.SupervisorWeight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.SupervisorNumber)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.SupervisorVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.SupervisorDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ConsultantLength)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ConsultantWidth)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ConsultantHeight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ConsultantWeight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ConsultantNumber)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ConsultantDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.EmployerRepresentativeLength)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerRepresentativeWidth)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerRepresentativeHeight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerRepresentativeWeight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerRepresentativeNumber)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerRepresentativeVolume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.EmployerRepresentativeDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.EmployerStatusStatementProjectOperationDetail)
            .WithMany(oo => oo.EmployerStatusStatementProjectOperationDetailDailies)
            .HasForeignKey("EmployerStatusStatementProjectOperationDetailId").HasPrincipalKey(nameof(EmployerStatusStatementProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.DailyProjectOperation)
            .WithMany(oo => oo.EmployerStatusStatementProjectOperationDetailDailies)
            .HasForeignKey("DailyProjectOperationId").HasPrincipalKey(nameof(DailyProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}


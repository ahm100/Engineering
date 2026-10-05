using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.EmployerStatusStatements;

public class EmployerStatusStatementProjectOperationDetailConfiguration : IEntityTypeConfiguration<EmployerStatusStatementProjectOperationDetail>
{
    private const string _tableName = "EmployerStatusStatementProjectOperationDetails";
    public void Configure(EntityTypeBuilder<EmployerStatusStatementProjectOperationDetail> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.TotalWorkVolume)
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


        builder.HasOne(oo => oo.EmployerStatusStatementProjectOperation)
            .WithMany(oo => oo.EmployerStatusStatementProjectOperationDetails)
            .HasForeignKey("EmployerStatusStatementProjectOperationId").HasPrincipalKey(nameof(EmployerStatusStatementProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.EmployerStatusStatementProjectOperationDetails)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}


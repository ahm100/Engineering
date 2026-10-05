using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.EmployerStatusStatements;

public class EmployerStatusStatementConfiguration : IEntityTypeConfiguration<EmployerStatusStatement>
{
    private const string _tableName = "EmployerStatusStatements";
    public void Configure(EntityTypeBuilder<EmployerStatusStatement> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.StatusStatementCode)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.PercentageOfWorkDone)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.PercentageOfWorkDone)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.CalculatedAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.StatusStatementVolume)
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


        builder.HasOne(oo => oo.EmployerContract)
            .WithMany(oo => oo.EmployerStatusStatements)
            .HasForeignKey("EmployerContractId").HasPrincipalKey(nameof(EmployerContract.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.EmployerStatusStatements)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}


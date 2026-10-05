using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Persistence.Configurations.EmployerStatusStatements;

public class EmployerStatusStatementHistoryConfiguration : IEntityTypeConfiguration<EmployerStatusStatementHistory>
{
    private const string _tableName = "EmployerStatusStatementHistories";
    public void Configure(EntityTypeBuilder<EmployerStatusStatementHistory> builder)
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


        builder.HasOne(oo => oo.EmployerStatusStatement)
            .WithMany(oo => oo.EmployerStatusStatementHistories)
            .HasForeignKey("EmployerStatusStatementId").HasPrincipalKey(nameof(EmployerStatusStatement.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}


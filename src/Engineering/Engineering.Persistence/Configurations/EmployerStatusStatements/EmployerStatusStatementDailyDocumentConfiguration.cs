using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Persistence.Configurations.EmployerStatusStatementProjectOperationDetailDailys;

public class EmployerStatusStatementDailyDocumentConfiguration : IEntityTypeConfiguration<EmployerStatusStatementDailyDocument>
{
    private const string _tableName = "EmployerStatusStatementDailyDocuments";
    public void Configure(EntityTypeBuilder<EmployerStatusStatementDailyDocument> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Url)
            .HasColumnType("nvarchar(1500)")
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.EmployerStatusStatementProjectOperationDetailDaily)
               .WithMany(oo => oo.EmployerStatusStatementDailyDocuments)
               .HasForeignKey("EmployerStatusStatementProjectOperationDetailDailyId")
               .HasPrincipalKey(nameof(EmployerStatusStatementProjectOperationDetailDaily.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}

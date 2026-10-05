using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Persistence.Configurations.EmployerStatusStatementProjectOperations;

public class EmployerStatusStatementProjectOperationDocumentConfiguration : IEntityTypeConfiguration<EmployerStatusStatementProjectOperationDocument>
{
    private const string _tableName = "EmployerStatusStatementProjectOperationDocuments";
    public void Configure(EntityTypeBuilder<EmployerStatusStatementProjectOperationDocument> builder)
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


        builder.HasOne(oo => oo.EmployerStatusStatementProjectOperation)
               .WithMany(oo => oo.EmployerStatusStatementProjectOperationDocuments)
               .HasForeignKey("EmployerStatusStatementProjectOperationId")
               .HasPrincipalKey(nameof(EmployerStatusStatementProjectOperation.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}

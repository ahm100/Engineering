using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Persistence.Configurations.EmployerStatusStatements;

public class EmployerStatusStatementDocumentConfiguration : IEntityTypeConfiguration<EmployerStatusStatementDocument>
{
    private const string _tableName = "EmployerStatusStatementDocuments";
    public void Configure(EntityTypeBuilder<EmployerStatusStatementDocument> builder)
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


        builder.HasOne(oo => oo.EmployerStatusStatement)
               .WithMany(oo => oo.EmployerStatusStatementDocuments)
               .HasForeignKey("EmployerStatusStatementId")
               .HasPrincipalKey(nameof(EmployerStatusStatement.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}

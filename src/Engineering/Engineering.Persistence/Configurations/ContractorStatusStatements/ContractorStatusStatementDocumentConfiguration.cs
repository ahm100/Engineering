using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementDocumentConfiguration : IEntityTypeConfiguration<ContractorStatusStatementDocument>
{
    private const string _tableName = "ContractorStatusStatementDocuments";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementDocument> builder)
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


        builder.HasOne(oo => oo.ContractorStatusStatement)
               .WithMany(oo => oo.ContractorStatusStatementDocuments)
               .HasForeignKey("ContractorStatusStatementId")
               .HasPrincipalKey(nameof(ContractorStatusStatement.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}

using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.EmployerContracts;

public class EmployerDocConfiguration : IEntityTypeConfiguration<EmployerDoc>
{
    private const string TableName = "EmployerDocs";
    public void Configure(EntityTypeBuilder<EmployerDoc> builder)
    {
        builder.MetaActiveConfiguration<EmployerDoc, long>(TableName);

        builder.Property(oo => oo.Type)
            .HasComment(EContractCmts.EDocumentType)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Version)
            .HasComment(GlobalCmts.Version)
            .HasColumnType("decimal");

        builder.Property(oo => oo.RegistrationDate)
            .HasComment(EContractCmts.RegistrationDate)
            .IsRequired();


        builder.HasOne(oo => oo.EmployerContract)
            .WithMany(oo => oo.EmployerDocs)
            .HasForeignKey("EmployerContractId")
            .HasPrincipalKey(nameof(EmployerContract.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
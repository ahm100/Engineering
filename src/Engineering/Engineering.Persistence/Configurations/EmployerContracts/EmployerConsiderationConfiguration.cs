using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.EmployerContracts;

public class EmployerConsiderationConfiguration : IEntityTypeConfiguration<EmployerConsideration>
{
    private const string TableName = "EmployerConsiderations";
    public void Configure(EntityTypeBuilder<EmployerConsideration> builder)
    {
        builder.MetaActiveConfiguration<EmployerConsideration, long>(TableName);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Type)
            .IsRequired();

        builder.HasOne(oo => oo.EmployerContract)
            .WithMany(oo => oo.EmployerConsiderations)
            .HasForeignKey("EmployerContractId")
            .HasPrincipalKey(nameof(EmployerContract.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
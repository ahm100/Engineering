using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractTypeConfiguration
    : IEntityTypeConfiguration<ContractType>
{
    private const string TableName = "ContractTypes";

    public void Configure(
        EntityTypeBuilder<ContractType> builder)
    {
        builder.MetaConfiguration<ContractType, long>(
            TableName);

        builder.Property(oo => oo.Kind)
            .HasComment(GlobalCmts.ContractTypeKind)
            .IsRequired();

        builder.Property(oo => oo.PricingMethod)
            .HasComment(GlobalCmts.PricingMethod)
            .IsRequired();

        builder.Property(oo => oo.ContractId)
            .HasComment(GlobalCmts.ContractId)
            .IsRequired();

        builder.HasIndex(oo => new
        {
            oo.ContractId,
            oo.Kind
        })
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.HasOne(oo => oo.Contract)
            .WithMany(oo => oo.ContractTypes)
            .HasForeignKey(oo => oo.ContractId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}

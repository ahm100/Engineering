using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementCostOverConfiguration : IEntityTypeConfiguration<ContractorStatusStatementCostOver>
{
    private const string _tableName = "ContractorStatusStatementCostOvers";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementCostOver> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Amount)
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


        builder
            .HasOne(oo => oo.ContractorStatusStatement)
            .WithMany(oo => oo.ContractorStatusStatementCostOvers)
            .HasForeignKey("ContractorStatusStatementId")
            .HasPrincipalKey(nameof(ContractorStatusStatement.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(oo => oo.ContractorContractDetailCostOver)
            .WithMany(oo => oo.ContractorStatusStatementCostOvers)
            .HasForeignKey("ContractorContractDetailCostOverId")
            .HasPrincipalKey(nameof(ContractorContractDetailCostOver.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}

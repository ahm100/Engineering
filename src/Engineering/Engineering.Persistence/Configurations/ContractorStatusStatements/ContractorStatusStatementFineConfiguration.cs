using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementFineConfiguration : IEntityTypeConfiguration<ContractorStatusStatementFine>
{
    private const string _tableName = "ContractorStatusStatementFines";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementFine> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.RegistrationDate)
           .IsRequired();

        builder.Property(oo => oo.ConfirmedPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

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
            .WithMany(oo => oo.ContractorStatusStatementFines)
            .HasForeignKey("ContractorStatusStatementId")
            .HasPrincipalKey(nameof(ContractorStatusStatement.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(oo => oo.RequestReward)
            .WithMany(oo => oo.ContractorStatusStatementFines)
            .HasForeignKey("RequestRewardId")
            .HasPrincipalKey(nameof(RequestReward.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}

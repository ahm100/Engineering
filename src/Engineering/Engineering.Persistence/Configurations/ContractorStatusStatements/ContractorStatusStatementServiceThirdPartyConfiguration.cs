using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementServiceThirdPartyConfiguration : IEntityTypeConfiguration<ContractorStatusStatementServiceThirdParty>
{
    private const string _tableName = "ContractorStatusStatementServiceThirdParties";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementServiceThirdParty> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ThirdPartyId)
            .IsRequired();

        builder.Property(oo => oo.SkillId)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .IsRequired();

        builder.Property(oo => oo.WorkingDay)
           .IsRequired();

        builder.Property(oo => oo.Price)
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
            .HasOne(oo => oo.ContractorStatusStatementService)
            .WithMany(oo => oo.ContractorStatusStatementServiceThirdParties)
            .HasForeignKey("ContractorStatusStatementServiceId")
            .HasPrincipalKey(nameof(ContractorStatusStatementService.Id))
            .OnDelete(DeleteBehavior.NoAction);


    }
}

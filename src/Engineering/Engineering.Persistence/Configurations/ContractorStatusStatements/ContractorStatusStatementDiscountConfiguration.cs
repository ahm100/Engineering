using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementDiscountConfiguration : IEntityTypeConfiguration<ContractorStatusStatementDiscount>
{
    private const string _tableName = "ContractorStatusStatementDiscounts";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementDiscount> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.RegistrationDate);

        builder.Property(oo => oo.Description);

        builder.Property(oo => oo.DiscountPrice)
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
            .WithMany(oo => oo.ContractorStatusStatementDiscounts)
            .HasForeignKey("ContractorStatusStatementId")
            .HasPrincipalKey(nameof(ContractorStatusStatement.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}

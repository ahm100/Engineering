using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementProductConfiguration : IEntityTypeConfiguration<ContractorStatusStatementProduct>
{
    private const string _tableName = "ContractorStatusStatementProducts";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementProduct> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ProductId)
            .IsRequired();

        builder.Property(oo => oo.RequestedCount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalSupplyCount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.RegistrationDate)
           .IsRequired();

        builder.Property(oo => oo.IsPurchaseForContractor)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.OtherPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TransferPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TaxNumber)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.DiscountOnInvoiceNumber)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TotalPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.CustomerInvoiceNumber)
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
            .WithMany(oo => oo.ContractorStatusStatementProducts)
            .HasForeignKey("ContractorStatusStatementId")
            .HasPrincipalKey(nameof(ContractorStatusStatement.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(oo => oo.RequestGoodsSupplyDetail)
            .WithMany(oo => oo.ContractorStatusStatementServiceProducts)
            .HasForeignKey("RequestGoodsSupplyDetailId")
            .HasPrincipalKey(nameof(RequestGoodsSupplyDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}

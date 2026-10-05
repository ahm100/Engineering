using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class FiduciaryProductDetailReturnConfiguration : IEntityTypeConfiguration<FiduciaryProductDetailReturn>
{
    private const string _tableName = "FiduciaryProductDetailReturns";
    public void Configure(EntityTypeBuilder<FiduciaryProductDetailReturn> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.LateFine)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ReturnDate)
            .IsRequired();

        builder.Property(oo => oo.ReturnCount)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .IsRequired();

        builder.Property(oo => oo.LateDay)
            .IsRequired();

        builder.Property(oo => oo.InvoiceId)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.HasOne(oo => oo.FiduciaryProductDetailManagement)
               .WithMany(oo => oo.Returns)
               .HasForeignKey("FiduciaryProductDetailManagementId")
               .HasPrincipalKey(nameof(FiduciaryProductDetailManagement.Id));
    }
}
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class FiduciaryProductHistoryConfiguration : IEntityTypeConfiguration<FiduciaryProductHistory>
{
    private const string _tableName = "FiduciaryProductHistories";
    public void Configure(EntityTypeBuilder<FiduciaryProductHistory> builder)
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

        builder.Property(oo => oo.ThirdPartyId)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.StatusDescription)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.HasOne(oo => oo.FiduciaryProduct)
               .WithMany(oo => oo.Histories)
               .HasForeignKey("FiduciaryProductId")
               .HasPrincipalKey(nameof(FiduciaryProduct.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}

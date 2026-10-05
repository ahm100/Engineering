using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class FiduciaryProductDetailReturnDocumentConfiguration : IEntityTypeConfiguration<FiduciaryProductDetailReturnDocument>
{
    private const string _tableName = "FiduciaryProductDetailReturnDocuments";
    public void Configure(EntityTypeBuilder<FiduciaryProductDetailReturnDocument> builder)
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

        builder.Property(oo => oo.Url)
            .IsRequired();


        builder.HasOne(oo => oo.FiduciaryProductDetailReturn)
               .WithMany(oo => oo.Documents)
               .HasForeignKey("FiduciaryProductDetailReturnId")
               .HasPrincipalKey(nameof(FiduciaryProductDetailReturn.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}


using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class RequestMachineryBillDocumentConfiguration : IEntityTypeConfiguration<RequestMachineryBillDocument>
{
    private const string _tableName = "RequestMachineryBillDocuments";
    public void Configure(EntityTypeBuilder<RequestMachineryBillDocument> builder)
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


        builder.HasOne(oo => oo.RequestMachinery)
               .WithMany(oo => oo.RequestMachineryBillDocuments)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}


using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class RequestMachineryDocumentConfiguration : IEntityTypeConfiguration<RequestMachineryDocument>
{
    private const string _tableName = "RequestMachineryDocuments";
    public void Configure(EntityTypeBuilder<RequestMachineryDocument> builder)
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
               .WithMany(oo => oo.RequestMachineryDocuments)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}


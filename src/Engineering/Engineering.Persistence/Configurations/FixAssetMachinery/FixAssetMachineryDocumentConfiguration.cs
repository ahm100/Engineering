using Engineering.Domain.Entities.FixAssetMachineries;
using FixAssetMachineryModel = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Persistence.Configurations.FixAssetMachinery;

public class FixAssetMachineryDocumentConfiguration : IEntityTypeConfiguration<FixAssetMachineryDocument>
{
    private const string _tableName = "FixAssetMachineryDocuments";
    public void Configure(EntityTypeBuilder<FixAssetMachineryDocument> builder)
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

        builder.HasOne(oo => oo.FixAssetMachinery)
               .WithMany(oo => oo.FixAssetMachineryDocuments)
               .HasForeignKey("FixAssetMachineryId")
               .HasPrincipalKey(nameof(FixAssetMachineryModel.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}


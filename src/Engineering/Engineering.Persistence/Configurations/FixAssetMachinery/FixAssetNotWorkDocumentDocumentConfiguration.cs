using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Persistence.Configurations.FixAssetMachinery;

public class FixAssetNotWorkDocumentDocumentConfiguration : IEntityTypeConfiguration<FixAssetNotWorkDocument>
{
    private const string _tableName = "FixAssetNotWorkDocuments";
    public void Configure(EntityTypeBuilder<FixAssetNotWorkDocument> builder)
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

        builder.HasOne(oo => oo.FixAssetMachineryNotWork)
               .WithMany(oo => oo.FixAssetNotWorkDocuments)
               .HasForeignKey("FixAssetMachineryNotWorkId")
               .HasPrincipalKey(nameof(FixAssetMachineryNotWork.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}


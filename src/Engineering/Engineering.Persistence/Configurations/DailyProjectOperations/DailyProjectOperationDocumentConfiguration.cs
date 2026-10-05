using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Configurations.DailyProjectOperations;

public class DailyProjectOperationDocumentConfiguration : IEntityTypeConfiguration<DailyProjectOperationDocument>
{
    private const string _tableName = "DailyProjectOperationDocuments";
    public void Configure(EntityTypeBuilder<DailyProjectOperationDocument> builder)
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
    }
}

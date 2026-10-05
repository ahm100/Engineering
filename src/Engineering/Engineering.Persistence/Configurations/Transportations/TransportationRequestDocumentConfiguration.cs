using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationRequestDocumentConfiguration : IEntityTypeConfiguration<TransportationRequestDocument>
{
    private const string _tableName = "TransportationRequestDocuments";
    public void Configure(EntityTypeBuilder<TransportationRequestDocument> builder)
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

        builder.Property(oo => oo.IsBill)
            .HasDefaultValue(false)
            .IsRequired();
    }
}
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationCargoDocumentConfiguration : IEntityTypeConfiguration<TransportationCargoDocument>
{
    private const string _tableName = "TransportationCargoDocuments";
    public void Configure(EntityTypeBuilder<TransportationCargoDocument> builder)
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

        builder.HasOne(oo => oo.TransportationCargo)
            .WithMany(x => x.TransportationCargoDocuments)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.TransportationCargoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
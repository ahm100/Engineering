using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationCargoConfiguration : IEntityTypeConfiguration<TransportationCargo>
{
    private const string _tableName = "TransportationCargos";
    public void Configure(EntityTypeBuilder<TransportationCargo> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(a => a.IsDeleted).IsRequired();

        builder.Property(a => a.Updated);

        builder.Property(a => a.Created).IsRequired();

        builder.Property(a => a.UpdaterId);

        builder.Property(a => a.PackingNumber)
            .IsRequired();

        builder.Property(a => a.SecurityConfirm)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(a => a.ThirdPartyName)
            .IsRequired(false);

        builder.HasOne(oo => oo.ThirdParty)
            .WithMany()
            .HasForeignKey(x => x.ThirdPartyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Packing)
            .WithMany()
            .HasForeignKey(x => x.PackingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

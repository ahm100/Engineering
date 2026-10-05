using Engineering.Domain.Entities.Trips;

namespace Engineering.Persistence.Configurations.Trips;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    private const string _tableName = "Trips";
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.TripName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.TripCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.IsLock)
            .HasDefaultValue(false);

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

    }
}

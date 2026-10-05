using Engineering.Domain.Entities.Messengers;

namespace Engineering.Persistence.Configurations.Messengers;

public class CostCenterMessengersConfiguration : IEntityTypeConfiguration<Messenger>
{
    private const string _tableName = "Messengers";
    public void Configure(EntityTypeBuilder<Messenger> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.MessengerType)
            .IsRequired();

        builder.Property(oo => oo.MessengerTargetType)
            .IsRequired();

        builder.Property(oo => oo.TargetId)
            .IsRequired();

        builder.Property(oo => oo.CompanyId)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(500)")
            .HasMaxLength(500)
            .IsUnicode(true);

        builder.Property(oo => oo.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

    }
}
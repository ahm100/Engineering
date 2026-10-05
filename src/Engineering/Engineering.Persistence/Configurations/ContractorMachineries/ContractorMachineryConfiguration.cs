using Engineering.Domain.Entities.ContractorMachineries;
using Engineering.Domain.Entities.ContractorMachineries.Enums;
using Engineering.Domain.Entities.Machineries;

namespace Engineering.Persistence.Configurations.ContractorMachineries;

public class ContractorMachineryConfiguration : IEntityTypeConfiguration<ContractorMachinery>
{
    private const string _tableName = "ContractorMachineries";
    public void Configure(EntityTypeBuilder<ContractorMachinery> builder)
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
        .ValueGeneratedOnAdd().
        IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.MachineryIdentifier)
          .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.NumberPlates)
            .HasColumnType("nvarchar(20)");

        builder.Property(oo => oo.Unit)
            .HasDefaultValue(ContractorMachineryUnit.Daily)
            .IsRequired();

        builder.Property(oo => oo.MachineryPrice)
             .HasColumnType("decimal(18, 2)")
             .IsRequired();

        builder.Property(oo => oo.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.Machinery)
               .WithMany(oo => oo.ContractorMachineries)
               .HasForeignKey("MachineryId")
               .HasPrincipalKey(nameof(Machinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}

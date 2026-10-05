using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractAdjustmentConfigurationConfiguration : IEntityTypeConfiguration<ContractAdjustmentConfiguration>
{
    public void Configure(EntityTypeBuilder<ContractAdjustmentConfiguration> builder)
    {
        builder.MetaConfiguration<ContractAdjustmentConfiguration, long>("ContractAdjustmentConfigurations");
        builder.Property(x => x.ContractId).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.CurrencyBaseRate).HasColumnType("decimal(18,6)");
        builder.Property(x => x.CurrencyCustomReference).HasMaxLength(250);
        builder.Property(x => x.OtherBasis).HasMaxLength(250);
        builder.Property(x => x.OtherReference).HasMaxLength(250);
        builder.Property(x => x.OtherIndex).HasMaxLength(250);
        builder.Property(x => x.Description).HasMaxLength(1500);
        builder.HasIndex(x => x.ContractId);
        builder.HasOne(x => x.Contract).WithMany(x => x.AdjustmentConfigurations)
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Cascade).IsRequired();
        builder.HasOne(x => x.PriceIndex).WithMany().HasForeignKey(x => x.PriceIndexId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractTypeDetailAdjustmentConfiguration
    : IEntityTypeConfiguration<ContractTypeDetailAdjustment>
{
    private const string TableName = "ContractTypeDetailAdjustments";

    public void Configure(
        EntityTypeBuilder<ContractTypeDetailAdjustment> builder)
    {
        builder.MetaConfiguration<ContractTypeDetailAdjustment, long>(TableName);

        builder.Property(oo => oo.ContractTypeDetailId)
            .HasComment(ContractCmts.ContractTypeDetail)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .HasComment(ContractCmts.ContractTypeDetailAdjustmentType)
            .IsRequired();

        builder.Property(oo => oo.PriceIndexBaseYear)
            .HasComment(ContractCmts.PriceIndexBaseYear);

        builder.Property(oo => oo.PriceIndexBasePeriod)
            .HasComment(ContractCmts.PriceIndexBasePeriod);

        builder.Property(oo => oo.PriceIndexId)
            .HasComment(ContractCmts.ContractAdjustmentIndexId);

        builder.HasOne(oo => oo.PriceIndex)
            .WithMany()
            .HasForeignKey(oo => oo.PriceIndexId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(oo => oo.CurrencyBaseDate)
            .HasComment(ContractCmts.CurrencyBaseDate);

        builder.Property(oo => oo.CurrencyBaseRate)
            .HasColumnType("decimal(18,6)")
            .HasComment(ContractCmts.CurrencyBaseRate);

        builder.Property(oo => oo.CurrencyId)
            .HasComment(GlobalCmts.CurrencyId);

        builder.Property(oo => oo.CurrencyReferenceType)
            .HasComment(ContractCmts.CurrencyReferenceType);

        builder.Property(oo => oo.CurrencyCustomReference)
            .HasMaxLength(250)
            .HasComment(ContractCmts.CurrencyCustomReference);

        builder.Property(oo => oo.OtherBasis)
            .HasMaxLength(250)
            .HasComment(ContractCmts.OtherBasis);

        builder.Property(oo => oo.OtherReference)
            .HasMaxLength(250)
            .HasComment(ContractCmts.OtherReference);

        builder.Property(oo => oo.OtherIndex)
            .HasMaxLength(250)
            .HasComment(ContractCmts.OtherIndex);

        builder.Property(oo => oo.Description)
            .HasMaxLength(1500)
            .HasComment(GlobalCmts.Description);

        builder.HasIndex(oo => oo.ContractTypeDetailId)
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.HasOne(oo => oo.ContractTypeDetail)
            .WithOne(oo => oo.Adjustment)
            .HasForeignKey<ContractTypeDetailAdjustment>(
                oo => oo.ContractTypeDetailId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}

using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class TransportationContractorPriceWeightConfiguration : IEntityTypeConfiguration<TransportationContractorPriceWeight>
{
    private const string _tableName = "TransportationContractorPriceWeights";
    public void Configure(EntityTypeBuilder<TransportationContractorPriceWeight> builder)
    {
        builder.MetaConfiguration<TransportationContractorPriceWeight, long>(_tableName);

        builder.Property(a => a.UntilWeight)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorPriceWeightCmts.UntilWeight)
            .IsRequired(true);

        builder.Property(a => a.Price)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorPriceWeightCmts.Price)
            .IsRequired(true);

        builder.Property(a => a.IsFixed)
            .HasComment(TransportationContractorPriceWeightCmts.Price)
            .IsRequired(true);

        builder.HasOne(x => x.TransportationContractor)
       .WithMany(x => x.PriceWeights)
       .HasForeignKey(x => x.TransportationContractorId)
       .HasPrincipalKey(x => x.Id)
       .OnDelete(DeleteBehavior.Restrict);
    }
}

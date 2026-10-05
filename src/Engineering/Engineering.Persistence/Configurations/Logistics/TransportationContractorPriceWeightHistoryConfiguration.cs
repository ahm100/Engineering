using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class TransportationContractorPriceWeightHistoryConfiguration : IEntityTypeConfiguration<TransportationContractorPriceWeightHistory>
{
    private const string _tableName = "TransportationContractorPriceWeightHistories";
    public void Configure(EntityTypeBuilder<TransportationContractorPriceWeightHistory> builder)
    {
        builder.MetaConfiguration<TransportationContractorPriceWeightHistory, long>(_tableName);

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

        builder.HasOne(x => x.TransportationContractorPriceWeight)
            .WithMany(x => x.PriceWeightHistories)
            .HasForeignKey(x => x.TransportationContractorPriceWeightId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(x => x.Creator)
        //    .WithMany()
        //    .HasPrincipalKey(x => x.UserId)
        //    .HasForeignKey(x => x.CreatorId)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}

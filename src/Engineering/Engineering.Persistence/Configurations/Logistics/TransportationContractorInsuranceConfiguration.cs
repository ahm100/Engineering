using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class TransportationContractorInsuranceConfiguration : IEntityTypeConfiguration<TransportationContractorInsurance>
{
    private const string _tableName = "TransportationContractorInsurances";
    public void Configure(EntityTypeBuilder<TransportationContractorInsurance> builder)
    {
        builder.MetaConfiguration<TransportationContractorInsurance, long>(_tableName);

        builder.Property(a => a.MinProductPrice)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorInsuranceCmts.MinProductPrice)
            .IsRequired(true);

        builder.Property(a => a.MaxProductPrice)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorInsuranceCmts.MaxProductPrice)
            .IsRequired(true);

        builder.Property(a => a.FixedPrice)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorInsuranceCmts.FixedPrice)
            .IsRequired(true);

        builder.Property(a => a.Multiplication)
            .HasDefaultValue(1m)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorInsuranceCmts.Multiplication)
            .IsRequired(false);

        builder.Property(a => a.Division)
            .HasDefaultValue(1m)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorInsuranceCmts.Division)
            .IsRequired(false);

        builder.Property(a => a.Subtraction)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorInsuranceCmts.Subtraction)
            .IsRequired(false);

        builder.Property(a => a.Addition)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorInsuranceCmts.Addition)
            .IsRequired(false);

        builder.HasOne(x => x.TransportationContractor)
       .WithMany(x => x.TransportationContractorInsurances)
       .HasForeignKey(x => x.TransportationContractorId)
       .HasPrincipalKey(x => x.Id)
       .OnDelete(DeleteBehavior.Restrict);
    }
}

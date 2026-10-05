using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class TransportationContractorPersonnelConfiguration : IEntityTypeConfiguration<TransportationContractorPersonnel>
{
    private const string _tableName = "TransportationContractorPersonnels";
    public void Configure(EntityTypeBuilder<TransportationContractorPersonnel> builder)
    {
        builder.MetaActiveConfiguration<TransportationContractorPersonnel, long>(_tableName);

        builder.Property(a => a.LegacyId);

        builder.Property(a => a.CertificateNumber)
            .HasColumnType("nvarchar(50)")
            .IsRequired(false);

        builder.HasOne(x => x.ThirdParty)
            .WithMany()
            .HasForeignKey(x => x.ThirdPartyId);

        builder.HasOne(x => x.TransportationContractor)
            .WithMany(x => x.ContractorPersonnels)
            .HasForeignKey(x => x.TransportationContractorId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.ThirdParty)
            .WithMany()
            .HasForeignKey(x => x.ThirdPartyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

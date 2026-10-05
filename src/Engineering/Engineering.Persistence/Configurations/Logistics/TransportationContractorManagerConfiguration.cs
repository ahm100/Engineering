using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class TransportationContractorManagerConfiguration : IEntityTypeConfiguration<TransportationContractorManager>
{
    private const string _tableName = "TransportationContractorManagers";
    public void Configure(EntityTypeBuilder<TransportationContractorManager> builder)
    {
        builder.MetaActiveConfiguration<TransportationContractorManager, long>(_tableName);

        builder.HasOne(x => x.ThirdParty)
            .WithMany()
            .HasForeignKey(x => x.ThirdPartyId);

        builder.HasOne(x => x.TransportationContractor)
            .WithMany(x => x.ContractorManagers)
            .HasForeignKey(x => x.TransportationContractorId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.ThirdParty)
            .WithMany()
            .HasForeignKey(x => x.ThirdPartyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

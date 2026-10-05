using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class TransportationContractorPersonnelMachineConfiguration : IEntityTypeConfiguration<TransportationContractorPersonnelMachine>
{
    private const string _tableName = "TransportationContractorPersonnelMachines";
    public void Configure(EntityTypeBuilder<TransportationContractorPersonnelMachine> builder)
    {
        builder.MetaActiveConfiguration<TransportationContractorPersonnelMachine, long>(_tableName);

        builder.HasOne(x => x.TransportationContractorPersonnel)
            .WithMany(x => x.ContractorPersonnelMachines)
            .HasForeignKey(x => x.TransportationContractorPersonnelId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TransportationContractorMachine)
            .WithMany(x => x.ContractorPersonnelMachines)
            .HasForeignKey(x => x.TransportationContractorMachineId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

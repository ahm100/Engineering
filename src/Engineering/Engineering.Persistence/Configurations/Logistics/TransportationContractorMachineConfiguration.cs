using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class TransportationContractorMachineConfiguration : IEntityTypeConfiguration<TransportationContractorMachine>
{
    private const string _tableName = "TransportationContractorMachines";
    public void Configure(EntityTypeBuilder<TransportationContractorMachine> builder)
    {
        builder.MetaActiveConfiguration<TransportationContractorMachine, long>(_tableName);

        builder.Property(a => a.Vin)
            .HasComment(TransportationContractorMachineCmts.Vin)
            .HasMaxLength(25);

        builder.Property(a => a.Color)
            .HasComment(TransportationContractorMachineCmts.Color)
            .HasMaxLength(100);

        builder.Property(a => a.NumberPlate)
            .HasComment(TransportationContractorMachineCmts.NumberPlate)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasOne(x => x.MachineType)
            .WithMany(x => x.TransportationContractorMachines)
            .HasForeignKey(x => x.MachineTypeId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TransportationContractor)
            .WithMany(x => x.TransportationContractorMachines)
            .HasForeignKey(x => x.TransportationContractorId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

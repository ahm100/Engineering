namespace Engineering.Domain.Entities.Logistics;

public class TransportationContractorPersonnelMachine : ActivateEntity<TransportationContractorPersonnelMachine, long>
{
    [Description(TransportationContractorMachineCmts.TransportationContractorPersonnel)]
    public long TransportationContractorPersonnelId { get; set; }
    public TransportationContractorPersonnel TransportationContractorPersonnel { get; set; }

    [Description(TransportationContractorMachineCmts.TransportationContractorMachine)]
    public long TransportationContractorMachineId { get; set; }
    public TransportationContractorMachine TransportationContractorMachine { get; set; }

    public TransportationContractorPersonnelMachine(
        TransportationContractorPersonnel? personnel,
        TransportationContractorMachine? machine) : this()
    {
        TransportationContractorPersonnel = Guard.Against.Null(personnel, nameof(personnel));
        TransportationContractorPersonnelId = personnel.Id;
        TransportationContractorMachine = Guard.Against.Null(machine, nameof(machine));
        TransportationContractorMachineId = machine.Id;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private TransportationContractorPersonnelMachine()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}


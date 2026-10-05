using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Domain.Entities.Logistics;

public class TransportationContractorMachine : ActivateEntity<TransportationContractorMachine, long>
{
    [Description(TransportationContractorMachineCmts.NumberPlate)]
    public string NumberPlate { get; set; }

    [Description(TransportationContractorMachineCmts.Vin)]
    public string? Vin { get; set; }

    [Description(TransportationContractorMachineCmts.Color)]
    public string? Color { get; set; }

    [Description(TransportationContractorMachineCmts.MachineType)]
    public long MachineTypeId { get; set; }
    public MachineType MachineType { get; set; }

    [Description(TransportationContractorMachineCmts.TransportationContractor)]
    public long TransportationContractorId { get; set; }
    public TransportationContractor TransportationContractor { get; set; }


    public TransportationContractorMachine(
        string numberPlate,
        string? vin,
        string? color,
        MachineType machineType,
        TransportationContractor transportationContractor
    ) : this()
    {
        SetMachineType(machineType);
        SetTransportationContractor(transportationContractor);
        SetNumberPlate(numberPlate);
        SetVin(vin);
        SetColor(color);
        SetActive();
    }

    public void Update(
        string numberPlate,
        string? vin,
        string? color,
        bool isActive,
        MachineType machineType,
        TransportationContractor transportationContractor
    )
    {
        SetMachineType(machineType);
        SetTransportationContractor(transportationContractor);
        SetNumberPlate(numberPlate);
        SetVin(vin);
        SetColor(color);
        SetActive();
    }

    public void SetMachineType(MachineType machineType)
    {
        MachineType = Guard.Against.Null(machineType, nameof(machineType));
        MachineTypeId = machineType.Id;
    }

    public void SetTransportationContractor(TransportationContractor transportationContractor)
    {
        TransportationContractor = Guard.Against.Null(transportationContractor, nameof(transportationContractor));
        TransportationContractorId = transportationContractor.Id;
    }

    public void SetNumberPlate(string numberPlate)
    {
        NumberPlate = Guard.Against.NullOrEmpty(numberPlate, nameof(numberPlate));
    }

    public void SetVin(string? vin)
    {
        Vin = vin;
    }

    public void SetColor(string? color)
    {
        Color = color;
    }

    public void SetPersonnel(List<TransportationContractorPersonnel>? contractorPersonnels)
    {
        _contractorPersonnelMachines.ForEach(contractorPersonnelMachine => contractorPersonnelMachine.SoftDelete());

        if (contractorPersonnels is not null && contractorPersonnels.Count > 0)
            foreach (var item in contractorPersonnels)
                _contractorPersonnelMachines.Add(new TransportationContractorPersonnelMachine(item, this));
    }
    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(TransportationContractorCmts.TransportationContractorPersonnelMachine)]
    private List<TransportationContractorPersonnelMachine> _contractorPersonnelMachines;
    public IReadOnlyList<TransportationContractorPersonnelMachine> ContractorPersonnelMachines => _contractorPersonnelMachines;
    private TransportationContractorMachine()
    {
        _contractorPersonnelMachines = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}

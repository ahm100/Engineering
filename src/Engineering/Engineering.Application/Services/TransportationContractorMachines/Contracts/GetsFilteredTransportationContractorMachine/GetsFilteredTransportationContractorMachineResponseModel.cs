using Engineering.Application.Extensions.PlateHelper;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;

namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;

public record GetsFilteredTransportationContractorMachineResponseModel
{
    public long Id { get; set; }
    public long? ContractorId { get; set; }
    public bool? IsIndivisual { get; set; }
    public string? IsIndivisualTitle => IsIndivisual == true ? "حقیقی" : "حقوقی";
    public long? ThirdPartyId { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? FullName => FirstName + " " + LastName;
    public string? PhoneNumber { get; set; }
    public string? IdentityNo { get; set; }
    public long? LegalId { get; set; }
    public string? CompanyName { get; set; }
    public string? RegisterationNo { get; set; }
    public long? MachineTypeId { get; set; }
    public string? MachineTypeName { get; set; }
    public string? MachineTypeCode { get; set; }
    public string? NumberPlate { get; set; }
    public MachineTypeNumberPlatesModel? NumberPlateModel => NumberPlate.ToPlateModel<MachineTypeNumberPlatesModel>();
    public string? Vin { get; set; }
    public string? Color { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public List<GetContractorPersonnelMachineResponseModel>? Personnels { get; set; }
}
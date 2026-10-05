using Engineering.Application.Extensions.PlateHelper;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;

namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;

public record GetsActiveTransportationContractorPersonnelResponseModel
{
    public long Id { get; set; }
    public long? ContractorId { get; set; }
    public bool? IsIndivisual { get; set; }
    public string? IsIndivisualTitle => IsIndivisual == true ? "حقیقی" : "حقوقی";
    public long? ThirdPartyId { get; set; }
    public long? LegalId { get; set; }
    public string? CompanyName { get; set; }
    public string? RegisterationNo { get; set; }
    public long? PersonnelThirdPartyId { get; set; }
    public string? PersonnelFirstName { get; set; } = string.Empty;
    public string? PersonnelLastName { get; set; } = string.Empty;
    public string? PersonnelFullName => PersonnelFirstName + " " + PersonnelLastName;
    public string? PersonnelPhoneNumber { get; set; }
    public string? PersonnelIdentityNo { get; set; }
    public string? PersonnelDescription { get; set; }
    public long? PersonnelLegacyId { get; set; }
    public long? PersonnelAddressId { get; set; }
    public long? PersonnelCityId { get; set; }
    public string? PersonnelCity { get; set; }
    public string? PersonnelAddress { get; set; }
    public string? PersonnelTitle { get; set; }
    public string? CertificateNumber { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public List<GetContractorMachinePersonnelResponseModel>? Machines { get; set; }
}

public record GetContractorMachinePersonnelResponseModel
{
    public long? Id { get; set; }
    public long? ContractorMachineId { get; set; }
    public long? MachineTypeId { get; set; }
    public string? MachineTypeName { get; set; }
    public string? MachineTypeCode { get; set; }
    public string? Vin { get; set; }
    public string? Color { get; set; }
    public string? NumberPlate { get; set; }
    public MachineTypeNumberPlatesModel? NumberPlateModel => NumberPlate.ToPlateModel<MachineTypeNumberPlatesModel>();
}

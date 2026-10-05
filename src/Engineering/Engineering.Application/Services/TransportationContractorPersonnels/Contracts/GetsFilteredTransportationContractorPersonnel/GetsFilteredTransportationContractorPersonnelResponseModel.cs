using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;

namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;

public record GetsFilteredTransportationContractorPersonnelResponseModel
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
    public string? PersonnelMachines => Machines?.Any() == true ?
        string.Join(" - ", Machines.Where(x => x != null).Select(x => $"{x.MachineTypeName}: {x.NumberPlate}"))
        : null;
    public List<GetContractorMachinePersonnelResponseModel>? Machines { get; set; }
}
namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetContractorEmployeesByContractorId;

public record GetContractorEmployeesByContractorIdModel(
    long Id,
    long ThirdPartyId,
    string? FullName,
    string? OrganizationCode,
    bool IsActive,
    long? CompanyId,
    string? CompanyNameFa);


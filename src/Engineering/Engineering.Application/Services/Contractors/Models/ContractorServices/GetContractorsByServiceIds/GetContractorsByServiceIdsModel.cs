namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorsByServiceIds;

public record GetContractorsByServiceIdsModel(
    long Id,
    string? FullName,
    string? NickName,
    string? OrganizationCode,
    string? DefaultPhoneNo,
    bool? IsActive
);

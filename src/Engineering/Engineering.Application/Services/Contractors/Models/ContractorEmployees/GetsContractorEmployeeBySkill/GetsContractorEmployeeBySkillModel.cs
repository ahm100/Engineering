namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetsContractorEmployeeBySkill;

public record GetsContractorEmployeeBySkillModel(
    long Id,
    long ThirdPartyId,
    string? FullName,
    string? Nickname,
    string? OrganizationCode,
    bool IsActive);


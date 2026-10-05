namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetsContractorEmployeeBySkill;

public record GetsContractorEmployeeBySkillRequest(
    long ContractorId,
    long SkillId,
    //string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;

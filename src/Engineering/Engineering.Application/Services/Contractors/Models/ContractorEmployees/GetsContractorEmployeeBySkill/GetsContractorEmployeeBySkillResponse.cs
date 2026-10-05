namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetsContractorEmployeeBySkill;

public record GetsContractorEmployeeBySkillResponse(
    List<GetsContractorEmployeeBySkillModel> Data,
    int RowCount
    );


namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.CreateContractorEmployee;

public record CreateContractorEmployeeRequestModel(long? Id,
                                                   long EmployeeId,
                                                   bool IsDeleted,
                                                   bool IsActive,
                                                   bool IsConfirm);
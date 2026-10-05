using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationByParams;

public record GetProjectOperationByParamsResponse(
    long Id,
    long OperationInfoId,
    string OperationInfoName,
    string OperationInfoCode,
    long? ProjectId,
    string? ProjectName,
    string? ProjectCode,
    long? EmployerContractId,
    string? ContractCode,
    decimal Workload,
    decimal TolerancePercentage,
    decimal? Price,
    int? Priority,
    ProjectOperationStatus Status,
    string StatusDescription
    );

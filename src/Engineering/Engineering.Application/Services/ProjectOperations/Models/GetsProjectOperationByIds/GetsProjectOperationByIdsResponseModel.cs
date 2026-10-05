
namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationByIds;

public record GetsProjectOperationByIdsResponseModel(
    long Id,
    string OperationInfoCode,
    string OperationInfoName,
    long? CompanyId,
    string? CompanyNameFa
    );

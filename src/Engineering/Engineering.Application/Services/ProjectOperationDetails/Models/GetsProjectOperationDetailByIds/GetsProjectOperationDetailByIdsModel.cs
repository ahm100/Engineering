
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByIds;

public record GetsProjectOperationDetailByIdsModel(
    long Id,
    long ProjectOperationId,
    string OperationInfoName,
    string OperationInfoCode,
    decimal Workload,
    long OperationLocationId,
    string PublicName,
    string PublicCode,
    decimal FinalAmount,
    long? CompanyId,
    string? CompanyNameFa
    );



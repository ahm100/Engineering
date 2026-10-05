
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsMinimalByProjectOperationIds;

public record GetsMinimalByProjectOperationIdsModel(
    long Id,
    string? Code,
    long ProjectOperationId,
    string OperationInfoName,
    string OperationInfoCode,
    long MeasurementId,
    string? MeasurementName,
    decimal Workload,
    long OperationLocationId,
    string? PrivateName,
    string? PrivateCode,
    string? PublicName,
    string? PublicCode,
    decimal FinalAmount
    );



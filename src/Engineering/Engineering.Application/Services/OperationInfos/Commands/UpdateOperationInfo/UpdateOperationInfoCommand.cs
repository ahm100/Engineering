using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.UpdateOperationInfo;

public record UpdateOperationInfoCommand(
    OperationInfo OperationInfo,
    string OperationInfoCode,
    string OperationInfoName,
    string? OperationInfoLatinName,
    int? Priority,
    long UnitOfMeasurementId,
    decimal? BasePrice,
    bool IsActive,
    bool HasChanged,
    long? CompanyId
    ) : ICommand<OperationInfo>;
namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByName;

public record GetOperationInfoByNameResponse(
    long Id,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    long UnitOfMeasurementId,
    bool IsActive);

namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByCode;

public record GetOperationInfoByCodeResponse(
    long Id,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    long UnitOfMeasurementId,
    bool IsActive
    );

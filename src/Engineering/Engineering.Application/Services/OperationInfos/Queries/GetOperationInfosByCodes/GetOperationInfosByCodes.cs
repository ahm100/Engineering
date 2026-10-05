namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfosByCodes;
public record OperationInfoBulkDto(
    long Id,
    string OperationInfoCode,
    long UnitOfMeasurementId
);

public record GetOperationInfosByCodesQuery(
    List<string> OperationInfoCodes,
    long? CompanyId
) : IQuery<List<OperationInfoBulkDto>>;
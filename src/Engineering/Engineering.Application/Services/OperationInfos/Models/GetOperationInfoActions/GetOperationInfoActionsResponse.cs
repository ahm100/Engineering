namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoAction;

public record GetOperationInfoActionsResponse(
    List<GetOperationInfoActionModel> Data,
    int RowCount);
public record GetOperationInfoActionModel(
    long Id,
    long ActionId,
    string ActionName,
    string ActionCode,
    decimal? Price
    );
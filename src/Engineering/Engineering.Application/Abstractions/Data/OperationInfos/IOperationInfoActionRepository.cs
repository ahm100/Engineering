using Engineering.Application.Services.OperationInfos.Models.GetOIActionByOperationInfoId;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoAction;
using Engineering.Domain.Entities.OperationInfos;
namespace Engineering.Application.Abstractions.Data.OperationInfos;

public interface IOperationInfoActionRepository : IBaseRepository<OperationInfoAction>
{
    Task<(List<GetOperationInfoActionModel> Data, int RowCount)> GetsOperationInfoActions(
    long oInfoId,
    string? FilterData,
    int pageIndex,
    int pageSize,
    CT ct);

    Task<List<OperationInfoAction>> GetOperationInfoActions(
    List<long> oInfoIds,
    CT ct);

    Task<List<GetOIActionByOperationInfoIdModel>> GetOIActionByOperationInfoId(
    long oInfoId,
    CT ct);

    Task<List<OperationInfoAction>> GetOIActionByOIId(
    long oInfoId,
    CT ct);

    Task<OperationInfoAction> GetOperationInfoActionByActionId(
    long oInfoId,
    long actionId,
    CT ct);

    Task<List<OperationInfoAction>> GetOperationInfoActionByActionId(
    List<long> oInfoActionIds,
    CT ct);

    Task<List<OperationInfoAction>> GetOperationInfoActionsWithActionIds(
    long oInfoId,
    List<long> actionsIds,
    CT ct);
}

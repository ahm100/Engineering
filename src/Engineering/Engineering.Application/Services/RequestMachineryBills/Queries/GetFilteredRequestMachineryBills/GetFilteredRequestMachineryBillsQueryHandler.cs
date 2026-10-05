using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Queries.GetFilteredRequestMachineryBills;

public class GetFilteredRequestMachineryBillsQueryHandler : IQueryHandler<GetFilteredRequestMachineryBillsQuery, DataResult<List<RequestMachineryBill>>>
{
    private readonly ILogger<GetFilteredRequestMachineryBillsQueryHandler> _logger;
    private readonly IRequestMachineryBillRepository _repository;

    public GetFilteredRequestMachineryBillsQueryHandler(ILogger<GetFilteredRequestMachineryBillsQueryHandler> logger, IRequestMachineryBillRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestMachineryBill>>?>> Handle(GetFilteredRequestMachineryBillsQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetFilteredAsync(
                    request.Ids,
                    request.RequestMachineryId,
                    request.CostCenterId,
                    request.ProjectId,
                    request.ContractorIds,
                    request.ProjectOperationIds,
                    request.MachineriesGroupId,
                    request.MachineryId,
                    request.FromDate,
                    request.ToDate,
                    request.CreatorId,
                    request.BillNumber,
                    request.FilterData,
                    request.CompanyId,
                    request.OrderBy,
                    request.PageIndex,
                    request.PageSize,
                    ct);

            return entities.Data.Any()
                    ? new DataResult<List<RequestMachineryBill>>
                    {
                        Data = entities.Data,
                        RowCount = entities.RowCount
                    } : Result.Failure<DataResult<List<RequestMachineryBill>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestMachineryBill>>>(SharedErrors.UnknownError);
        }
    }
}

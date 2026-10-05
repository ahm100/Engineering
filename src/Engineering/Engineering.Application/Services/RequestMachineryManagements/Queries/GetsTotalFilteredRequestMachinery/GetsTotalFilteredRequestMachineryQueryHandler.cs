using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetsTotalFilteredRequestMachinery;

public class GetsTotalFilteredRequestMachineryQueryHandler : IQueryHandler<GetsTotalFilteredRequestMachineryQuery, DataResult<List<RequestMachinery>>>
{
    private readonly ILogger<GetsTotalFilteredRequestMachineryQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public GetsTotalFilteredRequestMachineryQueryHandler(ILogger<GetsTotalFilteredRequestMachineryQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestMachinery>>?>> Handle(GetsTotalFilteredRequestMachineryQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetTotalFiltered(
                request.Ids,
                request.CostCenterId,
                request.ProjectId,
                request.ContractorIds,
                request.ProjectOperationIds,
                request.OperationInfoIds,
                request.MachineriesGroupId,
                request.MachineryId,
                request.Status,
                request.PaymentType,
                request.FromDate,
                request.ToDate,
                request.ConfirmedFromDate,
                request.ConfirmedToDate,
                request.CreatorId,
                request.OperatorAppoinmentUserId,
                request.RequestNumber,
                request.DriverName,
                request.FilterData,
                request.CompanyId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return entities.Data.Any()
                    ? new DataResult<List<RequestMachinery>>
                    {
                        Data = entities.Data,
                        RowCount = entities.RowCount
                    } : Result.Failure<DataResult<List<RequestMachinery>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestMachinery>>>(SharedErrors.UnknownError);
        }
    }
}

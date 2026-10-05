using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetManagerConfirmMachineryReports;

public class GetManagerConfirmMachineryReportsQueryHandler : IQueryHandler<GetManagerConfirmMachineryReportsQuery, DataResult<List<RequestMachinery>>>
{
    private readonly ILogger<GetManagerConfirmMachineryReportsQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public GetManagerConfirmMachineryReportsQueryHandler(ILogger<GetManagerConfirmMachineryReportsQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestMachinery>>?>> Handle(GetManagerConfirmMachineryReportsQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetManagerConfirmedMachineryReports(
                                                request.Ids,
                                                request.CostCenterIds,
                                                request.ProjectIds,
                                                request.ContractorId,
                                                request.ProjectOperationIds,
                                                request.ProjectOperationDetailIds,
                                                request.MachineryIds,
                                                request.Unit,
                                                request.Statuses,
                                                request.StartDate,
                                                request.EndDate,
                                                request.ConfirmedFromDate,
                                                request.ConfirmedToDate,
                                                request.FilterData,
                                                request.OwnCompany,
                                                request.OrderBy,
                                                request.CompanyId,
                                                request.PageIndex,
                                                request.PageSize, ct);

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

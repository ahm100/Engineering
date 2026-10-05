using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetOnProjectMachineryReports;

public class GetOnProjectMachineryReportsQueryHandler : IQueryHandler<GetOnProjectMachineryReportsQuery, DataResult<List<RequestMachinery>>>
{
    private readonly ILogger<GetOnProjectMachineryReportsQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public GetOnProjectMachineryReportsQueryHandler(ILogger<GetOnProjectMachineryReportsQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestMachinery>>?>> Handle(GetOnProjectMachineryReportsQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetOnProjectMachineryReports(
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

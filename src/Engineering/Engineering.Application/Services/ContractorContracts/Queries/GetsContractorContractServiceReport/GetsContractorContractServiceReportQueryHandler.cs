using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractServiceReport;

public class GetsContractorContractServiceReportQueryHandler : IQueryHandler<GetsContractorContractServiceReportQuery, DataResult<List<GetsContractorContractServiceReportModel>>>
{
    private readonly ILogger<GetsContractorContractServiceReportQueryHandler> _logger;
    private readonly IContractorContractDetailServiceRepository _repository;

    public GetsContractorContractServiceReportQueryHandler(ILogger<GetsContractorContractServiceReportQueryHandler> logger,
                                                       IContractorContractDetailServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsContractorContractServiceReportModel>>?>> Handle(GetsContractorContractServiceReportQuery request, CT ct)
    {
        try
        {
            var contractType = (ContractorContractType?)request.ContractTypeId;
            var result = await _repository.GetsContractorContractServiceReport(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ContractorIds,
                request.ServiceInfoIds,
                request.MeasurUnitIds,
                contractType,
                request.StartDate,
                request.EndDate,
                request.FromDate,
                request.ToDate,
                request.FromCreated,
                request.ToCreated,
                request.Status,
                request.ContractStatus,
                request.FilterData,
                request.FilterDescription,
                request.FilterServiceInfo,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsContractorContractServiceReportModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsContractorContractServiceReportModel>>>(ContractorContractDetailErrors.ReportNotfound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsContractorContractServiceReportModel>>>(SharedErrors.UnknownError);
        }
    }
}

using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsContractorProjectOperationDetailDetailReports;

public class GetsContractorProjectOperationDetailDetailReportsQueryHandler : IQueryHandler<GetsContractorProjectOperationDetailDetailReportsQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsContractorProjectOperationDetailDetailReportsQueryHandler> _logger;

    public GetsContractorProjectOperationDetailDetailReportsQueryHandler(ILogger<GetsContractorProjectOperationDetailDetailReportsQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(GetsContractorProjectOperationDetailDetailReportsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorProjectOperationDetailDetailReports(
                request.Ids,
                request.ContractorContractId,
                request.ContractorId,
                request.FromDate,
                request.ToDate,
                request.CompanyId,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetail>>>(ProjectOperationDetailErrors.ProjectOperationDetailWithFilterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}
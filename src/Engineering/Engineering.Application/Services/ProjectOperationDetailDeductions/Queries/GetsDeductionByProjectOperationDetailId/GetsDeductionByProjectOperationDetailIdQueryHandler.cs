using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailDeduction = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailDeduction;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetsDeductionByProjectOperationDetailId;

public class GetsDeductionByProjectOperationDetailIdQueryHandler : IQueryHandler<GetsDeductionByProjectOperationDetailIdQuery, DataResult<List<ProjectOperationDetailDeduction>>>
{
    private readonly IProjectOperationDetailDeductionRepository _repository;
    private readonly ILogger<GetsDeductionByProjectOperationDetailIdQueryHandler> _logger;

    public GetsDeductionByProjectOperationDetailIdQueryHandler(ILogger<GetsDeductionByProjectOperationDetailIdQueryHandler> logger, IProjectOperationDetailDeductionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailDeduction>>?>> Handle(GetsDeductionByProjectOperationDetailIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationDetailId(request.ProjectOperationDetailId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetailDeduction>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetailDeduction>>>(ProjectOperationDetailDeductionErrors.DeductionsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetailDeduction>>>(SharedErrors.UnknownError);
        }
    }
}
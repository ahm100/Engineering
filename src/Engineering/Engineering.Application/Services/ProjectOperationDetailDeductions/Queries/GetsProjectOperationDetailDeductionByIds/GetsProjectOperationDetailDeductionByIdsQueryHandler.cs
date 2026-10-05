using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetsProjectOperationDetailDeductionByIds;

public class GetsProjectOperationDetailDeductionByIdsQueryHandler : IQueryHandler<GetsProjectOperationDetailDeductionByIdsQuery, List<ProjectOperationDetailDeduction>>
{
    private readonly IProjectOperationDetailDeductionRepository _repository;
    private readonly ILogger<GetsProjectOperationDetailDeductionByIdsQuery> _logger;

    public GetsProjectOperationDetailDeductionByIdsQueryHandler(ILogger<GetsProjectOperationDetailDeductionByIdsQuery> logger, IProjectOperationDetailDeductionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ProjectOperationDetailDeduction>?>> Handle(GetsProjectOperationDetailDeductionByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationDetailDeductionByIds(request.Ids, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperationDetailDeduction>>(SharedErrors.UnknownError);
        }
    }
}
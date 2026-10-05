using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetDeductionAmountByProjectOperationDetailId;

public class GetDeductionAmountByProjectOperationDetailIdQueryHandler : IQueryHandler<GetDeductionAmountByProjectOperationDetailIdQuery, List<decimal>>
{
    private readonly IProjectOperationDetailDeductionRepository _repository;
    private readonly ILogger<GetDeductionAmountByProjectOperationDetailIdQueryHandler> _logger;

    public GetDeductionAmountByProjectOperationDetailIdQueryHandler(ILogger<GetDeductionAmountByProjectOperationDetailIdQueryHandler> logger, IProjectOperationDetailDeductionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<decimal>?>> Handle(GetDeductionAmountByProjectOperationDetailIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetDeductionAmountsByDetailId(request.ProjectOperationDetailId, ct);

            return result ?? Result.Failure<List<decimal>>(ProjectOperationDetailDeductionErrors.DeductionWithDetailIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<decimal>>(SharedErrors.UnknownError);
        }
    }
}
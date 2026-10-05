using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetProjectOperationDetailDeductionById;

public class GetProjectOperationDetailDeductionByIdQueryHandler : IQueryHandler<GetProjectOperationDetailDeductionByIdQuery, ProjectOperationDetailDeduction?>
{
    private readonly ILogger<GetProjectOperationDetailDeductionByIdQueryHandler> _logger;
    private readonly IProjectOperationDetailDeductionRepository _repository;

    public GetProjectOperationDetailDeductionByIdQueryHandler(ILogger<GetProjectOperationDetailDeductionByIdQueryHandler> logger, IProjectOperationDetailDeductionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailDeduction?>> Handle(GetProjectOperationDetailDeductionByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);

            return entity ?? Result.Failure<ProjectOperationDetailDeduction>(ProjectOperationDetailDeductionErrors.DeductionWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailDeduction>(SharedErrors.UnknownError);
        }
    }
}
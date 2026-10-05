using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetSchedulingProjectOperationDetails;

public class GetSchedulingProjectOperationDetailsQueryHandler : IQueryHandler<GetSchedulingProjectOperationDetailsQuery, List<ProjectOperationDetail>>
{
    private readonly ILogger<GetSchedulingProjectOperationDetailsQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetSchedulingProjectOperationDetailsQueryHandler(ILogger<GetSchedulingProjectOperationDetailsQueryHandler> logger,
                                                            IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ProjectOperationDetail>?>> Handle(GetSchedulingProjectOperationDetailsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetForSchecdulingAsync(request.CostCenterId,
                                                                  request.ProjectId,
                                                                  request.ProjectOperationId,
                                                                  ct);

            return result.Any() ? result : Result.Failure<List<ProjectOperationDetail>>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperationDetail>>(SharedErrors.UnknownError);
        }
    }
}

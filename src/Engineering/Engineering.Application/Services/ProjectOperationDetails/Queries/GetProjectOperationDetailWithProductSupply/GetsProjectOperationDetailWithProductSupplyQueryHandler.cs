using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailWithProductSupply;

public class GetsProjectOperationDetailWithProductSupplyQueryHandler : IQueryHandler<GetsProjectOperationDetailWithProductSupplyQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetsProjectOperationDetailWithProductSupplyQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetsProjectOperationDetailWithProductSupplyQueryHandler(ILogger<GetsProjectOperationDetailWithProductSupplyQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetsProjectOperationDetailWithProductSupplyQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationDetailWithProductSupply(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}

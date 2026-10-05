using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.Projects.Queries.FindLastUnitOrg;

public class FindLastUnitOrgQueryHandler : IQueryHandler<FindLastUnitOrgQuery, long>
{
    private readonly ILogger<FindLastUnitOrgQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public FindLastUnitOrgQueryHandler(
        ILogger<FindLastUnitOrgQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<long>> Handle(FindLastUnitOrgQuery request, CT ct)
    {
        try
        {
            var newCode = await _repository.FindLastUnitOrg(request.OrganizationId, ct);
            return newCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<long>(SharedErrors.UnknownError);
        }
    }
}
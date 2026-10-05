using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.ProjectTypes.Queries.FindProjectTypeByNamesOrCodes;

public class FindProjectTypeByNamesOrCodesQueryHandler : IQueryHandler<FindProjectTypeByNamesOrCodesQuery, bool>
{
    private readonly ILogger<FindProjectTypeByNamesOrCodesQueryHandler> _logger;
    private readonly IProjectTypeRepository _repository;

    public FindProjectTypeByNamesOrCodesQueryHandler(ILogger<FindProjectTypeByNamesOrCodesQueryHandler> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindProjectTypeByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindProjectTypeByNamesOrCodes(request.Names, request.Codes, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}

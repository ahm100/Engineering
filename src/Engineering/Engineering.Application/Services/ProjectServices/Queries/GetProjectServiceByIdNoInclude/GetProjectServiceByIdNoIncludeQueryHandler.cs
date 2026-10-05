using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;

namespace Engineering.Application.Services.ProjectServices.Queries.GetProjectServiceByIdNoInclude;

public class GetProjectServiceByIdNoIncludeQueryHandler : IQueryHandler<GetProjectServiceByIdNoIncludeQuery, GetProjectServiceByIdResponse>
{
    private readonly ILogger<GetProjectServiceByIdNoIncludeQueryHandler> _logger;
    private readonly IProjectServiceRepository _repository;

    public GetProjectServiceByIdNoIncludeQueryHandler(ILogger<GetProjectServiceByIdNoIncludeQueryHandler> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetProjectServiceByIdResponse?>> Handle(GetProjectServiceByIdNoIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectServiceByIdNoInclude(
                request.Id,
                ct);

            return result ?? Result.Failure<GetProjectServiceByIdResponse>(ProjectErrors.ProjectWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectServiceByIdResponse>(SharedErrors.UnknownError);
        }
    }
}
using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.Projects.Queries.GetProjectModelById;

public class GetProjectModelByIdQueryHandler : IQueryHandler<GetProjectModelByIdQuery, GetProjectModelByIdReponse>
{
    private readonly ILogger<GetProjectModelByIdQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public GetProjectModelByIdQueryHandler(
        ILogger<GetProjectModelByIdQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetProjectModelByIdReponse?>> Handle(GetProjectModelByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectModelById(request.Id, ct);

            return result ?? Result.Failure<GetProjectModelByIdReponse>(ProjectErrors.ProjectWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectModelByIdReponse>(SharedErrors.UnknownError);
        }
    }
}
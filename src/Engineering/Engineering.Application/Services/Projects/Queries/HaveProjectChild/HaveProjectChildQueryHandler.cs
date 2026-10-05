using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.HaveProjectChild;

public class HaveProjectChildQueryHandler : IQueryHandler<HaveProjectChildQuery, Project>
{
    private readonly ILogger<HaveProjectChildQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public HaveProjectChildQueryHandler(ILogger<HaveProjectChildQueryHandler> logger, IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(HaveProjectChildQuery request, CT ct)
    {
        try
        {
            var result = await _repository.HaveProjectChild(request.Id, ct);

            return result ?? Result.Failure<Project>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}
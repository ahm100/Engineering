using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Models.GetUnAssignedProjects;

namespace Engineering.Application.Services.Projects.Queries.GetUnAssignedProjects;

public class GetUnAssignedProjectsQueryHandler : IQueryHandler<GetUnAssignedProjectsQuery, GetUnAssignedProjectsResponse?>
{
    private readonly ILogger<GetUnAssignedProjectsQueryHandler> _logger;
    private readonly IProjectRepository _repository;
    private readonly IViewCityRepository _cityRepo;

    public GetUnAssignedProjectsQueryHandler(
        ILogger<GetUnAssignedProjectsQueryHandler> logger,
        IProjectRepository repository,
        IViewCityRepository cityRepo)
    {
        _logger = logger;
        _repository = repository;
        _cityRepo = cityRepo;
    }

    public async Task<Result<GetUnAssignedProjectsResponse?>> Handle(GetUnAssignedProjectsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetUnAssignedProjects(request.FilterData,
                request.CityId,
                request.PageIndex,
                request.PageSize, ct);

            if (result.Data is null || result.RowCount < 1)
                return Result.Failure<GetUnAssignedProjectsResponse>(ProjectErrors.ProjectWithIdNotFound);

            var cityIds = result.Data.NullListed(x => x.CityId);
            var cities = await _cityRepo.GetByIds(cityIds, ct);

            foreach (var item in result.Data)
                if (item.CityId is not null)
                    item.City = cities.FirstOrDefault(x => x.Id == item.CityId.Value)?.Name;

            return new GetUnAssignedProjectsResponse(result.Data, result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetUnAssignedProjectsResponse>(SharedErrors.UnknownError);
        }
    }
}
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Abstractions.Data.WbsTemplates;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateForProject;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.WbsTemplates.Queries.GetWbsTemplateForProject;

public class GetWbsTemplateForProjectQueryHandler : IQueryHandler<GetWbsTemplateForProjectQuery, GetWbsTemplateForProjectResponse?>
{
    private readonly ILogger<GetWbsTemplateForProjectQueryHandler> _logger;
    private readonly IWbsTemplateRepository _repo;
    private readonly IProjectWbsRepository _projectWbsrepo;

    public GetWbsTemplateForProjectQueryHandler(ILogger<GetWbsTemplateForProjectQueryHandler> logger,
        IWbsTemplateRepository repo,
        IProjectWbsRepository projectWbsrepo)
    {
        _logger = logger;
        _repo = repo;
        _projectWbsrepo = projectWbsrepo;
    }

    public async Task<Result<GetWbsTemplateForProjectResponse?>> Handle(GetWbsTemplateForProjectQuery request, CT ct)
    {
        try
        {
            var ids = await _projectWbsrepo.GetWbsIdsToNotShow(request.ProjectId, request.ParentId, ct);
            var result = await _repo.GetWbsTemplateForProject(request.FilterData,
                ids,
                request.PageIndex,
                request.PageSize, ct);

            if (result.Data is null || result.RowCount < 1)
                return Result.Failure<GetWbsTemplateForProjectResponse?>(WbsTemplateErrors.WbsTemplateWithFilterNotFound);

            return new GetWbsTemplateForProjectResponse(result.Data, result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetWbsTemplateForProjectResponse?>(SharedErrors.UnknownError);
        }
    }
}
using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Models.GetProjectForPdf;

namespace Engineering.Application.Services.Projects.Queries.GetProjectForPdf;

public class GetProjectForPdfQueryHandler : IQueryHandler<GetProjectForPdfQuery, GetProjectForPdfResponse>
{
    private readonly ILogger<GetProjectForPdfQueryHandler> _logger;
    private readonly IProjectRepository _repo;
    private readonly IViewThirdPartyRepository _thridPartyRepo;

    public GetProjectForPdfQueryHandler(
        ILogger<GetProjectForPdfQueryHandler> logger,
        IProjectRepository repo,
        IViewThirdPartyRepository thridPartyRepo)
    {
        _logger = logger;
        _repo = repo;
        _thridPartyRepo = thridPartyRepo;
    }

    public async Task<Result<GetProjectForPdfResponse?>> Handle(GetProjectForPdfQuery request, CT ct)
    {
        try
        {
            var result = await _repo.GetProjectForPdf(
                request.Id,
                ct);

            var userIds = new[]
            {
                result.ProjectManagerId,
                result.AdvisorId,
                result.EmployerId
            }
            .NullListed(x => x);

            var users = await _thridPartyRepo.GetByIds(userIds, ct);
            if (users is not null && users.Count > 0)
            {
                var emp = users.FirstOrDefault(x => x.Id == result.EmployerId);
                var advisor = users.FirstOrDefault(x => x.Id == result.AdvisorId);
                var projectManager = users.FirstOrDefault(x => x.Id == result.ProjectManagerId);

                result.EmployerName = emp?.FirstName + " " + emp?.LastName;
                result.AdvisorName = advisor?.FirstName + " " + advisor?.LastName;
                result.ProjectManagerName = projectManager?.FirstName + " " + projectManager?.LastName;
            }

            return result ?? Result.Failure<GetProjectForPdfResponse>(ProjectErrors.ProjectWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectForPdfResponse>(SharedErrors.UnknownError);
        }
    }
}
using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectRisks.Contracts.GetFltrProjectRisk;
using Engineering.Domain.Errors.Projects;
namespace Engineering.Application.Services.ProjectRisks.Queries.GetFltrProjectRisk;

public class GetFltrProjectRiskQueryHandler : IQueryHandler<GetFltrProjectRiskQuery, GetFltrProjectRiskResponse?>
{
    private readonly ILogger<GetFltrProjectRiskQueryHandler> _logger;
    private readonly IProjectRiskRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetFltrProjectRiskQueryHandler(
        ILogger<GetFltrProjectRiskQueryHandler> logger,
        IProjectRiskRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetFltrProjectRiskResponse?>> Handle(GetFltrProjectRiskQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFltrProjectRisk(request.ProjectIds,
                request.FilterData,
                request.RiskProbability,
                request.RiskImpact,
                request.RiskStatus,
                request.PageIndex,
                request.PageSize, ct);

            if (result.Data is not null && result.Data.Any())
            {
                var creatorIds = result.Data?.Select(x => x.CreatorId);
                if (creatorIds is not null && creatorIds.Count() > 0)
                {
                    var creators = await _thirdPartyRepo.GetByUserIds(creatorIds.NullListed(x => x), ct);
                    foreach (var item in result.Data!)
                    {
                        var creator = creators.FirstOrDefault(x => x.UserId == item.CreatorId);
                        item.Creator = creator?.FirstName + " " + creator?.LastName;
                    }
                }
            }

            return result.Data.Any() ?
                new GetFltrProjectRiskResponse(result.Data, result.RowCount) :
                Result.Failure<GetFltrProjectRiskResponse?>(ProjectRiskErrors.ProjectRiskWithFilterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrProjectRiskResponse>(SharedErrors.UnknownError);
        }
    }
}
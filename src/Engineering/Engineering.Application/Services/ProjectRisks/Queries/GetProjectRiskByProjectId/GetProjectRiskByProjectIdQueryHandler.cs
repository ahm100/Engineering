using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskByProjectId;
using Engineering.Domain.Errors.Projects;

namespace Engineering.Application.Services.ProjectRisks.Queries.GetProjectRiskByProjectId;

public class GetProjectRiskByProjectIdQueryHandler : IQueryHandler<GetProjectRiskByProjectIdQuery, GetProjectRiskByProjectIdResponse?>
{
    private readonly ILogger<GetProjectRiskByProjectIdQueryHandler> _logger;
    private readonly IProjectRiskRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetProjectRiskByProjectIdQueryHandler(
        ILogger<GetProjectRiskByProjectIdQueryHandler> logger,
        IProjectRiskRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetProjectRiskByProjectIdResponse?>> Handle(GetProjectRiskByProjectIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectRiskByProjectId(request.ProjectId,
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
                new GetProjectRiskByProjectIdResponse(result.Data, result.RowCount) :
                Result.Failure<GetProjectRiskByProjectIdResponse?>(ProjectRiskErrors.ProjectRiskWithProjectIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectRiskByProjectIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
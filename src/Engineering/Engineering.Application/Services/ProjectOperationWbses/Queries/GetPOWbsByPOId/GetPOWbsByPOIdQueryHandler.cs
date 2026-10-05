using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;
using Engineering.Domain.Errors.Projects;

namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetPOWbsByPOId;

public class GetPOWbsByPOIdQueryHandler : IQueryHandler<GetPOWbsByPOIdQuery, GetPOWbsByPOIdResponse?>
{
    private readonly ILogger<GetPOWbsByPOIdQueryHandler> _logger;
    private readonly IProjectOperationWbsRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetPOWbsByPOIdQueryHandler(
        ILogger<GetPOWbsByPOIdQueryHandler> logger,
        IProjectOperationWbsRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetPOWbsByPOIdResponse?>> Handle(GetPOWbsByPOIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetPOWbsByPOId(request.ProjectOperationId,
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
                new GetPOWbsByPOIdResponse(result.Data, result.RowCount) :
                Result.Failure<GetPOWbsByPOIdResponse?>(ProjectRiskErrors.ProjectRiskWithProjectIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetPOWbsByPOIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
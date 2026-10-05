using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByProjectWbsId;
using Engineering.Domain.Errors.Projects;

namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetPOWbsByProjectWbsId;

public class GetPOWbsByProjectWbsIdQueryHandler : IQueryHandler<GetPOWbsByProjectWbsIdQuery, GetPOWbsByProjectWbsIdResponse?>
{
    private readonly ILogger<GetPOWbsByProjectWbsIdQueryHandler> _logger;
    private readonly IProjectOperationWbsRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetPOWbsByProjectWbsIdQueryHandler(
        ILogger<GetPOWbsByProjectWbsIdQueryHandler> logger,
        IProjectOperationWbsRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetPOWbsByProjectWbsIdResponse?>> Handle(GetPOWbsByProjectWbsIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetPOWbsByProjectWbsId(request.ProjectWbsId,
                request.FilterData,
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
                new GetPOWbsByProjectWbsIdResponse(result.Data, result.RowCount) :
                Result.Failure<GetPOWbsByProjectWbsIdResponse?>(ProjectRiskErrors.ProjectRiskWithProjectIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetPOWbsByProjectWbsIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
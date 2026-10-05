using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetFilteredSkillByIds;
using Engineering.Domain.Errors.Projects;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Queries.GetPODContractorExpertsByCServiceId;

public class GetPODContractorExpertsByCServiceIdQueryHandler : IQueryHandler<GetPODContractorExpertsByCServiceIdQuery, GetPODContractorExpertsByCServiceIdResponse?>
{
    private readonly ILogger<GetPODContractorExpertsByCServiceIdQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorExpertRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IMediator _mediator;

    public GetPODContractorExpertsByCServiceIdQueryHandler(
        ILogger<GetPODContractorExpertsByCServiceIdQueryHandler> logger,
        IProjectOperationDetailContractorExpertRepository repository,
        IViewThirdPartyRepository thirdPartyRepo,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
        _mediator = mediator;
    }

    public async Task<Result<GetPODContractorExpertsByCServiceIdResponse?>> Handle(GetPODContractorExpertsByCServiceIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetPODContractorExpertsByCServiceId(request.ProjectOperationDetailContractorServiceId,
                request.PageIndex,
                request.PageSize, ct);

            if (result.Data is not null && result.Data.Any())
            {
                var skillIds = result.Data.Listed(x => x.SkillId);
                var skills = await _mediator.Send(new GetFilteredSkillByIdsQuery(skillIds, null, 1, skillIds.Count), ct);
                var creatorIds = result.Data.Select(x => x.CreatorId).ToList();
                var updaterIds = result.Data
                    .NullListed(x => x.UpdatorId);
                creatorIds.AddRange(updaterIds);

                var creators = await _thirdPartyRepo.GetByUserIds(creatorIds.Listed(x => x), ct);
                foreach (var item in result.Data!)
                {
                    var skill = skills.Value?.Data?.FirstOrDefault(x => x.Id == item.SkillId);
                    var creator = creators.FirstOrDefault(x => x.UserId == item.CreatorId);
                    var updator = creators.FirstOrDefault(x => x.UserId == item?.UpdatorId);
                    item.Creator = creator?.FirstName + " " + creator?.LastName;
                    item.Updator = updator?.FirstName + " " + updator?.LastName;
                    item.Skill = skill?.Name;
                }

            }

            return result.Data.HasAny() ?
                new GetPODContractorExpertsByCServiceIdResponse(result.Data!, result.RowCount) :
                Result.Failure<GetPODContractorExpertsByCServiceIdResponse?>(ProjectRiskErrors.ProjectRiskWithProjectIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetPODContractorExpertsByCServiceIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
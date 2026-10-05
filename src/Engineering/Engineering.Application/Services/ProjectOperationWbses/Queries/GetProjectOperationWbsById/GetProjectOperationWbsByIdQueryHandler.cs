using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetProjectOperationWbsById;

public class GetProjectOperationWbsByIdQueryHandler : IQueryHandler<GetProjectOperationWbsByIdQuery, GetProjectOperationWbsByIdResponse?>
{
    private readonly ILogger<GetProjectOperationWbsByIdQueryHandler> _logger;
    private readonly IProjectOperationWbsRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetProjectOperationWbsByIdQueryHandler(
        ILogger<GetProjectOperationWbsByIdQueryHandler> logger,
        IProjectOperationWbsRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetProjectOperationWbsByIdResponse?>> Handle(GetProjectOperationWbsByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationWbsById(request.Id, ct);

            if (result is not null)
            {
                var creatorId = result!.CreatorId;
                if (creatorId is not null)
                {
                    var creator = await _thirdPartyRepo.GetByUserIds([creatorId.Value], ct);
                    result!.Creator = creator?.FirstOrDefault()?.FirstName + " " + creator?.FirstOrDefault()?.LastName;
                }
            }
            return result ?? Result.Failure<GetProjectOperationWbsByIdResponse?>(WbsTemplateErrors.ProjectOperationWbsWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectOperationWbsByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
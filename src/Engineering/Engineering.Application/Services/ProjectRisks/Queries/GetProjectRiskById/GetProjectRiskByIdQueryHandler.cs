using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;
using Engineering.Domain.Errors.Projects;

namespace Engineering.Application.Services.ProjectRisks.Queries.GetProjectRiskById;

public class GetProjectRiskByIdQueryHandler : IQueryHandler<GetProjectRiskByIdQuery, GetProjectRiskByIdResponse?>
{
    private readonly ILogger<GetProjectRiskByIdQueryHandler> _logger;
    private readonly IProjectRiskRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetProjectRiskByIdQueryHandler(
        ILogger<GetProjectRiskByIdQueryHandler> logger,
        IProjectRiskRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetProjectRiskByIdResponse?>> Handle(GetProjectRiskByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectRiskById(request.Id, ct);

            if (result is not null)
            {
                var creatorId = result!.CreatorId;
                if (creatorId is not null)
                {
                    var creator = await _thirdPartyRepo.GetByUserIds([creatorId.Value], ct);
                    result!.Creator = creator?.FirstOrDefault()?.FirstName + " " + creator?.FirstOrDefault()?.LastName;
                }
            }
            return result ?? Result.Failure<GetProjectRiskByIdResponse?>(ProjectRiskErrors.ProjectRiskWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectRiskByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
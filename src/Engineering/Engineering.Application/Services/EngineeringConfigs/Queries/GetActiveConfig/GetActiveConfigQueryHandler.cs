using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Domain.Entities.EngineeringConfig;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;

public class GetActiveConfigQueryHandler : IQueryHandler<GetActiveConfigQuery, EngineeringConfig>
{
    private readonly IUserInfoService _userInfoService;
    private readonly IMediator _mediator;
    private readonly IEngineeringConfigRepository _repository;
    private readonly ILogger<GetActiveConfigQueryHandler> _logger;

    public GetActiveConfigQueryHandler(ILogger<GetActiveConfigQueryHandler> logger,
        IEngineeringConfigRepository repository,
        IMediator mediator,
        IUserInfoService userInfoService)
    {
        _logger = logger;
        _repository = repository;
        _userInfoService = userInfoService;
        _mediator = mediator;
    }

    public async Task<Result<EngineeringConfig?>> Handle(GetActiveConfigQuery request, CT ct)
    {
        try
        {
            var companyId = CompanyValidator.GetCompanyId(_userInfoService);
            if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
                return Result.Failure<EngineeringConfig>(GlobalErrors.InvalidCompany);

            var result = await _repository.GetActiveConfig(companyId.Value, ct);

            return result ?? Result.Failure<EngineeringConfig>(EngineeringConfigErrors.ActiveConfigNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EngineeringConfig>(SharedErrors.UnknownError);
        }
    }
}
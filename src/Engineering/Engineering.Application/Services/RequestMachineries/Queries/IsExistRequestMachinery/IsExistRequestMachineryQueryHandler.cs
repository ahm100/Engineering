using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.IsExistRequestMachinery;

public class IsExistRequestMachineryQueryHandler : IQueryHandler<IsExistRequestMachineryQuery, bool>
{
    private readonly ILogger<IsExistRequestMachineryQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public IsExistRequestMachineryQueryHandler(ILogger<IsExistRequestMachineryQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(IsExistRequestMachineryQuery request, CT ct)
    {
        try
        {
            var result = await _repository.IsExistRequestMachinery(request.ProjectId, request.MachineryId, request.TimeRequired, request.RequestCount, request.CompanyId, ct);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}

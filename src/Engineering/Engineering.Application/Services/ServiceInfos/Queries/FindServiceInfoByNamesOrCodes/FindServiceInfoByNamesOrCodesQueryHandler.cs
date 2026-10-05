using Engineering.Application.Abstractions.Data.ServiceInfos;

namespace Engineering.Application.Services.ServiceInfos.Queries.FindServiceInfoByNamesOrCodes;

public class FindServiceInfoByNamesOrCodesQueryHandler : IQueryHandler<FindServiceInfoByNamesOrCodesQuery, bool>
{
    private readonly ILogger<FindServiceInfoByNamesOrCodesQueryHandler> _logger;
    private readonly IServiceInfoRepository _repository;

    public FindServiceInfoByNamesOrCodesQueryHandler(ILogger<FindServiceInfoByNamesOrCodesQueryHandler> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindServiceInfoByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindServiceInfoByNamesOrCodes(request.Names, request.Codes, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}

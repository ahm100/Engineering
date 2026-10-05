using Engineering.Application.Abstractions.Data.CostOvers;

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOverByNamesOrCodes;

public class GetsCostOverByNamesOrCodesQueryHandler : IQueryHandler<GetsCostOverByNamesOrCodesQuery, bool>
{
    private readonly ILogger<GetsCostOverByNamesOrCodesQueryHandler> _logger;
    private readonly ICostOverRepository _repository;

    public GetsCostOverByNamesOrCodesQueryHandler(
        ILogger<GetsCostOverByNamesOrCodesQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(GetsCostOverByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetsCostOverByNamesOrCodes(
                request.Names,
                request.Codes,
                request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
using Engineering.Application.Abstractions.Data.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.FindOperationInfoGroupByNamesOrCodes;

public class FindOperationInfoGroupByNamesOrCodesQueryHandler : IQueryHandler<FindOperationInfoGroupByNamesOrCodesQuery, bool>
{
    private readonly ILogger<FindOperationInfoGroupByNamesOrCodesQueryHandler> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public FindOperationInfoGroupByNamesOrCodesQueryHandler(ILogger<FindOperationInfoGroupByNamesOrCodesQueryHandler> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindOperationInfoGroupByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindOperationInfoGroupByNamesOrCodes(request.Names, request.Codes, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}

using Engineering.Application.Abstractions.Data.OperationInfos;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfosByCodes;

public class GetOperationInfosByCodesQueryHandler : IQueryHandler<GetOperationInfosByCodesQuery, List<OperationInfoBulkDto>>
{
    private readonly ILogger<GetOperationInfosByCodesQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfosByCodesQueryHandler(
        ILogger<GetOperationInfosByCodesQueryHandler> logger,
        IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<OperationInfoBulkDto>?>> Handle(GetOperationInfosByCodesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByCodes(request.OperationInfoCodes, request.CompanyId, ct);
            return Result.Success<List<OperationInfoBulkDto>?>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<OperationInfoBulkDto>?>(SharedErrors.UnknownError);
        }
    }
}
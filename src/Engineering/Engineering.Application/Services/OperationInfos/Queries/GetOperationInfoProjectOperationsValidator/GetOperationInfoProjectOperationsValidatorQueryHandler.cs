using Engineering.Application.Abstractions.Data.OperationInfos;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoProjectOperationsValidator;

public class GetOperationInfoProjectOperationsValidatorQueryHandler : IQueryHandler<GetOperationInfoProjectOperationsValidatorQuery, bool>
{
    private readonly ILogger<GetOperationInfoProjectOperationsValidatorQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoProjectOperationsValidatorQueryHandler(ILogger<GetOperationInfoProjectOperationsValidatorQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(GetOperationInfoProjectOperationsValidatorQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoProjectOperationsValidator(request.ProjectId, request.OperationInfoId, request.EmployerContractId, request.UnitOfMeasurementId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
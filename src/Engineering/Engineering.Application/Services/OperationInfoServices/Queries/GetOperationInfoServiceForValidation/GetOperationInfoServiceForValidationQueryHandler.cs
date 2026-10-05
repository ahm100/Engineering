using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetOperationInfoServiceForValidation;

public class GetOperationInfoServiceForValidationQueryHandler : IQueryHandler<GetOperationInfoServiceForValidationQuery, OperationInfoService>
{
    private readonly IOperationInfoServiceRepository _repository;
    private readonly ILogger<GetOperationInfoServiceForValidationQueryHandler> _logger;

    public GetOperationInfoServiceForValidationQueryHandler(ILogger<GetOperationInfoServiceForValidationQueryHandler> logger, IOperationInfoServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoService?>> Handle(GetOperationInfoServiceForValidationQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoServiceForValidation(request.OperationInfoId, request.ServiceInfoId, ct);

            return result ?? Result.Failure<OperationInfoService?>(OperationInfoServiceErrors.OperationInfoServiceWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoService>(SharedErrors.UnknownError);
        }
    }
}
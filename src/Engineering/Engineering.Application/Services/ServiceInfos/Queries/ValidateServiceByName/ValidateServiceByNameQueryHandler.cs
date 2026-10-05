using Engineering.Application.Abstractions.Data.ServiceInfos;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.ValidateServiceByName;

public class ValidateServiceByNameQueryHandler : IQueryHandler<ValidateServiceByNameQuery, ServiceInfo>
{
    private readonly ILogger<ValidateServiceByNameQueryHandler> _logger;
    private readonly IServiceInfoRepository _repository;

    public ValidateServiceByNameQueryHandler(ILogger<ValidateServiceByNameQueryHandler> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ServiceInfo?>> Handle(ValidateServiceByNameQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.ValidateServiceByName(request.ServiceInfoName, request.MeasurementId, request.CompanyId, ct);
            return entity ?? Result.Failure<ServiceInfo>(ServiceInfoErrors.ServiceInfoWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ServiceInfo>(SharedErrors.UnknownError);
        }
    }
}
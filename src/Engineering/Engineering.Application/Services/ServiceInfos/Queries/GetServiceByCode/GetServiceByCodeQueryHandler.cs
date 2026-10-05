using Engineering.Application.Abstractions.Data.ServiceInfos;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetServiceByCode;

public class GetServiceInfoByCodeQueryHandler : IQueryHandler<GetServiceInfoByCodeQuery, ServiceInfo>
{
    private readonly ILogger<GetServiceInfoByCodeQueryHandler> _logger;
    private readonly IServiceInfoRepository _repository;

    public GetServiceInfoByCodeQueryHandler(ILogger<GetServiceInfoByCodeQueryHandler> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ServiceInfo?>> Handle(GetServiceInfoByCodeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByCode(request.ServiceInfoCode, request.CompanyId, ct);
            return result ?? Result.Failure<ServiceInfo>(ServiceInfoErrors.ServiceInfoWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ServiceInfo>(SharedErrors.UnknownError);
        }
    }
}
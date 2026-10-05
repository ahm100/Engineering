using Engineering.Application.Abstractions.Data.ServiceInfos;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetServiceByName;

public class GetServiceInfoByNameQueryHandler : IQueryHandler<GetServiceInfoByNameQuery, ServiceInfo>
{
    private readonly ILogger<GetServiceInfoByNameQueryHandler> _logger;
    private readonly IServiceInfoRepository _repository;

    public GetServiceInfoByNameQueryHandler(ILogger<GetServiceInfoByNameQueryHandler> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ServiceInfo?>> Handle(GetServiceInfoByNameQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByName(request.ServiceInfoName, request.CompanyId, ct);
            return result ?? Result.Failure<ServiceInfo>(ServiceInfoErrors.ServiceInfoWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ServiceInfo>(SharedErrors.UnknownError);
        }
    }
}
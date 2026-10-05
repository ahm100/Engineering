using Engineering.Application.Abstractions.Data.ServiceInfos;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetServiceById;

public class GetServiceInfoByIdQueryHandler : IQueryHandler<GetServiceInfoByIdQuery, ServiceInfo>
{
    private readonly ILogger<GetServiceInfoByIdQueryHandler> _logger;
    private readonly IServiceInfoRepository _repository;

    public GetServiceInfoByIdQueryHandler(ILogger<GetServiceInfoByIdQueryHandler> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ServiceInfo?>> Handle(GetServiceInfoByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);

            return result ?? Result.Failure<ServiceInfo>(ServiceInfoErrors.ServiceInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ServiceInfo>(SharedErrors.UnknownError);
        }
    }
}
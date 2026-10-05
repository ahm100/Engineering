using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ServiceInfos;
using Engineering.Domain.Entities.ServiceInfos.Enums;

namespace Engineering.Application.Services.ServiceInfos.Commands.CreateService;

public class CreateServiceInfoCommandHandler : ICommandHandler<CreateServiceInfoCommand, ServiceInfo>
{
    private readonly ILogger<CreateServiceInfoCommand> _logger;
    private readonly IServiceInfoRepository _repository;

    public CreateServiceInfoCommandHandler(ILogger<CreateServiceInfoCommand> logger,
        IServiceInfoRepository repository,
        IMeasureUnitRepository measureRepo)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ServiceInfo?>> Handle(CreateServiceInfoCommand request, CT ct)
    {
        try
        {
            if (request.Type == ServiceInfoType.Administrative && request.ServiceInfoEnName == null)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.AdminEnNameIsRequired);

            var entity = new ServiceInfo(request.ServiceInfoName,
                request.ServiceInfoEnName,
                request.ServiceInfoCode,
                request.DescriptionFa,
                request.DescriptionEn,
                request.UnitOfMeasurementId,
                request.IsActive,
                request.Type,
                request.DocumentUrls,
                request.CompanyId);
            var result = await _repository.Create(entity, ct);

            if (request.Type == ServiceInfoType.Engineering &&
                request.OperationInfos is not null &&
                request.OperationInfos?.Count > 0)
                foreach (var item in request.OperationInfos)
                    entity.AddOperationInfoService(new OperationInfoService(item, entity, request.TimeSpant));

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ServiceInfo>(SharedErrors.UnknownError);
        }
    }
}
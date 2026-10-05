using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Domain.Entities.ServiceInfos.Enums;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.UpdateService;

public class UpdateServiceInfoCommandHandler : ICommandHandler<UpdateServiceInfoCommand, ServiceInfo>
{
    private readonly ILogger<UpdateServiceInfoCommand> _logger;
    private readonly IServiceInfoRepository _repository;

    public UpdateServiceInfoCommandHandler(ILogger<UpdateServiceInfoCommand> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ServiceInfo?>> Handle(UpdateServiceInfoCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.ServiceInfoWithIdNotFound);

            if (request.Type == ServiceInfoType.Administrative && request.ServiceInfoEnName == null)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.AdminEnNameIsRequired);

            entity.SetName(request.ServiceInfoName);
            entity.SetCode(request.ServiceInfoCode);
            entity.SetUnitOfMeasurement(request.UnitOfMeasurementId);
            entity.SetCompanyId(request.CompanyId);
            entity.SetType(request.Type);
            entity.SetDescriptionEn(request.DescriptionEn);
            entity.SetDescriptionFa(request.DescriptionFa);
            entity.SetServiceInfoNameEn(request.ServiceInfoEnName);
            entity.AddDocuments(request.DocumentUrls);

            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            if (entity.PreferentialReferenceCode == Guid.Empty)
                entity.SetPreferentialCode();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ServiceInfo>(SharedErrors.UnknownError);
        }
    }
}
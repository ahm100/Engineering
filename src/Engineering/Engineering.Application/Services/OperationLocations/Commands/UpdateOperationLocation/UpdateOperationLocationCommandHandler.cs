using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.UpdateOperationLocation;

public class UpdateOperationLocationCommandHandler : ICommandHandler<UpdateOperationLocationCommand, OperationLocation>
{
    private readonly ILogger<UpdateOperationLocationCommand> _logger;
    private readonly IOperationLocationRepository _repository;

    public UpdateOperationLocationCommandHandler(ILogger<UpdateOperationLocationCommand> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(UpdateOperationLocationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetOperationLocationById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationLocation>(OperationLocationErrors.OperationLocationWithIdNotFound);

            if (request.Parent is not null)
                entity.SetParent(request.Parent);

            entity.SetPrivateName(request.PrivateName);
            entity.SetPrivateCode(request.PrivateCode);
            entity.SetPublicName(request.PublicName);
            entity.SetPublicCode(request.PublicCode);
            entity.SetPriority(request.Priority);
            entity.SetCompanyId(request.CompanyId);
            if (request.Parent is null)
                entity.SetProject(request.Project!);

            if (entity.Parent is null)
                entity.SetCoding($"{entity.CostCenter.CostCenterCode},{request.PrivateCode}");
            if (entity.Parent is not null)
                entity.SetCoding($"{request.Parent!.Coding},{request.PrivateCode}");

            if (entity.Parent is null && request.Parent is null)
                entity.SetPath($"{entity.CostCenter.Id},{entity.Id}");
            else if (entity.Parent is not null && request.Parent is not null)
                entity.SetPath($"{request.Parent!.Path},{entity.Id}");
            else if (entity.Parent is null && request.Parent is not null)
                entity.SetPath($"{request.Parent!.Path},{entity.Id}");

            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<OperationLocation>(SharedErrors.UnknownError);
        }
    }
}
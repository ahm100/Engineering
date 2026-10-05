using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.UpdateCostCenterType;

public class UpdateCostCenterTypeCommandHandler : ICommandHandler<UpdateCostCenterTypeCommand, CostCenterType>
{
    private readonly ILogger<UpdateCostCenterTypeCommand> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public UpdateCostCenterTypeCommandHandler(
        ILogger<UpdateCostCenterTypeCommand> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterType?>> Handle(UpdateCostCenterTypeCommand request, CT ct)
    {
        try
        {
            var Entity = await _repository.GetCostCenterTypeById(request.Id, ct);
            if (Entity is null)
                return Result.Failure<CostCenterType>(CostCenterErrors.CostCenterWithIdNotFound);

            Entity.SetName(request.CostCenterTypeName);
            Entity.SetCode(request.CostCenterTypeCode);
            Entity.SetCompanyId(request.CompanyId);
            if (request.IsActive == false)
                Entity.SetDeactivate();
            else
                Entity.SetActive();

            await _repository.Update(Entity);
            return Entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<CostCenterType>(SharedErrors.UnknownError);
        }
    }
}
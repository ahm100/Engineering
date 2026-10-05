using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.UpdateCostCenterVirtualGroupAdmin;

public class UpdateCostCenterVirtualGroupAdminCommandHandler : ICommandHandler<UpdateCostCenterVirtualGroupAdminCommand, CostCenterVirtualGroupAdmin>
{
    private readonly ILogger<UpdateCostCenterVirtualGroupAdminCommandHandler> _logger;
    private readonly ICostCenterVirtualGroupAdminRepository _repository;

    public UpdateCostCenterVirtualGroupAdminCommandHandler(ILogger<UpdateCostCenterVirtualGroupAdminCommandHandler> logger,
                                                      ICostCenterVirtualGroupAdminRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterVirtualGroupAdmin?>> Handle(UpdateCostCenterVirtualGroupAdminCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.CostCenterVirtualGroupAdminId, ct);

            if (entity is null)
                return Result.Failure<CostCenterVirtualGroupAdmin>(CostCenterVirtualGroupAdminErrors.CostCenterVirtualGroupAdminNotFound);

            entity.SetThirdPartyId(request.ThirdPartyId);
            entity.SetUserName(request.UserName);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterVirtualGroupAdmin>(SharedErrors.UnknownError);
        }
    }
}

using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.CreateCostCenterVirtualGroupAdmin;

public class CreateCostCenterVirtualGroupAdminCommandHandler : ICommandHandler<CreateCostCenterVirtualGroupAdminCommand, CostCenterVirtualGroupAdmin>
{
    private readonly ILogger<CreateCostCenterVirtualGroupAdminCommandHandler> _logger;
    private readonly ICostCenterVirtualGroupAdminRepository _repository;

    public CreateCostCenterVirtualGroupAdminCommandHandler(ILogger<CreateCostCenterVirtualGroupAdminCommandHandler> logger,
                                                      ICostCenterVirtualGroupAdminRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterVirtualGroupAdmin?>> Handle(CreateCostCenterVirtualGroupAdminCommand request, CT ct)
    {
        try
        {
            var entity = new CostCenterVirtualGroupAdmin(request.ThirdPartyId,
                                                         request.UserName,
                                                         request.CostCenterVirtualGroup);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterVirtualGroupAdmin>(SharedErrors.UnknownError);
        }
    }
}
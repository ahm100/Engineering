using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.CreateCostCenterVirtualGroup;

public class CreateCostCenterVirtualGroupCommandHandler : ICommandHandler<CreateCostCenterVirtualGroupCommand, CostCenterVirtualGroup>
{
    private readonly ILogger<CreateCostCenterVirtualGroupCommandHandler> _logger;
    private readonly ICostCenterVirtualGroupRepository _repository;

    public CreateCostCenterVirtualGroupCommandHandler(ILogger<CreateCostCenterVirtualGroupCommandHandler> logger,
                                                      ICostCenterVirtualGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterVirtualGroup?>> Handle(CreateCostCenterVirtualGroupCommand request, CT ct)
    {
        try
        {
            var entity = new CostCenterVirtualGroup(request.Title,
                                                    request.Link,
                                                    request.Identifier,
                                                    request.Description,
                                                    request.SendToday,
                                                    request.TodayTime,
                                                    request.SendYesterday,
                                                    request.YesterdayTime,
                                                    request.CostCenter);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterVirtualGroup>(SharedErrors.UnknownError);
        }
    }
}

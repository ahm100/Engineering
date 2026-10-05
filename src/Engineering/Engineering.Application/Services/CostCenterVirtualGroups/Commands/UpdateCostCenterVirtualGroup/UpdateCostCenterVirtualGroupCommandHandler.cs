using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.UpdateCostCenterVirtualGroup;

public class UpdateCostCenterVirtualGroupCommandHandler : ICommandHandler<UpdateCostCenterVirtualGroupCommand, CostCenterVirtualGroup>
{
    private readonly ILogger<UpdateCostCenterVirtualGroupCommandHandler> _logger;
    private readonly ICostCenterVirtualGroupRepository _repository;

    public UpdateCostCenterVirtualGroupCommandHandler(ILogger<UpdateCostCenterVirtualGroupCommandHandler> logger,
                                                           ICostCenterVirtualGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterVirtualGroup?>> Handle(UpdateCostCenterVirtualGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.CostCenterVirtualGroupId, ct);

            if (entity is null)
                return Result.Failure<CostCenterVirtualGroup>(CostCenterVirtualGroupErrors.CostCenterVirtualGroupNotFound);

            entity.SetTitle(request.Title);
            entity.SetLink(request.Link);
            entity.SetIdentifier(request.Identifier);
            entity.SetDescription(request.Description);
            entity.SetSendToday(request.SendToday);
            entity.SetSendYesterday(request.SendYesterday);
            entity.SetTodayTime(request.TodayTime);
            entity.SetYesterdayTime(request.YesterdayTime);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterVirtualGroup>(SharedErrors.UnknownError);
        }
    }
}


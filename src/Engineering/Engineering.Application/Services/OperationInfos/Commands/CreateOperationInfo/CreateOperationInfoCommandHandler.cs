using Engineering.Application.Abstractions.Data.Actions;
using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Errors.Actions;
using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;
using ConsumptionStandardGoods = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;
using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.CreateOperationInfo;

public class CreateOperationInfoCommandHandler : ICommandHandler<CreateOperationInfoCommand, OperationInfo>
{
    private readonly ILogger<CreateOperationInfoCommand> _logger;
    private readonly IOperationInfoRepository _repository;
    private readonly IActionRepository _actionRepo;

    public CreateOperationInfoCommandHandler(ILogger<CreateOperationInfoCommand> logger, IOperationInfoRepository repository, IActionRepository repo)
    {
        _logger = logger;
        _repository = repository;
        _actionRepo = repo;
    }

    public async Task<Result<OperationInfo?>> Handle(CreateOperationInfoCommand request, CT ct)
    {
        try
        {
            var entity = new OperationInfo(request.OperationInfoName, request.OperationInfoCode, request.OperationLatinName,
                request.Priority, request.UnitOfMeasurementId, request.IsActive, request.IsPriceList, request.BasePrice, request.CompanyId);
            var result = await _repository.Create(entity, ct);

            if (request.ExpertStandards is not null && request.ExpertStandards.Count > 0)
                foreach (var expertStandard in request.ExpertStandards)
                    entity.AddExpertStandard(new ConsumptionStandardExpert(entity, expertStandard!.Id, expertStandard.ExpertNumber, expertStandard.TimeSpant, expertStandard.UnusedPercentage));

            if (request.GoodsStandards is not null && request.GoodsStandards.Count > 0)
                foreach (var goodsStandard in request.GoodsStandards)
                    entity.AddMaterialStandard(new ConsumptionStandardGoods(entity, goodsStandard!.Id, goodsStandard.GoodsNumber, goodsStandard.UnusedPercentage, goodsStandard.StandardProductType, goodsStandard.ProductAllowedType));

            if (request.MachineryStandards is not null && request.MachineryStandards.Count > 0)
                foreach (var machineryStandard in request.MachineryStandards)
                    entity.AddMachineryStandard(new ConsumptionStandardMachinery(entity, machineryStandard!.Machinery, machineryStandard.MachineryNumber, machineryStandard.TimeSpant, machineryStandard.UnusedPercentage));

            if (request.ServiceInfos is not null && request.ServiceInfos.Count > 0)
                foreach (var serviceInfo in request.ServiceInfos)
                    entity.AddOperationInfoService(new OperationInfoService(entity, serviceInfo.ServiceInfo, serviceInfo.TimeSpant));

            if (request.OperationInfoActions is not null && request.OperationInfoActions.Count > 0)
                foreach (var OperationInfoAction in request.OperationInfoActions)
                {
                    var action = await _actionRepo.GetActionById(OperationInfoAction.ActionId, ct);
                    if (action is null)
                        return Result.Failure<OperationInfo>(ActionErrors.ActionWithIdNotFound);
                    entity.AddOperationInfoAction(new OperationInfoAction(entity, action, OperationInfoAction.Price));
                }


            if (request.Seasons is not null && request.Seasons.Count > 0)
                foreach (var season in request.Seasons)
                {
                    if (!season.IsActive)
                        return Result.Failure<OperationInfo?>(OperationInfoErrors.SeasonIsInActive)!;
                    if (!season.Branch.IsActive)
                        return Result.Failure<OperationInfo?>(OperationInfoErrors.BranchIsInActive)!;
                    if (!season.Branch.Category.IsActive)
                        return Result.Failure<OperationInfo?>(OperationInfoErrors.CategoryIsInActive)!;

                    entity.AddOperationInfoSeason(new OperationInfoSeason(entity, season));
                }
            if (request.OperationInfoGroups is not null && request.OperationInfoGroups.Count > 0)
                foreach (var group in request.OperationInfoGroups)
                    entity.AddOperationInfoGroupRelation(new OperationInfoGroupRelation(entity, group));

            entity.SetStandard();
            entity.AddHistory();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}
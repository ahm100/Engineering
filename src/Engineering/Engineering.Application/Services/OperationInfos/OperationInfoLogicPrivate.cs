using Engineering.Application.Services.OperationInfoGroupRelations.Models.OperationInfoGroupRelationModels;
using Engineering.Application.Services.OperationInfos.Models.CreateOperationInfoActions;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;
using Engineering.Application.Services.OperationInfoSeasons.Models;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetsMeasureunitById;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.Enums;
using Engineering.Domain.Errors.Actions;

namespace Engineering.Application.Services.OperationInfos;

public partial class OperationInfoLogic
{
    private async Task<Result<CreateOperationInfoActionsResponse>> CreateOperationInfoActionsPrivate(
        CreateOperationInfoActionsRequest request,
        OperationInfo info, CT ct)
    {
        var reqActionIds = request.actions
            .Select(x => x.ActionId)
            .Distinct()
            .ToList();
        var actions = await _actionRepository.GetActions(reqActionIds, ct);
        if (actions == null || actions.Count != reqActionIds.Count)
            return Result.Failure<CreateOperationInfoActionsResponse>(
                ActionErrors.ActionWithIdNotFound)!;
        var existingRelations = await _operationInfoActionRepository
            .GetOperationInfoActions(request.OperationInfoId.ToDataList(), ct);
        if (existingRelations.Any())
        {
            var actionIdsToDelete = existingRelations
                .Select(x => x.Id)
                .ToList();

            var deleteResult = await DeleteOperationInfoActionsHandler(
                actionIdsToDelete,
                ct);

            if (deleteResult.IsBad())
                return deleteResult.Failure<CreateOperationInfoActionsResponse>();
        }
        if (actions.Any())
        {
            var createResult = await CreateOperationInfoActionsHandler(
                actions,
                info,
                request.actions.First().Price,
                ct);

            if (createResult.IsBad())
                return createResult.Failure<CreateOperationInfoActionsResponse>();
        }
        if (request.IncreaseRate.HasValue)
            info.SetIncreaseRate(request.IncreaseRate.Value);
        return new CreateOperationInfoActionsResponse(true);
    }


    private async Task<OperationInfoModel> FullModeling(
        OperationInfo item,
        List<Measureunit>? measureunits,
        OperationInfoDependencyModel? dependency,
        Company? company, CT ct)
    {
        var request = new GetsOperationInfoSeasonByIdRequest(item.Id, 0, 0);
        var operationInfoSeasons = await _operationInfoSeasonLogic.GetsByOperationInfoId(request, ct);
        var seasons = operationInfoSeasons?.Value;
        var infoMersur = new OperationInfoMeasurementModel(item.UnitOfMeasurementId, measureunits?.Where(m => m.Id == item.UnitOfMeasurementId).FirstOrDefault()?.Name);
        var expertData = await ExpertDataReceiver(item, ct);
        var goodsData = await GoodsDataReceiver(item, ct);
        var machineryData = MachineryDataReceiver(item);
        var serviceInfoData = ServiceInfoDataReceiver(item, measureunits);
        var groupData = GroupDataReceiver(item);

        return new OperationInfoModel(item.Id, item.OperationInfoName, item.OperationInfoCode, item.OperationLatinName, item.Priority, item.HaveStandard, infoMersur, dependency, serviceInfoData, expertData,
            goodsData, machineryData, seasons?.CategoryData, seasons?.BranchData, seasons?.SeasonData, groupData, item.IsActive, item.CompanyId, company?.NameFa);
    }

    private async Task<List<Measureunit>?> MeasurementDataReceiver(
        List<OperationInfo>? operationInfos, CT ct)
    {
        var dataResult = new List<long>();
        if (operationInfos is not null && operationInfos.Count > 0)
        {
            dataResult.AddRange(operationInfos.Select(x => x.UnitOfMeasurementId).ToList());
            dataResult.AddRange(operationInfos.SelectMany(x => x.OperationInfoServices.Select(b => b.ServiceInfo.UnitOfMeasurementId)).ToList());
        }

        var allIds = dataResult.Where(x => x != 0).Distinct().ToList();
        var measureUnitsData = await _mediator.Send(new GetsMeasureunitByIdQuery(1, allIds.Count, allIds, true), ct);
        List<Measureunit> result = [];
        if (!measureUnitsData.IsBad() && measureUnitsData.Value!.Data != null)
            foreach (var item in measureUnitsData.Value!.Data)
                result.Add(new Measureunit(
                    item.Id,
                    item.Name,
                    item.MeasureUnitGroupId,
                    item.ConversionFactor,
                    item.Tolerance,
                    item.IsActive,
                    item.IsPrimary));

        return result;
    }

    private async Task<List<Measureunit>?> MeasurementDataReceiver(
        List<GetOperationInfosModel>? operationInfos, CT ct)
    {
        var dataResult = new List<long>();
        if (operationInfos is not null && operationInfos.Count > 0)
        {
            dataResult.AddRange(operationInfos.Select(x => x.UnitOfMeasurementId).ToList());
        }

        var allIds = dataResult.Where(x => x != 0).Distinct().ToList();
        var measureUnitsData = await _mediator.Send(new GetsMeasureunitByIdQuery(1, allIds.Count, allIds, true), ct);

        List<Measureunit> result = [];
        if (!measureUnitsData.IsBad() && measureUnitsData.Value!.Data != null)
            foreach (var item in measureUnitsData.Value!.Data)
                result.Add(new Measureunit(
                    item.Id,
                    item.Name,
                    item.MeasureUnitGroupId,
                    item.ConversionFactor,
                    item.Tolerance,
                    item.IsActive,
                    item.IsPrimary));

        return result;
    }

    private async Task<List<OperationInfoExpertDataModel>?> ExpertDataReceiver(
        OperationInfo operationInfo, CT ct)
    {
        List<OperationInfoExpertDataModel>? expertsData = [];
        if (operationInfo.ConsumptionStandardExperts is not null && operationInfo.ConsumptionStandardExperts.Count > 0)
        {
            var allIds = operationInfo.ConsumptionStandardExperts.Select(x => x.ExpertUnitId).ToList();
            var experts = await WebServicesLogic.SkillsDataReceiver(allIds, _mediator, ct); // بره سراغ متا دیتا

            foreach (var item in operationInfo.ConsumptionStandardExperts)
            {
                var expert = experts?.Where(c => c?.Id == item?.ExpertUnitId).FirstOrDefault();
                expertsData.Add(new(item.Id, item!.ExpertUnitId, expert?.Name, expert?.Code, item!.ExpertNumber, TimeCalculator.TicksToStringHM(item.TimeSpant), item!.UnusedPercentage));
            }
            return expertsData;
        }
        else
            return expertsData;
    }

    private List<OperationInfoMachineryDataModel>? MachineryDataReceiver(
        OperationInfo operationInfo)
    {
        List<OperationInfoMachineryDataModel>? machinerysData = [];
        if (operationInfo.ConsumptionStandardMachineries is not null && operationInfo.ConsumptionStandardMachineries.Count > 0)
        {
            foreach (var item in operationInfo.ConsumptionStandardMachineries)
            {
                machinerysData.Add(new(item.Id, item!.Machinery.Id, item!.Machinery.MachineryName, item!.Machinery.MachineryCode,
                    item!.MachineryNumber, TimeCalculator.TicksToStringHM(item.TimeSpant), item!.UnusedPercentage));
            }
            return machinerysData;
        }
        else
            return machinerysData;
    }

    private List<ServiceInfoDataModel>? ServiceInfoDataReceiver(
        OperationInfo operationInfo, List<Measureunit>? measureunits)
    {
        List<ServiceInfoDataModel>? serviceInfoData = [];
        if (operationInfo.OperationInfoServices is not null && operationInfo.OperationInfoServices.Count > 0)
        {
            foreach (var item in operationInfo.OperationInfoServices)
            {
                var infoMersur = new OperationInfoMeasurementModel(item.ServiceInfo.UnitOfMeasurementId, measureunits?.Where(m => m.Id == item.ServiceInfo.UnitOfMeasurementId).FirstOrDefault()?.Name);
                serviceInfoData.Add(new(item.Id, item.ServiceInfo.Id, item.ServiceInfo.ServiceInfoName, item.ServiceInfo.ServiceInfoCode, infoMersur, TimeCalculator.TicksToStringHM(item.TimeSpant), item.ServiceInfo.IsActive));
            }
            return serviceInfoData;
        }
        else
            return serviceInfoData;
    }

    private List<OperationInfoGroupDataModel>? GroupDataReceiver(
        OperationInfo operationInfo)
    {
        List<OperationInfoGroupDataModel>? groupData = [];
        if (operationInfo.OperationInfoGroupRelations is not null && operationInfo.OperationInfoGroupRelations.Count > 0)
        {
            foreach (var item in operationInfo.OperationInfoGroupRelations)
            {
                groupData.Add(new(item.Id, item.OperationInfoGroup.Id, item.OperationInfoGroup.OperationInfoGroupTitle, item.OperationInfoGroup.OperationInfoGroupCode, item.OperationInfoGroup.IsActive));
            }
            return groupData;
        }
        else
            return groupData;
    }

    private async Task<List<OperationInfoGoodsDataModel>?> GoodsDataReceiver(
        OperationInfo operationInfo, CT ct)
    {
        List<OperationInfoGoodsDataModel>? goodsData = [];
        if (operationInfo.ConsumptionStandardProduct is not null && operationInfo.ConsumptionStandardProduct.Count > 0)
        {
            var productIds = operationInfo.ConsumptionStandardProduct.Where(x => x.StandardProductType == StandardProductType.ProductGroup).Select(c => c!.ProductUnitId).Where(x => x != 0).ToList();
            var productsData = await WebServicesLogic.GroupsDataReceiver(productIds, _mediator, ct); // بره سراغ انبار

            var categoryIds = operationInfo.ConsumptionStandardProduct.Where(x => x.StandardProductType == StandardProductType.Category).Select(c => c!.ProductUnitId).Where(x => x != 0).ToList();
            var categoriesData = await WebServicesLogic.CategoriesDataReceiver(categoryIds, null, _mediator, ct); // بره سراغ انبار

            foreach (var item in operationInfo.ConsumptionStandardProduct)
            {
                if (item.StandardProductType == StandardProductType.ProductGroup)
                {
                    var product = productsData?.Where(c => c?.Id == item?.ProductUnitId).FirstOrDefault();
                    goodsData.Add(new(item.Id, item!.ProductUnitId, product?.Name, product?.Code, product?.IsActive, item!.Number, item!.UnusedPercentage, product?.MeasureUnitId,
                        product?.MeasureUnitName, item.StandardProductType, item.StandardProductType.GetEnumDescription(), item.ProductAllowedType, item.ProductAllowedType.GetEnumDescription()));
                }

                if (item.StandardProductType == StandardProductType.Category)
                {
                    var category = categoriesData?.Where(c => c?.Id == item?.ProductUnitId).FirstOrDefault();
                    goodsData.Add(new(item.Id, item!.ProductUnitId, category?.Title, category?.Code, category?.IsActive, item!.Number, item!.UnusedPercentage, null, null,
                        item.StandardProductType, item.StandardProductType.GetEnumDescription(), item.ProductAllowedType, item.ProductAllowedType.GetEnumDescription()));
                }
            }
            return goodsData;
        }
        else
            return goodsData;
    }

    private GetsOperationInfoGroupRelationResponseModel? GetsByOperationIdModeling(
        List<OperationInfoGroupRelation>? items)
    {
        List<GroupRelationsOperationInfoModel>? operationInfos = [];
        List<GroupRelationsOperationInfoGroupModel>? operationInfoGroups = [];

        if (items is not null && items.Count > 0)
            foreach (var item in items)
            {
                operationInfos.Add(new GroupRelationsOperationInfoModel(item.OperationInfo.Id, item.OperationInfo.OperationInfoName, item.OperationInfo.OperationInfoCode));
                operationInfoGroups.Add(new GroupRelationsOperationInfoGroupModel(item.OperationInfoGroup.Id, item.OperationInfoGroup.OperationInfoGroupTitle, item.OperationInfoGroup.OperationInfoGroupCode));
            }

        return new GetsOperationInfoGroupRelationResponseModel(operationInfos, operationInfoGroups);
    }

    public static GetsGroupsNameModel? GetsGroupNameModeling(
        GetsOperationInfoGroupRelationResponseModel? groupData)
    {
        var groupsName = "";

        var groups = groupData?.OperationInfoGroupData?.Select(x => x.GroupName);
        if (groups is not null)
            groupsName = string.Join(", ", groups);

        return new GetsGroupsNameModel(groupsName);
    }

    public static GetsGroupsNameModel? GetsGroupNameModeling(
        List<GroupRelationsOperationInfoGroupModel>? groupData)
    {
        var groupsName = "";

        var groups = groupData?.Select(x => x.GroupName);
        if (groups is not null)
            groupsName = string.Join(", ", groups);

        return new GetsGroupsNameModel(groupsName);
    }


}

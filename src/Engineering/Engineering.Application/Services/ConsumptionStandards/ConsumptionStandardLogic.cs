using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.ConsumptionStandards.Commands.Experts.CreateExpert;
using Engineering.Application.Services.ConsumptionStandards.Commands.Experts.DisableExpert;
using Engineering.Application.Services.ConsumptionStandards.Commands.Experts.UpdateExpert;
using Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.CreateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.DisableMachinery;
using Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.UpdateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Commands.Products.CreateProduct;
using Engineering.Application.Services.ConsumptionStandards.Commands.Products.DisableProduct;
using Engineering.Application.Services.ConsumptionStandards.Commands.Products.UpdateProduct;
using Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.GetsConsumptionStandardByOperationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.UpdateConsumptionStandards;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.CreateExpert;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.DisableExpert;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.ExpertGetsByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.ExpertModels;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.UpdateExpert;
using Engineering.Application.Services.ConsumptionStandards.Models.GetProductAllowedTypes;
using Engineering.Application.Services.ConsumptionStandards.Models.GetStandardProductTypes;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.CreateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.DisableMachinery;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.MachineryGetsByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.MachineryModels;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.UpdateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.CreateProduct;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.DisableProduct;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsNonStandardProductByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsProductByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.ProducModels;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.UpdateProduct;
using Engineering.Application.Services.ConsumptionStandards.Queries.Experts.ExpertGetsByOperationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Queries.Machiner.MachineryGetsByOperationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Queries.Products.GetsNonStandardProductByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Queries.Products.GetsProductByOperationInfoId;
using Engineering.Application.Services.Machineries.Queries.GetMachineryById;
using Engineering.Application.Services.Machineries.Queries.GetsMachineryByIds;
using Engineering.Application.Services.OperationInfos.Commands.SetOperationInfoHaveStandard;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByConsumptionStandards;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByExperts;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByMachineries;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByProducts;
using Engineering.Application.Services.OperationInfos.Queries.HaveOperationInfoChild;
using Engineering.Application.Services.PublicGroups.Queries.GetFilteredPublicGroups;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillById;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetsSkillById;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetActiveGroups;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetsWarehouseCategoryById;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetWarehouseCategoryById;
using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;
using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.ConsumptionStandards;

public class ConsumptionStandardLogic : IConsumptionStandardLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ConsumptionStandardLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ConsumptionStandardLogic(
        IMediator mediator,
        ILogger<ConsumptionStandardLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    #region Experts
    public async Task<Result<CreateConsumptionStandardExpertResponse?>> CreateExpertStandard(
        CreateConsumptionStandardExpertRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateExpert, ExpertNumber:{ExpertNumber}, UnusedPercentage:{UnusedPercentage},", request.ExpertNumber, request.UnusedPercentage);

        var isValidRequest = await request.IsValidAsync<CreateConsumptionStandardExpertValidator, CreateConsumptionStandardExpertRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateConsumptionStandardExpertResponse>(isValidRequest.Error!);

        var operationInfo = await _mediator.Send(new GetOperationInfoByIdByExpertsQuery(request.OperationInfoId), ct);
        if (operationInfo.IsFailure)
            return Result.Failure<CreateConsumptionStandardExpertResponse>(operationInfo.Error!);
        if (operationInfo.Value is null)
            return Result.Failure<CreateConsumptionStandardExpertResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
        if (operationInfo.Value.ConsumptionStandardExperts.Any(x => x.ExpertUnitId == request.Id))
            return Result.Failure<CreateConsumptionStandardExpertResponse>(ExpertStandardErrors.ExpertIsDuplicate);

        if (!string.IsNullOrEmpty(request.TimeSpant))
            if (!(request.TimeSpant.Split(':')[0].Count() >= 2 && request.TimeSpant.Split(':')[1].Count() == 2))
                return Result.Failure<CreateConsumptionStandardExpertResponse>(MachineryStandardErrors.TimeSpantCountError);

        if (int.Parse(request.TimeSpant.Split(':')[1]) > 59)
            return Result.Failure<CreateConsumptionStandardExpertResponse>(MachineryStandardErrors.MoreThan59Min);

        var expertData = await _mediator.Send(new GetSkillByIdQuery(request.Id), ct); // بره سراغ متا دیتا
        if (expertData.IsFailure)
            return Result.Failure<CreateConsumptionStandardExpertResponse>(ExpertStandardErrors.ExpertWithIdNotFound);

        var response = await _mediator.Send(new CreateExpertCommand(operationInfo.Value!, request.Id, request.ExpertNumber, TimeCalculator.StringToTicks(request.TimeSpant), request.UnusedPercentage), ct);
        if (response.IsFailure)
            return Result.Failure<CreateConsumptionStandardExpertResponse>(response.Error!);

        var setStandard = await _mediator.Send(new SetOperationInfoHaveStandardCommand(operationInfo.Value!.Id), ct);

        await _unitOfWork.CommitAsync(ct);
        return new CreateConsumptionStandardExpertResponse(response.Value!.Id, response.Value!.ExpertUnitId, true);
    }

    public async Task<Result<DisableConsumptionStandardExpertResponse?>> DisableExpertStandard(
        DisableConsumptionStandardExpertRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableExpert, Id:{Id}", request.OperationInfoExpertId);

        var isValidRequest = await request.IsValidAsync<DisableConsumptionStandardExpertValidator, DisableConsumptionStandardExpertRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableConsumptionStandardExpertResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableExpertCommand(request.OperationInfoExpertId), ct);
        if (response.IsFailure)
            return Result.Failure<DisableConsumptionStandardExpertResponse>(response.Error!);
        var expert = response.Value!;

        var setStandard = await _mediator.Send(new SetOperationInfoHaveStandardCommand(expert.OperationInfo.Id), ct);

        await _unitOfWork.CommitAsync(ct);
        return new DisableConsumptionStandardExpertResponse(expert.Id, expert.IsDeleted);
    }

    public async Task<Result<UpdateConsumptionStandardExpertResponse?>> UpdateExpertStandard(
        UpdateConsumptionStandardExpertRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateExpert, OperationInfoExpertId:{OperationInfoExpertId}, ExpertNumber:{ExpertNumber} , UnusedPercentage:{UnusedPercentage},", request.OperationInfoExpertId, request.ExpertNumber, request.UnusedPercentage);

        var isValidRequest = await request.IsValidAsync<UpdateConsumptionStandardExpertValidator, UpdateConsumptionStandardExpertRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateConsumptionStandardExpertResponse>(isValidRequest.Error!);

        var expertData = await _mediator.Send(new GetSkillByIdQuery(request.Id), ct); // بره سراغ متا دیتا
        if (expertData.IsFailure)
            return Result.Failure<UpdateConsumptionStandardExpertResponse>(ExpertStandardErrors.ExpertWithIdNotFound);

        if (!string.IsNullOrEmpty(request.TimeSpant))
            if (!(request.TimeSpant.Split(':')[0].Count() >= 2 && request.TimeSpant.Split(':')[1].Count() == 2))
                return Result.Failure<UpdateConsumptionStandardExpertResponse>(MachineryStandardErrors.TimeSpantCountError);

        if (int.Parse(request.TimeSpant.Split(':')[1]) > 59)
            return Result.Failure<UpdateConsumptionStandardExpertResponse>(MachineryStandardErrors.MoreThan59Min);

        var response = await _mediator.Send(new UpdateExpertCommand(request.OperationInfoExpertId, request.Id, request.ExpertNumber,
                TimeCalculator.StringToTicks(request.TimeSpant), request.UnusedPercentage), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateConsumptionStandardExpertResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateConsumptionStandardExpertResponse(response.Value!.Id, response.Value!.ExpertUnitId, true);
    }

    public async Task<Result<ExpertGetsByOprationInfoIdResponse?>> ExpertGetsByOperationInfoId(
        ExpertGetsByOprationInfoIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for ExpertGetsByOperationInfoId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<ExpertGetsByOprationInfoIdValidator, ExpertGetsByOprationInfoIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ExpertGetsByOprationInfoIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ExpertGetsByOperationInfoIdQuery(request.OprationInfoId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<ExpertGetsByOprationInfoIdResponse>(response.Error!);
        if (response.Value?.Data is null)
            return Result.Failure<ExpertGetsByOprationInfoIdResponse>(ExpertStandardErrors.ExpertWithIdNotFound);
        var values = response.Value?.Data;

        var expertIds = values!.Select(c => c.ExpertUnitId).Where(x => x != 0).ToList();
        var expertsData = await WebServicesLogic.SkillsDataReceiver(expertIds, _mediator, ct); // بره سراغ متا دیتا

        var data = new List<ExpertGetsByOperationInfoIdModel?>();
        if (values is not null)
            foreach (var item in values!)
            {
                var skill = expertsData?.Where(c => c?.Id == item?.ExpertUnitId).FirstOrDefault();
                data.Add(new ExpertGetsByOperationInfoIdModel(item.Id, item!.ExpertUnitId, skill?.Name, skill?.Code,
                    item!.ExpertNumber, TimeCalculator.TicksToStringHM(item.TimeSpant), item!.UnusedPercentage));
            }

        return new ExpertGetsByOprationInfoIdResponse(data, response.Value?.RowCount ?? 0);
    }
    #endregion

    #region Products
    public async Task<Result<CreateConsumptionStandardProductResponse?>> CreateProductStandard(
        CreateConsumptionStandardProductRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateGoods, Number:{Number}, UnusedPercentage:{UnusedPercentage},", request.GoodsNumber, request.UnusedPercentage);

        var isValidRequest = await request.IsValidAsync<CreateConsumptionStandardProductValidator, CreateConsumptionStandardProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateConsumptionStandardProductResponse>(isValidRequest.Error!);

        var operationInfo = await _mediator.Send(new GetOperationInfoByIdByProductsQuery(request.OperationInfoId), ct);
        if (operationInfo.IsFailure || operationInfo.Value is null)
            return Result.Failure<CreateConsumptionStandardProductResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);

        if (request.StandardProductType == StandardProductType.ProductGroup)
        {
            if (operationInfo.Value.ConsumptionStandardProduct.Where(x => x.StandardProductType == StandardProductType.ProductGroup)
                 .Any(x => x.ProductUnitId == request.Id))
                return Result.Failure<CreateConsumptionStandardProductResponse>(ProductStandardErrors.ProductIsDuplicate);

            var productData = await _mediator.Send(new GetGroupByIdQuery(request.Id), ct); // بره سراغ متا دیتا
            if (productData.IsFailure)
                return Result.Failure<CreateConsumptionStandardProductResponse>(ProductStandardErrors.ProductWithIdNotFound);
        }
        else if (request.StandardProductType == StandardProductType.Category)
        {
            if (operationInfo.Value.ConsumptionStandardProduct.Where(x => x.StandardProductType == StandardProductType.Category)
                .Any(x => x.ProductUnitId == request.Id))
                return Result.Failure<CreateConsumptionStandardProductResponse>(ProductStandardErrors.CategoryIsDuplicate);


            var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(request.Id), ct); // بره سراغ متا دیتا
            if (categoryData.IsFailure)
                return Result.Failure<CreateConsumptionStandardProductResponse>(ProductStandardErrors.CategoryWithIdNotFound);
        }
        else
            return Result.Failure<CreateConsumptionStandardProductResponse>(ProductStandardErrors.UnValidType);

        var response = await _mediator.Send(new CreateProductCommand(operationInfo.Value!, request.Id, request.GoodsNumber, request.UnusedPercentage, request.StandardProductType.Value, request.ProductAllowedType!.Value), ct);
        if (response.IsFailure)
            return Result.Failure<CreateConsumptionStandardProductResponse>(response.Error!);

        var setStandard = await _mediator.Send(new SetOperationInfoHaveStandardCommand(operationInfo.Value!.Id), ct);

        await _unitOfWork.CommitAsync(ct);
        return new CreateConsumptionStandardProductResponse(response.Value!.Id, true);
    }

    public async Task<Result<DisableConsumptionStandardProductResponse?>> DisableProductStandard(
        DisableConsumptionStandardProductRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableGoods, Id:{Id}", request.OperationInfoGoodsId);

        var isValidRequest = await request.IsValidAsync<DisableConsumptionStandardProductValidator, DisableConsumptionStandardProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableConsumptionStandardProductResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableProductCommand(request.OperationInfoGoodsId), ct);
        if (response.IsFailure)
            return Result.Failure<DisableConsumptionStandardProductResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableConsumptionStandardProductResponse(response.Value!.Id, response.Value!.ProductUnitId, response.Value!.IsDeleted);
    }

    public async Task<Result<UpdateConsumptionStandardProductResponse?>> UpdateProductStandard(
        UpdateConsumptionStandardProductRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateGoods, id:{Id}, Number:{Number} , UnusedPercentage:{UnusedPercentage},", request.Id, request.GoodsNumber, request.UnusedPercentage);

        var isValidRequest = await request.IsValidAsync<UpdateConsumptionStandardProductValidator, UpdateConsumptionStandardProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateConsumptionStandardProductResponse>(isValidRequest.Error!);

        if (request.StandardProductType == StandardProductType.ProductGroup)
        {
            var productData = await _mediator.Send(new GetGroupByIdQuery(request.Id), ct); // بره سراغ متا دیتا
            if (productData.IsFailure)
                return Result.Failure<UpdateConsumptionStandardProductResponse>(ProductStandardErrors.ProductWithIdNotFound);
        }
        else if (request.StandardProductType == StandardProductType.Category)
        {
            var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(request.Id), ct); // بره سراغ متا دیتا
            if (categoryData.IsFailure)
                return Result.Failure<UpdateConsumptionStandardProductResponse>(ProductStandardErrors.CategoryWithIdNotFound);
        }
        else
            return Result.Failure<UpdateConsumptionStandardProductResponse>(ProductStandardErrors.UnValidType);

        var response = await _mediator.Send(new UpdateProductCommand(request.OperationInfoGoodsId, request.Id, request.GoodsNumber, request.UnusedPercentage, request.StandardProductType.Value, request.ProductAllowedType!.Value), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateConsumptionStandardProductResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateConsumptionStandardProductResponse(response.Value!.Id, true);
    }

    public async Task<Result<GetsProductByOprationInfoIdResponse?>> GetsProductByOperationInfoId(
        GetsProductByOprationInfoIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GoodsGetsByOperationInfoId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProductByOprationInfoIdValidator, GetsProductByOprationInfoIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProductByOprationInfoIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProductByOperationInfoIdQuery(request.OprationInfoId, request.StandardProductType, request.ProductAllowedType, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProductByOprationInfoIdResponse>(response.Error!);
        var values = response.Value?.Data;

        List<Group>? goodsData = [];
        var goodsIds = values!.Where(x => x.StandardProductType == StandardProductType.ProductGroup).Select(c => c!.ProductUnitId).Where(x => x != 0).Distinct().ToList();
        if (goodsIds is not null && goodsIds.Count > 0)
            goodsData = await WebServicesLogic.GroupsDataReceiver(goodsIds, _mediator, ct);

        List<WarehouseCategory>? categoriesData = [];
        var categoriesIds = values!.Where(x => x.StandardProductType == StandardProductType.Category).Select(c => c!.ProductUnitId).Where(x => x != 0).Distinct().ToList();
        if (categoriesIds is not null && categoriesIds.Count > 0)
            categoriesData = await WebServicesLogic.CategoriesDataReceiver(categoriesIds, null, _mediator, ct);

        var data = new List<GetsProductByOperationInfoIdModels?>();
        if (values is not null)
            foreach (var item in values!)
            {
                if (item.StandardProductType == StandardProductType.ProductGroup)
                {
                    var goodData = goodsData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                    data.Add(new GetsProductByOperationInfoIdModels(item.Id, item!.ProductUnitId, goodData?.Name, goodData?.Code, goodData?.IsActive, goodData?.MeasureUnitId, goodData?.MeasureUnitName,
                        item!.Number, item!.UnusedPercentage, item.StandardProductType, item.StandardProductType.GetEnumDescription(), item.ProductAllowedType, item.ProductAllowedType.GetEnumDescription()));
                }

                if (item.StandardProductType == StandardProductType.Category)
                {
                    var categoryData = categoriesData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                    data.Add(new GetsProductByOperationInfoIdModels(item.Id, item!.ProductUnitId, categoryData?.Title, categoryData?.Code, categoryData?.IsActive, null, null, item!.Number, item!.UnusedPercentage,
                        item.StandardProductType, item.StandardProductType.GetEnumDescription(), item.ProductAllowedType, item.ProductAllowedType.GetEnumDescription()));
                }
            }

        return new GetsProductByOprationInfoIdResponse(data, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsNonStandardProductByOprationInfoIdResponse?>> GetsNonStandardProductByOprationInfoId(
        GetsNonStandardProductByOprationInfoIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsNonStandardProductByOprationInfoId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsNonStandardProductByOprationInfoIdValidator, GetsNonStandardProductByOprationInfoIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsNonStandardProductByOprationInfoIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsNonStandardProductByOprationInfoIdQuery(request.OprationInfoId, request.PageIndex, request.PageSize), ct);
        var values = response.Value?.Data;

        var data = new List<GetsNonStandardProductByOprationInfoIdModels?>();

        List<ConsumptionStandardProduct>? standardProducts = [];
        if (values is not null && values.Count > 0)
            if (values!.Any(x => x.ProductAllowedType == ProductAllowedType.NonStandard))
                standardProducts = values!.Where(x => x.ProductAllowedType == ProductAllowedType.NonStandard).ToList();

        var publicGroupsQuery = await _mediator.Send(new GetFilteredPublicGroupsQuery(null), ct);
        var publicGroups = publicGroupsQuery.Value?.Data;

        if (standardProducts is not null && standardProducts.Count > 0)
        {
            List<FilteredGroup>? goodsData = [];
            var goodsIds = standardProducts!.Where(x => x.StandardProductType == StandardProductType.ProductGroup).Select(c => c!.ProductUnitId).Where(x => x != 0).Distinct().ToList();
            goodsIds.AddRange(publicGroups?.Where(x => x.ProductGroupId > 0).Select(x => x.ProductGroupId).Distinct().ToList() ?? []);
            if (goodsIds is not null && goodsIds.Count > 0)
                goodsData = await WebServicesLogic.GetFilteredGroupsDataReceiver(goodsIds.Distinct().ToList(), request.FilterData, _mediator, ct);

            List<WarehouseCategory>? categoriesData = [];
            var categoriesIds = standardProducts!.Where(x => x.StandardProductType == StandardProductType.Category).Select(c => c!.ProductUnitId).Where(x => x != 0).Distinct().ToList();
            if (categoriesIds is not null && categoriesIds.Count > 0)
                categoriesData = await WebServicesLogic.CategoriesDataReceiver(categoriesIds, request.FilterData, _mediator, ct);

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var item in standardProducts)
                {
                    if (data.Any(x => x?.Id == item.ProductUnitId))
                        continue;

                    if (item.StandardProductType == StandardProductType.ProductGroup)
                    {
                        var goodData = goodsData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                        if (goodData != null)
                            data.Add(new GetsNonStandardProductByOprationInfoIdModels(item.Id, item!.ProductUnitId, goodData?.Name, goodData?.Code, goodData?.IsActive, goodData?.MeasureUnitId, goodData?.MeasureUnitName,
                                item!.Number, item!.UnusedPercentage, item.StandardProductType, item.StandardProductType.GetEnumDescription()));
                    }

                    if (item.StandardProductType == StandardProductType.Category)
                    {
                        var categoryData = categoriesData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                        if (categoryData != null)
                            data.Add(new GetsNonStandardProductByOprationInfoIdModels(item.Id, item!.ProductUnitId, categoryData?.Title, categoryData?.Code, categoryData?.IsActive, null, null, item!.Number, item!.UnusedPercentage,
                                item.StandardProductType, item.StandardProductType.GetEnumDescription()));
                    }
                }

                if (publicGroups is not null && publicGroups.Count > 0)
                    foreach (var item in publicGroups)
                    {
                        if (data.Any(x => x?.Id == item.ProductGroupId))
                            continue;
                        else
                        {
                            var goodData = goodsData?.FirstOrDefault(x => x.Id == item.ProductGroupId);
                            if (goodData != null)
                                data.Add(new GetsNonStandardProductByOprationInfoIdModels(null, item!.ProductGroupId, goodData?.Name, goodData?.Code, goodData?.IsActive, goodData?.MeasureUnitId, goodData?.MeasureUnitName,
                                    null, null, StandardProductType.ProductGroup, StandardProductType.ProductGroup.GetEnumDescription()));
                        }
                    }
            }
            else
            {
                foreach (var item in standardProducts)
                {
                    if (data.Any(x => x?.Id == item.ProductUnitId))
                        continue;

                    if (item.StandardProductType == StandardProductType.ProductGroup)
                    {
                        var goodData = goodsData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                        if (goodData != null)
                            data.Add(new GetsNonStandardProductByOprationInfoIdModels(item.Id, item!.ProductUnitId, goodData?.Name, goodData?.Code, goodData?.IsActive, goodData?.MeasureUnitId, goodData?.MeasureUnitName,
                                item!.Number, item!.UnusedPercentage, item.StandardProductType, item.StandardProductType.GetEnumDescription()));
                    }

                    if (item.StandardProductType == StandardProductType.Category)
                    {
                        var categoryData = categoriesData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                        if (categoryData != null)
                            data.Add(new GetsNonStandardProductByOprationInfoIdModels(item.Id, item!.ProductUnitId, categoryData?.Title, categoryData?.Code, categoryData?.IsActive, null, null, item!.Number, item!.UnusedPercentage,
                                item.StandardProductType, item.StandardProductType.GetEnumDescription()));
                    }
                }

                if (publicGroups is not null && publicGroups.Count > 0)
                    foreach (var item in publicGroups)
                    {
                        if (data.Any(x => x?.Id == item.ProductGroupId))
                            continue;
                        else
                        {
                            var goodData = goodsData?.FirstOrDefault(x => x.Id == item.ProductGroupId);
                            if (goodData != null)
                                data.Add(new GetsNonStandardProductByOprationInfoIdModels(null, item!.ProductGroupId, goodData?.Name, goodData?.Code, goodData?.IsActive, goodData?.MeasureUnitId, goodData?.MeasureUnitName,
                                    null, null, StandardProductType.ProductGroup, StandardProductType.ProductGroup.GetEnumDescription()));
                        }
                    }
            }
        }
        else if (standardProducts?.Count <= 0 && publicGroups is not null && publicGroups.Count > 0)
        {
            List<FilteredGroup>? goodsData = [];
            List<long>? goodsIds = publicGroups?.Where(x => x.ProductGroupId > 0).Select(x => x.ProductGroupId).Distinct().ToList();
            if (goodsIds is not null && goodsIds.Count > 0)
                goodsData = await WebServicesLogic.GetFilteredGroupsDataReceiver(goodsIds.Distinct().ToList(), request.FilterData, _mediator, ct);

            if (publicGroups is not null && publicGroups.Count > 0)
                foreach (var item in publicGroups)
                {
                    if (data.Any(x => x?.Id == item.ProductGroupId))
                        continue;
                    else
                    {
                        var goodData = goodsData?.FirstOrDefault(x => x.Id == item.ProductGroupId);
                        if (goodData != null)
                            data.Add(new GetsNonStandardProductByOprationInfoIdModels(null, item!.ProductGroupId, goodData?.Name, goodData?.Code, goodData?.IsActive, goodData?.MeasureUnitId, goodData?.MeasureUnitName,
                                null, null, StandardProductType.ProductGroup, StandardProductType.ProductGroup.GetEnumDescription()));
                    }
                }
        }
        else
        {
            var groupResponse = await _mediator.Send(new GetActiveGroupsQuery(request.FilterData, 1, 1000), ct);
            if (groupResponse.IsFailure || groupResponse.Value is null || groupResponse.Value.Data is null)
                return Result.Failure<GetsNonStandardProductByOprationInfoIdResponse>(groupResponse.Error!);
            var groups = groupResponse.Value!.Data;

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var item in groups)
                {
                    if (data.Any(x => x?.Id == item.Id))
                        continue;

                    data.Add(new GetsNonStandardProductByOprationInfoIdModels(null, item.Id, item.Name, item.Code, true, item.MeasureUnitId, item.MeasureUnitName,
                        null, null, StandardProductType.ProductGroup, StandardProductType.ProductGroup.GetEnumDescription()));
                }
            }
            else
            {
                foreach (var item in groups)
                {
                    if (data.Any(x => x?.Id == item.Id))
                        continue;

                    data.Add(new GetsNonStandardProductByOprationInfoIdModels(null, item.Id, item.Name, item.Code, true, item.MeasureUnitId, item.MeasureUnitName,
                        null, null, StandardProductType.ProductGroup, StandardProductType.ProductGroup.GetEnumDescription()));
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsNonStandardProductByOprationInfoIdResponse(responseData ?? new List<GetsNonStandardProductByOprationInfoIdModels?>(0), data?.Count ?? 0);
    }

    public async Task<Result<GetProductAllowedTypesResponse?>> GetProductAllowedTypes(
        GetProductAllowedTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProductAllowedTypes");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ProductAllowedType>());
        return new GetProductAllowedTypesResponse(response);
    }

    public async Task<Result<GetStandardProductTypesResponse?>> GetStandardProductTypes(
        GetStandardProductTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for StandardProductTypes");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<StandardProductType>());
        return new GetStandardProductTypesResponse(response);
    }

    #endregion

    #region Machineries

    public async Task<Result<CreateConsumptionStandardMachineryResponse?>> CreateMachineryStandard(
        CreateConsumptionStandardMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateMachinery, MachineryNumber:{MachineryNumber}, UnusedPercentage:{UnusedPercentage},", request.MachineryNumber, request.UnusedPercentage);

        var isValidRequest = await request.IsValidAsync<CreateConsumptionStandardMachineryValidator, CreateConsumptionStandardMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateConsumptionStandardMachineryResponse>(isValidRequest.Error!);

        var operationInfo = await _mediator.Send(new GetOperationInfoByIdByMachineriesQuery(request.OperationInfoId), ct);
        if (operationInfo.IsFailure)
            return Result.Failure<CreateConsumptionStandardMachineryResponse>(operationInfo.Error!);
        if (operationInfo.Value is null)
            return Result.Failure<CreateConsumptionStandardMachineryResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
        if (operationInfo.Value.ConsumptionStandardMachineries.Any(x => x.Machinery.Id == request.Id))
            return Result.Failure<CreateConsumptionStandardMachineryResponse>(MachineryStandardErrors.MachineryIsDuplicate);

        if (!string.IsNullOrEmpty(request.TimeSpant))
            if (!(request.TimeSpant.Split(':')[0].Count() >= 2 && request.TimeSpant.Split(':')[1].Count() == 2))
                return Result.Failure<CreateConsumptionStandardMachineryResponse>(MachineryStandardErrors.TimeSpantCountError);

        if (int.Parse(request.TimeSpant.Split(':')[1]) > 59)
            return Result.Failure<CreateConsumptionStandardMachineryResponse>(MachineryStandardErrors.MoreThan59Min);

        var machineryData = await _mediator.Send(new GetMachineryByIdQuery(request.Id), ct); // بره سراغ متا دیتا
        if (machineryData.IsFailure)
            return Result.Failure<CreateConsumptionStandardMachineryResponse>(MachineryStandardErrors.MachineryWithIdNotFound);

        var response = await _mediator.Send(new CreateMachineryCommand(operationInfo.Value!, machineryData.Value!, request.MachineryNumber, TimeCalculator.StringToTicks(request.TimeSpant), request.UnusedPercentage), ct);
        if (response.IsFailure)
            return Result.Failure<CreateConsumptionStandardMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateConsumptionStandardMachineryResponse(response.Value!.Id, true);
    }

    public async Task<Result<DisableConsumptionStandardMachineryResponse?>> DisableMachineryStandard(
        DisableConsumptionStandardMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableMachinery, Id:{Id}", request.OperationInfoMachineryId);

        var isValidRequest = await request.IsValidAsync<DisableConsumptionStandardMachineryValidator, DisableConsumptionStandardMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableConsumptionStandardMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableMachineryCommand(request.OperationInfoMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<DisableConsumptionStandardMachineryResponse>(response.Error!);

        var machinery = response.Value!;
        var setStandard = await _mediator.Send(new SetOperationInfoHaveStandardCommand(machinery.OperationInfo!.Id), ct);

        await _unitOfWork.CommitAsync(ct);
        return new DisableConsumptionStandardMachineryResponse(machinery.Id, machinery.IsDeleted);
    }

    public async Task<Result<UpdateConsumptionStandardMachineryResponse?>> UpdateMachineryStandard(
        UpdateConsumptionStandardMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateMachinery, id:{MachineryId}, MachineryNumber:{MachineryNumber} , UnusedPercentage:{UnusedPercentage},", request.Id, request.MachineryNumber, request.UnusedPercentage);

        var isValidRequest = await request.IsValidAsync<UpdateConsumptionStandardMachineryValidator, UpdateConsumptionStandardMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateConsumptionStandardMachineryResponse>(isValidRequest.Error!);

        var machineryData = await _mediator.Send(new GetMachineryByIdQuery(request.Id), ct); // بره سراغ متا دیتا
        if (machineryData.IsFailure)
            return Result.Failure<UpdateConsumptionStandardMachineryResponse>(MachineryStandardErrors.MachineryWithIdNotFound);

        if (!string.IsNullOrEmpty(request.TimeSpant))
            if (!(request.TimeSpant.Split(':')[0].Count() >= 2 && request.TimeSpant.Split(':')[1].Count() == 2))
                return Result.Failure<UpdateConsumptionStandardMachineryResponse>(MachineryStandardErrors.TimeSpantCountError);

        if (int.Parse(request.TimeSpant.Split(':')[1]) > 59)
            return Result.Failure<UpdateConsumptionStandardMachineryResponse>(MachineryStandardErrors.MoreThan59Min);

        var response = await _mediator.Send(new UpdateMachineryCommand(request.OperationInfoMachineryId, machineryData.Value!, request.MachineryNumber, TimeCalculator.StringToTicks(request.TimeSpant), request.UnusedPercentage), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateConsumptionStandardMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateConsumptionStandardMachineryResponse(response.Value!.Id, true);
    }

    public async Task<Result<MachineryGetsByOprationInfoIdResponse?>> MachineryGetsByOperationInfoId(
        MachineryGetsByOprationInfoIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineryGetsByOperationInfoId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<MachineryGetsByOprationInfoIdValidator, MachineryGetsByOprationInfoIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineryGetsByOprationInfoIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new MachineryGetsByOperationInfoIdQuery(request.OprationInfoId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<MachineryGetsByOprationInfoIdResponse>(response.Error!);

        var data = new List<MachineryGetsByOperationInfoIdModel?>();
        if (response.Value?.Data is not null)
        {
            foreach (var item in response.Value?.Data!)
                data.Add(new MachineryGetsByOperationInfoIdModel(item.Id, item!.Machinery.Id, item.Machinery.MachineryName, item.Machinery.MachineryCode,
                    item!.MachineryNumber, TimeCalculator.TicksToStringHM(item.TimeSpant), item!.UnusedPercentage));
        }

        return new MachineryGetsByOprationInfoIdResponse(data, response.Value?.RowCount ?? 0);
    }

    #endregion

    #region ConsumptionStandards

    public async Task<Result<UpdateConsumptionStandardsResponse?>> UpdateConsumptionStandards(
        UpdateConsumptionStandardsRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineryGetsByOperationInfoId, OprationInfoId:{OprationInfoId}", request.OprationInfoId);

        var isValidRequest = await request.IsValidAsync<UpdateConsumptionStandardsValidator, UpdateConsumptionStandardsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateConsumptionStandardsResponse>(isValidRequest.Error!);

        var operationInfoResponse = await _mediator.Send(new GetOperationInfoByIdByConsumptionStandardsQuery(request.OprationInfoId), ct);
        if (operationInfoResponse.IsFailure)
            return Result.Failure<UpdateConsumptionStandardsResponse>(operationInfoResponse.Error!);
        if (operationInfoResponse.Value is null)
            return Result.Failure<UpdateConsumptionStandardsResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
        var operationInfo = operationInfoResponse.Value;

        if (request.ExpertStandards is not null && request.ExpertStandards?.Count > 0)
        {
            var expertStandards = request.ExpertStandards.OrderByDescending(x => (x.IsDeleted is null || x.IsDeleted == false) && x.OperationInfoExpertId != null).ThenByDescending(x => x.IsDeleted).ToList();

            var expertIds = expertStandards.Where(x => x.IsDeleted != true).Select(c => c.Id).Where(x => x != 0).ToList();
            if (expertIds.Count > 0)
            {
                var expertsData = await _mediator.Send(new GetsSkillByIdQuery(expertIds, true, 1, expertIds.Count), ct); // بره سراغ متا دیتا
                if (expertsData.IsFailure)
                    return Result.Failure<UpdateConsumptionStandardsResponse>(OperationInfoErrors.UnValidExperts);
            }

            foreach (var item in expertStandards)
            {
                if (operationInfo.ConsumptionStandardExperts.Any(x => x.ExpertUnitId == item.Id && item.OperationInfoExpertId is null))
                    return Result.Failure<UpdateConsumptionStandardsResponse>(ExpertStandardErrors.ExpertIsDuplicate);
                if (item.OperationInfoExpertId is null && item.IsDeleted == true)
                    return Result.Failure<UpdateConsumptionStandardsResponse>(ExpertStandardErrors.CantDelete);
                if (!string.IsNullOrEmpty(item.TimeSpant))
                    if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                        return Result.Failure<UpdateConsumptionStandardsResponse>(MachineryStandardErrors.TimeSpantCountError);

                if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                    return Result.Failure<UpdateConsumptionStandardsResponse>(MachineryStandardErrors.MoreThan59Min);

                if (item.IsDeleted == true)
                {
                    var deleteExpert = await _mediator.Send(new DisableExpertCommand((long)item.OperationInfoExpertId!), ct);
                    if (deleteExpert.IsFailure)
                        return Result.Failure<UpdateConsumptionStandardsResponse>(deleteExpert.Error!);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.OperationInfoExpertId is not null)
                {
                    var updateExpert = await _mediator.Send(new UpdateExpertCommand((long)item.OperationInfoExpertId, item.Id, item.ExpertNumber, TimeCalculator.StringToTicks(item.TimeSpant), item.UnusedPercentage), ct);
                    if (updateExpert.IsFailure)
                        return Result.Failure<UpdateConsumptionStandardsResponse>(updateExpert.Error!);
                }
                else if (item.OperationInfoExpertId is null)
                {
                    var createExpert = await _mediator.Send(new CreateExpertCommand(operationInfo!, item.Id, item.ExpertNumber, TimeCalculator.StringToTicks(item.TimeSpant), item.UnusedPercentage), ct);
                    if (createExpert.IsFailure)
                        return Result.Failure<UpdateConsumptionStandardsResponse>(createExpert.Error!);
                }
            }
        }

        if (request.MachineryStandards is not null && request.MachineryStandards?.Count > 0)
        {
            var machineryStandards = request.MachineryStandards.OrderByDescending(x => (x.IsDeleted is null || x.IsDeleted == false) && x.OperationInfoMachineryId != null).ThenByDescending(x => x.IsDeleted).ToList();

            var machineryIds = machineryStandards.Where(x => x.IsDeleted != true).Select(c => c.Id).Where(x => x != 0).ToList();
            var machineriesData = new List<Machinery?>();
            if (machineryIds.Count > 0)
            {
                var responseData = await _mediator.Send(new GetsMachineryByIdsQuery(machineryIds, 1, machineryIds.Count), ct); // بره سراغ متا دیتا
                if (responseData.IsFailure)
                    return Result.Failure<UpdateConsumptionStandardsResponse>(OperationInfoErrors.UnValidMachineries);
                machineriesData = responseData.Value?.Data;
            }

            foreach (var item in machineryStandards)
            {
                if (operationInfo.ConsumptionStandardMachineries.Any(x => x.Machinery.Id == item.Id && item.OperationInfoMachineryId is null))
                    return Result.Failure<UpdateConsumptionStandardsResponse>(MachineryStandardErrors.MachineryIsDuplicate);
                if (item.OperationInfoMachineryId is null && item.IsDeleted == true)
                    return Result.Failure<UpdateConsumptionStandardsResponse>(MachineryStandardErrors.CantDelete);
                if (!string.IsNullOrEmpty(item.TimeSpant))
                    if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                        return Result.Failure<UpdateConsumptionStandardsResponse>(MachineryStandardErrors.TimeSpantCountError);

                if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                    return Result.Failure<UpdateConsumptionStandardsResponse>(MachineryStandardErrors.MoreThan59Min);

                if (item.IsDeleted == true)
                {
                    var deleteMachinery = await _mediator.Send(new DisableMachineryCommand((long)item.OperationInfoMachineryId!), ct);
                    if (deleteMachinery.IsFailure)
                        return Result.Failure<UpdateConsumptionStandardsResponse>(deleteMachinery.Error!);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.OperationInfoMachineryId is not null)
                {
                    var machineryData = machineriesData?.Where(x => x!.Id == item.Id).FirstOrDefault();
                    var updateMachinery = await _mediator.Send(new UpdateMachineryCommand((long)item.OperationInfoMachineryId, machineryData!, item.MachineryNumber, TimeCalculator.StringToTicks(item.TimeSpant), item.UnusedPercentage), ct);
                    if (updateMachinery.IsFailure)
                        return Result.Failure<UpdateConsumptionStandardsResponse>(updateMachinery.Error!);
                }
                else if (item.OperationInfoMachineryId is null)
                {
                    var machineryData = machineriesData?.Where(x => x!.Id == item.Id).FirstOrDefault();
                    var createMachinery = await _mediator.Send(new CreateMachineryCommand(operationInfo!, machineryData!, item.MachineryNumber, TimeCalculator.StringToTicks(item.TimeSpant), item.UnusedPercentage), ct);
                    if (createMachinery.IsFailure)
                        return Result.Failure<UpdateConsumptionStandardsResponse>(createMachinery.Error!);
                }
            }
        }

        if (request.GoodsStandards is not null && request.GoodsStandards?.Count > 0)
        {
            var goodsStandards = request.GoodsStandards.OrderByDescending(x => (x.IsDeleted is null || x.IsDeleted == false) && x.OperationInfoGoodsId != null).ThenByDescending(x => x.IsDeleted).ToList();

            var productIds = goodsStandards.Where(x => x.IsDeleted != true && x.StandardProductType == StandardProductType.ProductGroup).Select(c => c!.Id).Where(x => x != 0).Distinct().ToList();
            if (productIds != null && productIds.Count > 0)
            {
                var productsData = await _mediator.Send(new GetGroupsByIdsQuery(productIds, 1, productIds.Count), ct); // بره سراغ انبار
                if (productsData.IsFailure)
                    return Result.Failure<UpdateConsumptionStandardsResponse>(OperationInfoErrors.UnValidProducts);
            }

            var categoryIds = goodsStandards.Where(x => x.IsDeleted != true && x.StandardProductType == StandardProductType.Category).Select(c => c!.Id).Where(x => x != 0).Distinct().ToList();
            if (categoryIds != null && categoryIds.Count > 0)
            {
                var ids = categoryIds.Adapt<List<long?>>();
                var categoriesData = await _mediator.Send(new GetsWarehouseCategoryByIdQuery(1, ids.Count, ids, false, null), ct); // بره سراغ انبار
                if (categoriesData.IsFailure)
                    return Result.Failure<UpdateConsumptionStandardsResponse>(OperationInfoErrors.UnValidCategories);
            }

            foreach (var item in goodsStandards)
            {
                if (operationInfo.ConsumptionStandardProduct.Where(x => x.StandardProductType == StandardProductType.ProductGroup)
                    .Any(x => x.ProductUnitId == item.Id && item.OperationInfoGoodsId is null))
                    return Result.Failure<UpdateConsumptionStandardsResponse>(ProductStandardErrors.ProductIsDuplicate);

                if (operationInfo.ConsumptionStandardProduct.Where(x => x.StandardProductType == StandardProductType.Category)
                    .Any(x => x.ProductUnitId == item.Id && item.OperationInfoGoodsId is null))
                    return Result.Failure<UpdateConsumptionStandardsResponse>(ProductStandardErrors.CategoryIsDuplicate);

                if (item.OperationInfoGoodsId is null && item.IsDeleted == true)
                    return Result.Failure<UpdateConsumptionStandardsResponse>(ProductStandardErrors.CantDelete);

                if (item.IsDeleted == true)
                {
                    var deleteProduct = await _mediator.Send(new DisableProductCommand((long)item.OperationInfoGoodsId!), ct);
                    if (deleteProduct.IsFailure)
                        return Result.Failure<UpdateConsumptionStandardsResponse>(deleteProduct.Error!);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.OperationInfoGoodsId is not null)
                {
                    var updateProduct = await _mediator.Send(new UpdateProductCommand((long)item.OperationInfoGoodsId, item.Id!, item.GoodsNumber, item.UnusedPercentage, item.StandardProductType!.Value, item.ProductAllowedType!.Value), ct);
                    if (updateProduct.IsFailure)
                        return Result.Failure<UpdateConsumptionStandardsResponse>(updateProduct.Error!);
                }
                else if (item.OperationInfoGoodsId is null)
                {
                    var createProduct = await _mediator.Send(new CreateProductCommand(operationInfo!, item.Id!, item.GoodsNumber, item.UnusedPercentage, item.StandardProductType!.Value, item.ProductAllowedType!.Value), ct);
                    if (createProduct.IsFailure)
                        return Result.Failure<UpdateConsumptionStandardsResponse>(createProduct.Error!);
                }
            }
        }

        var setStandard = await _mediator.Send(new SetOperationInfoHaveStandardCommand(operationInfo!.Id), ct);
        await _unitOfWork.CommitAsync(ct);
        return new UpdateConsumptionStandardsResponse(true);
    }

    public async Task<Result<GetsConsumptionStandardByOperationInfoIdResponse?>> GetsConsumptionStandardByOperationInfoId(
        GetsConsumptionStandardByOperationInfoIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineryGetsByOperationInfoId, OprationInfoId:{OprationInfoId}", request.OprationInfoId);

        var isValidRequest = await request.IsValidAsync<GetsConsumptionStandardByOperationInfoIdValidator, GetsConsumptionStandardByOperationInfoIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsConsumptionStandardByOperationInfoIdResponse>(isValidRequest.Error!);

        var operationInfoResponse = await _mediator.Send(new HaveOperationInfoChildQuery(request.OprationInfoId), ct);
        if (operationInfoResponse.IsFailure)
            return Result.Failure<GetsConsumptionStandardByOperationInfoIdResponse>(operationInfoResponse.Error!);
        if (operationInfoResponse.Value is null)
            return Result.Failure<GetsConsumptionStandardByOperationInfoIdResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
        var operationInfo = operationInfoResponse.Value;

        //Get Experts
        List<OperationInfoExpertDataModel?> expertsData = new List<OperationInfoExpertDataModel?>();
        if (operationInfo.ConsumptionStandardExperts is not null)
            if (operationInfo.ConsumptionStandardExperts.Count > 0)
            {
                var expertIds = operationInfo.ConsumptionStandardExperts.Select(c => c.ExpertUnitId).Where(x => x != 0).ToList();
                var experts = await WebServicesLogic.SkillsDataReceiver(expertIds, _mediator, ct); // بره سراغ متا دیتا
                foreach (var item in operationInfo.ConsumptionStandardExperts)
                {
                    var expert = experts?.Where(c => c?.Id == item?.ExpertUnitId).FirstOrDefault();
                    expertsData.Add(new OperationInfoExpertDataModel(item.Id, item!.ExpertUnitId, expert?.Name, expert?.Code, item!.ExpertNumber, TimeCalculator.TicksToStringHM(item.TimeSpant), item!.UnusedPercentage));
                }
            }
        //Get Machineries
        List<OperationInfoMachineryDataModel?> machinerysData = new List<OperationInfoMachineryDataModel?>();
        if (operationInfo.ConsumptionStandardMachineries is not null)
            if (operationInfo.ConsumptionStandardMachineries.Count > 0)
            {
                foreach (var item in operationInfo.ConsumptionStandardMachineries)
                    machinerysData.Add(new OperationInfoMachineryDataModel(item.Id, item!.Machinery.Id, item.Machinery.MachineryName, item.Machinery.MachineryCode,
                        item.MachineryNumber, TimeCalculator.TicksToStringHM(item.TimeSpant), item!.UnusedPercentage));
            }
        //Get Goods
        List<OperationInfoGoodsDataModel?> productsData = new List<OperationInfoGoodsDataModel?>();
        if (operationInfo.ConsumptionStandardProduct is not null && operationInfo.ConsumptionStandardProduct.Count > 0)
        {
            var goodsIds = operationInfo.ConsumptionStandardProduct!.Where(x => x.StandardProductType == StandardProductType.ProductGroup).Select(c => c!.ProductUnitId).Where(x => x != 0).Distinct().ToList();
            var goodsData = await WebServicesLogic.GroupsDataReceiver(goodsIds, _mediator, ct);

            var categoriesIds = operationInfo.ConsumptionStandardProduct!.Where(x => x.StandardProductType == StandardProductType.Category).Select(c => c!.ProductUnitId).Where(x => x != 0).Distinct().ToList();
            var categoriesData = await WebServicesLogic.CategoriesDataReceiver(categoriesIds, null, _mediator, ct);

            foreach (var item in operationInfo.ConsumptionStandardProduct)
            {
                if (item.StandardProductType == StandardProductType.ProductGroup)
                {
                    var goodData = goodsData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                    productsData.Add(new OperationInfoGoodsDataModel(item.Id, item!.ProductUnitId, goodData?.Name, goodData?.Code, goodData?.IsActive, item!.Number, item!.UnusedPercentage, goodData?.MeasureUnitId,
                        goodData?.MeasureUnitName, item.StandardProductType, item.StandardProductType.GetEnumDescription(), item.ProductAllowedType, item.ProductAllowedType.GetEnumDescription()));
                }

                if (item.StandardProductType == StandardProductType.Category)
                {
                    var categoryData = categoriesData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                    productsData.Add(new OperationInfoGoodsDataModel(item.Id, item!.ProductUnitId, categoryData?.Title, categoryData?.Code, categoryData?.IsActive, item!.Number, item!.UnusedPercentage, null, null,
                        item.StandardProductType, item.StandardProductType.GetEnumDescription(), item.ProductAllowedType, item.ProductAllowedType.GetEnumDescription()));
                }
            }
        }
        return new GetsConsumptionStandardByOperationInfoIdResponse(expertsData, productsData, machinerysData);
    }

    #endregion

}
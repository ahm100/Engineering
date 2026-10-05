using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.ConsumableVolumes.Commands.Experts.CreateExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Experts.DeleteExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Experts.UpdateExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.CreateMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.DeleteMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.UpdateMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Products.CreateProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Products.DeleteProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Products.UpdateProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.CreateExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DeleteExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertConsumableVolumeById;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationIds;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.UpdateExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.GetProjectOperationDetailVolumes;
using Engineering.Application.Services.ConsumableVolumes.Models.GetVolumeProductTypes;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.CreateMachineryConsumable;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DataModels;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DeleteMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetFilteredTotalOfConsumebleMachineries;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineryConsumableVolumeById;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetsFilteredMachineriyVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.UpdateMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.CreateProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.DeleteProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductConsumableVolumeById;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetsProductsByFiltered;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.UpdateProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.UpdateConsumableVolumes;
using Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertConsumableVolumeById;
using Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertsByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertsByProjectOperationIds;
using Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Queries.GetProjectOperationDetailVolumes;
using Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetConsumableVolumeMachineryById;
using Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetFilteredTotalOfConsumebleMachineries;
using Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetMachineriesByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetMachineriesByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetsFilteredMachineriyVolume;
using Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductConsumableVolumeById;
using Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductsByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductsByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsProductsByFiltered;
using Engineering.Application.Services.Machineries.Queries.GetMachineryById;
using Engineering.Application.Services.Machineries.Queries.GetsMachineryByIds;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithStandards;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillById;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetWarehouseCategoryById;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using MathNet.Numerics;

namespace Engineering.Application.Services.ConsumableVolumes;

public class ConsumableVolumeLogic : IConsumableVolumeLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ConsumableVolumeLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ConsumableVolumeLogic(
        IMediator mediator,
        ILogger<ConsumableVolumeLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateConsumableVolumesResponse?>> UpdateConsumableVolumes(
        UpdateConsumableVolumesRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateConsumableVolumes, ProjectOperationDetailId:{ProjectOperationDetailId},", request.ProjectOperationDetailId);
        //Validate data
        var isValidRequest = await request.IsValidAsync<UpdateConsumableVolumesValidator, UpdateConsumableVolumesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateConsumableVolumesResponse>(isValidRequest.Error!);
        //Find ProjectOperationDetail
        var projectOperationDetailResponse = await _mediator.Send(new GetProjectOperationDetailWithStandardsQuery(request.ProjectOperationDetailId), ct);
        if (projectOperationDetailResponse.IsFailure)
            return Result.Failure<UpdateConsumableVolumesResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var projectOperationDetail = projectOperationDetailResponse.Value!;

        if (request.ExpertRequests is not null && request.ExpertRequests.Count > 0)
        {
            var volumeExperts = request.ExpertRequests.OrderByDescending(x => x.Id != null).ToList();
            volumeExperts = volumeExperts.OrderByDescending(x => x.IsDeleted).ToList();

            foreach (var item in volumeExperts)
            {
                if (item.IsDeleted is not null && item.IsDeleted == true)
                {
                    if (item.Id is not null)
                    {
                        var deleteData = await _mediator.Send(new DeleteConsumableVolumeExpertCommand((long)item.Id), ct);
                        if (deleteData.IsFailure)
                            return Result.Failure<UpdateConsumableVolumesResponse>(deleteData.Error!);
                        continue;
                    }
                    else
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeExpertErrors.CanNotDelete);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null)
                {
                    var consumableVolumeExpert = await _mediator.Send(new GetConsumableVolumeExpertByIdQuery((long)item.Id), ct);
                    if (consumableVolumeExpert.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeExpertErrors.NoHaveExperts);
                    var expert = consumableVolumeExpert.Value!;
                    //Validate Experts
                    var expertData = await _mediator.Send(new GetSkillByIdQuery(item.ExpertId), ct); // بره سراغ متا دیتا
                    if (expertData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeExpertErrors.UnValidExperts);

                    var standardData = projectOperationDetail.ProjectOperation!.OperationInfo.ConsumptionStandardExperts.Where(x => x.ExpertUnitId == item?.ExpertId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;
                    long standardValue = 0;
                    if (isStandard == true)
                        standardValue = standardData!.TimeSpant;

                    var finalValue = TimeCalculator.StringToTicks(item!.FinalValue);

                    var updateData = await _mediator.Send(new UpdateConsumableVolumeExpertCommand(consumableVolumeExpert.Value!, item.ExpertId,
                        item.Number, item.UnusedPercentage, isStandard, standardValue, finalValue), ct);
                    if (updateData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(updateData.Error!);
                }
                else if (item.Id is null)
                {
                    var expertData = await _mediator.Send(new GetSkillByIdQuery(item.ExpertId), ct); // بره سراغ متا دیتا
                    if (expertData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeExpertErrors.UnValidExperts);
                    if (projectOperationDetail.ConsumableVolumeExperts.Any(x => x.ExpertId == item.ExpertId))
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeExpertErrors.AvailableId);

                    var standardData = projectOperationDetail.ProjectOperation!.OperationInfo.ConsumptionStandardExperts.Where(x => x.ExpertUnitId == item?.ExpertId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;
                    long standardValue = 0;
                    if (isStandard == true)
                        standardValue = standardData!.TimeSpant;

                    var finalValue = TimeCalculator.StringToTicks(item!.FinalValue);

                    var createData = await _mediator.Send(new CreateConsumableVolumeExpertCommand(projectOperationDetail, item.ExpertId, item.Number, item.UnusedPercentage, isStandard, standardValue, finalValue), ct);
                    if (createData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(createData.Error!);
                }
            }
        }

        if (request.MachineryRequests is not null && request.MachineryRequests.Count > 0)
        {
            var volumeMachineries = request.MachineryRequests.OrderByDescending(x => x.Id != null).ToList();
            volumeMachineries = volumeMachineries.OrderByDescending(x => x.IsDeleted).ToList();

            foreach (var item in volumeMachineries)
            {
                if (item.IsDeleted is not null && item.IsDeleted == true)
                {
                    if (item.Id is not null)
                    {
                        var deleteData = await _mediator.Send(new DeleteConsumableVolumeMachineryCommand((long)item.Id), ct);
                        if (deleteData.IsFailure)
                            return Result.Failure<UpdateConsumableVolumesResponse>(deleteData.Error!);
                        continue;
                    }
                    else
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeMachineryErrors.CanNotDelete);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null)
                {
                    var consumableVolumeMachinery = await _mediator.Send(new GetConsumableVolumeMachineryByIdQuery((long)item.Id), ct);
                    if (consumableVolumeMachinery.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeMachineryErrors.NoHaveMachineries);
                    var machinery = consumableVolumeMachinery.Value!;
                    //Validate Machinerys
                    var machineryData = await _mediator.Send(new GetMachineryByIdQuery(item.MachineryId), ct); // بره سراغ متا دیتا
                    if (machineryData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeMachineryErrors.UnValidMachinerys);

                    var standardData = projectOperationDetail.ProjectOperation!.OperationInfo.ConsumptionStandardMachineries.Where(x => x.Machinery?.Id == item?.MachineryId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;
                    long standardValue = 0;
                    if (isStandard == true)
                        standardValue = standardData!.TimeSpant;

                    var finalValue = (item.FinalValue.Contains(":") && item.Unit != null && item.Unit == RequestMachineryUnit.Hourly) ||
                         (Convert.ToDecimal(item.FinalValue) > 1000000) ?
                         Convert.ToDecimal(TimeCalculator.StringToTicks(item!.FinalValue)) : Convert.ToDecimal(item.FinalValue);

                    var updateData = await _mediator.Send(new UpdateConsumableVolumeMachineryCommand(consumableVolumeMachinery.Value!,
                        machineryData.Value!, item.Number, item.UnusedPercentage, isStandard, standardValue, Convert.ToDecimal(finalValue), item.Unit), ct);
                    if (updateData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(updateData.Error!);
                }
                else if (item.Id is null)
                {
                    var machineryData = await _mediator.Send(new GetMachineryByIdQuery(item.MachineryId), ct); // بره سراغ متا دیتا
                    if (machineryData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeMachineryErrors.UnValidMachinerys);
                    if (projectOperationDetail.ConsumableVolumeMachineries.Any(x => x.Machinery.Id == item.MachineryId))
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeMachineryErrors.AvailableId);

                    var standardData = projectOperationDetail.ProjectOperation!.OperationInfo.ConsumptionStandardMachineries.Where(x => x.Machinery.Id == item?.MachineryId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;
                    long standardValue = 0;
                    if (isStandard == true)
                        standardValue = standardData!.TimeSpant;

                    var finalValue = (item.FinalValue.Contains(":") && item.Unit != null && item.Unit == RequestMachineryUnit.Hourly) ||
                            (Convert.ToDecimal(item.FinalValue) > 1000000) ?
                            Convert.ToDecimal(TimeCalculator.StringToTicks(item!.FinalValue)) : Convert.ToDecimal(item.FinalValue);

                    var createData = await _mediator.Send(new CreateConsumableVolumeMachineryCommand(projectOperationDetail,
                        machineryData.Value!, item.Number, item.UnusedPercentage, isStandard, standardValue, Convert.ToDecimal(finalValue), item.Unit), ct);
                    if (createData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(createData.Error!);
                }
            }
        }

        if (request.ProductRequests is not null && request.ProductRequests.Count > 0)
        {
            var volumeProducts = request.ProductRequests.OrderByDescending(x => x.Id != null).ToList();
            volumeProducts = volumeProducts.OrderByDescending(x => x.IsDeleted).ToList();

            foreach (var item in volumeProducts)
            {
                if (item.IsDeleted is not null && item.IsDeleted == true)
                {
                    if (item.Id is not null)
                    {
                        var deleteData = await _mediator.Send(new DeleteConsumableVolumeProductCommand((long)item.Id), ct);
                        if (deleteData.IsFailure)
                            return Result.Failure<UpdateConsumableVolumesResponse>(deleteData.Error!);
                        continue;
                    }
                    else
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.CanNotDelete);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null)
                {
                    var consumableVolumeProduct = await _mediator.Send(new GetConsumableVolumeProductByIdQuery((long)item.Id), ct);
                    if (consumableVolumeProduct.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.NoHaveProducts);

                    if (ValidateConsumableVolumeProduct(consumableVolumeProduct.Value!, item.FinalValue, item.UnusedPercentage ?? 0))
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.UnValidForSupplies);

                    if (item.VolumeProductType == VolumeProductType.ProductGroup)
                    {
                        var productData = await _mediator.Send(new GetGroupByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                        if (productData.IsFailure)
                            return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.UnValidProducts);
                    }
                    else if (item.VolumeProductType == VolumeProductType.Category)
                    {
                        var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                        if (categoryData.IsFailure)
                            return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.UnValidCategories);
                    }
                    else
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.UnValidType);

                    var standardData = projectOperationDetail.ProjectOperation!.OperationInfo.ConsumptionStandardProduct.Where(x => x.ProductUnitId == item?.ProductGroupId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;

                    decimal standardValue = 0;
                    if (isStandard == true)
                        standardValue = standardData!.Number * projectOperationDetail.FinalAmount;

                    var updateData = await _mediator.Send(new UpdateConsumableVolumeProductCommand(consumableVolumeProduct.Value!, item.ProductGroupId,
                        item.UnusedPercentage, isStandard, standardValue, item.FinalValue, item.VolumeProductType!.Value), ct);
                    if (updateData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(updateData.Error!);
                }
                else if (item.Id is null)
                {
                    if (item.VolumeProductType == VolumeProductType.ProductGroup)
                    {
                        if (projectOperationDetail.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup).Any(x => x.ProductGroupId == item.ProductGroupId))
                            return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.AvailableId);

                        var productData = await _mediator.Send(new GetGroupByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                        if (productData.IsFailure)
                            return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.UnValidProducts);
                    }
                    else if (item.VolumeProductType == VolumeProductType.Category)
                    {
                        if (projectOperationDetail.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.Category).Any(x => x.ProductGroupId == item.ProductGroupId))
                            return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.AvailableId);

                        var productData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                        if (productData.IsFailure)
                            return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.UnValidCategories);
                    }
                    else
                        return Result.Failure<UpdateConsumableVolumesResponse>(ConsumableVolumeProductErrors.UnValidType);

                    var standardData = projectOperationDetail.ProjectOperation.OperationInfo.ConsumptionStandardProduct.Where(x => x.ProductUnitId == item?.ProductGroupId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;

                    decimal standardValue = 0;
                    if (isStandard == true)
                        standardValue = standardData!.Number * projectOperationDetail.FinalAmount;

                    var createData = await _mediator.Send(new CreateConsumableVolumeProductCommand(projectOperationDetail, item.ProductGroupId,
                        item.UnusedPercentage, isStandard, standardValue, item.FinalValue, item.VolumeProductType!.Value), ct);
                    if (createData.IsFailure)
                        return Result.Failure<UpdateConsumableVolumesResponse>(createData.Error!);
                }
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateConsumableVolumesResponse(true);
    }

    public async Task<Result<GetProjectOperationDetailVolumesResponse?>> GetProjectOperationDetailVolumes(
        GetProjectOperationDetailVolumesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationDetailVolumes, id:{Id}", request.ProjectOperationDetailId);

        var isValidRequest = await request.IsValidAsync<GetProjectOperationDetailVolumesValidator, GetProjectOperationDetailVolumesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationDetailVolumesResponse>(isValidRequest.Error!);

        var projectOperationDetail = await _mediator.Send(new GetProjectOperationDetailVolumesQuery((long)request.ProjectOperationDetailId), ct);
        if (projectOperationDetail.IsFailure)
            return Result.Failure<GetProjectOperationDetailVolumesResponse>(projectOperationDetail.Error!);
        if (projectOperationDetail.Value is null)
            return Result.Failure<GetProjectOperationDetailVolumesResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var value = projectOperationDetail.Value;

        var expertsData = value.ConsumableVolumeExperts.ToList();
        List<ExpertDataModel>? experts = [];
        if (expertsData is not null && expertsData.Count > 0)
        {
            var expertIds = expertsData.Select(e => e.ExpertId).ToList();
            var expertData = await WebServicesLogic.SkillsDataReceiver(expertIds, _mediator, ct);

            experts = expertsData.Adapt<List<ExpertDataModel>>();
            foreach (var item in experts)
            {
                var expertInfo = expertData?.Where(x => x?.Id == item.ExpertId).FirstOrDefault();

                item.ExpertCode = expertInfo?.Code;
                item.ExpertName = expertInfo?.Name;
            }
        }

        var productsData = value.ConsumableVolumeProducts.ToList();
        List<ProductDataModel>? consumbleVolumeProducts = [];
        if (productsData is not null && productsData.Count > 0)
        {
            if (productsData.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
            {
                var volumeProducts = productsData.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup).ToList();
                var productIds = volumeProducts.Select(e => e.ProductGroupId).Distinct().ToList();
                var productsInfo = await WebServicesLogic.GroupsDataReceiver(productIds, _mediator, ct);

                List<ProductDataModel>? products = [];
                products = volumeProducts.Adapt<List<ProductDataModel>>();
                foreach (var item in products)
                {
                    var volume = volumeProducts.FirstOrDefault(x => x.Id == item.Id);
                    var product = productsInfo?.Where(x => x.Id == item.ProductGroupId).FirstOrDefault();

                    item.ProductGroupName = product?.Name;
                    item.ProductGroupCode = product?.Code;
                    item.RequestedValue = SumRequestedProduct(volume!);
                    item.IsActive = product?.IsActive;
                    item.MeasureUnitId = product?.MeasureUnitId;
                    item.MeasureUnitName = product?.MeasureUnitName;
                    item.VolumeProductType = VolumeProductType.ProductGroup;
                    item.FinalValueRes = volume!.FinalValue;
                }

                consumbleVolumeProducts.AddRange(products);
            }

            if (productsData.Any(x => x.VolumeProductType == VolumeProductType.Category))
            {
                var volumeProducts = productsData.Where(x => x.VolumeProductType == VolumeProductType.Category).ToList();
                var categoryIds = volumeProducts.Select(e => e.ProductGroupId).Distinct().ToList();
                var categorysInfo = await WebServicesLogic.CategoriesDataReceiver(categoryIds, null, _mediator, ct);

                List<ProductDataModel>? categories = [];
                categories = volumeProducts.Adapt<List<ProductDataModel>>();
                foreach (var item in categories)
                {
                    var volume = volumeProducts.FirstOrDefault(x => x.Id == item.Id);
                    var category = categorysInfo?.Where(x => x.Id == item.ProductGroupId).FirstOrDefault();

                    item.ProductGroupName = category?.Title;
                    item.ProductGroupCode = category?.Code;
                    item.MeasureUnitId = null;
                    item.MeasureUnitName = null;
                    item.RequestedValue = SumRequestedProduct(volume!);
                    item.IsActive = category.IsActive;
                    item.VolumeProductType = VolumeProductType.Category;
                    item.FinalValueRes = volume!.FinalValue;
                }

                consumbleVolumeProducts.AddRange(categories);
            }
        }

        var machineriesData = value.ConsumableVolumeMachineries.ToList();
        List<MachineryDataModel>? machineries = [];

        machineries = machineriesData.Adapt<List<MachineryDataModel>>();

        var result = new GetProjectOperationDetailVolumesResponseModel(experts, machineries, consumbleVolumeProducts);
        return new GetProjectOperationDetailVolumesResponse(result ?? null);
    }

    #region Experts

    public async Task<Result<CreateConsumableVolumeExpertResponse?>> CreateConsumableVolumeExpert(
        CreateConsumableVolumeExpertRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateConsumableVolumeExpert, ProjectOperationDetailId:{ProjectOperationDetailId}, ExpertId:{ExpertId},",
            request.ProjectOperationDetailId, request.ExpertId);
        //Validate data
        var isValidRequest = await request.IsValidAsync<CreateConsumableVolumeExpertValidator, CreateConsumableVolumeExpertRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateConsumableVolumeExpertResponse>(isValidRequest.Error!);
        //Find ProjectOperationDetail
        var projectOperationDetail = await _mediator.Send(new GetProjectOperationDetailWithStandardsQuery(request.ProjectOperationDetailId), ct);
        if (projectOperationDetail.IsFailure)
            return Result.Failure<CreateConsumableVolumeExpertResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        if (projectOperationDetail.Value!.ConsumableVolumeExperts.Any(x => x.ExpertId == request.ExpertId))
            return Result.Failure<CreateConsumableVolumeExpertResponse>(ConsumableVolumeExpertErrors.AvailableId);
        var projectDetail = projectOperationDetail.Value;
        //Validate Experts
        var expertData = await _mediator.Send(new GetSkillByIdQuery(request.ExpertId), ct); // بره سراغ متا دیتا
        if (expertData.IsFailure)
            return Result.Failure<CreateConsumableVolumeExpertResponse>(ConsumableVolumeExpertErrors.UnValidExperts);

        var standardData = projectDetail.ProjectOperation!.OperationInfo.ConsumptionStandardExperts.Where(x => x.ExpertUnitId == request?.ExpertId).FirstOrDefault();

        var isStandard = standardData is not null ? true : false;
        long standardValue = 0;
        if (isStandard == true)
            standardValue = standardData!.TimeSpant;

        var finalValue = TimeCalculator.StringToTicks(request!.FinalValue);

        var response = await _mediator.Send(new CreateConsumableVolumeExpertCommand(projectDetail,
            request.ExpertId, request.Number, request.UnusedPercentage, isStandard, standardValue, finalValue), ct);
        if (response.IsFailure)
            return Result.Failure<CreateConsumableVolumeExpertResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateConsumableVolumeExpertResponse { ProjectOperationDetailExpertId = response.Value!.Id };
    }

    public async Task<Result<UpdateConsumableVolumeExpertResponse?>> UpdateConsumableVolumeExpert(
        UpdateConsumableVolumeExpertRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateConsumableVolumeExpertValidator, UpdateConsumableVolumeExpertRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateConsumableVolumeExpertResponse>(isValidRequest.Error!);
        //Find ProjectOperationDetail
        var consumableVolumeExpert = await _mediator.Send(new GetConsumableVolumeExpertByIdQuery(request.Id), ct);
        if (consumableVolumeExpert.IsFailure)
            return Result.Failure<UpdateConsumableVolumeExpertResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var expert = consumableVolumeExpert.Value!;
        //Validate Experts
        var expertData = await _mediator.Send(new GetSkillByIdQuery(request.ExpertId), ct); // بره سراغ متا دیتا
        if (expertData.IsFailure)
            return Result.Failure<UpdateConsumableVolumeExpertResponse>(ConsumableVolumeExpertErrors.UnValidExperts);

        var standardData = expert.ProjectOperationDetail.ProjectOperation!.OperationInfo.ConsumptionStandardExperts
            .Where(x => x.ExpertUnitId == request?.ExpertId).FirstOrDefault();

        var isStandard = standardData is not null ? true : false;
        long standardValue = 0;
        if (isStandard == true)
            standardValue = standardData!.TimeSpant;

        var finalValue = TimeCalculator.StringToTicks(request!.FinalValue);

        var response = await _mediator.Send(new UpdateConsumableVolumeExpertCommand(consumableVolumeExpert.Value!, request.ExpertId,
            request.Number, request.UnusedPercentage, isStandard, standardValue, finalValue), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateConsumableVolumeExpertResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateConsumableVolumeExpertResponse { ProjectOperationDetailExpertId = response.Value!.Id };
    }

    public async Task<Result<DeleteConsumableVolumeExpertResponse?>> DeleteConsumableVolumeExpert(
        DeleteConsumableVolumeExpertRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteConsumableVolumeExpert, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteConsumableVolumeExpertValidator, DeleteConsumableVolumeExpertRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteConsumableVolumeExpertResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteConsumableVolumeExpertCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteConsumableVolumeExpertResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteConsumableVolumeExpertResponse(response.Value!.Id, true);
    }

    public async Task<Result<GetConsumableVolumeExpertByIdResponse?>> GetConsumableVolumeExpertById(
        GetConsumableVolumeExpertByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetConsumableVolumeExpertById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetConsumableVolumeExpertByIdValidator, GetConsumableVolumeExpertByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetConsumableVolumeExpertByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetConsumableVolumeExpertByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetConsumableVolumeExpertByIdResponse>(response.Error!);

        var value = response.Value!;

        //Validate Experts
        var expertData = await WebServicesLogic.SkillDataReceiver(value.ExpertId, _mediator, ct); // بره سراغ متا دیتا

        return new GetConsumableVolumeExpertByIdResponse
        {
            ProjectOperationDetailExpertId = value!.Id,
            ProjectOperationDetailId = value.ProjectOperationDetail.Id,
            Id = value.ExpertId,
            Name = expertData?.Name,
            Code = expertData?.Code,
            Number = value.Number,
            UnusedPercentage = value.UnusedPercentage,
            IsStandard = value.IsStandard,
            StandardValue = TimeCalculator.TicksToStringHM((long)value.StandardValue!),
            FinalValue = TimeCalculator.TicksToStringHM(value.FinalValue!)
        };
    }

    public async Task<Result<GetExpertsByProjectOperationDetailIdResponse?>> GetExpertsByProjectOperationDetailId(
        GetExpertsByProjectOperationDetailIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, id:{Id}", request.ProjectOperationDetailId);

        var isValidRequest = await request.IsValidAsync<GetExpertsByProjectOperationDetailIdValidator, GetExpertsByProjectOperationDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetExpertsByProjectOperationDetailIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetExpertsByProjectOperationDetailIdQuery(request.ProjectOperationDetailId), ct);
        if (response.IsFailure)
            return Result.Failure<GetExpertsByProjectOperationDetailIdResponse>(response.Error!);
        var value = response.Value?.Data;

        var responseData = new List<ConsumableVolumeExpertModel>();
        List<Skill?> skillsResponse = new();
        if (value is not null && value?.Count > 0)
        {
            var data = new List<ConsumableVolumeExpertModel>();

            var allIds = value.Select(x => x!.ExpertId).ToList();
            var skillInfos = await WebServicesLogic.GetFilteredSkillsDataReceiver(allIds, request.FilterData, _mediator, ct); // بره سراغ متا دیتا
            if (skillInfos is not null && skillInfos.Count > 0)
                skillsResponse.AddRange(skillInfos);

            if (!string.IsNullOrEmpty(request.FilterData))
                foreach (var item in value)
                {
                    if (!skillsResponse.Any(x => x?.Id == item.ExpertId))
                        continue;

                    var standardTitle = item.IsStandard == true ? "استاندارد" : "غیراستاندارد";
                    var skillInfo = skillsResponse.Where(x => x?.Id == item.ExpertId).FirstOrDefault();

                    data.Add(new ConsumableVolumeExpertModel
                    {
                        ProjectOperationDetailExpertId = item!.Id,
                        ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                        Id = item.ExpertId,
                        Name = skillInfo?.Name,
                        Code = skillInfo?.Code,
                        Number = item.Number,
                        UnusedPercentage = item.UnusedPercentage,
                        IsStandard = item.IsStandard,
                        StandardValue = TimeCalculator.TicksToStringHM((long)item.StandardValue!),
                        FinalValue = TimeCalculator.TicksToStringHM(item.FinalValue!)
                    });
                }
            else
                foreach (var item in value)
                {
                    var standardTitle = item.IsStandard == true ? "استاندارد" : "غیراستاندارد";
                    var skillInfo = skillsResponse.Where(x => x?.Id == item.ExpertId).FirstOrDefault();

                    data.Add(new ConsumableVolumeExpertModel
                    {
                        ProjectOperationDetailExpertId = item!.Id,
                        ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                        Id = item.ExpertId,
                        Name = skillInfo?.Name,
                        Code = skillInfo?.Code,
                        Number = item.Number,
                        UnusedPercentage = item.UnusedPercentage,
                        IsStandard = item.IsStandard,
                        StandardValue = TimeCalculator.TicksToStringHM((long)item.StandardValue!),
                        FinalValue = TimeCalculator.TicksToStringHM(item.FinalValue!)
                    });
                }
            responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        }

        return new GetExpertsByProjectOperationDetailIdResponse(responseData ?? new List<ConsumableVolumeExpertModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetExpertsByProjectOperationIdResponse?>> GetExpertsByProjectOperationId(
        GetExpertsByProjectOperationIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, id:{Id}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetExpertsByProjectOperationIdValidator, GetExpertsByProjectOperationIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetExpertsByProjectOperationIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetExpertsByProjectOperationIdQuery(request.ProjectOperationId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetExpertsByProjectOperationIdResponse>(response.Error!);
        var value = response.Value.Data;

        var allIds = value.Select(x => x!.ExpertId).Distinct().ToList();
        var metaDataInfos = await WebServicesLogic.SkillsDataReceiver(allIds, _mediator, ct); // بره سراغ متا دیتا

        var data = ExpertsCalc(value, metaDataInfos!);
        return new GetExpertsByProjectOperationIdResponse(data ?? new List<ConsumableVolumeExpertModel>(0), response.Value.RowCount);
    }

    public async Task<Result<GetExpertsByProjectOperationIdsResponse?>> GetExpertsByProjectOperationIds(
        GetExpertsByProjectOperationIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, id:{Id}", request.ProjectOperationIds);

        var isValidRequest = await request.IsValidAsync<GetExpertsByProjectOperationIdsValidator, GetExpertsByProjectOperationIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetExpertsByProjectOperationIdsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetExpertsByProjectOperationIdsQuery(request.ProjectOperationIds), ct);
        if (response.IsFailure)
            return Result.Failure<GetExpertsByProjectOperationIdsResponse>(response.Error!);
        if (response.Value is null)
            return Result.Failure<GetExpertsByProjectOperationIdsResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        if (response.Value.Data is null)
            return Result.Failure<GetExpertsByProjectOperationIdsResponse>(ConsumableVolumeExpertErrors.NoHaveExperts);

        var value = response.Value!.Data!;
        var expertIds = value!.Select(x => x.ExpertId).Distinct().ToList();

        var data = new List<GetExpertsByProjectOperationIdsModel>();
        List<Skill?> skillsResponse = new();
        if (expertIds is not null)
            if (expertIds?.Count > 0)
            {
                var skillInfos = await WebServicesLogic.GetFilteredSkillsDataReceiver(expertIds, request.FilterData, _mediator, ct); // بره سراغ متا دیتا
                if (skillInfos is not null && skillInfos.Count > 0)
                    skillsResponse.AddRange(skillInfos!);

                if (!string.IsNullOrEmpty(request.FilterData))
                    foreach (var item in expertIds)
                    {
                        var numbers = value!.Where(x => x.ExpertId.Equals(item)).Sum(x => x.Number);
                        if (!skillsResponse.Any(x => x?.Id == item))
                            continue;

                        var skillInfo = skillsResponse.Where(x => x?.Id == item).FirstOrDefault();
                        data.Add(new(item!, numbers, skillInfo?.Name, skillInfo?.Code));
                    }
                else
                    foreach (var item in expertIds)
                    {
                        var numbers = value!.Where(x => x.ExpertId.Equals(item)).Sum(x => x.Number);
                        var skillInfo = skillsResponse.Where(x => x?.Id == item).FirstOrDefault();
                        data.Add(new(item!, numbers, skillInfo?.Name, skillInfo?.Code));
                    }
            }

        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetExpertsByProjectOperationIdsResponse(data ?? new List<GetExpertsByProjectOperationIdsModel>(0), data!.Count);
    }

    #endregion

    #region Machineries

    public async Task<Result<CreateConsumableVolumeMachineryResponse?>> CreateConsumableVolumeMachinery(
        CreateConsumableVolumeMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateConsumableVolumeMachinery, ProjectOperationDetailId:{ProjectOperationDetailId}, MachineryId:{MachineryId},",
            request.ProjectOperationDetailId, request.MachineryId);
        //Validate data
        var isValidRequest = await request.IsValidAsync<CreateConsumableVolumeMachineryValidator, CreateConsumableVolumeMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateConsumableVolumeMachineryResponse>(isValidRequest.Error!);
        //Find ProjectOperationDetail
        var projectOperationDetail = await _mediator.Send(new GetProjectOperationDetailWithStandardsQuery(request.ProjectOperationDetailId), ct);
        if (projectOperationDetail.IsFailure)
            return Result.Failure<CreateConsumableVolumeMachineryResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        if (projectOperationDetail.Value!.ConsumableVolumeMachineries.Any(x => x.Machinery.Id == request.MachineryId))
            return Result.Failure<CreateConsumableVolumeMachineryResponse>(ConsumableVolumeMachineryErrors.AvailableId);
        var projectDetail = projectOperationDetail.Value;
        //Validate Machinerys
        var machineryData = await _mediator.Send(new GetMachineryByIdQuery(request.MachineryId), ct); // بره سراغ متا دیتا
        if (machineryData.IsFailure)
            return Result.Failure<CreateConsumableVolumeMachineryResponse>(ConsumableVolumeMachineryErrors.UnValidMachinerys);

        var standardData = projectDetail.ProjectOperation!.OperationInfo.ConsumptionStandardMachineries
            .Where(x => x.Machinery.Id == request?.MachineryId).FirstOrDefault();

        var isStandard = standardData is not null ? true : false;
        long standardValue = 0;
        if (isStandard == true)
            standardValue = standardData!.TimeSpant;

        var finalValue = (request.FinalValue.Contains(":") && request.Unit != null && request.Unit == RequestMachineryUnit.Hourly) ||
                (Convert.ToDecimal(request.FinalValue) > 1000000) ?
                Convert.ToDecimal(TimeCalculator.StringToTicks(request!.FinalValue)) : Convert.ToDecimal(request.FinalValue);

        var response = await _mediator.Send(new CreateConsumableVolumeMachineryCommand(projectOperationDetail.Value!,
            machineryData.Value!, request.Number, request.UnusedPercentage, isStandard, standardValue, Convert.ToDecimal(finalValue), request.Unit), ct);
        if (response.IsFailure)
            return Result.Failure<CreateConsumableVolumeMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateConsumableVolumeMachineryResponse { ProjectOperationDetailMachineryId = response.Value!.Id };
    }

    public async Task<Result<UpdateConsumableVolumeMachineryResponse?>> UpdateConsumableVolumeMachinery(
        UpdateConsumableVolumeMachineryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateConsumableVolumeMachineryValidator, UpdateConsumableVolumeMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateConsumableVolumeMachineryResponse>(isValidRequest.Error!);
        //Find ProjectOperationDetail
        var consumableVolumeMachinery = await _mediator.Send(new GetConsumableVolumeMachineryByIdQuery(request.Id), ct);
        if (consumableVolumeMachinery.IsFailure)
            return Result.Failure<UpdateConsumableVolumeMachineryResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var machinery = consumableVolumeMachinery.Value!;
        //Validate Machinerys
        var machineryData = await _mediator.Send(new GetMachineryByIdQuery(request.MachineryId), ct); // بره سراغ متا دیتا
        if (machineryData.IsFailure)
            return Result.Failure<UpdateConsumableVolumeMachineryResponse>(ConsumableVolumeMachineryErrors.UnValidMachinerys);

        var standardData = machinery.ProjectOperationDetail.ProjectOperation!.OperationInfo.ConsumptionStandardMachineries
            .Where(x => x.Machinery.Id == request?.MachineryId).FirstOrDefault();

        var isStandard = standardData is not null ? true : false;
        long standardValue = 0;
        if (isStandard == true)
            standardValue = standardData!.TimeSpant;

        var finalValue = (request.FinalValue.Contains(":") && request.Unit != null && request.Unit == RequestMachineryUnit.Hourly) ||
                (Convert.ToDecimal(request.FinalValue) > 1000000) ?
                Convert.ToDecimal(TimeCalculator.StringToTicks(request!.FinalValue)) : Convert.ToDecimal(request.FinalValue);

        var response = await _mediator.Send(new UpdateConsumableVolumeMachineryCommand(consumableVolumeMachinery.Value!,
            machineryData.Value!, request.Number, request.UnusedPercentage, isStandard, standardValue, Convert.ToDecimal(finalValue), request.Unit), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateConsumableVolumeMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateConsumableVolumeMachineryResponse { ProjectOperationDetailMachineryId = response.Value!.Id };
    }

    public async Task<Result<DeleteConsumableVolumeMachineryResponse?>> DeleteConsumableVolumeMachinery(
        DeleteConsumableVolumeMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteConsumableVolumeMachinery, Id:{Id}", request.Id);

        var isValidRequest =
            await request.IsValidAsync<DeleteConsumableVolumeMachineryValidator, DeleteConsumableVolumeMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteConsumableVolumeMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteConsumableVolumeMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteConsumableVolumeMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteConsumableVolumeMachineryResponse(response.Value!.Id, true);
    }

    public async Task<Result<GetConsumableVolumeMachineryByIdResponse?>> GetConsumableVolumeMachineryById(
        GetConsumableVolumeMachineryByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetConsumableVolumeMachineryById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetConsumableVolumeMachineryByIdValidator, GetConsumableVolumeMachineryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetConsumableVolumeMachineryByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetConsumableVolumeMachineryByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetConsumableVolumeMachineryByIdResponse>(response.Error!);

        var value = response.Value!;

        //Validate Machinerys
        var machineryData = await _mediator.Send(new GetMachineryByIdQuery(value.Id), ct); // بره سراغ متا دیتا
        return new GetConsumableVolumeMachineryByIdResponse
        {
            ProjectOperationDetailMachineryId = value!.Id,
            ProjectOperationDetailId = value.ProjectOperationDetail.Id,
            Id = value.Machinery.Id,
            MachineryName = machineryData.Value?.MachineryName,
            MachineryCode = machineryData.Value?.MachineryCode,
            Number = value.Number,
            UnusedPercentage = value.UnusedPercentage,
            IsStandard = value.IsStandard,
            StandardValue = TimeCalculator.TicksToStringHM((long)value.StandardValue!),
            FinalValue = value.FinalValue!.ToString()
        };
    }

    public async Task<Result<GetMachineriesByProjectOperationDetailIdResponse?>> GetMachineriesByProjectOperationDetailId(
        GetMachineriesByProjectOperationDetailIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, id:{Id}", request.ProjectOperationDetailId);

        var isValidRequest = await request.IsValidAsync<GetMachineriesByProjectOperationDetailIdValidator, GetMachineriesByProjectOperationDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineriesByProjectOperationDetailIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineriesByProjectOperationDetailIdQuery(request.ProjectOperationDetailId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineriesByProjectOperationDetailIdResponse>(response.Error!);

        var value = response.Value?.Data;
        var data = new List<ConsumableVolumeMachineryModel>();
        if (value?.Count > 0)
        {
            var allIds = value.Select(x => x!.Machinery.Id).ToList();
            var machineryData = await _mediator.Send(new GetsMachineryByIdsQuery(allIds, 1, allIds.Count), ct); // بره سراغ متا دیتا
            foreach (var item in value)
            {
                var machineryInfo = machineryData.Value?.Data?.Where(x => x?.Id == item.Machinery.Id).FirstOrDefault();
                data.Add(new ConsumableVolumeMachineryModel
                {
                    ProjectOperationDetailMachineryId = item!.Id,
                    ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                    Id = item.Machinery.Id,
                    MachineryName = machineryInfo?.MachineryName,
                    MachineryCode = machineryInfo?.MachineryCode,
                    Number = item.Number,
                    UnusedPercentage = item.UnusedPercentage,
                    IsStandard = item.IsStandard,
                    StandardValue = TimeCalculator.TicksToStringHM((long)item.StandardValue!),
                    FinalValue = item.FinalValue!
                });
            }
        }

        return new GetMachineriesByProjectOperationDetailIdResponse(data ?? new List<ConsumableVolumeMachineryModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetMachineriesByProjectOperationIdResponse?>> GetMachineriesByProjectOperationId(
        GetMachineriesByProjectOperationIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, id:{Id}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetMachineriesByProjectOperationIdValidator, GetMachineriesByProjectOperationIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineriesByProjectOperationIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineriesByProjectOperationIdQuery(request.ProjectOperationId, 1, 10), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineriesByProjectOperationIdResponse>(response.Error!);
        if (response.Value is null)
            return Result.Failure<GetMachineriesByProjectOperationIdResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        if (response.Value.Data is null)
            return Result.Failure<GetMachineriesByProjectOperationIdResponse>(ConsumableVolumeMachineryErrors.NoHaveMachineries);

        var value = response.Value.Data;
        var data = MachineriesCalc(value);

        return new GetMachineriesByProjectOperationIdResponse(data ?? new List<ConsumableVolumeMachineryModel>(0), response.Value.RowCount);
    }

    public async Task<Result<GetsFilteredMachineriyVolumeResponse?>> GetsFilteredMachineriyVolume(
        GetsFilteredMachineriyVolumeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, id:{Id}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetsFilteredMachineriyVolumeValidator, GetsFilteredMachineriyVolumeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredMachineriyVolumeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsFilteredMachineriyVolumeQuery(request.ProjectId, request.ProjectOperationId, request.ProjectOperationDetailId, request.MachineryGroupId, request.MachineryId, request.FilterData), ct);
        if (response.IsFailure)
            return Result.Failure<GetsFilteredMachineriyVolumeResponse>(response.Error!);
        if (response.Value is null)
            return Result.Failure<GetsFilteredMachineriyVolumeResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        if (response.Value.Data is null)
            return Result.Failure<GetsFilteredMachineriyVolumeResponse>(ConsumableVolumeMachineryErrors.NoHaveMachineries);

        var data = response.Value?.Data.Sum(x => x.Number);

        return new GetsFilteredMachineriyVolumeResponse(data ?? 0);
    }

    public async Task<Result<GetFilteredTotalOfConsumebleMachineriesResponse?>> GetFilteredTotalOfConsumebleMachineries(
        GetFilteredTotalOfConsumebleMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFilteredTotalOfConsumebleMachineries");

        var isValidRequest = await request.IsValidAsync<GetFilteredTotalOfConsumebleMachineriesValidator, GetFilteredTotalOfConsumebleMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredTotalOfConsumebleMachineriesResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFilteredTotalOfConsumebleMachineriesQuery(request.MachineryId, request.ProjectId, request.CostCenterId, request.ProjectOperationIds,
            request.ProjectOperationDetailIds), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetFilteredTotalOfConsumebleMachineriesResponse>(response.Error!);
        var values = response.Value;

        var maxNumber = values.Max(x => x.Number);

        string totalValue = string.Empty;
        var total = values.Sum(x => x.FinalValue);
        if (total > 1000000)
        {
            long finals = Convert.ToInt64(total);
            totalValue = TimeCalculator.TicksToStringHM(finals);
        }
        else
        {
            totalValue = total.ToString();
        }

        return new GetFilteredTotalOfConsumebleMachineriesResponse(maxNumber, totalValue);
    }

    #endregion

    #region Products

    public async Task<Result<CreateConsumableVolumeProductResponse?>> CreateConsumableVolumeProduct(
        CreateConsumableVolumeProductRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateConsumableVolumeProduct, ProjectOperationDetailId:{ProjectOperationDetailId}, ProductId:{ProductId},",
            request.ProjectOperationDetailId, request.ProductGroupId);
        //Validate data
        var isValidRequest = await request.IsValidAsync<CreateConsumableVolumeProductValidator, CreateConsumableVolumeProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateConsumableVolumeProductResponse>(isValidRequest.Error!);
        //Find ProjectOperationDetail
        var projectOperationDetail = await _mediator.Send(new GetProjectOperationDetailWithStandardsQuery(request.ProjectOperationDetailId), ct);
        if (projectOperationDetail.IsFailure)
            return Result.Failure<CreateConsumableVolumeProductResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var projectDetail = projectOperationDetail.Value;

        if (request.VolumeProductType == VolumeProductType.ProductGroup)
        {
            if (projectDetail!.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup).Any(x => x.ProductGroupId == request.ProductGroupId))
                return Result.Failure<CreateConsumableVolumeProductResponse>(ConsumableVolumeProductErrors.AvailableId);

            //Validate Products
            var productData = await _mediator.Send(new GetGroupByIdQuery(request.ProductGroupId), ct); // بره سراغ انبار
            if (productData.IsFailure)
                return Result.Failure<CreateConsumableVolumeProductResponse>(ConsumableVolumeProductErrors.UnValidProducts);
        }
        else if (request.VolumeProductType == VolumeProductType.Category)
        {
            if (projectDetail!.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.Category).Any(x => x.ProductGroupId == request.ProductGroupId))
                return Result.Failure<CreateConsumableVolumeProductResponse>(ConsumableVolumeProductErrors.AvailableId);

            //Validate Categories
            var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(request.ProductGroupId), ct); // بره سراغ انبار
            if (categoryData.IsFailure)
                return Result.Failure<CreateConsumableVolumeProductResponse>(ConsumableVolumeProductErrors.UnValidCategories);
        }
        else
            return Result.Failure<CreateConsumableVolumeProductResponse>(ConsumableVolumeProductErrors.UnValidType);

        var standardData = projectDetail.ProjectOperation.OperationInfo.ConsumptionStandardProduct.Where(x => x.ProductUnitId == request?.ProductGroupId).FirstOrDefault();

        var isStandard = standardData is not null ? true : false;

        decimal standardValue = 0;
        if (isStandard == true)
            standardValue = standardData!.Number * projectDetail.FinalAmount;

        var response = await _mediator.Send(new CreateConsumableVolumeProductCommand(projectOperationDetail.Value!, request.ProductGroupId,
            request.UnusedPercentage, isStandard, standardValue, request.FinalValue, request.VolumeProductType.Value), ct);
        if (response.IsFailure)
            return Result.Failure<CreateConsumableVolumeProductResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateConsumableVolumeProductResponse { ProjectOperationDetailProductGroupId = response.Value!.Id };
    }

    public async Task<Result<UpdateConsumableVolumeProductResponse?>> UpdateConsumableVolumeProduct(
        UpdateConsumableVolumeProductRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateConsumableVolumeProductValidator, UpdateConsumableVolumeProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateConsumableVolumeProductResponse>(isValidRequest.Error!);
        //Find ProjectOperationDetail
        var consumableVolumeProduct = await _mediator.Send(new GetConsumableVolumeProductByIdQuery(request.Id), ct);
        if (consumableVolumeProduct.IsFailure)
            return Result.Failure<UpdateConsumableVolumeProductResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);

        if (ValidateConsumableVolumeProduct(consumableVolumeProduct.Value!, request.FinalValue, request.UnusedPercentage ?? 0))
            return Result.Failure<UpdateConsumableVolumeProductResponse>(ConsumableVolumeProductErrors.UnValidForSupplies);

        if (request.VolumeProductType == VolumeProductType.ProductGroup)
        {
            //Validate Products
            var productData = await _mediator.Send(new GetGroupByIdQuery(request.ProductGroupId), ct); // بره سراغ انبار
            if (productData.IsFailure)
                return Result.Failure<UpdateConsumableVolumeProductResponse>(ConsumableVolumeProductErrors.UnValidProducts);
        }
        else if (request.VolumeProductType == VolumeProductType.Category)
        {
            //Validate Categories
            var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(request.ProductGroupId), ct); // بره سراغ انبار
            if (categoryData.IsFailure)
                return Result.Failure<UpdateConsumableVolumeProductResponse>(ConsumableVolumeProductErrors.UnValidCategories);
        }
        else
            return Result.Failure<UpdateConsumableVolumeProductResponse>(ConsumableVolumeProductErrors.UnValidType);

        var product = consumableVolumeProduct.Value!;
        var standardData = product.ProjectOperationDetail.ProjectOperation!.OperationInfo.ConsumptionStandardProduct
            .Where(x => x.ProductUnitId == request?.ProductGroupId).FirstOrDefault();

        var isStandard = standardData is not null ? true : false;

        decimal standardValue = 0;
        if (isStandard == true)
            standardValue = standardData!.Number * product.ProjectOperationDetail.FinalAmount;

        var response = await _mediator.Send(new UpdateConsumableVolumeProductCommand(consumableVolumeProduct.Value!, request.ProductGroupId,
            request.UnusedPercentage, isStandard, standardValue, request.FinalValue, request.VolumeProductType.Value), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateConsumableVolumeProductResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateConsumableVolumeProductResponse { ProjectOperationDetailProductGroupId = response.Value!.Id };
    }

    public async Task<Result<DeleteConsumableVolumeProductResponse?>> DeleteConsumableVolumeProduct(
        DeleteConsumableVolumeProductRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteConsumableVolumeProduct, Id:{Id}", request.Id);

        var isValidRequest =
            await request.IsValidAsync<DeleteConsumableVolumeProductValidator, DeleteConsumableVolumeProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteConsumableVolumeProductResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteConsumableVolumeProductCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteConsumableVolumeProductResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteConsumableVolumeProductResponse(response.Value!.Id, true);
    }

    public async Task<Result<GetConsumableVolumeProductByIdResponse?>> GetConsumableVolumeProductById(
        GetConsumableVolumeProductByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetConsumableVolumeProductById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetConsumableVolumeProductByIdValidator, GetConsumableVolumeProductByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetConsumableVolumeProductByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetConsumableVolumeProductByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetConsumableVolumeProductByIdResponse>(response.Error!);
        var value = response.Value!;

        var data = new GetConsumableVolumeProductByIdResponse();
        if (value.VolumeProductType == VolumeProductType.ProductGroup)
        {
            var productData = await WebServicesLogic.GroupsDataReceiver([value.ProductGroupId], _mediator, ct); // بره سراغ انبار
            data = new GetConsumableVolumeProductByIdResponse
            {
                ProjectOperationDetailProductGroupId = value!.Id,
                ProjectOperationDetailId = value.ProjectOperationDetail.Id,
                Id = value.ProductGroupId,
                Name = productData?.FirstOrDefault()?.Name,
                Code = productData?.FirstOrDefault()?.Code,
                Measure = productData?.FirstOrDefault()?.Name,
                IsActive = productData?.FirstOrDefault()?.IsActive,
                UnusedPercentage = value.UnusedPercentage,
                IsStandard = value.IsStandard,
                StandardValue = value.StandardValue!,
                FinalValue = value.FinalValue!
            };
        }

        if (value.VolumeProductType == VolumeProductType.Category)
        {
            var categoryData = await WebServicesLogic.CategoriesDataReceiver([value.ProductGroupId], null, _mediator, ct); // بره سراغ انبار
            data = new GetConsumableVolumeProductByIdResponse
            {
                ProjectOperationDetailProductGroupId = value!.Id,
                ProjectOperationDetailId = value.ProjectOperationDetail.Id,
                Id = value.ProductGroupId,
                Name = categoryData?.FirstOrDefault()?.Title,
                Code = categoryData?.FirstOrDefault()?.Code,
                Measure = categoryData?.FirstOrDefault()?.Title,
                IsActive = categoryData?.FirstOrDefault()?.IsActive,
                UnusedPercentage = value.UnusedPercentage,
                IsStandard = value.IsStandard,
                StandardValue = value.StandardValue!,
                FinalValue = value.FinalValue!
            };
        }

        return data;
    }

    public async Task<Result<GetProductsByProjectOperationDetailIdResponse?>> GetProductsByProjectOperationDetailId(
        GetProductsByProjectOperationDetailIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, id:{Id}", request.ProjectOperationDetailId);

        var isValidRequest = await request.IsValidAsync<GetProductsByProjectOperationDetailIdValidator, GetProductsByProjectOperationDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProductsByProjectOperationDetailIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProductsByProjectOperationDetailIdQuery(request.ProjectOperationDetailId, 1, 100), ct);
        if (response.IsFailure)
            return Result.Failure<GetProductsByProjectOperationDetailIdResponse>(response.Error!);
        var values = response.Value?.Data;

        List<FilteredGroup?> productsResponse = new();
        List<WarehouseCategory?> categoriesResponse = new();
        var data = new List<ConsumableVolumeProductModel>();
        if (values is not null && values?.Count > 0)
        {
            if (values.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
            {
                var allIds = values.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup).Select(x => x!.ProductGroupId).Distinct().ToList();
                var responseValue = await WebServicesLogic.GetFilteredGroupsDataReceiver(allIds, request.FilterData, _mediator, ct); // بره سراغ متا دیتا
                if (responseValue is not null && responseValue.Count > 0)
                    productsResponse.AddRange(responseValue!);

                if (!string.IsNullOrEmpty(request.FilterData))
                    foreach (var item in values.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                    {
                        if (!productsResponse.Any(x => x?.Id == item.ProductGroupId))
                            continue;

                        var standardTitle = item.IsStandard == true ? "استاندارد" : "غیراستاندارد";
                        var productResponse = productsResponse.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();

                        data.Add(new ConsumableVolumeProductModel
                        {
                            ProjectOperationDetailProductGroupId = item!.Id,
                            ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                            Id = item.ProductGroupId,
                            Name = productResponse?.Name,
                            Code = productResponse?.Code,
                            IsActive = productResponse?.IsActive,
                            UnusedPercentage = item.UnusedPercentage,
                            IsStandard = item.IsStandard,
                            StandardValue = item.StandardValue!,
                            FinalValue = item.FinalValue!,
                            MeasureUnitId = productResponse?.MeasureUnitId,
                            MeasureUnitName = productResponse?.MeasureUnitName,
                            VolumeProductType = item.VolumeProductType
                        });
                    }
                else
                    foreach (var item in values.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                    {
                        var standardTitle = item.IsStandard == true ? "استاندارد" : "غیراستاندارد";
                        var productResponse = productsResponse.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();

                        data.Add(new ConsumableVolumeProductModel
                        {
                            ProjectOperationDetailProductGroupId = item!.Id,
                            ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                            Id = item.ProductGroupId,
                            Name = productResponse?.Name,
                            Code = productResponse?.Code,
                            IsActive = productResponse?.IsActive,
                            UnusedPercentage = item.UnusedPercentage,
                            IsStandard = item.IsStandard,
                            StandardValue = item.StandardValue!,
                            FinalValue = item.FinalValue!,
                            MeasureUnitId = productResponse?.MeasureUnitId,
                            MeasureUnitName = productResponse?.MeasureUnitName,
                            VolumeProductType = item.VolumeProductType
                        });
                    }
            }

            if (values.Any(x => x.VolumeProductType == VolumeProductType.Category))
            {
                var allIds = values.Where(x => x.VolumeProductType == VolumeProductType.Category).Select(x => x!.ProductGroupId).Distinct().ToList();
                var responseValue = await WebServicesLogic.CategoriesDataReceiver(allIds, request.FilterData, _mediator, ct); // بره سراغ متا دیتا
                if (responseValue is not null && responseValue.Count > 0)
                    categoriesResponse.AddRange(responseValue!);

                if (!string.IsNullOrEmpty(request.FilterData))
                    foreach (var item in values.Where(x => x.VolumeProductType == VolumeProductType.Category))
                    {
                        if (!categoriesResponse.Any(x => x?.Id == item.ProductGroupId))
                            continue;

                        var standardTitle = item.IsStandard == true ? "استاندارد" : "غیراستاندارد";
                        var categoryResponse = categoriesResponse.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();

                        data.Add(new ConsumableVolumeProductModel
                        {
                            ProjectOperationDetailProductGroupId = item!.Id,
                            ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                            Id = item.ProductGroupId,
                            Name = categoryResponse?.Title,
                            Code = categoryResponse?.Code,
                            IsActive = categoryResponse?.IsActive,
                            UnusedPercentage = item.UnusedPercentage,
                            IsStandard = item.IsStandard,
                            StandardValue = item.StandardValue!,
                            FinalValue = item.FinalValue!,
                            MeasureUnitId = null,
                            MeasureUnitName = null,
                            VolumeProductType = item.VolumeProductType
                        });
                    }
                else
                    foreach (var item in values.Where(x => x.VolumeProductType == VolumeProductType.Category))
                    {
                        var standardTitle = item.IsStandard == true ? "استاندارد" : "غیراستاندارد";
                        var categoryResponse = categoriesResponse.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();

                        data.Add(new ConsumableVolumeProductModel
                        {
                            ProjectOperationDetailProductGroupId = item!.Id,
                            ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                            Id = item.ProductGroupId,
                            Name = categoryResponse?.Title,
                            Code = categoryResponse?.Code,
                            IsActive = categoryResponse?.IsActive,
                            UnusedPercentage = item.UnusedPercentage,
                            IsStandard = item.IsStandard,
                            StandardValue = item.StandardValue!,
                            FinalValue = item.FinalValue!,
                            MeasureUnitId = null,
                            MeasureUnitName = null,
                            VolumeProductType = item.VolumeProductType
                        });
                    }
            }
        }

        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetProductsByProjectOperationDetailIdResponse(responseData ?? new List<ConsumableVolumeProductModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetProductsByProjectOperationIdResponse?>> GetProductsByProjectOperationId(
        GetProductsByProjectOperationIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, id:{Id}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetProductsByProjectOperationIdValidator, GetProductsByProjectOperationIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProductsByProjectOperationIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProductsByProjectOperationIdQuery(request.ProjectOperationId, 1, 10), ct);
        if (response.IsFailure)
            return Result.Failure<GetProductsByProjectOperationIdResponse>(response.Error!);
        if (response.Value is null)
            return Result.Failure<GetProductsByProjectOperationIdResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        if (response.Value.Data is null)
            return Result.Failure<GetProductsByProjectOperationIdResponse>(ConsumableVolumeProductErrors.NoHaveProducts);

        var values = response.Value.Data;

        var allProductIds = values.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup).Select(x => x!.ProductGroupId).ToList();
        var productsInfo = await WebServicesLogic.GroupsDataReceiver(allProductIds, _mediator, ct); // بره سراغ متا دیتا

        var allCategoryIds = values.Where(x => x.VolumeProductType == VolumeProductType.Category).Select(x => x!.ProductGroupId).ToList();
        var categoriesInfo = await WebServicesLogic.CategoriesDataReceiver(allCategoryIds, null, _mediator, ct); // بره سراغ متا دیتا

        var data = ProductsCalc(values, productsInfo, categoriesInfo);

        return new GetProductsByProjectOperationIdResponse(data ?? new List<ConsumableVolumeProductModel>(0), response.Value.RowCount);
    }

    public async Task<Result<GetsProductsByFilteredResponse?>> GetsProductsByFiltered(
        GetsProductsByFilteredRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, id:{Id}", request.ProjectOperationIds);

        var isValidRequest = await request.IsValidAsync<GetsProductsByFilteredValidator, GetsProductsByFilteredRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProductsByFilteredResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProductsByFilteredQuery(request.CostCenterId, request.ProjectId, request.ProjectOperationIds, request.ProjectOperationDetailIds), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProductsByFilteredResponse>(response.Error!);
        var value = response.Value?.Data;

        List<FilteredGroup?> productsResponse = new();
        List<WarehouseCategory?> categoriesResponse = new();
        var data = new List<ConsumableVolumeProductModel>();
        if (value is not null && value?.Count > 0)
        {
            if (value.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
            {
                var allIds = value.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup).Select(x => x!.ProductGroupId).Distinct().ToList();
                var responseValue = await WebServicesLogic.GetFilteredGroupsDataReceiver(allIds, request.FilterData, _mediator, ct); // بره سراغ متا دیتا
                if (responseValue is not null && responseValue.Count > 0)
                    productsResponse.AddRange(responseValue!);

                if (!string.IsNullOrEmpty(request.FilterData))
                    foreach (var item in value.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                    {
                        if (!productsResponse.Any(x => x?.Id == item.ProductGroupId))
                            continue;

                        var productResponse = productsResponse.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();

                        data.Add(new ConsumableVolumeProductModel
                        {
                            ProjectOperationDetailProductGroupId = item!.Id,
                            ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                            Id = item.ProductGroupId,
                            Name = productResponse?.Name,
                            Code = productResponse?.Code,
                            IsActive = productResponse?.IsActive,
                            UnusedPercentage = item.UnusedPercentage,
                            StandardValue = item.StandardValue!,
                            FinalValue = item.FinalValue!,
                            MeasureUnitId = productResponse?.MeasureUnitId,
                            MeasureUnitName = productResponse?.MeasureUnitName,
                            VolumeProductType = item.VolumeProductType
                        });
                    }
                else
                    foreach (var item in value.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                    {
                        var productResponse = productsResponse.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();

                        data.Add(new ConsumableVolumeProductModel
                        {
                            ProjectOperationDetailProductGroupId = item!.Id,
                            ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                            Id = item.ProductGroupId,
                            Name = productResponse?.Name,
                            Code = productResponse?.Code,
                            IsActive = productResponse?.IsActive,
                            UnusedPercentage = item.UnusedPercentage,
                            StandardValue = item.StandardValue!,
                            FinalValue = item.FinalValue!,
                            MeasureUnitId = productResponse?.MeasureUnitId,
                            MeasureUnitName = productResponse?.MeasureUnitName,
                            VolumeProductType = item.VolumeProductType
                        });
                    }
            }

            if (value.Any(x => x.VolumeProductType == VolumeProductType.Category))
            {
                var allIds = value.Where(x => x.VolumeProductType == VolumeProductType.Category).Select(x => x!.ProductGroupId).Distinct().ToList();
                var responseValue = await WebServicesLogic.CategoriesDataReceiver(allIds, request.FilterData, _mediator, ct); // بره سراغ متا دیتا
                if (responseValue is not null && responseValue.Count > 0)
                    categoriesResponse.AddRange(responseValue!);

                if (!string.IsNullOrEmpty(request.FilterData))
                    foreach (var item in value.Where(x => x.VolumeProductType == VolumeProductType.Category))
                    {
                        if (!categoriesResponse.Any(x => x?.Id == item.ProductGroupId))
                            continue;

                        var categoryResponse = categoriesResponse.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();

                        data.Add(new ConsumableVolumeProductModel
                        {
                            ProjectOperationDetailProductGroupId = item!.Id,
                            ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                            Id = item.ProductGroupId,
                            Name = categoryResponse?.Title,
                            Code = categoryResponse?.Code,
                            IsActive = categoryResponse?.IsActive,
                            UnusedPercentage = item.UnusedPercentage,
                            StandardValue = item.StandardValue!,
                            FinalValue = item.FinalValue!,
                            MeasureUnitId = null,
                            MeasureUnitName = null,
                            VolumeProductType = item.VolumeProductType
                        });
                    }
                else
                    foreach (var item in value.Where(x => x.VolumeProductType == VolumeProductType.Category))
                    {
                        var categoryResponse = categoriesResponse.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();

                        data.Add(new ConsumableVolumeProductModel
                        {
                            ProjectOperationDetailProductGroupId = item!.Id,
                            ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                            Id = item.ProductGroupId,
                            Name = categoryResponse?.Title,
                            Code = categoryResponse?.Code,
                            IsActive = categoryResponse?.IsActive,
                            UnusedPercentage = item.UnusedPercentage,
                            StandardValue = item.StandardValue!,
                            FinalValue = item.FinalValue!,
                            MeasureUnitId = null,
                            MeasureUnitName = null,
                            VolumeProductType = item.VolumeProductType
                        });
                    }
            }
        }

        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsProductsByFilteredResponse(responseData ?? new List<ConsumableVolumeProductModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetVolumeProductTypesResponse?>> GetVolumeProductTypes(
        GetVolumeProductTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetVolumeProductType");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<VolumeProductType>());
        return new GetVolumeProductTypesResponse(response);
    }
    #endregion

    private List<ConsumableVolumeExpertModel>? ExpertsCalc(
        List<ExpertsDataModel> expertList, List<Skill?>? expertInfs)
    {
        var result = new List<ConsumableVolumeExpertModel>();
        foreach (var expertItem in expertList)
        {
            if (result.Any(x => x.Id == expertItem.ExpertId))
                foreach (var item in result.Where(x => x.Id == expertItem.ExpertId))
                {
                    var itemTicks = TimeCalculator.StringToTicks(item.FinalValue);
                    var sumTicks = itemTicks + expertItem.FinalValue;
                    var time = TimeCalculator.TicksToStringHM(sumTicks);
                    item.FinalValue = time;
                    item.UnusedPercentage = item.UnusedPercentage + expertItem.UnusedPercentage;
                    item.Number = item.Number + expertItem.Number;
                }
            else
            {
                var expertInfo = expertInfs?.Where(x => x?.Id == expertItem.ExpertId).FirstOrDefault();
                result.Add(new ConsumableVolumeExpertModel
                {
                    ProjectOperationDetailExpertId = expertItem!.Id,
                    ProjectOperationDetailId = expertItem.ProjectOperationDetailId,
                    Id = expertItem.ExpertId,
                    Name = expertInfo?.Name,
                    Code = expertInfo?.Code,
                    Number = expertItem.Number,
                    UnusedPercentage = expertItem.UnusedPercentage,
                    IsStandard = expertItem.IsStandard,
                    StandardValue = TimeCalculator.TicksToStringHM(expertItem.StandardValue),
                    FinalValue = TimeCalculator.TicksToStringHM(expertItem.FinalValue)
                });
            }
        }

        return result;
    }

    private List<ConsumableVolumeMachineryModel>? MachineriesCalc(
        List<MachineriesDataModel> machineryList)
    {
        var result = new List<ConsumableVolumeMachineryModel>();
        foreach (var machineryItem in machineryList)
        {
            if (result.Any(x => x.Id == machineryItem.Id))
            {
                foreach (var item in result.Where(x => x.Id == machineryItem.Id))
                {
                    var itemTicks = item.FinalValue;
                    var sumTicks = itemTicks + machineryItem.FinalValue;
                    item.FinalValue = sumTicks;
                    item.UnusedPercentage = item.UnusedPercentage + machineryItem.UnusedPercentage;
                    item.Number = item.Number + machineryItem.Number;
                }
            }
            else
            {
                result.Add(new ConsumableVolumeMachineryModel
                {
                    ProjectOperationDetailMachineryId = machineryItem!.Id,
                    ProjectOperationDetailId = machineryItem.ProjectOperationDetailId,
                    Id = machineryItem.Id,
                    MachineryName = machineryItem?.MachineryName,
                    MachineryCode = machineryItem?.MachineryCode,
                    Number = machineryItem!.Number,
                    UnusedPercentage = machineryItem.UnusedPercentage,
                    IsStandard = machineryItem.IsStandard,
                    StandardValue = TimeCalculator.TicksToStringHM(machineryItem.StandardValue),
                    FinalValue = machineryItem.FinalValue
                });
            }
        }

        return result;
    }

    private List<ConsumableVolumeProductModel>? ProductsCalc(
        List<ProductsDataModel> productList, List<Group>? productsInfo, List<WarehouseCategory>? categoriesInfo)
    {
        var result = new List<ConsumableVolumeProductModel>();
        foreach (var productItem in productList)
        {
            if (result.Any(x => x.Id == productItem.ProductGroupId))
            {
                foreach (var item in result.Where(x => x.Id == productItem.ProductGroupId))
                {
                    item.FinalValue = item.FinalValue + productItem.FinalValue;
                    item.UnusedPercentage = item.UnusedPercentage + productItem.UnusedPercentage;
                }
            }
            else
            {
                Group? productInfo = null;
                if (productItem.VolumeProductType == VolumeProductType.ProductGroup)
                    productInfo = productsInfo?.Where(x => x?.Id == productItem.ProductGroupId).FirstOrDefault();

                WarehouseCategory? categoryInfo = null;
                if (productItem.VolumeProductType == VolumeProductType.Category)
                    categoryInfo = categoriesInfo?.Where(x => x?.Id == productItem.ProductGroupId).FirstOrDefault();

                result.Add(new ConsumableVolumeProductModel
                {
                    ProjectOperationDetailProductGroupId = productItem!.Id,
                    ProjectOperationDetailId = productItem.ProjectOperationDetailId,
                    Id = productItem.ProductGroupId,
                    Name = productItem.VolumeProductType == VolumeProductType.ProductGroup ? productInfo?.Name : categoryInfo?.Title,
                    Code = productItem.VolumeProductType == VolumeProductType.ProductGroup ? productInfo?.Code : categoryInfo?.Code,
                    UnusedPercentage = productItem.UnusedPercentage,
                    IsActive = productInfo?.IsActive,
                    IsStandard = productItem.IsStandard,
                    StandardValue = productItem.StandardValue!,
                    FinalValue = productItem.FinalValue!,
                    MeasureUnitId = productItem.VolumeProductType == VolumeProductType.ProductGroup ? productInfo?.MeasureUnitId : null,
                    MeasureUnitName = productItem.VolumeProductType == VolumeProductType.ProductGroup ? productInfo?.MeasureUnitName : null,
                    VolumeProductType = productItem.VolumeProductType == VolumeProductType.ProductGroup ? VolumeProductType.ProductGroup : VolumeProductType.Category
                });
            }
        }

        return result;
    }

    public bool ValidateConsumableVolumeProduct(
        ConsumableVolumeProduct product, decimal finalValue, decimal newUnusedPercentage)
    {
        if (product.RequestGoodsSupplyDetails is not null && product.RequestGoodsSupplyDetails.Count > 0)
        {
            var details = product.RequestGoodsSupplyDetails.Where(x =>
                    x.Status != GoodsSupplyDetailStatus.ProjectManagerReturned &&
                    x.Status != GoodsSupplyDetailStatus.ProjectManagerRejected &&
                    x.Status != GoodsSupplyDetailStatus.ManagementReturned &&
                    x.Status != GoodsSupplyDetailStatus.ManagementRejected &&
                    x.Status != GoodsSupplyDetailStatus.SupplyUnitReturned &&
                    x.Status != GoodsSupplyDetailStatus.SupplyUnitRejected &&
                    x.Status != GoodsSupplyDetailStatus.NotCompleteSupply &&
                    x.Status != GoodsSupplyDetailStatus.Closed).ToList();
            if (details.Any())
            {
                decimal unusedPercentageCount = 0;
                if (product.UnusedPercentage != newUnusedPercentage)
                    unusedPercentageCount = (product.FinalValue / 100) * newUnusedPercentage;
                else
                    unusedPercentageCount = (product.FinalValue / 100) * product.UnusedPercentage ?? 0;

                var sumRequest = details.Sum(x => x.RequestedCount);
                decimal finalCount = finalValue + unusedPercentageCount.Round(2);
                if (sumRequest > finalCount)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
            return false;
    }

    private decimal SumRequestedProduct(
        ConsumableVolumeProduct product)
    {
        if (product.RequestGoodsSupplyDetails is not null && product.RequestGoodsSupplyDetails.Count > 0)
        {
            var details = product.RequestGoodsSupplyDetails.Where(x =>
                    x.Status != GoodsSupplyDetailStatus.ProjectManagerReturned &&
                    x.Status != GoodsSupplyDetailStatus.ProjectManagerRejected &&
                    x.Status != GoodsSupplyDetailStatus.ManagementReturned &&
                    x.Status != GoodsSupplyDetailStatus.ManagementRejected &&
                    x.Status != GoodsSupplyDetailStatus.SupplyUnitReturned &&
                    x.Status != GoodsSupplyDetailStatus.SupplyUnitRejected &&
                    x.Status != GoodsSupplyDetailStatus.NotCompleteSupply &&
                    x.Status != GoodsSupplyDetailStatus.Closed).ToList();
            if (details.Any())
            {
                var sumRequest = details.Sum(x => x.RequestedCount);
                return sumRequest;
            }
            else
                return 0;
        }
        else
            return 0;
    }

}
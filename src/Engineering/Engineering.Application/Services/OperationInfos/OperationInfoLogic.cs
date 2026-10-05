using DocumentFormat.OpenXml.Bibliography;
using Engineering.Application.Abstractions.Data.Actions;
using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Abstractions.Data.Synonyms.FinanicalPeriod;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Extensions.StringExtensions;
using Engineering.Application.Services.ConsumptionStandards.Commands.Experts.CreateExpert;
using Engineering.Application.Services.ConsumptionStandards.Commands.Experts.DisableExpert;
using Engineering.Application.Services.ConsumptionStandards.Commands.Experts.UpdateExpert;
using Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.CreateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.DisableMachinery;
using Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.UpdateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Commands.Products.CreateProduct;
using Engineering.Application.Services.ConsumptionStandards.Commands.Products.DisableProduct;
using Engineering.Application.Services.ConsumptionStandards.Commands.Products.UpdateProduct;
using Engineering.Application.Services.Machineries.Queries.GetsMachineryByIds;
using Engineering.Application.Services.OperationInfoDependencies.Commands.CreateOperationInfoDependency;
using Engineering.Application.Services.OperationInfoDependencies.Commands.DisableOperationInfoDependency;
using Engineering.Application.Services.OperationInfoDependencies.Commands.UpdateOperationInfoDependency;
using Engineering.Application.Services.OperationInfoDependencies.Queries.FindOperationInfoDependencies;
using Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependence;
using Engineering.Application.Services.OperationInfoGroupRelations;
using Engineering.Application.Services.OperationInfoGroups.Queries.GetOperationInfoGroupById;
using Engineering.Application.Services.OperationInfos.Commands.ActiveOperationInfo;
using Engineering.Application.Services.OperationInfos.Commands.CodeCreator;
using Engineering.Application.Services.OperationInfos.Commands.CreateOperationInfo;
using Engineering.Application.Services.OperationInfos.Commands.DisableOperationInfo;
using Engineering.Application.Services.OperationInfos.Commands.InactiveOperationInfo;
using Engineering.Application.Services.OperationInfos.Commands.SetOperationInfoPriority;
using Engineering.Application.Services.OperationInfos.Commands.StateChangerOperationInfos;
using Engineering.Application.Services.OperationInfos.Commands.UpdateOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.ActiveOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.AddSeasonsToOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.CodeCreator;
using Engineering.Application.Services.OperationInfos.Models.CreateOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.CreateOperationInfoActions;
using Engineering.Application.Services.OperationInfos.Models.DeleteOperationInfoAction;
using Engineering.Application.Services.OperationInfos.Models.DisableOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.FehrestBaha;
using Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfoBySeasonIds;
using Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.GetOIActionByOperationInfoId;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoAction;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByCode;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoById;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByName;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoContractors;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.GetsByMultiFilter;
using Engineering.Application.Services.OperationInfos.Models.GetsBySeasonId;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoByContractorIds;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelEnum;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelExporter;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoHistoryById;
using Engineering.Application.Services.OperationInfos.Models.GetsPrioritizeOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.InactiveOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoActions;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoExcelImports;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoGroupDelete;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Services;
using Engineering.Application.Services.OperationInfos.Models.RasteReshteExcelImporter;
using Engineering.Application.Services.OperationInfos.Models.SetOperationInfoPriority;
using Engineering.Application.Services.OperationInfos.Models.StateChangerOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.UpdateOperationInfo;
using Engineering.Application.Services.OperationInfos.Queries.GetActiveOperationInfoBySeasonIds;
using Engineering.Application.Services.OperationInfos.Queries.GetActiveOperationInfos;
using Engineering.Application.Services.OperationInfos.Queries.GetFilteredOperationInfo;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByCode;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdIncludeLess;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdWithChild;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByName;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoContractors;
using Engineering.Application.Services.OperationInfos.Queries.GetsByMultiFilter;
using Engineering.Application.Services.OperationInfos.Queries.GetsBySeasonId;
using Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByContractorIds;
using Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIds;
using Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIdsIncludeLess;
using Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoHistoryById;
using Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoModelByIds;
using Engineering.Application.Services.OperationInfos.Queries.GetsPrioritizeOperationInfo;
using Engineering.Application.Services.OperationInfoSeasons;
using Engineering.Application.Services.OperationInfoSeasons.Queries.GetByOperationInfoIdsQuery;
using Engineering.Application.Services.OperationInfoServices;
using Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationUnitOfMeasurement;
using Engineering.Application.Services.ProjectOperations.Queries.GetsByOperationInfoIncludeLess;
using Engineering.Application.Services.Seasons.Queries.GetSeasonById;
using Engineering.Application.Services.ServiceInfos.Queries.GetServiceById;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetMeasureunitById;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetsSkillById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetsWarehouseCategoryById;
using Engineering.Domain.Entities.Branchs;
using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.Enums;
using Engineering.Domain.Entities.Seasons;
using Engineering.Domain.Errors.Actions;
using System.Text.RegularExpressions;
using Action = Engineering.Domain.Entities.Actions.Action;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.OperationInfos;

public partial class OperationInfoLogic : IOperationInfoLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<OperationInfoLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOperationInfoServiceLogic _operationInfoServiceLogic;
    private readonly IOperationInfoSeasonLogic _operationInfoSeasonLogic;
    private readonly IOperationInfoGroupRelationLogic _operationInfoGroupRelationLogic;
    private readonly IUserInfoService _userInfoService;
    private readonly IMeasureUnitRepository _measureUnitRepository;
    private readonly IOperationInfoActionRepository _operationInfoActionRepository;
    private readonly IActionRepository _actionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly ISeasonRepository _seasonRepository;
    private readonly IOperationInfoRepository _operationInfoRepository;
    private readonly IOperationInfoSeasonRepository _operationInfoSeasonRepository;
    private readonly IViewFinancialPeriodRepository _viewFinancialPeriodRepository;
    public OperationInfoLogic(
        IMediator mediator,
        ILogger<OperationInfoLogic> logger,
        IUnitOfWork unitOfWork,
        IOperationInfoServiceLogic operationInfoServiceLogic,
        IOperationInfoSeasonLogic operationInfoSeasonLogic,
        IOperationInfoGroupRelationLogic operationInfoGroupRelationLogic,
        IUserInfoService userInfoService,
        IMeasureUnitRepository measureUnitRepository,
        IOperationInfoActionRepository operationInfoActionRepository,
        IActionRepository actionRepository,
        ICategoryRepository categoryRepository,
        IBranchRepository branchRepository,
        ISeasonRepository seasonRepository,
        IOperationInfoRepository operationInfoRepository,
        IOperationInfoSeasonRepository operationInfoSeasonRepository,
        IViewFinancialPeriodRepository viewFinancialPeriodRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _operationInfoServiceLogic = operationInfoServiceLogic;
        _operationInfoSeasonLogic = operationInfoSeasonLogic;
        _operationInfoGroupRelationLogic = operationInfoGroupRelationLogic;
        _userInfoService = userInfoService;
        _measureUnitRepository = measureUnitRepository;
        _operationInfoActionRepository = operationInfoActionRepository;
        _actionRepository = actionRepository;
        _categoryRepository = categoryRepository;
        _branchRepository = branchRepository;
        _seasonRepository = seasonRepository;
        _operationInfoRepository = operationInfoRepository;
        _operationInfoSeasonRepository = operationInfoSeasonRepository;
        _viewFinancialPeriodRepository = viewFinancialPeriodRepository;
    }

    public async Task<Result<CreateOperationInfoResponse?>> CreateOperationInfo(
        CreateOperationInfoRequest request, CT ct)
    {
        var transactionOptions = new System.Transactions.TransactionOptions();
        transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
        {
            _logger.LogInformation("Request for CreateOperationInfo, OperationInfoName:{OperationInfoName}, OperationInfoCode:{OperationInfoCode},", request.OperationInfoName, request.OperationInfoCode);

            var isValidRequest = await request.IsValidAsync<CreateOperationInfoValidator, CreateOperationInfoRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<CreateOperationInfoResponse>(isValidRequest.Error!);
            if (request.Priority <= 0)
                return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.PriorityCanNot0);

            if (request.Priority == 1 && request.OperationInfoDependency is not null)
                return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.PriorityIsOne);

            long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
            if (companyId >= 1)
            {
                var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
                if (companyResponse.IsFailure)
                    return Result.Failure<CreateOperationInfoResponse>(companyResponse.Error!);
            }

            var nameIsDuplicate = await _mediator.Send(new GetOperationInfoByNameQuery(request.OperationInfoName, request.UnitOfMeasurementId, companyId), ct);
            if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
                return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.NameIsDuplicate);

            var codeIsDuplicate = await _mediator.Send(new GetOperationInfoByCodeQuery(request.OperationInfoCode, companyId), ct);
            if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
                return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.CodeIsDuplicate);

            var expertRequests = new List<OperationInfoExpertServiceModel?>();
            if (request.ExpertStandards is not null && request.ExpertStandards?.Count > 0)
            {
                var expertIds = new List<long>();
                foreach (var item in request.ExpertStandards)
                {
                    if (expertIds.Any(x => x == item.Id))
                        return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.ExistsExpertInList);
                    else
                        expertIds.Add(item.Id);
                }
                var expertsData = await _mediator.Send(new GetsSkillByIdQuery(expertIds, true, 1, expertIds.Count), ct); // بره سراغ متا دیتا
                if (expertsData.IsFailure)
                    return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.UnValidExperts);
                foreach (var item in request.ExpertStandards)
                {
                    if (!string.IsNullOrEmpty(item.TimeSpant))
                    {
                        if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                            return Result.Failure<CreateOperationInfoResponse>(MachineryStandardErrors.TimeSpantCountError);

                        if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                            return Result.Failure<CreateOperationInfoResponse>(MachineryStandardErrors.MoreThan59Min);
                    }

                    var expertData = expertsData.Value?.Data?.Where(c => c?.Id == item?.Id).FirstOrDefault();
                    expertRequests.Add(new OperationInfoExpertServiceModel(item!.Id, expertData?.Name, expertData?.Code, item!.ExpertNumber, TimeCalculator.StringToTicks(item.TimeSpant), item!.UnusedPercentage));
                }
            }

            var machineryRequests = new List<OperationInfoMachineryServiceModel?>();
            if (request.MachineryStandards is not null && request.MachineryStandards?.Count > 0)
            {
                var machineryIds = new List<long>();
                foreach (var item in request.MachineryStandards)
                {
                    if (machineryIds.Any(x => x == item.Id))
                        return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.ExistsMachineryInList);
                    else
                        machineryIds.Add(item.Id);
                }
                var machineriesData = await _mediator.Send(new GetsMachineryByIdsQuery(machineryIds, 1, machineryIds.Count), ct); // بره سراغ متا دیتا
                if (machineriesData.IsFailure)
                    return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.UnValidMachineries);
                foreach (var item in request.MachineryStandards)
                {
                    if (!string.IsNullOrEmpty(item.TimeSpant))
                    {
                        if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                            return Result.Failure<CreateOperationInfoResponse>(MachineryStandardErrors.TimeSpantCountError);

                        if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                            return Result.Failure<CreateOperationInfoResponse>(MachineryStandardErrors.MoreThan59Min);
                    }

                    var machinerieData = machineriesData.Value?.Data?.Where(c => c?.Id == item?.Id).FirstOrDefault();
                    machineryRequests.Add(new OperationInfoMachineryServiceModel(machinerieData!, item!.MachineryNumber, TimeCalculator.StringToTicks(item.TimeSpant), item!.UnusedPercentage));
                }
            }

            var productRequests = new List<OperationInfoGoodsServiceModel?>();
            if (request.GoodsStandards is not null && request.GoodsStandards?.Count > 0)
            {
                List<long>? productIds = [];
                List<Group>? groups = [];
                if (request.GoodsStandards.Any(x => x.StandardProductType == Domain.Entities.OperationInfos.Enums.StandardProductType.ProductGroup))
                {
                    foreach (var item in request.GoodsStandards.Where(x => x.StandardProductType == Domain.Entities.OperationInfos.Enums.StandardProductType.ProductGroup))
                    {
                        if (productIds.Any(x => x == item.Id))
                            return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.ExistsProductInList);
                        else
                            productIds.Add(item.Id);

                        var productsData = await _mediator.Send(new GetGroupsByIdsQuery(productIds, 1, productIds.Count), ct); // بره سراغ انبار
                        if (productsData.IsFailure)
                            return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.UnValidProducts);
                        groups = productsData.Value?.Data;
                    }
                }

                List<long>? categoryIds = [];
                List<WarehouseCategory>? warehouseCategories = [];
                if (request.GoodsStandards.Any(x => x.StandardProductType == StandardProductType.Category))
                {
                    foreach (var item in request.GoodsStandards.Where(x => x.StandardProductType == StandardProductType.Category))
                    {
                        if (categoryIds.Any(x => x == item.Id))
                            return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.ExistsCategoryInList);
                        else
                            categoryIds.Add(item.Id);

                        var ids = categoryIds.Adapt<List<long?>>();
                        var categoriesData = await _mediator.Send(new GetsWarehouseCategoryByIdQuery(1, ids.Count, ids, false, null), ct); // بره سراغ انبار
                        if (categoriesData.IsFailure)
                            return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.UnValidCategories);
                        warehouseCategories = categoriesData.Value?.Data;
                    }
                }

                foreach (var item in request.GoodsStandards)
                {
                    if (item.StandardProductType == StandardProductType.ProductGroup)
                    {
                        var productData = groups?.Where(c => c?.Id == item?.Id).FirstOrDefault();
                        productRequests.Add(new OperationInfoGoodsServiceModel(item!.Id, productData?.Name, productData?.Code, item!.GoodsNumber, item!.UnusedPercentage, productData?.Name, item.StandardProductType!.Value, item.ProductAllowedType!.Value));
                    }

                    if (item.StandardProductType == StandardProductType.Category)
                    {
                        var categoryData = warehouseCategories?.Where(c => c?.Id == item?.Id).FirstOrDefault();
                        productRequests.Add(new OperationInfoGoodsServiceModel(item!.Id, categoryData?.Title, categoryData?.Code, item!.GoodsNumber, item!.UnusedPercentage, categoryData?.Title, item.StandardProductType!.Value, item.ProductAllowedType!.Value));
                    }
                }
            }

            List<OperationInfoServiceModel>? serviceInfos = new();
            if (request.ServiceInfos is not null && request.ServiceInfos.Count > 0)
            {
                foreach (var serviceInfo in request.ServiceInfos)
                {
                    if (serviceInfos.Any(x => x.ServiceInfo.Id == serviceInfo.ServiceId))
                        continue;
                    var getServiceInfo = await _mediator.Send(new GetServiceInfoByIdQuery(serviceInfo.ServiceId!), ct);
                    if (getServiceInfo.IsFailure)
                        return Result.Failure<CreateOperationInfoResponse>(ServiceInfoErrors.ServiceInfoWithIdNotFound);

                    if (!string.IsNullOrEmpty(serviceInfo.TimeSpant))
                    {
                        if (!(serviceInfo.TimeSpant.Split(':')[0].Count() >= 2 && serviceInfo.TimeSpant.Split(':')[1].Count() == 2))
                            return Result.Failure<CreateOperationInfoResponse>(MachineryStandardErrors.TimeSpantCountError);

                        if (int.Parse(serviceInfo.TimeSpant.Split(':')[1]) > 59)
                            return Result.Failure<CreateOperationInfoResponse>(MachineryStandardErrors.MoreThan59Min);
                    }

                    serviceInfos.Add(new(getServiceInfo.Value!, TimeCalculator.StringToTicks(serviceInfo.TimeSpant)));
                }
            }

            List<Season>? seasons = new();
            if (request.SeasonIds is not null && request.SeasonIds.Count > 0)
            {
                foreach (var SeasonId in request.SeasonIds)
                {
                    if (seasons.Any(x => x.Id == SeasonId))
                        continue;
                    var getSeason = await _mediator.Send(new GetSeasonByIdQuery(SeasonId!), ct);
                    if (getSeason.IsFailure)
                        return Result.Failure<CreateOperationInfoResponse>(SeasonErrors.SeasonWithIdNotFound);
                    seasons.Add(getSeason.Value!);
                }
            }

            List<OperationInfoGroup>? operationInfoGroups = new();
            if (request.OperationInfoGroupIds is not null && request.OperationInfoGroupIds.Count > 0)
            {
                foreach (var operationInfoGroupId in request.OperationInfoGroupIds)
                {
                    if (operationInfoGroups.Any(x => x.Id == operationInfoGroupId))
                        continue;
                    var getOperationInfoGroup = await _mediator.Send(new GetOperationInfoGroupByIdQuery(operationInfoGroupId!), ct);
                    if (getOperationInfoGroup.IsFailure)
                        return Result.Failure<CreateOperationInfoResponse>(OperationInfoGroupErrors.OperationInfoGroupWithIdNotFound);
                    operationInfoGroups.Add(getOperationInfoGroup.Value!);
                }
            }

            OperationInfo? operationInfoDependency = null;

            if (request.OperationInfoDependency is not null && request.Priority == null)
                return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.CanNotaddDependency);
            if (request.OperationInfoDependency is not null && request.Priority == 1)
                return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.CanNotGetDependency);
            if (request.OperationInfoDependency is not null)
            {
                var getDependency = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.OperationInfoDependency.RelationId), ct);
                if (getDependency.IsFailure)
                    return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.OperationInfoDependencyWithIdNotFound);
                if (request.OperationInfoDependency.DependencyType == Domain.Entities.OperationInfos.Enums.OperationInfoDependencyType.StartDate && request.OperationInfoDependency.WorkingDays < 0)
                    return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.WorkingDayIsLow);
                if (getDependency.Value!.Priority == null)
                    return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.PriorityIsNull);
                if (getDependency.Value!.Priority == request.Priority)
                    return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.CanNotRealatedtwoSamePriority);
                if (getDependency.Value!.Priority > request.Priority)
                    return Result.Failure<CreateOperationInfoResponse>(OperationInfoErrors.CanNotRealatedforDependencyPriority);

                operationInfoDependency = getDependency.Value;
            }

            var measureunitData = await _mediator.Send(new GetMeasureunitByIdQuery(request.UnitOfMeasurementId), ct);
            if (measureunitData.IsFailure)
                return Result.Failure<CreateOperationInfoResponse>(MetaDataErrors.MeasureunitWithIdNotFound!);

            var response = await _mediator.Send(new CreateOperationInfoCommand(
                seasons,
                serviceInfos,
                operationInfoGroups,
                request.OperationInfoName,
                request.OperationInfoCode,
                request.OperationLatinName,
                request.IsPriceList,
                request.Priority,
                request.UnitOfMeasurementId,
                request.BasePrice,
                request.OperationInfoDependency,
                request.IsActive,
                expertRequests, productRequests,
                machineryRequests,
                request.OperationInfoActions,
                companyId), ct);
            if (response.IsFailure)
                return Result.Failure<CreateOperationInfoResponse>(response.Error!);

            await _unitOfWork.CommitAsync(ct);

            if (request.OperationInfoDependency is not null && operationInfoDependency is not null)
            {
                var createDependencyResponse = await _mediator.Send(new CreateOperationInfoDependencyCommand(operationInfoDependency, response.Value!.Id,
                       request.OperationInfoDependency.WorkingDays, request.OperationInfoDependency.DependencyType), ct);
                if (createDependencyResponse.IsFailure)
                    return Result.Failure<CreateOperationInfoResponse>(createDependencyResponse.Error!);

                await _unitOfWork.CommitAsync(ct);
            }

            scope.Complete();
            return new CreateOperationInfoResponse(response.Value!.Id, true);
        }
    }

    public async Task<Result<CreateOperationInfoActionResponse>> CreateOperationInfoAction(
        CreateOperationInfoActionRequest request, CT ct)
    {
        var oInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.OperationInfoId));
        if (oInfo.IsBad() || oInfo.Value == null)
            return oInfo.Failure<CreateOperationInfoActionResponse>();
        var action = _actionRepository.GetActionById(request.ActionId, ct);
        if (action == null)
            return Result.Failure<CreateOperationInfoActionResponse>(ActionErrors.ActionWithIdNotFound)!;
        var result = await CreateOperationInfoActionHandler(action.Result, oInfo.Value, request.Price ?? 0, ct);
        if (result.IsBad())
            return result.Failure<CreateOperationInfoActionResponse>();

        await _unitOfWork.CommitAsync(ct);
        return new CreateOperationInfoActionResponse(result.Value!.Id, true);
    }

    public async Task<Result<GetOIActionByOperationInfoIdResponse>> GetOIActionByOperationInfoId(
        GetOIActionByOperationInfoIdRequest request, CT ct)
    {
        var response = await GetOIActionByOperationInfoIdQuery(request, ct);
        if (response == null)
            return Result.Failure<GetOIActionByOperationInfoIdResponse>(ActionErrors.ActionWithIdNotFound)!;

        return response;
    }

    public async Task<Result<CreateOperationInfoActionsResponse>> CreateOperationInfoActions(
        CreateOperationInfoActionsRequest request, CT ct)
    {
        var oInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.OperationInfoId));
        if (oInfo.IsBad() || oInfo.Value == null)
            return oInfo.Failure<CreateOperationInfoActionsResponse>();
        var result = await CreateOperationInfoActionsPrivate(request, oInfo.Value, ct);

        await _unitOfWork.CommitAsync(ct);
        return new CreateOperationInfoActionsResponse(true);
    }

    public async Task<Result<OperationInfoExcelImportsResponse>> OperationInfoExcelImports(
        OperationInfoExcelImportsRequest request, CT ct)
    {
        var operationInfos = ExcelImporter.Import<OperationInfoExcelImportsModel>(request.DocumentFile);
        if (operationInfos is null)
            return Result.Failure<OperationInfoExcelImportsResponse>(GlobalErrors.ErrorOnReadFile)!;

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<OperationInfoExcelImportsResponse>(GlobalErrors.InvalidCompany)!;

        var names = operationInfos.Select(x => x.OperationInfo).ToList();
        var unitName = operationInfos.Listed(x => x.UnitOfMeasurementName);
        var seasons = operationInfos.Listed(x => x.SeasonName);
        var branchName = operationInfos.Listed(x => x.BranchName);
        var categoryName = operationInfos.Listed(x => x.CategoryName);

        var checkUnitNames = unitName.Select(StringSeparator.Normalize).ToList();
        var checkSeason = seasons.Select(StringSeparator.Normalize).ToList();
        var checkBranch = branchName.Select(StringSeparator.Normalize).ToList();
        var checkCategory = categoryName.Select(StringSeparator.Normalize).ToList();

        var unitOfMeasure = await _measureUnitRepository.GetByNames(checkUnitNames, companyId!.Value, ct);
        if (unitOfMeasure is null)
            return Result.Failure<OperationInfoExcelImportsResponse>(OperationInfoErrors.InvalidImportStatus(string.Join(", ", unitName), null, null, null))!;
        var categoryRes = await _categoryRepository.GetCategoryByNames(categoryName, ct);
        if (categoryRes is null)
            return Result.Failure<OperationInfoExcelImportsResponse>(OperationInfoErrors.InvalidImportStatus(null, string.Join(", ", categoryName), null, null))!;

        List<string> unitNames = new();
        List<string> seasonNames = new();
        List<string> branchNames = new();
        List<string> categoryNames = new();

        var allBranches = categoryRes
            .SelectMany(c => c.Branchs)
            .ToList();

        var allSeasons = allBranches
            .SelectMany(b => b.Seasons)
            .ToList();

        foreach (var operationInfo in operationInfos.Where(x => x.OperationInfoCode is not null))
        {
            if (!unitOfMeasure.Any(x => StringSeparator.Normalize(x.Name) == StringSeparator.Normalize(operationInfo.UnitOfMeasurementName)))
                unitNames.Add(operationInfo.UnitOfMeasurementName);

            var category = categoryRes
                .FirstOrDefault(c => StringSeparator.Normalize(c.CategoryName) == StringSeparator.Normalize(operationInfo.CategoryName));
            if (category is null)
            {
                categoryNames.Add(operationInfo.CategoryName);
                continue;
            }

            var branch = category.Branchs
                .FirstOrDefault(b => StringSeparator.Normalize(b.BranchName) == StringSeparator.Normalize(operationInfo.BranchName));
            if (branch is null)
            {
                branchNames.Add(operationInfo.BranchName);
                continue;
            }

            var season = branch.Seasons
                .FirstOrDefault(s => StringSeparator.Normalize(s.SeasonName) == StringSeparator.Normalize(operationInfo.SeasonName));
            if (season is null)
            {
                seasonNames.Add(operationInfo.SeasonName);
            }
        }


        unitNames = unitNames.Distinct().ToList();
        seasonNames = seasonNames.Distinct().ToList();
        branchNames = branchNames.Distinct().ToList();
        categoryNames = categoryNames.Distinct().ToList();

        var unitNamesStr = string.Join(", ", unitNames);
        var seasonNamesStr = string.Join(", ", seasonNames);
        var branchNamesStr = string.Join(", ", branchNames);
        var categoryNamesStr = string.Join(", ", categoryNames);

        if (unitNames.Any() || categoryNames.Any() || branchNames.Any() || seasonNames.Any())
            return Result.Failure<OperationInfoExcelImportsResponse>(OperationInfoErrors.InvalidImportStatus(unitNamesStr, categoryNamesStr, branchNamesStr, seasonNamesStr))!;

        foreach (var item in operationInfos.Where(x => x.OperationInfoCode is not null))
        {
            var oCategory = categoryRes
                .FirstOrDefault(x => StringSeparator.Normalize(x.CategoryName) == StringSeparator.Normalize(item.CategoryName));
            if (oCategory is null)
                return Result.Failure<OperationInfoExcelImportsResponse>(CategoryErrors.NotFound)!;

            var oBranch = allBranches
                .FirstOrDefault(x => StringSeparator.Normalize(x.BranchName) == StringSeparator.Normalize(item.BranchName));
            if (oBranch is null)
                return Result.Failure<OperationInfoExcelImportsResponse>(SeasonErrors.SeasonBranchNotFound)!;

            var oSeason = allSeasons
                .FirstOrDefault(x => StringSeparator.Normalize(x.SeasonName) == StringSeparator.Normalize(item.SeasonName));
            if (oSeason is null)
                return Result.Failure<OperationInfoExcelImportsResponse>(SeasonErrors.SeasonWithNameNotFound)!;

            var unitId = unitOfMeasure
                .FirstOrDefault(x => StringSeparator.Normalize(x.Name) == StringSeparator.Normalize(item.UnitOfMeasurementName));

            if (unitId is null)
                return Result.Failure<OperationInfoExcelImportsResponse>(OperationInfoErrors.UnitOfMeasurementNameIsNotFound)!;

            var result = await _mediator.Send(new CreateOperationInfoCommand(
                oSeason!.ToDataList(),
                null,
                null,
                item.OperationInfo,
                item.OperationInfoCode,
                null,
                true,
                null,
                unitId!.Id,
                item.BasePrice,
                null,
                true,
                null,
                null,
                null,
                null,
                companyId));
            if (result.IsBad())
                return result.Failure<OperationInfoExcelImportsResponse>()!;
        }


        await _unitOfWork.CommitAsync(ct);


        return new OperationInfoExcelImportsResponse(true);
    }


    //fehrestBaha:
    public async Task<Result<FehrestBahaExcelImportsResponse>> FehrestBahaExcelImports(
    FehrestBahaExcelImportsRequest request,
    CT ct)
    {
        _logger.LogInformation("Engineering - FehrestBahaExcelImports");

        try
        {
            var branches = await _branchRepository.GetsBranchByIds([request.BranchId], ct);
            var branch = branches.FirstOrDefault();
            if (branch is null)
                return Result.Failure<FehrestBahaExcelImportsResponse>(
                    SharedErrors.UnknownError)!;
            var year = await _viewFinancialPeriodRepository.GetByYearId(request.YearId, ct);
            if (year is null)
                return Result.Failure<FehrestBahaExcelImportsResponse>(
                    SharedErrors.UnknownError)!;
            // Removes extra space and year
            string yearNumber = Regex.Replace(year.NameEn, @"\D", "");
            var fileName = $"{yearNumber}-{branch.Category.CategoryCode}-{branch.BranchCode}.xlsx";
            var filePath = Path.Combine(AppContext.BaseDirectory, "Reports", "FehrestBaha", fileName);
            var formFile = filePath.ToIFormFile();
           
            var items = ExcelImporter.Import<FehrestBahaExcelModel>(formFile);
            if (items is null)
                return Result.Failure<FehrestBahaExcelImportsResponse>(GlobalErrors.ErrorOnReadFile)!;
            var result = await CreateFehrestBahaCommand(
                items,
                request.BranchId,
                request.CompanyId,
                request.YearId,
                ct);
            if (result.IsBad())
                return result.Failure<FehrestBahaExcelImportsResponse>()!;
            await _unitOfWork.CommitAsync(ct);
            return result.Value!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in FehrestBahaExcelImports");
            return Result.Failure<FehrestBahaExcelImportsResponse>(
                SharedErrors.UnknownError)!;
        }
    }

    //RasteReshte:
    public async Task<Result<RasteReshteExcelImportsResponse>> RasteReshteExcelImports(
    RasteReshteExcelImportsRequest request,
    CT ct)
    {
        _logger.LogInformation("Engineering - RasteReshteExcelImports");
        long companyId = request.companyId;

        var transactionOptions = new System.Transactions.TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        };
        using var scope = new System.Transactions.TransactionScope(
            System.Transactions.TransactionScopeOption.Required,
            transactionOptions,
            System.Transactions.TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var (categories, branches, seasons) = RasteReshteExcelImporter.Import(request.DocumentFile);
            var result = await CreateRasteReshteCommand(categories, branches, seasons, companyId, ct);
            if (result.IsBad())
                return result.Failure<RasteReshteExcelImportsResponse>()!;
            await _unitOfWork.CommitAsync(ct);
            scope.Complete();
            return new RasteReshteExcelImportsResponse(true, result.Value!.Categories, result.Value.Branches, result.Value.Seasons);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in RasteReshteExcelImports");
            return Result.Failure<RasteReshteExcelImportsResponse>(SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<OperationInfoCodeCreatorResponse?>> CodeCreator(
        OperationInfoCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<OperationInfoCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new CodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<OperationInfoCodeCreatorResponse>(response.Error!);

        return new OperationInfoCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<StateChangerOperationInfosResponse?>> StateChangerOperationInfos(
        StateChangerOperationInfosRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerOperationInfos, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerOperationInfosValidator, StateChangerOperationInfosRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerOperationInfosResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerOperationInfosResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsOperationInfoByIdsQuery(request.Ids, 1, request.Ids.Count), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerOperationInfosResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerOperationInfosCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerOperationInfosResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerOperationInfosResponse(true);
    }

    public async Task<Result<DisableOperationInfoResponse?>> DisableOperationInfo(
        DisableOperationInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableOperationInfo, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableOperationInfoValidator, DisableOperationInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableOperationInfoResponse>(isValidRequest.Error!);

        var disableOperationInfoResponse = await _mediator.Send(new DisableOperationInfoCommand(request.Id), ct);
        if (disableOperationInfoResponse.IsFailure)
            return Result.Failure<DisableOperationInfoResponse>(disableOperationInfoResponse.Error!);

        var disabledData = disableOperationInfoResponse.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new DisableOperationInfoResponse(disabledData.Id, true);
    }

    public async Task<Result<OperationInfoGroupDeleteResponse?>> OperationInfoGroupDelete(
        OperationInfoGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for OperationInfoGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<OperationInfoGroupDeleteValidator, OperationInfoGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<OperationInfoGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableOperationInfoCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<OperationInfoGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new OperationInfoGroupDeleteResponse(true);
    }

    public async Task<Result<SetOperationInfoPriorityResponse?>> SetOperationInfoPriority(
        SetOperationInfoPriorityRequest request, CT ct)
    {
        _logger.LogInformation("Request for SetOperationInfoPriority, id:{Id},", request.Id);

        var isValidRequest = await request.IsValidAsync<SetOperationInfoPriorityValidator, SetOperationInfoPriorityRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetOperationInfoPriorityResponse>(isValidRequest.Error!);

        var operationInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.Id), ct);
        if (operationInfo.IsFailure)
            return Result.Failure<SetOperationInfoPriorityResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);

        if (request.SetPriority <= 0)
            return Result.Failure<SetOperationInfoPriorityResponse>(OperationInfoErrors.PriorityCanNot0);

        var getDependencies = await _mediator.Send(new FindOperationInfoDependenciesQuery(request.Id), ct);
        if (getDependencies.Value?.Data is not null && request.SetPriority != operationInfo.Value!.Priority)
        {
            var ids = getDependencies.Value.Data.Select(x => x.RelationId).ToList();
            var getOperationInfos = await _mediator.Send(new GetsOperationInfoByIdsIncludeLessQuery(ids), ct);

            var priorities = getOperationInfos.Value!.Data!.Select(x => x.Priority).ToList();
            if (priorities.Any(x => x <= request.SetPriority))
                return Result.Failure<SetOperationInfoPriorityResponse>(OperationInfoErrors.PriorityIsGreatertThanDependenciesPriority);
        }

        var getOldRelation = await _mediator.Send(new GetOperationInfoDependenceQuery(request.Id), ct);
        if (getOldRelation.Value is not null && request.SetPriority <= getOldRelation.Value.OperationInfo.Priority)
            return Result.Failure<SetOperationInfoPriorityResponse>(OperationInfoErrors.PriorityIsLesstThanDependenciesPriority);

        var response = await _mediator.Send(new SetOperationInfoPriorityCommand(request.Id, request.SetPriority), ct);
        if (response.IsFailure)
            return Result.Failure<SetOperationInfoPriorityResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetOperationInfoPriorityResponse(response.Value!.Id, response.Value!.Priority);
    }

    public async Task<Result<UpdateOperationInfoResponse?>> UpdateOperationInfo(
        UpdateOperationInfoRequest request, CT ct)
    {
        var transactionOptions = new TransactionOptions();
        transactionOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
        {
            _logger.LogInformation("Request for UpdateOperationInfo, id:{Id}, OperationInfo:{OperationInfoName} , description:{OperationInfoCode},", request.Id, request.OperationInfoName, request.OperationInfoCode);

            var isValidRequest = await request.IsValidAsync<UpdateOperationInfoValidator, UpdateOperationInfoRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<UpdateOperationInfoResponse>(isValidRequest.Error!);
            if (request.Priority <= 0)
                return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.PriorityCanNot0);
            //Find OperationInfo
            var operationInfo = await _mediator.Send(new GetOperationInfoByIdWithChildQuery(request.Id), ct);
            if (operationInfo.IsFailure)
                return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
            var value = operationInfo.Value!;
            long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
            if (companyId >= 1)
            {
                var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
                if (companyResponse.IsFailure)
                    return Result.Failure<UpdateOperationInfoResponse>(companyResponse.Error!);
            }

            //Validate name
            if (request.OperationInfoName != value.OperationInfoName)
            {
                var nameIsDuplicate = await _mediator.Send(new GetOperationInfoByNameQuery(request.OperationInfoName, request.UnitOfMeasurementId, companyId), ct);
                if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
                    return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.NameIsDuplicate);
            }
            //Validate code
            if (request.OperationInfoCode != value.OperationInfoCode)
            {
                var codeIsDuplicate = await _mediator.Send(new GetOperationInfoByCodeQuery(request.OperationInfoCode, companyId), ct);
                if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
                    return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.CodeIsDuplicate);
            }

            if (request.ExpertStandards is not null && request.ExpertStandards?.Count > 0)
            {
                var expertStandards = request.ExpertStandards.OrderByDescending(x => (x.IsDeleted is null || x.IsDeleted == false) && x.OperationInfoExpertId != null).ThenByDescending(x => x.IsDeleted).ToList();

                var expertIds = expertStandards.Where(x => x != null && (x.IsDeleted == false || x.IsDeleted is null)).Select(c => c.Id).ToList();
                if (expertIds.Count > 0)
                {
                    var expertsData = await _mediator.Send(new GetsSkillByIdQuery(expertIds, true, 1, expertIds.Count), ct); // بره سراغ متا دیتا
                    if (expertsData.IsFailure)
                        return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.UnValidExperts);
                }

                foreach (var item in expertStandards)
                {
                    if (item.OperationInfoExpertId is null && item.IsDeleted == true)
                        return Result.Failure<UpdateOperationInfoResponse>(ExpertStandardErrors.CantDelete);

                    if (!string.IsNullOrEmpty(item.TimeSpant))
                    {
                        if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                            return Result.Failure<UpdateOperationInfoResponse>(MachineryStandardErrors.TimeSpantCountError);

                        if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                            return Result.Failure<UpdateOperationInfoResponse>(MachineryStandardErrors.MoreThan59Min);
                    }

                    if (item.IsDeleted == true)
                    {
                        var deleteExpert = await _mediator.Send(new DisableExpertCommand((long)item.OperationInfoExpertId!), ct);
                        if (deleteExpert.IsFailure)
                            return Result.Failure<UpdateOperationInfoResponse>(deleteExpert.Error!);
                    }
                    else if ((item.IsDeleted == false || item.IsDeleted is null) && item.OperationInfoExpertId is not null)
                    {
                        if (value.ConsumptionStandardExperts.Any(x => x.ExpertUnitId == item.Id && x.Id != item.OperationInfoExpertId))
                            return Result.Failure<UpdateOperationInfoResponse>(ExpertStandardErrors.ExpertIsDuplicate);
                        var updateExpert = await _mediator.Send(new UpdateExpertCommand((long)item.OperationInfoExpertId, item.Id, item.ExpertNumber, TimeCalculator.StringToTicks(item.TimeSpant), item.UnusedPercentage), ct);
                        if (updateExpert.IsFailure)
                            return Result.Failure<UpdateOperationInfoResponse>(updateExpert.Error!);
                    }
                    else if (item.OperationInfoExpertId is null)
                    {
                        if (value.ConsumptionStandardExperts.Any(x => x.ExpertUnitId == item.Id))
                            return Result.Failure<UpdateOperationInfoResponse>(ExpertStandardErrors.ExpertIsDuplicate);
                        var createExpert = await _mediator.Send(new CreateExpertCommand(value, item.Id, item.ExpertNumber, TimeCalculator.StringToTicks(item.TimeSpant), item.UnusedPercentage), ct);
                        if (createExpert.IsFailure)
                            return Result.Failure<UpdateOperationInfoResponse>(createExpert.Error!);
                    }
                }
            }

            if (request.MachineryStandards is not null && request.MachineryStandards?.Count > 0)
            {
                var machineryStandards = request.MachineryStandards.OrderByDescending(x => (x.IsDeleted is null || x.IsDeleted == false) && x.OperationInfoMachineryId != null).ToList();
                machineryStandards = machineryStandards.OrderByDescending(x => x.IsDeleted).ToList();

                var machineryIds = machineryStandards.Where(x => x.IsDeleted == false || x.IsDeleted is null).Select(c => c.Id).Where(x => x != 0).ToList();
                var machineriesData = new List<Machinery?>();
                if (machineryIds.Count > 0)
                {
                    var responseData = await _mediator.Send(new GetsMachineryByIdsQuery(machineryIds, 1, machineryIds.Count), ct); // بره سراغ متا دیتا
                    if (responseData.IsFailure)
                        return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.UnValidMachineries);

                    machineriesData = responseData.Value?.Data;
                }

                foreach (var item in machineryStandards)
                {
                    if (value.ConsumptionStandardMachineries.Any(x => x.Machinery.Id == item.Id && item.OperationInfoMachineryId is null))
                        return Result.Failure<UpdateOperationInfoResponse>(MachineryStandardErrors.MachineryIsDuplicate);
                    if (item.OperationInfoMachineryId is null && item.IsDeleted == true)
                        return Result.Failure<UpdateOperationInfoResponse>(MachineryStandardErrors.CantDelete);

                    if (!string.IsNullOrEmpty(item.TimeSpant))
                    {
                        if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                            return Result.Failure<UpdateOperationInfoResponse>(MachineryStandardErrors.TimeSpantCountError);

                        if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                            return Result.Failure<UpdateOperationInfoResponse>(MachineryStandardErrors.MoreThan59Min);
                    }

                    if (item.IsDeleted == true)
                    {
                        var deleteMachinery = await _mediator.Send(new DisableMachineryCommand((long)item.OperationInfoMachineryId!), ct);
                        if (deleteMachinery.IsFailure)
                            return Result.Failure<UpdateOperationInfoResponse>(deleteMachinery.Error!);
                    }
                    else if ((item.IsDeleted == false || item.IsDeleted is null) && item.OperationInfoMachineryId is not null)
                    {
                        var machineryData = machineriesData?.Where(x => x!.Id == item.Id).FirstOrDefault();
                        var updateMachinery = await _mediator.Send(new UpdateMachineryCommand((long)item.OperationInfoMachineryId, machineryData!, item.MachineryNumber, TimeCalculator.StringToTicks(item.TimeSpant), item.UnusedPercentage), ct);
                        if (updateMachinery.IsFailure)
                            return Result.Failure<UpdateOperationInfoResponse>(updateMachinery.Error!);
                    }
                    else if (item.OperationInfoMachineryId is null)
                    {
                        var machineryData = machineriesData?.Where(x => x!.Id == item.Id).FirstOrDefault();
                        var createMachinery = await _mediator.Send(new CreateMachineryCommand(value, machineryData!, item.MachineryNumber, TimeCalculator.StringToTicks(item.TimeSpant), item.UnusedPercentage), ct);
                        if (createMachinery.IsFailure)
                            return Result.Failure<UpdateOperationInfoResponse>(createMachinery.Error!);
                    }
                }
            }

            if (request.GoodsStandards is not null && request.GoodsStandards?.Count > 0)
            {
                List<Group>? groups = [];
                var productStandards = request.GoodsStandards.OrderByDescending(x => (x.IsDeleted is null || x.IsDeleted == false) && x.OperationInfoGoodsId != null).ToList();
                productStandards = productStandards.OrderByDescending(x => x.IsDeleted).ToList();

                var productIds = request.GoodsStandards.Where(x => (x.IsDeleted == false || x.IsDeleted is null) && x.StandardProductType == StandardProductType.ProductGroup)
                    .Select(c => c!.Id).Where(x => x != 0).ToList();
                if (productIds is not null && productIds.Count > 0)
                {
                    var productsData = await _mediator.Send(new GetGroupsByIdsQuery(productIds, 1, productIds.Count), ct); // بره سراغ انبار
                    if (productsData.IsFailure)
                        return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.UnValidProducts);
                    groups = productsData.Value?.Data;
                }

                var categoryIds = request.GoodsStandards.Where(x => (x.IsDeleted == false || x.IsDeleted is null) && x.StandardProductType == StandardProductType.Category)
                    .Select(c => c!.Id).Where(x => x != 0).ToList();
                if (categoryIds is not null && categoryIds.Count > 0)
                {
                    var categoriesData = await WebServicesLogic.CategoriesDataReceiver(categoryIds, null, _mediator, ct); // بره سراغ انبار
                    if (categoriesData is null)
                        return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.UnValidCategories);
                }

                foreach (var item in productStandards)
                {
                    if (value.ConsumptionStandardProduct.Where(x => x.StandardProductType == StandardProductType.ProductGroup)
                        .Any(x => x.ProductUnitId == item.Id && item.OperationInfoGoodsId is null))
                        return Result.Failure<UpdateOperationInfoResponse>(ProductStandardErrors.ProductIsDuplicate);

                    if (value.ConsumptionStandardProduct.Where(x => x.StandardProductType == StandardProductType.Category)
                        .Any(x => x.ProductUnitId == item.Id && item.OperationInfoGoodsId is null))
                        return Result.Failure<UpdateOperationInfoResponse>(ProductStandardErrors.CategoryIsDuplicate);

                    if (item.OperationInfoGoodsId is null && item.IsDeleted == true)
                        return Result.Failure<UpdateOperationInfoResponse>(ProductStandardErrors.CantDelete);

                    if (item.IsDeleted == true)
                    {
                        var deleteProduct = await _mediator.Send(new DisableProductCommand((long)item.OperationInfoGoodsId!), ct);
                        if (deleteProduct.IsFailure)
                            return Result.Failure<UpdateOperationInfoResponse>(deleteProduct.Error!);
                    }
                    else if ((item.IsDeleted == false || item.IsDeleted is null) && item.OperationInfoGoodsId is not null)
                    {
                        var updateProduct = await _mediator.Send(new UpdateProductCommand((long)item.OperationInfoGoodsId, item.Id!, item.GoodsNumber, item.UnusedPercentage, item.StandardProductType!.Value, item.ProductAllowedType!.Value), ct);
                        if (updateProduct.IsFailure)
                            return Result.Failure<UpdateOperationInfoResponse>(updateProduct.Error!);
                    }
                    else if (item.OperationInfoGoodsId is null)
                    {
                        var createProduct = await _mediator.Send(new CreateProductCommand(value, item.Id!, item.GoodsNumber, item.UnusedPercentage, item.StandardProductType!.Value, item.ProductAllowedType!.Value), ct);
                        if (createProduct.IsFailure)
                            return Result.Failure<UpdateOperationInfoResponse>(createProduct.Error!);
                    }
                }
            }

            if (request.Priority != value.Priority)
            {
                var getDependency = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.Id), ct);
                if (getDependency.IsFailure)
                    return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);

                var getDependencies = await _mediator.Send(new FindOperationInfoDependenciesQuery(getDependency.Value!.Id), ct);
                if (getDependencies.Value?.Data is not null && request.Priority != value.Priority)
                {
                    var ids = getDependencies.Value.Data.Select(x => x.RelationId).ToList();
                    var getOperationInfos = await _mediator.Send(new GetsOperationInfoByIdsIncludeLessQuery(ids), ct);

                    var priorities = getOperationInfos.Value!.Data!.Select(x => x.Priority).ToList();
                    if (priorities.Any(x => x <= request.Priority))
                        return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.PriorityIsGreatertThanDependenciesPriority);
                }
            }

            if (request.OperationInfoDependency is not null && request.Priority == null)
                return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.CanNotaddDependency);
            if (request.OperationInfoDependency is not null && request.Priority == 1)
                return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.CanNotGetDependency);

            if (request.OperationInfoDependency is not null && request.Priority != 1)
            {
                if (request.OperationInfoDependency.RelationId == value.Id)
                    return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.CantCreateThisRelation);
                if (request.OperationInfoDependency.DependencyType == Domain.Entities.OperationInfos.Enums.OperationInfoDependencyType.StartDate && request.OperationInfoDependency.WorkingDays < 0)
                    return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.WorkingDayIsLow);

                var getDependency = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.OperationInfoDependency.RelationId), ct);
                if (getDependency.IsFailure)
                    return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.OperationInfoDependencyWithIdNotFound);
                if (getDependency.Value!.Priority == null)
                    return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.PriorityIsNull);
                if (value.Priority != request.Priority)
                {
                    if (request.Priority < getDependency.Value!.Priority)
                    {
                        if (getDependency.Value!.Priority == request.Priority)
                            return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.CanNotRealatedtwoSamePriority);
                        if (getDependency.Value!.Priority > request.Priority)
                            return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.CanNotRealatedforDependencyPriority);
                    }
                }
                else
                {
                    if (getDependency.Value!.Priority == value.Priority)
                        return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.CanNotRealatedtwoSamePriority);
                    if (getDependency.Value!.Priority > value.Priority)
                        return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.CanNotRealatedforDependencyPriority);
                }

                var getOldRelation = await _mediator.Send(new GetOperationInfoDependenceQuery(request.Id), ct);
                if (getOldRelation.Value is not null)
                {
                    var updateDependencyResponse = await _mediator.Send(new UpdateOperationInfoDependencyCommand(getOldRelation.Value.Id,
                         getDependency.Value, request.OperationInfoDependency.WorkingDays, request.OperationInfoDependency.DependencyType), ct);
                    if (updateDependencyResponse.IsFailure)
                        return Result.Failure<UpdateOperationInfoResponse>(updateDependencyResponse.Error!);
                }
                else
                {
                    var createDependencyResponse = await _mediator.Send(new CreateOperationInfoDependencyCommand(getDependency.Value, request.Id,
                        request.OperationInfoDependency.WorkingDays, request.OperationInfoDependency.DependencyType), ct);
                    if (createDependencyResponse.IsFailure)
                        return Result.Failure<UpdateOperationInfoResponse>(createDependencyResponse.Error!);
                }
            }

            if (request.OperationInfoDependency is null)
            {
                var dependencyInfo = await _mediator.Send(new GetOperationInfoDependenceQuery(request.Id), ct);
                if (dependencyInfo.Value is not null)
                {
                    var disableDependncy = await _mediator.Send(new DisableOperationInfoDependencyCommand(dependencyInfo.Value.Id), ct);
                    if (disableDependncy.IsFailure)
                        return Result.Failure<UpdateOperationInfoResponse>(disableDependncy.Error!);
                }
            }

            bool hasChanged = false;
            if (request.SeasonIds is not null && request.SeasonIds.Count > 0)
            {
                var createOperationInfoSeason = await _operationInfoSeasonLogic.CreateOperationInfoSeason(new(null, [value], request.SeasonIds, request.DeletedOperationInfoSeasonIds), ct);
                if (createOperationInfoSeason.IsFailure)
                    return Result.Failure<UpdateOperationInfoResponse>(createOperationInfoSeason.Error!);
                hasChanged = createOperationInfoSeason.Value!.HasCreated;
            }

            var response = await _mediator.Send(new UpdateOperationInfoCommand(value, request.OperationInfoCode, request.OperationInfoName,
                request.OperationLatinName, request.Priority, request.UnitOfMeasurementId, request.BasePrice, request.IsActive, hasChanged, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<UpdateOperationInfoResponse>(response.Error!);

            if (request.ServiceInfos is not null && request.ServiceInfos.Count > 0)
            {
                var createOperationInfoService = await _operationInfoServiceLogic.CreateOperationInfoService(new(null, [value], request.ServiceInfos), ct);
                if (createOperationInfoService.IsFailure)
                    return Result.Failure<UpdateOperationInfoResponse>(createOperationInfoService.Error!);
            }

            if (request.OperationInfoGroupIds is not null && request.OperationInfoGroupIds.Count > 0)
            {
                var createGroupRelation = await _operationInfoGroupRelationLogic.CreateOperationInfoGroupRelation(new(request.Id, request.OperationInfoGroupIds), ct);
                if (createGroupRelation.IsFailure)
                    return Result.Failure<UpdateOperationInfoResponse>(createGroupRelation.Error!);
            }

            if (request.ChangeUnitOfMeasurement)
            {
                var projectOperationResponse = await _mediator.Send(new GetsByOperationInfoIncludeLessQuery(request.Id, 0, 0), ct);
                var projectOperations = projectOperationResponse.Value?.Data;
                if (projectOperations is not null && projectOperations.Count > 0)
                {
                    var projectOperationChanged = await _mediator.Send(new UpdateProjectOperationUnitOfMeasurementCommand(projectOperations, request.UnitOfMeasurementId), ct);
                    if (projectOperationChanged.IsFailure)
                        return Result.Failure<UpdateOperationInfoResponse>(projectOperationChanged.Error!);
                }
            }

            var actionRequest = request.OperationInfoActions;
            if (actionRequest is not null && actionRequest?.Count > 0)
            {
                List<long> oInfoActions = [];
                List<OperationInfoAction> oInfoAction = [];
                if (actionRequest.Any(x => x.Id != null))
                {
                    oInfoActions = actionRequest
                        .Where(x => x.Id != null)
                        .Select(x => x.Id!.Value)
                        .ToList();
                    oInfoAction = await _operationInfoActionRepository.GetOperationInfoActions(
                        oInfoActions, ct);
                }

                List<long> createActions = [];
                List<Action> actions = [];
                if (actionRequest.Any(x => x.Id == null))
                {
                    createActions = actionRequest
                        .Where(x => x.Id == null)
                        .Select(x => x.actionId)
                        .ToList();
                    actions = await _actionRepository.GetActions(createActions, ct);
                }
                foreach (var oIAction in actionRequest)
                {
                    if (oIAction.Id != null && oIAction.IsDeleted == null && oIAction.IsDeleted == false)
                    {
                        var action = oInfoAction.FirstOrDefault(x => x.Id == oIAction.Id);
                        if (action is null)
                            return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.OperationInfoActionWithIdNotFound);
                        action.Update(oIAction.Price);
                    }
                    if (oIAction.Id != null && oIAction.IsDeleted != null && oIAction.IsDeleted == true)
                    {
                        var action = oInfoAction.FirstOrDefault(x => x.Id == oIAction.Id);
                        if (action is null)
                            return Result.Failure<UpdateOperationInfoResponse>(OperationInfoErrors.OperationInfoActionWithIdNotFound);
                        action.SoftDelete();
                    }
                    if (oIAction.Id == null)
                    {
                        var action = actions.FirstOrDefault(x => x.Id == oIAction.actionId);
                        if (action == null)
                            return Result.Failure<UpdateOperationInfoResponse>(ActionErrors.ActionWithIdNotFound);
                        var result = await _operationInfoActionRepository.Create(new OperationInfoAction(value, action, oIAction.Price), ct);
                    }
                }
            }
            await _unitOfWork.CommitAsync(ct);
            scope.Complete();
            return new UpdateOperationInfoResponse(response.Value!.Id, true);
        }
    }

    public async Task<Result<AddSeasonsToOperationInfosResponse?>> AddSeasonsToOperationInfos(
        AddSeasonsToOperationInfosRequest request, CT ct)
    {
        _logger.LogInformation("Request for OperationInfo, ids:{Ids}, SeasonIds :{SeasonIds} ,", request.Ids, request.SeasonIds);

        var isValidRequest = await request.IsValidAsync<AddSeasonsToOperationInfosValidator, AddSeasonsToOperationInfosRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<AddSeasonsToOperationInfosResponse>(isValidRequest.Error!);

        if (request.SeasonIds is not null && request.SeasonIds.Count > 0)
        {
            var createOperationInfoSeason = await _operationInfoSeasonLogic.CreateOperationInfoSeason(new(request.Ids, null, request.SeasonIds, null), ct);
            if (createOperationInfoSeason.IsFailure)
                return Result.Failure<AddSeasonsToOperationInfosResponse>(createOperationInfoSeason.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new AddSeasonsToOperationInfosResponse(true);
    }

    public async Task<Result<InactiveOperationInfoResponse?>> InactiveOperationInfo(
        InactiveOperationInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveOperationInfo, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveOperationInfoValidator, InactiveOperationInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveOperationInfoResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveOperationInfoCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveOperationInfoResponse>(response.Error!);

        var inactivatedData = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new InactiveOperationInfoResponse(inactivatedData.Id, inactivatedData.IsActive);
    }

    public async Task<Result<ActiveOperationInfoResponse?>> ActiveOperationInfo(
        ActiveOperationInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveOperationInfo, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveOperationInfoValidator, ActiveOperationInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveOperationInfoResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveOperationInfoCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveOperationInfoResponse>(response.Error!);

        var activedData = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new ActiveOperationInfoResponse(activedData.Id, activedData.IsActive);
    }

    public async Task<Result<GetOperationInfoByIdResponse?>> GetOperationInfoById(
        GetOperationInfoByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfoById, id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<GetOperationInfoByIdValidator, GetOperationInfoByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationInfoByIdResponse>(isValidRequest.Error!);

        //Find OperationInfo
        var response = await _mediator.Send(new GetOperationInfoByIdWithChildQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationInfoByIdResponse>(OperationInfoErrors.OperationInfoWithIdNotFound!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        //Find Measureunit
        var measureUnits = await MeasurementDataReceiver([value], ct);

        var dependencyInfo = await _mediator.Send(new GetOperationInfoDependenceQuery(value.Id), ct);
        OperationInfoDependencyModel? operationInfoDependency = null;
        if (dependencyInfo.Value is not null)
        {
            var dependency = dependencyInfo.Value!;
            operationInfoDependency = new OperationInfoDependencyModel(dependency.Id, dependency.OperationInfo.Id, dependency.OperationInfo.OperationInfoName, dependency.OperationInfo.OperationInfoCode,
                dependency.OperationInfo.Priority, dependency.WorkingDays, new((int)dependency.DependencyType, dependency.DependencyType.GetEnumDescription()));
        }

        var measurement = new OperationInfoMeasurementModel(value.UnitOfMeasurementId, measureUnits?.Where(m => m.Id == value.UnitOfMeasurementId).FirstOrDefault()?.Name);

        var ActionData = value.OperationInfoActions
        .Select(x => new GetOperationInfoActionModel(
            x.Id,
            x.ActionId,
            x.Action.ActionName,
            x.Action.ActionCode,
            x.Price
        ))
        .ToList();

        decimal price = 0;
        if (value.OperationInfoActions.Any(x => x.Price != 0))
            price = value.OperationInfoActions.Sum(x => x.Price)!.Value!;
        else
            price = value.BasePrice;

        var data = await FullModeling(value, measureUnits, operationInfoDependency, company, ct);
        var res = data.Adapt<GetOperationInfoByIdResponse>();
        var result = new GetOperationInfoByIdResponse(
            res.Id,
            res.OperationInfoName,
            res.OperationInfoCode,
            res.OperationLatinName,
            res.Priority,
            value.BasePrice,
            res.HaveStandard,
            res.MeasurementData,
            res.DependencyData,
            res.ServiceInfoData,
            res.ExpertData,
            res.GoodsData,
            res.MachineryData,
            res.CategoryData,
            res.BranchData,
            res.SeasonData,
            res.GroupData,
            ActionData,
            res.IsActive,
            res.HasChanged,
            res.CompanyId,
            res.CompanyNameFa
            );

        return result;
    }

    public async Task<Result<GetOperationInfoActionsResponse?>> GetOperationInfoActions(
        GetOperationInfoActionsRequest request, CT ct)
    {
        _logger.LogInformation("GetOperationInfoActions");

        var result = await _operationInfoActionRepository.GetsOperationInfoActions(request.OInfoId, request.FilterData, request.PageIndex, request.PageSize, ct);
        if (result.Data is null || result.Data.Count < 1)
            return Result.Failure<GetOperationInfoActionsResponse>(OperationInfoErrors.OperationInfoActionWithIdNotFound);

        return new GetOperationInfoActionsResponse(result.Data, result.RowCount);
    }

    public async Task<Result<GetOperationInfoByNameResponse?>> GetOperationInfoByName(
        GetOperationInfoByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfoByName OperationInfoName:{OperationInfoName}", request.OperationInfoName);

        var isValidRequest = await request.IsValidAsync<GetOperationInfoByNameValidator, GetOperationInfoByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationInfoByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationInfoByNameQuery(request.OperationInfoName, null, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationInfoByNameResponse>(response.Error!);

        var value = response.Value!;
        return new GetOperationInfoByNameResponse(value.Id, value.OperationInfoName, value.OperationInfoCode, value.OperationLatinName, value.UnitOfMeasurementId, value.IsActive);
    }

    public async Task<Result<GetOperationInfoByCodeResponse?>> GetOperationInfoByCode(
        GetOperationInfoByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfoByCode, OperationInfoCode:{OperationInfoCode}", request.OperationInfoCode);

        var isValidRequest = await request.IsValidAsync<GetOperationInfoByCodeValidator, GetOperationInfoByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationInfoByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationInfoByCodeQuery(request.OperationInfoCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationInfoByCodeResponse>(response.Error!);

        var value = response.Value!;
        return new GetOperationInfoByCodeResponse(value.Id, value.OperationInfoName, value.OperationInfoCode, value.OperationLatinName, value.UnitOfMeasurementId, value.IsActive);
    }

    public async Task<Result<GetActiveOperationInfosResponse?>> GetActiveOperationInfos(
        GetActiveOperationInfosRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveOperationInfos, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveOperationInfosValidator, GetActiveOperationInfosRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveOperationInfosResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveOperationInfosQuery(request.FilterData, request.CategoryId, request.BranchId, request.SeasonId,
            request.Priority, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveOperationInfosResponse>(response.Error!);
        if (response.Value is null || response.Value.Data is null)
            return Result.Failure<GetActiveOperationInfosResponse>(OperationInfoErrors.FilteredOperationInfoNotFound);
        var values = response.Value!.Data;

        var measureUnits = await MeasurementDataReceiver(values, ct);
        var data = new List<GetsActiveOperationInfosModel>();
        foreach (var item in values)
        {
            var measure = measureUnits?.Where(x => x.Id == item.UnitOfMeasurementId).FirstOrDefault();
            data.Add(new(item.Id, item.OperationInfoName, item.OperationInfoCode, item.OperationLatinName, item.UnitOfMeasurementId, measure?.Name,
                new(item.UnitOfMeasurementId, measure?.Name), item.Priority));
        }

        return new GetActiveOperationInfosResponse(data ?? new List<GetsActiveOperationInfosModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetOperationInfosResponse?>> GetOperationInfos(
        GetOperationInfosRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfos, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        List<GetOperationInfosModel> operationInfos = new();
        int count = 0;
        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;

        if (request.DependencyId != null && !request.DependencyId.Equals(0))
        {
            var operationInfoDependencies = await _mediator.Send(new FindOperationInfoDependenciesQuery(request.DependencyId.Value), ct);
            var allRelationIds = operationInfoDependencies.Value?.Data?.Select(x => x.RelationId).ToList();
            if (allRelationIds is null)
                return Result.Failure<GetOperationInfosResponse>(OperationInfoErrors.FilteredDependencyNotFound);
            if (allRelationIds is not null && allRelationIds.Count > 0)
            {
                var response = await _mediator.Send(new GetsOperationInfoModelByIdsQuery(allRelationIds, request.OrderBy, request.PageIndex, request.PageSize), ct);
                if (response.IsFailure)
                    return Result.Failure<GetOperationInfosResponse>(response.Error!);
                operationInfos = response.Value!.Data!;
                count = response.Value.RowCount;
            }
        }
        else
        {
            var response = await _mediator.Send(new GetFilteredOperationInfoQuery(null, request.FilterData, request.CategoryId, request.BranchId,
                   request.SeasonId, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
            if (response.IsFailure)
                return Result.Failure<GetOperationInfosResponse>(response.Error!);
            operationInfos = response.Value!.Data!;
            count = response.Value.RowCount;
        }
        await operationInfos!.SetFullName(_mediator, ct);

        var companies = await WebServicesLogic.CompaniesDataReceiver([companyId!.Value], _mediator, ct);
        var company = companies?.FirstOrDefault();

        var baseData = await _mediator.Send(new GetByOperationInfoIdsQuery(operationInfos.Select(x => x.Id).ToList(), 1, count));

        var measureUnits = await MeasurementDataReceiver(operationInfos, ct);
        foreach (var item in operationInfos)
        {
            var measurement = measureUnits?.FirstOrDefault(a => a.Id == item.UnitOfMeasurementId);

            if (!baseData.IsBad() && baseData.Value!.Data != null)
            {
                item.SeasonItems = baseData.Value.Data.Where(c => c.OperationInfo.Id == item.Id).Listed(x => x.Season.SeasonName);
                item.BranchItems = baseData.Value.Data.Where(c => c.OperationInfo.Id == item.Id).Listed(x => x.Season.Branch.BranchName);
                item.CategoryItems = baseData.Value.Data.Where(c => c.OperationInfo.Id == item.Id).Listed(x => x.Season.Branch.Category.CategoryName);
            }

            item.MeasurementName = measurement?.Name;
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetOperationInfosResponse(operationInfos!, count);
    }

    public async Task<Result<GetsPrioritizeOperationInfoResponse?>> GetsPrioritizeOperationInfo(
        GetsPrioritizeOperationInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsPrioritizeOperationInfo, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsPrioritizeOperationInfoValidator, GetsPrioritizeOperationInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsPrioritizeOperationInfoResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsPrioritizeOperationInfoQuery(request.FilterData, request.Id, request.Priority, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsPrioritizeOperationInfoResponse>(response.Error!);
        if (response.Value is null)
            return Result.Failure<GetsPrioritizeOperationInfoResponse>(OperationInfoErrors.FilteredOperationInfoNotFound);
        if (response.Value.Data is null)
            return Result.Failure<GetsPrioritizeOperationInfoResponse>(OperationInfoErrors.FilteredOperationInfoNotFound);
        var operationInfos = response.Value!.Data;

        var getsPrioritizeOperationInfo = operationInfos!.Select(a => a.Adapt<GetsPrioritizeOperationInfoResponseModel>()).ToList();

        return new GetsPrioritizeOperationInfoResponse(getsPrioritizeOperationInfo!, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByMultiFilterResponse?>> GetsByMultiFilter(
        GetsByMultiFilterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByMultiFilter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByMultiFilterValidator, GetsByMultiFilterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByMultiFilterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsByMultiFilterQuery(request.FilterData, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsByMultiFilterResponse>(response.Error!);
        var operationInfos = response.Value!.Data;

        var measureUnits = await MeasurementDataReceiver(operationInfos, ct);

        var getOperationInfos = new List<GetOperationInfosModel>();
        if (operationInfos is not null)
            if (operationInfos?.Count > 0)
                foreach (var item in operationInfos)
                {
                    var measurement = measureUnits?.FirstOrDefault(a => a.Id == item.UnitOfMeasurementId);

                    var dependencyInfo = await _mediator.Send(new GetOperationInfoDependenceQuery(item.Id), ct);
                    OperationInfoDependencyModel? operationInfoDependency = null;
                    if (dependencyInfo.Value is not null)
                    {
                        var dependency = dependencyInfo.Value!;
                        operationInfoDependency = new OperationInfoDependencyModel(dependency.Id, dependency.OperationInfo.Id, dependency.OperationInfo.OperationInfoName, dependency.OperationInfo.OperationInfoCode,
                            dependency.OperationInfo.Priority, dependency.WorkingDays, new((int)dependency.DependencyType, dependency.DependencyType.GetEnumDescription()));
                    }

                    getOperationInfos.Add(new GetOperationInfosModel
                    {
                        Id = item.Id,
                        OperationInfoName = item.OperationInfoName,
                        OperationInfoCode = item.OperationInfoCode,
                        OperationLatinName = item.OperationLatinName,
                        Priority = item.Priority,
                        UnitOfMeasurementId = item.UnitOfMeasurementId,
                        MeasurementName = measurement?.Name,
                        BasePrice = item.OperationInfoActions.Any(x => x.Price != 0)
                            ? item.OperationInfoActions.Sum(x => x.Price) ?? 0
                            : item.BasePrice,
                        OperationInfoDependencyId = operationInfoDependency?.OperationInfoDependencyId,
                        RelationId = operationInfoDependency?.RelationId,
                        DependencyName = operationInfoDependency?.DependencyName,
                        DependencyCode = operationInfoDependency?.DependencyCode,
                        DependencyPriority = operationInfoDependency?.DependencyPriority,
                        WorkingDays = operationInfoDependency?.WorkingDays,
                        DependencyType = operationInfoDependency?.TypeData.Description,
                        IsPriceList = item.IsPriceList,
                        IsActive = item.IsActive
                    });
                }
        return new GetsByMultiFilterResponse(getOperationInfos!, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsOperationInfoHistoryByIdResponse?>> GetsOperationInfoHistoryById(
        GetsOperationInfoHistoryByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoHistoryById, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoHistoryByIdValidator, GetsOperationInfoHistoryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoHistoryByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsOperationInfoHistoryByIdQuery(request.OperationInfoId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationInfoHistoryByIdResponse>(response.Error!);
        var values = response.Value!.Data!;

        var measureUnitIds = values.Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);

        values.ForEach(value =>
            value.MeasurementName = measurments?.FirstOrDefault(x => x.Id == value.UnitOfMeasurementId)?.Name
            );

        return new GetsOperationInfoHistoryByIdResponse(values!, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsBySeasonIdResponse?>> GetsBySeasonId(
        GetsBySeasonIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsBySeasonId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsBySeasonIdValidator, GetsBySeasonIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsBySeasonIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsBySeasonIdQuery(request.SeasonId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsBySeasonIdResponse>(response.Error!);
        if (response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsBySeasonIdResponse>(OperationInfoErrors.FilteredOperationInfoNotFound);
        var values = response.Value!.Data;

        var measureUnits = await MeasurementDataReceiver(values, ct);
        var data = new List<GetsBySeasonIdModel>();
        foreach (var item in values)
        {
            var measure = measureUnits?.Where(x => x.Id == item.UnitOfMeasurementId).FirstOrDefault();
            data.Add(new(item.Id, item.OperationInfoName, item.OperationInfoCode, item.OperationLatinName, item.UnitOfMeasurementId, measure?.Name,
                new(item.UnitOfMeasurementId, measure?.Name), item.IsActive));
        }

        return new GetsBySeasonIdResponse(data!, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetOperationInfoContractorsResponse?>> GetOperationInfoContractors(
        GetOperationInfoContractorsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfoContractors");

        var isValidRequest = await request.IsValidAsync<GetOperationInfoContractorsValidator, GetOperationInfoContractorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationInfoContractorsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationInfoContractorsQuery(), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetOperationInfoContractorsResponse>(response.Error!);
        var ids = response.Value!.Data;

        List<UserModel>? contractors = [];
        var data = new List<GetOperationInfoContractorsResponseModel>();
        if (ids?.Count > 0)
        {
            var responseValue = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(ids!, request.FilterData, null, _mediator, ct);
            if (responseValue is not null && responseValue.Count > 0)
                foreach (var item in responseValue)
                    if (item != null)
                        contractors.Add(item);

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var id in ids)
                {
                    if (!contractors.Any(x => x?.Id == id))
                        continue;

                    var contractor = contractors.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetOperationInfoContractorsResponseModel()
                    {
                        Id = contractor?.Id,
                        UserId = contractor?.UserId,
                        FullName = contractor?.FullName,
                        NickName = contractor?.Nickname
                    });
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    if (!contractors.Any(x => x?.Id == id))
                        continue;

                    var contractor = contractors.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetOperationInfoContractorsResponseModel()
                    {
                        Id = contractor?.Id,
                        UserId = contractor?.UserId,
                        FullName = contractor?.FullName,
                        NickName = contractor?.Nickname
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetOperationInfoContractorsResponse(responseData ?? new List<GetOperationInfoContractorsResponseModel>(0), contractors?.Count ?? 0);
    }

    public async Task<Result<GetsOperationInfoByContractorIdsResponse?>> GetsOperationInfoByContractorIds(
        GetsOperationInfoByContractorIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoByContractorIds, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoByContractorIdsValidator, GetsOperationInfoByContractorIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoByContractorIdsResponse>(isValidRequest.Error!);

        var contractorIds = request.ContractorIds.Distinct().ToList();
        var response = await _mediator.Send(new GetsOperationInfoByContractorIdsQuery(contractorIds, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationInfoByContractorIdsResponse>(response.Error!);
        var operationInfos = response.Value!.Data;

        var measureUnits = await MeasurementDataReceiver(operationInfos, ct);

        List<UserModel>? contractors = [];
        if (contractorIds is not null && contractorIds.Count > 0)
        {
            var responseValue = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds!, null, null, _mediator, ct);
            if (responseValue is not null && responseValue.Count > 0)
                foreach (var item in responseValue)
                    if (item != null)
                        contractors.Add(item);
        }

        var data = new List<GetsOperationInfoByContractorIdsResponseModel>();
        if (contractors is not null && contractors.Count > 0)
        {
            foreach (var contractor in contractors)
            {
                var operationInfoData = operationInfos!.Where(x => x.OperationInfoServices is not null && x.OperationInfoServices.Count > 0)
                                                .SelectMany(x => x.OperationInfoServices)
                                                .Where(c => c.ProjectOperationDetailContractorServices is not null && c.ProjectOperationDetailContractorServices.Count > 0)
                                                .SelectMany(c => c.ProjectOperationDetailContractorServices)
                                                .Where(c => c.ContractorId != null && c.ContractorId > 0 && c.ContractorId == contractor.Id)
                                                .Select(x => x.OperationInfoService.OperationInfo).ToList();

                var operationInfoResponseData = new List<OperationInfoResponseModel>();
                if (operationInfoData is not null && operationInfoData.Count > 0)
                    foreach (var operation in operationInfoData)
                    {
                        if (operationInfoResponseData.Any(x => x.Id == operation.Id))
                            continue;

                        var measurement = measureUnits?.FirstOrDefault(a => a.Id == operation.UnitOfMeasurementId);
                        operationInfoResponseData.Add(new OperationInfoResponseModel()
                        {
                            Id = operation.Id,
                            IsActive = operation.IsActive,
                            IsPriceList = operation.IsPriceList,
                            MeasurementId = operation.UnitOfMeasurementId,
                            MeasurementName = measurement?.Name,
                            OperationInfoCode = operation.OperationInfoCode,
                            OperationInfoName = operation.OperationInfoName,
                            OperationLatinName = operation.OperationLatinName
                        });
                    }

                data.Add(new GetsOperationInfoByContractorIdsResponseModel()
                {
                    Id = contractor.Id,
                    FullName = contractor.FullName,
                    NickName = contractor.Nickname,
                    UserId = contractor.UserId,
                    operationInfos = operationInfoResponseData
                });
            }
        }
        return new GetsOperationInfoByContractorIdsResponse(data ?? new List<GetsOperationInfoByContractorIdsResponseModel>(0), data!.Count);
    }

    public async Task<Result<GetsOperationInfoExcelExporterResponse?>> GetsOperationInfoExcelExporter(
        GetsOperationInfoExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetFilteredOperationInfoQuery(request.Ids, request.FilterData, request.CategoryId, request.BranchId,
               request.SeasonId, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsOperationInfoExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companies = await WebServicesLogic.CompaniesDataReceiver([companyId!.Value], _mediator, ct);
        var company = companies?.FirstOrDefault();

        var measureUnits = await MeasurementDataReceiver(values, ct);

        if (values is not null && values?.Count > 0)
            foreach (var item in values)
            {
                var dependencyInfo = await _mediator.Send(new GetOperationInfoDependenceQuery(item.Id), ct);
                OperationInfoDependencyModel? operationInfoDependency = null;
                if (dependencyInfo.Value is not null)
                {
                    var dependency = dependencyInfo.Value!;
                    operationInfoDependency = new OperationInfoDependencyModel(dependency.Id, dependency.OperationInfo.Id, dependency.OperationInfo.OperationInfoName, dependency.OperationInfo.OperationInfoCode,
                        dependency.OperationInfo.Priority, dependency.WorkingDays, new((int)dependency.DependencyType, dependency.DependencyType.GetEnumDescription()));
                }
                var measurement = measureUnits?.FirstOrDefault(a => a.Id == item.UnitOfMeasurementId);

                item.MeasurementName = measurement?.Name;
                item.OperationInfoDependencyId = operationInfoDependency?.OperationInfoDependencyId;
                item.RelationId = operationInfoDependency?.RelationId;
                item.DependencyName = operationInfoDependency?.DependencyName;
                item.DependencyCode = operationInfoDependency?.DependencyCode;
                item.DependencyPriority = operationInfoDependency?.DependencyPriority;
                item.WorkingDays = operationInfoDependency?.WorkingDays;
                item.DependencyType = operationInfoDependency?.TypeData.Description;
                item.CompanyNameFa = company?.NameFa;
            }

        var data = values.Adapt<List<GetsOperationInfoExcelExporterModel>>();
        var file = new FileContentResult(OperationInfoExcels.OperationInfoToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"OperationInfos-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsOperationInfoExcelExporterResponse(file);
    }

    public async Task<Result<GetsOperationInfoExcelEnumResponse?>> GetsOperationInfoExcelEnum(
        GetsOperationInfoExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<OperationInfoExcelEnum>());
        return new GetsOperationInfoExcelEnumResponse(response);
    }

    public async Task<Result<GetActiveOperationInfoBySeasonIdsResponse?>> GetActiveOperationInfoBySeasonIds(
        GetActiveOperationInfoBySeasonIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveOperationInfoBySeasonIds");


        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveOperationInfoBySeasonIdsQuery(
            request.CategoryIds,
            request.BranchIds,
            request.SeasonIds,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);

        if (response.IsBad())
            return Result.Failure<GetActiveOperationInfoBySeasonIdsResponse>(response.Error!);

        var values = response.Value!;

        var measureUnits = await MeasurementDataReceiver(values, ct);
        var data = new List<GetsActiveOperationInfosModel>();
        foreach (var item in values)
        {
            var measure = measureUnits?.Where(x => x.Id == item.UnitOfMeasurementId).FirstOrDefault();
            data.Add(new(item.Id, item.OperationInfoName, item.OperationInfoCode, item.OperationLatinName, item.UnitOfMeasurementId, measure?.Name,
                new(item.UnitOfMeasurementId, measure?.Name), item.Priority));
        }

        return new GetActiveOperationInfoBySeasonIdsResponse(data, data.Count);
    }

    public async Task<Result<DeleteOperationInfoActionResponse?>> DeleteOperationInfoAction(
        DeleteOperationInfoActionRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveOperationInfoBySeasonIds");

        var result = await DeleteOperationInfoActionHandler(request, ct);
        if (result.IsBad())
            return result.Failure<DeleteOperationInfoActionResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteOperationInfoActionResponse(result.Value!.Id, true);
    }

}
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Abstractions.Data.OperationLocations;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Abstractions.Data.Projects;
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
using Engineering.Application.Services.ConsumableVolumes.Commands.UpdateConsumableVolumes;
using Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertConsumableVolumeById;
using Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetConsumableVolumeMachineryById;
using Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductConsumableVolumeById;
using Engineering.Application.Services.Machineries.Queries.GetMachineryById;
using Engineering.Application.Services.Machineries.Queries.GetsMachineryByIds;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoWithProjectOperationId;
using Engineering.Application.Services.OperationInfos.Queries.HaveOperationInfoChild;
using Engineering.Application.Services.OperationInfoServices.Queries.GetOperationInfoServiceForValidation;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationById;
using Engineering.Application.Services.OperationLocations.Queries.GetsByIds;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.CreateBatchPODContractorExpert;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.UpdateBatchPODContractorExpert;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.CreateContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.DisableContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetById;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.CreateProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.DeleteProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.UpdateProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetProjectOperationDetailDeductionById;
using Engineering.Application.Services.ProjectOperationDetails.Commands.CreateProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Commands.CreateVizardProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Commands.DeleteProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Commands.ProjectOperationDetailCodeCreator;
using Engineering.Application.Services.ProjectOperationDetails.Commands.ProjectOperationDetailStatusChanger;
using Engineering.Application.Services.ProjectOperationDetails.Commands.SetProjectOperationDetailPriority;
using Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Commands.UpdatesProjectOperationDetailDate;
using Engineering.Application.Services.ProjectOperationDetails.Models.CreateProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.CreateVizardProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Services;
using Engineering.Application.Services.ProjectOperationDetails.Models.DeleteProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetFilteredProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetHistoryByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectContractors;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailByCode;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailById;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailContractors;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailDoneVolume;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelEnums;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsConsumableVolumes;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorDetailReportsExcelEnum;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorDetailReportsExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorProjectOperationDetailDetailReports;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorProjectOperationDetailReports;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorReportsExcelEnum;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorReportsExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsMinimalByProjectOperationIds;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByContractorIds;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByExpertId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByIds;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByMachineryId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByProductId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByProjectOperationId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailDocument;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailForScheduling;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReporting;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReportingExcelEnum;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReportingExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailStatus;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsSummarizedByProjectOperationIds;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetTotalsByProjectOperationId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetTotalsProjectOperationDetailReport;
using Engineering.Application.Services.ProjectOperationDetails.Models.GroupProjectOperationDetailStatusChanger;
using Engineering.Application.Services.ProjectOperationDetails.Models.PODetailExcelImport;
using Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailCodeCreator;
using Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailGroupDelete;
using Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;
using Engineering.Application.Services.ProjectOperationDetails.Models.SetProjectOperationDetailPriority;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetailVolumes;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdatesProjectOperationDetailDate;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetFilteredProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetHistoryByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectContractors;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByCode;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailById;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessInclude;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessIncludes;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailContractors;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailDoneVolume;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailForValidates;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithVolumes;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsContractorProjectOperationDetailDetailReports;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsContractorProjectOperationDetailReports;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsForVolumesByIds;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsForVolumesByProjectOperationId;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsMinimalByProjectOperationIds;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByContractorIds;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByExpertId;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByIds;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByIdsWithPage;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByMachineryId;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByProductId;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByProjectOperationId;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailDocument;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailForScheduling;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailReporting;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsSummarizedByProjectOperationIds;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetTotalsByProjectOperationId;
using Engineering.Application.Services.ProjectOperations.Commands.ProjectOperationWorkloder;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdLessInclude;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdOperationInfoInclude;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdsLessInclude;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceDetailByIds;
using Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillById;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetsSkillById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetsWarehouseCategoryById;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetWarehouseCategoryById;
using Engineering.Domain.Entities.OperationInfos.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using TimeCalculator = Engineering.Application.Extensions.TimeCalculator.TimeCalculator;

namespace Engineering.Application.Services.ProjectOperationDetails;

public partial class ProjectOperationDetailLogic : IProjectOperationDetailLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectOperationDetailLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserProfileService _userProfileService;
    private readonly IMeasureUnitRepository _measureUnitRepo;
    private readonly IProjectOperationRepository _projectOperationRepository;
    private readonly IProjectOperationDetailRepository _projectOperationDetailRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IOperationInfoRepository _operationInfoRepository;
    private readonly IViewProductRepository _pRepo;
    private readonly IOperationLocationRepository _operationLocationRepository;
    private readonly IProjectOperationDetailContractorExpertRepository _detailRepo;

    public ProjectOperationDetailLogic(
        IMediator mediator,
        ILogger<ProjectOperationDetailLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IUserProfileService userProfileService,
        IMeasureUnitRepository measureUnitRepo,
        IProjectOperationRepository projectOperationRepository,
        IProjectOperationDetailRepository projectOperationDetailRepository,
        IProjectRepository projectRepository,
        IOperationInfoRepository operationInfoRepository,
        IViewProductRepository pRepo,
        IOperationLocationRepository operationLocationRepository,
        IProjectOperationDetailContractorExpertRepository detailRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _userProfileService = userProfileService;
        _pRepo = pRepo;
        _measureUnitRepo = measureUnitRepo;
        _projectOperationRepository = projectOperationRepository;
        _projectOperationDetailRepository = projectOperationDetailRepository;
        _projectRepository = projectRepository;
        _operationInfoRepository = operationInfoRepository;
        _operationLocationRepository = operationLocationRepository;
        _detailRepo = detailRepo;
    }

    public async Task<Result<ProjectOperationDetailCodeCreatorResponse?>> ProjectOperationDetailCodeCreator(
        ProjectOperationDetailCodeCreatorRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<ProjectOperationDetailCodeCreatorValidator, ProjectOperationDetailCodeCreatorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationDetailCodeCreatorResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<ProjectOperationDetailCodeCreatorResponse>(GlobalErrors.InvalidCompany);

        var response = await _mediator.Send(new ProjectOperationDetailCodeCreatorCommand(request.ProjectOperationId, request.OperationLocationId, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<ProjectOperationDetailCodeCreatorResponse>(response.Error!);

        return new ProjectOperationDetailCodeCreatorResponse(response.Value);
    }

    public async Task<Result<CreateProjectOperationDetailResponse?>> CreateProjectOperationDetail(
        CreateProjectOperationDetailRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectOperationDetail, ProjectOperationId:{ProjectOperationId}, OperationLocationId:{OperationLocationId},", request.ProjectOperationId, request.OperationLocationId);

        var isValidRequest = await request.IsValidAsync<CreateProjectOperationDetailValidator, CreateProjectOperationDetailRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateProjectOperationDetailResponse>(isValidRequest.Error!);

        var finalAmount = Calculator.FinalAmount(request.Length, request.Width, request.Height, request.Weight, request.Number);

        var projectOperationResponse = await _mediator.Send(new GetProjectOperationByIdLessIncludeQuery(request.ProjectOperationId), ct);
        if (projectOperationResponse.IsFailure || projectOperationResponse.Value is null)
            return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        var projectOperation = projectOperationResponse.Value;

        var responseGetLocation = await _mediator.Send(new GetOperationLocationByIdQuery(request.OperationLocationId), ct);
        if (responseGetLocation.IsFailure || responseGetLocation.Value is null)
            return Result.Failure<CreateProjectOperationDetailResponse>(OperationLocationErrors.OperationLocationWithIdNotFound);
        var operationLocation = responseGetLocation.Value!;

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateProjectOperationDetailResponse>(GlobalErrors.InvalidCompany);

        var newCode = request.Code;
        if (string.IsNullOrEmpty(newCode))
        {
            var codeResponse = await _mediator.Send(new ProjectOperationDetailCodeCreatorCommand(
                projectOperation.Id, operationLocation!.Id, companyId), ct);
            if (codeResponse.IsFailure)
                return Result.Failure<CreateProjectOperationDetailResponse>(codeResponse.Error!);
            newCode = codeResponse.Value ?? "";
        }
        var isDuplicate = await _mediator.Send(new GetProjectOperationDetailForValidatesQuery(
            request.ProjectOperationId, request.OperationLocationId, newCode, companyId), ct);
        if (isDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProjectOperationDetailIsDuplicate);

        var plannersValidate = await GetUserDataAsync(request.PlannerRequests, ProjectOperationDetailErrors.UnValidPlanners, ct);
        if (plannersValidate.IsFailure)
            return Result.Failure<CreateProjectOperationDetailResponse>(plannersValidate.Error!);

        var implementationsValidate = await GetUserDataAsync(request.ImplementationAssistantRequests, ProjectOperationDetailErrors.UnValidImplementations, ct);
        if (implementationsValidate.IsFailure)
            return Result.Failure<CreateProjectOperationDetailResponse>(implementationsValidate.Error!);

        var technicalsValidate = await GetUserDataAsync(request.TechnicalAssistantRequests, ProjectOperationDetailErrors.UnValidTechnicals, ct);
        if (technicalsValidate.IsFailure)
            return Result.Failure<CreateProjectOperationDetailResponse>(technicalsValidate.Error!);

        var contractorServiceRequests = new List<ContractorServiceModel>();

        if (request.ContractorServiceRequests is not null &&
            request.ContractorServiceRequests.Count > 0)
        {
            var contractorsData = new List<UserModel>();
            var contractorIds = new List<long>();

            foreach (var item in request.ContractorServiceRequests)
            {
                if (item.ContractorId is not null && item.ContractorId != default)
                    contractorIds.Add(item.ContractorId.Value);
            }

            if (contractorIds.Count > 0)
            {
                var contractorsValue = await _mediator.Send(
                    new GetWithSkillOnlyByIdsQuery(
                        1,
                        contractorIds.Count,
                        contractorIds,
                        null,
                        false,
                        null),
                    ct);

                if (contractorsValue.IsFailure)
                    return Result.Failure<CreateProjectOperationDetailResponse>(
                        ProjectOperationDetailErrors.UnValidContractors);

                contractorsData = contractorsValue.Value?.Data ?? [];
            }

            var projectServiceDetails = new List<ProjectServiceDetail>();

            var projectServiceIds = request.ContractorServiceRequests
                .Where(x =>
                    x.ProjectServiceDetailId is not null &&
                    x.ProjectServiceDetailId > 0)
                .Select(x => x.ProjectServiceDetailId!.Value)
                .Distinct()
                .ToList();

            if (projectServiceIds.Any())
            {
                var getProjectServices = await _mediator.Send(
                    new GetsProjectServiceDetailByIdsQuery(
                        projectServiceIds,
                        1,
                        projectServiceIds.Count),
                    ct);

                if (getProjectServices.IsFailure)
                    return Result.Failure<CreateProjectOperationDetailResponse>(
                        getProjectServices.Error!);

                var projectServiceValues = getProjectServices.Value!.Data!;

                if (projectServiceIds.Count > projectServiceValues.Count)
                    return Result.Failure<CreateProjectOperationDetailResponse>(
                        ProjectOperationDetailErrors.ProjectServicesNotValidate);

                projectServiceDetails = projectServiceValues;
            }

            var serviceIds = request.ContractorServiceRequests
                .Select(x => x.ServiceInfoId)
                .Where(x => x != default)
                .Distinct()
                .ToList();

            var serviceInfosData = await _mediator.Send(
                new GetsServiceInfoByIdsQuery(
                    1,
                    serviceIds.Count,
                    serviceIds),
                ct);

            if (serviceInfosData.IsFailure)
                return Result.Failure<CreateProjectOperationDetailResponse>(
                    ProjectOperationDetailErrors.UnValidServiceInfos);

            foreach (var item in request.ContractorServiceRequests)
            {
                var serviceInfoData = projectOperation
                    .OperationInfo
                    .OperationInfoServices
                    .FirstOrDefault(x =>
                        x.ServiceInfo.Id == item.ServiceInfoId &&
                        !x.IsDeleted);

                if (serviceInfoData is null)
                    return Result.Failure<CreateProjectOperationDetailResponse>(
                        ProjectOperationDetailErrors.UnValidServiceInfoInOperationInfo);

                if (!string.IsNullOrEmpty(item.TimeSpant))
                {
                    var timeParts = item.TimeSpant.Split(':');

                    if (!(timeParts[0].Length >= 2 &&
                          timeParts[1].Length == 2))
                    {
                        return Result.Failure<CreateProjectOperationDetailResponse>(
                            MachineryStandardErrors.TimeSpantCountError);
                    }

                    if (int.Parse(timeParts[1]) > 59)
                    {
                        return Result.Failure<CreateProjectOperationDetailResponse>(
                            MachineryStandardErrors.MoreThan59Min);
                    }
                }

                var projectServiceDetail = projectServiceDetails
                    .FirstOrDefault(x => x.Id == item.ProjectServiceDetailId);

                // Backward compatibility:
                // اگر Front قدیمی Type نفرستد => ServiceBased
                var type = item.Type ?? PODContractorServiceType.ServiceBased;

                var contractorData = item.ContractorId.HasValue
                    ? contractorsData.FirstOrDefault(x => x.Id == item.ContractorId.Value)
                    : null;

                if (contractorData is not null)
                {
                    if (projectServiceDetail is not null)
                    {
                        contractorServiceRequests.Add(
                            new ContractorServiceModel(
                                item.TempId,
                                contractorData.Id,
                                contractorData.FullName,
                                projectServiceDetail.OperationInfoService!,
                                projectServiceDetail,
                                item.Volume,
                                TimeCalculator.StringToTicks(item.TimeSpant),
                                item.IsActive,
                                type));
                    }
                    else
                    {
                        contractorServiceRequests.Add(
                            new ContractorServiceModel(
                                item.TempId,
                                contractorData.Id,
                                contractorData.FullName,
                                serviceInfoData,
                                null,
                                item.Volume,
                                TimeCalculator.StringToTicks(item.TimeSpant),
                                item.IsActive,
                                type));
                    }
                }
                else
                {
                    if (projectServiceDetail is not null)
                    {
                        contractorServiceRequests.Add(
                            new ContractorServiceModel(
                                item.TempId,
                                null,
                                null,
                                projectServiceDetail.OperationInfoService!,
                                projectServiceDetail,
                                item.Volume,
                                TimeCalculator.StringToTicks(item.TimeSpant),
                                item.IsActive,
                                type));
                    }
                    else
                    {
                        contractorServiceRequests.Add(
                            new ContractorServiceModel(
                                item.TempId,
                                null,
                                null,
                                serviceInfoData,
                                null,
                                item.Volume,
                                TimeCalculator.StringToTicks(item.TimeSpant),
                                item.IsActive,
                                type));
                    }
                }
            }
        }
        //Validate Experts
        var expertRequests = new List<ExpertServiceModel>();
        if (request.ExpertRequests is not null && request.ExpertRequests?.Count > 0)
        {
            var expertIds = request.ExpertRequests.Select(x => x.ExpertId).ToList();
            if (expertIds.Count != expertIds.Distinct().Count())
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExistsExpertInList);

            var expertsData = await _mediator.Send(new GetsSkillByIdQuery(expertIds, true, 1, expertIds.Count), ct); // بره سراغ متا دیتا
            if (expertsData.IsFailure)
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidExperts);
            var experts = ExpertModeling(request.ExpertRequests!, projectOperation, expertsData.Value?.Data!);
            if (!experts.Item1)
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExpertStandardValueNotValid);
            expertRequests.AddRange(experts.Item2);
        }

        //Validate Machineries
        var machineryRequests = new List<MachineryServiceModel>();
        if (request.MachineryRequests is not null && request.MachineryRequests?.Count > 0)
        {
            var machineryIds = request.MachineryRequests.Select(x => x.MachineryId).ToList();
            if (machineryIds.Count != machineryIds.Distinct().Count())
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExistsExpertInList);

            var machineriesData = await _mediator.Send(new GetsMachineryByIdsQuery(machineryIds, 1, machineryIds.Count), ct); // بره سراغ متا دیتا
            if (machineriesData.IsFailure)
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidMachineries);
            var machineries = MachineryModeling(request.MachineryRequests!, projectOperation, machineriesData.Value?.Data);
            if (!machineries.Item1)
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.MachineryStandardValueNotValid);
            machineryRequests.AddRange(machineries.Item2);
        }

        //Validate Products
        var productRequests = new List<ProductServiceModel>();
        if (request.ProductRequests is not null && request.ProductRequests?.Count > 0)
        {
            List<Group>? groups = [];
            if (request.ProductRequests.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
            {
                var productIds = new List<long>();
                foreach (var item in request.ProductRequests.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                {
                    if (productIds.Any(x => x == item.ProductGroupId))
                        return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExistsProductInList);
                    else
                        productIds.Add(item.ProductGroupId);
                }
                var productsData = await _mediator.Send(new GetGroupsByIdsQuery(productIds, 1, productIds.Count), ct); // بره سراغ متا دیتا
                if (productsData.IsFailure || productsData.Value?.Data is null)
                    return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidProducts);
                groups = productsData.Value?.Data!;
            }

            List<WarehouseCategory>? categories = [];
            if (request.ProductRequests.Any(x => x.VolumeProductType == VolumeProductType.Category))
            {
                var categoryIds = new List<long>();
                foreach (var item in request.ProductRequests.Where(x => x.VolumeProductType == VolumeProductType.Category))
                {
                    if (categoryIds.Any(x => x == item.ProductGroupId))
                        return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExistsCategoryInList);
                    else
                        categoryIds.Add(item.ProductGroupId);
                }
                //var productIds = request.ProductRequests.Select(c => c!.ProductGroupId).Where(x => x != default).ToList();
                var ids = categoryIds.Adapt<List<long?>>();
                var categoriesData = await _mediator.Send(new GetsWarehouseCategoryByIdQuery(1, ids.Count, ids, false, null), ct); // بره سراغ متا دیتا
                if (categoriesData.IsFailure || categoriesData.Value?.Data is null)
                    return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidCategories);
                categories = categoriesData.Value?.Data!;
            }

            var products = ProductModeling(request.ProductRequests!, projectOperation, groups, categories);
            if (!products.Item1)
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProductStandardValueNotValid);
            productRequests.AddRange(products.Item2);
        }

        //Deductions
        var deductionRequests = new List<DeductionServiceModel>();
        if (request.DeductionRequests is not null && request.DeductionRequests?.Count > 0)
        {
            foreach (var item in request.DeductionRequests)
            {
                var deduction = new DeductionServiceModel()
                {
                    Height = item.Height,
                    Length = item.Length,
                    Number = item.Number,
                    Weight = item.Weight,
                    Width = item.Width,
                };
                deductionRequests.Add(deduction);
            }

            var sumFinalAmounts = deductionRequests.Sum(x => x.FinalAmount);
            if (sumFinalAmounts >= finalAmount)
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.DeductionsUnValid);
        }

        if (request.CreatedProductId is not null)
        {
            if (!projectOperation.GoodsInProgress)
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.CanNotCreateGoods);

            var productData = await WebServicesLogic.ProductDataReceiver(request.CreatedProductId, _mediator, _pRepo, ct);
            if (productData is null)
                return Result.Failure<CreateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidProducts);
        }

        var response = await _mediator.Send(new CreateProjectOperationDetailCommand(
            newCode,
            projectOperation!,
            operationLocation!,
            request.StartDate,
            request.EndDate,
            request.Length,
            request.LengthChangeable,
            request.Width,
            request.WidthChangeable,
            request.Height,
            request.HeightChangeable,
            request.Weight,
            request.WeightChangeable,
            request.Number,
            request.NumberChangeable,
            request.Status,
            request.Priority,
            request.Day,
            request.Hour,
            request.CreatedProductId,
            request.Description,
            request.PlannerRequests,
            request.ImplementationAssistantRequests,
            request.TechnicalAssistantRequests,
            contractorServiceRequests,
            expertRequests,
            request.ContractorExpertLinks,
            machineryRequests,
            productRequests,
            deductionRequests,
            request.Urls,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateProjectOperationDetailResponse>(response.Error!);

        if (!projectOperation.Project.Contractual)
            if (!Workloader(projectOperation))
                await SendNotification(projectOperation, operationLocation, request.Description, ct);

        if (request!.Status is not null && request.Status != ProjectOperationDetailStatus.NotStarted)
            projectOperation.SetProjectOperationStatus((ProjectOperationStatus)request.Status);

        var itemListed = new ProjectOperationDetailList(response.Value!.Id, response.Value!.FinalAmount, true);
        var workloder = await _mediator.Send(new ProjectOperationWorkloderCommand(projectOperation!.Id, [itemListed]), ct);
        if (workloder.IsFailure)
            return Result.Failure<CreateProjectOperationDetailResponse>(workloder.Error!);
        projectOperation = workloder.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectOperationDetailResponse(response.Value!.Id, projectOperation.Workload, projectOperation.Id, projectOperation.ProjectOperationStatus, projectOperation.ProjectOperationStatus.GetEnumDescription());
    }

    public async Task<Result<CreateVizardProjectOperationDetailResponse?>> CreateVizardProjectOperationDetail(
        CreateVizardProjectOperationDetailRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateVizardProjectOperationDetail, OperationLocationIds:{OperationLocationIds},", request.OperationLocationIds);

        var isValidRequest = await request.IsValidAsync<CreateVizardProjectOperationDetailValidator, CreateVizardProjectOperationDetailRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateVizardProjectOperationDetailResponse>(isValidRequest.Error!);

        var operationLocationData = await _mediator.Send(new GetsOperationLocationByIdsQuery(request.OperationLocationIds), ct);
        if (operationLocationData.IsFailure)
            return Result.Failure<CreateVizardProjectOperationDetailResponse>(OperationLocationErrors.OperationLocationWithIdNotFound);
        if (operationLocationData.Value is null || operationLocationData.Value.Data is null)
            return Result.Failure<CreateVizardProjectOperationDetailResponse>(OperationLocationErrors.OperationLocationWithIdNotFound);
        if (operationLocationData.Value.Data.Count != request.OperationLocationIds.Count)
            return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.OperationLocationIsNotEqual);
        var operationLocations = operationLocationData.Value.Data!;
        if (operationLocationData.Value.Data!.Any(x => x.CostCenter.Id != operationLocations.FirstOrDefault()?.CostCenter.Id))
            return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.OperationLocationIsUnValid);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateVizardProjectOperationDetailResponse>(GlobalErrors.InvalidCompany);

        var values = new List<ProjectOperationDetail>();
        var validated = new List<ValidateModel>();
        foreach (var location in operationLocations)
        {
            var projectOperationIds = new List<long>();
            foreach (var item in request.RequestModel)
                projectOperationIds.AddRange(item.ProjectOperationIds);
            var projectOperationData = await _mediator.Send(new GetProjectOperationByIdsLessIncludeQuery(projectOperationIds), ct);
            if (projectOperationData.IsFailure)
                return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
            if (projectOperationData.Value is null || projectOperationData.Value.Data is null)
                return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
            if (projectOperationData.Value.Data.Count != projectOperationIds.Count)
                return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProjectOperationIsNotEqual);
            var projectOperations = projectOperationData.Value.Data!;
            if (projectOperationData.Value.Data!.Any(x => x.Project.Id != projectOperations.FirstOrDefault()?.Project.Id))
                return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProjectOperationIsUnValid);

            var plannerIds = new List<long>();
            foreach (var item in request.RequestModel)
                if (item.PlannerRequests is not null && item.PlannerRequests.Count > 0)
                    plannerIds.AddRange(item.PlannerRequests);
            if (plannerIds is not null && plannerIds.Count > 0)
            {
                var ids = plannerIds.Where(x => x != default).ToList();
                var plannerDatas = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct); // بره سراغ متا دیتا
                if (plannerDatas.IsFailure)
                    return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidPlanners);
            }

            var implementationIds = new List<long>();
            foreach (var item in request.RequestModel)
                if (item.ImplementationAssistantRequests is not null && item.ImplementationAssistantRequests.Count > 0)
                    implementationIds.AddRange(item.ImplementationAssistantRequests);
            if (implementationIds is not null && implementationIds.Count > 0)
            {
                var ids = implementationIds.Where(x => x != default).ToList();
                var implementationDatas = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct); // بره سراغ متا دیتا
                if (implementationDatas.IsFailure)
                    return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidPlanners);
            }

            var technicalIds = new List<long>();
            foreach (var item in request.RequestModel)
                if (item.TechnicalAssistantRequests is not null && item.TechnicalAssistantRequests.Count > 0)
                    technicalIds.AddRange(item.TechnicalAssistantRequests);
            if (technicalIds is not null && technicalIds.Count > 0)
            {
                var ids = technicalIds.Where(x => x != default).ToList();
                var technicalDatas = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct); // بره سراغ متا دیتا
                if (technicalDatas.IsFailure)
                    return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidPlanners);
            }

            foreach (var item in request.RequestModel)
            {
                var finalAmount = Calculator.FinalAmount(item.Length, item.Width, item.Height, item.Weight, item.Number);

                //Validate Experts
                var expertRequests = new List<ExpertServiceModel>();
                if (item.ExpertRequests is not null && item.ExpertRequests?.Count > 0)
                {
                    var expertIds = new List<long>();
                    foreach (var expert in item.ExpertRequests)
                    {
                        if (expertIds.Any(x => x == expert.ExpertId))
                            return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExistsExpertInList);
                        else
                            expertIds.Add(expert.ExpertId);
                    }
                    var expertsData = await _mediator.Send(new GetsSkillByIdQuery(expertIds, true, 1, expertIds.Count), ct); // بره سراغ متا دیتا
                    if (expertsData.IsFailure)
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidExperts);
                    var experts = ExpertModeling(item.ExpertRequests!, projectOperations.FirstOrDefault()!, expertsData.Value?.Data!);
                    if (!experts.Item1)
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExpertStandardValueNotValid);
                    expertRequests.AddRange(experts.Item2);
                }

                //Validate Machineries
                var machineryRequests = new List<MachineryServiceModel>();
                if (item.MachineryRequests is not null && item.MachineryRequests?.Count > 0)
                {
                    var machineryIds = new List<long>();
                    foreach (var machinery in item.MachineryRequests)
                    {
                        if (machineryIds.Any(x => x == machinery.MachineryId))
                            return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExistsMachineryInList);
                        else
                            machineryIds.Add(machinery.MachineryId);
                    }
                    var machineriesData = await _mediator.Send(new GetsMachineryByIdsQuery(machineryIds, 1, machineryIds.Count), ct); // بره سراغ متا دیتا
                    if (machineriesData.IsFailure)
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidMachineries);
                    var machineries = MachineryModeling(item.MachineryRequests!, projectOperations.FirstOrDefault()!, machineriesData.Value?.Data);
                    if (!machineries.Item1)
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.MachineryStandardValueNotValid);
                    machineryRequests.AddRange(machineries.Item2);
                }

                //Validate Products
                var productRequests = new List<ProductServiceModel>();
                if (item.ProductRequests is not null && item.ProductRequests?.Count > 0)
                {
                    List<Group>? groups = [];
                    if (item.ProductRequests.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                    {
                        var productIds = new List<long>();
                        foreach (var item1 in item.ProductRequests.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                        {
                            if (productIds.Any(x => x == item1.ProductGroupId))
                                return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExistsProductInList);
                            else
                                productIds.Add(item1.ProductGroupId);
                        }

                        var productsData = await _mediator.Send(new GetGroupsByIdsQuery(productIds, 1, productIds.Count), ct); // بره سراغ متا دیتا
                        if (productsData.IsFailure || productsData.Value?.Data is null)
                            return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidProducts);
                        groups = productsData.Value?.Data!;
                    }

                    List<WarehouseCategory>? categories = [];
                    if (item.ProductRequests.Any(x => x.VolumeProductType == VolumeProductType.Category))
                    {
                        var categoryIds = new List<long>();
                        foreach (var item1 in item.ProductRequests.Where(x => x.VolumeProductType == VolumeProductType.Category))
                        {
                            if (categoryIds.Any(x => x == item1.ProductGroupId))
                                return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ExistsCategoryInList);
                            else
                                categoryIds.Add(item1.ProductGroupId);
                        }

                        var ids = categoryIds.Adapt<List<long?>>();
                        var categoriesData = await _mediator.Send(new GetsWarehouseCategoryByIdQuery(1, ids.Count, ids, false, null), ct); // بره سراغ متا دیتا
                        if (categoriesData.IsFailure || categoriesData.Value?.Data is null)
                            return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidCategories);
                        categories = categoriesData.Value?.Data!;
                    }

                    var products = ProductModeling(item.ProductRequests!, projectOperations.FirstOrDefault()!, groups, categories);
                    if (!products.Item1)
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProductStandardValueNotValid);
                    productRequests.AddRange(products.Item2);
                }

                //Deductions
                var deductionRequests = new List<DeductionServiceModel>();
                if (item.DeductionRequests is not null && item.DeductionRequests?.Count > 0)
                {
                    foreach (var item1 in item.DeductionRequests)
                    {
                        var deduction = new DeductionServiceModel()
                        {
                            Height = item1.Height,
                            Length = item1.Length,
                            Number = item1.Number,
                            Weight = item1.Weight,
                            Width = item1.Width,
                        };
                        deductionRequests.Add(deduction);
                    }

                    var sumFinalAmounts = deductionRequests.Sum(x => x.FinalAmount);
                    if (sumFinalAmounts >= finalAmount)
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.DeductionsUnValid);
                }

                foreach (var projectOperationId in item.ProjectOperationIds)
                {
                    var projectOperation = projectOperations.Where(x => x.Id == projectOperationId).FirstOrDefault();
                    if (projectOperation is null)
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
                    ///TODO Employers
                    ///TODO در حال حاضر نیاز به این ولیدیت نیست بخاطر اینکه در قرارداد کارفرما ثبت تاریخ واقعی قطعی نیست
                    //if (item!.StartDate.HasValue && item.EndDate.HasValue && 1 == 2)
                    //{
                    //    if (projectOperation.EmployerContract is not null && projectOperation.EmployerContract.StartDate is not null)
                    //        if (!(projectOperation.EmployerContract.StartDate.Value.Date <= item.StartDate.Value.Date))
                    //            return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.DateTimeNotValidForContract);
                    //}

                    var newCode = "";
                    if (string.IsNullOrEmpty(newCode))
                    {
                        var codeResponse = await _mediator.Send(new ProjectOperationDetailCodeCreatorCommand(projectOperation.Id, location!.Id, companyId), ct);
                        if (codeResponse.IsFailure)
                            return Result.Failure<CreateVizardProjectOperationDetailResponse>(codeResponse.Error!);
                        newCode = codeResponse.Value ?? "";
                    }

                    var isDuplicate = await _mediator.Send(new GetProjectOperationDetailForValidatesQuery(projectOperationId, location.Id, newCode, companyId), ct);
                    if (isDuplicate is { IsSuccess: true, Value: not null })
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProjectOperationDetailIsDuplicate);

                    //Validate Contractor and ServiceInfo
                    var contractorServiceRequests = new List<ContractorServiceModel>();

                    if (item.ContractorServiceRequests is not null &&
                        item.ContractorServiceRequests.Count > 0)
                    {
                        // Contractor Ids
                        var contractorIds = item.ContractorServiceRequests
                            .Where(x => x.ContractorId.HasValue && x.ContractorId.Value > 0)
                            .Select(x => x.ContractorId!.Value)
                            .Distinct()
                            .ToList();

                        List<UserModel?>? contractors = null;

                        if (contractorIds.Count > 0)
                        {
                            var contractorsData = await _mediator.Send(
                                new GetWithSkillOnlyByIdsQuery(
                                    1,
                                    contractorIds.Count,
                                    contractorIds,
                                    null,
                                    false,
                                    null),
                                ct);

                            if (contractorsData.IsFailure)
                                return Result.Failure<CreateVizardProjectOperationDetailResponse>(
                                    ProjectOperationDetailErrors.UnValidContractors);

                            contractors = contractorsData.Value?.Data;
                        }

                        // Service Ids
                        var serviceIds = item.ContractorServiceRequests
                            .Select(x => x.ServiceInfoId)
                            .Where(x => x != 0)
                            .Distinct()
                            .ToList();

                        var serviceInfosData = await _mediator.Send(
                            new GetsServiceInfoByIdsQuery(
                                1,
                                serviceIds.Count,
                                serviceIds),
                            ct);

                        if (serviceInfosData.IsFailure)
                            return Result.Failure<CreateVizardProjectOperationDetailResponse>(
                                ProjectOperationDetailErrors.UnValidServiceInfos);

                        foreach (var service in item.ContractorServiceRequests)
                        {
                            var serviceInfoData = projectOperation
                                .OperationInfo
                                .OperationInfoServices
                                .FirstOrDefault(x =>
                                    x.ServiceInfo.Id == service.ServiceInfoId &&
                                    !x.IsDeleted);

                            if (serviceInfoData is null)
                                return Result.Failure<CreateVizardProjectOperationDetailResponse>(
                                    ProjectOperationDetailErrors.UnValidServiceInfoInOperationInfo);

                            if (!string.IsNullOrEmpty(service.TimeSpant))
                            {
                                var timeParts = service.TimeSpant.Split(':');

                                if (!(timeParts[0].Length >= 2 &&
                                      timeParts[1].Length == 2))
                                {
                                    return Result.Failure<CreateVizardProjectOperationDetailResponse>(
                                        MachineryStandardErrors.TimeSpantCountError);
                                }

                                if (int.Parse(timeParts[1]) > 59)
                                {
                                    return Result.Failure<CreateVizardProjectOperationDetailResponse>(
                                        MachineryStandardErrors.MoreThan59Min);
                                }
                            }

                            // Backward compatibility:
                            var type = service.Type ?? PODContractorServiceType.ServiceBased;

                            UserModel? contractorData = null;

                            if (service.ContractorId.HasValue && service.ContractorId.Value > 0)
                            {
                                contractorData = contractors?
                                    .FirstOrDefault(x => x?.Id == service.ContractorId.Value);

                                if (contractorData is null)
                                    return Result.Failure<CreateVizardProjectOperationDetailResponse>(
                                        ProjectOperationDetailErrors.UnValidContractors);
                            }

                            contractorServiceRequests.Add(
                                new ContractorServiceModel(
                                    service.TempId,
                                    contractorData?.Id,
                                    contractorData?.FullName,
                                    serviceInfoData,
                                    null,
                                    service.Volume,
                                    TimeCalculator.StringToTicks(service.TimeSpant),
                                    service.IsActive,
                                    type));
                        }
                    }

                    if (item.CreatedProductId is not null)
                    {
                        if (!projectOperation.GoodsInProgress)
                            return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.CanNotCreateGoods);

                        var productData = await WebServicesLogic.ProductDataReceiver(item.CreatedProductId, _mediator, _pRepo, ct);
                        if (productData is null)
                            return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidProducts);
                    }

                    var response = await _mediator.Send(new CreateVizardProjectOperationDetailCommand(
                        newCode,
                        projectOperation,
                        location,
                        item.StartDate,
                        item.EndDate,
                        item.Length,
                        item.LengthChangeable,
                        item.Width,
                        item.WidthChangeable,
                        item.Height,
                        item.HeightChangeable,
                        item.Weight,
                        item.WeightChangeable,
                        item.Number,
                        item.NumberChangeable,
                        item.Status,
                        item.Priority,
                        item.Day,
                        item.Hour,
                        item.CreatedProductId,
                        item.Description,
                        item.PlannerRequests,
                        item.ImplementationAssistantRequests,
                        item.TechnicalAssistantRequests,
                        contractorServiceRequests,
                        expertRequests,
                        item.ContractorExpertLinks,
                        machineryRequests,
                        productRequests,
                        deductionRequests,
                        item.Urls,
                        companyId), ct);
                    if (response.IsFailure)
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(response.Error!);

                    if (!projectOperation.Project.Contractual)
                        if (!Workloader(projectOperation))
                            await SendNotification(projectOperation, location, item?.Description, ct);

                    var detials = new ProjectOperationDetailList(response.Value!.Id, response.Value!.FinalAmount, true);
                    var workloder = await _mediator.Send(new ProjectOperationWorkloderCommand(projectOperation!.Id, [detials]), ct);
                    if (workloder.IsFailure)
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(workloder.Error!);
                    projectOperation = workloder.Value!;

                    if (validated.Any(x => x.ProjectOperationId.Equals(projectOperation.Id) && x.OperationLocationId.Equals(location.Id)))
                        return Result.Failure<CreateVizardProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProjectOperationDetailIsDuplicateInVizard);
                    else
                        validated.Add(new(projectOperation.Id, location.Id));

                    values.Add(response.Value!);
                }
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateVizardProjectOperationDetailResponse(values.Select(x => x.Id).ToList());
    }

    public async Task<Result<UpdateProjectOperationDetailResponse?>> UpdateProjectOperationDetail(
        UpdateProjectOperationDetailRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectOperationDetail, perationLocationId:{OperationLocationId},", request.OperationLocationId);

        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationDetailValidator, UpdateProjectOperationDetailRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailResponse>(isValidRequest.Error!);

        var projectOperationDetailValue = await _mediator.Send(new GetProjectOperationDetailByIdQuery(request.Id), ct);
        if (projectOperationDetailValue.IsFailure || projectOperationDetailValue.Value is null)
            return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var projectOperationDetail = projectOperationDetailValue.Value;
        var projectOperation = projectOperationDetailValue.Value.ProjectOperation;
        var operationInfoRes = await _mediator.Send(new GetOperationInfoWithProjectOperationIdQuery(projectOperation.Id), ct);
        var operationInfo = operationInfoRes.Value!;

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateProjectOperationDetailResponse>(GlobalErrors.InvalidCompany);

        if (projectOperationDetail.DailyOperations.Where(x => x.FinalAmount > 0).ToList().Count > 0)
        {
            var limitedTime = projectOperationDetail.DailyOperations.Where(x => x.FinalAmount > 0).ToList().OrderByDescending(x => x.StartDate).FirstOrDefault();
            if (limitedTime is not null)
                if (request.StartDate.HasValue)
                    if (limitedTime.StartDate.Date < request.StartDate!.Value.Date)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.DateTimeNotValidForDaily);
        }

        var finalAmount = Calculator.FinalAmount(request.Length, request.Width, request.Height, request.Weight, request.Number);

        if (projectOperationDetail.Status == ProjectOperationDetailStatus.EndOfWork ||
                projectOperationDetail.Status == ProjectOperationDetailStatus.TemporaryDelivery ||
                    projectOperationDetail.Status == ProjectOperationDetailStatus.DefiniteDelivery)
            return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.StatusIsInvalid);

        var operationLocation = await _mediator.Send(new GetOperationLocationByIdQuery(request.OperationLocationId), ct);
        if (operationLocation.IsFailure || operationLocation.Value is null)
            return Result.Failure<UpdateProjectOperationDetailResponse>(OperationLocationErrors.OperationLocationWithIdNotFound);

        if (projectOperationDetail.OperationLocation.Id != operationLocation.Value.Id && projectOperationDetail.DailyOperations.Where(x => x.FinalAmount > 0).ToList().Count > 0)
        {
            var currentUser = _userProfileService.GetProfileInfo();
            var queryCreators = await WebServicesLogic.UserDataReceiver([currentUser.UserId], null, _mediator, ct);
            var projectManager = queryCreators?.FirstOrDefault();
            if (projectManager is null)
                return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProjectManagerInfoIsUnValid);

            if (projectManager.Id != projectOperationDetail.ProjectOperation.Project.ProjectManager)
                return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.OnlyProjectManagerCanChangeLocation);
        }

        var newCode = projectOperationDetail.Code;
        if (!string.IsNullOrEmpty(request.Code))
            if (request.Code != projectOperationDetail.Code)
                newCode = request.Code;

        var isDuplicate = await _mediator.Send(new GetProjectOperationDetailForValidatesQuery(projectOperationDetail.ProjectOperation.Id, request.OperationLocationId, newCode, companyId), ct);
        if (isDuplicate is { IsSuccess: true, Value: not null } && isDuplicate.Value.Id != request.Id)
        {
            if (isDuplicate.Value.Code == "1" && newCode == "1")
            {
                var codeResponse = await _mediator.Send(new ProjectOperationDetailCodeCreatorCommand(projectOperation.Id, operationLocation.Value!.Id, companyId), ct);
                if (codeResponse.IsFailure)
                    return Result.Failure<UpdateProjectOperationDetailResponse>(codeResponse.Error!);
                newCode = codeResponse.Value ?? "";
            }
            else
                return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProjectOperationDetailIsDuplicate);
        }

        var plannersData = new List<UserModel?>();
        if (request.PlannerRequests is not null && request.PlannerRequests?.Count > 0)
        {
            var ids = request.PlannerRequests.Where(x => x != default).ToList();
            var plannerDatas = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct); // بره سراغ متا دیتا
            if (plannerDatas.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidPlanners);
            plannersData = plannerDatas.Value?.Data!;
        }

        var implementationsData = new List<UserModel?>();
        if (request.ImplementationAssistantRequests is not null && request.ImplementationAssistantRequests.Count > 0)
        {
            var ids = request.ImplementationAssistantRequests.Where(x => x != default).ToList();
            var implementationDatas = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct); // بره سراغ متا دیتا
            if (implementationDatas.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidImplementations);
            implementationsData = implementationDatas.Value?.Data!;
        }

        var technicalsData = new List<UserModel?>();
        if (request.TechnicalAssistantRequests is not null && request.TechnicalAssistantRequests?.Count > 0)
        {
            var ids = request.TechnicalAssistantRequests.Where(x => x != default).ToList();
            var technicalDatas = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct); // بره سراغ متا دیتا
            if (technicalDatas.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidTechnicals);
            technicalsData = technicalDatas.Value?.Data!;
        }

        var contractorMap = new Dictionary<string, ProjectOperationDetailContractorService>();
        var expertMap = new Dictionary<string, ConsumableVolumeExpert>();

        if (request.ContractorServiceRequests is not null && request.ContractorServiceRequests.Count > 0)
        {
            var contractorServices = request.ContractorServiceRequests.OrderByDescending(x => x.Id != null).ToList();
            contractorServices = contractorServices.OrderByDescending(x => x.IsDeleted).ToList();

            var projectServiceDetails = new List<ProjectServiceDetail>();
            var projectServiceIds = request.ContractorServiceRequests.Where(x => x.ProjectServiceDetailId is not null && x.ProjectServiceDetailId > 0).Select(x => x.ProjectServiceDetailId!.Value).Distinct().ToList();
            if (projectServiceIds.Any())
            {
                var getProjectServices = await _mediator.Send(new GetsProjectServiceDetailByIdsQuery(projectServiceIds, 1, projectServiceIds.Count), ct); // بره سراغ متا دیتا
                if (getProjectServices.IsFailure)
                    return Result.Failure<UpdateProjectOperationDetailResponse>(getProjectServices.Error!);
                var projectServiceValues = getProjectServices.Value!.Data!;
                if (projectServiceIds.Count > projectServiceValues!.Count)
                    return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.ProjectServicesNotValidate);
                projectServiceDetails = projectServiceValues;
            }

            foreach (var item in contractorServices)
            {
                var serviceInfo = await _mediator.Send(new GetOperationInfoServiceForValidationQuery(operationInfo.Id, item.ServiceInfoId), ct);
                if (serviceInfo.IsFailure || serviceInfo.Value is null)
                    return Result.Failure<UpdateProjectOperationDetailResponse>(serviceInfo.Error!);
                var serviceInfoData = operationInfo.OperationInfoServices.Where(c => c.ServiceInfo.Id == item.ServiceInfoId && !c.IsDeleted).FirstOrDefault();
                if (serviceInfoData is null)
                    return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidServiceInfoInOperationInfo);

                if (!string.IsNullOrEmpty(item.TimeSpant))
                {
                    if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                        return Result.Failure<UpdateProjectOperationDetailResponse>(MachineryStandardErrors.TimeSpantCountError);

                    if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(MachineryStandardErrors.MoreThan59Min);
                }

                if (item.IsDeleted is not null && item.IsDeleted == true)
                {
                    if (item.Id is not null && item.Id != 0)
                    {
                        var deleteData = await _mediator.Send(new DisableContractorServiceCommand((long)item.Id, projectOperationDetail.Id), ct);
                        if (deleteData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(deleteData.Error!);
                    }
                    else
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ContractorServiceErrors.CanNotDelete);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null)
                {
                    var contractorInfo = new List<UserModel>();
                    if (item.ContractorId is not null && item.ContractorId != default)
                    {
                        var ids = new List<long> { (long)item.ContractorId };
                        var contractorsData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, ids, null, false, null), ct); // بره سراغ متا دیتا
                        if (contractorsData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidContractors);
                        contractorInfo = contractorsData.Value?.Data!;
                    }

                    var projectServiceDetail = projectServiceDetails.FirstOrDefault(x => x.Id == item.ProjectServiceDetailId);
                    if (projectServiceDetail is not null)
                    {
                        var updateData = await _mediator.Send(new UpdateContractorServiceCommand((long)item.Id, projectServiceDetail.OperationInfoService, projectServiceDetail, item.ContractorId, item.Volume, TimeCalculator.StringToTicks(item.TimeSpant), item.IsActive), ct);
                        if (updateData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(updateData.Error!);
                        if (!String.IsNullOrEmpty(item.TempId))
                            contractorMap[item.TempId] = updateData.Value!;
                    }
                    else
                    {
                        var updateData = await _mediator.Send(new UpdateContractorServiceCommand((long)item.Id, serviceInfoData, null, item.ContractorId, item.Volume, TimeCalculator.StringToTicks(item.TimeSpant), item.IsActive), ct);
                        if (updateData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(updateData.Error!);
                        if (!String.IsNullOrEmpty(item.TempId))
                            contractorMap[item.TempId] = updateData.Value!;
                    }
                }
                else if (item.Id is null)
                {
                    var contractorInfo = new List<UserModel>();
                    if (item.ContractorId is not null)
                        if (item.ContractorId != default)
                        {
                            var ids = new List<long> { (long)item.ContractorId };
                            var contractorsData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, ids, null, false, null), ct); // بره سراغ متا دیتا
                            if (contractorsData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidContractors);
                            contractorInfo = contractorsData.Value?.Data!;
                        }

                    var projectServiceDetail = projectServiceDetails.FirstOrDefault(x => x.Id == item.ProjectServiceDetailId);
                    if (projectServiceDetail is not null)
                    {
                        var createData = await _mediator.Send(
                            new CreateContractorServiceCommand(
                                projectOperationDetail,
                                projectServiceDetail.OperationInfoService,
                                projectServiceDetail,
                                item.ContractorId,
                                item.Volume,
                                TimeCalculator.StringToTicks(item.TimeSpant),
                                item.IsActive,
                                item.Type ?? PODContractorServiceType.ServiceBased),
                            ct);
                        if (createData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(createData.Error!);
                        if (!String.IsNullOrEmpty(item.TempId))
                            contractorMap[item.TempId] = createData.Value!;
                    }
                    else
                    {
                        var createData = await _mediator.Send(
                            new CreateContractorServiceCommand(
                                projectOperationDetail,
                                serviceInfoData,
                                null,
                                item.ContractorId,
                                item.Volume,
                                TimeCalculator.StringToTicks(item.TimeSpant),
                                item.IsActive,
                                item.Type ?? PODContractorServiceType.ServiceBased),
                            ct);
                        if (createData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(createData.Error!);
                        if (!String.IsNullOrEmpty(item.TempId))
                            contractorMap[item.TempId] = createData.Value!;
                    }
                }
            }
        }

        if (request.ExpertRequests is not null && request.ExpertRequests.Count > 0)
        {
            var consumabaleExperts = request.ExpertRequests.OrderByDescending(x => x.Id != null).ToList();
            consumabaleExperts = consumabaleExperts.OrderByDescending(x => x.IsDeleted).ToList();
            foreach (var item in consumabaleExperts)
            {
                if (item.IsDeleted is not null && item.IsDeleted == true)
                {
                    if (item.Id is not null)
                    {
                        var deleteData = await _mediator.Send(new DeleteConsumableVolumeExpertCommand((long)item.Id), ct);
                        if (deleteData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(deleteData.Error!);
                    }
                    else
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeExpertErrors.CanNotDelete);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null)
                {
                    var consumableVolumeExpert = await _mediator.Send(new GetConsumableVolumeExpertByIdQuery((long)item.Id), ct);
                    if (consumableVolumeExpert.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeExpertErrors.NoHaveExperts);
                    var expert = consumableVolumeExpert.Value!;
                    //Validate Experts
                    var expertData = await _mediator.Send(new GetSkillByIdQuery(item.ExpertId), ct); // بره سراغ متا دیتا
                    if (expertData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeExpertErrors.UnValidExperts);

                    var standardData = operationInfo.ConsumptionStandardExperts.Where(x => x.ExpertUnitId == item?.ExpertId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;
                    long standardValue = 0;
                    if (isStandard == true)
                        standardValue = TimeCalculator.StandardTimeConsumption(standardData!.TimeSpant, expert.ProjectOperationDetail.FinalAmount);

                    var finalValue = TimeCalculator.StringToTicks(item!.FinalValue);

                    var updateData = await _mediator.Send(new UpdateConsumableVolumeExpertCommand(consumableVolumeExpert.Value!, item.ExpertId,
                        item.Number, item.UnusedPercentage, isStandard, standardValue, finalValue), ct);
                    if (updateData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(updateData.Error!);

                    if (!String.IsNullOrEmpty(item.TempId))
                        expertMap[item.TempId] = updateData.Value!;
                }
                else if (item.Id is null)
                {
                    var expertData = await _mediator.Send(new GetSkillByIdQuery(item.ExpertId), ct); // بره سراغ متا دیتا
                    if (expertData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeExpertErrors.UnValidExperts);
                    if (projectOperationDetail.ConsumableVolumeExperts.Any(x => x.ExpertId == item.ExpertId))
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeExpertErrors.AvailableId);

                    var standardData = operationInfo.ConsumptionStandardExperts.Where(x => x.ExpertUnitId == item?.ExpertId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;
                    long standardValue = 0;
                    if (isStandard == true)
                        standardValue = TimeCalculator.StandardTimeConsumption(standardData!.TimeSpant, projectOperationDetail.FinalAmount);

                    var finalValue = TimeCalculator.StringToTicks(item!.FinalValue);

                    var createData = await _mediator.Send(new CreateConsumableVolumeExpertCommand(projectOperationDetail,
                        item.ExpertId, item.Number, item.UnusedPercentage, isStandard, standardValue, finalValue), ct);
                    if (createData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(createData.Error!);

                    if (!String.IsNullOrEmpty(item.TempId))
                        expertMap[item.TempId] = createData.Value!;
                }
            }
        }

        if (request.MachineryRequests is not null && request.MachineryRequests.Count > 0)
        {
            var consumabaleMachineries = request.MachineryRequests.OrderByDescending(x => x.Id != null).ToList();
            consumabaleMachineries = consumabaleMachineries.OrderByDescending(x => x.IsDeleted).ToList();
            foreach (var item in consumabaleMachineries)
            {
                if (item.IsDeleted is not null && item.IsDeleted == true)
                {
                    if (item.Id is not null)
                    {
                        var deleteData = await _mediator.Send(new DeleteConsumableVolumeMachineryCommand((long)item.Id), ct);
                        if (deleteData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(deleteData.Error!);
                    }
                    else
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeMachineryErrors.CanNotDelete);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null)
                {
                    var consumableVolumeMachinery = await _mediator.Send(new GetConsumableVolumeMachineryByIdQuery((long)item.Id), ct);
                    if (consumableVolumeMachinery.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeMachineryErrors.NoHaveMachineries);
                    var machinery = consumableVolumeMachinery.Value!;
                    //Validate Machinerys
                    var machineryData = await _mediator.Send(new GetMachineryByIdQuery(item.MachineryId), ct); // بره سراغ متا دیتا
                    if (machineryData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeMachineryErrors.UnValidMachinerys);

                    var standardData = operationInfo.ConsumptionStandardMachineries.Where(x => x.Machinery.Id == item?.MachineryId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;
                    long standardValue = 0;
                    if (isStandard == true)
                        standardValue = TimeCalculator.StandardTimeConsumption(standardData!.TimeSpant, machinery.ProjectOperationDetail.FinalAmount);

                    //string input = item!.FinalValue;
                    //decimal finalValue = 0;

                    //if (decimal.TryParse(input, out var decimalVal))
                    //    finalValue = decimalVal;

                    var final = (item.FinalValue.Contains(":") && item.Unit != null && item.Unit == RequestMachineryUnit.Hourly) ||
                                (Convert.ToDecimal(item.FinalValue) > 1000000) ?
                                Convert.ToDecimal(TimeCalculator.StringToTicks(item!.FinalValue)) : Convert.ToDecimal(item.FinalValue);

                    var updateData = await _mediator.Send(new UpdateConsumableVolumeMachineryCommand(consumableVolumeMachinery.Value!,
                        machineryData.Value!, item.Number, item.UnusedPercentage, isStandard, standardValue, Convert.ToDecimal(final), item.Unit), ct);
                    if (updateData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(updateData.Error!);
                }
                else if (item.Id is null)
                {
                    var machineryData = await _mediator.Send(new GetMachineryByIdQuery(item.MachineryId), ct); // بره سراغ متا دیتا
                    if (machineryData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeMachineryErrors.UnValidMachinerys);
                    if (projectOperationDetail.ConsumableVolumeMachineries.Any(x => x.Machinery.Id == item.MachineryId))
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeMachineryErrors.AvailableId);

                    var standardData = operationInfo.ConsumptionStandardMachineries
                        .Where(x => x.Machinery.Id == item?.MachineryId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;
                    long standardValue = 0;
                    if (isStandard == true)
                        standardValue = TimeCalculator.StandardTimeConsumption(standardData!.TimeSpant, projectOperationDetail.FinalAmount);

                    //string input = item!.FinalValue;
                    //decimal finalValue = 0;

                    //if (decimal.TryParse(input, out var decimalVal))
                    //    finalValue = decimalVal;

                    var final = (item.FinalValue.Contains(":") && item.Unit != null && item.Unit == RequestMachineryUnit.Hourly) ||
                                (Convert.ToDecimal(item.FinalValue) > 1000000) ?
                                Convert.ToDecimal(TimeCalculator.StringToTicks(item!.FinalValue)) : Convert.ToDecimal(item.FinalValue);

                    var createData = await _mediator.Send(new CreateConsumableVolumeMachineryCommand(projectOperationDetail,
                        machineryData.Value!, item.Number, item.UnusedPercentage, isStandard, standardValue, Convert.ToDecimal(final), item.Unit), ct);
                    if (createData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(createData.Error!);
                }
            }
        }

        if (request.ProductRequests is not null && request.ProductRequests.Count > 0)
        {
            var consumabaleProducts = request.ProductRequests.OrderByDescending(x => x.Id != null).ToList();
            consumabaleProducts = consumabaleProducts.OrderByDescending(x => x.IsDeleted).ToList();
            foreach (var item in consumabaleProducts)
            {
                if (item.IsDeleted is not null && item.IsDeleted == true)
                {
                    if (item.Id is not null)
                    {
                        var deleteData = await _mediator.Send(new DeleteConsumableVolumeProductCommand((long)item.Id), ct);
                        if (deleteData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(deleteData.Error!);
                    }
                    else
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.CanNotDelete);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null)
                {
                    var consumableVolumeProduct = await _mediator.Send(new GetConsumableVolumeProductByIdQuery((long)item.Id), ct);
                    if (consumableVolumeProduct.IsFailure || consumableVolumeProduct.Value is null)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.NoHaveProducts);

                    if (ValidateProduct(consumableVolumeProduct.Value, item.FinalValue, consumableVolumeProduct.Value!.UnusedPercentage ?? 0))
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.UnValidForSupplies);

                    if (item.VolumeProductType == VolumeProductType.ProductGroup)
                    {
                        //Validate Products
                        var productData = await _mediator.Send(new GetGroupByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                        if (productData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.UnValidProducts);
                    }
                    else if (item.VolumeProductType == VolumeProductType.Category)
                    {
                        //Validate Products
                        var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                        if (categoryData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.UnValidProducts);
                    }
                    else
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.UnValidType);

                    var product = consumableVolumeProduct.Value!;
                    var standardData = operationInfo.ConsumptionStandardProduct.Where(x => x.ProductUnitId == item?.ProductGroupId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;

                    decimal standardValue = 0;
                    if (isStandard == true)
                        standardValue = standardData!.Number * product.ProjectOperationDetail.FinalAmount;

                    var updateData = await _mediator.Send(new UpdateConsumableVolumeProductCommand(consumableVolumeProduct.Value!, item.ProductGroupId,
                        item.UnusedPercentage, isStandard, standardValue, item.FinalValue, item.VolumeProductType!.Value), ct);
                    if (updateData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(updateData.Error!);
                }
                else if (item.Id is null)
                {
                    if (item.VolumeProductType == VolumeProductType.ProductGroup)
                    {
                        if (projectOperationDetail.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup)
                            .Any(x => x.ProductGroupId == item.ProductGroupId))
                            return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.AvailableId);

                        var productData = await _mediator.Send(new GetGroupByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                        if (productData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.UnValidProducts);
                    }
                    else if (item.VolumeProductType == VolumeProductType.Category)
                    {
                        if (projectOperationDetail.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.Category)
                            .Any(x => x.ProductGroupId == item.ProductGroupId))
                            return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.AvailableId);

                        var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                        if (categoryData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.UnValidCategories);
                    }
                    else
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ConsumableVolumeProductErrors.UnValidType);

                    var standardData = operationInfo.ConsumptionStandardProduct.Where(x => x.ProductUnitId == item?.ProductGroupId).FirstOrDefault();

                    var isStandard = standardData is not null ? true : false;

                    decimal standardValue = 0;
                    if (isStandard == true)
                        standardValue = standardData!.Number * projectOperationDetail.FinalAmount;

                    var createData = await _mediator.Send(new CreateConsumableVolumeProductCommand(projectOperationDetail, item.ProductGroupId,
                        item.UnusedPercentage, isStandard, standardValue, item.FinalValue, item.VolumeProductType!.Value), ct);
                    if (createData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(createData.Error!);
                }
            }
        }

        if (request.DeductionRequests is not null && request.DeductionRequests.Count > 0)
        {
            var deductions = request.DeductionRequests.OrderByDescending(x => x.Id != null).ToList();
            deductions = deductions.OrderByDescending(x => x.IsDeleted).ToList();

            foreach (var item in deductions)
            {
                if (item.IsDeleted is not null && item.IsDeleted == true)
                {
                    if (item.Id is not null)
                    {
                        var deleteData = await _mediator.Send(new DeleteProjectOperationDetailDeductionCommand((long)item.Id), ct);
                        if (deleteData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailResponse>(deleteData.Error!);
                    }
                    else
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailDeductionErrors.CanNotDelete);
                }
                else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null)
                {
                    var deductionResponse = await _mediator.Send(new GetProjectOperationDetailDeductionByIdQuery((long)item.Id), ct);
                    if (deductionResponse.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailDeductionErrors.DeductionWithIdNotFound);
                    var deduction = deductionResponse.Value!;

                    var updateData = await _mediator.Send(new UpdateProjectOperationDetailDeductionCommand(deduction.Id, item.Length, item.Width,
                        item.Height, item.Weight, item.Number), ct);
                    if (updateData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(updateData.Error!);
                }
                else if (item.Id is null)
                {
                    var createData = await _mediator.Send(new CreateProjectOperationDetailDeductionCommand(projectOperationDetail,
                        item.Length, item.Width, item.Height, item.Weight, item.Number), ct);
                    if (createData.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailResponse>(createData.Error!);
                }
            }

            var deductionRequests = new List<DeductionServiceModel>();
            foreach (var item in request.DeductionRequests.Where(x => x.IsDeleted == null || x.IsDeleted == false))
            {
                var deduction = new DeductionServiceModel()
                {
                    Height = item.Height,
                    Length = item.Length,
                    Number = item.Number,
                    Weight = item.Weight,
                    Width = item.Width,
                };
                deductionRequests.Add(deduction);
            }

            var sumFinalAmounts = deductionRequests.Sum(x => x.FinalAmount);
            if (sumFinalAmounts >= finalAmount)
                return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.DeductionsUnValid);
        }

        if (request.CreatedProductId is not null)
        {
            if (!projectOperation.GoodsInProgress)
                return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.CanNotCreateGoods);

            if (projectOperationDetail.CreatedProductId != request.CreatedProductId)
            {
                if (projectOperationDetail.Status == ProjectOperationDetailStatus.EndOfWork)
                    return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.CanNotChangeProduct);

                var productData = await WebServicesLogic.ProductDataReceiver(request.CreatedProductId, _mediator, _pRepo, ct);
                if (productData is null)
                    return Result.Failure<UpdateProjectOperationDetailResponse>(ProjectOperationDetailErrors.UnValidProducts);
            }
        }

        List<CreateBatchPODContractorExpertModel>? createContractorExperts = [];
        List<UpdateBatchPODContractorExpertModel>? updateContractorExperts = [];

        if (request.ContractorExpertLinks is not null && request.ContractorExpertLinks.Count > 0)
        {
            var ids = request.ContractorExpertLinks.NullListed(x => x.Id);
            var deletes = await _detailRepo.GetByIds(ids, ct);
            foreach (var item in request.ContractorExpertLinks)
            {
                if (item.Id is not null && item.IsDelete == true)
                {
                    var delete = deletes?.FirstOrDefault(x => x.Id == item.Id);
                    if (delete is not null)
                        delete.SoftDelete();
                    await _detailRepo.Update(delete);
                }

                else if (item.Id is not null && item.IsDelete == false && item.Volume != null && item.IsActive != null)
                {
                    updateContractorExperts.Add(new UpdateBatchPODContractorExpertModel
                    {
                        Id = item.Id.Value,
                        Volume = item.Volume.Value,
                        IsActive = item.IsActive.Value
                    });
                }

                else if (item.Id is null && item.IsDelete == false && !String.IsNullOrEmpty(item.ContractorServiceTempId) && !String.IsNullOrEmpty(item.ExpertTempId))
                {
                    if (!contractorMap.TryGetValue(item.ContractorServiceTempId, out var contractor))
                        continue;

                    if (!expertMap.TryGetValue(item.ExpertTempId, out var expert))
                        continue;

                    var createModel = new CreateBatchPODContractorExpertModel
                    {
                        ProjectOperationDetailContractorService = contractor,
                        ConsumableVolumeExpert = expert,
                        Volume = item.Volume!.Value,
                        IsActive = item.IsActive!.Value
                    };

                    createContractorExperts.Add(createModel);
                }

                else if (item.IsDelete == false && item.ExpertId != null && item.ContractorServiceId != null)
                {
                    var contractorService = await _mediator.Send(new GetContractorServiceByIdQuery(item.ContractorServiceId.Value), ct);
                    var consumeExpert = await _mediator.Send(new GetConsumableVolumeExpertByIdQuery(item.ExpertId.Value), ct);
                    if (!contractorService.IsBad() || !consumeExpert.IsBad())
                    {
                        var createModel = new CreateBatchPODContractorExpertModel
                        {
                            ProjectOperationDetailContractorService = contractorService.Value!,
                            ConsumableVolumeExpert = consumeExpert.Value!,
                            Volume = item.Volume!.Value,
                            IsActive = item.IsActive!.Value
                        };

                        createContractorExperts.Add(createModel);
                    }
                }
            }

            var update = await _mediator.Send(new UpdateBatchPODContractorExpertCommand(updateContractorExperts), ct);
            if (update.IsBad())
                return update.Failure<UpdateProjectOperationDetailResponse?>();

            var create = await _mediator.Send(new CreateBatchPODContractorExpertCommand(createContractorExperts), ct);
            if (create.IsBad())
                return create.Failure<UpdateProjectOperationDetailResponse?>();
        }

        var response = await _mediator.Send(new UpdateProjectOperationDetailCommand(
            newCode,
            projectOperationDetail,
            operationLocation.Value!,
            request.StartDate,
            request.EndDate,
            request.Length,
            request.LengthChangeable,
            request.Width,
            request.WidthChangeable,
            request.Height,
            request.HeightChangeable,
            request.Weight,
            request.WeightChangeable,
            request.Number,
            request.NumberChangeable,
            finalAmount,
            request.Status,
            request.Priority,
            request.Day,
            request.Hour,
            request.CreatedProductId,
            request.Description,
            request.PlannerRequests,
            request.ImplementationAssistantRequests,
            request.TechnicalAssistantRequests,
            request.Urls,
            companyId,
            request.StatusDescription), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailResponse>(response.Error!);

        if (!projectOperation.Project.Contractual)
            if (!Workloader(projectOperation))
                await SendNotification(projectOperation, operationLocation.Value, request?.Description, ct);

        if (request!.Status is not null && request.Status != ProjectOperationDetailStatus.NotStarted)
            projectOperation.SetProjectOperationStatus((ProjectOperationStatus)request.Status);

        var itemListed = new ProjectOperationDetailList(response.Value!.Id, response.Value!.FinalAmount, false);
        var workloder = await _mediator.Send(new ProjectOperationWorkloderCommand(projectOperation!.Id, [itemListed]), ct);
        if (workloder.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailResponse>(workloder.Error!);
        projectOperation = workloder.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationDetailResponse(response.Value!.Id, projectOperation!.Workload, projectOperation.Id, projectOperation.ProjectOperationStatus, projectOperation.ProjectOperationStatus.GetEnumDescription());
    }

    public async Task<Result<UpdateProjectOperationDetailsResponse?>> UpdateProjectOperationDetails(
        UpdateProjectOperationDetailsRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectOperationDetails");

        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationDetailsValidator, UpdateProjectOperationDetailsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailsResponse>(isValidRequest.Error!);

        var projectOperationDetailValues = await _mediator.Send(new GetsProjectOperationDetailByIdsQuery(request.Ids), ct);
        if (projectOperationDetailValues.IsFailure || projectOperationDetailValues.Value is null)
            return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var projectOperationDetails = projectOperationDetailValues.Value!.Data!;

        var projectOperations = projectOperationDetails.Select(x => x.ProjectOperation).ToList();
        var operationInfos = projectOperationDetails.Select(x => x.ProjectOperation.OperationInfo).ToList();

        if (projectOperationDetails.Any(x => x.DailyOperations.Where(x => x.FinalAmount > 0).ToList().Count > 0))
        {
            foreach (var projectOperationDetail in projectOperationDetails.Where(x => x.DailyOperations is not null && x.DailyOperations.Count > 0))
            {
                var limitedTime = projectOperationDetail.DailyOperations.Where(x => x.FinalAmount > 0).ToList().OrderByDescending(x => x.StartDate).FirstOrDefault();
                if (limitedTime is not null)
                    if (request.StartDate.HasValue)
                        if (limitedTime.StartDate.Date < request.StartDate!.Value.Date)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.DateTimeNotValidForDaily);
            }
        }

        var finalAmount = Calculator.FinalAmount(request.Length, request.Width, request.Height, request.Weight, request.Number);

        if (projectOperationDetails.Any(x => x.Status == ProjectOperationDetailStatus.EndOfWork ||
                                           x.Status == ProjectOperationDetailStatus.TemporaryDelivery ||
                                           x.Status == ProjectOperationDetailStatus.DefiniteDelivery))
            return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.StatusIsInvalid);

        var plannersData = new List<UserModel?>();
        if (request.PlannerRequests is not null && request.PlannerRequests?.Count > 0)
        {
            var ids = request.PlannerRequests.Where(x => x != default).ToList();
            var plannerDatas = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct); // بره سراغ متا دیتا
            if (plannerDatas.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.UnValidPlanners);
            plannersData = plannerDatas.Value?.Data!;
        }

        var implementationsData = new List<UserModel?>();
        if (request.ImplementationAssistantRequests is not null && request.ImplementationAssistantRequests.Count > 0)
        {
            var ids = request.ImplementationAssistantRequests.Where(x => x != default).ToList();
            var implementationDatas = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct); // بره سراغ متا دیتا
            if (implementationDatas.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.UnValidImplementations);
            implementationsData = implementationDatas.Value?.Data!;
        }

        var technicalsData = new List<UserModel?>();
        if (request.TechnicalAssistantRequests is not null && request.TechnicalAssistantRequests?.Count > 0)
        {
            var ids = request.TechnicalAssistantRequests.Where(x => x != default).ToList();
            var technicalDatas = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct); // بره سراغ متا دیتا
            if (technicalDatas.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.UnValidTechnicals);
            technicalsData = technicalDatas.Value?.Data!;
        }

        foreach (var projectOperationDetail in projectOperationDetails)
        {
            if (request.ContractorServiceRequests is not null && request.ContractorServiceRequests.Count > 0)
            {
                var contractorServices = request.ContractorServiceRequests.OrderByDescending(x => x.Id != null).ToList();
                contractorServices = contractorServices.OrderByDescending(x => x.IsDeleted).ToList();

                var projectServiceDetails = new List<ProjectServiceDetail>();
                var projectServiceIds = request.ContractorServiceRequests.Where(x => x.ProjectServiceDetailId is not null && x.ProjectServiceDetailId > 0).Select(x => x.ProjectServiceDetailId!.Value).Distinct().ToList();
                if (projectServiceIds.Any())
                {
                    var getProjectServices = await _mediator.Send(new GetsProjectServiceDetailByIdsQuery(projectServiceIds, 1, projectServiceIds.Count), ct); // بره سراغ متا دیتا
                    if (getProjectServices.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailsResponse>(getProjectServices.Error!);
                    var projectServiceValues = getProjectServices.Value!.Data!;
                    if (projectServiceIds.Count > projectServiceValues!.Count)
                        return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.ProjectServicesNotValidate);
                    projectServiceDetails = projectServiceValues;
                }
                foreach (var item in contractorServices)
                {
                    var serviceInfo = await _mediator.Send(new GetOperationInfoServiceForValidationQuery(projectOperationDetail.ProjectOperation.OperationInfo.Id, item.ServiceInfoId), ct);
                    if (serviceInfo.IsFailure || serviceInfo.Value is null)
                        return Result.Failure<UpdateProjectOperationDetailsResponse>(serviceInfo.Error!);
                    var serviceInfoData = projectOperationDetail.ProjectOperation.OperationInfo.OperationInfoServices.Where(c => c.ServiceInfo.Id == item.ServiceInfoId && !c.IsDeleted).FirstOrDefault();
                    if (serviceInfoData is null)
                        return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.UnValidServiceInfoInOperationInfo);

                    var projectServiceDetail = projectServiceDetails.FirstOrDefault(x => x.Id == item.ProjectServiceDetailId);
                    if (!string.IsNullOrEmpty(item.TimeSpant))
                    {
                        if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(MachineryStandardErrors.TimeSpantCountError);

                        if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(MachineryStandardErrors.MoreThan59Min);
                    }

                    if (item.Id is not null && item.IsDeleted is not null && item.IsDeleted == true && projectOperationDetail.ProjectOperationDetailContractorServices.Any(x => x.Id == item.Id))
                    {
                        if (item.Id is not null && item.Id != 0)
                        {
                            var deleteData = await _mediator.Send(new DisableContractorServiceCommand((long)item.Id, projectOperationDetail.Id), ct);
                            if (deleteData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(deleteData.Error!);
                        }
                        else
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ContractorServiceErrors.CanNotDelete);
                    }
                    else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null && projectOperationDetail.ProjectOperationDetailContractorServices.Any(x => x.Id == item.Id))
                    {
                        var contractorInfo = new List<UserModel>();
                        if (item.ContractorId is not null && item.ContractorId != default)
                        {
                            var ids = new List<long> { (long)item.ContractorId };
                            var contractorsData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, ids, null, false, null), ct); // بره سراغ متا دیتا
                            if (contractorsData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.UnValidContractors);
                            contractorInfo = contractorsData.Value?.Data!;
                        }

                        var updateData = await _mediator.Send(new UpdateContractorServiceCommand((long)item.Id, serviceInfoData, projectServiceDetail, item.ContractorId, item.Volume, TimeCalculator.StringToTicks(item.TimeSpant), item.IsActive), ct);
                        if (updateData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(updateData.Error!);
                    }
                    else if (item.Id is null)
                    {
                        var contractorInfo = new List<UserModel>();
                        if (item.ContractorId is not null)
                            if (item.ContractorId != default)
                            {
                                var ids = new List<long> { (long)item.ContractorId };
                                var contractorsData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, ids, null, false, null), ct); // بره سراغ متا دیتا
                                if (contractorsData.IsFailure)
                                    return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.UnValidContractors);
                                contractorInfo = contractorsData.Value?.Data!;
                            }

                        var createData = await _mediator.Send(
                            new CreateContractorServiceCommand(
                                projectOperationDetail,
                                serviceInfoData,
                                projectServiceDetail,
                                item.ContractorId,
                                item.Volume,
                                TimeCalculator.StringToTicks(item.TimeSpant),
                                item.IsActive,
                                item.Type ?? PODContractorServiceType.ServiceBased),
                            ct);
                        if (createData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(createData.Error!);
                    }
                }
            }

            if (request.ExpertRequests is not null && request.ExpertRequests.Count > 0)
            {
                var consumabaleExperts = request.ExpertRequests.OrderByDescending(x => x.Id != null).ToList();
                consumabaleExperts = consumabaleExperts.OrderByDescending(x => x.IsDeleted).ToList();
                foreach (var item in consumabaleExperts)
                {
                    if (item.Id is not null && item.IsDeleted is not null && item.IsDeleted == true && projectOperationDetail.ConsumableVolumeExperts.Any(x => x.Id == item.Id))
                    {
                        if (item.Id is not null)
                        {
                            var deleteData = await _mediator.Send(new DeleteConsumableVolumeExpertCommand((long)item.Id), ct);
                            if (deleteData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(deleteData.Error!);
                        }
                        else
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeExpertErrors.CanNotDelete);
                    }
                    else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null && projectOperationDetail.ConsumableVolumeExperts.Any(x => x.Id == item.Id))
                    {
                        var consumableVolumeExpert = await _mediator.Send(new GetConsumableVolumeExpertByIdQuery((long)item.Id), ct);
                        if (consumableVolumeExpert.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeExpertErrors.NoHaveExperts);
                        var expert = consumableVolumeExpert.Value!;
                        //Validate Experts
                        var expertData = await _mediator.Send(new GetSkillByIdQuery(item.ExpertId), ct); // بره سراغ متا دیتا
                        if (expertData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeExpertErrors.UnValidExperts);

                        var standardData = projectOperationDetail.ProjectOperation.OperationInfo.ConsumptionStandardExperts.Where(x => x.ExpertUnitId == item?.ExpertId).FirstOrDefault();

                        var isStandard = standardData is not null ? true : false;
                        long standardValue = 0;
                        if (isStandard == true)
                            standardValue = TimeCalculator.StandardTimeConsumption(standardData!.TimeSpant, expert.ProjectOperationDetail.FinalAmount);

                        var finalValue = TimeCalculator.StringToTicks(item!.FinalValue);

                        var updateData = await _mediator.Send(new UpdateConsumableVolumeExpertCommand(consumableVolumeExpert.Value!, item.ExpertId,
                            item.Number, item.UnusedPercentage, isStandard, standardValue, finalValue), ct);
                        if (updateData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(updateData.Error!);
                    }
                    else if (item.Id is null)
                    {
                        var expertData = await _mediator.Send(new GetSkillByIdQuery(item.ExpertId), ct); // بره سراغ متا دیتا
                        if (expertData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeExpertErrors.UnValidExperts);
                        if (projectOperationDetail.ConsumableVolumeExperts.Any(x => x.ExpertId == item.ExpertId))
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeExpertErrors.AvailableId);

                        var standardData = projectOperationDetail.ProjectOperation.OperationInfo.ConsumptionStandardExperts.Where(x => x.ExpertUnitId == item?.ExpertId).FirstOrDefault();

                        var isStandard = standardData is not null ? true : false;
                        long standardValue = 0;
                        if (isStandard == true)
                            standardValue = TimeCalculator.StandardTimeConsumption(standardData!.TimeSpant, projectOperationDetail.FinalAmount);

                        var finalValue = TimeCalculator.StringToTicks(item!.FinalValue);

                        var createData = await _mediator.Send(new CreateConsumableVolumeExpertCommand(projectOperationDetail,
                            item.ExpertId, item.Number, item.UnusedPercentage, isStandard, standardValue, finalValue), ct);
                        if (createData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(createData.Error!);
                    }
                }
            }

            if (request.MachineryRequests is not null && request.MachineryRequests.Count > 0)
            {
                var consumabaleMachineries = request.MachineryRequests.OrderByDescending(x => x.Id != null).ToList();
                consumabaleMachineries = consumabaleMachineries.OrderByDescending(x => x.IsDeleted).ToList();
                foreach (var item in consumabaleMachineries)
                {
                    if (item.Id is not null && item.IsDeleted is not null && item.IsDeleted == true && projectOperationDetail.ConsumableVolumeMachineries.Any(x => x.Id == item.Id))
                    {
                        if (item.Id is not null)
                        {
                            var deleteData = await _mediator.Send(new DeleteConsumableVolumeMachineryCommand((long)item.Id), ct);
                            if (deleteData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(deleteData.Error!);
                        }
                        else
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeMachineryErrors.CanNotDelete);
                    }
                    else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null && projectOperationDetail.ConsumableVolumeMachineries.Any(x => x.Id == item.Id))
                    {
                        var consumableVolumeMachinery = await _mediator.Send(new GetConsumableVolumeMachineryByIdQuery((long)item.Id), ct);
                        if (consumableVolumeMachinery.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeMachineryErrors.NoHaveMachineries);
                        var machinery = consumableVolumeMachinery.Value!;
                        //Validate Machinerys
                        var machineryData = await _mediator.Send(new GetMachineryByIdQuery(item.MachineryId), ct); // بره سراغ متا دیتا
                        if (machineryData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeMachineryErrors.UnValidMachinerys);

                        var standardData = projectOperationDetail.ProjectOperation.OperationInfo.ConsumptionStandardMachineries.Where(x => x.Machinery.Id == item?.MachineryId).FirstOrDefault();

                        var isStandard = standardData is not null ? true : false;
                        long standardValue = 0;
                        if (isStandard == true)
                            standardValue = TimeCalculator.StandardTimeConsumption(standardData!.TimeSpant, machinery.ProjectOperationDetail.FinalAmount);

                        var final = (item.FinalValue.Contains(":") && item.Unit != null && item.Unit == RequestMachineryUnit.Hourly) ||
                                    (Convert.ToDecimal(item.FinalValue) > 1000000) ?
                                    Convert.ToDecimal(TimeCalculator.StringToTicks(item!.FinalValue)) : Convert.ToDecimal(item.FinalValue);

                        var updateData = await _mediator.Send(new UpdateConsumableVolumeMachineryCommand(consumableVolumeMachinery.Value!,
                            machineryData.Value!, item.Number, item.UnusedPercentage, isStandard, standardValue, final, item.Unit), ct);
                        if (updateData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(updateData.Error!);
                    }
                    else if (item.Id is null)
                    {
                        var machineryData = await _mediator.Send(new GetMachineryByIdQuery(item.MachineryId), ct); // بره سراغ متا دیتا
                        if (machineryData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeMachineryErrors.UnValidMachinerys);
                        if (projectOperationDetail.ConsumableVolumeMachineries.Any(x => x.Machinery.Id == item.MachineryId))
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeMachineryErrors.AvailableId);

                        var standardData = projectOperationDetail.ProjectOperation.OperationInfo.ConsumptionStandardMachineries
                            .Where(x => x.Machinery.Id == item?.MachineryId).FirstOrDefault();

                        var isStandard = standardData is not null ? true : false;
                        long standardValue = 0;
                        if (isStandard == true)
                            standardValue = TimeCalculator.StandardTimeConsumption(standardData!.TimeSpant, projectOperationDetail.FinalAmount);

                        var final = (item.FinalValue.Contains(":") && item.Unit != null && item.Unit == RequestMachineryUnit.Hourly) ||
                                    (Convert.ToDecimal(item.FinalValue) > 1000000) ?
                                    Convert.ToDecimal(TimeCalculator.StringToTicks(item!.FinalValue)) : Convert.ToDecimal(item.FinalValue);

                        var createData = await _mediator.Send(new CreateConsumableVolumeMachineryCommand(projectOperationDetail,
                            machineryData.Value!, item.Number, item.UnusedPercentage, isStandard, standardValue, final, item.Unit), ct);
                        if (createData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(createData.Error!);
                    }
                }
            }

            if (request.ProductRequests is not null && request.ProductRequests.Count > 0)
            {
                var consumabaleProducts = request.ProductRequests.OrderByDescending(x => x.Id != null).ToList();
                consumabaleProducts = consumabaleProducts.OrderByDescending(x => x.IsDeleted).ToList();
                foreach (var item in consumabaleProducts)
                {
                    if (item.Id is not null && item.IsDeleted is not null && item.IsDeleted == true && projectOperationDetail.ConsumableVolumeProducts.Any(x => x.Id == item.Id))
                    {
                        if (item.Id is not null)
                        {
                            var deleteData = await _mediator.Send(new DeleteConsumableVolumeProductCommand((long)item.Id), ct);
                            if (deleteData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(deleteData.Error!);
                        }
                        else
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.CanNotDelete);
                    }
                    else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null && projectOperationDetail.ConsumableVolumeProducts.Any(x => x.Id == item.Id))
                    {
                        var consumableVolumeProduct = await _mediator.Send(new GetConsumableVolumeProductByIdQuery((long)item.Id), ct);
                        if (consumableVolumeProduct.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.NoHaveProducts);

                        if (ValidateProduct(consumableVolumeProduct.Value!, item.FinalValue, consumableVolumeProduct.Value!.UnusedPercentage ?? 0))
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.UnValidForSupplies);

                        if (item.VolumeProductType == VolumeProductType.ProductGroup)
                        {
                            //Validate Products
                            var productData = await _mediator.Send(new GetGroupByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                            if (productData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.UnValidProducts);
                        }
                        else if (item.VolumeProductType == VolumeProductType.Category)
                        {
                            //Validate Products
                            var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                            if (categoryData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.UnValidProducts);
                        }
                        else
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.UnValidType);

                        var product = consumableVolumeProduct.Value!;
                        var standardData = projectOperationDetail.ProjectOperation.OperationInfo.ConsumptionStandardProduct.Where(x => x.ProductUnitId == item?.ProductGroupId).FirstOrDefault();

                        var isStandard = standardData is not null ? true : false;

                        decimal standardValue = 0;
                        if (isStandard == true)
                            standardValue = standardData!.Number * product.ProjectOperationDetail.FinalAmount;

                        var updateData = await _mediator.Send(new UpdateConsumableVolumeProductCommand(consumableVolumeProduct.Value!, item.ProductGroupId,
                            item.UnusedPercentage, isStandard, standardValue, item.FinalValue, item.VolumeProductType!.Value), ct);
                        if (updateData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(updateData.Error!);
                    }
                    else if (item.Id is null)
                    {
                        if (item.VolumeProductType == VolumeProductType.ProductGroup)
                        {
                            if (projectOperationDetail.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup)
                                .Any(x => x.ProductGroupId == item.ProductGroupId))
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.AvailableId);

                            var productData = await _mediator.Send(new GetGroupByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                            if (productData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.UnValidProducts);
                        }
                        else if (item.VolumeProductType == VolumeProductType.Category)
                        {
                            if (projectOperationDetail.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.Category)
                                .Any(x => x.ProductGroupId == item.ProductGroupId))
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.AvailableId);

                            var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery(item.ProductGroupId), ct); // بره سراغ انبار
                            if (categoryData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.UnValidCategories);
                        }
                        else
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ConsumableVolumeProductErrors.UnValidType);

                        var standardData = projectOperationDetail.ProjectOperation.OperationInfo.ConsumptionStandardProduct.Where(x => x.ProductUnitId == item?.ProductGroupId).FirstOrDefault();

                        var isStandard = standardData is not null ? true : false;

                        decimal standardValue = 0;
                        if (isStandard == true)
                            standardValue = standardData!.Number * projectOperationDetail.FinalAmount;

                        var createData = await _mediator.Send(new CreateConsumableVolumeProductCommand(projectOperationDetail, item.ProductGroupId,
                            item.UnusedPercentage, isStandard, standardValue, item.FinalValue, item.VolumeProductType!.Value), ct);
                        if (createData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(createData.Error!);
                    }
                }
            }

            if (request.DeductionRequests is not null && request.DeductionRequests.Count > 0)
            {
                var deductions = request.DeductionRequests.OrderByDescending(x => x.Id != null).ToList();
                deductions = deductions.OrderByDescending(x => x.IsDeleted).ToList();

                foreach (var item in deductions)
                {
                    if (item.IsDeleted is not null && item.IsDeleted == true)
                    {
                        if (item.Id is not null)
                        {
                            var deleteData = await _mediator.Send(new DeleteProjectOperationDetailDeductionCommand((long)item.Id), ct);
                            if (deleteData.IsFailure)
                                return Result.Failure<UpdateProjectOperationDetailsResponse>(deleteData.Error!);
                        }
                        else
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailDeductionErrors.CanNotDelete);
                    }
                    else if ((item.IsDeleted == false || item.IsDeleted is null) && item.Id is not null)
                    {
                        var deductionResponse = await _mediator.Send(new GetProjectOperationDetailDeductionByIdQuery((long)item.Id), ct);
                        if (deductionResponse.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailDeductionErrors.DeductionWithIdNotFound);
                        var deduction = deductionResponse.Value!;

                        var updateData = await _mediator.Send(new UpdateProjectOperationDetailDeductionCommand(deduction.Id, item.Length, item.Width,
                            item.Height, item.Weight, item.Number), ct);
                        if (updateData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(updateData.Error!);
                    }
                    else if (item.Id is null)
                    {
                        var createData = await _mediator.Send(new CreateProjectOperationDetailDeductionCommand(projectOperationDetail,
                            item.Length, item.Width, item.Height, item.Weight, item.Number), ct);
                        if (createData.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailsResponse>(createData.Error!);
                    }
                }

                var deductionRequests = new List<DeductionServiceModel>();
                foreach (var item in request.DeductionRequests.Where(x => x.IsDeleted == null || x.IsDeleted == false))
                {
                    var deduction = new DeductionServiceModel()
                    {
                        Height = item.Height,
                        Length = item.Length,
                        Number = item.Number,
                        Weight = item.Weight,
                        Width = item.Width,
                    };
                    deductionRequests.Add(deduction);
                }

                var sumFinalAmounts = deductionRequests.Sum(x => x.FinalAmount);
                if (sumFinalAmounts >= finalAmount)
                    return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.DeductionsUnValid);
            }
        }

        if (request.CreatedProductId is not null)
        {
            if (projectOperations.Any(x => !x.GoodsInProgress))
                return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.CanNotCreateGoods);

            var productData = await WebServicesLogic.ProductDataReceiver(request.CreatedProductId, _mediator, _pRepo, ct);
            if (productData is null)
                return Result.Failure<UpdateProjectOperationDetailsResponse>(ProjectOperationDetailErrors.UnValidProducts);
        }
        var response = await _mediator.Send(new UpdateProjectOperationDetailsCommand(
            projectOperationDetails,
            request.StartDate,
            request.EndDate,
            request.Length,
            request.LengthChangeable,
            request.Width,
            request.WidthChangeable,
            request.Height,
            request.HeightChangeable,
            request.Weight,
            request.WeightChangeable,
            request.Number,
            request.NumberChangeable,
            finalAmount,
            request.Status,
            request.Day,
            request.Hour,
            request.CreatedProductId,
            request.PlannerRequests,
            request.ImplementationAssistantRequests,
            request.TechnicalAssistantRequests,
            request.StatusDescription), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationDetailsResponse(true);
    }

    public async Task<Result<UpdateProjectOperationDetailVolumesResponse?>> UpdateProjectOperationDetailVolumes(
        UpdateProjectOperationDetailVolumesRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectOperationDetailVolumes");

        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationDetailVolumesValidator, UpdateProjectOperationDetailVolumesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(isValidRequest.Error!);

        List<ProjectOperationDetail>? projectOperationDetails = [];
        if (request.projectOperationId != null && request.projectOperationId > 0)
        {
            var projectOperationDetailValues = await _mediator.Send(new GetsForVolumesByProjectOperationIdQuery(request.projectOperationId), ct);
            if (projectOperationDetailValues.IsFailure || projectOperationDetailValues.Value is null)
                return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
            projectOperationDetails = projectOperationDetailValues.Value!.Data!;
        }
        else if (request.ProjectOperationDetailIds is not null && request.ProjectOperationDetailIds.Count > 0)
        {
            var projectOperationDetailValues = await _mediator.Send(new GetsForVolumesByIdsQuery(request.ProjectOperationDetailIds), ct);
            if (projectOperationDetailValues.IsFailure || projectOperationDetailValues.Value is null)
                return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
            projectOperationDetails = projectOperationDetailValues.Value!.Data!;
        }
        else
            return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(ProjectOperationDetailErrors.InvalidRequestParams);

        var projectOperations = projectOperationDetails.Select(x => x.ProjectOperation).Distinct().ToList();
        if (projectOperations.Count != 1)
            return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(ProjectOperationDetailErrors.InValidProjectOperationDetails);
        var projectOperation = projectOperations.FirstOrDefault();

        var consumableVolumeProducts = projectOperationDetails.SelectMany(x => x.ConsumableVolumeProducts).ToList();
        var consumableVolumeExperts = projectOperationDetails.SelectMany(x => x.ConsumableVolumeExperts).ToList();
        var consumableVolumeMachineries = projectOperationDetails.SelectMany(x => x.ConsumableVolumeMachineries).ToList();

        var newExperts = projectOperation?.OperationInfo.ConsumptionStandardExperts.ToList();
        var newMachineries = projectOperation?.OperationInfo.ConsumptionStandardMachineries.ToList();
        var newProducts = projectOperation?.OperationInfo.ConsumptionStandardProduct.ToList();

        if (consumableVolumeExperts is not null && consumableVolumeExperts.Count > 0)
            foreach (var expert in consumableVolumeExperts)
            {
                if (expert.IsStandard)
                    expert.SetIsStandard(false);

                var newExpert = newExperts?.FirstOrDefault(x => x.ExpertUnitId.Equals(expert.ExpertId));
                if (newExpert is not null)
                {
                    var pod = expert.ProjectOperationDetail;
                    var finalAmount = pod.Length * pod.Width * pod.Height * pod.Number * pod.Weight;

                    expert.SetIsStandard(true);
                    expert.SetStandardValue(newExpert.TimeSpant);
                    expert.SetFinalValue(newExpert.TimeSpant * (long)finalAmount);
                    expert.SetNumber(newExpert.ExpertNumber);
                    expert.SetUnusedPercentage(newExpert.UnusedPercentage);
                }
            }

        if (consumableVolumeMachineries is not null && consumableVolumeMachineries.Count > 0)
            foreach (var machinery in consumableVolumeMachineries)
            {
                if (machinery.IsStandard)
                    machinery.SetIsStandard(false);

                var newMachinery = newMachineries?.FirstOrDefault(x => x.Machinery.Id.Equals(machinery.Machinery.Id));
                if (newMachinery is not null)
                {
                    var pod = machinery.ProjectOperationDetail;
                    var finalAmount = pod.Length * pod.Width * pod.Height * pod.Number * pod.Weight;

                    machinery.SetIsStandard(true);
                    machinery.SetStandardValue(newMachinery.TimeSpant);
                    machinery.SetFinalValue(newMachinery.TimeSpant * (long)finalAmount);
                    machinery.SetNumber(newMachinery.MachineryNumber);
                    machinery.SetUnusedPercentage(newMachinery.UnusedPercentage);
                }
            }

        if (consumableVolumeProducts is not null && consumableVolumeProducts.Count > 0)
            foreach (var product in consumableVolumeProducts)
            {
                if (product.IsStandard)
                    product.SetIsStandard(false);

                var newProduct = newProducts?.FirstOrDefault(x => x.ProductUnitId.Equals(product.ProductGroupId));
                if (newProduct is not null)
                {
                    var pod = product.ProjectOperationDetail;
                    var finalAmount = pod.Length * pod.Width * pod.Height * pod.Number * pod.Weight;

                    if (ValidateProduct(product, newProduct.Number * finalAmount, newProduct.UnusedPercentage ?? 0))
                        return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(ConsumableVolumeProductErrors.UnValidForSupplies);

                    product.SetIsStandard(true);
                    product.SetStandardValue(newProduct.Number);
                    product.SetFinalValue(newProduct.Number * finalAmount);
                    product.SetUnusedPercentage(newProduct.UnusedPercentage);
                }
            }

        var response = await _mediator.Send(new UpdateConsumableVolumesCommand(consumableVolumeExperts, consumableVolumeMachineries, consumableVolumeProducts), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(response.Error!);

        foreach (var item in projectOperationDetails)
        {
            foreach (var expert in newExperts!)
                if (!item.ConsumableVolumeExperts.Any(x => x.ExpertId.Equals(expert.ExpertUnitId)))
                {
                    var createExpert = await _mediator.Send(new CreateConsumableVolumeExpertCommand(item, expert.ExpertUnitId, expert.ExpertNumber,
                        expert.UnusedPercentage, true, expert.TimeSpant, expert.TimeSpant), ct);
                    if (createExpert.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(createExpert.Error!);
                }

            foreach (var machinery in newMachineries!)
                if (!item.ConsumableVolumeMachineries.Any(x => x.Machinery.Id.Equals(machinery.Machinery.Id)))
                {
                    var createMachinery = await _mediator.Send(new CreateConsumableVolumeMachineryCommand(item, machinery.Machinery, machinery.MachineryNumber,
                        machinery.UnusedPercentage, true, machinery.TimeSpant, machinery.TimeSpant, null), ct);
                    if (createMachinery.IsFailure)
                        return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(createMachinery.Error!);
                }

            foreach (var product in newProducts!)
                if (!item.ConsumableVolumeProducts.Any(x => x.ProductGroupId.Equals(product.ProductUnitId)))
                {
                    if (product.StandardProductType == StandardProductType.ProductGroup &&
                        (!item.ConsumableVolumeProducts
                        .Where(x => x.VolumeProductType == VolumeProductType.ProductGroup)
                        .Any(x => x.ProductGroupId.Equals(product.ProductUnitId)))
                       )
                    {
                        var createProduct = await _mediator.Send(new CreateConsumableVolumeProductCommand(item, product.ProductUnitId, product.UnusedPercentage, true, product.Number, product.Number, VolumeProductType.ProductGroup), ct);
                        if (createProduct.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(createProduct.Error!);
                    }
                    else if (product.StandardProductType == StandardProductType.Category &&
                             (!item.ConsumableVolumeProducts
                              .Where(x => x.VolumeProductType == VolumeProductType.Category)
                              .Any(x => x.ProductGroupId.Equals(product.ProductUnitId)))
                            )
                    {
                        var createProduct = await _mediator.Send(new CreateConsumableVolumeProductCommand(item, product.ProductUnitId, product.UnusedPercentage, true, product.Number, product.Number, VolumeProductType.Category), ct);
                        if (createProduct.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailVolumesResponse>(createProduct.Error!);
                    }
                }
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationDetailVolumesResponse(true);
    }

    public async Task<Result<UpdatesProjectOperationDetailDateResponse?>> UpdatesProjectOperationDetailDate(UpdatesProjectOperationDetailDateRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdatesProjectOperationDetailDate,");

        var isValidRequest = await request.IsValidAsync<UpdatesProjectOperationDetailDateValidator, UpdatesProjectOperationDetailDateRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdatesProjectOperationDetailDateResponse>(isValidRequest.Error!);

        foreach (var item in request.RequestModel)
        {
            var projectOperationDetailValue = await _mediator.Send(new GetProjectOperationDetailByIdLessIncludeQuery(item.Id), ct);
            if (projectOperationDetailValue.IsFailure || projectOperationDetailValue.Value is null)
                return Result.Failure<UpdatesProjectOperationDetailDateResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
            var projectOperationDetail = projectOperationDetailValue.Value;

            ///TODO Employers
            ///TODO در حال حاضر نیاز به این ولیدیت نیست بخاطر اینکه در قرارداد کارفرما ثبت تاریخ واقعی قطعی نیست
            //if (item.StartDate.HasValue && item.EndDate.HasValue && 1 == 2)
            //{
            //    if (projectOperationDetail.ProjectOperation.EmployerContract is not null && projectOperationDetail.ProjectOperation.EmployerContract.StartDate is not null)
            //        if (projectOperationDetail.ProjectOperation.EmployerContract.StartDate.Value.Date < item.StartDate.Value.Date)
            //            return Result.Failure<UpdatesProjectOperationDetailDateResponse>(ProjectOperationDetailErrors.DateTimeNotValidForContract);
            //}

            if (projectOperationDetail.DailyOperations.Where(x => x.FinalAmount > 0).ToList().Count > 0)
            {
                var limitedTime = projectOperationDetail.DailyOperations.Where(x => x.FinalAmount > 0).ToList().OrderByDescending(x => x.StartDate).FirstOrDefault();
                if (limitedTime is not null)
                    if (item.StartDate.HasValue)
                        if (limitedTime.StartDate.Date < item.StartDate.Value.Date)
                            return Result.Failure<UpdatesProjectOperationDetailDateResponse>(ProjectOperationDetailErrors.DateTimeNotValidForDaily);
            }

            var response = await _mediator.Send(new UpdatesProjectOperationDetailDateCommand(projectOperationDetailValue.Value, item.StartDate, item.EndDate), ct);
            if (response.IsFailure)
                return Result.Failure<UpdatesProjectOperationDetailDateResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdatesProjectOperationDetailDateResponse(true);
    }

    public async Task<Result<DeleteProjectOperationDetailResponse?>> DeleteProjectOperationDetail(DeleteProjectOperationDetailRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteProjectOperationDetail, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteProjectOperationDetailValidator, DeleteProjectOperationDetailRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteProjectOperationDetailResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteProjectOperationDetailCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteProjectOperationDetailResponse>(response.Error!);

        decimal worklode = response.Value!.ProjectOperation.Workload;

        var detial = new ProjectOperationDetailList(response.Value!.Id, 0, false);
        var workloder = await _mediator.Send(new ProjectOperationWorkloderCommand(response.Value!.ProjectOperation!.Id, [detial]), ct);
        if (workloder.IsFailure)
            return Result.Failure<DeleteProjectOperationDetailResponse>(workloder.Error!);
        worklode = workloder.Value!.Workload;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectOperationDetailResponse(response.Value!.Id, true, worklode);
    }

    public async Task<Result<ProjectOperationDetailGroupDeleteResponse?>> ProjectOperationDetailGroupDelete(ProjectOperationDetailGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationDetailGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ProjectOperationDetailGroupDeleteValidator, ProjectOperationDetailGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationDetailGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DeleteProjectOperationDetailCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<ProjectOperationDetailGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ProjectOperationDetailGroupDeleteResponse(true);
    }

  

    public async Task<Result<SetProjectOperationDetailPriorityResponse?>> SetProjectOperationDetailPriority(SetProjectOperationDetailPriorityRequest request, CT ct)
    {
        _logger.LogInformation("Request for SetProjectOperationDetailPriority, id:{Id},", request.Id);

        var isValidRequest = await request.IsValidAsync<SetProjectOperationDetailPriorityValidator, SetProjectOperationDetailPriorityRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetProjectOperationDetailPriorityResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new SetProjectOperationDetailPriorityCommand(request.Id, request.Priority), ct);
        if (response.IsFailure)
            return Result.Failure<SetProjectOperationDetailPriorityResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetProjectOperationDetailPriorityResponse(response.Value!.Id, response.Value!.Priority);
    }

    public async Task<Result<ProjectOperationDetailStatusChangerResponse?>> ProjectOperationDetailStatusChanger(ProjectOperationDetailStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationDetailStatusChanger, id:{Id},", request.Id);

        var isValidRequest = await request.IsValidAsync<ProjectOperationDetailStatusChangerValidator, ProjectOperationDetailStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationDetailStatusChangerResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ProjectOperationDetailStatusChangerCommand(request.Id, request.Status, request.StatusDescription), ct);
        if (response.IsFailure)
            return Result.Failure<ProjectOperationDetailStatusChangerResponse>(response.Error!);
        var projectOperation = response.Value?.ProjectOperation;

        await _unitOfWork.CommitAsync(ct);
        return new ProjectOperationDetailStatusChangerResponse(response.Value!.Id, projectOperation!.Id, projectOperation.ProjectOperationStatus, projectOperation.ProjectOperationStatus.GetEnumDescription());
    }

    public async Task<Result<GroupProjectOperationDetailStatusChangerResponse?>> GroupProjectOperationDetailStatusChanger(GroupProjectOperationDetailStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("Request for GroupProjectOperationDetailStatusChanger, Ids:{Ids},", request.Ids);

        var isValidRequest = await request.IsValidAsync<GroupProjectOperationDetailStatusChangerValidator, GroupProjectOperationDetailStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GroupProjectOperationDetailStatusChangerResponse>(isValidRequest.Error!);

        List<ProjectOperationDetail>? projectOperationDetails = [];
        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new ProjectOperationDetailStatusChangerCommand(item, request.Status, request.StatusDescription), ct);
            if (response.IsFailure || response.Value is null)
                return Result.Failure<GroupProjectOperationDetailStatusChangerResponse>(response.Error!);
            projectOperationDetails.Add(response.Value);
        }

        var projectOperation = projectOperationDetails.LastOrDefault()?.ProjectOperation;

        await _unitOfWork.CommitAsync(ct);
        return new GroupProjectOperationDetailStatusChangerResponse(true, projectOperation!.Id, projectOperation.ProjectOperationStatus, projectOperation.ProjectOperationStatus.GetEnumDescription());
    }

    public async Task<Result<GetProjectOperationDetailByIdResponse?>> GetProjectOperationDetailById(GetProjectOperationDetailByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationDetailById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetProjectOperationDetailByIdValidator, GetProjectOperationDetailByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationDetailByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationDetailByIdLessIncludesQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectOperationDetailByIdResponse>(response.Error!);
        var value = response.Value!;

        var measureunitsData = new List<Measureunit?>();
        if (value.ProjectOperationDetailContractorServices is not null)
            if (value.ProjectOperationDetailContractorServices.Count > 0)
            {
                var measureunitIds = value.ProjectOperationDetailContractorServices.Select(c => c.OperationInfoService.ServiceInfo.UnitOfMeasurementId).Where(x => x != 0).ToList();
                if (value.ProjectOperationDetailContractorServices.Any(x => x.ProjectServiceDetail is not null))
                    measureunitIds.AddRange(value.ProjectOperationDetailContractorServices.Where(x => x.ProjectServiceDetail is not null).Select(x => x.ProjectServiceDetail!.ProjectService.ServiceInfo.UnitOfMeasurementId).ToList());
                var measureUnitData = await WebServicesLogic.MeasurementDataReceiver(measureunitIds, _mediator, ct);
                if (measureUnitData is not null && measureUnitData.Count > 0)
                    measureunitsData = measureUnitData!;
            }

        var productRequests = new List<Group?>();
        var categoryRequests = new List<WarehouseCategory?>();
        if (value.ConsumableVolumeProducts is not null)
            if (value.ConsumableVolumeProducts.Count > 0)
            {
                if (value.ConsumableVolumeProducts.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                {
                    var productIds = value.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup)
                        .Select(c => c!.ProductGroupId).Where(x => x != default).ToList();
                    var productsData = await WebServicesLogic.GroupsDataReceiver(productIds, _mediator, ct); // بره سراغ متا دیتا
                    if (productsData is not null && productsData.Count > 0)
                        productRequests = productsData!;
                }

                if (value.ConsumableVolumeProducts.Any(x => x.VolumeProductType == VolumeProductType.Category))
                {
                    var categoryIds = value.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.Category)
                                        .Select(c => c!.ProductGroupId).Where(x => x != default).ToList();
                    var categoriesData = await WebServicesLogic.CategoriesDataReceiver(categoryIds, null, _mediator, ct); // بره سراغ متا دیتا
                    if (categoriesData is not null && categoriesData.Count > 0)
                        categoryRequests = categoriesData!;
                }
            }

        var expertsData = new List<Skill?>();
        if (value.ConsumableVolumeExperts is not null)
            if (value.ConsumableVolumeExperts?.Count > 0)
            {
                var expertIds = value.ConsumableVolumeExperts.Select(c => c.ExpertId).Where(x => x != default).ToList();
                var experts = await WebServicesLogic.SkillsDataReceiver(expertIds, _mediator, ct);
                if (experts is not null && experts.Count > 0)
                    expertsData = experts!;
            }

        IdentityServices.Users.Models.User? creatorInfo = null;
        if (value!.CreatorId != default)
        {
            var userIds = new List<long> { value.CreatorId };
            var userData = await WebServicesLogic.GetUsersDataReceiver(userIds, _mediator, ct); // بره سراغ متا دیتا
            creatorInfo = userData?.FirstOrDefault();
        }

        var allIds = IdCollector(value);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا
        var usersInfos = metaDataInfos?.Adapt<List<GetUserInfo>>(); // تبدیل به نوع مورد نیاز

        var data = await DataCollector(value, usersInfos, measureunitsData, productRequests, categoryRequests, expertsData, creatorInfo, ct);
        return data.Adapt<GetProjectOperationDetailByIdResponse>();
    }

    public async Task<Result<GetProjectOperationDetailByCodeResponse?>> GetProjectOperationDetailByCode(GetProjectOperationDetailByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationDetailByCode, Code:{Code}", request.Code);

        var isValidRequest = await request.IsValidAsync<GetProjectOperationDetailByCodeValidator, GetProjectOperationDetailByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationDetailByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationDetailByCodeQuery(request.Code, request.OperationLocationId, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectOperationDetailByCodeResponse>(response.Error!);
        var value = response.Value!;

        return new GetProjectOperationDetailByCodeResponse(value.Id, value.Code);
    }

    public async Task<Result<GetProjectOperationDetailDoneVolumeResponse?>> GetProjectOperationDetailDoneVolume(GetProjectOperationDetailDoneVolumeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationDetailDoneVolume, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetProjectOperationDetailDoneVolumeValidator, GetProjectOperationDetailDoneVolumeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationDetailDoneVolumeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationDetailDoneVolumeQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectOperationDetailDoneVolumeResponse>(response.Error!);
        var value = response.Value!;

        var deductionAmounts = value.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList().Sum(x => x);
        var data = new GetProjectOperationDetailDoneVolumeResponse()
        {
            Id = value.Id,
            FinalAmount = value.FinalAmount - deductionAmounts,
        };

        var dailies = new List<GetDailyProjectOperationsModel>();
        foreach (var item in value.DailyOperations.Where(x => x.FinalAmount > 0).ToList())
        {
            List<GetDailyDocumentsModel>? docs = [];
            foreach (var item1 in item.DailyProjectOperationDocuments)
            {
                docs.Add(new GetDailyDocumentsModel()
                {
                    Id = item1.Id,
                    Url = item1.Url
                });
            }

            dailies.Add(new()
            {
                Id = item.Id,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                FinalAmount = item.FinalAmount,
                Status = item.Status,
                Length = item.Length,
                Width = item.Width,
                Height = item.Height,
                Weight = item.Weight,
                Number = item.Number,
                Description = item.Description,
                documents = docs
            });
        }

        data.DailyProjectOperations = dailies;
        return data;
    }

    public async Task<Result<GetsProjectOperationDetailByProjectOperationIdResponse?>> GetsProjectOperationDetailByProjectOperationId(GetsProjectOperationDetailByProjectOperationIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, ProjectOperationId:{ProjectOperationId}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailByProjectOperationIdValidator, GetsProjectOperationDetailByProjectOperationIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByProjectOperationIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailByProjectOperationIdQuery(null, request.ProjectOperationId, request.PrivateName, request.PrivateCode, request.FilterData,
            request.EmployerId, request.Status, request.ContractorIds, request.CreateDate, request.StartDate, request.EndDate, request.ServiceInfoIds, request.ImplementationAssistantIds,
            request.TechnicalAssistantIds, request.CreatorId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsProjectOperationDetailByProjectOperationIdResponse>(response.Error!);
        var values = response.Value.Data;

        var companyIds = values.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = IdCollectors(values);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا
        var usersInfos = metaDataInfos?.Adapt<List<GetUserInfo>>(); // تبدیل به نوع مورد نیاز

        List<FilteredUserResponseModel>? userInfos = null;
        var userIds = new List<long>();
        userIds.AddRange(values.Where(x => x.CreatorId != null && x.CreatorId > 0).Select(x => (long)x.CreatorId!).ToList());
        userIds.AddRange(values.Where(x => x.UpdaterId != null && x.CreatorId > 0).Select(x => (long)x.UpdaterId!).ToList());
        userInfos = await WebServicesLogic.UserDataReceiver(userIds.Distinct().ToList(), null, _mediator, ct); // بره سراغ متا دیتا

        var totals = new TotalProjectOperationDetailDataModel()
        {
            TotalHeights = values.Sum(x => x.Height),
            TotalLengths = values.Sum(x => x.Length),
            TotalNumbers = values.Sum(x => x.Number),
            TotalWidths = values.Sum(x => x.Width),
            TotalWeights = values.Sum(x => x.Weight),
            TotalFinalAmounts = values.Sum(x => x.FinalAmount),
            TotalAmounts = values.Sum(x => x.FinalAmount) - values.Where(x => x.DeductionAmounts is not null && x.DeductionAmounts.Count > 0)
                    .Sum(x => x.DeductionAmounts!.Select(a => a).ToList().Sum(a => a)),
            TotalDeductionAmounts = values.Where(x => x.DeductionAmounts is not null && x.DeductionAmounts.Count > 0)
                    .Sum(x => x.DeductionAmounts!.Select(a => a).ToList().Sum(a => a)),
            DailyFinalAmounts = values.Sum(x => x.DailyAmounts?.Sum(d => d)),
            ProjectOperationWorkload = values.FirstOrDefault()?.WorkLoad,
        };

        var responses = values.SetPaging(request.PageIndex - 1, request.PageSize);

        var data = DataCollectors(responses, usersInfos, userInfos, companies);

        return new GetsProjectOperationDetailByProjectOperationIdResponse(totals, data ?? new List<ProjectOperationDetailsModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationDetailReportingResponse?>> GetsProjectOperationDetailReporting(GetsProjectOperationDetailReportingRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationDetailReporting");

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailReportingValidator, GetsProjectOperationDetailReportingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailReportingResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailReportingQuery(
            null,
            request.StartDate,
            request.EndDate,
            request.CreateFrom,
            request.CreateTo,
            request.CostCenterId,
            request.ProjectIds,
            request.OperationInfoIds,
            request.ProjectOperationIds,
            request.ContractorIds,
            request.Statuses,
            request.LocationFilterData,
            request.DescriptionFilterData,
            request.DailyDescription,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectOperationDetailReportingResponse>(response.Error!);
        var values = response.Value?.Data!;

        List<long> measureIds = [];
        var podmeasureIds = values.Where(x => x.MeasurementId > 0).Select(x => x.MeasurementId).Distinct().ToList();
        measureIds.AddRange(podmeasureIds);
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var contractorIds = values?.SelectMany(c => c.ContractorIds.Where(x => x.HasValue && x.Value > 0).Select(x => (long)x!)).Distinct().ToList();
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        List<FilteredUserResponseModel>? userInfos = null;
        var userIds = new List<long>();
        foreach (var item in values!)
        {
            userIds.Add(item.CreatorId);
            if (item.UpdaterId is not null && item.UpdaterId > 0)
                userIds.Add(item.UpdaterId!.Value);
        }
        userInfos = await WebServicesLogic.UserDataReceiver(userIds.Distinct().ToList(), null, _mediator, ct); // بره سراغ متا دیتا

        values.ForEach(item =>
        {
            var creator = userInfos?.FirstOrDefault(x => x.UserId.Equals(item.CreatorId));
            item.CreatorName = creator?.FullName;
            item.CreatorNickname = creator?.Nickname;

            var updater = userInfos?.FirstOrDefault(x => x.UserId.Equals(item.UpdaterId));
            item.UpdaterName = updater?.FullName;
            item.UpdaterNickname = updater?.Nickname;

            item.MeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.MeasurementId)?.Name;

            if (item.DailyAmounts is not null && item.DailyAmounts.Count > 0)
                item.DoneFinalAmount = item.DailyAmounts.Sum(x => x);
            else
                item.DoneFinalAmount = 0;

            if (item.DeductionAmounts is not null && item.DeductionAmounts.Count > 0)
                item.FinalAmount = item.FinalAmount - item.DeductionAmounts.Sum(x => x);

            if (metaDataInfos is not null && metaDataInfos.Any())
            {
                if (item.ContractorIds is not null && item.ContractorIds.Count > 0)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    var contractorFullNames = metaDataInfos?.Where(x => item.ContractorIds.Contains(x.Id)).Select(x => x.FullName).ToList();
                    var contractorNicknames = metaDataInfos?.Where(x => item.ContractorIds.Contains(x.Id)).Select(x => x.Nickname).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    if (contractorFullNames is not null && contractorFullNames.Any())
                        item.Contractors = string.Join(" - ", contractorFullNames!);
                    if (contractorNicknames is not null && contractorNicknames.Any())
                        item.ContractorsNickName = string.Join(" - ", contractorNicknames!);
                }
            }
        });

        values = values.Where(x => (request.MinimumFinalAmount == null || x.FinalAmount >= request.MinimumFinalAmount) &&
                                   (request.MinimumDoneFinalAmount == null || x.DoneFinalAmount >= request.MinimumDoneFinalAmount) &&
                                   (request.MinimumRemaindedFinalAmount == null || x.RemaindedFinalAmount >= request.MinimumRemaindedFinalAmount)).ToList();

        return new GetsProjectOperationDetailReportingResponse(values ?? new List<GetsProjectOperationDetailReportingModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationDetailByExpertIdResponse?>> GetsProjectOperationDetailByExpertId(GetsProjectOperationDetailByExpertIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByExpertId, ProjectOperationId:{ProjectOperationId}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailByExpertIdValidator, GetsProjectOperationDetailByExpertIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByExpertIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailByExpertIdQuery(request.ProjectOperationId, request.ExpertId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByExpertIdResponse>(response.Error!);

        var data = response.Value?.Data!.Adapt<List<GetsByExpertIdModel>>();
        return new GetsProjectOperationDetailByExpertIdResponse(data ?? new List<GetsByExpertIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationDetailByMachineryIdResponse?>> GetsProjectOperationDetailByMachineryId(GetsProjectOperationDetailByMachineryIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByMachineryId, ProjectOperationId:{ProjectOperationId}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailByMachineryIdValidator, GetsProjectOperationDetailByMachineryIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByMachineryIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailByMachineryIdQuery(request.ProjectOperationId, request.MachineryId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByMachineryIdResponse>(response.Error!);

        var value = response.Value?.Data!;
        var data = value.Adapt<List<GetsByMachineryIdModel>>();

        return new GetsProjectOperationDetailByMachineryIdResponse(data ?? new List<GetsByMachineryIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationDetailByProductIdResponse?>> GetsProjectOperationDetailByProductId(GetsProjectOperationDetailByProductIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProductId, ProjectOperationId:{ProjectOperationId}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailByProductIdValidator, GetsProjectOperationDetailByProductIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByProductIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailByProductIdQuery(request.ProjectOperationId, request.ProductId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByProductIdResponse>(response.Error!);
        if (response.Value?.Data is null || response.Value?.Data.Count < 1)
            return Result.Failure<GetsProjectOperationDetailByProductIdResponse>(response.Error!);

        var data = response.Value?.Data!.Adapt<List<GetsByProductIdModel>>();

        return new GetsProjectOperationDetailByProductIdResponse(data ?? new List<GetsByProductIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsConsumableVolumesResponse?>> GetsConsumableVolumes(GetsConsumableVolumesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsConsumableVolumes, id:{Id}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetsConsumableVolumesValidator, GetsConsumableVolumesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsConsumableVolumesResponse>(isValidRequest.Error!);

        if (request.ProjectOperationDetailId is not null)
        {
            var projectOperationDetail = await _mediator.Send(new GetProjectOperationDetailWithVolumesQuery((long)request.ProjectOperationDetailId), ct);
            if (projectOperationDetail.IsFailure)
                return Result.Failure<GetsConsumableVolumesResponse>(projectOperationDetail.Error!);
            if (projectOperationDetail.Value is null)
                return Result.Failure<GetsConsumableVolumesResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
            var value = projectOperationDetail.Value;
            var operationInfo = value.ProjectOperation.OperationInfo;

            var standardResult = await StandardConsumableVolumesCollector(operationInfo, request.FinalAmount, ct);
            var consumableVolumes = await ConsumableVolumesCollector(standardResult, value, request.FinalAmount, value.FinalAmount, ct);

            var expertsData = consumableVolumes.ExpertData;
            if (expertsData is not null && expertsData.Count > 0)
            {
                var expertIds = expertsData.Select(e => e.ExpertId).ToList();
                var expertData = await WebServicesLogic.SkillsDataReceiver(expertIds, _mediator, ct);
                foreach (var item in expertsData)
                {
                    var userInfo = expertData?.Where(x => x?.Id == item.ExpertId).FirstOrDefault();
                    item.ExpertName = userInfo?.Name;
                    item.ExpertCode = userInfo?.Code;
                }
            }

            var productsData = consumableVolumes.ProductData;
            if (productsData is not null && productsData.Count > 0)
            {
                var productItems = value.ConsumableVolumeProducts.ToList();
                if (productItems.Any())
                    foreach (var item in productItems)
                    {
                        var product = productsData.FirstOrDefault(x => x.ProductGroupId == item.ProductGroupId);
                        if (product is not null)
                            if (ValidateProduct(item, product.FinalValue, product.UnusedPercentage ?? 0))
                                return Result.Failure<GetsConsumableVolumesResponse>(ConsumableVolumeProductErrors.UnValidForSupplies);
                    }

                if (productsData.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                {
                    var productIds = productsData.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup).Select(e => e.ProductGroupId).Distinct().ToList();
                    var productsInfo = await WebServicesLogic.GroupsDataReceiver(productIds, _mediator, ct);
                    foreach (var item in productsData.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup))
                    {
                        var product = productsInfo?.Where(x => x.Id == item.ProductGroupId).FirstOrDefault();

                        item.ProductGroupName = product?.Name;
                        item.ProductGroupCode = product?.Code;
                        item.MeasureUnitId = product?.MeasureUnitId;
                        item.MeasureUnitName = product?.MeasureUnitName;
                        item.VolumeProductType = VolumeProductType.ProductGroup;
                    }
                }

                if (productsData.Any(x => x.VolumeProductType == VolumeProductType.Category))
                {
                    var categoryIds = productsData.Where(x => x.VolumeProductType == VolumeProductType.Category).Select(e => e.ProductGroupId).Distinct().ToList();
                    var categorysInfo = await WebServicesLogic.CategoriesDataReceiver(categoryIds, null, _mediator, ct);
                    foreach (var item in productsData.Where(x => x.VolumeProductType == VolumeProductType.Category))
                    {
                        var category = categorysInfo?.Where(x => x.Id == item.ProductGroupId).FirstOrDefault();

                        item.ProductGroupName = category?.Title;
                        item.ProductGroupCode = category?.Code;
                        item.MeasureUnitId = null;
                        item.MeasureUnitName = null;
                        item.VolumeProductType = VolumeProductType.Category;
                    }
                }
            }

            var machineriesData = consumableVolumes.MachineryData;

            var result = new GetsConsumableVolumesResponseModel(expertsData, machineriesData, productsData);
            return new GetsConsumableVolumesResponse(result ?? null);
        }
        else
        {
            var projectOperation = await _mediator.Send(new GetProjectOperationByIdOperationInfoIncludeQuery(request.ProjectOperationId), ct);
            if (projectOperation.IsFailure)
                return Result.Failure<GetsConsumableVolumesResponse>(projectOperation.Error!);
            if (projectOperation.Value is null)
                return Result.Failure<GetsConsumableVolumesResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

            var projectOperationResponse = await _mediator.Send(new HaveOperationInfoChildQuery(projectOperation.Value.OperationInfo.Id), ct);
            if (projectOperationResponse.IsFailure)
                return Result.Failure<GetsConsumableVolumesResponse>(projectOperation.Error!);
            var operationInfo = projectOperationResponse.Value!;

            var result = await StandardConsumableVolumesCollector(operationInfo, request.FinalAmount, ct);
            return new GetsConsumableVolumesResponse(result ?? null);
        }
    }

    public async Task<Result<GetsProjectOperationDetailStatusResponse?>> GetsProjectOperationDetailStatus(GetsProjectOperationDetailStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectOperationDetailStatus>());

        if (request.RemoveNotStarted is not null && request.RemoveNotStarted == true)
            response = response.Where(x => x.Code != 1).ToList();

        return new GetsProjectOperationDetailStatusResponse(response);
    }

  
    public async Task<Result<GetsSummarizedByProjectOperationIdsResponse?>> GetsSummarizedByProjectOperationIds(GetsSummarizedByProjectOperationIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsSummarizedByProjectOperationIds, ProjectOperationIds:{ProjectOperationIds},", request.ProjectOperationIds);

        var isValidRequest = await request.IsValidAsync<GetsSummarizedByProjectOperationIdsValidator, GetsSummarizedByProjectOperationIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsSummarizedByProjectOperationIdsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsSummarizedByProjectOperationIdsQuery(request.ProjectOperationIds, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsSummarizedByProjectOperationIdsResponse>(response.Error!);
        var valueResponse = response.Value?.Data!;

        var data = new List<GetsSummarizedByProjectOperationIdsModel>();
        foreach (var item in valueResponse)
        {
            var location = item.OperationLocation;
            data.Add(new(item.Id, item.ProjectOperation.Id, location.PrivateName, $"{location.PrivateCode}({item.Code})", location.PublicName, location.PublicCode, item.Description));
        }
        return new GetsSummarizedByProjectOperationIdsResponse(data ?? [], response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsMinimalByProjectOperationIdsResponse?>> GetsMinimalByProjectOperationIds(GetsMinimalByProjectOperationIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMinimalByProjectOperationIds, ProjectOperationIds:{ProjectOperationIds},", request.ProjectOperationIds);

        var isValidRequest = await request.IsValidAsync<GetsMinimalByProjectOperationIdsValidator, GetsMinimalByProjectOperationIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsMinimalByProjectOperationIdsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsMinimalByProjectOperationIdsQuery(request.ProjectOperationIds, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsMinimalByProjectOperationIdsResponse>(response.Error!);
        var valueResponse = response.Value?.Data!;

        var projectOperations = valueResponse.Select(x => x.ProjectOperation).Distinct().ToList();
        var ids = projectOperations.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureDatas = await WebServicesLogic.MeasurementDataReceiver(ids, _mediator, ct);

        var data = new List<GetsMinimalByProjectOperationIdsModel>();
        foreach (var item in valueResponse)
        {
            var location = item.OperationLocation;
            var operation = item.ProjectOperation;
            var measure = measureDatas?.Where(x => x.Id == operation.UnitOfMeasurementId).FirstOrDefault();
            data.Add(new(item.Id, item.Code, operation.Id, operation.OperationInfo.OperationInfoName, operation.OperationInfo.OperationInfoCode, operation.UnitOfMeasurementId, measure?.Name, operation.Workload, location.Id,
                location.PrivateName, $"{location.PrivateCode}({item.Code})", location.PublicName, location.PublicCode, item.FinalAmount));
        }
        return new GetsMinimalByProjectOperationIdsResponse(data ?? [], response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationDetailByIdsResponse?>> GetsProjectOperationDetailByIds(GetsProjectOperationDetailByIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationDetailByIds, ProjectOperationIds:{ProjectOperationIds},", request.Ids);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailByIdsValidator, GetsProjectOperationDetailByIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByIdsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsProjectOperationDetailByIdsWithPageQuery(request.Ids, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByIdsResponse>(response.Error!);
        var valueResponse = response.Value?.Data!;

        var projectOperations = valueResponse.Select(x => x.ProjectOperation).Distinct().ToList();

        var companyIds = valueResponse?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = new List<GetsProjectOperationDetailByIdsModel>();
        foreach (var item in valueResponse!)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            var location = item.OperationLocation;
            var operation = item.ProjectOperation;
            data.Add(new(item.Id, operation.Id, operation.OperationInfo.OperationInfoName, operation.OperationInfo.OperationInfoCode, operation.Workload, location.Id,
                location.PublicName, location.PublicCode, item.FinalAmount, item.CompanyId, company?.NameFa));
        }
        return new GetsProjectOperationDetailByIdsResponse(data ?? [], response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationDetailForSchedulingResponse?>> GetsProjectOperationDetailForScheduling(GetsProjectOperationDetailForSchedulingRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationDetailForScheduling, OperationInfoId:{OperationInfoId},", request.OperationInfoId);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailForSchedulingValidator, GetsProjectOperationDetailForSchedulingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailForSchedulingResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailForSchedulingQuery(request.OperationInfoId, request.OperationLocationId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectOperationDetailForSchedulingResponse>(response.Error!);
        var valueResponse = response.Value?.Data!;

        var projectOperations = valueResponse.Select(x => x.ProjectOperation).Distinct().ToList();
        var ids = projectOperations.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureDatas = await WebServicesLogic.MeasurementDataReceiver(ids, _mediator, ct);

        var data = new List<GetsProjectOperationDetailForSchedulingResponseModel?>();
        foreach (var item in valueResponse)
        {
            var measureData = measureDatas?.Where(x => x.Id.Equals(item.ProjectOperation.UnitOfMeasurementId)).FirstOrDefault();
            data.Add(new(item.Id, item.ProjectOperation.Id, item.ProjectOperation.UnitOfMeasurementId, measureData?.Name, TimeCalculator.DatePiker(item.StartDate),
                TimeCalculator.DatePiker(item.EndDate), item.Status, item.Status?.GetEnumDescription(), item.Priority, item.Day, item.Hour, item.Description));
        }

        return new GetsProjectOperationDetailForSchedulingResponse(data ?? [], response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetProjectOperationDetailContractorsResponse?>> GetProjectOperationDetailContractors(GetProjectOperationDetailContractorsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationDetailContractors");

        var isValidRequest = await request.IsValidAsync<GetProjectOperationDetailContractorsRequestValidator, GetProjectOperationDetailContractorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationDetailContractorsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationDetailContractorsQuery(request.CostCenterIds, request.ProjectIds, request.OperationInfoIds, request.ProjectOperationIds, request.ProjectOperationDetailIds), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetProjectOperationDetailContractorsResponse>(response.Error!);
        var ids = response.Value!.Data;

        List<UserModel>? contractors = [];
        var data = new List<GetProjectOperationDetailContractorsResponseModel>();
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
                    data.Add(new GetProjectOperationDetailContractorsResponseModel()
                    {
                        Id = contractor?.Id,
                        UserId = contractor?.UserId,
                        FullName = contractor?.FullName,
                        Nickname = contractor?.Nickname
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
                    data.Add(new GetProjectOperationDetailContractorsResponseModel()
                    {
                        Id = contractor?.Id,
                        UserId = contractor?.UserId,
                        FullName = contractor?.FullName,
                        Nickname = contractor?.Nickname
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetProjectOperationDetailContractorsResponse(responseData ?? new List<GetProjectOperationDetailContractorsResponseModel>(0), contractors?.Count ?? 0);
    }

    public async Task<Result<GetsProjectOperationDetailByContractorIdsResponse?>> GetsProjectOperationDetailByContractorIds(GetsProjectOperationDetailByContractorIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationDetailByContractorIds, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailByContractorIdsRequestValidator, GetsProjectOperationDetailByContractorIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByContractorIdsResponse>(isValidRequest.Error!);

        var contractorIds = request.ContractorIds?.Distinct().ToList();
        var response = await _mediator.Send(new GetsProjectOperationDetailByContractorIdsQuery(
            request.CostCenterIds,
            request.ProjectIds,
            request.OperationInfoIds,
            request.ProjectOperationIds,
            contractorIds,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectOperationDetailByContractorIdsResponse>(response.Error!);
        var projectOperationDetails = response.Value!.Data;

        List<long> measureIds = [];
        var podmeasureIds = projectOperationDetails!.Where(x => x.ProjectOperation.UnitOfMeasurementId > 0).Select(x => x.ProjectOperation.UnitOfMeasurementId).Distinct().ToList();
        measureIds.AddRange(podmeasureIds);
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        List<UserModel>? contractors = [];
        if (contractorIds is not null && contractorIds.Count > 0)
        {
            var responseValue = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds!, null, null, _mediator, ct);
            if (responseValue is not null && responseValue.Count > 0)
                foreach (var item in responseValue)
                    if (item != null)
                        contractors.Add(item);
        }

        var data = new List<GetsProjectOperationDetailByContractorIdsResponseModel>();
        if (contractors is not null && contractors.Count > 0)
        {
            foreach (var contractor in contractors)
            {
                var projectOperationDetailData = projectOperationDetails!.Where(x => x.ProjectOperationDetailContractorServices is not null && x.ProjectOperationDetailContractorServices.Count > 0)
                                                .SelectMany(x => x.ProjectOperationDetailContractorServices)
                                                .Where(c => c.ContractorId is not null && c.ContractorId > 0 && c.ContractorId == contractor.Id)
                                                .Select(x => x.ProjectOperationDetail).ToList();

                var projectOperationDetailResponseData = new List<ProjectOperationDetailResponseModel>();
                if (projectOperationDetailData is not null && projectOperationDetailData.Count > 0)
                    foreach (var detail in projectOperationDetailData)
                    {
                        if (projectOperationDetailResponseData.Any(x => x.Id == detail.Id))
                            continue;

                        var measurement = measureUnits?.FirstOrDefault(a => a.Id == detail.ProjectOperation.UnitOfMeasurementId);
                        projectOperationDetailResponseData.Add(new ProjectOperationDetailResponseModel()
                        {
                            Id = detail.Id,
                            Code = detail.Code,
                            MeasurementId = detail.ProjectOperation.UnitOfMeasurementId,
                            MeasurementName = measurement?.Name,
                            ProjectOperationId = detail.ProjectOperation.Id,
                            OperationInfoId = detail.ProjectOperation.OperationInfo.Id,
                            OperationInfoCode = detail.ProjectOperation.OperationInfo.OperationInfoCode,
                            OperationInfoName = detail.ProjectOperation.OperationInfo.OperationInfoName,
                            CostCenterId = detail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId,
                            CostCenterName = detail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName,
                            CostCenterCode = detail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode,
                            ProjectId = detail.ProjectOperation.Project.Id,
                            ProjectName = detail.ProjectOperation.Project.ProjectName,
                            ProjectCode = detail.ProjectOperation.Project.ProjectCode,
                            OperationLocationId = detail.OperationLocation.Id,
                            PrivateName = detail.OperationLocation.PrivateName,
                            PrivateCode = detail.OperationLocation.PrivateCode,
                            PublicName = detail.OperationLocation.PublicName,
                            PublicCode = detail.OperationLocation.PublicCode,
                            FinalAmount = detail.FinalAmount,
                            DoneFinalAmount = detail.DailyOperations.Sum(x => x.FinalAmount),
                            StartDate = detail.StartDate.ToString(),
                            EndDate = detail.EndDate.ToString(),
                            Description = detail.Description
                        });
                    }

                data.Add(new GetsProjectOperationDetailByContractorIdsResponseModel()
                {
                    Id = contractor.Id,
                    FullName = contractor.FullName,
                    Nickname = contractor.Nickname,
                    UserId = contractor.UserId,
                    projectOperationDetails = projectOperationDetailResponseData
                });
            }
        }
        return new GetsProjectOperationDetailByContractorIdsResponse(data ?? new List<GetsProjectOperationDetailByContractorIdsResponseModel>(0), data!.Count);
    }

    public async Task<Result<GetsByProjectOperationIdExcelExporterResponse?>> GetsByProjectOperationIdExcelExporter(GetsByProjectOperationIdExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, ProjectOperationId:{ProjectOperationId}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetsByProjectOperationIdExcelExporterValidator, GetsByProjectOperationIdExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByProjectOperationIdExcelExporterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailByProjectOperationIdQuery(request.Ids, request.ProjectOperationId, request.PrivateName, request.PrivateCode, request.FilterData,
            request.EmployerId, request.Status, request.ContractorIds, request.CreateDate, request.StartDate, request.EndDate, request.ServiceInfoIds, request.ImplementationAssistantIds,
            request.TechnicalAssistantIds, request.CreatorId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsByProjectOperationIdExcelExporterResponse>(response.Error!);
        var values = response.Value?.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = IdCollectors(values!);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا
        var usersInfos = metaDataInfos?.Adapt<List<GetUserInfo>>(); // تبدیل به نوع مورد نیاز

        List<FilteredUserResponseModel>? userInfos = null;
        var userIds = new List<long>();
        foreach (var item in values!)
        {
            if (item.CreatorId != null && item.CreatorId > 0)
                userIds.Add(item.CreatorId.Value);
            if (item.UpdaterId != null && item.UpdaterId > 0)
                userIds.Add(item.UpdaterId.Value);
        }
        userInfos = await WebServicesLogic.UserDataReceiver(userIds.Distinct().ToList(), null, _mediator, ct); // بره سراغ متا دیتا

        var totals = new TotalProjectOperationDetailDataExcelExporterModel()
        {
            TotalHeights = values.Sum(x => x.Height),
            TotalLengths = values.Sum(x => x.Length),
            TotalNumbers = values.Sum(x => x.Number),
            TotalWidths = values.Sum(x => x.Width),
            TotalWeights = values.Sum(x => x.Weight),
            TotalFinalAmounts = values.Sum(x => x.FinalAmount),
            TotalAmounts = values.Sum(x => x.FinalAmount) - values.Where(x => x.DeductionAmounts is not null && x.DeductionAmounts.Count > 0)
                    .Sum(x => x.DeductionAmounts!.Select(a => a).ToList().Sum(a => a)),
            TotalDeductionAmounts = values.Where(x => x.DeductionAmounts is not null && x.DeductionAmounts.Count > 0)
                    .Sum(x => x.DeductionAmounts!.Select(a => a).ToList().Sum(a => a)),
            DailyFinalAmounts = values.Sum(x => x.DailyAmounts?.Sum(d => d)),
            ProjectOperationWorkload = values.FirstOrDefault()?.WorkLoad,
        };

        var responses = values.SetPaging(request.PageIndex - 1, request.PageSize);

        var data = DataCollectors(responses, usersInfos, userInfos, companies);

        var dataList = data.Adapt<List<GetsByProjectOperationIdExcelExporterResponseModel>>();

        List<ImplementationAssistantsExcelExporterModel>? implementationAssistants = [];
        List<TechnicalAssistantsExcelExporterModel>? techninalAssistants = [];
        List<PlannerAssistantsExcelExporterModel>? plannerAssistants = [];

        foreach (var item in dataList)
        {
            item.ProjectManagerId = values?.Where(x => x.Id == item.Id).FirstOrDefault()?.ProjectManagerId;
            item.CreatorId = values?.Where(x => x.Id == item.Id).FirstOrDefault()?.CreatorId;
            item.UpdatorId = values?.Where(x => x.Id == item.Id).FirstOrDefault()?.UpdaterId;
            item.Updator = data?.Where(x => x.Id == item.Id).FirstOrDefault()?.Updator?.FullName;
            item.Creator = data?.Where(x => x.Id == item.Id).FirstOrDefault()?.Creator?.FullName;
            item.StatusDescription = data?.Where(x => x.Id == item.Id).FirstOrDefault()?.StatusModel?.Description;
        }

        foreach (var item in data!)
        {
            if (item.ImplementationAssistants != null && item.ImplementationAssistants.Count > 0)
                foreach (var item1 in item.ImplementationAssistants)
                {
                    implementationAssistants.Add(new ImplementationAssistantsExcelExporterModel()
                    {
                        ProjectOpertationDetailId = item.Id,
                        ImplementationAssistantId = item1?.ImplementationAssistantId,
                        ImplementationAssistantName = item1?.ImplementationAssistantName,
                        ImplementationAssistantNickName = item1?.ImplementationAssistantNickName
                    });
                }

            if (item.TechnicalAssistants != null && item.TechnicalAssistants.Count > 0)
                foreach (var item1 in item.TechnicalAssistants)
                {
                    techninalAssistants.Add(new TechnicalAssistantsExcelExporterModel()
                    {
                        ProjectOpertationDetailId = item.Id,
                        TechnicalAssistantId = item1?.TechnicalAssistantId,
                        TechnicalAssistantName = item1?.TechnicalAssistantName,
                        TechnicalAssistantNickName = item1?.TechnicalAssistantNickName
                    });
                }

            if (item.Planners != null && item.Planners.Count > 0)
                foreach (var item1 in item.Planners)
                {
                    plannerAssistants.Add(new PlannerAssistantsExcelExporterModel()
                    {
                        ProjectOpertationDetailId = item.Id,
                        PlannerAssistantId = item1?.PlannerId,
                        PlannerAssistantName = item1?.PlannerName,
                        PlannerAssistantNickName = item1?.PlannerNickName
                    });
                }
        }

        var file = new FileContentResult(ProjectOperationDetailExcels.ProjectOperationDetailToExcel(dataList, totals, implementationAssistants, techninalAssistants, plannerAssistants, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectOperationDetail-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsByProjectOperationIdExcelExporterResponse(file);
    }

    public async Task<Result<GetsByProjectOperationIdExcelEnumsResponse?>> GetsByProjectOperationIdExcelEnums(GetsByProjectOperationIdExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectOperationDetailExcelEnum>());
        return new GetsByProjectOperationIdExcelEnumsResponse(response);
    }

    public async Task<Result<GetsProjectOperationDetailReportingExcelEnumResponse?>> GetsProjectOperationDetailReportingExcelEnum(GetsProjectOperationDetailReportingExcelEnumRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectOperationDetailReportingExcelEnum>());
        return new GetsProjectOperationDetailReportingExcelEnumResponse(response);
    }

    public async Task<Result<GetsProjectOperationDetailReportingExcelExporterResponse?>> GetsProjectOperationDetailReportingExcelExporter(GetsProjectOperationDetailReportingExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationDetailReportingExcelExporter");

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailReportingExcelExporterValidator, GetsProjectOperationDetailReportingExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailReportingExcelExporterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailReportingQuery(
          request.Ids,
          request.StartDate,
          request.EndDate,
          request.CreateFrom,
          request.CreateTo,
          request.CostCenterId,
          request.ProjectIds,
          request.OperationInfoIds,
          request.ProjectOperationIds,
          request.ContractorIds,
          request.Statuses,
          request.LocationFilterData,
          request.DescriptionFilterData,
          request.DailyDescription,
          request.FilterData,
          request.OrderBy,
          request.PageIndex,
          request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectOperationDetailReportingExcelExporterResponse>(response.Error!);
        var values = response.Value?.Data!;

        List<long> measureIds = [];
        var podmeasureIds = values.Where(x => x.MeasurementId > 0).Select(x => x.MeasurementId).Distinct().ToList();
        measureIds.AddRange(podmeasureIds);
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var contractorIds = values?.SelectMany(c => c.ContractorIds.Where(x => x.HasValue && x.Value > 0).Select(x => (long)x!)).Distinct().ToList();
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        List<FilteredUserResponseModel>? userInfos = null;
        var userIds = new List<long>();
        foreach (var item in values!)
        {
            userIds.Add(item.CreatorId);
            if (item.UpdaterId is not null && item.UpdaterId > 0)
                userIds.Add(item.UpdaterId!.Value);
        }
        userInfos = await WebServicesLogic.UserDataReceiver(userIds.Distinct().ToList(), null, _mediator, ct); // بره سراغ متا دیتا

        values.ForEach(item =>
        {
            var creator = userInfos?.FirstOrDefault(x => x.UserId.Equals(item.CreatorId));
            item.CreatorName = creator?.FullName;
            item.CreatorNickname = creator?.Nickname;

            var updater = userInfos?.FirstOrDefault(x => x.UserId.Equals(item.UpdaterId));
            item.UpdaterName = updater?.FullName;
            item.UpdaterNickname = updater?.Nickname;

            item.MeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.MeasurementId)?.Name;

            if (item.DailyAmounts is not null && item.DailyAmounts.Count > 0)
                item.DoneFinalAmount = item.DailyAmounts.Sum(x => x);
            else
                item.DoneFinalAmount = 0;

            if (item.DeductionAmounts is not null && item.DeductionAmounts.Count > 0)
                item.FinalAmount = item.FinalAmount - item.DeductionAmounts.Sum(x => x);

            if (metaDataInfos is not null && metaDataInfos.Any())
            {
                if (item.ContractorIds is not null && item.ContractorIds.Count > 0)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    var contractorFullNames = metaDataInfos?.Where(x => item.ContractorIds.Contains(x.Id)).Select(x => x.FullName).ToList();
                    var contractorNicknames = metaDataInfos?.Where(x => item.ContractorIds.Contains(x.Id)).Select(x => x.Nickname).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    if (contractorFullNames is not null && contractorFullNames.Any())
                        item.Contractors = string.Join(" - ", contractorFullNames!);
                    if (contractorNicknames is not null && contractorNicknames.Any())
                        item.ContractorsNickName = string.Join(" - ", contractorNicknames!);
                }
            }
        });

        values = values.Where(x => (request.MinimumFinalAmount == null || x.FinalAmount >= request.MinimumFinalAmount) &&
                                   (request.MinimumDoneFinalAmount == null || x.DoneFinalAmount >= request.MinimumDoneFinalAmount) &&
                                   (request.MinimumRemaindedFinalAmount == null || x.RemaindedFinalAmount >= request.MinimumRemaindedFinalAmount)).ToList();

        var data = values.Adapt<List<GetsProjectOperationDetailReportingExcelExporterModel>>();
        var file = new FileContentResult(ProjectOperationDetailExcels.ProjectOperationDetailReportingToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectOperations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsProjectOperationDetailReportingExcelExporterResponse(file);
    }

    public async Task<Result<GetsContractorProjectOperationDetailReportsResponse?>> GetsContractorProjectOperationDetailReports(GetsContractorProjectOperationDetailReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsContractorProjectOperationDetailReportsValidator, GetsContractorProjectOperationDetailReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractorProjectOperationDetailReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsContractorProjectOperationDetailReportsQuery(null, request.ContractorId, request.CostCenterId, request.ProjectIds, request.ContractorContractIds,
             request.FromDate, request.ToDate, companyId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsContractorProjectOperationDetailReportsResponse>(responses.Error!);
        var values = responses.Value!.Data;

        var contracts = values!.SelectMany(x => x.DailyOperations.Where(x => x.DailyProjectOperationServices.Any())
            .SelectMany(x => x.DailyProjectOperationServices.Where(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices.Any())
            .SelectMany(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices
            .Select(x => x.ContractorContractDetail.ContractorContract)))).Distinct().ToList();

        var contractorIds = contracts.Select(x => x.ContractorContractHeader.ContractorId).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var currenciesInfo = await CurrencyDataReceiver(contracts.Select(x => x.ContractorContractHeader).Distinct().ToList(), ct);

        var ids = contracts!.Where(x => x.CreatorId > 0).Select(c => c.CreatorId).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(ids, null, _mediator, ct);

        List<GetsContractorProjectOperationDetailReportsModel>? results = [];
        foreach (var item in contracts!)
        {
            var contractor = contractors?.Where(c => c?.Id == item.ContractorContractHeader.ContractorId).FirstOrDefault()?.FullName!;
            var currency = currenciesInfo?.Where(c => c.Id == item.ContractorContractHeader.CurrencyId).FirstOrDefault()?.Name;
            var creator = creators?.Where(c => c.UserId == item.CreatorId).FirstOrDefault()?.FullName;

            results.Add(new()
            {
                ContractorId = item.ContractorContractHeader.ContractorId,
                Contractor = contractor,
                Id = item.Id,
                ContractorContractTypeId = item.ContractorContractType,
                Status = item.ContractorContractHeader.Status,
                CurrencyId = item.ContractorContractHeader.CurrencyId,
                Currency = currency,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                TotalAmount = item.TotalAmount,
                PercentageDoingJobWell = item.PercentageDoingJobWell,
                DoingJobWellAmount = item.DoingJobWellAmount,
                PercentageAdvancePayment = item.PercentageAdvancePayment,
                AdvancePaymentAmount = item.AdvancePaymentAmount,
                DailyLatenessPenalty = item.DailyLatenessPenalty,
                WorkDonePercent = item.WorkDonePercent,
                WorkDeliveryPercent = item.WorkDeliveryPercent,
                WorkCompletionPercent = item.WorkCompletionPercent,
                Description = item.Description,
                CreatorId = item.CreatorId,
                Creator = creator,
                Created = item.Created,
            });
        }
        return new GetsContractorProjectOperationDetailReportsResponse(results, results.Count);
    }

    public async Task<Result<GetsContractorProjectOperationDetailDetailReportsResponse?>> GetsContractorProjectOperationDetailDetailReports(GetsContractorProjectOperationDetailDetailReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsContractorProjectOperationDetailDetailReportsValidator, GetsContractorProjectOperationDetailDetailReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractorProjectOperationDetailDetailReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsContractorProjectOperationDetailDetailReportsQuery(null, request.ContractorContractId, request.ContractorId, request.FromDate,
            request.ToDate, companyId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsContractorProjectOperationDetailDetailReportsResponse>(responses.Error!);
        var values = responses.Value!.Data;

        var contractDetails = values!.SelectMany(x => x.DailyOperations.Where(x => x.DailyProjectOperationServices.Any())
            .SelectMany(x => x.DailyProjectOperationServices.Where(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices.Any())
            .SelectMany(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices))).Distinct().ToList();

        var measurunitIds = contractDetails.Where(x => x.ProjectOperationDetailContractorService is not null)
            .Select(x => x.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.UnitOfMeasurementId).Where(x => x > 0).Distinct().ToList();
        var measreIds = measurunitIds.Adapt<List<long>?>();
        var measurunits = await WebServicesLogic.MeasurementDataReceiver(measreIds, _mediator, ct);

        var ids = contractDetails.Where(x => x.CreatorId > 0).Select(c => c.CreatorId!).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(ids, null, _mediator, ct);

        List<GetsContractorProjectOperationDetailDetailReportsModel>? results = [];
        foreach (var item in contractDetails!)
        {
            var measure = measurunits?.Where(c => c.Id == item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.UnitOfMeasurementId).FirstOrDefault()?.Name;
            var creator = creators?.Where(c => c.UserId == item.CreatorId).FirstOrDefault()?.FullName;

            results.Add(new()
            {
                ContractorContractId = item.ContractorContractDetail.ContractorContract.Id,
                Id = item.Id,
                WorkLoad = item.ContractorContractDetail.WorkLoad,
                StartDate = TimeCalculator.DatePiker(item.ContractorContractDetail.StartDate),
                EndDate = TimeCalculator.DatePiker(item.ContractorContractDetail.EndDate),
                UnitAmount = item.ContractorContractDetail.UnitAmount,
                TotalAmount = item.ContractorContractDetail.TotalAmount,
                ProjectOperationId = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Id : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Id,
                OperationInfoId = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.OperationInfo.Id : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.OperationInfo.Id,
                OperationInfoName = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.OperationInfo.OperationInfoName : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.OperationInfo.OperationInfoCode : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                ProjectId = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.Id : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.Id,
                ProjectName = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectName : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                ProjectCode = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectCode : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectCode,
                CostCenterId = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                CostCenterName = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                CostCenterCode = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterCode : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterCode,
                ServiceInfoId = item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoUnitOfMeasurementId = item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                ServiceInfoUnitOfMeasurement = measure,
                ProjectOperationDetailId = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.Id,
                OperationLocationId = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.Id,
                PrivateName = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.PublicCode,
                PublicName = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.PublicCode,
                Status = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.Status,
                CreatorId = item.CreatorId,
                Creator = creator,
                Created = item.Created,
            });
        }
        return new GetsContractorProjectOperationDetailDetailReportsResponse(results, results.Count);
    }

    public async Task<Result<GetsContractorReportsExcelExporterResponse?>> GetsContractorReportsExcelExporter(GetsContractorReportsExcelExporterRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsContractorReportsExcelExporterValidator, GetsContractorReportsExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractorReportsExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsContractorProjectOperationDetailReportsQuery(null, request.ContractorId, request.CostCenterId, request.ProjectIds, request.ContractorContractIds,
             request.FromDate, request.ToDate, companyId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsContractorReportsExcelExporterResponse>(responses.Error!);
        var values = responses.Value!.Data;

        var contracts = values!.SelectMany(x => x.DailyOperations.Where(x => x.DailyProjectOperationServices.Any())
            .SelectMany(x => x.DailyProjectOperationServices.Where(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices.Any())
            .SelectMany(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices
            .Select(x => x.ContractorContractDetail.ContractorContract)))).Distinct().ToList();

        var contractorIds = contracts.Select(x => x.ContractorContractHeader.ContractorId).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var currenciesInfo = await CurrencyDataReceiver(contracts.Select(x => x.ContractorContractHeader).Distinct().ToList(), ct);

        var ids = contracts!.Where(x => x.CreatorId > 0).Select(c => c.CreatorId).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(ids, null, _mediator, ct);

        List<GetsContractorReportsExcelExporterModel>? results = [];
        foreach (var item in contracts!)
        {
            var contractor = contractors?.Where(c => c?.Id == item.ContractorContractHeader.ContractorId).FirstOrDefault()?.FullName!;
            var currency = currenciesInfo?.Where(c => c.Id == item.ContractorContractHeader.CurrencyId).FirstOrDefault()?.Name;
            var creator = creators?.Where(c => c.UserId == item.CreatorId).FirstOrDefault()?.FullName;

#pragma warning disable CS8601 // Possible null reference assignment.
            results.Add(new()
            {
                ContractorId = item.ContractorContractHeader.ContractorId,
                Contractor = contractor,
                Id = item.Id,
                ContractorContractTypeId = item.ContractorContractType,
                Status = item.ContractorContractHeader.Status,
                CurrencyId = item.ContractorContractHeader.CurrencyId,
                Currency = currency,
                StartDate = TimeCalculator.ConvertToShamsi(item.StartDate),
                EndDate = TimeCalculator.ConvertToShamsi(item.EndDate),
                TotalAmount = item.TotalAmount,
                PercentageDoingJobWell = item.PercentageDoingJobWell,
                DoingJobWellAmount = item.DoingJobWellAmount,
                PercentageAdvancePayment = item.PercentageAdvancePayment,
                AdvancePaymentAmount = item.AdvancePaymentAmount,
                DailyLatenessPenalty = item.DailyLatenessPenalty,
                WorkDonePercent = item.WorkDonePercent,
                WorkDeliveryPercent = item.WorkDeliveryPercent,
                WorkCompletionPercent = item.WorkCompletionPercent,
                Description = item.Description,
                CreatorId = item.CreatorId,
                Creator = creator,
                Created = TimeCalculator.ConvertToShamsi(item.Created),
            });
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        var file = new FileContentResult(ProjectOperationDetailExcels.ContractorReportsToExcel(results, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"EmployerContracts-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsContractorReportsExcelExporterResponse(file);
    }

    public async Task<Result<GetsContractorReportsExcelEnumResponse?>> GetsContractorReportsExcelEnum(GetsContractorReportsExcelEnumRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<ContractorReportsExcelEnum>());
        return new GetsContractorReportsExcelEnumResponse(result);
    }

    public async Task<Result<GetsContractorDetailReportsExcelExporterResponse?>> GetsContractorDetailReportsExcelExporter(GetsContractorDetailReportsExcelExporterRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsContractorDetailReportsExcelExporterValidator, GetsContractorDetailReportsExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractorDetailReportsExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsContractorProjectOperationDetailDetailReportsQuery(null, request.ContractorContractId, request.ContractorId, request.FromDate,
            request.ToDate, companyId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsContractorDetailReportsExcelExporterResponse>(responses.Error!);
        var values = responses.Value!.Data;

        var contractDetails = values!.SelectMany(x => x.DailyOperations.Where(x => x.DailyProjectOperationServices.Any())
            .SelectMany(x => x.DailyProjectOperationServices.Where(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices.Any())
            .SelectMany(x => x.ProjectOperationDetailContractorService.ContractorContractDetailServices))).Distinct().ToList();


        var measurunitIds = contractDetails.Where(x => x.ProjectOperationDetailContractorService is not null)
            .Select(x => x.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.UnitOfMeasurementId).Where(x => x > 0).Distinct().ToList();
        var measreIds = measurunitIds.Adapt<List<long>?>();
        var measurunits = await WebServicesLogic.MeasurementDataReceiver(measreIds, _mediator, ct);

        var ids = contractDetails.Where(x => x.CreatorId > 0).Select(c => c.CreatorId!).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(ids, null, _mediator, ct);

        List<GetsContractorDetailReportsExcelExporterModel>? results = [];
        foreach (var item in contractDetails!)
        {
            var measure = measurunits?.Where(c => c.Id == item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.UnitOfMeasurementId).FirstOrDefault()?.Name;
            var creator = creators?.Where(c => c.UserId == item.CreatorId).FirstOrDefault()?.FullName;

#pragma warning disable CS8601 // Possible null reference assignment.
            results.Add(new()
            {
                ContractorContractId = item.ContractorContractDetail.ContractorContract.Id,
                Id = item.Id,
                WorkLoad = item.ContractorContractDetail.WorkLoad,
                StartDate = TimeCalculator.ConvertToShamsi(item.ContractorContractDetail.StartDate),
                EndDate = TimeCalculator.ConvertToShamsi(item.ContractorContractDetail.EndDate),
                UnitAmount = item.ContractorContractDetail.UnitAmount,
                TotalAmount = item.ContractorContractDetail.TotalAmount,
                ProjectOperationId = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Id : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Id,
                OperationInfoId = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.OperationInfo.Id : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.OperationInfo.Id,
                OperationInfoName = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.OperationInfo.OperationInfoName : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.OperationInfo.OperationInfoCode : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                ProjectId = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.Id : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.Id,
                ProjectName = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectName : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                ProjectCode = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectCode : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectCode,
                CostCenterId = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                CostCenterName = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                CostCenterCode = item.ContractorContractDetail.ProjectOperation is not null ? item.ContractorContractDetail.ProjectOperation?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterCode : item.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterCode,
                ServiceInfoId = item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoUnitOfMeasurementId = item.ProjectOperationDetailContractorService?.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                ServiceInfoUnitOfMeasurement = measure,
                ProjectOperationDetailId = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.Id,
                OperationLocationId = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.Id,
                PrivateName = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.PublicCode,
                PublicName = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.OperationLocation.PublicCode,
                Status = item.ProjectOperationDetailContractorService?.ProjectOperationDetail.Status,
                CreatorId = item.CreatorId,
                Creator = creator,
                Created = TimeCalculator.ConvertToShamsi(item.Created),
            });
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        var file = new FileContentResult(ProjectOperationDetailExcels.ContractorDetailReportsToExcel(results, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"EmployerContracts-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsContractorDetailReportsExcelExporterResponse(file);
    }

    public async Task<Result<GetsContractorDetailReportsExcelEnumResponse?>> GetsContractorDetailReportsExcelEnum(GetsContractorDetailReportsExcelEnumRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<ContractorDetailReportsExcelEnum>());
        return new GetsContractorDetailReportsExcelEnumResponse(result);
    }

    public async Task<Result<GetsProjectOperationDetailDocumentResponse?>> GetsProjectOperationDetailDocument(GetsProjectOperationDetailDocumentRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailDocumentValidator, GetsProjectOperationDetailDocumentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailDocumentResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsProjectOperationDetailDocumentQuery(request.ProjectOperationDetailId), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsProjectOperationDetailDocumentResponse>(responses.Error!);

        return responses.Value!;
    }

    public async Task<Result<PODetailExcelImportResponse>> PODetailExcelImport(
    PODetailExcelImportRequest request,
    CT ct)
    {
        var pODetails = ExcelImporter.Import<PODetailExcelImportModel>(request.DocumentFile);
        if (pODetails is null)
            return Result.Failure<PODetailExcelImportResponse>(GlobalErrors.ErrorOnReadFile)!;

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<PODetailExcelImportResponse>(GlobalErrors.InvalidCompany)!;

        var projectCodes = pODetails.Listed(x => x.ProjectCode);
        var oInfoCodes = pODetails.Listed(x => x.OperationInfoCode);
        var oLocationCodes = pODetails.Listed(x => x.OperationLocationCode);

        var oInfos = await _operationInfoRepository.GetByCodes(oInfoCodes, ct);
        if (oInfos is null || oInfos.Count < 1)
            return Result.Failure<PODetailExcelImportResponse>(OperationInfoErrors.OperationInfoWithCodesNotFound)!;
        var existingOInfoCodes = oInfos.Select(o => o.OperationInfoCode);
        var missingOInfo = oInfoCodes
            .Where(code => !existingOInfoCodes.Contains(code))
            .ToList();

        var pOperations = oInfos.SelectMany(x => x.ProjectOperations).ToList();
        if (pOperations is null && !pOperations.HasAny())
            return Result.Failure<PODetailExcelImportResponse>(ProjectErrors.ProjectOperationWithDetailsNotFound)!;
        var projects = await _projectRepository.GetByCodes(projectCodes, null, false, false, null, ct);
        if (projects is null || projects.Count < 1)
            return Result.Failure<PODetailExcelImportResponse>(ProjectErrors.ProjectWithCodesNotFound)!;
        var existingProjectCodes = projects.Select(p => p.ProjectCode);
        var missingProjects = projectCodes
            .Where(code => !existingProjectCodes.Contains(code))
            .ToList();

        var oLocations = await _operationLocationRepository.GetByCodes(oLocationCodes, ct);
        if (oLocations is null || oLocations.Count < 1)
            return Result.Failure<PODetailExcelImportResponse>(OperationInfoErrors.OperationInfoWithCodesNotFound)!;
        var existingOLocationCodes = oLocations.Select(o => o.PrivateCode);
        var missingOLocations = oLocationCodes
            .Where(code => !existingOLocationCodes.Contains(code))
            .ToList();

        if ((missingOInfo != null && missingOInfo.Count > 0) || (missingProjects != null && missingProjects.Count > 0) || (missingOLocations != null && missingOLocations.Count > 0))
            return Result.Failure<PODetailExcelImportResponse>(ProjectOperationErrors.InvalidImportStatus(missingOInfo.JoinList(), missingProjects.JoinList(), missingOLocations.JoinList()))!;

        foreach (var item in pODetails)
        {
            var finalVal = item.Lenght * item.Width * item.Height * item.Weight * item.Number;
            var project = projects.FirstOrDefault(x => x.ProjectCode == item.ProjectCode);
            var oInfo = project.ProjectOperations.FirstOrDefault(x => x.OperationInfo.OperationInfoCode == item.OperationInfoCode).OperationInfo;
            var oLocation = oLocations.FirstOrDefault(x => x.PrivateCode == item.OperationLocationCode);
            var projectOperation = pOperations.FirstOrDefault(x => x.ProjectId == project.Id && x.OperationInfoId == oInfo.Id);
            var code = await _projectOperationDetailRepository.CodeCreator(projectOperation.Id, oLocation.Id, companyId, ct);

            List<ExpertServiceModel>? ExpertRequests = [];
            List<MachineryServiceModel>? MachineryRequests = [];
            List<ProductServiceModel>? ProductRequests = [];

            //Experts
            if (oInfo.ConsumptionStandardExperts != null)
            {
                foreach (var exp in oInfo.ConsumptionStandardExperts)
                {
                    var expValue = exp.TimeSpant * (long)finalVal;
                    var expert = new ExpertServiceModel
                    {
                        ExpertId = exp.Id,
                        ExpertName = null,
                        ExpertCode = null,
                        Number = exp.ExpertNumber,
                        UnusedPercentage = exp.UnusedPercentage,
                        IsStandard = true,
                        StandardValue = expValue,
                        FinalValue = expValue,
                    };

                    ExpertRequests.Add(expert);
                }
            }

            //Product
            if (oInfo.ConsumptionStandardProduct != null)
            {
                foreach (var product in oInfo.ConsumptionStandardProduct)
                {
                    var consumeProduct = new ProductServiceModel
                    {
                        ProductGroupId = product.ProductUnitId,
                        ProductGroupName = null,
                        ProductGroupCode = null,
                        MeasureUnitName = null,
                        UnusedPercentage = product.UnusedPercentage,
                        IsStandard = true,
                        StandardValue = finalVal * product.Number,
                        FinalValue = finalVal * product.Number,
                        VolumeProductType =
                        product.StandardProductType == StandardProductType.ProductGroup
                        ? VolumeProductType.ProductGroup
                        : VolumeProductType.Category
                    };
                    ProductRequests.Add(consumeProduct);
                }
            }

            //Machinery
            if (oInfo.ConsumptionStandardMachineries != null)
            {
                foreach (var mach in oInfo.ConsumptionStandardMachineries)
                {
                    var machValue = mach.TimeSpant * (long)finalVal;

                    var machine = new MachineryServiceModel
                    {
                        Machinery = mach.Machinery,
                        Number = mach.MachineryNumber,
                        UnusedPercentage = mach.UnusedPercentage,
                        IsStandard = true,
                        StandardValue = machValue,
                        FinalValue = machValue
                    };
                    MachineryRequests.Add(machine);
                }
            }

            var createReq = new CreateProjectOperationDetailCommand(
                code,
                projectOperation,
                oLocation,
                null,
                null,
                item.Lenght,
                false,
                item.Width,
                false,
                item.Height,
                false,
                item.Weight,
                false,
                item.Number,
                false,
                ProjectOperationDetailStatus.NotStarted,
                1,
                null,
                null,
                null,
                item.Description,
                null,
                null,
                null,
                null,
                ExpertRequests,
                null,
                MachineryRequests,
                ProductRequests,
                null,
                null,
                companyId);
            var create = await _mediator.Send(createReq, ct);
            if (create.IsBad())
                return create.Failure<PODetailExcelImportResponse>();
            if (!projectOperation.Project.Contractual)
                if (!Workloader(projectOperation))
                    await SendNotification(projectOperation, oLocation, item.Description, ct);

            var itemListed = new ProjectOperationDetailList(projectOperation.Id, create.Value.FinalAmount, true);
            var workloder = await _mediator.Send(new ProjectOperationWorkloderCommand(projectOperation!.Id, [itemListed]), ct);
            if (workloder.IsBad())
                return workloder.Failure<PODetailExcelImportResponse>()!;
            projectOperation = workloder.Value!;
        }
        await _unitOfWork.CommitAsync(ct);
        return new PODetailExcelImportResponse(true);
    }

    public async Task<Result<GetTotalsByProjectOperationIdResponse?>> GetTotalsByProjectOperationId(GetTotalsByProjectOperationIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTotalsByProjectOperationId, ProjectOperationId:{ProjectOperationId}", request.ProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetTotalsByProjectOperationIdValidator, GetTotalsByProjectOperationIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTotalsByProjectOperationIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTotalsByProjectOperationIdQuery(request.ProjectOperationId), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetTotalsByProjectOperationIdResponse>(response.Error!);
        var values = response.Value!;

        var totals = new GetTotalsByProjectOperationIdResponse()
        {
            TotalHeights = values.Sum(x => x.Height),
            TotalLengths = values.Sum(x => x.Length),
            TotalNumbers = values.Sum(x => x.Number),
            TotalWidths = values.Sum(x => x.Width),
            TotalWeights = values.Sum(x => x.Weight),
            TotalAmounts = values.Sum(x => x.FinalAmount) - values.Sum(x => x.ProjectOperationDetailDeductions.Select(a => a.FinalAmount).ToList().Sum(a => a)),
            TotalFinalAmounts = values.Sum(x => x.FinalAmount),
            TotalDeductionAmounts = values.Sum(x => x.ProjectOperationDetailDeductions.Select(a => a.FinalAmount).ToList().Sum(a => a)),
            DailyFinalAmounts = values.Sum(x => x.DailyOperations.Sum(d => d.FinalAmount)),
            ProjectOperationWorkload = values.FirstOrDefault()?.ProjectOperation.Workload,
        };

        return totals;
    }

    public async Task<Result<GetHistoryByProjectOperationDetailIdResponse?>> GetHistoryByProjectOperationDetailId(GetHistoryByProjectOperationDetailIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetHistoryByProjectOperationDetailId");

        var isValidRequest = await request.IsValidAsync<GetHistoryByProjectOperationDetailIdValidator, GetHistoryByProjectOperationDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetHistoryByProjectOperationDetailIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetHistoryByProjectOperationDetailIdQuery(request.ProjectOperationDetailId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetHistoryByProjectOperationDetailIdResponse>(response.Error!);
        var values = response.Value.Data;

        var creatorIds = values.Where(x => x.CreatorId > 0).Select(x => x.CreatorId).Distinct().ToList();
        creatorIds.Add(values.FirstOrDefault()!.ProjectOperationDetail.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds.Distinct().ToList(), null, _mediator, ct);

        List<GetHistoryByProjectOperationDetailIdModel>? data = [];
        foreach (var item in values)
        {
            data.Add(new GetHistoryByProjectOperationDetailIdModel()
            {
                Id = item.Id,
                Code = item.Code,
                CreatDate = TimeCalculator.ConvertToShamsi(item.Created),
                CreatorId = item.CreatorId,
                Day = item.Day,
                Description = item.Description,
                EndDate = item.EndDate,
                StartDate = item.StartDate,
                FinalAmount = item.FinalAmount,
                Height = item.Height,
                Hour = item.Hour,
                Length = item.Length,
                Number = item.Number,
                Status = item.Status,
                StatusDescription = item.StatusDescription,
                Weight = item.Weight,
                Width = item.Width,
                Creator = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName
            });
        }

        var projectOperationDetail = values.FirstOrDefault()!.ProjectOperationDetail;
        var result = new GetHistoryByProjectOperationDetailIdResponse(
            projectOperationDetail!.Id, projectOperationDetail.ProjectOperation.Id,
            projectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
            projectOperationDetail.OperationLocation.Id, projectOperationDetail.OperationLocation.PublicName,
            projectOperationDetail.OperationLocation.PublicCode, projectOperationDetail.OperationLocation.PrivateName,
            projectOperationDetail.OperationLocation.PrivateCode, projectOperationDetail.CreatorId,
            creators?.FirstOrDefault(x => x.UserId == projectOperationDetail.CreatorId)?.FullName,
            TimeCalculator.ConvertToShamsi(projectOperationDetail.StartDate), TimeCalculator.ConvertToShamsi(projectOperationDetail.EndDate),
            TimeCalculator.ConvertToShamsi(projectOperationDetail.Created), data, response.Value.RowCount);

        return result;
    }

    public async Task<Result<GetTotalsProjectOperationDetailReportResponse?>> GetTotalsProjectOperationDetailReport(GetTotalsProjectOperationDetailReportRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTotalsProjectOperationDetailReport");

        var isValidRequest = await request.IsValidAsync<GetTotalsProjectOperationDetailReportValidator, GetTotalsProjectOperationDetailReportRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTotalsProjectOperationDetailReportResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailReportingQuery(
            null,
            request.StartDate,
            request.EndDate,
            request.CreateFrom,
            request.CreateTo,
            request.CostCenterId,
            request.ProjectIds,
            request.OperationInfoIds,
            request.ProjectOperationIds,
            request.ContractorIds,
            request.Statuses,
            request.LocationFilterData,
            request.DescriptionFilterData,
            request.DailyDescription,
            request.FilterData,
            null,
            0,
            0), ct);
        if (response.IsFailure)
            return Result.Failure<GetTotalsProjectOperationDetailReportResponse>(response.Error!);
        var values = response.Value?.Data!;

        var totals = new GetTotalsProjectOperationDetailReportResponse()
        {
            TotalHeights = values.Sum(x => x.Height),
            TotalLengths = values.Sum(x => x.Length),
            TotalNumbers = values.Sum(x => x.Number),
            TotalWidths = values.Sum(x => x.Width),
            TotalWeights = values.Sum(x => x.Weight),
            TotalAmounts = values.Sum(x => x.FinalAmount) - values.Sum(x => x.DeductionAmounts?.Sum(z => z) ?? 0),
            TotalFinalAmounts = values.Sum(x => x.FinalAmount),
            TotalDeductionAmounts = values.Sum(x => x.DeductionAmounts?.Sum(a => a) ?? 0),
            DailyFinalAmounts = values.Sum(x => x.DailyAmounts?.Sum(z => z) ?? 0),
            ProjectOperationWorkload = values.Sum(x => x.ProjectOperationWorkLoad),
        };

        return totals;
    }

    public async Task<Result<GetFilteredProjectOperationDetailsResponse?>> GetsFilteredDetailsByCostCenterId(
        GetFilteredProjectOperationDetailsRequest request, CT ct)
    {
        var data = await _mediator.Send(
            new GetFilteredProjectOperationDetailsQuery(request.CostCenterId, request.ProjectIds, request.CategoryIds, request.BranchIds, request.SeasonIds, request.OperationInfoIds, request.FilterData, request.PageIndex, request.PageSize));
        if (data.IsFailure)
            return Result.Failure<GetFilteredProjectOperationDetailsResponse>(ProjectOperationErrors.UnValidData);
        var result = data.Value;
        var pOperations = result.Data.Select(x => x.POperationModel);
        var measureNames = await _measureUnitRepo.GetMeasureUnitsByIds(pOperations.Listed(x => x.MeasurementId), ct);

        foreach (var pOperation in pOperations)
            pOperation.MeasurementName = measureNames.FirstOrDefault(x => x.Id == pOperation.MeasurementId).Name;
        return result;
    }

    public async Task<Result<GetProjectContractorsResponse?>> GetProjectContractors(
        GetProjectContractorsRequest request, CT ct)
    {
        _logger.LogInformation("GetMainDashboard");
        var result = await _mediator.Send(new GetProjectContractorsQuery(request.ProjectId,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetProjectContractorsResponse>()!;

        return result;
    }
}
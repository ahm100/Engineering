using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Extensions.Excels.LetterheadExcel;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Extensions.StringExtensions;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoById;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByConsumptionStandards;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdIncludeLess;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfosByCodes;
using Engineering.Application.Services.OperationInfoServices.Queries.GetsByOperationInfoIdIncludeless;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.ValidateProjectOperationDependencyQuery;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorServiceServiceInfo;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsContractorServiceByProjectOperationId;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsByProjectOperationIdForVolumes;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsMinimalByProjectOperationDetialIds;
using Engineering.Application.Services.ProjectOperations.Command.SetPlannedDate;
using Engineering.Application.Services.ProjectOperations.Command.UpdatePrice;
using Engineering.Application.Services.ProjectOperations.Commands.Create;
using Engineering.Application.Services.ProjectOperations.Commands.Delete;
using Engineering.Application.Services.ProjectOperations.Commands.ProjectOperationStatusChanger;
using Engineering.Application.Services.ProjectOperations.Commands.SetProjectOperationPriority;
using Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperation;
using Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationDocuments;
using Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationPrice;
using Engineering.Application.Services.ProjectOperations.Models.ChangeProject;
using Engineering.Application.Services.ProjectOperations.Models.CreateProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.CreateProjectOperationActions;
using Engineering.Application.Services.ProjectOperations.Models.CreatesProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.DeleteProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.DeleteProjectOperationActions;
using Engineering.Application.Services.ProjectOperations.Models.GetCriticalPO;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrBasePricedPOs;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrPOForReports;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetPOActionByPOId;
using Engineering.Application.Services.ProjectOperations.Models.GetPODate;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationById;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationByParams;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationDocuments;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationProgress;
using Engineering.Application.Services.ProjectOperations.Models.GetsByEmployerContract;
using Engineering.Application.Services.ProjectOperations.Models.GetsByOperationInfo;
using Engineering.Application.Services.ProjectOperations.Models.GetsByProject;
using Engineering.Application.Services.ProjectOperations.Models.GetsByProjectOperationId;
using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredByProject;
using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredByProjectIds;
using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredForReports;
using Engineering.Application.Services.ProjectOperations.Models.GetsForDailyProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetsForEmployerStatusStatement;
using Engineering.Application.Services.ProjectOperations.Models.GetsForPricing;
using Engineering.Application.Services.ProjectOperations.Models.GetsPrioritizeProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationByIds;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationByProjectIds;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReportingExcelExporter;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReportingExcelExporter;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelEnum;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelExporter;
using Engineering.Application.Services.ProjectOperations.Models.GetsProposedPrice;
using Engineering.Application.Services.ProjectOperations.Models.GetsStatusType;
using Engineering.Application.Services.ProjectOperations.Models.GetsSummarizedProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetsSummaryProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetsTotalProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsWithoutContract;
using Engineering.Application.Services.ProjectOperations.Models.GroupProjectOperationStatusChanger;
using Engineering.Application.Services.ProjectOperations.Models.Models;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationExcelImports;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationGroupDelete;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationWorkloadManagement;
using Engineering.Application.Services.ProjectOperations.Models.SetPlannedDate;
using Engineering.Application.Services.ProjectOperations.Models.SetProjectOperationPriority;
using Engineering.Application.Services.ProjectOperations.Models.UpdatePrice;
using Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperationDocuments;
using Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperationPrice;
using Engineering.Application.Services.ProjectOperations.Queries.GetCriticalPO;
using Engineering.Application.Services.ProjectOperations.Queries.GetExistingOperationInfoCodesInProject;
using Engineering.Application.Services.ProjectOperations.Queries.GetFltrProjectOperation;
using Engineering.Application.Services.ProjectOperations.Queries.GetForValidate;
using Engineering.Application.Services.ProjectOperations.Queries.GetPODate;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationById;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdNoIncluding;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByParams;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationDocuments;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForUpdate;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForValidating;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationProgress;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationRealtionInfo;
using Engineering.Application.Services.ProjectOperations.Queries.GetsByEmployerContract;
using Engineering.Application.Services.ProjectOperations.Queries.GetsByOperationInfo;
using Engineering.Application.Services.ProjectOperations.Queries.GetsByProject;
using Engineering.Application.Services.ProjectOperations.Queries.GetsByProjectOperationId;
using Engineering.Application.Services.ProjectOperations.Queries.GetsFilteredByProject;
using Engineering.Application.Services.ProjectOperations.Queries.GetsFilteredByProjectIds;
using Engineering.Application.Services.ProjectOperations.Queries.GetsFilteredForReports;
using Engineering.Application.Services.ProjectOperations.Queries.GetsForDailyProjectOperations;
using Engineering.Application.Services.ProjectOperations.Queries.GetsForEmployerStatusStatement;
using Engineering.Application.Services.ProjectOperations.Queries.GetsForPricing;
using Engineering.Application.Services.ProjectOperations.Queries.GetsPrioritizeProjectOperation;
using Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByIds;
using Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByProjectIds;
using Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationDailyReporting;
using Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationEmployerReporting;
using Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Queries.GetsProposedPrice;
using Engineering.Application.Services.ProjectOperations.Queries.GetsSummarizedProjectOperation;
using Engineering.Application.Services.ProjectOperations.Queries.GetsSummaryProjectOperation;
using Engineering.Application.Services.ProjectOperations.Queries.GetsTotalProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Queries.GetsWithoutContract;
using Engineering.Application.Services.ProjectOperations.Queries.ProjectOperationWorkloadManagement;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailByProjectOperationId;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetMeasureunitById;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectOperations;

public partial class ProjectOperationLogic : IProjectOperationLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectOperationLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IMeasureUnitRepository _measureUnitRepository;
    private readonly IProjectOperationRepository _repository;
    private readonly IProjectOperationActionRepository _projectOperationActionRepository;
    private readonly IOperationInfoActionRepository _operationInfoActionRepository;

    public ProjectOperationLogic(
        IMediator mediator,
        ILogger<ProjectOperationLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IMeasureUnitRepository measureUnitRepository,
        IProjectOperationRepository repository,
        IProjectOperationActionRepository projectOperationActionRepository,
        IOperationInfoActionRepository operationInfoActionRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _measureUnitRepository = measureUnitRepository;
        _repository = repository;
        _projectOperationActionRepository = projectOperationActionRepository;
        _operationInfoActionRepository = operationInfoActionRepository;
    }

    public async Task<Result<CreateProjectOperationResponse?>> CreateProjectOperation(
        CreateProjectOperationRequest request, CT ct)
    {
        using (var transaction = new CreateTransaction())
        {
            _logger.LogInformation("Request for CreateProjectOperation, ProjectId:{ProjectId}, OperationInfoId:{OperationInfoId},", request.ProjectId, request.OperationInfoId);
            //Validate data
            var isValidRequest = await request.IsValidAsync<CreateProjectOperationValidator, CreateProjectOperationRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<CreateProjectOperationResponse>(isValidRequest.Error!);
            if (request.Priority <= 0)
                return Result.Failure<CreateProjectOperationResponse>(ProjectOperationErrors.PriorityCanNot0);
            //Find OperationInfo
            var operationInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.OperationInfoId), ct);
            if (operationInfo.IsFailure)
                return Result.Failure<CreateProjectOperationResponse>(OperationInfoErrors.DependencyWithIdNotFound);
            //Find employerContract && project
            EmployerContract? employerContract = null;
            Project? project = null;
            if (request.EmployerContractId is not null && request.ProjectId is not null)
            {
                var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery((long)request.ProjectId), ct);
                if (getProject.IsFailure || getProject.Value is null)
                    return Result.Failure<CreateProjectOperationResponse>(ProjectErrors.ProjectWithIdNotFound);
                ///TODO Employers
                //var getContract = await _mediator.Send(new GetEmployerContractWithoutIncludeQuery((long)request.EmployerContractId!), ct);
                //if (getContract.IsFailure || getContract.Value is null)
                //    return Result.Failure<CreateProjectOperationResponse>(EContractErrors.EmployerContractWithIdNotFound);
                //if (getContract.Value.EmployerContractHead.Project.Id != getProject.Value.Id)
                //    return Result.Failure<CreateProjectOperationResponse>(ProjectOperationErrors.SelectValidateData);
                //employerContract = getContract.Value;
                project = getProject.Value;
            }
            else if (request.EmployerContractId is not null && request.ProjectId is null)
            {
                ///TODO Employers
                //var getContract = await _mediator.Send(new GetEmployerContractWithoutIncludeQuery((long)request.EmployerContractId!), ct);
                //if (getContract.IsFailure || getContract.Value is null)
                //    return Result.Failure<CreateProjectOperationResponse>(EContractErrors.EmployerContractWithIdNotFound);
                //employerContract = getContract.Value;
                //project = getContract.Value.EmployerContractHead.Project;
            }
            else if (request.EmployerContractId is null && request.ProjectId is not null)
            {
                var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery((long)request.ProjectId), ct);
                if (getProject.IsFailure || getProject.Value is null)
                    return Result.Failure<CreateProjectOperationResponse>(ProjectErrors.ProjectWithIdNotFound);
                project = getProject.Value;
            }
            else
                return Result.Failure<CreateProjectOperationResponse>(ProjectOperationErrors.SelectProject);

            //Find MeasureunitById
            var measureunitData = await _mediator.Send(new GetMeasureunitByIdQuery(request.UnitOfMeasurementId), ct);
            if (measureunitData.IsFailure)
                return Result.Failure<CreateProjectOperationResponse>(MetaDataErrors.MeasureunitWithIdNotFound!);

            //Find ForValidate
            var validator = await _mediator.Send(new GetProjectOperationForValidateQuery(project.Id, request.OperationInfoId, request.EmployerContractId, request.UnitOfMeasurementId), ct);
            if (validator is { IsSuccess: true, Value: not null })
                return Result.Failure<CreateProjectOperationResponse>(ProjectOperationErrors.IsExsist);

            long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
            if (companyId >= 1)
            {
                var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
                if (companyResponse.IsFailure)
                    return Result.Failure<CreateProjectOperationResponse>(companyResponse.Error!);
            }

            var newData = await _mediator.Send(new CreateProjectOperationCommand(
                project,
                operationInfo.Value!,
                employerContract,
                request.Workload,
                request.TolerancePercentage,
                request.Price,
                request.Priority,
                request.UnitOfMeasurementId,
                request.ProjectOperationStatus,
                request.GoodsInProgress,
                request.BaselineStartDate,
                request.BaselineFinishDate,
                request.BaselineDuration,
                request.Description,
                request.Urls,
                companyId), ct);
            if (newData.IsFailure)
                return Result.Failure<CreateProjectOperationResponse>(newData.Error!);

            await _unitOfWork.CommitAsync(ct);

            transaction.Complete();
            return new CreateProjectOperationResponse(newData.Value!.Id);
        }
    }

    public async Task<Result<CreatesProjectOperationResponse?>> CreatesProjectOperation(
        CreatesProjectOperationRequest request, CT ct)
    {
        using (var transaction = new CreateTransaction())
        {
            _logger.LogInformation("Request for CreatesProjectOperation");
            //Validate data
            var isValidRequest = await request.IsValidAsync<CreatesProjectOperationValidator, CreatesProjectOperationRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<CreatesProjectOperationResponse>(isValidRequest.Error!);

            var projectOperationModels = request.ProjectOperationModel.OrderBy(x => x.IsDeleted == false && x.Id != null).ThenByDescending(x => x.IsDeleted).ToList();
            foreach (var item in projectOperationModels)
            {
                if (item.Priority <= 0)
                    return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.PriorityCanNot0);
                if (item.IsDeleted)
                {
                    if (item.Id is null || item.Id == 0)
                        return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.IdIsEmptyForDelete);

                    var response = await _mediator.Send(new DeleteProjectOperationCommand((long)item.Id), ct);
                    if (response.IsFailure)
                        return Result.Failure<CreatesProjectOperationResponse>(response.Error!);

                    continue;
                }

                //Find employerContract && project
                EmployerContract? employerContract = null;
                Project? project = null;
                if (item.EmployerContractId is not null && item.ProjectId is not null)
                {
                    var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery((long)item.ProjectId), ct);
                    if (getProject.IsFailure || getProject.Value is null)
                        return Result.Failure<CreatesProjectOperationResponse>(ProjectErrors.ProjectWithIdNotFound);
                    ///TODO Employers
                    //var getContract = await _mediator.Send(new GetEmployerContractWithoutIncludeQuery((long)item.EmployerContractId!), ct);
                    //if (getContract.IsFailure || getContract.Value is null)
                    //    return Result.Failure<CreatesProjectOperationResponse>(EContractErrors.EmployerContractWithIdNotFound);
                    //if (getContract.Value.EmployerContractHead.Project.Id != getProject.Value.Id)
                    //    return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.SelectValidateData);
                    //employerContract = getContract.Value;
                    project = getProject.Value;
                }
                else if (item.EmployerContractId is not null && item.ProjectId is null)
                {
                    ///TODO Employers
                    //var getContract = await _mediator.Send(new GetEmployerContractWithoutIncludeQuery((long)item.EmployerContractId!), ct);
                    //if (getContract.IsFailure || getContract.Value is null)
                    //    return Result.Failure<CreatesProjectOperationResponse>(EContractErrors.EmployerContractWithIdNotFound);
                    //employerContract = getContract.Value;
                    //project = getContract.Value.EmployerContractHead.Project;
                }
                else if (item.EmployerContractId is null && item.ProjectId is not null)
                {
                    var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery((long)item.ProjectId), ct);
                    if (getProject.IsFailure || getProject.Value is null)
                        return Result.Failure<CreatesProjectOperationResponse>(ProjectErrors.ProjectWithIdNotFound);
                    project = getProject.Value;
                }
                else
                    return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.SelectProject);

                //Find MeasureunitById
                var measureunitData = await _measureUnitRepository.GetByIdAsync(item.UnitOfMeasurementId, ct);
                if (measureunitData is null)
                    return Result.Failure<CreatesProjectOperationResponse>(MetaDataErrors.MeasureunitWithIdNotFound!);

                long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
                if (companyId >= 1)
                {
                    var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
                    if (companyResponse.IsFailure)
                        return Result.Failure<CreatesProjectOperationResponse>(companyResponse.Error!);
                }

                if (item.Id is not null && item.Id != default)
                {
                    var projectOperationResponse = await _mediator.Send(new GetProjectOperationForUpdateQuery((long)item.Id), ct);
                    if (projectOperationResponse.IsFailure)
                        return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
                    var projectOperationValue = projectOperationResponse.Value!;

                    //Find ForValidate
                    var validator = await _mediator.Send(new GetProjectOperationForValidateQuery(project.Id, item.OperationInfoId, item.EmployerContractId, item.UnitOfMeasurementId), ct);
                    if (validator is { IsSuccess: true, Value: not null } && item.Id != validator.Value.Id)
                        return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.IsExsist);

                    if (item.ProjectOperationStatus is not null)
                        if (item.ProjectOperationStatus != projectOperationValue!.ProjectOperationStatus)
                        {
                            var statusChanger = await _mediator.Send(new ProjectOperationStatusChangerCommand((long)item.Id!, (ProjectOperationStatus)item.ProjectOperationStatus!), ct);
                            if (statusChanger.IsFailure)
                                return Result.Failure<CreatesProjectOperationResponse>(statusChanger.Error!);
                        }

                    if (item.OperationInfoId != projectOperationValue!.OperationInfo.Id)
                    {
                        //Find new OperationInfo
                        var operationInfoResponse = await _mediator.Send(new GetOperationInfoByIdByConsumptionStandardsQuery(item.OperationInfoId), ct);
                        if (operationInfoResponse.IsFailure)
                            return Result.Failure<CreatesProjectOperationResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
                        var operationInfoNew = operationInfoResponse.Value!;

                        var requestGoodsSupplyDetailsResponse = await _mediator.Send(new GetsGoodsSupplyDetailByProjectOperationIdQuery(projectOperationValue.Id), ct);
                        var requestGoodsSupplyDetails = requestGoodsSupplyDetailsResponse.Value?.Data;
                        if (requestGoodsSupplyDetails is not null && requestGoodsSupplyDetails.Count > 0)
                        {
                            var supplyProducts = requestGoodsSupplyDetails
                                .Select(x => x.ConsumableVolumeProduct).Where(x => x.IsStandard == true).Distinct().ToList();

                            List<ConsumptionStandardProduct>? productGroups = [];
                            var consumptionStandardProducts = operationInfoNew.ConsumptionStandardProduct.ToList();
                            if (consumptionStandardProducts is not null && consumptionStandardProducts.Count > 0)
                                foreach (var consumptionStandardProduct in consumptionStandardProducts)
                                    if (supplyProducts.Any(x => x.ProductGroupId == consumptionStandardProduct.ProductUnitId))
                                        productGroups.Add(consumptionStandardProduct);

                            foreach (var productGroup in productGroups)
                            {
                                var supplyProduct = supplyProducts.FirstOrDefault(x => x.ProductGroupId == productGroup.ProductUnitId);
                                if (productGroup.Number * projectOperationValue.Workload < supplyProduct?.FinalValue)
                                    return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.OperationInfoCanNotChangedForValue);
                            }
                        }

                        var canCreate = await _mediator.Send(new GetProjectOperationForValidatingQuery(item.OperationInfoId, project.Id, item.UnitOfMeasurementId), ct);
                        if (canCreate.Value == true)
                            return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.OperationInfoCanNotChanged2);

                        var projectOperationDetailsResponse = await _mediator.Send(new GetsByProjectOperationIdForVolumesQuery(projectOperationValue.Id), ct);
                        var projectOperationDetails = projectOperationDetailsResponse.Value?.Data;
                        var changingResponse = await OperationInfoChanger(projectOperationValue.OperationInfo, operationInfoNew!, projectOperationValue.Id, projectOperationDetails, ct);
                        if (!changingResponse)
                            return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.OperationInfoChangedFailed);

                        var projectOperationDetailsContractorService = await _mediator.Send(new GetsContractorServiceByProjectOperationIdQuery(item.Id!.Value), ct);
                        var contractorServices = projectOperationDetailsContractorService.Value?.Data?.ToList();
                        if (contractorServices is not null && contractorServices.Count > 0)
                        {
                            var operationInfoServicesResponse = await _mediator.Send(new GetsByOperationInfoIdIncludelessQuery(item.OperationInfoId), ct);
                            var operationInfoServices = operationInfoServicesResponse.Value?.Data?.ToList();
                            if (operationInfoServices is not null)
                            {
                                var serviceData = contractorServices.FirstOrDefault(x => x.OperationInfoService.ServiceInfo.Id == 23607 && x.OperationInfoService.ServiceInfo.ServiceInfoCode == "001");
                                if (serviceData is not null)
                                    contractorServices.Remove(serviceData);

                                if (contractorServices.Any(x => operationInfoServices.All(z => z.ServiceInfo.Id != x.OperationInfoService.ServiceInfo.Id)))
                                    return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.OperationInfoChangedFailedForService);

                                var updateContractorService = await _mediator.Send(new UpdateContractorServiceServiceInfoCommand(contractorServices, operationInfoServices), ct);
                                if (updateContractorService.Value == false)
                                    return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.OperationInfoChangedFailedForService);
                            }
                            else
                                return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.OperationInfoChangedFailedForService);
                        }

                        var updateData = await _mediator.Send(new UpdateProjectOperationCommand(
                            projectOperationValue!,
                            project,
                            operationInfoNew,
                            employerContract,
                            item.Workload,
                            item.TolerancePercentage,
                            item.Price,
                            item.ChangedPrice ?? 0,
                            item.Priority,
                            item.UnitOfMeasurementId,
                            item.GoodsInProgress,
                            item.Description,
                            item.Urls,
                            companyId), ct);
                        if (updateData.IsFailure)
                            return Result.Failure<CreatesProjectOperationResponse>(updateData.Error!);
                    }
                    else
                    {
                        var updateData = await _mediator.Send(new UpdateProjectOperationCommand(
                            projectOperationValue!,
                            project,
                            projectOperationValue.OperationInfo,
                            employerContract,
                            item.Workload,
                            item.TolerancePercentage,
                            item.Price,
                            item.ChangedPrice ?? 0,
                            item.Priority,
                            item.UnitOfMeasurementId,
                            item.GoodsInProgress,
                            item.Description,
                            item.Urls,
                            companyId), ct);
                        if (updateData.IsFailure)
                            return Result.Failure<CreatesProjectOperationResponse>(updateData.Error!);
                    }
                }
                else
                {
                    //Find ForValidate
                    var validator = await _mediator.Send(new GetProjectOperationForValidateQuery(project.Id, item.OperationInfoId, item.EmployerContractId, item.UnitOfMeasurementId), ct);
                    if (validator is { IsSuccess: true, Value: not null } && !validator.Value.IsDeleted)
                        return Result.Failure<CreatesProjectOperationResponse>(ProjectOperationErrors.IsExsist);

                    var operationInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(item.OperationInfoId), ct);
                    if (operationInfo.IsFailure)
                        return Result.Failure<CreatesProjectOperationResponse>(OperationInfoErrors.DependencyWithIdNotFound);

                    int? baselineDuration =
                        item.BaselineStartDate.HasValue && item.BaselineFinishDate.HasValue
                        ? (item.BaselineFinishDate.Value - item.BaselineStartDate.Value).Days
                        : null;

                    var status = item.ProjectOperationStatus is null ? ProjectOperationStatus.NotStarted : (ProjectOperationStatus)item.ProjectOperationStatus!;
                    var newData = await _mediator.Send(new CreateProjectOperationCommand(
                        project,
                        operationInfo.Value!,
                        employerContract,
                        item.Workload,
                        item.TolerancePercentage,
                        item.Price,
                        item.Priority,
                        item.UnitOfMeasurementId,
                        status,
                        item.GoodsInProgress,
                        item.BaselineStartDate,
                        item.BaselineFinishDate,
                        item.BaselineDuration,
                        item.Description,
                        item.Urls,
                        companyId), ct);
                    if (newData.IsFailure)
                        return Result.Failure<CreatesProjectOperationResponse>(newData.Error!);

                    await _unitOfWork.CommitAsync(ct);

                    var value = newData.Value!;
                }
            }

            await _unitOfWork.CommitAsync(ct);
            transaction.Complete();
            return new CreatesProjectOperationResponse(true);
        }
    }

    public async Task<Result<ProjectOperationExcelImportsResponse>> ProjectOperationExcelImports(
    ProjectOperationExcelImportsRequest request, CT ct)
    {
        var importedRows = ExcelImporter.Import<ProjectOperationExcelImportsModel>(request.DocumentFile);
        if (importedRows is null || !importedRows.Any())
            return Result.Failure<ProjectOperationExcelImportsResponse>(GlobalErrors.ErrorOnReadFile)!;

        var rows = importedRows
            .Where(r => !string.IsNullOrWhiteSpace(r.OperationInfoCode))
            .Where(r => r.OperationInfoCode != "کد عملیات" && r.OperationInfoCode != "OperationInfoCode")
            .ToList();

        if (!rows.Any())
            return Result.Failure<ProjectOperationExcelImportsResponse>(GlobalErrors.ErrorOnReadFile)!;

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<ProjectOperationExcelImportsResponse>(GlobalErrors.InvalidCompany)!;

        var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery(request.ProjectId), ct);
        if (getProject.IsFailure || getProject.Value is null)
            return Result.Failure<ProjectOperationExcelImportsResponse>(ProjectErrors.ProjectWithIdNotFound);

        var codesInFile = rows.Select(x => x.OperationInfoCode).ToList();
        var distinctCodesInFile = codesInFile.Distinct().ToList();
        var duplicatesInFile = codesInFile.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key).ToHashSet();

        var existingCodesResponse = await _mediator.Send(
            new GetExistingOperationInfoCodesInProjectQuery(request.ProjectId, distinctCodesInFile), ct);
        var existingCodesInProject = existingCodesResponse.Value?.ToHashSet() ?? new HashSet<string>();

        var oInfosResponse = await _mediator.Send(new GetOperationInfosByCodesQuery(distinctCodesInFile, companyId), ct);
        var foundOInfos = oInfosResponse.Value ?? new List<OperationInfoBulkDto>();

        var operationInfoMap = foundOInfos
            .OrderBy(x => x.Id)
            .GroupBy(x => x.OperationInfoCode)
            .ToDictionary(
                g => g.Key,
                g => (g.First().Id, g.First().UnitOfMeasurementId));

        var invalidCodes = distinctCodesInFile.Except(operationInfoMap.Keys).ToHashSet();

        // Call the extracted private validation method
        var (rowErrors, validRows) = ValidateExcelImportRows(rows, invalidCodes, duplicatesInFile, existingCodesInProject, operationInfoMap);

        if (rowErrors.Any())
        {
            var errorModels = rows.Select((r, i) => new ProjectOperationImportErrorModel
            {
                OperationInfoCode = r.OperationInfoCode,
                TolerancePercentage = r.TolerancePercentage,
                Workload = r.Workload,
                StartDate = r.StartDate,
                FinishDate = r.FinishDate,
                Error = rowErrors.ContainsKey(i) ? rowErrors[i] : ""
            }).ToList();

            return Result.Success(new ProjectOperationExcelImportsResponse(IsDone: false, ErrorModels: errorModels));
        }

        var models = new List<CreatesProjectOperationModel>();
        int priority = 1;

        foreach (var (index, row, startDate, finishDate, oInfoId, unitId) in validRows)
        {
            int? duration = (startDate.HasValue && finishDate.HasValue) ? (finishDate.Value - startDate.Value).Days : null;

            models.Add(new CreatesProjectOperationModel(
                Id: null, ProjectId: request.ProjectId, OperationInfoId: oInfoId, EmployerContractId: null,
                Workload: row.Workload, TolerancePercentage: row.TolerancePercentage, Price: null, BasePrice: 0,
                ChangedPrice: null, Priority: priority++, UnitOfMeasurementId: unitId, ProjectOperationStatus: null,
                GoodsInProgress: false, BaselineStartDate: startDate, BaselineFinishDate: finishDate,
                BaselineDuration: duration, Description: null, Urls: null, IsDeleted: false
            ));
        }

        var createsRequest = new CreatesProjectOperationRequest(models);
        var result = await CreatesProjectOperation(createsRequest, ct);

        if (result.IsFailure)
            return Result.Failure<ProjectOperationExcelImportsResponse>(result.Error!);

        return Result.Success(new ProjectOperationExcelImportsResponse(true));
    }

    public async Task<Result<CreateProjectOperationActionsResponse>> CreateProjectOperationActions(
        CreateProjectOperationActionsRequest request,
        CT ct)
    {
        var pOperation = await _mediator.Send(new GetProjectOperationByIdQuery(request.ProjectOperationId), ct);
        if (pOperation.IsBad() || pOperation.Value == null)
            return pOperation.Failure<CreateProjectOperationActionsResponse>();
        var oInfoactions = await _operationInfoActionRepository.GetOIActionByOIId(pOperation.Value.OperationInfoId, ct);
        if (oInfoactions == null)
            return Result.Failure<CreateProjectOperationActionsResponse>(OperationInfoErrors.OperationInfoActionWithIdNotFound)!;
        var result = await CreateProjectOperationActionsHandler(request, pOperation.Value, ct);
        if (result.IsBad())
            return result.Failure<CreateProjectOperationActionsResponse>();

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectOperationActionsResponse(true);
    }

    public async Task<Result<DeleteProjectOperationActionsResponse>> DeleteProjectOperationActions(
        DeleteProjectOperationActionsRequest request,
        CT ct)
    {
        var delete = await DeleteProjectOperationActionsHandler(request.ProjectOperationActionIds, ct);
        if (delete.IsBad())
            return delete.Failure<DeleteProjectOperationActionsResponse>();
        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectOperationActionsResponse(true);
    }

    public async Task<Result<ChangeProjectResponse>> ChangeProject(
        ChangeProjectRequest request,
        CT ct)
    {
        var change = await ChangeProject(request.Id, request.ProjectId, ct);
        if (change.IsBad())
            return change.Failure<ChangeProjectResponse>();
        await _unitOfWork.CommitAsync(ct);
        return new ChangeProjectResponse(true);
    }

    public async Task<Result<UpdateProjectOperationResponse?>> UpdateProjectOperation(
        UpdateProjectOperationRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectOperation, id:{Id}", request.Id);
        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationValidator, UpdateProjectOperationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        var value = response.Value!;

        if (request!.Workload <= value.Workload)
            if (!Workloader(value, request!.Workload))
                return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationDetailErrors.FinalAmountIsBiggerThanWorkLoad);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateProjectOperationResponse>(companyResponse.Error!);
        }

        EmployerContract? employerContract = null;
        Project? project = null;
        if (request.EmployerContractId is not null && request.ProjectId is not null)
        {
            var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery((long)request.ProjectId), ct);
            if (getProject.IsFailure || getProject.Value is null)
                return Result.Failure<UpdateProjectOperationResponse>(ProjectErrors.ProjectWithIdNotFound);
            ///TODO Employers
            //var getContract = await _mediator.Send(new GetEmployerContractWithoutIncludeQuery((long)request.EmployerContractId!), ct);
            //if (getContract.IsFailure || getContract.Value is null)
            //    return Result.Failure<UpdateProjectOperationResponse>(EContractErrors.EmployerContractWithIdNotFound);
            //if (getContract.Value.EmployerContractHead.Project.Id != getProject.Value.Id)
            //    return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.SelectValidateData);
            //employerContract = getContract.Value;
            project = getProject.Value;
        }
        else if (request.EmployerContractId is not null && request.ProjectId is null)
        {
            ///TODO Employers
            //var getContract = await _mediator.Send(new GetEmployerContractWithoutIncludeQuery((long)request.EmployerContractId!), ct);
            //if (getContract.IsFailure || getContract.Value is null)
            //    return Result.Failure<UpdateProjectOperationResponse>(EContractErrors.EmployerContractWithIdNotFound);
            //employerContract = getContract.Value;
            //project = getContract.Value.EmployerContractHead.Project;
        }
        else if (request.EmployerContractId is null && request.ProjectId is not null)
        {
            var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery((long)request.ProjectId), ct);
            if (getProject.IsFailure || getProject.Value is null)
                return Result.Failure<UpdateProjectOperationResponse>(ProjectErrors.ProjectWithIdNotFound);
            project = getProject.Value;
        }
        else
            return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.SelectProject);

        var measureunitData = await _mediator.Send(new GetMeasureunitByIdQuery(request.UnitOfMeasurementId), ct);
        if (measureunitData.IsFailure)
            return Result.Failure<UpdateProjectOperationResponse>(MetaDataErrors.MeasureunitWithIdNotFound!);

        var validator = await _mediator.Send(new GetProjectOperationForValidateQuery(project.Id, request.OperationInfoId, request.EmployerContractId, request.UnitOfMeasurementId), ct);
        if (validator is { IsSuccess: true, Value: not null } && validator.Value.Id != request.Id)
            return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.IsExsist);

        ///TODO Employers
        //if (employerContract is not null)
        //{
        //    if (value.EmployerContract is not null && employerContract.Id != value.EmployerContract.Id)
        //    {
        //        if (value!.ProjectOperationDetails.Any())
        //        {
        //            var projectOperationDetails = value.ProjectOperationDetails.Where(x => !x.IsDeleted).ToList();
        //            if (projectOperationDetails.Any())
        //            {
        //                var limitStartDate = projectOperationDetails.Where(x => x.StartDate.HasValue).OrderByDescending(x => x.StartDate).FirstOrDefault();
        //                if (limitStartDate is not null)
        //                    if (limitStartDate.StartDate!.Value.Date < employerContract.StartDate!.Value.Date)
        //                        return Result.Failure<UpdateProjectOperationResponse>(EContractErrors.CanNotAssainge);
        //            }
        //        }
        //    }
        //}

        if (project.Contractual)
            if (request.Workload != value.Workload)
                return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.WorkloadCanNotChanged);

        OperationInfo? operationInfo = null;
        if (request.OperationInfoId != value.OperationInfo.Id)
        {
            List<ProjectOperationDetail> projectOperationDetails = new();
            var detialIds = value.ProjectOperationDetails.Select(x => x.Id).ToList();
            if (detialIds.Count > 0)
            {
                var details = await _mediator.Send(new GetsMinimalByProjectOperationDetialIdsQuery(detialIds, 1, detialIds.Count), ct);
                if (details.Value is not null && details.Value.Data is not null && (details.Value.Data.Any(x => x.DailyOperations.Any()) || details.Value.Data.Any(x => x.RequestGoodsSupplies.Any())))
                    return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.OperationInfoCanNotChanged);
                else
                    projectOperationDetails = details.Value!.Data!;
            }

            var operationInfoResponse = await _mediator.Send(new GetOperationInfoByIdQuery(request.OperationInfoId), ct);
            if (operationInfoResponse.IsFailure)
                return Result.Failure<UpdateProjectOperationResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
            operationInfo = operationInfoResponse.Value;

            if (operationInfo!.ProjectOperations.Any(x => x.Project.Id.Equals(project.Id) && x.UnitOfMeasurementId.Equals(request.UnitOfMeasurementId)))
                return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.OperationInfoCanNotChanged2);

            var changingResponse = await OperationInfoChanger(value.OperationInfo, operationInfo!, value.Id, projectOperationDetails, ct);
            if (!changingResponse)
                return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.OperationInfoChangedFailed);

            var projectOperationDetailsContractorService = await _mediator.Send(new GetsContractorServiceByProjectOperationIdQuery(request.Id), ct);
            var contractorServices = projectOperationDetailsContractorService.Value?.Data?.ToList();
            if (contractorServices is not null && contractorServices.Count > 0)
            {
                var operationInfoServicesResponse = await _mediator.Send(new GetsByOperationInfoIdIncludelessQuery(request.OperationInfoId), ct);
                var operationInfoServices = operationInfoServicesResponse.Value?.Data?.ToList();
                if (operationInfoServices is not null)
                {
                    if (contractorServices.Any(x => operationInfoServices.All(z => z.ServiceInfo.Id != x.OperationInfoService.ServiceInfo.Id)))
                        return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.OperationInfoChangedFailedForService);

                    var updateContractorService = await _mediator.Send(new UpdateContractorServiceServiceInfoCommand(contractorServices, operationInfoServices), ct);
                    if (updateContractorService.Value == false)
                        return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.OperationInfoChangedFailedForService);
                }
                else
                    return Result.Failure<UpdateProjectOperationResponse>(ProjectOperationErrors.OperationInfoChangedFailedForService);
            }
        }
        else
            operationInfo = value.OperationInfo;

        var updateData = await _mediator.Send(new UpdateProjectOperationCommand(
            value,
            project,
            operationInfo,
            employerContract,
            request.Workload,
            request.TolerancePercentage,
            request.Price,
            request.ChangedPrice,
            request.Priority,
            request.UnitOfMeasurementId,
            request.GoodsInProgress,
            request.Description,
            request.Urls,
            companyId), ct);
        if (updateData.IsFailure)
            return Result.Failure<UpdateProjectOperationResponse>(updateData.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationResponse(value.Id);
    }

    public async Task<Result<UpdateProjectOperationPriceResponse?>> UpdateProjectOperationPrice(
        UpdateProjectOperationPriceRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectOperationPrice, id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationPriceValidator, UpdateProjectOperationPriceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationPriceResponse>(isValidRequest.Error!);
        //Find projectOperation
        var projectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(request.Id), ct);
        if (projectOperation.IsFailure)
            return Result.Failure<UpdateProjectOperationPriceResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

        var response = await _mediator.Send(new UpdateProjectOperationPriceCommand(projectOperation.Value!, request.Price), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateProjectOperationPriceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationPriceResponse(response.Value!.Id, response.Value.Price);
    }

    public async Task<Result<UpdateProjectOperationDocumentsResponse?>> UpdateProjectOperationDocuments(
        UpdateProjectOperationDocumentsRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectOperationDocuments, id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationDocumentsValidator, UpdateProjectOperationDocumentsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationDocumentsResponse>(isValidRequest.Error!);
        //Find projectOperation
        var projectOperation = await _mediator.Send(new GetProjectOperationDocumentsQuery(request.Id), ct);
        if (projectOperation.IsFailure)
            return Result.Failure<UpdateProjectOperationDocumentsResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

        var response = await _mediator.Send(new UpdateProjectOperationDocumentsCommand(projectOperation.Value!, request.Urls), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateProjectOperationDocumentsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationDocumentsResponse(true);
    }

    public async Task<Result<GroupProjectOperationStatusChangerResponse?>> GroupProjectOperationStatusChanger(
        GroupProjectOperationStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("Request for GroupProjectOperationStatusChanger, Status:{Status}", request.Status);

        var isValidRequest = await request.IsValidAsync<GroupProjectOperationStatusChangerValidator, GroupProjectOperationStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GroupProjectOperationStatusChangerResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new ProjectOperationStatusChangerCommand(item, request.Status), ct);
            if (response.IsFailure)
                return Result.Failure<GroupProjectOperationStatusChangerResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new GroupProjectOperationStatusChangerResponse(true);
    }

    public async Task<Result<ProjectOperationStatusChangerResponse?>> ProjectOperationStatusChanger(
        ProjectOperationStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationStatusChanger, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ProjectOperationStatusChangerValidator, ProjectOperationStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationStatusChangerResponse>(isValidRequest.Error!);

        if (request.Status == ProjectOperationStatus.Doing || request.Status == ProjectOperationStatus.EndOfWork)
        {
            var checkDependency = await _mediator.Send(new ValidateProjectOperationDependencyQuery(request.Id, DateTime.Now, null, null, null), ct);
            if (checkDependency.IsBad())
                return checkDependency.Failure<ProjectOperationStatusChangerResponse?>();
        }

        var response = await _mediator.Send(new ProjectOperationStatusChangerCommand(request.Id, request.Status), ct);
        if (response.IsFailure)
            return Result.Failure<ProjectOperationStatusChangerResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ProjectOperationStatusChangerResponse(response.Value!.Id);
    }

    public async Task<Result<DeleteProjectOperationResponse?>> DeleteProjectOperation(
        DeleteProjectOperationRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteProjectOperation, Id:{ProjectOperationId}", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteProjectOperationValidator, DeleteProjectOperationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteProjectOperationResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteProjectOperationCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteProjectOperationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectOperationResponse(response.Value!.Id, true);
    }

    public async Task<Result<ProjectOperationGroupDeleteResponse?>> ProjectOperationGroupDelete(
        ProjectOperationGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ProjectOperationGroupDeleteValidator, ProjectOperationGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DeleteProjectOperationCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<ProjectOperationGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ProjectOperationGroupDeleteResponse(true);
    }

    public async Task<Result<GetProjectOperationByIdResponse?>> GetProjectOperationById(
        GetProjectOperationByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationById, id:{ProjectOperationId}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetProjectOperationByIdValidator, GetProjectOperationByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectOperationByIdResponse>(response.Error!);
        var value = response.Value!;

        var measureUnits = await WebServicesLogic.MeasurementDataReceiver([value.UnitOfMeasurementId], _mediator, ct);
        var modelingData = FullModeling(value, measureUnits, null);

        return modelingData.Adapt<GetProjectOperationByIdResponse>();
    }

    public async Task<Result<GetPOActionByPOIdResponse?>> GetPOActionByPOId(
        GetPOActionByPOIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetPOActionByPOId, id:{ProjectOperationId}", request.ProjectOperationId);

        var result = await GetPOActionByPOIdQuery(request, ct);
        if (result.IsBad())
            return result.Failure<GetPOActionByPOIdResponse>()!;
        return result;
    }

    public async Task<Result<GetProjectOperationByParamsResponse?>> GetProjectOperationByParams(
        GetProjectOperationByParamsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationByParams, ProjectId:{ProjectId}", request.ProjectId);

        var isValidRequest = await request.IsValidAsync<GetProjectOperationByParamsValidator, GetProjectOperationByParamsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationByParamsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationByParamsQuery(request.ProjectId, request.OperationInfoId, request.EmployerContractId, request.MeasurementId), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectOperationByParamsResponse>(response.Error!);
        var value = response.Value!;

        var measureUnits = await WebServicesLogic.MeasurementDataReceiver([value.UnitOfMeasurementId], _mediator, ct);
        var modelingData = FullModeling(value, measureUnits, null);

        return modelingData.Adapt<GetProjectOperationByParamsResponse>();
    }

    public async Task<Result<GetProjectOperationDocumentsResponse?>> GetProjectOperationDocuments(
        GetProjectOperationDocumentsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationDocuments, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetProjectOperationDocumentsValidator, GetProjectOperationDocumentsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationDocumentsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationDocumentsQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectOperationDocumentsResponse>(response.Error!);
        var value = response.Value!;

        return new GetProjectOperationDocumentsResponse(value.Id, value.ProjectOperationDocuments.Select(x => x.Url).ToList());
    }

    public async Task<Result<GetsByEmployerContractResponse?>> GetsByEmployerContract(
        GetsByEmployerContractRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsByEmployerContractId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByEmployerContractValidator, GetsByEmployerContractRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByEmployerContractResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsByEmployerContractQuery(request.EmployerContractId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsByEmployerContractResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var dataList = new List<GetsEmployerContractModel>();
        foreach (var item in values!)
        {
            var modelingData = FullModeling(item, measureUnits, null);
            dataList.Add(modelingData.Adapt<GetsEmployerContractModel>());
        }

        return new GetsByEmployerContractResponse(dataList ?? new List<GetsEmployerContractModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByProjectResponse?>> GetsByProject(
        GetsByProjectRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsByProjectId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByProjectValidator, GetsByProjectRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByProjectResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsByProjectQuery(request.ProjectId, request.CategoryId, request.BranchId,
            request.SeasonId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure || getsResponse.Value is null || getsResponse.Value.Data is null)
            return Result.Failure<GetsByProjectResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        List<long>? measureunitIds = [];
        var measureIds = values?.Where(x => x.MeasurementId > 0).Select(x => x.MeasurementId).ToList();
        var operationMeasureIds = values?.Where(x => x.OperationInfoMeasurementId > 0).Select(x => x.OperationInfoMeasurementId!).ToList();
        measureunitIds?.AddRange(measureIds ?? []);
        measureunitIds?.AddRange(operationMeasureIds ?? []);
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureunitIds?.Distinct().ToList(), _mediator, ct);

        foreach (var item in values)
        {
            var measure = measureUnits?.FirstOrDefault(x => x.Id == item.MeasurementId);
            item.MeasurementName = measure?.Name;
            var oImeasure = measureUnits?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurementId);
            item.OperationInfoMeasurementName = oImeasure?.Name;
        }

        return new GetsByProjectResponse(values ?? new List<GetsProjectOperationByProjectModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredForReportsResponse?>> GetsFilteredForReports(
        GetsFilteredForReportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsFilteredForReportsId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);
        var isValidRequest = await request.IsValidAsync<GetsFilteredForReportsValidator, GetsFilteredForReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredForReportsResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsFilteredForReportsQuery(request.CostCenterIds, request.ProjectIds, request.FilterData,
            request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure || getsResponse.Value is null || getsResponse.Value.Data is null)
            return Result.Failure<GetsFilteredForReportsResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        List<long>? measureunitIds = [];
        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).ToList();
        measureunitIds?.AddRange(values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList() ?? []);
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureunitIds?.Distinct().ToList(), _mediator, ct);

        foreach (var item in values!)
            item.MeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.UnitOfMeasurementId)?.Name;

        return new GetsFilteredForReportsResponse(values ?? new List<GetsFilteredForReportsModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetFltrPOForReportsResponse?>> GetFltrPOForReports(
        GetFltrPOForReportsRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrPOForReports");

        var getsResponse = await GetFltrPOForReportsQuery(request, ct);
        if (getsResponse.IsFailure || getsResponse.Value is null || getsResponse.Value.Data is null)
            return Result.Failure<GetFltrPOForReportsResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        List<long>? measureunitIds = [];
        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).ToList();
        measureunitIds?.AddRange(values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList() ?? []);
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureunitIds?.Distinct().ToList(), _mediator, ct);

        foreach (var item in values!)
            item.MeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.UnitOfMeasurementId)?.Name;

        return new GetFltrPOForReportsResponse(values ?? new List<GetFltrPOForReportsModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredByProjectResponse?>> GetsFilteredByProject(
        GetsFilteredByProjectRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsFilteredByProjectId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsFilteredByProjectValidator, GetsFilteredByProjectRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredByProjectResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsFilteredByProjectQuery(request.ProjectId, request.CategoryId, request.BranchId,
            request.SeasonId, request.ContractorIds, request.StartDate, request.EndDate, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize),
            ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsFilteredByProjectResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        List<long>? measureunitIds = [];
        var measureIds = values?.Where(x => x.MeasurementId > 0).Select(x => x.MeasurementId).ToList();
        var operationMeasureIds = values?.Where(x => x.OperationInfoMeasurementId > 0).Select(x => x.OperationInfoMeasurementId!).ToList();
        measureunitIds?.AddRange(measureIds ?? []);
        measureunitIds?.AddRange(operationMeasureIds ?? []);
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureunitIds?.Distinct().ToList(), _mediator, ct);

        foreach (var item in values)
        {
            var measure = measureUnits?.FirstOrDefault(x => x.Id == item.MeasurementId);
            item.MeasurementName = measure?.Name;
            var oImeasure = measureUnits?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurementId);
            item.OperationInfoMeasurementName = oImeasure?.Name;
        }

        return new GetsFilteredByProjectResponse(values ?? new List<GetsProjectOperationByProjectModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationByProjectIdsResponse?>> GetsProjectOperationByProjectIds(
        GetsProjectOperationByProjectIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsProjectOperationByProjectIdsId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationByProjectIdsValidator, GetsProjectOperationByProjectIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationByProjectIdsResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsProjectOperationByProjectIdsQuery(
            request.ProjectIds,
            request.ContractorIds,
            request.CategoryId,
            request.BranchId,
            request.SeasonId,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsProjectOperationByProjectIdsResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var dataList = new List<GetsProjectOperationByProjectModel>();
        foreach (var item in values!)
        {
            var modelingData = FullModeling(item, measureUnits, null);
            dataList.Add(modelingData.Adapt<GetsProjectOperationByProjectModel>());
        }

        return new GetsProjectOperationByProjectIdsResponse(dataList ?? new List<GetsProjectOperationByProjectModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredByProjectIdsResponse?>> GetsFilteredByProjectIds(
        GetsFilteredByProjectIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for FilteredGetsFilteredByProjectIdsId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsFilteredByProjectIdsValidator, GetsFilteredByProjectIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredByProjectIdsResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsFilteredByProjectIdsQuery(request.ProjectIds, request.NotShowProjectOperationIds, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsFilteredByProjectIdsResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        measureIds?.AddRange(values?.Where(x => x.OperationInfo.UnitOfMeasurementId > 0).Select(x => x.OperationInfo.UnitOfMeasurementId).Distinct().ToList() ?? []);
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds?.Distinct().ToList(), _mediator, ct);

        var dataList = new List<GetsFilteredByProjectIdsModel>();
        dataList = values.Adapt<List<GetsFilteredByProjectIdsModel>>();
        foreach (var item in dataList!)
        {
            item.OperationInfoMeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurementId)?.Name;
            item.MeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.MeasurementId)?.Name;
        }

        return new GetsFilteredByProjectIdsResponse(dataList ?? new List<GetsFilteredByProjectIdsModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsPrioritizeProjectOperationResponse?>> GetsPrioritizeProjectOperation(
        GetsPrioritizeProjectOperationRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsPrioritizeProjectOperationId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsPrioritizeProjectOperationValidator, GetsPrioritizeProjectOperationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsPrioritizeProjectOperationResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var getsResponse = await _mediator.Send(new GetsPrioritizeProjectOperationQuery(request.FilterData, request.Id, request.Priority, companyId, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsPrioritizeProjectOperationResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var dataList = new List<GetsPrioritizeProjectOperationResponseModel>();
        foreach (var item in values!)
        {
            dataList.Add(new GetsPrioritizeProjectOperationResponseModel(item.Id, item.OperationInfo.Id, item.OperationInfo.OperationInfoName, item.OperationInfo.OperationInfoCode, item.Priority));
        }

        return new GetsPrioritizeProjectOperationResponse(dataList ?? new List<GetsPrioritizeProjectOperationResponseModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsForDailyProjectOperationsResponse?>> GetsForDailyProjectOperations(
        GetsForDailyProjectOperationsRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsForDailyProjectOperationsId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsForDailyProjectOperationsValidator, GetsForDailyProjectOperationsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsForDailyProjectOperationsResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsForDailyProjectOperationsQuery(request.ProjectId, request.Priority, request.FilterData,
            request.StartDate, request.EndDate, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsForDailyProjectOperationsResponse>(getsResponse.Error!);
        if (getsResponse.Value is null || getsResponse.Value.Data is null)
            return Result.Failure<GetsForDailyProjectOperationsResponse>(ProjectOperationErrors.ProjectOperationWithFilterNotFound);
        var values = getsResponse.Value?.Data!;

        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var dataList = new List<GetsForDailyProjectOperationsModel>();
        foreach (var item in values!)
        {
            var modelingData = FullModeling(item, measureUnits, null);
            dataList.Add(modelingData.Adapt<GetsForDailyProjectOperationsModel>());
        }

        return new GetsForDailyProjectOperationsResponse(dataList ?? new List<GetsForDailyProjectOperationsModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsSummaryProjectOperationResponse?>> GetsSummaryProjectOperation(
        GetsSummaryProjectOperationRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsSummaryProjectOperationId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsSummaryProjectOperationValidator, GetsSummaryProjectOperationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsSummaryProjectOperationResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsSummaryProjectOperationQuery(request.ProjectId, request.SeasonId, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsSummaryProjectOperationResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var dataList = new List<GetsSummaryProjectOperationModel>();
        foreach (var item in values!)
        {
            var modelingData = FullModeling(item, measureUnits, null);
            dataList.Add(modelingData.Adapt<GetsSummaryProjectOperationModel>());
        }

        return new GetsSummaryProjectOperationResponse(dataList ?? new List<GetsSummaryProjectOperationModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByProjectOperationIdResponse?>> GetsByProjectOperationId(
        GetsByProjectOperationIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectOperationId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByProjectOperationIdValidator, GetsByProjectOperationIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByProjectOperationIdResponse>(isValidRequest.Error!);

        var projectOperationValue = await _mediator.Send(new GetProjectOperationRealtionInfoQuery(request.ProjectOperationId), ct);
        if (projectOperationValue.IsFailure)
            return Result.Failure<GetsByProjectOperationIdResponse>(projectOperationValue.Error!);
        if (projectOperationValue.Value is null)
            return Result.Failure<GetsByProjectOperationIdResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        var projectOperation = projectOperationValue.Value;

        var getsResponse = await _mediator.Send(new GetsByProjectOperationIdQuery(projectOperation.Project.Id, projectOperation.Priority, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsByProjectOperationIdResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var value = getsResponse.Value?.Data?.Where(x => !x.Id.Equals(request.ProjectOperationId)).ToList();
        var dataList = new List<GetsByProjectOperationIdModel>();
        if (value is not null && value.Count > 0)
        {
            var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
            var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

            foreach (var item in values!)
            {
                var modelingData = FullModeling(item, measureUnits, null);
                dataList.Add(modelingData.Adapt<GetsByProjectOperationIdModel>());
            }
        }

        return new GetsByProjectOperationIdResponse(dataList ?? new List<GetsByProjectOperationIdModel>(0), getsResponse.Value?.RowCount - 1 ?? 0);
    }

    public async Task<Result<GetsWithoutContractResponse?>> GetsWithoutContract(
        GetsWithoutContractRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsWithoutContractId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsWithoutContractValidator, GetsWithoutContractRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsWithoutContractResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsWithoutContractQuery(request.Ids, request.ProjectId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsWithoutContractResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var dataList = new List<GetsWithoutContractModel>();
        foreach (var item in values!)
        {
            var modelingData = FullModeling(item, measureUnits, null);
            dataList.Add(modelingData.Adapt<GetsWithoutContractModel>());
        }

        return new GetsWithoutContractResponse(dataList ?? new List<GetsWithoutContractModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationStatusTypeResponse?>> GetsProjectOperationStatusType(
        GetsProjectOperationStatusTypeRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectOperationStatus>());
        return new GetsProjectOperationStatusTypeResponse(result);
    }

    public async Task<Result<GetsByOperationInfoResponse?>> GetsByOperationInfo(
        GetsByOperationInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for OperationInfoOperationGetsByOperationInfoId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByOperationInfoValidator, GetsByOperationInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByOperationInfoResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsByOperationInfoQuery(request.OperationInfoId, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsByOperationInfoResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var dataList = new List<GetsOperationInfoModel>();
        foreach (var item in values!)
        {
            var modelingData = FullModeling(item, measureUnits, null);
            dataList.Add(modelingData.Adapt<GetsOperationInfoModel>());
        }

        return new GetsByOperationInfoResponse(dataList ?? new List<GetsOperationInfoModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProposedPriceResponse?>> GetsProposedPrice(
        GetsProposedPriceRequest request, CT ct)
    {
        _logger.LogInformation("Request for OperationInfoOperationGetsProposedPriceId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProposedPriceValidator, GetsProposedPriceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProposedPriceResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsProposedPriceQuery(request.OperationInfoId, request.FilterData, request.StartDate,
            request.EndDate, request.EmployerId, request.CostCenterId, request.ProjectId, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsProposedPriceResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        ///TODO Employers
        //var currencyIds = values!.Where(x => x.EmployerContract is not null).Select(c => c.EmployerContract!.EmployerContractHead.CurrencyId).Distinct().ToList();
        //var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var employerIds = values?.NullListed(x => x.Project.EmployerId);
        var employersData = await WebServicesLogic.ThirdPartiesDataReceiver(employerIds, _mediator, ct);

        var dataList = new List<GetsProposedPriceModel>();
        foreach (var item in values!)
        {
            ///TODO Employers
            //var currencyInfo = currencies?.Where(x => x.Id == item.EmployerContract?.EmployerContractHead.CurrencyId).FirstOrDefault();
            //var employerData = employersData?.Where(x => x?.Id == item.Project.EmployerId).FirstOrDefault();
            //var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            //dataList.Add(new GetsProposedPriceModel(item.Id, item.OperationInfo.Id, item.OperationInfo.OperationInfoName, item.OperationInfo.OperationInfoCode,
            //    item.EmployerContract?.Id, item.EmployerContract?.EmployerContractHead.Code, item.Project.EmployerId, employerData?.FullName, item.EmployerContract?.StartDate,
            //    item.EmployerContract?.EndDate, item.Project.Id, item.Project.ProjectName, item.Project.CostCenter.Id, item.Project.CostCenter.CostCenterName,
            //    item.Workload, item.TolerancePercentage, item.Price, item.EmployerContract?.EmployerContractHead.CurrencyId, currencyInfo?.Name, item.CompanyId, company?.NameFa));
        }

        return new GetsProposedPriceResponse(dataList ?? new List<GetsProposedPriceModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsForPricingResponse?>> GetsForPricing(
        GetsForPricingRequest request, CT ct)
    {
        _logger.LogInformation("Request for OperationInfoOperationGetsForPricingId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsForPricingValidator, GetsForPricingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsForPricingResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsForPricingQuery(request.EmployerId, request.ProjectId, request.CostCenterId, request.ContractCode, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsForPricingResponse>(getsResponse.Error!);
        if (getsResponse.Value?.Data is null)
            return Result.Failure<GetsForPricingResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        var values = getsResponse.Value?.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        ///TODO Employers
        //var curencyIds = values!.Where(x => x.EmployerContract is not null).Select(x => x.EmployerContract!.EmployerContractHead.CurrencyId).ToList();
        //var currenciesData = await _mediator.Send(new GetsCurrencyByIdQuery(1, curencyIds.Count, curencyIds, true), ct); // بره سراغ متا دیتا

        var dataList = new List<GetsForPricingModel>();
        foreach (var item in values!)
        {
            var measureUnit = measureUnits?.Where(m => m.Id == item.UnitOfMeasurementId).Select(x => x.Name).FirstOrDefault();
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();

            ///TODO Employers
            //var currency = currenciesData.Value?.Data?.Where(m => m.Id == item.EmployerContract?.EmployerContractHead.CurrencyId).Select(x => x.Name).FirstOrDefault();
            //dataList.Add(new GetsForPricingModel(item.Id, item.OperationInfo.Id, item.OperationInfo.OperationInfoName, item.OperationInfo.OperationInfoCode,
            //    item.Workload, item.TolerancePercentage, item.Price, item.UnitOfMeasurementId, measureUnit, item.EmployerContract?.EmployerContractHead.CurrencyId, currency,
            //    item.EmployerContract is null ? false : true, item.CompanyId, company?.NameFa));
        }

        return new GetsForPricingResponse(dataList ?? new List<GetsForPricingModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsForEmployerStatusStatementResponse?>> GetsForEmployerStatusStatement(
        GetsForEmployerStatusStatementRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsForEmployerStatusStatementId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsForEmployerStatusStatementValidator, GetsForEmployerStatusStatementRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsForEmployerStatusStatementResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsForEmployerStatusStatementQuery(
            request.EmployerId,
            request.EmployerStatusStatementId,
            request.ProjectId,
            request.CostCenterId,
            request.EmployerContractId,
            request.StartDate,
            request.EndDate,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsForEmployerStatusStatementResponse>(response.Error!);
        var values = response.Value?.Data!;

        var measureIds = values?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        foreach (var item in values!)
            item.UnitOfMeasurementName = measureUnits?.FirstOrDefault(m => m.Id == item.UnitOfMeasurementId)?.Name;

        return new GetsForEmployerStatusStatementResponse(values ?? new List<GetsForEmployerStatusStatementModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<SetProjectOperationPriorityResponse?>> SetProjectOperationPriority(
        SetProjectOperationPriorityRequest request, CT ct)
    {
        _logger.LogInformation("Request for SetProjectOperationPriority, id:{Id},", request.Id);

        var isValidRequest = await request.IsValidAsync<SetProjectOperationPriorityValidator, SetProjectOperationPriorityRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetProjectOperationPriorityResponse>(isValidRequest.Error!);
        if (request.Priority <= 0)
            return Result.Failure<SetProjectOperationPriorityResponse>(ProjectOperationErrors.PriorityCanNot0);

        var projectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(request.Id), ct);
        if (projectOperation.IsFailure)
            return Result.Failure<SetProjectOperationPriorityResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

        var response = await _mediator.Send(new SetProjectOperationPriorityCommand(request.Id, request.Priority), ct);
        if (response.IsFailure)
            return Result.Failure<SetProjectOperationPriorityResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetProjectOperationPriorityResponse(response.Value!.Id, response.Value!.Priority);
    }

    public async Task<Result<ProjectOperationWorkloadManagementResponse?>> ProjectOperationWorkloadManagement(
        ProjectOperationWorkloadManagementRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationWorkloadManagement, id:{ProjectOperationId}", request.Id);

        var isValidRequest = await request.IsValidAsync<ProjectOperationWorkloadManagementValidator, ProjectOperationWorkloadManagementRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationWorkloadManagementResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ProjectOperationWorkloadManagementQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ProjectOperationWorkloadManagementResponse>(response.Error!);

        var value = WorkloadCalc(response.Value!);

        return new ProjectOperationWorkloadManagementResponse(value.Id, value.Workload, value.UsedWorkload, value.RemainingWorkload);
    }

    public async Task<Result<GetsSummarizedProjectOperationResponse?>> GetsSummarizedProjectOperation(
        GetsSummarizedProjectOperationRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsSummarizedProjectOperationId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsSummarizedProjectOperationValidator, GetsSummarizedProjectOperationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsSummarizedProjectOperationResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsSummarizedProjectOperationQuery(request.ProjectId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsSummarizedProjectOperationResponse>(getsResponse.Error!);

        var measureIds = getsResponse.Value?.Data?.Where(x => x.UnitOfMeasurementId > 0).Select(x => x.UnitOfMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var dataList = new List<GetsSummarizedProjectOperationModel>();
        foreach (var item in getsResponse.Value?.Data!)
        {
            var projectOperationMersur = measureUnits?.Where(m => m.Id == item.UnitOfMeasurementId).FirstOrDefault();
            var operationInfoMersur = measureUnits?.Where(m => m.Id == item.OperationInfo.UnitOfMeasurementId).FirstOrDefault();
            var operationInfo = item.OperationInfo;

            dataList.Add(new GetsSummarizedProjectOperationModel(item.Id, operationInfo.OperationInfoName, operationInfo.OperationInfoCode, operationInfo.UnitOfMeasurementId,
                operationInfoMersur?.Name, item.UnitOfMeasurementId, projectOperationMersur?.Name));
        }

        return new GetsSummarizedProjectOperationResponse(dataList ?? new List<GetsSummarizedProjectOperationModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationByIdsResponse?>> GetsProjectOperationByIds(
        GetsProjectOperationByIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationGetsProjectOperationByIdsId, ProjectId:{ProjectId} , OperationInfoId:{OperationInfoId}", request.Ids, request.FilterData);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationByIdsValidator, GetsProjectOperationByIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationByIdsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var getsResponse = await _mediator.Send(new GetsProjectOperationByIdsQuery(request.Ids, request.FilterData, companyId, request.PageIndex, request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsProjectOperationByIdsResponse>(getsResponse.Error!);
        if (getsResponse.Value!.Data is null)
            return Result.Failure<GetsProjectOperationByIdsResponse>(ProjectOperationErrors.DataNotFoundWithFilters);
        var values = getsResponse.Value?.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var dataList = new List<GetsProjectOperationByIdsResponseModel>();
        foreach (var item in values!)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            dataList.Add(new(item.Id, item.OperationInfo.OperationInfoCode, item.OperationInfo.OperationInfoName, item.CompanyId, company?.NameFa));
        }

        return new GetsProjectOperationByIdsResponse(dataList ?? new List<GetsProjectOperationByIdsResponseModel>(0), dataList!.Count);
    }

    public async Task<Result<GetsProjectOperationReportingResponse?>> GetsProjectOperationReporting(
        GetsProjectOperationReportingRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationReporting, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationReportingValidator, GetsProjectOperationReportingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationReportingResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsProjectOperationReportingQuery(
                null,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ContractorIds,
                request.Statuses,
                request.StartDate,
                request.EndDate,
                request.Description,
                request.DailyDescription,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsProjectOperationReportingResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        List<long> measureIds = [];
        var pomeasureIds = values.Where(x => x.MeasurementId > 0).Select(x => x.MeasurementId).Distinct().ToList();
        measureIds.AddRange(pomeasureIds);
        var omeasureIds = values.Where(x => x.OperationInfoMeasurementId > 0).Select(x => x.OperationInfoMeasurementId).Distinct().ToList();
        measureIds.AddRange(omeasureIds);
        measureIds = measureIds.Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var ids = UserIdCollectors(values);
        var users = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(ids, null, null, _mediator, ct);

        var creatorIds = CreatorIdCollectors(values);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var dataList = new List<GetsProjectOperationReportingModel>();
        values.ForEach(item =>
        {
            if (item.OperationInfoSeasons is not null && item.OperationInfoSeasons.Count > 0)
            {
                var categoryNames = item.OperationInfoSeasons.Where(x => x.Category != null).Select(x => x.Category!).ToList();
                if (categoryNames is not null && categoryNames.Count > 0)
                    item.CategoryName = StringSeparator.WithComma(categoryNames);

                var branchNames = item.OperationInfoSeasons.Where(x => x.Branch != null).Select(x => x.Branch!).ToList();
                if (branchNames is not null && branchNames.Count > 0)
                    item.BranchName = StringSeparator.WithComma(branchNames);

                var seasonNames = item.OperationInfoSeasons.Where(x => x.Season != null).Select(x => x.Season!).ToList();
                if (seasonNames is not null && seasonNames.Count > 0)
                    item.SeasonName = StringSeparator.WithComma(seasonNames);
            }

            if (users is not null && users.Count > 0)
            {
                if (item.ImplementationAssistantIds is not null && item.ImplementationAssistantIds.Count > 0)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    var implementationFullNames = users?.Where(x => item.ImplementationAssistantIds.Contains(x.Id)).Select(x => x.FullName).ToList();
                    var implementationNicknames = users?.Where(x => item.ImplementationAssistantIds.Contains(x.Id)).Select(x => x.Nickname).ToList();
                    if (implementationFullNames is not null && implementationFullNames.Any())
                        item.ImplementationAssistants = string.Join(" - ", implementationFullNames!);
                    if (implementationNicknames is not null && implementationNicknames.Any())
                        item.ImplementationAssistantsNickName = string.Join(" - ", implementationNicknames!);
                }

                if (item.TechnicalAssistantIds is not null && item.TechnicalAssistantIds.Count > 0)
                {
                    var technicalFullNames = users?.Where(x => item.TechnicalAssistantIds.Contains(x.Id)).Select(x => x.FullName).ToList();
                    var technicalNicknames = users?.Where(x => item.TechnicalAssistantIds.Contains(x.Id)).Select(x => x.Nickname).ToList();
                    if (technicalFullNames is not null && technicalFullNames.Any())
                        item.TechnicalAssistants = string.Join(" - ", technicalFullNames!);
                    if (technicalNicknames is not null && technicalNicknames.Any())
                        item.TechnicalAssistantsNickName = string.Join(" - ", technicalNicknames!);
                }

                if (item.ContractorIds is not null && item.ContractorIds.Count > 0)
                {
                    var contractorFullNames = users?.Where(x => item.ContractorIds.Contains(x.Id)).Select(x => x.FullName).ToList();
                    var contractorNicknames = users?.Where(x => item.ContractorIds.Contains(x.Id)).Select(x => x.Nickname).ToList();
                    if (contractorFullNames is not null && contractorFullNames.Any())
                        item.Contractors = string.Join(" - ", contractorFullNames!);
                    if (contractorNicknames is not null && contractorNicknames.Any())
                        item.ContractorsNickName = string.Join(" - ", contractorNicknames!);
                }
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (item.DailyCreatorsIds is not null && item.DailyCreatorsIds.Count > 0)
            {
                var dailyCreatorFullNames = creators?.Where(x => x.UserId.HasValue && item.DailyCreatorsIds.Contains(x.UserId.Value)).Select(x => x.FullName).ToList();
                if (dailyCreatorFullNames is not null && dailyCreatorFullNames.Any())
                    item.DailyProjectOperationCreators = string.Join(" - ", dailyCreatorFullNames!);
            }

            item.CreatorName = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.MeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.MeasurementId)?.Name;
            item.OperationInfoMeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurementId)?.Name;

            if (item.DailyAmounts is not null && item.DailyAmounts.Count > 0)
                item.DoneWorkload = item.DailyAmounts.Sum(x => x);

            dataList.Add(item);
        });

        dataList = dataList.Where(x => (request.MinimumWorkload == null || x.Workload >= request.MinimumWorkload) &&
                                       (request.MinimumDoneWorkload == null || x.DoneWorkload >= request.MinimumDoneWorkload) &&
                                       (request.MinimumRemaindedWorkload == null || x.RemaindedWorkload >= request.MinimumRemaindedWorkload)).ToList();

        return new GetsProjectOperationReportingResponse(dataList ?? new List<GetsProjectOperationReportingModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsTotalProjectOperationReportingResponse?>> GetsTotalProjectOperationReporting(
        GetsTotalProjectOperationReportingRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTotalProjectOperationReporting, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsTotalProjectOperationReportingValidator, GetsTotalProjectOperationReportingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTotalProjectOperationReportingResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsTotalProjectOperationReportingQuery(
                null,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ContractorIds,
                request.Statuses,
                request.StartDate,
                request.EndDate,
                request.Description,
                request.DailyDescription,
                request.FilterData,
                0,
                0), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsTotalProjectOperationReportingResponse>(getsResponse.Error!);
        var response = getsResponse.Value?.Data!;

        var deductions = response.Where(x => x.Deductions is not null && x.Deductions.Count > 0)
            .SelectMany(x => x.Deductions).DistinctBy(z => z.Id).Sum(z => z.FinalAmount);
        var dailies = response.Where(x => x.Dailies is not null && x.Dailies.Count > 0)
            .SelectMany(x => x.Dailies).DistinctBy(z => z.Id).Sum(z => z.FinalAmount);
        var details = response.Where(x => x.Details is not null && x.Details.Count > 0)
            .SelectMany(x => x.Details).DistinctBy(z => z.Id).Sum(z => z.FinalAmount);

        return new GetsTotalProjectOperationReportingResponse()
        {
            TotalDeductionAmounts = deductions,
            DailyFinalAmounts = dailies,
            TotalFinalAmounts = details,
            ProjectOperationWorkload = response.Sum(x => x.ProjectOperationWorkload)
        };
    }

    public async Task<Result<GetsProjectOperationReportingExcelEnumResponse?>> GetsProjectOperationReportingExcelEnum(
        GetsProjectOperationReportingExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationReportingExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectOperationExcelEnum>());
        return new GetsProjectOperationReportingExcelEnumResponse(response);
    }

    public async Task<Result<GetsProjectOperationReportingExcelExporterResponse?>> GetsProjectOperationReportingExcelExporter(
        GetsProjectOperationReportingExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationReportingExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationReportingExcelExporterValidator, GetsProjectOperationReportingExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationReportingExcelExporterResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsProjectOperationReportingQuery(
                request.Ids,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ContractorIds,
                request.Statuses,
                request.StartDate,
                request.EndDate,
                request.Description,
                request.DailyDescription,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsProjectOperationReportingExcelExporterResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        List<long> measureIds = [];
        var pomeasureIds = values.Where(x => x.MeasurementId > 0).Select(x => x.MeasurementId).Distinct().ToList();
        measureIds.AddRange(pomeasureIds);
        var omeasureIds = values.Where(x => x.OperationInfoMeasurementId > 0).Select(x => x.OperationInfoMeasurementId).Distinct().ToList();
        measureIds.AddRange(omeasureIds);
        measureIds = measureIds.Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var ids = UserIdCollectors(values);
        var users = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(ids, null, null, _mediator, ct);

        var creatorIds = CreatorIdCollectors(values);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var dataList = new List<GetsProjectOperationReportingModel>();
        values.ForEach(item =>
        {
            if (item.OperationInfoSeasons is not null && item.OperationInfoSeasons.Count > 0)
            {
                var categoryNames = item.OperationInfoSeasons.Where(x => x.Category != null).Select(x => x.Category!).ToList();
                if (categoryNames is not null && categoryNames.Count > 0)
                    item.CategoryName = StringSeparator.WithComma(categoryNames);

                var branchNames = item.OperationInfoSeasons.Where(x => x.Branch != null).Select(x => x.Branch!).ToList();
                if (branchNames is not null && branchNames.Count > 0)
                    item.BranchName = StringSeparator.WithComma(branchNames);

                var seasonNames = item.OperationInfoSeasons.Where(x => x.Season != null).Select(x => x.Season!).ToList();
                if (seasonNames is not null && seasonNames.Count > 0)
                    item.SeasonName = StringSeparator.WithComma(seasonNames);
            }

            if (users is not null && users.Count > 0)
            {
                if (item.ImplementationAssistantIds is not null && item.ImplementationAssistantIds.Count > 0)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    var implementationFullNames = users?.Where(x => item.ImplementationAssistantIds.Contains(x.Id)).Select(x => x.FullName).ToList();
                    var implementationNicknames = users?.Where(x => item.ImplementationAssistantIds.Contains(x.Id)).Select(x => x.Nickname).ToList();
                    if (implementationFullNames is not null && implementationFullNames.Any())
                        item.ImplementationAssistants = string.Join(" - ", implementationFullNames!);
                    if (implementationNicknames is not null && implementationNicknames.Any())
                        item.ImplementationAssistantsNickName = string.Join(" - ", implementationNicknames!);
                }

                if (item.TechnicalAssistantIds is not null && item.TechnicalAssistantIds.Count > 0)
                {
                    var technicalFullNames = users?.Where(x => item.TechnicalAssistantIds.Contains(x.Id)).Select(x => x.FullName).ToList();
                    var technicalNicknames = users?.Where(x => item.TechnicalAssistantIds.Contains(x.Id)).Select(x => x.Nickname).ToList();
                    if (technicalFullNames is not null && technicalFullNames.Any())
                        item.TechnicalAssistants = string.Join(" - ", technicalFullNames!);
                    if (technicalNicknames is not null && technicalNicknames.Any())
                        item.TechnicalAssistantsNickName = string.Join(" - ", technicalNicknames!);
                }

                if (item.ContractorIds is not null && item.ContractorIds.Count > 0)
                {
                    var contractorFullNames = users?.Where(x => item.ContractorIds.Contains(x.Id)).Select(x => x.FullName).ToList();
                    var contractorNicknames = users?.Where(x => item.ContractorIds.Contains(x.Id)).Select(x => x.Nickname).ToList();
                    if (contractorFullNames is not null && contractorFullNames.Any())
                        item.Contractors = string.Join(" - ", contractorFullNames!);
                    if (contractorNicknames is not null && contractorNicknames.Any())
                        item.ContractorsNickName = string.Join(" - ", contractorNicknames!);
                }
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (item.DailyCreatorsIds is not null && item.DailyCreatorsIds.Count > 0)
            {
                var dailyCreatorFullNames = creators?.Where(x => x.UserId.HasValue && item.DailyCreatorsIds.Contains(x.UserId.Value)).Select(x => x.FullName).ToList();
                if (dailyCreatorFullNames is not null && dailyCreatorFullNames.Any())
                    item.DailyProjectOperationCreators = string.Join(" - ", dailyCreatorFullNames!);
            }

            item.CreatorName = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.MeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.MeasurementId)?.Name;
            item.OperationInfoMeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurementId)?.Name;

            if (item.DailyAmounts is not null && item.DailyAmounts.Count > 0)
                item.DoneWorkload = item.DailyAmounts.Sum(x => x);

            dataList.Add(item);
        });

        dataList = dataList.Where(x => (request.MinimumWorkload == null || x.Workload >= request.MinimumWorkload) &&
                                       (request.MinimumDoneWorkload == null || x.DoneWorkload >= request.MinimumDoneWorkload) &&
                                       (request.MinimumRemaindedWorkload == null || x.RemaindedWorkload >= request.MinimumRemaindedWorkload)).ToList();

        var data = dataList.Adapt<List<GetsProjectOperationReportingExcelExporterModel>>();

        var file = new FileContentResult(ProjectOperationExcels.ProjectOperationReportingToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectOperations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsProjectOperationReportingExcelExporterResponse(file);
    }

    public async Task<Result<GetsProjectOperationEmployerReportingExcelExporterResponse?>> GetsProjectOperationEmployerReportingExcelExporter(
        GetsProjectOperationEmployerReportingExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationEmployerReportingExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationEmployerReportingExcelExporterValidator, GetsProjectOperationEmployerReportingExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationEmployerReportingExcelExporterResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsProjectOperationEmployerReportingQuery(
                request.Ids,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ContractorIds,
                request.EmployerIds,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsProjectOperationEmployerReportingExcelExporterResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        _logger.LogInformation("GetMediatorSuccessful");
        var measureIds = values.Where(x => x.OperationInfoMeasurementId > 0).Select(x => x.OperationInfoMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        values.ForEach(item =>
        {
            item.OperationInfoMeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurementId)?.Name;
            if (item.DailyProjectOperations is not null)
                item.DailyProjectOperations.ForEach(daily =>
                {
                    daily.ProjectName = item.ProjectName;
                    daily.ProjectCode = item.ProjectCode;
                    daily.OperationInfoName = item.OperationInfoName;
                    daily.OperationInfoCode = item.OperationInfoCode;
                    daily.OperationInfoMeasurementName = item.OperationInfoMeasurementName;
                });
        });

        _logger.LogInformation("Set ForEach");
        var file = new FileContentResult(ProjectOperationLetterheadExcels.GetsProjectOperationEmployerReportingToExcel(request.StartDate, request.EndDate, values), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectOperations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        _logger.LogInformation("Create Excel File");
        return new GetsProjectOperationEmployerReportingExcelExporterResponse(file);
    }

    public async Task<Result<GetsProjectOperationEmployerReportingResponse?>> GetsProjectOperationEmployerReporting(
        GetsProjectOperationEmployerReportingRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationEmployerReporting, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationEmployerReportingValidator, GetsProjectOperationEmployerReportingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationEmployerReportingResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsProjectOperationEmployerReportingQuery(
                request.Ids,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ContractorIds,
                request.EmployerIds,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsProjectOperationEmployerReportingResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var measureIds = values.Where(x => x.OperationInfoMeasurementId > 0).Select(x => x.OperationInfoMeasurementId).Distinct().ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        values.ForEach(item =>
        {
            item.OperationInfoMeasurementName = measureUnits?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurementId)?.Name;
            if (item.DailyProjectOperations is not null)
                item.DailyProjectOperations.ForEach(daily =>
                {
                    daily.ProjectName = item.ProjectName;
                    daily.ProjectCode = item.ProjectCode;
                    daily.OperationInfoName = item.OperationInfoName;
                    daily.OperationInfoCode = item.OperationInfoCode;
                    daily.OperationInfoMeasurementName = item.OperationInfoMeasurementName;
                });
        });

        return new GetsProjectOperationEmployerReportingResponse(values ?? new List<GetsProjectOperationEmployerReportingModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationDailyReportingResponse?>> GetsProjectOperationDailyReporting(
        GetsProjectOperationDailyReportingRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationDailyReporting, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDailyReportingValidator,
            GetsProjectOperationDailyReportingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDailyReportingResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsProjectOperationDailyReportingQuery(
                request.Ids,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsProjectOperationDailyReportingResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var measureIds = values.Where(w => w.OperationInfoMeasurementId > 0)
                               .Select(s => s.OperationInfoMeasurementId)
                               .Distinct()
                               .ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        values.ForEach(item =>
        {
            item.OperationInfoMeasurementName = measureUnits?.FirstOrDefault(f => f.Id == item.OperationInfoMeasurementId)?.Name;
            if (item.ProjectOperationDaily is not null)
                item.ProjectOperationDaily.ForEach(daily =>
                {
                    daily.ProjectName = item.ProjectName;
                    daily.ProjectCode = item.ProjectCode;
                    daily.OperationInfoName = item.OperationInfoName;
                    daily.OperationInfoCode = item.OperationInfoCode;
                    daily.OperationInfoMeasurementName = item.OperationInfoMeasurementName;
                });
        });

        return new GetsProjectOperationDailyReportingResponse(values ?? new List<GetsProjectOperationDailyReportingModel>(0), getsResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationDailyReportingExcelExporterResponse?>> GetsProjectOperationDailyReportingExcelExporter(
        GetsProjectOperationDailyReportingExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationDailyReportingExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDailyReportingExcelExporterValidator,
            GetsProjectOperationDailyReportingExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDailyReportingExcelExporterResponse>(isValidRequest.Error!);

        var getsResponse = await _mediator.Send(new GetsProjectOperationDailyReportingQuery(
                request.Ids,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize), ct);
        if (getsResponse.IsFailure)
            return Result.Failure<GetsProjectOperationDailyReportingExcelExporterResponse>(getsResponse.Error!);
        var values = getsResponse.Value?.Data!;

        var measureIds = values.Where(w =>
                                    w.OperationInfoMeasurementId > 0)
                               .Select(s => s.OperationInfoMeasurementId)
                               .Distinct()
                               .ToList();
        var measureUnits = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        values.ForEach(item =>
        {
            item.OperationInfoMeasurementName = measureUnits?.FirstOrDefault(f => f.Id == item.OperationInfoMeasurementId)?.Name;
            if (item.ProjectOperationDaily is not null)
                item.ProjectOperationDaily.ForEach(daily =>
                {
                    daily.ProjectName = item.ProjectName;
                    daily.ProjectCode = item.ProjectCode;
                    daily.OperationInfoName = item.OperationInfoName;
                    daily.OperationInfoCode = item.OperationInfoCode;
                    daily.OperationInfoMeasurementName = item.OperationInfoMeasurementName;
                });
        });

        var data = values.Adapt<List<GetsProjectOperationDailyReportingExcelExporterModel>>();
        var file = new FileContentResult(ProjectOperationExcels.GetsProjectOperationDailyReportingToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectOperations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsProjectOperationDailyReportingExcelExporterResponse(file);
    }

    public async Task<Result<GetFltrBasePricedPOsResponse?>> GetFltrBasePricedPOs(
        GetFltrBasePricedPOsRequest request, CT ct)
    {
        var result = await GetFltrBasePricedHandle(request, ct);
        if (result.IsBad()) return result.Failure<GetFltrBasePricedPOsResponse>()!;
        var values = result.Value!;

        var measureNames = await _measureUnitRepository.GetMeasureUnitsByIds(values.Listed(x => x.MeasurementId), ct);
        foreach (var res in values)
        {
            res.MeasurementName = measureNames.FirstOrDefault(x => x.Id == res.MeasurementId)?.Name;
            if (res.POActionData.HasAny())
                res.ChangePrice = res.POActionData?.Sum(x => x.Price) ?? res.ChangePrice;
        }

        var otherData = values
            .GroupBy(x => x.SeasonId)
            .Select(g => new GetFltrBasePricedPOsGroupedModel
            {
                Id = g.Key,
                SeasonName = g.First().SeasonName,
                BranchName = g.First().BranchName,
                CategoryName = g.First().CategoryName,
                TotalPrice = g.Sum(x => x.FinalPrice),
                BaseTotalPrice = g.Sum(x => x.BaseTotalPrice)
            }).ToList();

        values = values.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetFltrBasePricedPOsResponse(values, otherData, result.Value!.Count);
    }

    public async Task<Result<UpdatePriceResponse?>> UpdateProjectOperationPrice(
    UpdatePriceRequest request, CT ct)
    {
        var query = new GetProjectOperationByIdQuery(
            Id: request.Id
        );
        var pOperationsResult = await _mediator.Send(query, ct);
        if (pOperationsResult.IsBad() || pOperationsResult.Value is null)
            return Result.Failure<UpdatePriceResponse?>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        var projectOperation = pOperationsResult.Value;
        var command = new UpdatePriceCommand(projectOperation, request.ChangePrice, request.IncreaseRate);
        var updateResult = await _mediator.Send(command, ct);
        if (updateResult.IsBad())
            return updateResult.Failure<UpdatePriceResponse?>()!;
        return new UpdatePriceResponse(true);
    }

    public async Task<Result<GetFltrProjectOperationResponse?>> GetFltrProjectOperation(
        GetFltrProjectOperationRequest request, CT ct)
    {
        var result = await _mediator.Send(request.Adapt<GetFltrProjectOperationQuery>(), ct);
        if (result.IsBad()) return result.Failure<GetFltrProjectOperationResponse>()!;
        var values = result.Value!;

        var measureNames = await _measureUnitRepository.GetMeasureUnitsByIds(values.Listed(x => x.MeasurementId), ct);
        foreach (var res in values)
            res.MeasurementName = measureNames.FirstOrDefault(x => x.Id == res.MeasurementId)?.Name;

        var otherData = values
            .GroupBy(x => x.SeasonId)
            .Select(g => new GetFltrProjectOperationGroupedModel
            {
                Id = g.Key,
                SeasonName = g.First().SeasonName,
                BranchName = g.First().BranchName,
                CategoryName = g.First().CategoryName,
                TotalPrice = g.Sum(x => x.FinalPrice),
                BaseTotalPrice = g.Sum(x => x.BaseTotalPrice)
            }).ToList();


        values = values.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetFltrProjectOperationResponse(values, otherData, result.Value!.Count);
    }

    public async Task<Result<GetProjectOperationProgressResponse?>> GetProjectOperationProgress(
        GetProjectOperationProgressRequest request, CT ct)
    {
        var result = await _mediator.Send(new GetProjectOperationProgressQuery(request.Id), ct);
        if (result.IsBad()) return result.Failure<GetProjectOperationProgressResponse>()!;

        return result;
    }

    public async Task<Result<SetPlannedDateResponse?>> SetPlannedDate(
        SetPlannedDateRequest request, CT ct)
    {
        var result = await _mediator.Send(new SetPlannedDateCommand(request.Id,
            request.PlannedStartDate,
            request.PlannedFinishDate,
            request.PlannedDuration), ct);
        if (result.IsBad())
            return result.Failure<SetPlannedDateResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return result;
    }

    public async Task<Result<GetPODateResponse?>> GetPODate(
        GetPODateRequest request, CT ct)
    {
        var result = await _mediator.Send(new GetPODateQuery(request.ProjectOperationId), ct);
        if (result.IsBad())
            return result.Failure<GetPODateResponse>()!;

        return result;
    }

    public async Task<Result<GetCriticalPOResponse?>> GetCriticalPO(
        GetCriticalPORequest request, CT ct)
    {
        var result = await _mediator.Send(new GetCriticalPOQuery(request.ProjectId,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetCriticalPOResponse>()!;

        return result;
    }
}
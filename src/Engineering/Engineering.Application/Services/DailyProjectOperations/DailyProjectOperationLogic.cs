using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Configs;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetConsumableVolumeMachineryById;
using Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetContractorEmployeesByContractorId;
using Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationDocument;
using Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationExpert;
using Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationMachinery;
using Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationProduct;
using Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationRequestReward;
using Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationDocument;
using Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationExpert;
using Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationMachinery;
using Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationProduct;
using Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationRequestReward;
using Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Commands.UpdateDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Commands.UpdateDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Models.CreateDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Models.DailyProjectOperationTelegramMessageSender;
using Engineering.Application.Services.DailyProjectOperations.Models.DeleteDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelEnums;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelExporter;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationByLegacyId;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationContractors;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistories;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistoryById;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDetailedDailyOperationTotals;
using Engineering.Application.Services.DailyProjectOperations.Models.GetEmployerStatusStatementLimitDate;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableExperts;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableProducts;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredDailyProjectOperationDetails;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredDailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredMachineries;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredProjectOperationsByProjectIds;
using Engineering.Application.Services.DailyProjectOperations.Models.GetOperationTotals;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationDocument;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationServiceExcelEnums;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationServiceExcelExporter;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationStatus;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyServiceInfo;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperationExcelEnums;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperationExcelExporter;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsTotalDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Models.GetTotalsByProjectOperationDetailId;
using Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperationDocuments;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetDailiesExcelExport;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationById;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationByLegacyId;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationContractors;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationDetails;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationHistoryById;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetDetailedDailyOperationTotals;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetEmployerStatusStatementLimitDate;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetFilteredDailyProjectOperationDetails;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetFilteredDailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetFilteredProjectOperationsByProjectIds;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetOperationTotals;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetProjectOperationDetailContractorServices;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyCreator;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyProjectOperationDocument;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyServiceInfo;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetsDetailedDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetsTotalDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetTotalsByProjectOperationDetailId;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.ValidateProjectOperationDependencyQuery;
using Engineering.Application.Services.ProjectOperationDetails.Commands.UpdatesProjectOperationDetailDate;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdForDailyProjectOperation;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessInclude;
using Engineering.Application.Services.ProjectOperations.Command.SetPOActualDate;
using Engineering.Application.Services.Projects.Commands.ProjectStatusChanger;
using Engineering.Application.Services.ProjectServices.Commands.UpdateProjectServiceDoneVolume;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetailForDaily;
using Engineering.Application.Services.RequestMachineries.Queries.GetFilteredRequestMachineryProjectOperationDetails;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryById;
using Engineering.Application.Services.RequestRewards;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;
using Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardsByIds;
using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Application.Services.TransportationRequests.Models.GetsDailyCreator;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSelectedSkillId;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Products.Queries.GetById;
using Microsoft.Extensions.Options;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.DailyProjectOperations;

public partial class DailyProjectOperationLogic : IDailyProjectOperationLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<DailyProjectOperationLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRequestRewardLogic _requestRewardLogic;
    private readonly IMessengerChannelRepository _messengerChannelRepo;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserProfileService _userProfileService;
    private readonly IViewThirdPartyRepository _tpRepo;
    private readonly IViewProductRepository _pRepo;
    private readonly ITelegramMessageHistoryLogic _telegramMessageHistoryLogic;
    private readonly string? _authorization;
    private readonly MessageSenderConfig _messageSenderConfig;

    public DailyProjectOperationLogic(
        IMediator mediator,
        ILogger<DailyProjectOperationLogic> logger,
        IUnitOfWork unitOfWork,
        IRequestRewardLogic requestRewardLogic,
        IUserInfoService userInfoService,
        IUserProfileService userProfileService,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        IViewThirdPartyRepository tpRepo,
        IMessengerChannelRepository messengerChannelRepo,
        IViewProductRepository pRepo,
        IOptionsSnapshot<MessageSenderConfig> options,
        IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _requestRewardLogic = requestRewardLogic;
        _userInfoService = userInfoService;
        _messengerChannelRepo = messengerChannelRepo;
        _tpRepo = tpRepo;
        _userProfileService = userProfileService;
        _telegramMessageHistoryLogic = telegramMessageHistoryLogic;
        _pRepo = pRepo;
        _messageSenderConfig = options.Value;
        _authorization = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();
    }

    public async Task<Result<CreateDailyProjectOperationResponse?>> CreateDailyProjectOperation(
        CreateDailyProjectOperationRequest request, CT ct)
    {
        using (var transaction = new CreateTransaction())
        {
            var isValidRequest = await request.IsValidAsync<CreateDailyProjectOperationValidator, CreateDailyProjectOperationRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<CreateDailyProjectOperationResponse>(isValidRequest.Error!);

            var projectOperationDetailQuery = await _mediator.Send(new GetProjectOperationDetailByIdForDailyQuery(request.ProjectOperationDetailId), ct);
            if (projectOperationDetailQuery.IsFailure || projectOperationDetailQuery is null)
                return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.ProjectOperationDetailWithIdNotFound);
            var projectOperationDetail = projectOperationDetailQuery.Value!;

            var checkDependency = await _mediator.Send(new ValidateProjectOperationDependencyQuery(projectOperationDetail.ProjectOperationId, request.StartDate, null, null, null), ct);
            if (checkDependency.IsBad())
                return checkDependency.Failure<CreateDailyProjectOperationResponse?>();

            if (projectOperationDetail.StartDate is null)
                return Result.Failure<CreateDailyProjectOperationResponse>(ProjectOperationDetailErrors.DateTimeNotValidForDaily3);
            if (request.StartDate.Date < projectOperationDetail.StartDate!.Value.Date)
                return Result.Failure<CreateDailyProjectOperationResponse>(ProjectOperationDetailErrors.DateTimeNotValidForDaily2);

            long? companyId = _userInfoService.UserCompanyId <= 0 ? null : _userInfoService.UserCompanyId;
            if (companyId is not null && companyId >= 1)
            {
                var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
                if (companyResponse.IsFailure)
                    return Result.Failure<CreateDailyProjectOperationResponse>(companyResponse.Error!);
            }

            var createDailyProjectOperation = await _mediator.Send(new CreateDailyProjectOperationCommand(request.Status, request.StartDate, request.EndDate,
                request.Length, request.Width, request.Height, request.Weight, request.Number, projectOperationDetail, request.Description, request.LegacyId, companyId), ct);
            if (createDailyProjectOperation.IsFailure)
                return Result.Failure<CreateDailyProjectOperationResponse>(createDailyProjectOperation.Error!);
            var createResponse = createDailyProjectOperation.Value!;

            if (request.DocumentUrls != null && request.DocumentUrls.Count > 0)
                foreach (var document in request.DocumentUrls!)
                {
                    var createDocument = await _mediator.Send(new CreateDailyProjectOperationDocumentCommand(document, createResponse), ct);
                    if (createDocument.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(createDocument.Error!);
                }

            if (request.Products != null && request.Products.Count > 0)
                foreach (var product in request.Products!)
                {
                    var consumableVolumeProduct = projectOperationDetail.ConsumableVolumeProducts.Where(c => c.Id.Equals(product.ConsumableVolumeProductId)).FirstOrDefault();
                    if (consumableVolumeProduct is null)
                        return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.ConsumableVolumeProductWithIdNotFound);

                    var getsProductByIdQuery = await _mediator.Send(new GetsProductByIdQuery(1, 1, null, [product.ProductId]), ct);
                    if (getsProductByIdQuery.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(getsProductByIdQuery.Error!);

                    if (getsProductByIdQuery.Value is null || getsProductByIdQuery.Value.RowCount != 1)
                        return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.ProductWithIdNotFound);

                    var createDailyDocument = await _mediator.Send(new CreateDailyProjectOperationProductCommand(product.ProductId, product.FinalValue,
                        product.UnusedValue, createResponse, consumableVolumeProduct), ct);
                    if (createDailyDocument.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(createDailyDocument.Error!);
                }

            List<ProjectOperationDetailContractorService>? detailContractorServicesHaveProjectService = new();
            if (request.ContractorServices != null)
                foreach (var item in request.ContractorServices)
                {
                    var contractorService = projectOperationDetail.ProjectOperationDetailContractorServices.Where(c => c.Id.Equals(item.ContractorServiceId)).FirstOrDefault();
                    if (contractorService is null)
                        return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.ContractorServiceWithIdNotFound);
                    if (!contractorService.IsActive)
                        return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.ContractorServiceIsNotActive);

                    if (!string.IsNullOrEmpty(item.TimeSpant))
                    {
                        if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                            return Result.Failure<CreateDailyProjectOperationResponse>(MachineryStandardErrors.TimeSpantCountError);

                        if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                            return Result.Failure<CreateDailyProjectOperationResponse>(MachineryStandardErrors.MoreThan59Min);
                    }

                    var createDailyService = await _mediator.Send(new CreateDailyProjectOperationServiceCommand(createResponse,
                        contractorService,
                        item.Volume,
                        item.ProjectServiceVolume,
                        item.ThirdPartyId,
                        TimeCalculator.StringToTicks(item.TimeSpant),
                        item.IsActive), ct);
                    if (createDailyService.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(createDailyService.Error!);

                    if (contractorService is not null)
                        if (contractorService.ProjectServiceDetail is not null)
                            detailContractorServicesHaveProjectService.Add(contractorService);
                }

            if (request.Machineries != null && request.Machineries.Count > 0)
            {
                foreach (var machineryModel in request.Machineries!)
                {
                    //var consumableVolumeMachinery = projectOperationDetail.ConsumableVolumeMachineries.Where(c => c.Id.Equals(machineryModel.ConsumableVolumeMachineryId)).FirstOrDefault();
                    //if (consumableVolumeMachinery is null)
                    //    return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.ConsumableVolumeMachineryWithIdNotFound);

                    var consumbeleMachinery = await _mediator.Send(new GetConsumableVolumeMachineryByIdQuery(machineryModel.ConsumableVolumeMachineryId), ct);
                    if (consumbeleMachinery.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(consumbeleMachinery.Error!);
                    var consumble = consumbeleMachinery.Value;

                    var getRequestMachineryByIdQuery = await _mediator.Send(new GetRequestMachineryByIdQuery(machineryModel.RequestMachineryId), ct);
                    if (getRequestMachineryByIdQuery.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(getRequestMachineryByIdQuery.Error!);

                    var requestMachinery = getRequestMachineryByIdQuery.Value!;
                    if (requestMachinery.Status != RequestMachineryStatus.OnProject)
                        return Result.Failure<CreateDailyProjectOperationResponse>(RequestMachineryErrors.InValidStatus);

                    if ((requestMachinery.Unit == RequestMachineryUnit.Daily ||
                        requestMachinery.Unit == RequestMachineryUnit.Serviced) &&
                        machineryModel.Number == null)
                        return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.InvalidUnitNumber);

                    if (!string.IsNullOrEmpty(machineryModel.FinalValue))
                        if (!(machineryModel.FinalValue.Split(':')[0].Count() >= 2 && machineryModel.FinalValue.Split(':')[1].Count() == 2))
                            return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.InvalidTime);

                    if (!string.IsNullOrEmpty(machineryModel.UnusedValue))
                        if (!(machineryModel.UnusedValue.Split(':')[0].Count() >= 2 && machineryModel.UnusedValue.Split(':')[1].Count() == 2))
                            return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.InvalidTime);

                    var finalValue = TimeCalculator.StringToTicks(machineryModel!.FinalValue);
                    var unusedValue = TimeCalculator.StringToTicks(machineryModel!.UnusedValue);

                    var createDailyMachinery = await _mediator.Send(new CreateDailyProjectOperationMachineryCommand(
                        requestMachinery, finalValue, unusedValue, machineryModel.Number, createResponse, consumble!),
                        ct);
                    if (createDailyMachinery.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(createDailyMachinery.Error!);
                }
            }

            if (request.Experts != null && request.Experts.Count > 0)
            {
                foreach (var model in request.Experts!)
                {
                    var consumableVolumeExpert = projectOperationDetail.ConsumableVolumeExperts.Where(c => c.Id.Equals(model.ConsumableVolumeExpertId)).FirstOrDefault();
                    if (consumableVolumeExpert is null)
                        return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.ConsumableVolumeExpertWithIdNotFound);

                    var GetWithSkillOnlyByIdsQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long>() { model.ThirdPartyId }, null, false, null), ct);
                    if (GetWithSkillOnlyByIdsQuery.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(GetWithSkillOnlyByIdsQuery.Error!);

                    if (GetWithSkillOnlyByIdsQuery.Value is null || GetWithSkillOnlyByIdsQuery.Value.RowCount != 1)
                        return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.ProductWithIdNotFound);

                    if (!string.IsNullOrEmpty(model.FinalValue))
                        if (!(model.FinalValue.Split(':')[0].Count() >= 2 && model.FinalValue.Split(':')[1].Count() == 2))
                            return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.InvalidTime);

                    if (!string.IsNullOrEmpty(model.UnusedValue))
                        if (!(model.UnusedValue.Split(':')[0].Count() >= 2 && model.UnusedValue.Split(':')[1].Count() == 2))
                            return Result.Failure<CreateDailyProjectOperationResponse>(DailyProjectOperationErrors.InvalidTime);

                    var finalValue = TimeCalculator.StringToTicks(model!.FinalValue);
                    var unusedValue = TimeCalculator.StringToTicks(model!.UnusedValue);
                    var createDailyExpert = await _mediator.Send(new CreateDailyProjectOperationExpertCommand(model.ThirdPartyId, finalValue, unusedValue,
                        createResponse, consumableVolumeExpert), ct);
                    if (createDailyExpert.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(createDailyExpert.Error!);
                }
            }

            if (request.RequestReward is not null)
            {
                var createRequestReward = await _requestRewardLogic.CreateRequestReward(request.RequestReward, ct);
                if (createRequestReward.IsFailure)
                    return Result.Failure<CreateDailyProjectOperationResponse>(createRequestReward.Error!);

                var requestRewardIds = createRequestReward.Value!.Ids.ToList();
                foreach (var requestRewardId in requestRewardIds)
                {
                    var createDailyRequestReward = await _mediator.Send(new CreateDailyProjectOperationRequestRewardCommand(createResponse, requestRewardId), ct);
                    if (createDailyRequestReward.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(createDailyRequestReward.Error!);
                }
            }
            await _mediator.Send(new SetPOActualDateCommand(projectOperationDetail.ProjectOperationId, request.StartDate), ct);

            await _unitOfWork.CommitAsync(ct);

            var statusChanger = await _mediator.Send(new ProjectStatusChangerCommand(projectOperationDetail.ProjectOperation.Project.Id,
                projectOperationDetail.ProjectOperation.Id, projectOperationDetail.Id, (ProjectStatus)request.Status, request.StatusDescription), ct);
            if (statusChanger.IsFailure)
                return Result.Failure<CreateDailyProjectOperationResponse>(statusChanger.Error!);

            await _unitOfWork.CommitAsync(ct);

            if (detailContractorServicesHaveProjectService.Count > 0)
                foreach (var service in detailContractorServicesHaveProjectService)
                {
                    var responseProjectService = await _mediator.Send(new UpdateProjectServiceDoneVolumeCommand(service.ProjectServiceDetail!.ProjectService.Id), ct);
                    if (statusChanger.IsFailure)
                        return Result.Failure<CreateDailyProjectOperationResponse>(statusChanger.Error!);
                }

            await _unitOfWork.CommitAsync(ct);

            var sendTelegramResult = await SendMessageCreateDailyProjectOperation(request, projectOperationDetail, createResponse, false, ct);
            if (sendTelegramResult.IsFailure)
                return Result.Failure<CreateDailyProjectOperationResponse>(sendTelegramResult.Error!);

            var projectOperation = projectOperationDetail.ProjectOperation;
            transaction.Complete();
            return new CreateDailyProjectOperationResponse(createResponse.Id, projectOperation!.Id, projectOperation.ProjectOperationStatus, projectOperation.ProjectOperationStatus.GetEnumDescription());
        }
    }

    public async Task<Result<UpdateDailyProjectOperationResponse?>> UpdateDailyProjectOperation(
        UpdateDailyProjectOperationRequest request, CT ct)
    {
        using (var transaction = new CreateTransaction())
        {
            var isValidRequest = await request.IsValidAsync<UpdateDailyProjectOperationValidator, UpdateDailyProjectOperationRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<UpdateDailyProjectOperationResponse>(isValidRequest.Error!);

            var dailyResponse = await _mediator.Send(new GetDailyProjectOperationByIdQuery(request.DailyProjectOperationId), ct);
            if (dailyResponse.IsFailure)
                return Result.Failure<UpdateDailyProjectOperationResponse>(dailyResponse.Error!);

            var dailyValue = dailyResponse.Value!;
            if (dailyValue is null)
                return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.ProjectOperationDetailWithIdNotFound);

            var checkDependency = await _mediator.Send(new ValidateProjectOperationDependencyQuery(dailyValue.ProjectOperationDetail.ProjectOperationId, request.StartDate, null, null, null), ct);
            if (checkDependency.IsBad())
                return checkDependency.Failure<UpdateDailyProjectOperationResponse?>();

            long? companyId = _userInfoService.UserCompanyId <= 0 ? null : _userInfoService.UserCompanyId;
            if (companyId is not null && companyId >= 1)
            {
                var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
                if (companyResponse.IsFailure)
                    return Result.Failure<UpdateDailyProjectOperationResponse>(companyResponse.Error!);
            }

            foreach (var document in dailyValue.DailyProjectOperationDocuments)
            {
                var deleteDailyDocument = await _mediator.Send(new DeleteDailyProjectOperationDocumentCommand(document.Id), ct);
                if (deleteDailyDocument.IsFailure)
                    return Result.Failure<UpdateDailyProjectOperationResponse>(deleteDailyDocument.Error!);
            }

            foreach (var product in dailyValue.DailyProjectOperationProducts)
            {
                var deleteDailyProduct = await _mediator.Send(new DeleteDailyProjectOperationProductCommand(product.Id), ct);
                if (deleteDailyProduct.IsFailure)
                    return Result.Failure<UpdateDailyProjectOperationResponse>(deleteDailyProduct.Error!);
            }

            foreach (var machineryModel in dailyValue.DailyProjectOperationMachineries)
            {
                var deleteDailyMachinery = await _mediator.Send(new DeleteDailyProjectOperationMachineryCommand(machineryModel.Id), ct);
                if (deleteDailyMachinery.IsFailure)
                    return Result.Failure<UpdateDailyProjectOperationResponse>(deleteDailyMachinery.Error!);
            }

            foreach (var expertModel in dailyValue.DailyProjectOperationExperts)
            {
                var deleteDailyExpert = await _mediator.Send(new DeleteDailyProjectOperationExpertCommand(expertModel.Id), ct);
                if (deleteDailyExpert.IsFailure)
                    return Result.Failure<UpdateDailyProjectOperationResponse>(deleteDailyExpert.Error!);
            }

            foreach (var requestRewardModel in dailyValue.DailyProjectOperationRequestRewards)
            {
                var deleteDailyRequestReward = await _mediator.Send(new DeleteDailyProjectOperationRequestRewardCommand(requestRewardModel.Id), ct);
                if (deleteDailyRequestReward.IsFailure)
                    return Result.Failure<UpdateDailyProjectOperationResponse>(deleteDailyRequestReward.Error!);
            }

            var projectOperationDetailQuery = await _mediator.Send(new GetProjectOperationDetailByIdForDailyQuery(dailyValue.ProjectOperationDetail.Id), ct);
            if (projectOperationDetailQuery.IsFailure)
                return Result.Failure<UpdateDailyProjectOperationResponse>(projectOperationDetailQuery.Error!);

            var projectOperationDetail = projectOperationDetailQuery.Value!;
            if (projectOperationDetail is null)
                return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.ProjectOperationDetailWithIdNotFound);
            if (projectOperationDetail.StartDate is null)
            {
                var responseDate = await _mediator.Send(new UpdatesProjectOperationDetailDateCommand(projectOperationDetail, request.StartDate, request.EndDate), ct);
                if (responseDate.IsFailure)
                    return Result.Failure<UpdateDailyProjectOperationResponse>(responseDate.Error!);
            }

            if (request.StartDate.Date < projectOperationDetail.StartDate!.Value.Date)
                return Result.Failure<UpdateDailyProjectOperationResponse>(ProjectOperationDetailErrors.DateTimeNotValidForDaily2);

            var updateDailyProjectOperationCommand = await _mediator.Send(new UpdateDailyProjectOperationCommand(request.DailyProjectOperationId, request.Status, request.StartDate, request.EndDate,
                request.Length, request.Width, request.Height, request.Weight, request.Number, request.Description, companyId), ct);
            if (updateDailyProjectOperationCommand.IsFailure)
                return Result.Failure<UpdateDailyProjectOperationResponse>(updateDailyProjectOperationCommand.Error!);

            var updateDaily = updateDailyProjectOperationCommand.Value!;
            if (request.DocumentUrls != null)
                foreach (var document in request.DocumentUrls!)
                {
                    var createDocument = await _mediator.Send(new CreateDailyProjectOperationDocumentCommand(document, updateDaily), ct);
                    if (createDocument.IsFailure)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(createDocument.Error!);
                }

            if (request.Products != null)
                foreach (var product in request.Products!)
                {
                    var consumableVolumeProduct = projectOperationDetail.ConsumableVolumeProducts.Where(c => c.Id.Equals(product.ConsumableVolumeProductId)).FirstOrDefault();
                    if (consumableVolumeProduct is null)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.ConsumableVolumeProductWithIdNotFound);

                    var getsProductByIdQuery = await _mediator.Send(new GetsProductByIdQuery(1, 1, null, [product.ProductId]), ct);
                    if (getsProductByIdQuery.IsFailure)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(getsProductByIdQuery.Error!);

                    if (getsProductByIdQuery.Value is null || getsProductByIdQuery.Value.RowCount != 1)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.ProductWithIdNotFound);

                    var createDocument = await _mediator.Send(new CreateDailyProjectOperationProductCommand(product.ProductId, product.FinalValue,
                        product.UnusedValue, updateDaily, consumableVolumeProduct), ct);
                    if (createDocument.IsFailure)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(createDocument.Error!);
                }

            List<ProjectOperationDetailContractorService>? detailContractorServicesHaveProjectService = new();
            if (request.ContractorServices != null)
            {
                foreach (var item in request.ContractorServices)
                {
                    if (item.IsDeleted && item.Id is not null)
                    {
                        var deleteDailyService = await _mediator.Send(new DeleteDailyProjectOperationServiceCommand(item.Id!.Value), ct);
                        if (deleteDailyService.IsFailure)
                            return Result.Failure<UpdateDailyProjectOperationResponse>(deleteDailyService.Error!);
                    }

                    if (!item.IsDeleted)
                    {
                        if (!string.IsNullOrEmpty(item.TimeSpant))
                        {
                            if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                                return Result.Failure<UpdateDailyProjectOperationResponse>(MachineryStandardErrors.TimeSpantCountError);

                            if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                                return Result.Failure<UpdateDailyProjectOperationResponse>(MachineryStandardErrors.MoreThan59Min);
                        }

                        var contractorService = projectOperationDetail.ProjectOperationDetailContractorServices.Where(c => c.Id.Equals(item.ContractorServiceId)).FirstOrDefault();
                        if (!item.IsDeleted && item.Id is not null)
                        {
                            contractorService = projectOperationDetail.ProjectOperationDetailContractorServices.Where(c => c.Id.Equals(item.ContractorServiceId)).FirstOrDefault();

                            var entity = dailyValue.DailyProjectOperationServices.FirstOrDefault(x => x.Id.Equals(item.Id));
                            if (entity is not null)
                            {
                                var createService = await _mediator.Send(new UpdateDailyProjectOperationServiceCommand(entity, contractorService, item.Volume, item.ProjectServiceVolume,
                                    TimeCalculator.StringToTicks(item.TimeSpant), item.IsActive), ct);
                                if (createService.IsFailure)
                                    return Result.Failure<UpdateDailyProjectOperationResponse>(createService.Error!);
                            }
                        }

                        if (item.Id is null)
                        {
                            if (contractorService is null)
                                return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.ContractorServiceWithIdNotFound);
                            if (!contractorService.IsActive)
                                return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.ContractorServiceIsNotActive);

                            var createService = await _mediator.Send(new CreateDailyProjectOperationServiceCommand(updateDaily,
                                contractorService,
                                item.Volume,
                                item.ProjectServiceVolume,
                                item.ThirdPartyId,
                                TimeCalculator.StringToTicks(item.TimeSpant),
                                item.IsActive), ct);
                            if (createService.IsFailure)
                                return Result.Failure<UpdateDailyProjectOperationResponse>(createService.Error!);
                        }

                        if (contractorService is not null)
                            if (contractorService.ProjectServiceDetail is not null)
                                detailContractorServicesHaveProjectService.Add(contractorService);
                    }
                }
            }

            if (request.Machineries != null)
                foreach (var machineryModel in request.Machineries!)
                {
                    var consumableVolumeMachinery = projectOperationDetail.ConsumableVolumeMachineries.Where(c => c.Id.Equals(machineryModel.ConsumableVolumeMachineryId)).FirstOrDefault();
                    if (consumableVolumeMachinery is null)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.ConsumableVolumeMachineryWithIdNotFound);

                    var getRequestMachineryByIdQuery = await _mediator.Send(new GetRequestMachineryByIdQuery(machineryModel.RequestMachineryId), ct);
                    if (getRequestMachineryByIdQuery.IsFailure)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(getRequestMachineryByIdQuery.Error!);
                    var requestMachinery = getRequestMachineryByIdQuery.Value!;

                    if ((requestMachinery.Unit == RequestMachineryUnit.Daily || requestMachinery.Unit == RequestMachineryUnit.Serviced) && machineryModel.Number == null)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.InvalidUnitNumber);

                    var finalValue = TimeCalculator.StringToTicks(machineryModel!.FinalValue);
                    var unusedValue = TimeCalculator.StringToTicks(machineryModel!.UnusedValue);
                    var createMachinery = await _mediator.Send(new CreateDailyProjectOperationMachineryCommand(
                        requestMachinery, finalValue, unusedValue, machineryModel.Number, updateDaily, consumableVolumeMachinery), ct);
                    if (createMachinery.IsFailure)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(createMachinery.Error!);
                }

            if (request.Experts != null)
                foreach (var model in request.Experts!)
                {
                    var consumableVolumeExpert = projectOperationDetail.ConsumableVolumeExperts.Where(c => c.Id.Equals(model.ConsumableVolumeExpertId)).FirstOrDefault();
                    if (consumableVolumeExpert is null)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.ConsumableVolumeExpertWithIdNotFound);

                    var GetWithSkillOnlyByIdsQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long>() { model.ThirdPartyId }, null, false, null), ct);
                    if (GetWithSkillOnlyByIdsQuery.IsFailure)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(GetWithSkillOnlyByIdsQuery.Error!);

                    if (GetWithSkillOnlyByIdsQuery.Value is null || GetWithSkillOnlyByIdsQuery.Value.RowCount != 1)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(DailyProjectOperationErrors.ProductWithIdNotFound);

                    var finalValue = TimeCalculator.StringToTicks(model!.FinalValue);
                    var unusedValue = TimeCalculator.StringToTicks(model!.UnusedValue);
                    var createExpert = await _mediator.Send(new CreateDailyProjectOperationExpertCommand(model.ThirdPartyId, finalValue, unusedValue,
                        updateDaily, consumableVolumeExpert), ct);
                    if (createExpert.IsFailure)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(createExpert.Error!);
                }

            if (request.RequestReward is not null)
            {
                var createRequestReward = await _requestRewardLogic.CreateRequestReward(request.RequestReward, ct);
                if (createRequestReward.IsFailure)
                    return Result.Failure<UpdateDailyProjectOperationResponse>(createRequestReward.Error!);

                var requestRewardIds = createRequestReward.Value!.Ids.ToList();
                foreach (var requestRewardId in requestRewardIds)
                {
                    var createDailyRequestReward = await _mediator.Send(new CreateDailyProjectOperationRequestRewardCommand(dailyValue, requestRewardId), ct);
                    if (createDailyRequestReward.IsFailure)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(createDailyRequestReward.Error!);
                }
            }

            await _mediator.Send(new SetPOActualDateCommand(projectOperationDetail.ProjectOperationId, request.StartDate), ct);

            await _unitOfWork.CommitAsync(ct);

            var statusChanger = await _mediator.Send(new ProjectStatusChangerCommand(projectOperationDetail.ProjectOperation.Project.Id,
                projectOperationDetail.ProjectOperation.Id, projectOperationDetail.Id, (ProjectStatus)request.Status, request.StatusDescription),
                ct);
            if (statusChanger.IsFailure)
                return Result.Failure<UpdateDailyProjectOperationResponse>(statusChanger.Error!);

            await _unitOfWork.CommitAsync(ct);

            if (detailContractorServicesHaveProjectService.Count > 0)
                foreach (var service in detailContractorServicesHaveProjectService)
                {
                    var responseProjectService = await _mediator.Send(new UpdateProjectServiceDoneVolumeCommand(service.ProjectServiceDetail!.ProjectService.Id), ct);
                    if (statusChanger.IsFailure)
                        return Result.Failure<UpdateDailyProjectOperationResponse>(statusChanger.Error!);
                }

            await _unitOfWork.CommitAsync(ct);

            var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
            if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
                await DailyProjectOperationMessageSender(request, updateDaily, projectOperationDetail, true, ct);

            var projectOperation = projectOperationDetail.ProjectOperation;
            transaction.Complete();
            return new UpdateDailyProjectOperationResponse(dailyValue.Id, projectOperation!.Id, projectOperation.ProjectOperationStatus, projectOperation.ProjectOperationStatus.GetEnumDescription());
        }
    }

    public async Task<Result<DailyProjectOperationTelegramMessageSenderResponse?>> DailyProjectOperationTelegramMessageSender(
        DailyProjectOperationTelegramMessageSenderRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DailyProjectOperationTelegramMessageSenderValidator, DailyProjectOperationTelegramMessageSenderRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DailyProjectOperationTelegramMessageSenderResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetDailyProjectOperationByIdQuery(request.Id), ct);
        if (responseGet.IsFailure || responseGet.Value is null)
            return Result.Failure<DailyProjectOperationTelegramMessageSenderResponse>(responseGet.Error!);
        var value = responseGet.Value!;

        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
            await MessageSender(value, value.ProjectOperationDetail, false, ct);

        return new DailyProjectOperationTelegramMessageSenderResponse(true);
    }

    public async Task<Result<UpdateDailyProjectOperationDocumentsResponse?>> UpdateDailyProjectOperationDocuments(
        UpdateDailyProjectOperationDocumentsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateDailyProjectOperationDocumentsValidator, UpdateDailyProjectOperationDocumentsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateDailyProjectOperationDocumentsResponse>(isValidRequest.Error!);

        var dailyResponse = await _mediator.Send(new GetDailyProjectOperationByIdQuery(request.Id), ct);
        if (dailyResponse.IsFailure)
            return Result.Failure<UpdateDailyProjectOperationDocumentsResponse>(dailyResponse.Error!);

        foreach (var document in dailyResponse.Value!.DailyProjectOperationDocuments)
        {
            var deleteDailyDocument = await _mediator.Send(new DeleteDailyProjectOperationDocumentCommand(document.Id), ct);
            if (deleteDailyDocument.IsFailure)
                return Result.Failure<UpdateDailyProjectOperationDocumentsResponse>(deleteDailyDocument.Error!);
        }

        if (request.DocumentUrls != null)
            foreach (var document in request.DocumentUrls!)
            {
                var createDocument = await _mediator.Send(new CreateDailyProjectOperationDocumentCommand(document, dailyResponse.Value!), ct);
                if (createDocument.IsFailure)
                    return Result.Failure<UpdateDailyProjectOperationDocumentsResponse>(createDocument.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateDailyProjectOperationDocumentsResponse(dailyResponse.Value!.Id);
    }

    public async Task<Result<DeleteDailyProjectOperationResponse?>> DeleteDailyProjectOperation(
        DeleteDailyProjectOperationRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteDailyProjectOperationValidator, DeleteDailyProjectOperationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteDailyProjectOperationResponse>(isValidRequest.Error!);

        var deleteDailyDocument = await _mediator.Send(new DeleteDailyProjectOperationCommand(request.Id), ct);
        if (deleteDailyDocument.IsFailure)
            return Result.Failure<DeleteDailyProjectOperationResponse>(deleteDailyDocument.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteDailyProjectOperationResponse(true);
    }

    public async Task<Result<GetDailyProjectOperationByIdResponse?>> GetDailyProjectOperationById(
        GetDailyProjectOperationByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetDailyProjectOperationByIdValidator, GetDailyProjectOperationByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetDailyProjectOperationByIdResponse>(isValidRequest.Error!);

        var getDailyProjectOperationByIdQuery = await _mediator.Send(new GetDailyProjectOperationByIdQuery(request.DailyProjectOperationId), ct);
        if (getDailyProjectOperationByIdQuery.IsFailure)
            return Result.Failure<GetDailyProjectOperationByIdResponse>(getDailyProjectOperationByIdQuery.Error!);
        var dailyProjectOperation = getDailyProjectOperationByIdQuery.Value!;

        Company? company = null;
        if (dailyProjectOperation.CompanyId is not null && dailyProjectOperation.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(dailyProjectOperation.CompanyId, _mediator, ct);

        var documents = new List<GetDailyProjectOperationByIdDocumentModel>();
        foreach (var document in dailyProjectOperation.DailyProjectOperationDocuments)
        {
            documents.Add(new GetDailyProjectOperationByIdDocumentModel()
            {
                Id = document.Id,
                Url = document.Url,
            });
        }

        var machineries = new List<GetDailyProjectOperationByIdMachineryModel>();
        var dailyMachineries = dailyProjectOperation.DailyProjectOperationMachineries.Select(oo => oo.RequestMachinery).Select(oo => oo.Machinery).Distinct().ToList();
        foreach (var machinery in dailyMachineries)
        {
            var requestMachineries = new List<GetDailyProjectOperationByIdRequestMachineryModel>();

            var dailyOperationMachineries = dailyProjectOperation.DailyProjectOperationMachineries.Where(oo => oo.RequestMachinery.Machinery!.Id.Equals(machinery?.Id)).ToList();

            var finalValue = dailyOperationMachineries.FirstOrDefault()!.ConsumableVolumeMachinery!.FinalValue;
            var consumableVolumeMachineryId = dailyOperationMachineries.FirstOrDefault()!.ConsumableVolumeMachinery!.Id;

            foreach (var dailyOperationMachinery in dailyOperationMachineries)
            {
                var machineryFinalValue = TimeCalculator.TicksToStringHM(dailyOperationMachinery.FinalValue!);
                var unusedValue = TimeCalculator.TicksToStringHM(dailyOperationMachinery.UnusedValue);
                requestMachineries.Add(new GetDailyProjectOperationByIdRequestMachineryModel()
                {
                    Id = dailyOperationMachinery.Id,
                    RequestMachineryId = dailyOperationMachinery.RequestMachinery.Id,
                    RequestNumber = dailyOperationMachinery.RequestMachinery.Id,
                    FinalValue = machineryFinalValue,
                    UnusedValue = unusedValue,
                    Unit = dailyOperationMachinery.RequestMachinery.Unit,
                    Number = dailyOperationMachinery.Number
                });
            }

            var model = new GetDailyProjectOperationByIdMachineryModel()
            {
                MachineryId = machinery!.Id,
                MachineryName = machinery.MachineryName,
                MachineryCode = machinery.MachineryCode,
                MachineryGroupId = machinery.MachineriesGroup.Id,
                MachineryGroupName = machinery.MachineriesGroup.GroupName,
                FinalValue = finalValue.ToString(),
                RequestMachineries = requestMachineries,
                ConsumableVolumeMachineryId = consumableVolumeMachineryId,
            };
            machineries.Add(model);
        }

        var services = new List<GetDailyProjectOperationByIdSeviceModel>();
        if (dailyProjectOperation.DailyProjectOperationServices != null && dailyProjectOperation.DailyProjectOperationServices.Count > 0)
        {
            var contractorIds = dailyProjectOperation.DailyProjectOperationServices.Where(x => x.ContractorId is not null).Select(c => (long)c.ContractorId!).ToList();
            var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);


            var serviceThirdPartyIds = dailyProjectOperation.DailyProjectOperationServices.Where(x => x.ThirdPartyId is not null).Select(c => (long)c.ThirdPartyId!).ToList();
            var serviceThirdParty = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(serviceThirdPartyIds, null, null, _mediator, ct);

            var unitOfMeasurementIds = dailyProjectOperation.DailyProjectOperationServices.Select(c => c.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId).ToList();
            var unitOfMeasurements = await WebServicesLogic.MeasurementDataReceiver(unitOfMeasurementIds, _mediator, ct);

            foreach (var service in dailyProjectOperation.DailyProjectOperationServices)
            {
                UserModel? contractor = null;
                if (contractors is not null)
                    contractor = contractors?.Where(c => c!.Id.Equals(service.ContractorId)).FirstOrDefault();
                UserModel? thirdParty = null;
                if (serviceThirdParty is not null)
                    thirdParty = serviceThirdParty?.Where(c => c!.Id.Equals(service.ThirdPartyId)).FirstOrDefault();

                var unitOfMeasurement = unitOfMeasurements?.Where(c => c.Id.Equals(service.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId)).FirstOrDefault();
                services.Add(new GetDailyProjectOperationByIdSeviceModel()
                {
                    Id = service.Id,
                    Volume = service.Volume,
                    ServiceInfoCode = service.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                    ServiceInfoName = service.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                    ContractorId = service.ContractorId,
                    Contractor = contractor?.FullName,
                    UnitOfMeasurement = unitOfMeasurement!.Name,
                    UnitOfMeasurementId = unitOfMeasurement!.Id,
                    ContractorServiceId = service.ProjectOperationDetailContractorService.Id,
                    ThirdPartyId = service.ThirdPartyId,
                    ThirdParty = thirdParty?.FullName,
                    ProjectServiceVolume = service.ProjectServiceVolume,
                    ProjectServiceSumVolume = service.ProjectOperationDetailContractorService?.ProjectServiceDetail?.ProjectService.Volume,
                    ProjectServiceSumDoneVolume = service.ProjectOperationDetailContractorService?.ProjectServiceDetail?.ProjectService.DoneVolume,
                    ProjectServiceSumRemaindDoneVolume = service.ProjectOperationDetailContractorService?.ProjectServiceDetail?.ProjectService.RemainderVolume,
                    TimeSpant = service.TimeSpant is null ? null : TimeCalculator.TicksToStringHM((long)service.TimeSpant!),
                    IsActive = service.IsActive,
                });
            }
        }

        var groupIds = dailyProjectOperation.DailyProjectOperationProducts.Select(c => c.ConsumableVolumeProduct.ProductGroupId).ToList();
        var products = new List<GetDailyProjectOperationProduct>();
        if (groupIds != null && groupIds.Count > 0)
        {
            var getGroup = await WebServicesLogic.GroupsDataReceiver(groupIds, _mediator, ct);

            foreach (var product in dailyProjectOperation.DailyProjectOperationProducts)
            {
                var getProductQuery = await _mediator.Send(new GetProductByIdQuery(product.ProductId), ct);
                var getProduct = getProductQuery.Value;

                var group = getGroup?.Where(c => c.Id.Equals(product.ConsumableVolumeProduct.ProductGroupId)).FirstOrDefault();
                products.Add(new GetDailyProjectOperationProduct()
                {
                    ProductId = product.ProductId,
                    ProductName = getProduct?.Name,
                    FinalValue = product.FinalValue,
                    UnusedValue = product.UnusedValue,
                    ProductGroupId = product.ConsumableVolumeProduct.ProductGroupId,
                    ProductGroupName = group?.Name,
                    ConsumableVolumeProductId = product.ConsumableVolumeProduct.Id
                });
            }
        }

        var thirdPartyIds = dailyProjectOperation.DailyProjectOperationExperts.Select(c => c.ThirdPartyId).ToList();
        var experts = new List<GetDailyProjectOperationByIdExpertModel>();
        if (thirdPartyIds != null && thirdPartyIds.Count > 0)
        {
            var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

            foreach (var expert in dailyProjectOperation.DailyProjectOperationExperts)
            {
                var skill = await WebServicesLogic.SkillDataReceiver(expert.ConsumableVolumeExpert.ExpertId, _mediator, ct);

                var finalValue = TimeCalculator.TicksToStringHM(expert.FinalValue!);
                var unusedValue = TimeCalculator.TicksToStringHM(expert.UnusedValue);

                UserModel? thirdParty = null;
                if (thirdParties is not null)
                    thirdParty = thirdParties?.Where(c => c!.Id.Equals(expert.ThirdPartyId)).FirstOrDefault();

                experts.Add(new GetDailyProjectOperationByIdExpertModel()
                {
                    Id = expert.Id,
                    FinalValue = finalValue,
                    UnusedValue = unusedValue,
                    ConsumableVolumeExpertId = expert.ConsumableVolumeExpert.Id,
                    ThirdPartyId = expert.ThirdPartyId,
                    ThirdParty = thirdParty?.FullName,
                    SkillId = skill?.Id,
                    Skill = skill?.Name,
                });
            }
        }

        var requestRewardIds = dailyProjectOperation.DailyProjectOperationRequestRewards.Select(c => c.RequestRewardId).ToList();
        var requestRewardModels = new List<GetDailyProjectOperationRequestRewardModel>();
        if (requestRewardIds != null && requestRewardIds.Count > 0)
        {
            var getRequestRewardsQuery = await _mediator.Send(new GetRequestRewardsByIdsQuery(requestRewardIds), ct);
            var requestRewards = getRequestRewardsQuery.Value?.Data;

            var requestRewardProducts = await ProductDataReceiver(requestRewards, ct);
            var requestRewardCurrencies = await CurrencyDataReceiver(requestRewards, ct);
            var requestRewardThirdParties = await ThirdPartyDataReceiver(requestRewards, ct);

            if (requestRewards is not null && requestRewards.Count > 0)
            {
                foreach (var item in requestRewards)
                {
                    var requestRewardProductModels = new List<GetRequestRewardProductModel>();
                    foreach (var item1 in item.RequestRewardProducts)
                    {
                        var product = requestRewardProducts?.Where(m => m.Id == item1.ProductId).FirstOrDefault();
                        var productCurrency = requestRewardCurrencies?.Where(m => m.Id == item1.CurrencyId).FirstOrDefault();
                        requestRewardProductModels.Add(new GetRequestRewardProductModel()
                        {
                            Id = item1.Id,
                            Count = item1.Count,
                            CurrencyId = productCurrency!.Id,
                            ProductId = product!.Id,
                            Price = item1.Price,
                            Currency = productCurrency.Name,
                            ProductName = product.Name,
                        });
                    }

                    List<GetRequestRewardDocumentModel> requestRewardDocuments = [];
                    foreach (var item1 in item.RequestRewardDocuments)
                    {
                        requestRewardDocuments.Add(new()
                        {
                            Id = item1.Id,
                            Url = item1.Url,
                        });
                    }

                    var requestRewardThirdPartyModels = new List<GetRequestRewardThirdPartyModel>();
                    foreach (var item1 in item.RequestRewardThirdParties)
                    {
                        var thirdParty = requestRewardThirdParties?.Where(m => m.Id == item1.ThirdPartyId).FirstOrDefault();
                        requestRewardThirdPartyModels.Add(new GetRequestRewardThirdPartyModel()
                        {
                            Id = item1.Id,
                            ThirdPartyId = thirdParty!.Id,
                            ThirdParty = thirdParty?.FirstName + " " + thirdParty?.LastName
                        });
                    }

                    var currency = requestRewardCurrencies?.Where(m => m.Id == item.CurrencyId).FirstOrDefault();
                    var modelingData = RequestRewardFullModeling(dailyProjectOperation, item, requestRewardProductModels, requestRewardDocuments, requestRewardThirdPartyModels, currency);
                    requestRewardModels.Add(modelingData.Adapt<GetDailyProjectOperationRequestRewardModel>());
                }
            }
        }

        var responseData = DailyProjectOperationFullModeling(dailyProjectOperation, documents, experts, machineries, services, products, requestRewardModels, company);

        var userInfos = await WebServicesLogic.UserDataReceiver([responseData.CreatorId], null, _mediator, ct); // بره سراغ متا دیتا
        responseData.CreatorName = userInfos?.FirstOrDefault(x => x.UserId.Equals(responseData.CreatorId))?.FullName;
        responseData.CreatorNickname = userInfos?.FirstOrDefault(x => x.UserId.Equals(responseData.CreatorId))?.Nickname;

        return responseData.Adapt<GetDailyProjectOperationByIdResponse>();
    }

    public async Task<Result<GetDailyProjectOperationByLegacyIdResponse?>> GetDailyProjectOperationByLegacyId(
        GetDailyProjectOperationByLegacyIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetDailyProjectOperationByLegacyIdValidator, GetDailyProjectOperationByLegacyIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetDailyProjectOperationByLegacyIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetDailyProjectOperationByLegacyIdQuery(request.LegacyId), ct);
        if (response.IsFailure)
            return Result.Failure<GetDailyProjectOperationByLegacyIdResponse>(response.Error!);

        return response.Value!.Adapt<GetDailyProjectOperationByLegacyIdResponse>();
    }

    public async Task<Result<GetDailyProjectOperationHistoriesResponse?>> GetDailyProjectOperationHistories(
        GetDailyProjectOperationHistoriesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetDailyProjectOperationHistoriesValidator, GetDailyProjectOperationHistoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetDailyProjectOperationHistoriesResponse>(isValidRequest.Error!);

        var projectOperationDetailQuery = await _mediator.Send(new GetProjectOperationDetailByIdLessIncludeQuery(request.ProjectOperationDetailId), ct);
        if (projectOperationDetailQuery.IsFailure)
            return Result.Failure<GetDailyProjectOperationHistoriesResponse>(projectOperationDetailQuery.Error!);
        if (projectOperationDetailQuery.Value is null)
            return Result.Failure<GetDailyProjectOperationHistoriesResponse>(DailyProjectOperationErrors.ProjectOperationDetailWithIdNotFound);
        var projectOperationDetail = projectOperationDetailQuery.Value;

        var getFilteredResponse = await _mediator.Send(new GetDailyProjectOperationDetailsQuery(request.ProjectOperationDetailId,
            request.ContractorId, request.Length, request.Width, request.Height, request.Weight, request.Number, request.StartDate,
            request.EndDate, request.CreatorId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize),
            ct);
        if (getFilteredResponse.IsFailure)
            return Result.Failure<GetDailyProjectOperationHistoriesResponse>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        if (getFilteredResponse.Value!.Data is null)
            return Result.Failure<GetDailyProjectOperationHistoriesResponse>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        var values = getFilteredResponse.Value?.Data;

        var measureunit = await WebServicesLogic.MeasureUnitDataReceiver(projectOperationDetail.ProjectOperation.UnitOfMeasurementId, _mediator, ct);

        var contractorIds = values?.SelectMany(x => x.DailyProjectOperationServices)
            .Where(x => x.ProjectOperationDetailContractorService is not null && x.ContractorId is not null && x.ContractorId > 0)
                .Select(x => x.ContractorId!.Value).ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var userIds = values!.Where(x => x.CreatorId > 0).Select(x => x.CreatorId).ToList();
        var userInfos = await WebServicesLogic.UserDataReceiver(userIds.Distinct().ToList(), null, _mediator, ct); // بره سراغ متا دیتا

        var deductionsAmount = values!.FirstOrDefault()!.ProjectOperationDetail.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList().Sum(x => x);
        var totals = new GetTotalDailyProjectOperationHistoriesDetailModel()
        {
            TotalHeights = values!.Sum(x => x.Height),
            TotalLengths = values!.Sum(x => x.Length),
            TotalNumbers = values!.Sum(x => x.Number),
            TotalWidths = values!.Sum(x => x.Width),
            TotalWeights = values!.Sum(x => x.Weight),
            TotalAmounts = values!.Sum(x => x.FinalAmount),
            ProjectOperationDetailFinalAmount = values!.FirstOrDefault()!.ProjectOperationDetail.FinalAmount - deductionsAmount,
            TotalDeductionFinalAmount = deductionsAmount,
            TotalProjectOperationDetailFinalAmount = values!.FirstOrDefault()!.ProjectOperationDetail.FinalAmount,
            ProjectOperationWorkload = values!.FirstOrDefault()!.ProjectOperationDetail.ProjectOperation.Workload
        };

        var dataResult = values.Adapt<List<GetDailyProjectOperationHistoriesDetailModel>>();
        foreach (var item in dataResult!)
        {
            var dailyServices = values?.FirstOrDefault(x => x.Id.Equals(item.DailyProjectOperationId))?.DailyProjectOperationServices?.ToList();
            if (dailyServices?.Count > 0)
            {
                var serviceNames = dailyServices
                    .Select(x => new
                    {
                        ServiceName = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                        ServiceVolume = x.ProjectOperationDetailContractorService.Volume
                    })
                    .Where(service => !string.IsNullOrEmpty(service.ServiceName))
                    .ToList();

                List<string>? serviceData = [];
                if (serviceNames is not null && serviceNames.Count > 0)
                    foreach (var service in serviceNames)
                        serviceData.Add(service.ServiceName + "(" + service.ServiceVolume.ToString() + ")");

                item.Services = string.Join(" - ", serviceData);
            }

            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            if (contractors is not null && contractors.Any())
            {
                var contractorsIds = dailyServices?.Where(x => x.ContractorId.HasValue && x.ContractorId > 0).Select(x => x.ContractorId).ToList();
                if (contractorsIds?.Count > 0)
                {
                    var contractorInfos = contractors.Where(x => contractorsIds.Contains(x!.Id)).ToList();

                    item.Contractors = string.Join(" - ", contractorInfos.Select(x => x!.FullName).ToList());
                    item.Nicknames = string.Join(" - ", contractorInfos.Select(x => x!.Nickname).ToList());
                }
            }

            item.CreatorName = userInfos?.FirstOrDefault(x => x.UserId.Equals(item.CreatorId))?.FullName;
            item.CreatorNickname = userInfos?.FirstOrDefault(x => x.UserId.Equals(item.CreatorId))?.Nickname;
            item.CompanyNameFa = company?.NameFa;
            item.UnitOfMeasurement = measureunit?.Name;
            item.UnitOfMeasurementId = measureunit?.Id;
        }

        return new GetDailyProjectOperationHistoriesResponse(totals, dataResult, getFilteredResponse.Value!.RowCount);
    }

    public async Task<Result<GetFilteredDailyProjectOperationDetailsResponse?>> GetFilteredDailyProjectOperationDetails(
        GetFilteredDailyProjectOperationDetailsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredDailyProjectOperationDetailsValidator, GetFilteredDailyProjectOperationDetailsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredDailyProjectOperationDetailsResponse>(isValidRequest.Error!);

        var projectOperationDetailsQuery = await _mediator.Send(new GetFilteredDailyProjectOperationDetailsQuery(request.ProjectOperationId, request.Status, request.StartDate, request.EndDate,
            request.CreateDate, request.OperationLocationIds, request.ServiceInfoIds, request.ContractorId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (projectOperationDetailsQuery.IsFailure)
            return Result.Failure<GetFilteredDailyProjectOperationDetailsResponse>(projectOperationDetailsQuery.Error!);
        var responses = projectOperationDetailsQuery.Value!.Data!;

        var creatorIds = responses.Select(x => x.CreatorId).Where(x => x > 0).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var Ids = responses.Where(x => x.ProjectOperationDetailContractorServices is not null && x.ProjectOperationDetailContractorServices.Count > 0)
            .SelectMany(x => x.ProjectOperationDetailContractorServices).Where(x => x.ContractorId != null && x.ContractorId > 0)
            .Select(x => (long)x.ContractorId!).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(Ids, null, null, _mediator, ct);

        var totals = new TotalDailyProjectOperationDetailDataModel()
        {
            TotalHeights = responses.Sum(x => x.Height),
            TotalLengths = responses.Sum(x => x.Length),
            TotalNumbers = responses.Sum(x => x.Number),
            TotalWidths = responses.Sum(x => x.Width),
            TotalWeights = responses.Sum(x => x.Weight),
            TotalFinalAmounts = responses.Sum(x => x.FinalAmount),
            TotalAmounts = responses.Sum(x => x.FinalAmount) - responses.Where(x => x.ProjectOperationDetailDeductions is not null && x.ProjectOperationDetailDeductions.Count > 0)
                    .Sum(x => x.ProjectOperationDetailDeductions!.Select(a => a.FinalAmount).ToList().Sum(a => a)),
            TotalDeductionAmounts = responses.Where(x => x.ProjectOperationDetailDeductions is not null && x.ProjectOperationDetailDeductions.Count > 0)
                    .Sum(x => x.ProjectOperationDetailDeductions!.Select(a => a.FinalAmount).ToList().Sum(a => a)),
            DailyFinalAmounts = responses.Sum(x => x.DailyOperations?.Select(z => z.FinalAmount).ToList().Sum(d => d)),
            ProjectOperationWorkload = responses.FirstOrDefault()?.ProjectOperation.Workload,
        };

        var dataResult = new List<GetFilteredDailyProjectOperationDetailsModel>();
        foreach (var item in responses)
        {
            var creator = creators?.Where(x => x.UserId == item.CreatorId).FirstOrDefault()?.FullName;

            var contractorIds = item.ProjectOperationDetailContractorServices.Where(x => x.ContractorId != null && x.ContractorId > 0)
                 .Select(x => (long)x.ContractorId!).Distinct().ToList();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var contractor = "";
            var contractorNickName = "";
            if (contractorIds is not null && contractorIds.Count > 0)
            {
                var contractorFullNames = contractors?.Where(x => contractorIds.Contains(x.Id)).Select(x => x!.FullName).ToList();
                var contractorNicknames = contractors?.Where(x => contractorIds.Contains(x.Id)).Select(x => x!.Nickname).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (contractorFullNames is not null && contractorFullNames.Any())
                    contractor = string.Join(" - ", contractorFullNames!);
                if (contractorNicknames is not null && contractorNicknames.Any())
                    contractorNickName = string.Join(" - ", contractorNicknames!);
            }

            var deductionAmounts = item.ProjectOperationDetailDeductions is not null && item.ProjectOperationDetailDeductions.Count > 0 ?
                item.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList().Sum(x => x) : 0;
            var data = new GetFilteredDailyProjectOperationDetailsModel()
            {
                ProjectOperationDetailId = item.Id,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Status = (ProjectOperationDetailStatus)item.Status!,
                PrivateName = item.OperationLocation.PrivateName,
                PrivateCode = item.OperationLocation.PrivateCode,
                PublicName = item.OperationLocation.PublicName,
                PublicCode = item.OperationLocation.PublicCode,
                FinalAmount = item.FinalAmount - deductionAmounts,
                DoneAmount = item.DailyOperations.Count > 0 ? item.DailyOperations.Sum(c => c.FinalAmount) : 0,
                Length = item.Length,
                LengthChangeable = item.LengthChangeable,
                Width = item.Width,
                WidthChangeable = item.WidthChangeable,
                Height = item.Height,
                HeightChangeable = item.HeightChangeable,
                Weight = item.Weight,
                WeightChangeable = item.WeightChangeable,
                Number = item.Number,
                NumberChangeable = item.NumberChangeable,
                Description = item.Description,
                CreatorId = item.CreatorId,
                Creator = creator,
                Contractors = contractor,
                ContractorNickNames = contractorNickName
            };
            dataResult.Add(data);
        }

        var deductionFinalAmounts = responses.Where(x => x.ProjectOperationDetailDeductions is not null && x.ProjectOperationDetailDeductions.Count > 0)
            .SelectMany(x => x.ProjectOperationDetailDeductions).Select(x => x.FinalAmount).ToList().Sum(f => f);

        return new GetFilteredDailyProjectOperationDetailsResponse(totals, dataResult, projectOperationDetailsQuery.Value!.RowCount!);
    }

    public async Task<Result<GetFilteredDailyProjectOperationsResponse?>> GetFilteredDailyProjectOperations(
        GetFilteredDailyProjectOperationsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredDailyProjectOperationsValidator, GetFilteredDailyProjectOperationsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredDailyProjectOperationsResponse>(isValidRequest.Error!);

        var projectOperationsQuery = await _mediator.Send(new GetFilteredDailyProjectOperationsQuery(request.CostCenterId, request.ProjectId, request.ContractorId, request.StartDate,
            request.EndDate, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (projectOperationsQuery.IsFailure)
            return Result.Failure<GetFilteredDailyProjectOperationsResponse>(projectOperationsQuery.Error!);
        var projectOperationsData = projectOperationsQuery.Value?.Data;

        var unitOfMeasurementIds = projectOperationsData?.Select(c => c.UnitOfMeasurementId).Distinct().ToList();
        var unitOfMeasurements = await WebServicesLogic.MeasurementDataReceiver(unitOfMeasurementIds, _mediator, ct);

        var dataResult = new List<GetDailyProjectOperationModel>();
        foreach (var item in projectOperationsData!)
        {
            var data = new GetDailyProjectOperationModel()
            {
                ProjectOperationId = item.Id,
                OperationInfoId = item.OperationInfo.Id,
                OperationInfoName = item.OperationInfo.OperationInfoName,
                OperationInfoCode = item.OperationInfo.OperationInfoCode,
                UnitOfMeasurement = unitOfMeasurements?.Where(x => x.Id == item.UnitOfMeasurementId).FirstOrDefault()?.Name,
                WorkLoad = item.Workload,
                Description = item.Description,
                StartDate = item.ProjectOperationDetails.Count > 0 ? item.ProjectOperationDetails!.Min(c => c.StartDate) : null,
                EndDate = item.ProjectOperationDetails.Count > 0 ? item.ProjectOperationDetails!.Max(c => c.EndDate) : null,
                Status = item.ProjectOperationStatus,
                DoneAmount = item.ProjectOperationDetails.Count > 0 ? (item.ProjectOperationDetails.SelectMany(c => c.DailyOperations).Count() > 0 ? item.ProjectOperationDetails.SelectMany(c => c.DailyOperations).Sum(c => c.FinalAmount) : 0) : 0,
            };
            dataResult.Add(data);
        }

        return new GetFilteredDailyProjectOperationsResponse(dataResult, projectOperationsQuery.Value!.RowCount!);
    }

    public async Task<Result<GetFilteredProjectOperationsByProjectIdsResponse?>> GetFilteredProjectOperationsByProjectIds(
        GetFilteredProjectOperationsByProjectIdsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredProjectOperationsByProjectIdsValidator, GetFilteredProjectOperationsByProjectIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredProjectOperationsByProjectIdsResponse>(isValidRequest.Error!);

        var projectOperationsQuery = await _mediator.Send(new GetFilteredProjectOperationsByProjectIdsQuery(request.ProjectIds, request.CostCenterId, request.ContractorId, request.StartDate,
            request.EndDate, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (projectOperationsQuery.IsFailure)
            return Result.Failure<GetFilteredProjectOperationsByProjectIdsResponse>(projectOperationsQuery.Error!);
        var projectOperationsData = projectOperationsQuery.Value?.Data;

        var unitOfMeasurementIds = projectOperationsData?.Select(c => c.UnitOfMeasurementId).Distinct().ToList();
        var unitOfMeasurements = await WebServicesLogic.MeasurementDataReceiver(unitOfMeasurementIds, _mediator, ct);

        var dataResult = new List<GetProjectOperationsByProjectIdsModel>();
        foreach (var item in projectOperationsData!)
        {
            var data = new GetProjectOperationsByProjectIdsModel()
            {
                ProjectOperationId = item.Id,
                OperationInfoId = item.OperationInfo.Id,
                OperationInfoName = item.OperationInfo.OperationInfoName,
                OperationInfoCode = item.OperationInfo.OperationInfoCode,
                UnitOfMeasurement = unitOfMeasurements?.Where(x => x.Id == item.UnitOfMeasurementId).FirstOrDefault()?.Name,
                WorkLoad = item.Workload,
                Description = item.Description,
                StartDate = item.ProjectOperationDetails.Count > 0 ? item.ProjectOperationDetails!.Min(c => c.StartDate) : null,
                EndDate = item.ProjectOperationDetails.Count > 0 ? item.ProjectOperationDetails!.Max(c => c.EndDate) : null,
                Status = item.ProjectOperationStatus,
                DoneAmount = item.ProjectOperationDetails.Count > 0 ? (item.ProjectOperationDetails.SelectMany(c => c.DailyOperations).Count() > 0 ? item.ProjectOperationDetails.SelectMany(c => c.DailyOperations).Sum(c => c.FinalAmount) : 0) : 0,
                ProjectId = item.Project.Id,
                Project = item.Project.ProjectName,
                CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                CostCenter = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
            };
            dataResult.Add(data);
        }

        return new GetFilteredProjectOperationsByProjectIdsResponse(dataResult, projectOperationsQuery.Value!.RowCount!);
    }

    public async Task<Result<GetsDetailedDailyProjectOperationResponse?>> GetsDetailedDailyProjectOperation(
        GetsDetailedDailyProjectOperationRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsDetailedDailyProjectOperationValidator, GetsDetailedDailyProjectOperationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsDetailedDailyProjectOperationResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsDetailedDailyProjectOperationQuery(null,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.ContractorIds,
            request.ServiceInfoIds,
            request.CreatorIds,
            request.StartDate,
            request.EndDate,
            request.FromDate,
            request.ToDate,
            request.ProjectOperationDetailStatus,
            request.Status,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize),
            ct);
        if (responses.IsFailure)
            return Result.Failure<GetsDetailedDailyProjectOperationResponse>(responses.Error!);
        var values = responses.Value?.Data;

        List<long>? measureIds = [];
        var measurementIds = values?.Select(c => c.UnitOfMeasurementId).Distinct().ToList();
        var servicemeasurementIds = values?.Where(x => x.services != null && x.services.Count > 0).SelectMany(x => x.services!).ToList().Select(x => x.ServiceInfoMeasureId).Distinct().ToList();
        measureIds.AddRange(measurementIds ?? []);
        measureIds.AddRange(servicemeasurementIds ?? []);
        var measurements = await WebServicesLogic.MeasurementDataReceiver(measureIds.Distinct().ToList(), _mediator, ct);

        var contractorIds = values?.SelectMany(c => c.ContractorIds.Where(x => x.HasValue && x.Value > 0).Select(x => (long)x!)).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var creatorIds = values?.Select(c => c.CreatorId).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        values!.ForEach(oo =>
        {
            var measurement = measurements?.FirstOrDefault(x => x.Id.Equals(oo.UnitOfMeasurementId));
            oo.MeasurementName = measurement?.Name;

            var creator = creators?.FirstOrDefault(x => x.UserId.Equals(oo.CreatorId));
            oo.CreatorName = creator?.FullName;
            oo.CreatorNickname = creator?.Nickname;

            if (oo.ContractorIds.Count > 0)
            {
                var contractorFullNames = contractors?.Where(x => oo.ContractorIds.Contains(x?.Id)).Select(x => x!.FullName).ToList();
                var contractorNicknames = contractors?.Where(x => oo.ContractorIds.Contains(x?.Id)).Select(x => x!.Nickname).ToList();
                if (contractorFullNames is not null && contractorFullNames.Any())
                    oo.Contractors = string.Join(" - ", contractorFullNames!);
                if (contractorNicknames is not null && contractorNicknames.Any())
                    oo.ContractorsNickName = string.Join(" - ", contractorNicknames!);
            }

            if (oo.services != null && oo.services.Count > 0)
                foreach (var item in oo.services)
                {
                    var measure = measurements?.FirstOrDefault(x => x.Id.Equals(item.ServiceInfoMeasureId))?.Name;
                    oo.ServiceInfos = string.Join(" - ", item.ServiceInfoName + "(" + measure + ")");
                }

            if (oo.DeductionAmounts is not null && oo.DeductionAmounts.Count > 0)
                oo.ProjectOperationDetailFinalAmount = oo.ProjectOperationDetailFinalAmount - oo.DeductionAmounts.Sum(x => x);
            else
                oo.ProjectOperationDetailFinalAmount = oo.ProjectOperationDetailFinalAmount - 0;
        });

        return new GetsDetailedDailyProjectOperationResponse(values!, responses.Value!.RowCount!);
    }

    public async Task<Result<GetsDailyProjectOperationDocumentResponse?>> GetsDailyProjectOperationDocument(
        GetsDailyProjectOperationDocumentRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsDailyProjectOperationDocumentValidator, GetsDailyProjectOperationDocumentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsDailyProjectOperationDocumentResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsDailyProjectOperationDocumentQuery(request.DailyProjectOperationId), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsDailyProjectOperationDocumentResponse>(responses.Error!);

        return responses.Value!;
    }

    public async Task<Result<GetFilteredMachineriesResponse?>> GetFilteredMachineries(
        GetFilteredMachineriesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredMachineriesValidator, GetFilteredMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredMachineriesResponse>(isValidRequest.Error!);

        var getFilteredQuery = await _mediator.Send(new GetFilteredRequestMachineryProjectOperationDetailsQuery(request.ProjectOperationDetailId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getFilteredQuery.IsFailure)
            return Result.Failure<GetFilteredMachineriesResponse>(getFilteredQuery.Error!);
        var requestMachineriesResponse = getFilteredQuery.Value!.Data!;

        var machineries = requestMachineriesResponse.Distinct().ToList();

        var contractorIds = machineries.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var response = new List<GetFilteredMachineriesResponseModel>();
        foreach (var machinery in machineries)
        {
            if (response.Count <= 0)
            {
                var number = requestMachineriesResponse?
                    .SelectMany(x => x.Project.ProjectOperations)
                    .SelectMany(z => z.ProjectOperationDetails)
                    .SelectMany(y => y.ConsumableVolumeMachineries)
                    .Where(oo => oo.Machinery.Id.Equals(machinery.Machinery.Id)).FirstOrDefault()?.Number;

                var consumableVolumeMachineryId = requestMachineriesResponse?
                    .SelectMany(x => x.Project.ProjectOperations)
                    .SelectMany(z => z.ProjectOperationDetails)
                    .SelectMany(y => y.ConsumableVolumeMachineries)
                    .Where(oo => oo.Machinery.Id.Equals(machinery.Machinery.Id)).FirstOrDefault()?.Id;

                var requestMachineries = new List<GetFilteredRequestMachineriesResponseModel>();
                foreach (var item in machinery.RequestMachineryAssignments)
                    requestMachineries.Add(new GetFilteredRequestMachineriesResponseModel()
                    {
                        RequestMachineryId = item.RequestMachinery.Id,
                        RequestNumber = item.RequestMachinery.Id,
                        MachineryIdentifier = item.MachineryIdentifier,
                        RequestMachineryAssignmentId = item.Id,
                        RequestFromDate = TimeCalculator.ConvertToShamsi(item.RequestMachinery.FromDate),
                        RequestToDate = TimeCalculator.ConvertToShamsi(item.RequestMachinery.ToDate),
                        RequestMachineryUnit = item.RequestMachinery.Unit,
                        Contractor = contractors?.FirstOrDefault(x => x?.Id == item.RequestMachinery.ContractorId)?.FullName
                    });

                var model = new GetFilteredMachineriesResponseModel()
                {
                    MachineryId = machinery.Machinery.Id,
                    MachineryName = machinery.Machinery?.MachineryName,
                    MachineryCode = machinery.Machinery?.MachineryCode,
                    MachineryGroupId = machinery.Machinery!.MachineriesGroup.Id,
                    MachineryGroupName = machinery.Machinery.MachineriesGroup.GroupName,
                    Number = number,
                    RequestMachineries = requestMachineries,
                    ConsumableVolumeMachineryId = consumableVolumeMachineryId,
                };
                response.Add(model);
            }
            else if (response.Count > 0 && response.Any(x => x.MachineryId == machinery.Machinery.Id))
            {
                var data = response.Where(x => x.MachineryId == machinery.Machinery.Id).FirstOrDefault();

                var requestMachineries = new List<GetFilteredRequestMachineriesResponseModel>();
                foreach (var requestMachinery in machinery.RequestMachineryAssignments)
                    requestMachineries.Add(new GetFilteredRequestMachineriesResponseModel()
                    {
                        RequestMachineryId = requestMachinery.RequestMachinery.Id,
                        RequestNumber = requestMachinery.RequestMachinery.Id,
                        MachineryIdentifier = requestMachinery.MachineryIdentifier,
                        RequestMachineryAssignmentId = requestMachinery.Id,
                        RequestFromDate = TimeCalculator.ConvertToShamsi(requestMachinery.RequestMachinery.FromDate),
                        RequestToDate = TimeCalculator.ConvertToShamsi(requestMachinery.RequestMachinery.ToDate),
                        RequestMachineryUnit = requestMachinery.RequestMachinery.Unit,
                        Contractor = contractors?.FirstOrDefault(x => x?.Id == requestMachinery.RequestMachinery.ContractorId)?.FullName
                    });

                data?.RequestMachineries.AddRange(requestMachineries);
            }
            else if (response.Count > 0 && response.Any(x => x.MachineryId != machinery.Machinery.Id))
            {
                var number = requestMachineriesResponse?
                   .SelectMany(x => x.Project.ProjectOperations)
                   .SelectMany(z => z.ProjectOperationDetails)
                   .SelectMany(y => y.ConsumableVolumeMachineries)
                   .Where(oo => oo.Machinery.Id.Equals(machinery.Machinery.Id)).FirstOrDefault()?.Number;

                var consumableVolumeMachineryId = requestMachineriesResponse?
                    .SelectMany(x => x.Project.ProjectOperations)
                    .SelectMany(z => z.ProjectOperationDetails)
                    .SelectMany(y => y.ConsumableVolumeMachineries)
                    .Where(oo => oo.Machinery.Id.Equals(machinery.Machinery.Id)).FirstOrDefault()?.Id;

                var requestMachineries = new List<GetFilteredRequestMachineriesResponseModel>();
                foreach (var item in machinery.RequestMachineryAssignments)
                    requestMachineries.Add(new GetFilteredRequestMachineriesResponseModel()
                    {
                        RequestMachineryId = item.RequestMachinery.Id,
                        RequestNumber = item.RequestMachinery.Id,
                        MachineryIdentifier = item.MachineryIdentifier,
                        RequestMachineryAssignmentId = item.Id,
                        RequestFromDate = TimeCalculator.ConvertToShamsi(item.RequestMachinery.FromDate),
                        RequestToDate = TimeCalculator.ConvertToShamsi(item.RequestMachinery.ToDate),
                        RequestMachineryUnit = item.RequestMachinery.Unit,
                        Contractor = contractors?.FirstOrDefault(x => x?.Id == item.RequestMachinery.ContractorId)?.FullName
                    });

                var model = new GetFilteredMachineriesResponseModel()
                {
                    MachineryId = machinery.Machinery.Id,
                    MachineryName = machinery.Machinery?.MachineryName,
                    MachineryCode = machinery.Machinery?.MachineryCode,
                    MachineryGroupId = machinery.Machinery!.MachineriesGroup.Id,
                    MachineryGroupName = machinery.Machinery.MachineriesGroup.GroupName,
                    Number = number,
                    RequestMachineries = requestMachineries,
                    ConsumableVolumeMachineryId = consumableVolumeMachineryId,
                };
                response.Add(model);
            }
        }

        return new GetFilteredMachineriesResponse(response, response.Count);
    }

    public async Task<Result<GetFilteredConsumableExpertsResponse?>> GetFilteredConsumableExperts(
        GetFilteredConsumableExpertsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredConsumableExpertsValidator, GetFilteredConsumableExpertsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredConsumableExpertsResponse>(isValidRequest.Error!);

        var getsExpert = await _mediator.Send(new GetExpertsByProjectOperationDetailIdQuery(request.ProjectOperationDetailId), ct);
        if (getsExpert.IsFailure)
            return Result.Failure<GetFilteredConsumableExpertsResponse>(getsExpert.Error!);
        var getsExpertData = getsExpert.Value!.Data!;

        var expertIds = getsExpertData.Select(x => x.ExpertId).ToList();
        var getFilteredSkillsQuery = await WebServicesLogic.GetFilteredSkillsDataReceiver(expertIds, request.FilterData, _mediator, ct);
        if (getFilteredSkillsQuery is null || getFilteredSkillsQuery.Count <= 0)
            return Result.Failure<GetFilteredConsumableExpertsResponse>(DailyProjectOperationErrors.ConsumableVolumeExpertWithFilterDataNotFound);
        var skillsData = getFilteredSkillsQuery?.ToList();
        var skillIds = skillsData?.Select(x => x.Id).ToList();
        var expertData = getsExpertData.Where(x => skillIds!.Contains(x.ExpertId)).ToList();

        var contractorServices = await _mediator.Send(new GetProjectOperationDetailContractorServicesQuery(request.ProjectOperationDetailId), ct);
        if (contractorServices.IsFailure || contractorServices.Value is null || contractorServices.Value.Data is null)
            return Result.Failure<GetFilteredConsumableExpertsResponse>(DailyProjectOperationErrors.ContractorServiceNotFound);
        var contractorIds = contractorServices.Value?.Data?.Where(oo => oo.ContractorId != null).Select(c => c.ContractorId!.Value!).ToList();
        if (contractorIds is null || contractorIds.Count <= 0)
            return Result.Failure<GetFilteredConsumableExpertsResponse>(DailyProjectOperationErrors.ContractorNotFound);

        var contractorEmployeesQuery = await _mediator.Send(new GetContractorEmployeesByContractorIdQuery(contractorIds, null), ct);
        var employeeIds = contractorEmployeesQuery.Value?.Data?.Select(c => c.EmployeeId).Distinct().ToList();

        var result = new List<GetFilteredConsumableExpertsModel>();
        foreach (var item in expertData)
        {
            var details = new List<GetFilteredConsumableExpertsDetailModel>();
            if (employeeIds is not null)
            {
                var getFilteredThirdPartiesQuery = await _mediator.Send(new GetWithSelectedSkillIdQuery(employeeIds, [item.ExpertId], null), ct);
                var thirdParties = getFilteredThirdPartiesQuery.Value?.Data;

                if (thirdParties is not null)
                    foreach (var thirdParty in thirdParties)
                    {
                        details.Add(new()
                        {
                            Id = thirdParty?.Id,
                            FullName = thirdParty?.FullName
                        });
                    }
            }

            var skill = skillsData?.Where(oo => oo.Id.Equals(item.ExpertId)).FirstOrDefault();
            var model = new GetFilteredConsumableExpertsModel()
            {
                ConsumableVolumeExpertId = item.Id,
                SkillId = skill?.Id,
                Name = skill?.Name,
                Code = skill?.Code,
                Number = item.Number,
                Details = details
            };

            result.Add(model);
        }

        return new GetFilteredConsumableExpertsResponse(result, getsExpertData.Count);
    }

    public async Task<Result<GetFilteredConsumableProductsResponse?>> GetFilteredConsumableProducts(
        GetFilteredConsumableProductsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredConsumableProductsValidator, GetFilteredConsumableProductsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredConsumableProductsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsRequestGoodsSupplyDetailForDailyQuery(request.ProjectOperationDetailId), ct);
        if (response.IsFailure)
            return Result.Failure<GetFilteredConsumableProductsResponse>(response.Error!);
        var values = response.Value!.Data?.Where(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply).ToList();
        var rowCount = response.Value.RowCount;

        List<Product>? products = null;
        var productIds = values?.Select(c => c.ProductId).Distinct().ToList();
        if (productIds is not null && productIds.Count > 0)
        {
            var productsInfo = await WebServicesLogic.ProductsDataReceiver(productIds, request.FilterData, _mediator, _pRepo, ct);
            products = productsInfo;
        }
        else
            return Result.Failure<GetFilteredConsumableProductsResponse>(DailyProjectOperationErrors.NoHaveProducts);

        var data = new List<GetFilteredConsumableProductsModel>();
        if (!string.IsNullOrEmpty(request.FilterData))
        {
            foreach (var item in products!)
            {
                var goodsSupplyDetail = values!.Where(x => x.ProductId == item.Id).FirstOrDefault();
                if (goodsSupplyDetail is not null)
                {
                    if (data.Any(x => x.ConsumableVolumeProductId == goodsSupplyDetail!.ConsumableVolumeProduct.Id))
                    {
                        if (!data.Any(x => x.Products!.Any(x => x.Id == item!.Id)))
                            data.ForEach(x => x.Products?.Add(new()
                            {
                                Id = item?.Id,
                                Name = item?.Name,
                                MeasureUnitName = item?.Group.Measure,
                                Brand = item?.Brand,
                                BrandModel = item?.BrandModel,
                                Code = item?.Code
                            }));
                    }
                    else
                    {
                        data.Add(new GetFilteredConsumableProductsModel()
                        {
                            ConsumableVolumeProductId = goodsSupplyDetail!.ConsumableVolumeProduct.Id,
                            ProductGroupId = goodsSupplyDetail.ConsumableVolumeProduct.ProductGroupId,
                            FinalValue = goodsSupplyDetail.ConsumableVolumeProduct.FinalValue,
                            ProductGroupCode = item?.Group.Code,
                            ProductGroupName = item?.Group.Name,
                            Products = [new()
                        {
                            Id = item?.Id,
                            Name = item?.Name,
                            MeasureUnitName = item?.Group.Measure,
                            Brand = item?.Brand,
                            BrandModel = item?.BrandModel,
                            Code = item?.Code
                        }]
                        });
                    }
                }
            }
        }
        else
        {
            foreach (var item in values!)
            {
                var goodsSupplyDetail = values!.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                if (goodsSupplyDetail is not null)
                {
                    var product = products?.Where(x => x.Id == item.ProductId).FirstOrDefault();
                    if (data.Any(x => x.ConsumableVolumeProductId == item.ConsumableVolumeProduct.Id))
                    {
                        if (!data.Any(x => x.Products!.Any(x => x.Id == product!.Id)))
                            data.ForEach(x => x.Products?.Add(new()
                            {
                                Id = product?.Id,
                                Name = product?.Name,
                                MeasureUnitName = product?.Group.Measure,
                                Brand = product?.Brand,
                                BrandModel = product?.BrandModel,
                                Code = product?.Code
                            }));
                    }
                    else
                    {
                        data.Add(new GetFilteredConsumableProductsModel()
                        {
                            ConsumableVolumeProductId = item.ConsumableVolumeProduct.Id,
                            ProductGroupId = item.ConsumableVolumeProduct.ProductGroupId,
                            FinalValue = item.ConsumableVolumeProduct.FinalValue,
                            ProductGroupCode = product?.Group.Code,
                            ProductGroupName = product?.Group.Name,
                            Products = [ new()
                            {
                                Id = product?.Id,
                                Name = product?.Name,
                                MeasureUnitName = product?.Group.Measure,
                                Brand = product?.Brand,
                                BrandModel = product?.BrandModel,
                                Code = product?.Code
                            }]
                        });
                    }
                }
            }
        }

        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetFilteredConsumableProductsResponse(data, data.Count);
    }

    public async Task<Result<GetsDailyProjectOperationStatusResponse?>> GetsDailyProjectOperationStatus(
        GetsDailyProjectOperationStatusRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectOperationStatus>());
        var response = result.Where(x => x.Code != 1).ToList();
        return new GetsDailyProjectOperationStatusResponse(response);
    }

    public async Task<Result<GetEmployerStatusStatementLimitDateResponse?>> GetEmployerStatusStatementLimitDate(
        GetEmployerStatusStatementLimitDateRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetEmployerStatusStatementLimitDateValidator, GetEmployerStatusStatementLimitDateRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetEmployerStatusStatementLimitDateResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetEmployerStatusStatementLimitDateQuery(request.ProjectId, request.EmployerContractId), ct);
        if (response.IsFailure)
            return Result.Failure<GetEmployerStatusStatementLimitDateResponse>(response.Error!);
        if (response.Value is null)
            return Result.Failure<GetEmployerStatusStatementLimitDateResponse>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);

        var minDate = response.Value.Min(x => x.StartDate.Date);
        return new GetEmployerStatusStatementLimitDateResponse(TimeCalculator.DatePiker(minDate) ?? null);
    }

    public async Task<Result<GetDailyProjectOperationContractorsResponse?>> GetDailyProjectOperationContractors(
        GetDailyProjectOperationContractorsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetDailyProjectOperationContractors");

        var isValidRequest = await request.IsValidAsync<GetDailyProjectOperationContractorsRequestValidator, GetDailyProjectOperationContractorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetDailyProjectOperationContractorsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetDailyProjectOperationContractorsQuery(request.CostCenterIds, request.ProjectIds, request.OperationInfoIds, request.ProjectOperationIds, request.ProjectOperationDetailIds), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetDailyProjectOperationContractorsResponse>(response.Error!);
        var ids = response.Value!.Data;

        List<UserModel>? contractors = [];
        var data = new List<GetDailyProjectOperationContractorsResponseModel>();
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
                    data.Add(new GetDailyProjectOperationContractorsResponseModel()
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
                    data.Add(new GetDailyProjectOperationContractorsResponseModel()
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
        return new GetDailyProjectOperationContractorsResponse(responseData ?? new List<GetDailyProjectOperationContractorsResponseModel>(0), contractors?.Count ?? 0);
    }

    public async Task<Result<GetDailyHistoriesExcelExporterResponse?>> GetDailyHistoriesExcelExporter(
        GetDailyHistoriesExcelExporterRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetDailyHistoriesExcelExporterValidator, GetDailyHistoriesExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetDailyHistoriesExcelExporterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationDetailByIdLessIncludeQuery(request.ProjectOperationDetailId), ct);
        if (response.IsFailure)
            return Result.Failure<GetDailyHistoriesExcelExporterResponse>(response.Error!);
        if (response.Value is null)
            return Result.Failure<GetDailyHistoriesExcelExporterResponse>(DailyProjectOperationErrors.ProjectOperationDetailWithIdNotFound);
        var projectOperationDetail = response.Value;

        var getFilteredResponse = await _mediator.Send(new GetDailiesExcelExportQuery(request.Ids, request.ProjectOperationDetailId, request.StartDate, request.EndDate, request.CreatorId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getFilteredResponse.IsFailure)
            return Result.Failure<GetDailyHistoriesExcelExporterResponse>(getFilteredResponse.Error!);
        if (getFilteredResponse.Value!.Data is null)
            return Result.Failure<GetDailyHistoriesExcelExporterResponse>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        var values = getFilteredResponse.Value?.Data;

        var measureunit = await WebServicesLogic.MeasureUnitDataReceiver(projectOperationDetail.ProjectOperation.UnitOfMeasurementId, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var deductionsAmount = values!.FirstOrDefault()!.ProjectOperationDetail.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList().Sum(x => x);
        var totals = new GetTotalsByProjectOperationDetailIdExcelExporterResponse()
        {
            TotalHeights = values!.Sum(x => x.Height),
            TotalLengths = values!.Sum(x => x.Length),
            TotalNumbers = values!.Sum(x => x.Number),
            TotalWidths = values!.Sum(x => x.Width),
            TotalWeights = values!.Sum(x => x.Weight),
            TotalAmounts = values!.Sum(x => x.FinalAmount),
            ProjectOperationDetailFinalAmount = values!.FirstOrDefault()!.ProjectOperationDetail.FinalAmount - deductionsAmount,
            TotalDeductionFinalAmount = deductionsAmount,
            TotalProjectOperationDetailFinalAmount = values!.FirstOrDefault()!.ProjectOperationDetail.FinalAmount,
            ProjectOperationWorkload = values!.FirstOrDefault()!.ProjectOperationDetail.ProjectOperation.Workload
        };

        var data = values.Adapt<List<GetDailyHistoriesExcelExporterResponseModel>>();
        foreach (var item in data!)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
            item.UnitOfMeasurement = measureunit?.Name;
            item.UnitOfMeasurementId = measureunit?.Id;
            item.StartDateMiladi = values.FirstOrDefault(x => x.Id == item.DailyProjectOperationId)!.StartDate;
            item.CreatedMiladi = values.FirstOrDefault(x => x.Id == item.DailyProjectOperationId)!.Created;
        }

        var file = new FileContentResult(DailyProjectOperationExcels.DailyProjectOperationExcel(totals, data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"DailyProjectOperations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetDailyHistoriesExcelExporterResponse(file);
    }

    public async Task<Result<GetDailyHistoriesExcelEnumsResponse?>> GetDailyHistoriesExcelEnums(
        GetDailyHistoriesExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<DailyProjectOperationsExcelEnum>());
        return new GetDailyHistoriesExcelEnumsResponse(response);
    }

    public async Task<Result<GetsDetailedDailyProjectOperationExcelExporterResponse?>> GetsDetailedDailyProjectOperationExcelExporter(
        GetsDetailedDailyProjectOperationExcelExporterRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsDetailedDailyProjectOperationExcelExporterValidator, GetsDetailedDailyProjectOperationExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsDetailedDailyProjectOperationExcelExporterResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsDetailedDailyProjectOperationQuery(request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.ContractorIds,
            request.ServiceInfoIds,
            request.CreatorIds,
            request.StartDate,
            request.EndDate,
            request.FromDate,
            request.ToDate,
            request.ProjectOperationDetailStatus,
            request.Status,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize),
            ct);
        if (responses.IsFailure)
            return Result.Failure<GetsDetailedDailyProjectOperationExcelExporterResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var measurementIds = values?.Select(c => c.UnitOfMeasurementId).Distinct().ToList();
        var measurements = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);

        var contractorIds = values?.SelectMany(c => c.ContractorIds.Where(x => x.HasValue && x.Value > 0).Select(x => (long)x!)).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var creatorIds = values?.Select(c => c.CreatorId).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        values!.ForEach(oo =>
        {
            var measurement = measurements?.FirstOrDefault(x => x.Id.Equals(oo.UnitOfMeasurementId));
            oo.MeasurementName = measurement?.Name;

            var creator = creators?.FirstOrDefault(x => x.UserId.Equals(oo.CreatorId));
            oo.CreatorName = creator?.FullName;
            oo.CreatorNickname = creator?.Nickname;

            if (oo.ContractorIds.Count > 0)
            {
                var contractorFullNames = contractors?.Where(x => oo.ContractorIds.Contains(x?.Id)).Select(x => x!.FullName).ToList();
                var contractorNicknames = contractors?.Where(x => oo.ContractorIds.Contains(x?.Id)).Select(x => x!.Nickname).ToList();
                if (contractorFullNames is not null && contractorFullNames.Any())
                    oo.Contractors = string.Join(" - ", contractorFullNames!);
                if (contractorNicknames is not null && contractorNicknames.Any())
                    oo.ContractorsNickName = string.Join(" - ", contractorNicknames!);
            }

            if (oo.services != null && oo.services.Count > 0)
                foreach (var item in oo.services)
                {
                    var measure = measurements?.FirstOrDefault(x => x.Id.Equals(item.ServiceInfoMeasureId))?.Name;
                    oo.ServiceInfos = string.Join(" - ", item.ServiceInfoName + "(" + measure + ")");
                }

            if (oo.DeductionAmounts is not null && oo.DeductionAmounts.Count > 0)
                oo.ProjectOperationDetailFinalAmount = oo.ProjectOperationDetailFinalAmount - oo.DeductionAmounts.Sum(x => x);
            else
                oo.ProjectOperationDetailFinalAmount = oo.ProjectOperationDetailFinalAmount - 0;
        });

        var data = values.Adapt<List<GetsDetailedDailyProjectOperationExcelExporterResponseModel>>();
        var file = new FileContentResult(DailyProjectOperationExcels.DetailedDailyProjectOperationExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"DailyProjectOperations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsDetailedDailyProjectOperationExcelExporterResponse(file);
    }

    public async Task<Result<GetsDetailedDailyProjectOperationExcelEnumsResponse?>> GetsDetailedDailyProjectOperationExcelEnums(
        GetsDetailedDailyProjectOperationExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<DetailedDailyProjectOperationsExcelEnum>());
        return new GetsDetailedDailyProjectOperationExcelEnumsResponse(response);
    }

    public async Task<Result<GetTotalsByProjectOperationDetailIdResponse?>> GetTotalsByProjectOperationDetailId(
        GetTotalsByProjectOperationDetailIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTotalsByProjectOperationDetailId, ProjectOperationDetailId:{ProjectOperationDetailId}", request.ProjectOperationDetailId);

        var isValidRequest = await request.IsValidAsync<GetTotalsByProjectOperationDetailIdValidator, GetTotalsByProjectOperationDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTotalsByProjectOperationDetailIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTotalsByProjectOperationDetailIdQuery(request.ProjectOperationDetailId), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetTotalsByProjectOperationDetailIdResponse>(response.Error!);
        var values = response.Value!;

        var deductionsAmount = values.FirstOrDefault()!.ProjectOperationDetail.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList().Sum(x => x);
        var totals = new GetTotalsByProjectOperationDetailIdResponse()
        {
            TotalHeights = values.Sum(x => x.Height),
            TotalLengths = values.Sum(x => x.Length),
            TotalNumbers = values.Sum(x => x.Number),
            TotalWidths = values.Sum(x => x.Width),
            TotalWeights = values.Sum(x => x.Weight),
            TotalAmounts = values.Sum(x => x.FinalAmount),
            ProjectOperationDetailFinalAmount = values.FirstOrDefault()!.ProjectOperationDetail.FinalAmount - deductionsAmount,
            TotalDeductionFinalAmount = deductionsAmount,
            TotalProjectOperationDetailFinalAmount = values.FirstOrDefault()!.ProjectOperationDetail.FinalAmount,
            ProjectOperationWorkload = values.FirstOrDefault()!.ProjectOperationDetail.ProjectOperation.Workload
        };

        return totals;
    }

    public async Task<Result<GetOperationTotalsResponse?>> GetOperationTotals(
        GetOperationTotalsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationTotals, ProjectOperationDetailId:{ProjectOperationDetailId}", request.ProjectOperationDetailId);

        var isValidRequest = await request.IsValidAsync<GetOperationTotalsValidator, GetOperationTotalsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationTotalsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationTotalsQuery(request.ProjectOperationId, request.ProjectOperationDetailId), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetOperationTotalsResponse>(response.Error!);
        var values = response.Value!;

        var projectOperationDetails = values.Select(x => x.ProjectOperationDetail).Distinct().ToList();
        var deductionsAmounts = projectOperationDetails.Where(x => x.ProjectOperationDetailDeductions is not null && x.ProjectOperationDetailDeductions.Count > 0)
            .SelectMany(x => x.ProjectOperationDetailDeductions).Select(x => x.FinalAmount).ToList().Sum(x => x);
        var totals = new GetOperationTotalsResponse()
        {
            DailyTotalHeights = values.Sum(x => x.Height),
            DailyTotalLengths = values.Sum(x => x.Length),
            DailyTotalNumbers = values.Sum(x => x.Number),
            DailyTotalWidths = values.Sum(x => x.Width),
            DailyTotalWeights = values.Sum(x => x.Weight),
            DailyTotalAmounts = values.Sum(x => x.FinalAmount),

            ProjectOperationDetailTotalHeights = projectOperationDetails.Sum(x => x.Height),
            ProjectOperationDetailTotalLengths = projectOperationDetails.Sum(x => x.Length),
            ProjectOperationDetailTotalNumbers = projectOperationDetails.Sum(x => x.Number),
            ProjectOperationDetailTotalWidths = projectOperationDetails.Sum(x => x.Width),
            ProjectOperationDetailTotalWeights = projectOperationDetails.Sum(x => x.Weight),
            ProjectOperationDetailTotalAmounts = projectOperationDetails.Sum(x => x.FinalAmount),

            TotalDeductionFinalAmount = deductionsAmounts,
            TotalProjectOperationDetailFinalAmount = projectOperationDetails.Sum(x => x.FinalAmount) - deductionsAmounts,

            TotalOperationAmounts = (projectOperationDetails.Sum(x => x.FinalAmount) - deductionsAmounts) - values.Sum(x => x.FinalAmount),

            ProjectOperationWorkload = projectOperationDetails.FirstOrDefault()!.ProjectOperation.Workload,
            TotalOperationWork = projectOperationDetails.FirstOrDefault()!.ProjectOperation.Workload - projectOperationDetails.Sum(x => x.FinalAmount) - deductionsAmounts
        };

        return totals;
    }

    public async Task<Result<GetDetailedDailyOperationTotalsResponse?>> GetDetailedDailyOperationTotals(
        GetDetailedDailyOperationTotalsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetDetailedDailyOperationTotalsValidator, GetDetailedDailyOperationTotalsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetDetailedDailyOperationTotalsResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetDetailedDailyOperationTotalsQuery(null,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.ContractorIds,
            request.ServiceInfoIds,
            request.CreatorIds,
            request.StartDate,
            request.EndDate,
            request.FromDate,
            request.ToDate,
            request.Status,
            request.ProjectOperationDetailStatus,
            request.FilterData),
            ct);
        if (responses.IsFailure || responses.Value is null)
            return Result.Failure<GetDetailedDailyOperationTotalsResponse>(responses.Error!);
        var values = responses.Value;

        var deductionsAmount = values.Where(x => x.DeductionAmounts is not null && x.DeductionAmounts.Count > 0)
            .SelectMany(x => x.DeductionAmounts!).ToList().Sum(x => x);

        List<GetDetailedDailyProjectOperationTotalsModel> projectOperationDetails = [];
        foreach (var item in values)
        {
            if (projectOperationDetails.Any(x => x.ProjectOperationDetailId == item.ProjectOperationDetailId))
                continue;
            else
                projectOperationDetails.Add(item);
        }

        List<GetDetailedDailyProjectOperationTotalsModel> projectOperations = [];
        foreach (var item in values)
        {
            if (projectOperations.Any(x => x.ProjectOperationId == item.ProjectOperationId))
                continue;
            else
                projectOperations.Add(item);
        }

        var totals = new GetDetailedDailyOperationTotalsResponse()
        {
            DailyTotalHeights = values.Sum(x => x.Height),
            DailyTotalLengths = values.Sum(x => x.Length),
            DailyTotalNumbers = values.Sum(x => x.Number),
            DailyTotalWidths = values.Sum(x => x.Width),
            DailyTotalWeights = values.Sum(x => x.Weight),
            DailyTotalAmounts = values.Sum(x => x.DailyProjectOperationFinalAmount),

            ProjectOperationDetailTotalHeights = projectOperationDetails.Sum(x => x.ProjectOperationDetailHeight),
            ProjectOperationDetailTotalLengths = projectOperationDetails.Sum(x => x.ProjectOperationDetailLength),
            ProjectOperationDetailTotalNumbers = projectOperationDetails.Sum(x => x.ProjectOperationDetailNumber),
            ProjectOperationDetailTotalWidths = projectOperationDetails.Sum(x => x.ProjectOperationDetailWidth),
            ProjectOperationDetailTotalWeights = projectOperationDetails.Sum(x => x.ProjectOperationDetailWeight),
            ProjectOperationDetailTotalAmounts = projectOperationDetails.Sum(x => x.ProjectOperationDetailFinalAmount),

            TotalDeductionFinalAmount = deductionsAmount,
            TotalProjectOperationDetailFinalAmount = projectOperationDetails.Sum(x => x.ProjectOperationDetailFinalAmount) - deductionsAmount,

            TotalOperationAmounts = (projectOperationDetails.Sum(x => x.ProjectOperationDetailFinalAmount) - deductionsAmount) - values.Sum(x => x.DailyProjectOperationFinalAmount),

            ProjectOperationWorkload = projectOperations.Sum(x => x.ProjectOperationWorkload),
            TotalOperationWork = projectOperations.Sum(x => x.ProjectOperationWorkload) - (projectOperationDetails.Sum(x => x.ProjectOperationDetailFinalAmount) - deductionsAmount)
        };

        return totals;
    }

    public async Task<Result<GetsDailyProjectOperationServiceResponse?>> GetsDailyProjectOperationService(
        GetsDailyProjectOperationServiceRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsDailyProjectOperationServiceValidator, GetsDailyProjectOperationServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsDailyProjectOperationServiceResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsDailyProjectOperationServiceQuery(
            null, request.CostCenterIds, request.ProjectIds, request.ProjectOperationIds, request.ProjectOperationDetailIds,
            request.ContractorIds, request.ServiceInfoIds, request.MeasurUnitIds, request.StartDate, request.EndDate, request.FromDate,
            request.ToDate, request.Status, request.FilterData, request.FilterServiceInfo, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsDailyProjectOperationServiceResponse>(responses.Error!);
        var values = responses.Value?.Data;

        List<Measureunit>? measureunits = [];
        var measureIds = GetMeasureunitIds(values);
        if (measureIds is not null && measureIds.Count > 0)
            measureunits = await WebServicesLogic.MeasurementDataReceiver(measureIds.Distinct().ToList(), _mediator, ct);

        List<UserModel?>? contractors = [];
        var cIds = GetContractorIds(values);
        if (cIds is not null && cIds.Count > 0)
            contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(cIds.Distinct().ToList(), null, null, _mediator, ct);

        var creatorIds = values?.Where(x => x.CreatorId != null && x.CreatorId > 0).Select(c => (long)c.CreatorId!).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        values!.ForEach(oo =>
        {
            oo.MeasurementName = measureunits?.FirstOrDefault(x => x.Id.Equals(oo.UnitOfMeasurementId))?.Name;
            oo.ServiceInfoMeasure = measureunits?.FirstOrDefault(x => x.Id.Equals(oo.ServiceInfoMeasureId))?.Name;
            oo.ProjectOperationDetailServiceInfoMeasure = measureunits?.FirstOrDefault(x => x.Id.Equals(oo.ProjectOperationDetailServiceInfoMeasureId))?.Name;

            var creator = creators?.FirstOrDefault(x => x.UserId.Equals(oo.CreatorId));
            oo.CreatorName = creator?.FullName;
            oo.CreatorNickname = creator?.Nickname;

            oo.Contractor = contractors?.FirstOrDefault(x => x.Id.Equals(oo.ContractorId))?.FullName;
            oo.ProjectOperationDetailContractor = contractors?.FirstOrDefault(x => x.Id.Equals(oo.ProjectOperationDetailContractorId))?.FullName;
            oo.ContractorNickName = contractors?.FirstOrDefault(x => x.Id.Equals(oo.ContractorId))?.Nickname;
            oo.ProjectOperationDetailContractorNickName = contractors?.FirstOrDefault(x => x.Id.Equals(oo.ProjectOperationDetailContractorId))?.Nickname;

            if (oo.DeductionAmounts is not null && oo.DeductionAmounts.Count > 0)
                oo.TotalProjectOperationDetailFinalAmount = oo.ProjectOperationDetailFinalAmount - oo.DeductionAmounts.Sum(x => x);
            else
                oo.TotalProjectOperationDetailFinalAmount = oo.ProjectOperationDetailFinalAmount - 0;
        });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        List<GetsDailyProjectOperationServiceModel> projectOperationDetails = [];
        foreach (var item in values)
        {
            if (projectOperationDetails.Any(x => x.ProjectOperationDetailId == item.ProjectOperationDetailId))
                continue;
            else
                projectOperationDetails.Add(item);
        }

        var totals = new GetsDailyProjectOperationServiceTotalModel()
        {
            TotalProjectOperationDetailServiceVolume = projectOperationDetails.Where(x => x.ProjectOperationDetailVolume != null && x.ProjectOperationDetailVolume >= 0).ToList().Sum(x => (decimal)x.ProjectOperationDetailVolume!),
            TotalDailyServiceVolume = values.Where(x => x.Volume != null && x.Volume >= 0).ToList().Sum(x => (decimal)x.Volume!),
        };

        return new GetsDailyProjectOperationServiceResponse(totals, values!, responses.Value!.RowCount!);
    }

    public async Task<Result<GetsTotalDailyProjectOperationServiceResponse?>> GetsTotalDailyProjectOperationService(
        GetsTotalDailyProjectOperationServiceRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsTotalDailyProjectOperationServiceValidator, GetsTotalDailyProjectOperationServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTotalDailyProjectOperationServiceResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsTotalDailyProjectOperationServiceQuery(
            null,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.ContractorIds,
            request.ServiceInfoIds,
            request.MeasurUnitIds,
            request.StartDate,
            request.EndDate,
            request.FromDate,
            request.ToDate,
            request.Status,
            request.FilterData,
            request.FilterServiceInfo), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsTotalDailyProjectOperationServiceResponse>(responses.Error!);
        var values = responses.Value;

        var totals = new GetsTotalDailyProjectOperationServiceResponse();
        if (values is not null && values.Count > 0)
        {
            List<GetsTotalDailyProjectOperationServiceModel> projectOperationDetails = [];
            foreach (var item in values)
            {
                if (projectOperationDetails.Any(x => x.ProjectOperationDetailId == item.ProjectOperationDetailId))
                    continue;
                else
                    projectOperationDetails.Add(item);
            }

            totals = new GetsTotalDailyProjectOperationServiceResponse()
            {
                TotalProjectOperationDetailServiceVolume = projectOperationDetails.Where(x => x.ProjectOperationDetailVolume != null && x.ProjectOperationDetailVolume >= 0).ToList().Sum(x => (decimal)x.ProjectOperationDetailVolume!),
                TotalDailyServiceVolume = values.Where(x => x.Volume != null && x.Volume >= 0).ToList().Sum(x => (decimal)x.Volume!),
            };
        }

        return totals;
    }

    public async Task<Result<GetsDailyProjectOperationServiceExcelExporterResponse?>> GetsDailyProjectOperationServiceExcelExporter(
        GetsDailyProjectOperationServiceExcelExporterRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsDailyProjectOperationServiceExcelExporterValidator, GetsDailyProjectOperationServiceExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsDailyProjectOperationServiceExcelExporterResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsDailyProjectOperationServiceQuery(
            request.Ids, request.CostCenterIds, request.ProjectIds, request.ProjectOperationIds, request.ProjectOperationDetailIds,
            request.ContractorIds, request.ServiceInfoIds, request.MeasurUnitIds, request.StartDate, request.EndDate, request.FromDate,
            request.ToDate, request.Status, request.FilterData, request.FilterServiceInfo, request.OrderBy, request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsDailyProjectOperationServiceExcelExporterResponse>(responses.Error!);
        var values = responses.Value?.Data;

        List<Measureunit>? measureunits = [];
        var measureIds = GetMeasureunitIds(values);
        if (measureIds is not null && measureIds.Count > 0)
            measureunits = await WebServicesLogic.MeasurementDataReceiver(measureIds.Distinct().ToList(), _mediator, ct);

        List<UserModel?>? contractors = [];
        var cIds = GetContractorIds(values);
        if (cIds is not null && cIds.Count > 0)
            contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(cIds.Distinct().ToList(), null, null, _mediator, ct);

        var creatorIds = values?.Where(x => x.CreatorId != null && x.CreatorId > 0).Select(c => (long)c.CreatorId!).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        values!.ForEach(oo =>
        {
            oo.MeasurementName = measureunits?.FirstOrDefault(x => x.Id.Equals(oo.UnitOfMeasurementId))?.Name;
            oo.ServiceInfoMeasure = measureunits?.FirstOrDefault(x => x.Id.Equals(oo.ServiceInfoMeasureId))?.Name;
            oo.ProjectOperationDetailServiceInfoMeasure = measureunits?.FirstOrDefault(x => x.Id.Equals(oo.ProjectOperationDetailServiceInfoMeasureId))?.Name;

            var creator = creators?.FirstOrDefault(x => x.UserId.Equals(oo.CreatorId));
            oo.CreatorName = creator?.FullName;
            oo.CreatorNickname = creator?.Nickname;

            oo.Contractor = contractors?.FirstOrDefault(x => x.Id.Equals(oo.ContractorId))?.FullName;
            oo.ProjectOperationDetailContractor = contractors?.FirstOrDefault(x => x.Id.Equals(oo.ProjectOperationDetailContractorId))?.FullName;
            oo.ContractorNickName = contractors?.FirstOrDefault(x => x.Id.Equals(oo.ContractorId))?.Nickname;
            oo.ProjectOperationDetailContractorNickName = contractors?.FirstOrDefault(x => x.Id.Equals(oo.ProjectOperationDetailContractorId))?.Nickname;

            if (oo.DeductionAmounts is not null && oo.DeductionAmounts.Count > 0)
                oo.TotalProjectOperationDetailFinalAmount = oo.ProjectOperationDetailFinalAmount - oo.DeductionAmounts.Sum(x => x);
            else
                oo.TotalProjectOperationDetailFinalAmount = oo.ProjectOperationDetailFinalAmount - 0;
        });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        List<GetsDailyProjectOperationServiceModel> projectOperationDetails = [];
        foreach (var item in values)
        {
            if (projectOperationDetails.Any(x => x.ProjectOperationDetailId == item.ProjectOperationDetailId))
                continue;
            else
                projectOperationDetails.Add(item);
        }

        var totals = new GetsDailyProjectOperationServiceTotalExcelModel()
        {
            TotalProjectOperationDetailServiceVolume = projectOperationDetails.Where(x => x.ProjectOperationDetailVolume != null && x.ProjectOperationDetailVolume >= 0).ToList().Sum(x => (decimal)x.ProjectOperationDetailVolume!),
            TotalDailyServiceVolume = values.Where(x => x.Volume != null && x.Volume >= 0).ToList().Sum(x => (decimal)x.Volume!),
        };

        var data = values.Adapt<List<GetsDailyProjectOperationServiceExcelExporterResponseModel>>();
        var file = new FileContentResult(DailyProjectOperationExcels.DailyProjectOperationServiceExcel(totals, data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"DailyProjectOperationServices-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsDailyProjectOperationServiceExcelExporterResponse(file);
    }

    public async Task<Result<GetsDailyProjectOperationServiceExcelEnumsResponse?>> GetsDailyProjectOperationServiceExcelEnums(
        GetsDailyProjectOperationServiceExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<DailyProjectOperationServicesExcelEnum>());
        return new GetsDailyProjectOperationServiceExcelEnumsResponse(response);
    }

    public async Task<Result<GetsDailyServiceInfoResponse?>> GetsDailyServiceInfo(
        GetsDailyServiceInfoRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsDailyServiceInfoValidator, GetsDailyServiceInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsDailyServiceInfoResponse>(isValidRequest.Error!);

        request.FilterData ??= request.ServiceInfoName;

        var responses = await _mediator.Send(new GetsDailyServiceInfoQuery(
            null,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.ContractorIds,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsDailyServiceInfoResponse>(responses.Error!);
        var values = responses.Value?.Data;

        List<Measureunit>? measureunits = [];
        var measureIds = GetMeasureunitIds(values);
        if (measureIds is not null && measureIds.Count > 0)
            measureunits = await WebServicesLogic.MeasurementDataReceiver(measureIds.Distinct().ToList(), _mediator, ct);

        values!.ForEach(oo =>
        {
            oo.MeasurementName = measureunits?.FirstOrDefault(x => x.Id.Equals(oo.UnitOfMeasurementId))?.Name;
        });

        return new GetsDailyServiceInfoResponse(values ?? new List<GetsDailyServiceInfoModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsDailyCreatorResponse?>> GetsDailyCreator(
        GetsDailyCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsDailyCreator");

        var isValidRequest = await request.IsValidAsync<GetsDailyCreatorRequestValidator, GetsDailyCreatorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsDailyCreatorResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsDailyCreatorQuery(), ct);
        if (response.IsFailure || response is null)
            return Result.Failure<GetsDailyCreatorResponse>(DailyProjectOperationErrors.CreatorIdsAreEmpty);
        var value = response!.Value!;

        List<FilteredUserResponseModel?> creators = new();
        var data = new List<GetsDailyCreatorResponseModel>();
        if (value.Data?.Count > 0)
        {
            var responseValue = await WebServicesLogic.UserDataReceiver(value.Data!, request.FilterData, _mediator, ct);
            if (responseValue is not null)
                creators.AddRange(responseValue);

            if (!string.IsNullOrEmpty(request.FilterData))
                foreach (var id in value.Data!)
                {
                    if (!creators.Any(x => x?.UserId == id))
                        continue;

                    var creator = creators.Where(x => x?.UserId == id).FirstOrDefault();
                    if (creator is not null)
                        data.Add(new GetsDailyCreatorResponseModel()
                        {
                            CreatorId = id,
                            ThirdPartyId = creator?.Id,
                            FullName = creator?.FullName,
                            FirstName = creator?.FirstName,
                            LastName = creator?.LastName,
                            DefaultPhoneNo = creator?.DefaultPhoneNo,
                            OrganizationCode = creator?.OrganizationCode,
                            IdentityNo = creator?.IdentityNo,
                            Nickname = creator?.Nickname
                        });
                }
            else
                foreach (var id in value.Data!)
                {
                    var creator = creators.Where(x => x?.UserId == id).FirstOrDefault();
                    data.Add(new GetsDailyCreatorResponseModel()
                    {
                        CreatorId = id,
                        ThirdPartyId = creator?.Id,
                        FullName = creator?.FullName,
                        FirstName = creator?.FirstName,
                        LastName = creator?.LastName,
                        DefaultPhoneNo = creator?.DefaultPhoneNo,
                        OrganizationCode = creator?.OrganizationCode,
                        IdentityNo = creator?.IdentityNo,
                        Nickname = creator?.Nickname,
                    });
                }
        }

        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsDailyCreatorResponse(responseData ?? new List<GetsDailyCreatorResponseModel>(0), creators?.Count ?? 0);
    }

    public async Task<Result<GetDailyProjectOperationHistoryByIdResponse?>> GetDailyProjectOperationHistoryById(
        GetDailyProjectOperationHistoryByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetDailyProjectOperationHistory");

        var responses = await _mediator.Send(new GetDailyProjectOperationHistoryByIdQuery(request.Id), ct);
        if (responses is null)
            return responses?.Failure<GetDailyProjectOperationHistoryByIdResponse>()!;
        var values = responses.Value?.Data;

        var creatorIds = values?.Select(x => x.CreatorId).Where(x => x > 0).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        foreach (var item in values)
        {
            var creatorName = creators.FirstOrDefault(x => x.UserId == item.CreatorId);
            item.CreatorName = creatorName.FullName;
        }

        return responses;
    }

}

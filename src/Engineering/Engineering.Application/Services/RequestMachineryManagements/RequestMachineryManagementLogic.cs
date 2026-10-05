using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Configs;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.RequestMachineries.Commands.ChangeRequestMachineryStatus;
using Engineering.Application.Services.RequestMachineries.Commands.RequestMachineryAssignments;
using Engineering.Application.Services.RequestMachineries.Commands.SetOnProjectRequestMachineryConfirmDate;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryConfirmDate;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryConfirmDescription;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryOperatorAppoinment;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryPaymentType;
using Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryContractor;
using Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryRequester;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryBillDocumentModel;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryDocumentModel;
using Engineering.Application.Services.RequestMachineries.Queries.GetFilteredRequestMachineries;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryById;
using Engineering.Application.Services.RequestMachineries.Queries.GetsFilteredMachineryContractor;
using Engineering.Application.Services.RequestMachineries.Queries.GetsFilteredMachineryRequester;
using Engineering.Application.Services.RequestMachineries.Queries.GetsTotalFilteredRequestMachinery;
using Engineering.Application.Services.RequestMachineryManagements.Commands.ActiveRequestMachineryInquiryOperator;
using Engineering.Application.Services.RequestMachineryManagements.Commands.ConfirmRequestMachineryInquiry;
using Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiry;
using Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryDocument;
using Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryOperator;
using Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiry;
using Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiryDocument;
using Engineering.Application.Services.RequestMachineryManagements.Commands.InActiveRequestMachineryInquiryOperator;
using Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryInquiry;
using Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryInquiryDocument;
using Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryOnPaymentState;
using Engineering.Application.Services.RequestMachineryManagements.Models.ActiveRequestMachineryInquiryOperator;
using Engineering.Application.Services.RequestMachineryManagements.Models.AssignMachineryForRequestMachinery;
using Engineering.Application.Services.RequestMachineryManagements.Models.CreateRequestMachineryInquiry;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetFilteredRequestMachineryInquieries;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetFilteredRequestMachineryManagements;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiries;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiryOperators;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryManagementById;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryOperators;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetsRequestMachineryManagementExcelEnum;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetsRequestMachineryManagementExcelExporter;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetsTotalFilteredRequestMachinery;
using Engineering.Application.Services.RequestMachineryManagements.Models.InActiveRequestMachineryInquiryOperator;
using Engineering.Application.Services.RequestMachineryManagements.Models.RequestMachineryManagementExcelEnums;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryBackToOnProject;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryConfirmed;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryDone;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryAppointment;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryDone;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryOperator;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryRejected;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryManagerConfirm;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryOnProject;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryPending;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryRejected;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryResended;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryReturned;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachinerySendToManager;
using Engineering.Application.Services.RequestMachineryManagements.Models.UpdateRequestMachineryAssignments;
using Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryInquiryOperators;
using Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryOperators;
using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators;
using Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Queries.GetMachineryOperators;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Gita.Backend.Shared.Domain.Shared.Contracts;
using Microsoft.Extensions.Options;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.RequestMachineryManagements;

public partial class RequestMachineryManagementLogic : IRequestMachineryManagementLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<RequestMachineryManagementLogic> _logger;
    private readonly IMessengerChannelRepository _messengerChannelRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly HttpContext _context;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserProfileService _userProfileService;
    private readonly ITelegramMessageHistoryLogic _telegramMessageHistoryLogic;
    private readonly string? _authorization;
    private readonly MessageSenderConfig _messageSenderConfig;

    public RequestMachineryManagementLogic(
        IMediator mediator,
        ILogger<RequestMachineryManagementLogic> logger,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor context,
        IUserInfoService userInfoService,
        IUserProfileService userProfileService,
        IMessengerChannelRepository messengerChannelRepo,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        IOptionsSnapshot<MessageSenderConfig> options)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _context = context.HttpContext!;
        _userProfileService = userProfileService;
        _messengerChannelRepo = messengerChannelRepo;
        _userInfoService = userInfoService;
        _telegramMessageHistoryLogic = telegramMessageHistoryLogic;
        _messageSenderConfig = options.Value;
        _authorization = context.HttpContext?.Request.Headers.Authorization.ToString();
    }

    public async Task<Result<CreateRequestMachineryInquiryResponse?>> CreateRequestMachineryInquiryAsync(CreateRequestMachineryInquiryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateRequestMachineryInquiryValidator, CreateRequestMachineryInquiryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateRequestMachineryInquiryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateRequestMachineryInquiryResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.Inquiry || requestMachinery.Status == RequestMachineryStatus.InquiryRejected))
            return Result.Failure<CreateRequestMachineryInquiryResponse>(RequestMachineryInquiryErrors.InValidStatus);

        var inquiryOperators = requestMachinery.InquiryOperators.Where(oo => oo.IsActive).ToList();
        if (inquiryOperators.Count != 1)
            return Result.Failure<CreateRequestMachineryInquiryResponse>(RequestMachineryInquiryErrors.InValidRequestMachineryInquiryOperator);

        var inquiryOperator = inquiryOperators.SingleOrDefault()!;
        var getOfficers = await _mediator.Send(new GetMachineryOperatorsQuery(inquiryOperator.OperatorAppoinmentId, null, null, null, null, 1, 1), ct);
        if (getOfficers.IsFailure)
            return Result.Failure<CreateRequestMachineryInquiryResponse>(getOfficers.Error!);
        var officers = getOfficers.Value!.Data!.FirstOrDefault()!;

        var currentUser = _userProfileService.GetProfileInfo();
        if (officers.UserId != currentUser.UserId)
            return Result.Failure<CreateRequestMachineryInquiryResponse>(RequestMachineryInquiryErrors.InValidRequestMachineryInquiryOperator);

        var inquiries = inquiryOperators.SelectMany(oo => oo.Inquiries).ToList();
        var inquiryIds = inquiries.Select(oo => oo.Id).ToList();

        var newInquiries = request.Inquiries.Where(oo => oo.Id == null).ToList();
        var deletedInquiryIds = request.Inquiries.Where(oo => oo.Id != null && oo.IsDeleted != null && oo.IsDeleted == true)
            .Select(oo => oo.Id!.Value).ToList();
        var updatedInquiries = request.Inquiries.Where(oo => oo.Id != null && (oo.IsDeleted == null || oo.IsDeleted == false)).ToList();

        foreach (var inquiry in newInquiries)
        {
            var finalPrice = inquiry.Count * inquiry.UnitPrice * inquiry.InquiryRequestedTime;

            if (!(inquiry.Count <= requestMachinery.RequestCount))
                return Result.Failure<CreateRequestMachineryInquiryResponse>(RequestMachineryInquiryErrors.CountIsInvalid);

            var thirdParties = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long>() { inquiry.ThirdPartyId }, null, false, null), ct);
            if (thirdParties.IsFailure)
                return Result.Failure<CreateRequestMachineryInquiryResponse>(thirdParties.Error!);

            var createInquiryResponse = await _mediator.Send(new CreateRequestMachineryInquiryCommand(inquiry.ThirdPartyId, inquiry.CurrencyId, inquiry.Count, inquiry.Unit,
                 inquiry.UnitPrice, finalPrice, inquiry.Description, inquiry.InquiryRequestedTime, inquiryOperator), ct);
            if (createInquiryResponse.IsFailure)
                return Result.Failure<CreateRequestMachineryInquiryResponse>(createInquiryResponse.Error!);
            var requestMachineryInquiry = createInquiryResponse.Value!;

            if (inquiry.Documents is not null && inquiry.Documents.Count > 0)
            {
                foreach (var document in inquiry.Documents)
                {
                    var createDocumentResponse = await _mediator.Send(new CreateRequestMachineryInquiryDocumentCommand(document.Url, requestMachineryInquiry), ct);
                    if (createDocumentResponse.IsFailure)
                        return Result.Failure<CreateRequestMachineryInquiryResponse>(createDocumentResponse.Error!);
                }
            }
        }

        foreach (var inquiry in deletedInquiryIds)
        {
            var deleteResponse = await _mediator.Send(new DeleteRequestMachineryInquiryCommand(inquiry), ct);
            if (deleteResponse.IsFailure)
                return Result.Failure<CreateRequestMachineryInquiryResponse>(deleteResponse.Error!);
        }

        foreach (var inquiry in updatedInquiries)
        {
            var finalPrice = inquiry.Count * inquiry.UnitPrice * inquiry.InquiryRequestedTime;

            if (!(inquiry.Count <= requestMachinery.RequestCount))
                return Result.Failure<CreateRequestMachineryInquiryResponse>(RequestMachineryInquiryErrors.CountIsInvalid);

            var thirdParties = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long>() { inquiry.ThirdPartyId }, null, false, null), ct);
            if (thirdParties.IsFailure)
                return Result.Failure<CreateRequestMachineryInquiryResponse>(thirdParties.Error!);

            var getCurrency = await _mediator.Send(new GetCurrencyByIdQuery(inquiry.CurrencyId), ct);
            if (getCurrency.IsFailure)
                return Result.Failure<CreateRequestMachineryInquiryResponse>(getCurrency.Error!);

            var updateInquiryResponse = await _mediator.Send(new UpdateRequestMachineryInquiryCommand(inquiry.ThirdPartyId, inquiry.Count,
                inquiry.Unit, inquiry.UnitPrice, finalPrice, inquiry.CurrencyId, inquiry.Description, inquiry.InquiryRequestedTime,
                inquiry.Id!.Value), ct);
            if (updateInquiryResponse.IsFailure)
                return Result.Failure<CreateRequestMachineryInquiryResponse>(updateInquiryResponse.Error!);
            var requestMachineryInquiry = updateInquiryResponse.Value!;

            if (inquiry.Documents is not null && inquiry.Documents.Count > 0)
            {
                var deleteDocuments = inquiry.Documents!.Where(oo => oo.IsDeleted && oo.Id != null).ToList();
                var newDocuments = inquiry.Documents!.Where(oo => oo.Id == null).ToList();
                var updateDocuments = inquiry.Documents!.Where(oo => oo.Id != null).ToList();

                foreach (var document in newDocuments)
                {
                    var createDocumentResponse = await _mediator.Send(new CreateRequestMachineryInquiryDocumentCommand(document.Url, requestMachineryInquiry), ct);
                    if (createDocumentResponse.IsFailure)
                        return Result.Failure<CreateRequestMachineryInquiryResponse>(createDocumentResponse.Error!);
                }

                foreach (var document in deleteDocuments)
                {
                    var deleteDocumentResponse = await _mediator.Send(new DeleteRequestMachineryInquiryDocumentCommand(document.Id!.Value), ct);
                    if (deleteDocumentResponse.IsFailure)
                        return Result.Failure<CreateRequestMachineryInquiryResponse>(deleteDocumentResponse.Error!);
                }

                foreach (var document in updateDocuments)
                {
                    var updateDocumentResponse = await _mediator.Send(new UpdateRequestMachineryInquiryDocumentCommand(document.Id!.Value, document.Url), ct);
                    if (updateDocumentResponse.IsFailure)
                        return Result.Failure<CreateRequestMachineryInquiryResponse>(updateDocumentResponse.Error!);
                }
            }
        }

        var status = request.EndInquiry ? RequestMachineryStatus.EndInquiry : RequestMachineryStatus.Inquiry;
        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(requestMachinery, status, null, null), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<CreateRequestMachineryInquiryResponse>(updateStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new CreateRequestMachineryInquiryResponse(requestMachinery.Id);
    }

    public async Task<Result<UpdateRequestMachineryAssignmentsResponse?>> UpdateRequestMachineryAssignmentsAsync(UpdateRequestMachineryAssignmentsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestMachineryAssignmentsValidator, UpdateRequestMachineryAssignmentsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestMachineryAssignmentsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateRequestMachineryAssignmentsResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.OnProject))
            return Result.Failure<UpdateRequestMachineryAssignmentsResponse>(RequestMachineryErrors.InValidStatus);

        if (requestMachinery.DailyMachineries.Any())
            return Result.Failure<UpdateRequestMachineryAssignmentsResponse>(RequestMachineryErrors.InDailyOperations);

        if (request.MachineryIdentifiers?.Count > 0 && request.MachineryIdentifiers is not null)
        {
            var identifiers = request.MachineryIdentifiers.Select(x => x.MachineryIdentifier).Distinct().ToList();
            if (identifiers.Count < request.MachineryIdentifiers?.Count)
                return Result.Failure<UpdateRequestMachineryAssignmentsResponse>(RequestMachineryErrors.ExistsIdentifierInList);

            if (request.MachineryIdentifiers!.Any(x => string.IsNullOrEmpty(x.MachineryIdentifier)))
                return Result.Failure<UpdateRequestMachineryAssignmentsResponse>(RequestMachineryErrors.InValidMachineryIdentifier);

            foreach (var machineryIdentifier in request.MachineryIdentifiers!)
            {
                var createResponse = await _mediator.Send(new CreateRequestMachineryAssignmentCommand(
                    machineryIdentifier.Id, machineryIdentifier.MachineryIdentifier, requestMachinery, machineryIdentifier.IsDeleted), ct);
                if (createResponse.IsFailure)
                    return Result.Failure<UpdateRequestMachineryAssignmentsResponse>(createResponse.Error!);
            }
        }

        await _unitOfWork.CommitAsync(ct);

        return new UpdateRequestMachineryAssignmentsResponse(requestMachinery.Id, true);
    }

    public async Task<Result<SetRequestMachineryInquiryOperatorResponse?>> SetRequestMachineryInquiryOperatorAsync(SetRequestMachineryInquiryOperatorRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryInquiryOperatorValidator, SetRequestMachineryInquiryOperatorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.Confirmed ||
              requestMachinery.Status == RequestMachineryStatus.Inquiry ||
              requestMachinery.Status == RequestMachineryStatus.EndInquiry ||
              requestMachinery.Status == RequestMachineryStatus.InquiryRejected))
            return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(RequestMachineryInquiryErrors.InValidStatus);

        var activeOperators = requestMachinery.InquiryOperators.Where(c => c.IsActive).ToList();
        if (activeOperators is not null && activeOperators.Count > 0)
            foreach (var @operator in activeOperators)
            {
                var inActiveOperatorResponse = await _mediator.Send(new InActiveRequestMachineryInquiryOperatorCommand(
                    request.RequestMachineryId, @operator.OperatorAppoinmentId), ct);
                if (inActiveOperatorResponse.IsFailure)
                    return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(inActiveOperatorResponse.Error!);
            }

        var operatorAppoinment = requestMachinery.InquiryOperators.Where(c => c.OperatorAppoinmentId.Equals(
            request.OperatorAppoinmentId)).SingleOrDefault();
        var operatorAppoinmentId = operatorAppoinment?.OperatorAppoinmentId;
        var operatorAppoinmentUserId = operatorAppoinment?.OperatorAppoinmentUserId;

        if (operatorAppoinment is not null)
        {
            var activeOperatorResponse = await _mediator.Send(new ActiveRequestMachineryInquiryOperatorCommand(
                request.RequestMachineryId, operatorAppoinment.OperatorAppoinmentId), ct);
            if (activeOperatorResponse.IsFailure)
                return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(activeOperatorResponse.Error!);
        }
        else
        {
            var getOfficers = await _mediator.Send(new GetMachineryOperatorsQuery(request.OperatorAppoinmentId, null, null, null, null, 1, 1), ct);
            if (getOfficers.IsFailure)
                return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(getOfficers.Error!);
            var inquiryOfficers = getOfficers.Value!.Data!.FirstOrDefault()!;

            if (inquiryOfficers is null)
                return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(RequestMachineryInquiryOperatorErrors.InValidOperatorId);

            operatorAppoinmentId = inquiryOfficers.Id;
            operatorAppoinmentUserId = inquiryOfficers.UserId;

            var createOperatorResponse = await _mediator.Send(new CreateRequestMachineryInquiryOperatorCommand(
                operatorAppoinmentId.Value, operatorAppoinmentUserId!.Value, requestMachinery), ct);
            if (createOperatorResponse.IsFailure)
                return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(createOperatorResponse.Error!);
            var requestMachineryOperator = createOperatorResponse.Value!;
        }

        var updateOperatorAppoinment = await _mediator.Send(new UpdateRequestMachineryOperatorAppoinmentCommand(
            requestMachinery.Id, operatorAppoinmentId!.Value, operatorAppoinmentUserId!.Value), ct);
        if (updateOperatorAppoinment.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(updateOperatorAppoinment.Error!);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.Inquiry, null, null), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryOperatorResponse>(updateStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestMachineryInquiryOperatorResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryConfirmedResponse?>> SetRequestMachineryConfirmedAsync(SetRequestMachineryConfirmedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryConfirmedValidator, SetRequestMachineryConfirmedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryConfirmedResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryConfirmedResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.Pending))
            return Result.Failure<SetRequestMachineryConfirmedResponse>(RequestMachineryErrors.InValidStatus);

        decimal? confirmTime = null;
        if (!string.IsNullOrEmpty(request.ConfirmedTimeRequired))
            confirmTime = Convert.ToDecimal(request.ConfirmedTimeRequired);
        else
            confirmTime = requestMachinery.TimeRequired;

        var updateStatusResponse = await _mediator.Send(new UpdateRequestMachineryConfirmDescriptionCommand(
            requestMachinery, confirmTime, request.Description), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestMachineryConfirmedResponse>(updateStatusResponse.Error!);

        if (!(updateStatusResponse.Value!.Status == RequestMachineryStatus.Confirmed ||
            updateStatusResponse.Value!.Status == RequestMachineryStatus.Inquiry ||
            updateStatusResponse.Value!.Status == RequestMachineryStatus.EndInquiry))
            return Result.Failure<SetRequestMachineryConfirmedResponse>(RequestMachineryInquiryErrors.InValidStatus);

        requestMachinery = updateStatusResponse.Value;

        var activeOperators = requestMachinery.InquiryOperators.Where(c => c.IsActive).ToList();
        if (activeOperators is not null && activeOperators.Count > 0)
            foreach (var @operator in activeOperators)
            {
                var inActiveOperatorResponse = await _mediator.Send(new InActiveRequestMachineryInquiryOperatorCommand(
                    request.RequestMachineryId, @operator.OperatorAppoinmentId), ct);
                if (inActiveOperatorResponse.IsFailure)
                    return Result.Failure<SetRequestMachineryConfirmedResponse>(inActiveOperatorResponse.Error!);
            }

        var operatorAppoinment = requestMachinery.InquiryOperators.Where(c => c.OperatorAppoinmentId.Equals(
            request.OperatorAppoinmentId)).SingleOrDefault();
        var operatorAppoinmentId = operatorAppoinment?.OperatorAppoinmentId;
        var operatorAppoinmentUserId = operatorAppoinment?.OperatorAppoinmentUserId;

        if (operatorAppoinment is not null)
        {
            var activeOperatorResponse = await _mediator.Send(new ActiveRequestMachineryInquiryOperatorCommand(
                request.RequestMachineryId, operatorAppoinment.OperatorAppoinmentId), ct);
            if (activeOperatorResponse.IsFailure)
                return Result.Failure<SetRequestMachineryConfirmedResponse>(activeOperatorResponse.Error!);
        }
        else
        {
            var getOfficers = await _mediator.Send(new GetMachineryOperatorsQuery(request.OperatorAppoinmentId, null, null, null, null, 1, 1), ct);
            if (getOfficers.IsFailure)
                return Result.Failure<SetRequestMachineryConfirmedResponse>(getOfficers.Error!);
            var inquiryOfficers = getOfficers.Value!.Data!.FirstOrDefault()!;

            if (inquiryOfficers is null)
                return Result.Failure<SetRequestMachineryConfirmedResponse>(RequestMachineryInquiryOperatorErrors.InValidOperatorId);

            operatorAppoinmentId = inquiryOfficers.Id;
            operatorAppoinmentUserId = inquiryOfficers.UserId;

            var createOperatorResponse = await _mediator.Send(new CreateRequestMachineryInquiryOperatorCommand(
                operatorAppoinmentId.Value, operatorAppoinmentUserId!.Value, requestMachinery), ct);
            if (createOperatorResponse.IsFailure)
                return Result.Failure<SetRequestMachineryConfirmedResponse>(createOperatorResponse.Error!);
            var requestMachineryOperator = createOperatorResponse.Value!;
        }

        var updateOperatorAppoinment = await _mediator.Send(new UpdateRequestMachineryOperatorAppoinmentCommand(
            requestMachinery.Id, operatorAppoinmentId!.Value, operatorAppoinmentUserId!.Value), ct);
        if (updateOperatorAppoinment.IsFailure)
            return Result.Failure<SetRequestMachineryConfirmedResponse>(updateOperatorAppoinment.Error!);

        var updateRequestStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.Inquiry, null, null), ct);
        if (updateRequestStatusResponse.IsFailure)
            return Result.Failure<SetRequestMachineryConfirmedResponse>(updateRequestStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestMachineryConfirmedResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryDoneResponse?>> SetRequestMachineryDoneAsync(SetRequestMachineryDoneRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryDoneValidator, SetRequestMachineryDoneRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryDoneResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryDoneResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.OnProject))
            return Result.Failure<SetRequestMachineryDoneResponse>(RequestMachineryErrors.InValidStatus);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.Done, requestMachinery.Description, null), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestMachineryDoneResponse>(updateStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestMachineryDoneResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryInquiryDoneResponse?>> SetRequestMachineryInquiryDoneAsync(SetRequestMachineryInquiryDoneRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryInquiryDoneValidator, SetRequestMachineryInquiryDoneRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryDoneResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryDoneResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.EndInquiry))
            return Result.Failure<SetRequestMachineryInquiryDoneResponse>(RequestMachineryErrors.InValidStatus);

        var confirmedUser = _userProfileService.GetProfileInfo();

        var confirmInquiryResponse = await _mediator.Send(new ConfirmRequestMachineryInquiryCommand(
            request.RequestMachineryId, request.RequestMachineryInquiryId, confirmedUser.UserId), ct);
        if (confirmInquiryResponse.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryDoneResponse>(confirmInquiryResponse.Error!);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.InquiryDone, request.Description, null), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryDoneResponse>(updateStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetRequestMachineryInquiryDoneResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryInquiryAppointmentResponse?>> SetRequestMachineryInquiryAppointmentAsync(SetRequestMachineryInquiryAppointmentRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryInquiryAppointmentValidator, SetRequestMachineryInquiryAppointmentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryAppointmentResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryAppointmentResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.EndInquiry))
            return Result.Failure<SetRequestMachineryInquiryAppointmentResponse>(RequestMachineryErrors.InValidStatus);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.MachineryAppoinment, request.Description, null), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryAppointmentResponse>(updateStatusResponse.Error!);

        var confirmedUser = _userProfileService.GetProfileInfo();

        var confirmInquiryResponse = await _mediator.Send(new ConfirmRequestMachineryInquiryCommand(
            request.RequestMachineryId, request.RequestMachineryInquiryId, confirmedUser.UserId), ct);
        if (confirmInquiryResponse.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryAppointmentResponse>(confirmInquiryResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestMachineryInquiryAppointmentResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryInquiryRejectedResponse?>> SetRequestMachineryInquiryRejectedAsync(SetRequestMachineryInquiryRejectedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryInquiryRejectedValidator, SetRequestMachineryInquiryRejectedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryRejectedResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryRejectedResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.EndInquiry))
            return Result.Failure<SetRequestMachineryInquiryRejectedResponse>(RequestMachineryErrors.InValidStatus);

        var updateResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.InquiryRejected, request.Description, null), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<SetRequestMachineryInquiryRejectedResponse>(updateResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestMachineryInquiryRejectedResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryOnProjectResponse?>> SetRequestMachineryOnProjectAsync(SetRequestMachineryOnProjectRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryOnProjectValidator, SetRequestMachineryOnProjectRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryOnProjectResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryOnProjectResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.InquiryDone))
            return Result.Failure<SetRequestMachineryOnProjectResponse>(RequestMachineryErrors.InValidStatus);

        var contractorId = requestMachinery.InquiryOperators.Where(x => x.Inquiries is not null && x.Inquiries.Count > 0)
            .SelectMany(x => x.Inquiries).Where(z => z.IsConfirmed == true).FirstOrDefault()?.ThirdPartyId;

        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.OnProject, requestMachinery.Description, contractorId), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestMachineryOnProjectResponse>(updateStatusResponse.Error!);

        var setConfirmDate = await _mediator.Send(new SetOnProjectRequestMachineryConfirmDateCommand(requestMachinery), ct);
        if (setConfirmDate.IsFailure)
            return Result.Failure<SetRequestMachineryOnProjectResponse>(setConfirmDate.Error!);

        if (request.MachineryIdentifiers?.Count > 0 && request.MachineryIdentifiers is not null)
        {
            var identifiers = request.MachineryIdentifiers.Select(x => x.MachineryIdentifier).Distinct().ToList();
            if (identifiers.Count < request.MachineryIdentifiers?.Count)
                return Result.Failure<SetRequestMachineryOnProjectResponse>(RequestMachineryErrors.ExistsIdentifierInList);

            if (request.MachineryIdentifiers!.Any(x => string.IsNullOrEmpty(x.MachineryIdentifier)))
                return Result.Failure<SetRequestMachineryOnProjectResponse>(RequestMachineryErrors.InValidMachineryIdentifier);

            foreach (var machineryIdentifier in request.MachineryIdentifiers!)
            {
                var createAssignmentResponse = await _mediator.Send(new CreateRequestMachineryAssignmentCommand(
                    machineryIdentifier.Id, machineryIdentifier.MachineryIdentifier, requestMachinery, machineryIdentifier.IsDeleted), ct);
                if (createAssignmentResponse.IsFailure)
                    return Result.Failure<SetRequestMachineryOnProjectResponse>(createAssignmentResponse.Error!);
            }
        }

        await _unitOfWork.CommitAsync(ct);

        var userExtra = await _userProfileService.GetExtraInfo(ct);

        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
            await SendMessageRequestMachinery(requestMachinery, $"{userExtra.FirstName} {userExtra.LastName}", ct);

        return new SetRequestMachineryOnProjectResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryPendingResponse?>> SetRequestMachineryPendingAsync(SetRequestMachineryPendingRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryPendingValidator, SetRequestMachineryPendingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryPendingResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryPendingResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.New))
            return Result.Failure<SetRequestMachineryPendingResponse>(RequestMachineryErrors.InValidStatus);

        var updateResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.Pending, requestMachinery.Description, null), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<SetRequestMachineryPendingResponse>(updateResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestMachineryPendingResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryManagerConfirmResponse?>> SetRequestMachineryManagerConfirm(SetRequestMachineryManagerConfirmRequest request, CT ct)
    {
        var hasAccessProduct = await _userProfileService.HasUserAccessToAction(3410, ct);
        if (!hasAccessProduct.IsOk) return Result.Failure<SetRequestMachineryManagerConfirmResponse>(RequestMachineryErrors.NotAllowedToConfirm);

        var response = await _mediator.Send(new UpdateRequestMachineryOnPaymentStateCommand(
            request.Ids, RequestMachineryStatus.ManagerConfirm, request.Description), ct);
        if (response.IsFailure) return Result.Failure<SetRequestMachineryManagerConfirmResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetRequestMachineryManagerConfirmResponse(request.Ids, true);
    }

    public async Task<Result<SetRequestMachinerySendToManagerResponse?>> SetRequestMachinerySendToManager(SetRequestMachinerySendToManagerRequest request, CT ct)
    {
        var response = await _mediator.Send(new UpdateRequestMachineryOnPaymentStateCommand(
            request.Ids, RequestMachineryStatus.SendToManager, request.Description), ct);
        if (response.IsFailure) return Result.Failure<SetRequestMachinerySendToManagerResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetRequestMachinerySendToManagerResponse(request.Ids, true);
    }

    public async Task<Result<SetRequestMachineryBackToOnProjectResponse?>> SetRequestMachineryBackToOnProject(SetRequestMachineryBackToOnProjectRequest request, CT ct)
    {
        var response = await _mediator.Send(new UpdateRequestMachineryOnPaymentStateCommand(
            request.Ids, RequestMachineryStatus.OnProject, request.Description), ct);
        if (response.IsFailure) return Result.Failure<SetRequestMachineryBackToOnProjectResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetRequestMachineryBackToOnProjectResponse(request.Ids, true);
    }

    public async Task<Result<SetRequestMachineryRejectedResponse?>> SetRequestMachineryRejectedAsync(SetRequestMachineryRejectedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryRejectedValidator, SetRequestMachineryRejectedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryRejectedResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryRejectedResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.Pending))
            return Result.Failure<SetRequestMachineryRejectedResponse>(RequestMachineryErrors.InValidStatus);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.Rejected, request.Description, null), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestMachineryRejectedResponse>(updateStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestMachineryRejectedResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryReturnedResponse?>> SetRequestMachineryReturnedAsync(SetRequestMachineryReturnedRequest request, CT ct)
    {
        var validationResult = await request.IsValidAsync<SetRequestMachineryReturnedValidator, SetRequestMachineryReturnedRequest>(ct);
        if (validationResult.IsFailure)
            return Result.Failure<SetRequestMachineryReturnedResponse>(validationResult.Error!);

        var requestMachineryResult = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (requestMachineryResult.IsFailure)
            return Result.Failure<SetRequestMachineryReturnedResponse>(requestMachineryResult.Error!);

        var requestMachinery = requestMachineryResult.Value!;

        var statusValidationResult = ValidateRequestMachineryStatus(requestMachinery);
        if (statusValidationResult.IsFailure)
            return Result.Failure<SetRequestMachineryReturnedResponse>(statusValidationResult.Error!);

        if (requestMachinery.Status == RequestMachineryStatus.OnProject)
        {
            var cleanupResult = await CleanupRequestMachineryResources(requestMachinery, ct);
            if (cleanupResult.IsFailure)
                return Result.Failure<SetRequestMachineryReturnedResponse>(cleanupResult.Error!);
        }

        var updateStatusResult = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.Returned, request.Description, null), ct);
        if (updateStatusResult.IsFailure)
            return Result.Failure<SetRequestMachineryReturnedResponse>(updateStatusResult.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestMachineryReturnedResponse(requestMachinery.Id);
    }

    public async Task<Result<SetRequestMachineryResendedResponse?>> SetRequestMachineryResendedAsync(SetRequestMachineryResendedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestMachineryResendedValidator, SetRequestMachineryResendedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestMachineryResendedResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestMachineryResendedResponse>(response.Error!);
        var requestMachinery = response.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.Pending))
            return Result.Failure<SetRequestMachineryResendedResponse>(RequestMachineryErrors.InValidStatus);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(
            requestMachinery, RequestMachineryStatus.Resended, request.Description, null), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestMachineryResendedResponse>(updateStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestMachineryResendedResponse(requestMachinery.Id);
    }

    public async Task<Result<GetRequestMachineryInquiriesResponse?>> GetRequestMachineryInquiriesRequestAsync(GetRequestMachineryInquiriesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryInquiriesValidator, GetRequestMachineryInquiriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryInquiriesResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<GetRequestMachineryInquiriesResponse>(getRequestMachinery.Error!);
        var requestMachinery = getRequestMachinery.Value!;

        Company? company = null;
        if (requestMachinery.CompanyId is not null && requestMachinery.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(requestMachinery.CompanyId, _mediator, ct);

        var inquries = requestMachinery.InquiryOperators.Where(oo => oo.Inquiries is not null && oo.Inquiries.Count > 0).SelectMany(oo => oo.Inquiries).ToList();

        var currencyIds = inquries.Where(x => x.CurrencyId > 0).Select(x => x.CurrencyId).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var thirdPartyIds = inquries.Where(x => x.ThirdPartyId > 0).Select(x => x.ThirdPartyId).Distinct().ToList();
        var appointmentIds = inquries.Where(x => x.RequestMachineryInquiryOperator is not null)
            .Select(x => x.RequestMachineryInquiryOperator.OperatorAppoinmentId).Where(x => x != 0).Distinct().ToList();
        thirdPartyIds.AddRange(appointmentIds);
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        var response = requestMachinery.Adapt<GetRequestMachineryInquiriesResponse>();

        List<GetRequestMachineryInquiriesResponseModel>? inquriesModel = [];
        if (inquries is not null && inquries.Count > 0)
        {
            foreach (var inquiry in inquries)
            {
                var inquiryModel = InquiryDataModeling(inquiry, currencies, thirdParties);
                inquriesModel.Add(inquiryModel!);
            }
            response.Data = inquriesModel.OrderByDescending(x => x.Created).ToList();
        }

        return response;
    }

    public async Task<Result<GetFilteredRequestMachineryManagementsResponse?>> GetFilteredRequestMachineriesAsync(GetFilteredRequestMachineryManagementsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredRequestMachineryManagementsValidator, GetFilteredRequestMachineryManagementsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredRequestMachineryManagementsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetFilteredRequestMachineriesQuery(null,
            request.CostCenterId,
            request.ProjectId,
            request.ContractorIds,
            request.ProjectOperationIds,
            request.OperationInfoIds,
            request.MachineriesGroupId,
            request.MachineryId,
            request.Status,
            request.PaymentType,
            request.FromDate,
            request.ToDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.CreatorId,
            null,
            null,
            request.DriverName,
            request.FilterData,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetFilteredRequestMachineryManagementsResponse>(response.Error!);
        var requestMachineries = response.Value!.Data!;

        var companyIds = requestMachineries?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? allIds = [];
        allIds.AddRange(IdCollectors(requestMachineries!).ToList());
        var histories = requestMachineries!.SelectMany(x => x.Histories).ToList();
        var requestMachineryHistories = histories.Where(c => c.Status == RequestMachineryStatus.Confirmed).ToList();
        if (requestMachineryHistories is not null)
            allIds.AddRange(RequestMachineryHistoryIdCollectors(histories!).ToList());
        var thirdParties = await WebServicesLogic.UserDataReceiver(allIds, null, _mediator, ct);

        List<long>? thirdpartyIds = [];
        thirdpartyIds.AddRange(requestMachineries!.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        thirdpartyIds.AddRange(requestMachineries!.Where(x => x.DriverId != null && x.DriverId > 0).Select(x => (long)x.DriverId!).Distinct().ToList() ?? []);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdpartyIds, null, null, _mediator, ct);

        var models = new List<GetFilteredRequestMachineryManagementsModel>();
        if (requestMachineries != null && requestMachineries.Count > 0)
            models = GetFilteredManagementDataModeling(requestMachineries, requestMachineryHistories, histories, thirdParties, contractors, companies);

        return new GetFilteredRequestMachineryManagementsResponse(models!, response.Value!.RowCount!);
    }

    public async Task<Result<GetsTotalFilteredRequestMachineryResponse?>> GetsTotalFilteredRequestMachinery(GetsTotalFilteredRequestMachineryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsTotalFilteredRequestMachineryValidator, GetsTotalFilteredRequestMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTotalFilteredRequestMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsTotalFilteredRequestMachineryQuery(null,
            request.CostCenterId,
            request.ProjectId,
            request.ContractorIds,
            request.ProjectOperationIds,
            request.OperationInfoIds,
            request.MachineriesGroupId,
            request.MachineryId,
            request.Status,
            request.PaymentType,
            request.FromDate,
            request.ToDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.CreatorId,
            null,
            null,
            request.DriverName,
            request.FilterData,
            companyId,
            null,
            0,
            0), ct);
        if (response.IsFailure)
            return Result.Failure<GetsTotalFilteredRequestMachineryResponse>(response.Error!);
        var requestMachineries = response.Value!.Data!;

        var totals = CalculateMachineryTotals(requestMachineries);

        return new GetsTotalFilteredRequestMachineryResponse()
        {
            FinalPrice = totals.TotalPrice,
            RequestedCount = requestMachineries.Sum(x => x.RequestCount),
            DailyTimes = totals.TotalDaily,
            HourlyTimes = totals.TotalHourly,
            ServiceTimes = totals.TotalServiced,
            VolumeTimes = totals.TotalVolumes
        };
    }

    public async Task<Result<GetFilteredRequestMachineryInquieriesResponse?>> GetFilteredRequestMachineryInquieriesAsync(GetFilteredRequestMachineryInquieriesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredRequestMachineryInquieriesValidator, GetFilteredRequestMachineryInquieriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredRequestMachineryInquieriesResponse>(isValidRequest.Error!);

        long? currentUser = _userProfileService.GetProfileInfo().UserId;
        var result = await _userProfileService.HasUserAccessToAction(982, ct);
        if (result.Status == ResponseStatusType.Ok)
            currentUser = null;

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetFilteredRequestMachineriesQuery(
            null,
            request.CostCenterId,
            request.ProjectId,
            request.ContractorIds,
            null,
            null,
            null,
            null,
            request.Status,
            null,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            null,
            currentUser,
            request.RequestNumber,
            null,
            request.FilterData,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetFilteredRequestMachineryInquieriesResponse>(response.Error!);
        var requestMachineries = response.Value!.Data!;

        var companyIds = requestMachineries?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? allIds = [];
        allIds.AddRange(IdCollectors(requestMachineries!).ToList());
        var histories = requestMachineries!.SelectMany(x => x.Histories).ToList();
        var historiesData = histories.Where(c => c.Status == RequestMachineryStatus.Confirmed).ToList();
        if (historiesData is not null)
            allIds.AddRange(RequestMachineryHistoryIdCollectors(histories!).ToList());

        var thirdParties = await WebServicesLogic.UserDataReceiver(allIds, null, _mediator, ct);

        var models = new List<GetFilteredRequestMachineryInquieriesModel>();
        if (requestMachineries != null && requestMachineries.Count > 0)
            models = GetFilteredInquiryDataModeling(requestMachineries, historiesData, histories, thirdParties, companies);

        return new GetFilteredRequestMachineryInquieriesResponse(
            models?.OrderByDescending(x => x.Created).ToList() ?? new List<GetFilteredRequestMachineryInquieriesModel>(0),
            response.Value!.RowCount!);
    }

    public async Task<Result<GetRequestMachineryManagementByIdResponse?>> GetRequestMachineryByIdAsync(GetRequestMachineryManagementByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryManagementByIdValidator, GetRequestMachineryManagementByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryManagementByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<GetRequestMachineryManagementByIdResponse>(response.Error!);
        var requestMachinery = response.Value!;

        Company? company = null;
        if (requestMachinery.CompanyId is not null && requestMachinery.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(requestMachinery.CompanyId, _mediator, ct);

        List<long>? allIds = [];
        allIds.AddRange(IdCollectors(new List<RequestMachinery?>() { requestMachinery! }).ToList());
        var history = requestMachinery.Histories.Where(c => c.Status == RequestMachineryStatus.Confirmed).FirstOrDefault();
        if (history is not null)
            allIds.AddRange(RequestMachineryHistoryIdCollectors(new List<RequestMachineryHistory?>() { history! }).ToList());

        List<FilteredUserResponseModel>? thirdParties = [];
        thirdParties = await WebServicesLogic.UserDataReceiver(allIds, null, _mediator, ct);

        var requestMachineryProjectOperations = requestMachinery.ProjectOperations.ToList();
        var projectOperations = new List<GetRequestMachineryManagementByIdProjectOperationModel>();
        foreach (var projectOperation in requestMachineryProjectOperations)
        {
            projectOperations.Add(new GetRequestMachineryManagementByIdProjectOperationModel()
            {
                OperationInfoCode = projectOperation.ProjectOperation.OperationInfo.OperationInfoCode,
                OperationInfoName = projectOperation.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationId = projectOperation.ProjectOperation.Id,
                Id = projectOperation.Id
            });
        }

        var requestMachineryProjectOperationDetails = requestMachinery.ProjectOperationDetails.ToList();
        var projectOperationDetails = new List<GetRequestMachineryManagementByIdProjectOperationDetailModel>();
        foreach (var projectOperationDetail in requestMachineryProjectOperationDetails)
        {
            projectOperationDetails.Add(new GetRequestMachineryManagementByIdProjectOperationDetailModel()
            {
                PrivateName = projectOperationDetail.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = projectOperationDetail.ProjectOperationDetail.OperationLocation.PrivateCode,
                PublicName = projectOperationDetail.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = projectOperationDetail.ProjectOperationDetail.OperationLocation.PublicCode,
                ProjectOperationDetailId = projectOperationDetail.ProjectOperationDetail.Id,
                Id = projectOperationDetail.Id
            });
        }

        var assignments = new List<GetRequestMachineryManagementByIdAssigmentModel>();
        foreach (var assigment in requestMachinery.RequestMachineryAssignments)
        {
            assignments.Add(new GetRequestMachineryManagementByIdAssigmentModel()
            {
                Id = assigment.Id,
                MachineryIdentifier = assigment.MachineryIdentifier
            });
        }

        var appoinment = new GetRequestMachineryManagementByIdOperatorModel()
        {
            AppointmentId = requestMachinery.OperatorAppoinmentId,
            AppointmentUserId = requestMachinery.OperatorAppoinmentUserId,
            AppointmentFullName = thirdParties?.Where(x => x.UserId == requestMachinery.OperatorAppoinmentUserId).FirstOrDefault()?.FullName,
        };

        List<RequestMachineryDocumentResponseModel>? documents = [];
        if (requestMachinery.RequestMachineryDocuments is not null && requestMachinery.RequestMachineryDocuments.Count > 0)
            foreach (var item in requestMachinery.RequestMachineryDocuments)
            {
                documents.Add(new RequestMachineryDocumentResponseModel(item.Id, item.Url));
            }

        List<RequestMachineryBillDocumentResponseModel>? billDocuments = [];
        if (requestMachinery.RequestMachineryBillDocuments is not null && requestMachinery.RequestMachineryBillDocuments.Count > 0)
            foreach (var item in requestMachinery.RequestMachineryBillDocuments)
            {
                billDocuments.Add(new RequestMachineryBillDocumentResponseModel(item.Id, item.Url));
            }

        var times = GetTimeRequireds(requestMachinery);

        var data = new GetRequestMachineryManagementByIdResponse()
        {
            CostCenterId = requestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
            CostCenterName = requestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
            Description = requestMachinery.Description,
            MachineriesGroupId = requestMachinery.Machinery.MachineriesGroup.Id,
            MachineriesGroupName = requestMachinery.Machinery.MachineriesGroup.GroupName,
            MachineryId = requestMachinery.Machinery?.Id,
            MachineryName = requestMachinery.Machinery?.MachineryName,
            ProjectId = requestMachinery.Project.Id,
            ProjectName = requestMachinery.Project.ProjectName,
            RequestCount = requestMachinery.RequestCount,
            RequestMachineryId = requestMachinery.Id,
            Status = requestMachinery.Status,
            TimeRequired = times.timeRequired,
            FromDate = requestMachinery.FromDate,
            FromTime = requestMachinery.FromDate != null ? requestMachinery.FromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            ToDate = requestMachinery.ToDate,
            ToTime = requestMachinery.ToDate != null ? requestMachinery.ToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            Unit = requestMachinery.Unit,
            ProjectOperations = projectOperations,
            ProjectOperationDetails = projectOperationDetails,
            Assignments = assignments,
            Creator = thirdParties?.Where(x => x.UserId == requestMachinery.CreatorId).FirstOrDefault()?.FullName,
            CreatorId = requestMachinery.CreatorId,
            InquiryOperator = appoinment,
            ConfirmDate = history?.Created,
            ConfirmUserId = thirdParties?.Where(x => x.UserId == history?.CreatorId).FirstOrDefault()?.UserId,
            ConfirmUser = thirdParties?.Where(x => x.UserId == history?.CreatorId).FirstOrDefault()?.FullName,
            CompanyId = requestMachinery.CompanyId,
            CompanyNameFa = company?.NameFa,
            RequestMachineryDocuments = documents,
            ConfirmFromDate = requestMachinery.ConfirmFromDate,
            ConfirmFromTime = requestMachinery.ConfirmFromDate != null ? requestMachinery.ConfirmFromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            ConfirmToDate = requestMachinery.ConfirmToDate,
            ConfirmToTime = requestMachinery.ConfirmToDate != null ? requestMachinery.ConfirmToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            RequestNumber = requestMachinery.RequestNumber,
            RequestMachineryBillDocuments = billDocuments,
            UnitPrice = requestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.UnitPrice,
            TotalPrice = requestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.TotalPrice
        };

        return data;
    }

    public async Task<Result<GetRequestMachineryInquiryOperatorsResponse?>> GetRequestMachineryInquiryOperatorsAsync(GetRequestMachineryInquiryOperatorsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryInquiryOperatorsValidator, GetRequestMachineryInquiryOperatorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryInquiryOperatorsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<GetRequestMachineryInquiryOperatorsResponse>(response.Error!);
        var requestMachinery = response.Value!;

        Company? company = null;
        if (requestMachinery.CompanyId is not null && requestMachinery.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(requestMachinery.CompanyId, _mediator, ct);

        List<GetRequestMachineryInquiryOperatorsQueryModel> inquiryOperators = new();
        if (requestMachinery.Machinery is not null && requestMachinery.Machinery.MachineriesGroup is not null)
        {
            var getInquiryOperators = await _mediator.Send(new GetRequestMachineryInquiryOperatorsQuery(
                requestMachinery.Machinery.Id, requestMachinery.Machinery.MachineriesGroup.Id), ct);
            if (getInquiryOperators.IsFailure)
                return Result.Failure<GetRequestMachineryInquiryOperatorsResponse>(getInquiryOperators.Error!);
            inquiryOperators = getInquiryOperators.Value!;
        }

        List<MachineryOperatorAppointment>? OrderBy = [];
        if (inquiryOperators is not null && inquiryOperators.Count > 0)
            foreach (var item in inquiryOperators)
                OrderBy.Add(new MachineryOperatorAppointment(item.OperatorId, item.InquiryCountConfirmed));

        var getOfficers = await _mediator.Send(new GetMachineryOperatorsQuery(null, null, null, request.FilterData, OrderBy, request.PageIndex, request.PageSize), ct);
        if (getOfficers.IsFailure)
            return Result.Failure<GetRequestMachineryInquiryOperatorsResponse>(getOfficers.Error!);

        var officers = getOfficers.Value!.Data!.Where(x => x?.UserId != null && x.UserId != 0).ToList();

        var operators = new List<GetRequestMachineryInquiryOperatorsResponseModel>();
        foreach (var item in officers)
        {
            var inquiryOperator = inquiryOperators?.Where(x => x.OperatorId == item?.Id).FirstOrDefault();
            operators.Add(new GetRequestMachineryInquiryOperatorsResponseModel()
            {
                OperatorId = item!.Id,
                OperatorUserId = item!.UserId,
                OperatorCode = item.OrganizationCode,
                OperatorName = item.FullName,
                InquiryCount = inquiryOperator != null ? inquiryOperator.InquiryCount : 0,
                InquiryCountConfirmed = inquiryOperator != null ? inquiryOperator.InquiryCountConfirmed : 0,
                LastInquiryDate = inquiryOperator != null ? inquiryOperator.LastInquiryDate : null,
                TotalInquiryCount = inquiryOperator != null ? inquiryOperator.TotalInquiryCount : 0,
                DefaultPhoneNo = item.DefaultPhoneNo
            });
        }

        var data = new GetRequestMachineryInquiryOperatorsResponse()
        {
            MachineryGroupName = requestMachinery.Machinery?.MachineriesGroup?.GroupName,
            MachineryName = requestMachinery.Machinery?.MachineryName,
            RequestMachineryId = requestMachinery.Id,
            Data = operators,
            RowCount = getOfficers.Value?.RowCount ?? 0,
            CompanyId = requestMachinery.CompanyId,
            CompanyNameFa = company?.NameFa,
            RequestNumber = requestMachinery.RequestNumber,
        };

        return data;
    }

    public async Task<Result<GetRequestMachineryOperatorsResponse?>> GetRequestMachineryOperatorsAsync(GetRequestMachineryOperatorsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryOperatorsValidator, GetRequestMachineryOperatorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryOperatorsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<GetRequestMachineryOperatorsResponse>(response.Error!);
        var requestMachinery = response.Value!;

        Company? company = null;
        if (requestMachinery.CompanyId is not null && requestMachinery.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(requestMachinery.CompanyId, _mediator, ct);

        List<GetRequestMachineryOperatorsQueryModel> inquiryOperators = new();
        if (requestMachinery.Machinery is not null && requestMachinery.Machinery.MachineriesGroup is not null)
        {
            var getInquiryOperators = await _mediator.Send(new GetRequestMachineryOperatorsQuery(requestMachinery.Id, requestMachinery.Machinery.Id, requestMachinery.Machinery.MachineriesGroup.Id), ct);
            if (getInquiryOperators.IsFailure)
                return Result.Failure<GetRequestMachineryOperatorsResponse>(getInquiryOperators.Error!);
            inquiryOperators = getInquiryOperators.Value!;
        }

        List<MachineryOperatorAppointment>? OrderBy = [];
        if (inquiryOperators is not null && inquiryOperators.Count > 0)
            foreach (var item in inquiryOperators)
                OrderBy.Add(new MachineryOperatorAppointment(item.OperatorId, item.InquiryCountConfirmed));

        var getOfficers = await _mediator.Send(new GetMachineryOperatorsQuery(null, null, null, request.FilterData, OrderBy, request.PageIndex, request.PageSize), ct);
        if (getOfficers.IsFailure)
            return Result.Failure<GetRequestMachineryOperatorsResponse>(getOfficers.Error!);
        var result = getOfficers.Value!.Data!.Where(x => x?.UserId != null && x.UserId != 0).ToList();

        List<OfficerModel>? officers = [];
        foreach (var item in inquiryOperators!)
        {
            var user = result.FirstOrDefault(x => x?.Id == item.OperatorId);
            if (user == null)
                return Result.Failure<GetRequestMachineryOperatorsResponse>(RequestMachineryErrors.OperatorUnvalid!);

            officers.Add(user);
        }

        var operators = new List<GetRequestMachineryOperatorsResponseModel>();
        foreach (var item in officers)
        {
            var inquiryOperator = inquiryOperators?.FirstOrDefault(x => x.OperatorId == item?.Id);
            operators.Add(new GetRequestMachineryOperatorsResponseModel()
            {
                OperatorId = item!.Id,
                OperatorUserId = item!.UserId,
                OperatorCode = item.OrganizationCode,
                OperatorName = item.FullName,
                InquiryCount = inquiryOperator != null ? inquiryOperator.InquiryCount : 0,
                InquiryCountConfirmed = inquiryOperator != null ? inquiryOperator.InquiryCountConfirmed : 0,
                LastInquiryDate = inquiryOperator != null ? inquiryOperator.LastInquiryDate : null,
                TotalInquiryCount = inquiryOperator != null ? inquiryOperator.TotalInquiryCount : 0,
                DefaultPhoneNo = item.DefaultPhoneNo
            });
        }

        var data = new GetRequestMachineryOperatorsResponse()
        {
            MachineryGroupName = requestMachinery.Machinery?.MachineriesGroup?.GroupName,
            MachineryName = requestMachinery.Machinery?.MachineryName,
            RequestMachineryId = requestMachinery.Id,
            Data = operators,
            RowCount = officers?.Count ?? 0,
            CompanyId = requestMachinery.CompanyId,
            CompanyNameFa = company?.NameFa,
            RequestNumber = requestMachinery.RequestNumber,
        };

        return data;
    }

    public async Task<Result<GetsFilteredMachineryRequesterResponse?>> GetsFilteredMachineryRequesterAsync(GetsFilteredMachineryRequesterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationRequestRequesters");

        var isValidRequest = await request.IsValidAsync<GetsFilteredMachineryRequesterRequestValidator, GetsFilteredMachineryRequesterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredMachineryRequesterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsFilteredMachineryRequesterQuery(), ct);
        if (response.IsFailure)
            return Result.Failure<GetsFilteredMachineryRequesterResponse>(response.Error!);
        if (response is null)
            return Result.Failure<GetsFilteredMachineryRequesterResponse>(RequestMachineryErrors.FilteredMachineryRequestNotFound);
        var ids = response?.Value?.Data!;

        List<FilteredUserResponseModel>? requesters = new();
        var data = new List<GetsFilteredMachineryRequesterResponseModel>();
        if (ids?.Count > 0)
        {
            var responseValue = await WebServicesLogic.UserDataReceiver(ids!, request.FilterData, _mediator, ct);
            if (responseValue is not null && responseValue.Count > 0)
                foreach (var item in responseValue)
                {
                    requesters.Add(item);
                }

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var id in ids)
                {
                    if (!requesters.Any(x => x?.UserId == id))
                        continue;

                    var requester = requesters.Where(x => x?.UserId == id).FirstOrDefault();
                    data.Add(new GetsFilteredMachineryRequesterResponseModel()
                    {
                        Id = requester!.Id,
                        UserId = requester?.UserId,
                        FirstName = requester?.FirstName,
                        LastName = requester?.LastName,
                        DefaultPhoneNo = requester?.DefaultPhoneNo,
                        OrganizationCode = requester?.OrganizationCode,
                        IdentityNo = requester?.IdentityNo,
                        Nickname = requester?.Nickname
                    });
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    var requester = requesters.Where(x => x?.UserId == id).FirstOrDefault();
                    data.Add(new GetsFilteredMachineryRequesterResponseModel()
                    {
                        Id = requester!.Id,
                        UserId = requester?.UserId,
                        FirstName = requester?.FirstName,
                        LastName = requester?.LastName,
                        DefaultPhoneNo = requester?.DefaultPhoneNo,
                        OrganizationCode = requester?.OrganizationCode,
                        IdentityNo = requester?.IdentityNo,
                        Nickname = requester?.Nickname
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsFilteredMachineryRequesterResponse(responseData ?? new List<GetsFilteredMachineryRequesterResponseModel>(0), requesters?.Count ?? 0);

    }

    public async Task<Result<GetsFilteredMachineryContractorResponse?>> GetsFilteredMachineryContractorAsync(GetsFilteredMachineryContractorRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationRequestContractors");

        var isValidRequest = await request.IsValidAsync<GetsFilteredMachineryContractorRequestValidator, GetsFilteredMachineryContractorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredMachineryContractorResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsFilteredMachineryContractorQuery(request.CostCenterIds, request.ProjectIds), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsFilteredMachineryContractorResponse>(RequestMachineryErrors.FilteredMachineryContractorNotFound);
        var ids = response?.Value?.Data!;

        List<UserModel?>? contractors = new();
        var data = new List<GetsFilteredMachineryContractorResponseModel>();
        if (ids?.Count > 0)
        {
            var responseValue = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(ids!, request.FilterData, null, _mediator, ct);
            if (responseValue is not null && responseValue.Count > 0)
                foreach (var item in responseValue)
                {
                    contractors.Add(item);
                }

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var id in ids)
                {
                    if (!contractors.Any(x => x?.Id == id))
                        continue;

                    var contractor = contractors.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetsFilteredMachineryContractorResponseModel()
                    {
                        Id = contractor!.Id,
                        UserId = contractor?.UserId,
                        DefaultPhoneNo = contractor?.DefaultPhoneNo,
                        OrganizationCode = contractor?.OrganizationCode,
                        FullName = contractor?.FullName,
                        Nickname = contractor?.Nickname,
                    });
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    var contractor = contractors.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetsFilteredMachineryContractorResponseModel()
                    {
                        Id = contractor!.Id,
                        UserId = contractor?.UserId,
                        DefaultPhoneNo = contractor?.DefaultPhoneNo,
                        OrganizationCode = contractor?.OrganizationCode,
                        FullName = contractor?.FullName,
                        Nickname = contractor?.Nickname,
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsFilteredMachineryContractorResponse(responseData ?? new List<GetsFilteredMachineryContractorResponseModel>(0), contractors?.Count ?? 0);

    }

    public async Task<Result<GetsRequestMachineryManagementExcelExporterResponse?>> GetsRequestMachineryManagementExcelExporter(GetsRequestMachineryManagementExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsRequestMachineryManagementExcelExporter, ProjectId:{ProjectId},", request.ProjectId);

        var isValidRequest = await request.IsValidAsync<GetsRequestMachineryManagementExcelExporterValidator, GetsRequestMachineryManagementExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsRequestMachineryManagementExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetFilteredRequestMachineriesQuery(
            request.Ids,
            request.CostCenterId,
            request.ProjectId,
            request.ContractorIds,
            request.ProjectOperationIds,
            request.OperationInfoIds,
            request.MachineriesGroupId,
            request.MachineryId,
            request.Status,
            request.PaymentType,
            request.FromDate,
            request.ToDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.CreatorId,
            null,
            null,
            request.DriverName,
            request.FilterData,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsRequestMachineryManagementExcelExporterResponse>(RequestMachineryErrors.FilteredMachineryRequestNotFound!);
        var values = response.Value!.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<FilteredUserResponseModel>? usersInfo = [];
        var userIds = values!.Select(x => x.CreatorId).Distinct().ToList();
        var operatorIds = values!.Where(x => x.OperatorAppoinmentUserId != null && x.OperatorAppoinmentUserId > 0).Select(x => (long)x.OperatorAppoinmentUserId!).Distinct().ToList();
        if (operatorIds is not null && operatorIds.Count > 0)
            foreach (var item in operatorIds)
            {
                userIds.Add(item);
            }

        var histories = values!.SelectMany(x => x.Histories).ToList();
        userIds.AddRange(histories.Select(x => x.CreatorId).Distinct().ToList());
        var creatorResponse = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);
        if (creatorResponse is not null && creatorResponse.Count > 0)
            usersInfo.AddRange(creatorResponse);

        var contractorIds = values!.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var data = response.Value.Data.Adapt<List<GetsRequestMachineryManagementExcelExporterResponseModel>>();
        foreach (var item in data)
        {
            var value = response.Value.Data.FirstOrDefault(x => x.Id == item.Id);

            item.Contractor = contractors?.FirstOrDefault(x => x is not null && x.Id == value!.ContractorId)?.FullName;
            item.StatusDescription = value!.Status.GetEnumDescription();
            item.UnitDescription = value!.Unit.GetEnumDescription();
            item.FromDate = TimeCalculator.ConvertToShamsi(value!.FromDate);
            item.FromTime = value!.FromDate != null ? value!.FromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0);
            item.ToDate = TimeCalculator.ConvertToShamsi(value!.ToDate);
            item.ToTime = value!.ToDate != null ? value!.ToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0);
            item.ConfirmFromDate = TimeCalculator.ConvertToShamsi(value!.ConfirmFromDate);
            item.ConfirmFromTime = value!.ConfirmFromDate != null ? value!.ConfirmFromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0);
            item.ConfirmToDate = TimeCalculator.ConvertToShamsi(value!.ConfirmToDate);
            item.ConfirmToTime = value!.ConfirmToDate != null ? value!.ConfirmToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0);
            item.CompanyNameFa = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault()?.NameFa;
            item.Creator = usersInfo?.Where(x => x.UserId == item.CreatorId).FirstOrDefault()?.FullName;
            item.AppointmentFullName = usersInfo?.Where(x => x.UserId == item.AppointmentId).FirstOrDefault()?.FullName;
            if (item.ConfirmUserId is not null && item.ConfirmUserId != 0)
                item.ConfirmUser = usersInfo?.Where(x => x.UserId == item.CreatorId).FirstOrDefault()?.FullName;

            item.UnitPrice = value.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.UnitPrice;
            item.TotalPrice = value.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.TotalPrice;

            var requestHistory = histories?.LastOrDefault(x => x.RequestMachinery.Id == item.Id);
            var historyCreator = usersInfo?.FirstOrDefault(x => x.UserId == requestHistory?.CreatorId)?.FullName;

            string? statusDesc = string.Empty;
            if (value.Status == RequestMachineryStatus.Confirmed ||
                value.Status == RequestMachineryStatus.Rejected ||
                value.Status == RequestMachineryStatus.Returned)
                statusDesc = $"{historyCreator} وضعیت درخواست را به {value.Status.GetEnumDescription()} به دلیل ({requestHistory?.RequestDescription}) در تاریخ {TimeCalculator.ConvertToShamsi(requestHistory?.Created)} تغییر داد";
            else
                statusDesc = $"{historyCreator} وضعیت درخواست را به {value.Status.GetEnumDescription()} در تاریخ {TimeCalculator.ConvertToShamsi(requestHistory?.Created)} تغییر داد";
            item.changeStatusDescription = statusDesc;

            item.PaymentType = value.RequestMachineryStatusStatementDetails.Any() &&
                               value.RequestMachineryStatusStatementDetails.Any(z => !z.RequestMachineryStatusStatement.IsDeleted) &&
                               (value.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentDate != null) ||
                                value.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentOrderId != null)) ?
                                         RequestMachineryPaymentType.Paid : RequestMachineryPaymentType.NotPaid;
        }

        var file = new FileContentResult(RequestMachineryManagementExcels.RequestMachineryManagementToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"RequestMachineryManagements-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsRequestMachineryManagementExcelExporterResponse(file);
    }

    public async Task<Result<GetsRequestMachineryManagementExcelEnumResponse?>> GetsRequestMachineryManagementExcelEnum(GetsRequestMachineryManagementExcelEnumRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<RequestMachineryManagementExcelEnum>());
        return new GetsRequestMachineryManagementExcelEnumResponse(response);
    }

    public async Task<Result<GetRequestMachineryPaymentTypeResponse?>> GetRequestMachineryPaymentType(GetRequestMachineryPaymentTypeRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<RequestMachineryPaymentType>());
        return new GetRequestMachineryPaymentTypeResponse(response);
    }

    public async Task<Result<AssignMachineryForRequestMachineryResponse?>> AssignMachineryForRequestMachinery(AssignMachineryForRequestMachineryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<AssignMachineryForRequestMachineryValidator, AssignMachineryForRequestMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(isValidRequest.Error!);

        var requestMachineryQuery = await GetRequestMachineryAsync(request.RequestMachineryId, ct);
        if (requestMachineryQuery.IsFailure || requestMachineryQuery.Value is null)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(requestMachineryQuery.Error!);
        var requestMachinery = requestMachineryQuery.Value;

        var contractorMachineryQuery = await GetContractorMachineryAsync(request.ContractorMachineryId, ct);
        if (contractorMachineryQuery.IsFailure || contractorMachineryQuery.Value is null)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(contractorMachineryQuery.Error!);
        var contractorMachinery = contractorMachineryQuery.Value;

        var unit = GetUnit(contractorMachinery);

        var (confirmTime, totalHours) = CalculateConfirmTime(request, requestMachinery);
        if (confirmTime == null)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(RequestMachineryErrors.TimeSpantCountError);

        var totalPrice = requestMachinery.RequestCount * contractorMachinery.MachineryPrice * confirmTime;

        var currentUser = await _userProfileService.GetExtraInfo();

        var operatorUserQuery = await GetOperatorUserAsync(currentUser, requestMachinery, ct);
        if (operatorUserQuery.IsFailure || operatorUserQuery.Value is null)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(operatorUserQuery.Error!);
        var operatorUser = operatorUserQuery.Value;

        var inquiryOperatorResult = await CreateInquiryOperatorAsync(requestMachinery, operatorUser.OperatorAppoinmentId, operatorUser.OperatorAppoinmentUserId, ct);
        if (inquiryOperatorResult.IsFailure)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(inquiryOperatorResult.Error!);

        var inquiryResult = await CreateInquiryAsync(contractorMachinery, requestMachinery, operatorUser, confirmTime.Value, totalPrice.Value, request.Description ?? "", ct);
        if (inquiryResult.IsFailure)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(inquiryResult.Error!);

        var statusUpdateResult = await UpdateRequestMachineryStatusAsync(requestMachinery, ct);
        if (statusUpdateResult.IsFailure)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(statusUpdateResult.Error!);

        var assignmentResult = await CreateAssignmentAsync(contractorMachinery, requestMachinery, ct);
        if (assignmentResult.IsFailure)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(assignmentResult.Error!);

        var updateResponse = await _mediator.Send(new UpdateRequestMachineryConfirmDateCommand(
            requestMachinery, confirmTime, request.Description, request.ConfirmFromDate, request.ConfirmFromTime,
            request.ConfirmToDate, request.ConfirmToTime, contractorMachinery.ContractorId, contractorMachinery), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(updateResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        var userExtra = await _userProfileService.GetExtraInfo(ct);

        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
            await SendMessageRequestMachinery(requestMachinery, $"{userExtra.FirstName} {userExtra.LastName}", ct);

        return new AssignMachineryForRequestMachineryResponse(requestMachinery.Id);
    }

    #region Active or InActive RequestMachineryInquiryOperator
    public async Task<Result<InActiveRequestMachineryInquiryOperatorResponse?>> InActiveRequestMachineryInquiryOperatorAsync(InActiveRequestMachineryInquiryOperatorRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<InActiveRequestMachineryInquiryOperatorValidator, InActiveRequestMachineryInquiryOperatorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InActiveRequestMachineryInquiryOperatorResponse>(isValidRequest.Error!);

        var inActiveRequestMachineryInquiryOperatorCommand = await _mediator.Send(new InActiveRequestMachineryInquiryOperatorCommand(request.OperatorId,
                                                                                                                                     request.RequestMachineryId), ct);
        if (inActiveRequestMachineryInquiryOperatorCommand.IsFailure)
            return Result.Failure<InActiveRequestMachineryInquiryOperatorResponse>(inActiveRequestMachineryInquiryOperatorCommand.Error!);
        var requestMachineryOperator = inActiveRequestMachineryInquiryOperatorCommand.Value!;

        await _unitOfWork.CommitAsync(ct);

        return new InActiveRequestMachineryInquiryOperatorResponse(requestMachineryOperator.Id);
    }
    public async Task<Result<ActiveRequestMachineryInquiryOperatorResponse?>> ActiveRequestMachineryInquiryOperatorAsync(ActiveRequestMachineryInquiryOperatorRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<ActiveRequestMachineryInquiryOperatorValidator, ActiveRequestMachineryInquiryOperatorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveRequestMachineryInquiryOperatorResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveRequestMachineryInquiryOperatorResponse>(response.Error!);
        var value = response.Value!;

        var responses = await _mediator.Send(new ActiveRequestMachineryInquiryOperatorCommand(request.OperatorId, request.RequestMachineryId), ct);
        if (responses.IsFailure)
            return Result.Failure<ActiveRequestMachineryInquiryOperatorResponse>(responses.Error!);
        var values = responses.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new ActiveRequestMachineryInquiryOperatorResponse(values.Id);
    }
    #endregion
}

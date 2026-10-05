using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorServiceContractor;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetContractorServiceByDetailService;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByIdsIncludeless;
using Engineering.Application.Services.RequestContractorInquiries.Commands.SetContractorInquiryConfirmedUser;
using Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryById;
using Engineering.Application.Services.RequestContractors.Commands.ChangeRequestContractorStatus;
using Engineering.Application.Services.RequestContractors.Commands.CreateRequestContractor;
using Engineering.Application.Services.RequestContractors.Commands.DeleteRequestContractor;
using Engineering.Application.Services.RequestContractors.Commands.UpdateRequestContractor;
using Engineering.Application.Services.RequestContractors.Models.CreateRequestContractor;
using Engineering.Application.Services.RequestContractors.Models.DeleteRequestContractor;
using Engineering.Application.Services.RequestContractors.Models.GetFilteredRequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorById;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorHistories;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorStatus;
using Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelEnums;
using Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelExporter;
using Engineering.Application.Services.RequestContractors.Models.GroupRequestContractorStatusChanger;
using Engineering.Application.Services.RequestContractors.Models.RequestContractorGroupDelete;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorConfirmed;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorEndInquiry;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryConfirmed;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryRejected;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorPending;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorRejected;
using Engineering.Application.Services.RequestContractors.Models.UpdateRequestContractor;
using Engineering.Application.Services.RequestContractors.Queries.GetFilteredRequestContractors;
using Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorById;
using Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorModelById;
using Engineering.Application.Services.RequestContractors.Queries.IsRequestContractorDuplicate;
using Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractors;

public partial class RequestContractorLogic : IRequestContractorLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<RequestContractorLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;

    public RequestContractorLogic(
        IMediator mediator,
        ILogger<RequestContractorLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateRequestContractorResponse?>> CreateRequestContractor(CreateRequestContractorRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateRequestContractorValidator, CreateRequestContractorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateRequestContractorResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateRequestContractorResponse>(companyResponse.Error!);
        }

        var detailIds = request.ProjectOperationDetails.Select(s => s.ProjectOperationDetailId).Distinct().ToList();
        var getProjectOperationDetailsQuery = await _mediator.Send(new GetsProjectOperationDetailByIdsIncludelessQuery(detailIds), ct);
        if (getProjectOperationDetailsQuery.IsFailure)
            return Result.Failure<CreateRequestContractorResponse>(getProjectOperationDetailsQuery.Error!);
        var projectOperationDetails = getProjectOperationDetailsQuery.Value!.Data;

        var serviceInfoIds = request.ProjectOperationDetails.SelectMany(x => x.ServiceInfos).Select(s => s.ServiceInfoId).Distinct().ToList();
        var getServiceInfosQuery = await _mediator.Send(new GetsServiceInfoByIdsQuery(1, serviceInfoIds.Count, serviceInfoIds), ct);
        if (getServiceInfosQuery.IsFailure)
            return Result.Failure<CreateRequestContractorResponse>(getServiceInfosQuery.Error!);
        var serviceInfos = getServiceInfosQuery.Value!.Data;

        foreach (var detail in request.ProjectOperationDetails)
        {
            var projectOperationDetail = projectOperationDetails!.FirstOrDefault(x => x.Id == detail.ProjectOperationDetailId);

            var serviceInfoCommands = detail.ServiceInfos.ToList();
            foreach (var service in serviceInfoCommands)
            {
                var serviceInfo = serviceInfos!.FirstOrDefault(x => x.Id == service.ServiceInfoId);

                if (projectOperationDetail is null || serviceInfo is null)
                    return Result.Failure<CreateRequestContractorResponse>(RequestContractorErrors.UnvalidData);

                var isDuplicate = await _mediator.Send(new IsRequestContractorDuplicateQuery(projectOperationDetail!.Id, serviceInfo!.Id), ct);
                if (isDuplicate.Value == true)
                    return Result.Failure<CreateRequestContractorResponse>
                        (RequestContractorErrors.IsDuplicate(projectOperationDetail.OperationLocation.PublicName, serviceInfo.ServiceInfoName));

                var createResponse = await _mediator.Send(new CreateRequestContractorCommand(
                    projectOperationDetail!,
                    serviceInfo!,
                    service.Volume,
                    service.Description,
                    companyId), ct);
                if (createResponse.IsFailure)
                    return Result.Failure<CreateRequestContractorResponse>(createResponse.Error!);
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateRequestContractorResponse(true);
    }

    public async Task<Result<UpdateRequestContractorResponse?>> UpdateRequestContractor(UpdateRequestContractorRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestContractorValidator, UpdateRequestContractorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestContractorResponse>(isValidRequest.Error!);

        var getRequestContractor = await _mediator.Send(new GetRequestContractorByIdQuery(request.Id), ct);
        if (getRequestContractor.IsFailure)
            return Result.Failure<UpdateRequestContractorResponse>(getRequestContractor.Error!);
        var requestContractor = getRequestContractor.Value!;

        if (!(ValidateRequestContractorStatus.AllowStatusForUpdate.Any(z => z == requestContractor.Status)))
            return Result.Failure<UpdateRequestContractorResponse>(RequestContractorErrors.UnvalidStatus);

        var updateResponse = await _mediator.Send(new UpdateRequestContractorCommand(
            requestContractor,
            request.Volume,
            request.Description), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<UpdateRequestContractorResponse>(updateResponse.Error!);
        var response = updateResponse.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestContractorResponse(response.Id);
    }

    public async Task<Result<GroupRequestContractorStatusChangerResponse?>> GroupRequestContractorStatusChanger(GroupRequestContractorStatusChangerRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GroupRequestContractorStatusChangerValidator, GroupRequestContractorStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GroupRequestContractorStatusChangerResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var getRequestContractor = await _mediator.Send(new GetRequestContractorByIdQuery(item), ct);
            if (getRequestContractor.IsFailure)
                return Result.Failure<GroupRequestContractorStatusChangerResponse>(getRequestContractor.Error!);
            var requestContractor = getRequestContractor.Value!;

            if (!(requestContractor.Status == RequestContractorStatus.New))
                return Result.Failure<GroupRequestContractorStatusChangerResponse>(RequestContractorErrors.UnvalidStatus);

            var statusChangeResponse = await _mediator.Send(new ChangeRequestContractorStatusCommand(
                requestContractor,
                request.Status,
                request.Description), ct);
            if (statusChangeResponse.IsFailure)
                return Result.Failure<GroupRequestContractorStatusChangerResponse>(statusChangeResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new GroupRequestContractorStatusChangerResponse(true);
    }

    public async Task<Result<DeleteRequestContractorResponse?>> DeleteRequestContractor(DeleteRequestContractorRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteRequestContractorValidator, DeleteRequestContractorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteRequestContractorResponse>(isValidRequest.Error!);

        var getRequestContractor = await _mediator.Send(new GetRequestContractorByIdQuery(request.Id), ct);
        if (getRequestContractor.IsFailure)
            return Result.Failure<DeleteRequestContractorResponse>(getRequestContractor.Error!);
        var requestContractor = getRequestContractor.Value!;

        if (!(ValidateRequestContractorStatus.AllowStatusForDelete.Any(x => x == requestContractor.Status)))
            return Result.Failure<DeleteRequestContractorResponse>(RequestContractorErrors.UnvalidStatus);

        var deleteResponse = await _mediator.Send(new DeleteRequestContractorCommand(requestContractor), ct);
        if (deleteResponse.IsFailure)
            return Result.Failure<DeleteRequestContractorResponse>(deleteResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteRequestContractorResponse(requestContractor.Id);
    }

    public async Task<Result<RequestContractorGroupDeleteResponse?>> RequestContractorGroupDelete(RequestContractorGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for RequestContractorGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<RequestContractorGroupDeleteValidator, RequestContractorGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<RequestContractorGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var getRequestContractor = await _mediator.Send(new GetRequestContractorByIdQuery(item), ct);
            if (getRequestContractor.IsFailure)
                return Result.Failure<RequestContractorGroupDeleteResponse>(getRequestContractor.Error!);
            var requestContractor = getRequestContractor.Value!;

            if (!(ValidateRequestContractorStatus.AllowStatusForDelete.Any(x => x == requestContractor.Status)))
                return Result.Failure<RequestContractorGroupDeleteResponse>(RequestContractorErrors.UnvalidStatus);

            var deleteResponse = await _mediator.Send(new DeleteRequestContractorCommand(requestContractor), ct);
            if (deleteResponse.IsFailure)
                return Result.Failure<RequestContractorGroupDeleteResponse>(deleteResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new RequestContractorGroupDeleteResponse(true);
    }

    public async Task<Result<SetRequestContractorConfirmedResponse?>> SetRequestContractorConfirmed(SetRequestContractorConfirmedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestContractorConfirmedValidator, SetRequestContractorConfirmedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestContractorConfirmedResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestContractorByIdQuery(request.RequestContractorId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestContractorConfirmedResponse>(response.Error!);
        var requestContractor = response.Value!;

        if (!(ValidateRequestContractorStatus.AllowStatusForInquiry.Any(x => x == requestContractor.Status)))
            return Result.Failure<SetRequestContractorConfirmedResponse>(RequestContractorErrors.UnvalidStatus);

        var updateRequestStatusResponse = await _mediator.Send(new ChangeRequestContractorStatusCommand(
            requestContractor,
            RequestContractorStatus.Inquiry,
            request.Description
            ), ct);
        if (updateRequestStatusResponse.IsFailure)
            return Result.Failure<SetRequestContractorConfirmedResponse>(updateRequestStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetRequestContractorConfirmedResponse(requestContractor.Id);
    }

    public async Task<Result<SetRequestContractorInquiryConfirmedResponse?>> SetRequestContractorInquiryConfirmed(SetRequestContractorInquiryConfirmedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestContractorInquiryConfirmedValidator, SetRequestContractorInquiryConfirmedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestContractorInquiryConfirmedResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestContractorByIdQuery(
            request.RequestContractorId
            ), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestContractorInquiryConfirmedResponse>(response.Error!);
        var requestContractor = response.Value!;

        var inquiryQuery = await _mediator.Send(new GetRequestContractorInquiryByIdQuery(
            request.RequestContractorInquiryId
            ), ct);
        if (inquiryQuery.IsFailure)
            return Result.Failure<SetRequestContractorInquiryConfirmedResponse>(inquiryQuery.Error!);
        var inquiry = inquiryQuery.Value!;

        if (!(ValidateRequestContractorStatus.AllowStatusForInquiryConfirmed.Any(x => x == requestContractor.Status)))
            return Result.Failure<SetRequestContractorInquiryConfirmedResponse>(RequestContractorErrors.UnvalidStatus);

        var currentUser = _userProfileService.GetProfileInfo();
        var confirmedInquiry = await _mediator.Send(new SetContractorInquiryConfirmedUserCommand(
            inquiry,
            currentUser.UserId), ct);
        if (confirmedInquiry.IsFailure)
            return Result.Failure<SetRequestContractorInquiryConfirmedResponse>(confirmedInquiry.Error!);

        var updateResponse = await _mediator.Send(new ChangeRequestContractorStatusCommand(
            requestContractor,
            RequestContractorStatus.Confirmed,
            request.Description), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<SetRequestContractorInquiryConfirmedResponse>(updateResponse.Error!);

        var contractorServiceQuery = await _mediator.Send(new GetContractorServiceByDetailServiceQuery(
            requestContractor.ProjectOperationDetail.Id,
            requestContractor.ServiceInfo.Id), ct);
        if (contractorServiceQuery.IsFailure)
            return Result.Failure<SetRequestContractorInquiryConfirmedResponse>(contractorServiceQuery.Error!);
        var contractorService = contractorServiceQuery.Value!;

        var updateContractor = await _mediator.Send(new UpdateContractorServiceContractorCommand(
            contractorService.Id,
            inquiry.ContractorId
            ), ct);
        if (updateContractor.IsFailure)
            return Result.Failure<SetRequestContractorInquiryConfirmedResponse>(updateContractor.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetRequestContractorInquiryConfirmedResponse(requestContractor.Id);
    }

    public async Task<Result<SetRequestContractorPendingResponse?>> SetRequestContractorPending(SetRequestContractorPendingRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestContractorPendingValidator, SetRequestContractorPendingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestContractorPendingResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestContractorByIdQuery(request.RequestContractorId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestContractorPendingResponse>(response.Error!);
        var requestContractor = response.Value!;

        if (!(ValidateRequestContractorStatus.AllowStatusForPending.Any(x => x == requestContractor.Status)))
            return Result.Failure<SetRequestContractorPendingResponse>(RequestContractorErrors.UnvalidStatus);

        var updateResponse = await _mediator.Send(new ChangeRequestContractorStatusCommand(
            requestContractor,
            RequestContractorStatus.Pending,
            null), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<SetRequestContractorPendingResponse>(updateResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetRequestContractorPendingResponse(requestContractor.Id);
    }

    public async Task<Result<SetRequestContractorInquiryRejectedResponse?>> SetRequestContractorInquiryRejected(SetRequestContractorInquiryRejectedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestContractorInquiryRejectedValidator, SetRequestContractorInquiryRejectedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestContractorInquiryRejectedResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestContractorByIdQuery(request.RequestContractorId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestContractorInquiryRejectedResponse>(response.Error!);
        var requestContractor = response.Value!;

        if (!(ValidateRequestContractorStatus.AllowStatusForInquiryRejected.Any(x => x == requestContractor.Status)))
            return Result.Failure<SetRequestContractorInquiryRejectedResponse>(RequestContractorErrors.UnvalidStatus);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestContractorStatusCommand(
            requestContractor,
            RequestContractorStatus.InquiryRejected,
            request.Description
            ), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestContractorInquiryRejectedResponse>(updateStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestContractorInquiryRejectedResponse(requestContractor.Id);
    }

    public async Task<Result<SetRequestContractorRejectedResponse?>> SetRequestContractorRejected(SetRequestContractorRejectedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestContractorRejectedValidator, SetRequestContractorRejectedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestContractorRejectedResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestContractorByIdQuery(request.RequestContractorId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestContractorRejectedResponse>(response.Error!);
        var requestContractor = response.Value!;

        if (!(ValidateRequestContractorStatus.AllowStatusForRequestRejected.Any(x => x == requestContractor.Status)))
            return Result.Failure<SetRequestContractorRejectedResponse>(RequestContractorErrors.UnvalidStatus);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestContractorStatusCommand(
            requestContractor,
            RequestContractorStatus.Rejected,
            request.Description
            ), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestContractorRejectedResponse>(updateStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestContractorRejectedResponse(requestContractor.Id);
    }

    public async Task<Result<SetRequestContractorEndInquiryResponse?>> SetRequestContractorEndInquiry(SetRequestContractorEndInquiryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetRequestContractorEndInquiryValidator, SetRequestContractorEndInquiryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetRequestContractorEndInquiryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestContractorByIdQuery(request.RequestContractorId), ct);
        if (response.IsFailure)
            return Result.Failure<SetRequestContractorEndInquiryResponse>(response.Error!);
        var requestContractor = response.Value!;

        if (!(ValidateRequestContractorStatus.AllowStatusForEndInquiry.Any(x => x == requestContractor.Status)))
            return Result.Failure<SetRequestContractorEndInquiryResponse>(RequestContractorErrors.UnvalidStatus);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestContractorStatusCommand(
            requestContractor,
            RequestContractorStatus.EndInquiry,
            request.Description
            ), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetRequestContractorEndInquiryResponse>(updateStatusResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetRequestContractorEndInquiryResponse(requestContractor.Id);
    }

    public async Task<Result<GetFilteredRequestContractorsResponse?>> GetFilteredRequestContractors(GetFilteredRequestContractorsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredRequestContractorsValidator, GetFilteredRequestContractorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredRequestContractorsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var getFiltered = await _mediator.Send(new GetFilteredRequestContractorsQuery(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ServiceInfoIds,
                request.ContractorIds,
                request.Status,
                request.FromDate,
                request.ToDate,
                request.CreatorId,
                request.FilterData,
                request.OrderBy,
                companyId,
                request.PageIndex,
                request.PageSize
                ), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetFilteredRequestContractorsResponse>(getFiltered.Error!);
        var requestContractors = getFiltered.Value!.Data!;

        List<long>? allIds = [];
        allIds.AddRange(requestContractors.Where(x => x.CreatorId != null && x.CreatorId > 0).Select(x => (long)x.CreatorId!).Distinct().ToList() ?? []);
        allIds.AddRange(requestContractors.Where(x => x.ConfirmUser != null && x.ConfirmUser > 0).Select(x => (long)x.ConfirmUser!).Distinct().ToList() ?? []);
        var users = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        var companyIds = requestContractors.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? thirdpartyIds = [];
        thirdpartyIds.AddRange(requestContractors.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdpartyIds, null, null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(requestContractors.Where(x => x.CurrencyId != null && x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList() ?? []);
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        requestContractors.ForEach(item =>
        {
            item.Creator = users?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.ConfirmUserName = users?.FirstOrDefault(x => x.UserId == item.ConfirmUser)?.FullName;
            item.ContractorName = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.FullName;
            item.ContractorNickName = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.Nickname;
            item.CompanyNameFa = companies?.FirstOrDefault(x => x.Id == item.CompanyId)?.NameFa;
        });

        return new GetFilteredRequestContractorsResponse(requestContractors ?? new List<GetFilteredRequestContractorsModel>(0), getFiltered.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetRequestContractorByIdResponse?>> GetRequestContractorById(GetRequestContractorByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestContractorByIdValidator, GetRequestContractorByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestContractorByIdResponse>(isValidRequest.Error!);

        var getRequestContractor = await _mediator.Send(new GetRequestContractorModelByIdQuery(request.RequestContractorId), ct);
        if (getRequestContractor.IsFailure)
            return Result.Failure<GetRequestContractorByIdResponse>(getRequestContractor.Error!);
        var requestContractor = getRequestContractor.Value!;

        List<long>? allIds = [];
        allIds.Add(requestContractor.CreatorId ?? 0);
        allIds.Add(requestContractor.ConfirmUser ?? 0);
        var users = await WebServicesLogic.UserDataReceiver(allIds.Where(x => x > 0).Distinct().ToList(), null, _mediator, ct);

        var company = await WebServicesLogic.CompanyDataReceiver(requestContractor.CompanyId, _mediator, ct);

        List<long>? thirdpartyIds = [];
        thirdpartyIds.Add(requestContractor.ContractorId ?? 0);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdpartyIds.Where(x => x > 0).ToList(), null, null, _mediator, ct);

        var currency = await WebServicesLogic.CurrencyDataReceiver(requestContractor.CurrencyId, _mediator, ct);

        requestContractor.Creator = users?.FirstOrDefault(x => x.UserId == requestContractor.CreatorId)?.FullName;
        requestContractor.ConfirmUserName = users?.FirstOrDefault(x => x.UserId == requestContractor.ConfirmUser)?.FullName;
        requestContractor.ContractorName = contractors?.FirstOrDefault(x => x is not null && x.Id == requestContractor.ContractorId)?.FullName;
        requestContractor.ContractorNickName = contractors?.FirstOrDefault(x => x is not null && x.Id == requestContractor.ContractorId)?.Nickname;
        requestContractor.CompanyNameFa = company?.NameFa;

        return requestContractor;
    }

    public async Task<Result<GetRequestContractorHistoriesResponse?>> GetRequestContractorHistories(GetRequestContractorHistoriesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestContractorHistoriesValidator, GetRequestContractorHistoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestContractorHistoriesResponse>(isValidRequest.Error!);

        var getRequestContractor = await _mediator.Send(new GetRequestContractorByIdQuery(request.RequestContractorId), ct);
        if (getRequestContractor.IsFailure)
            return Result.Failure<GetRequestContractorHistoriesResponse>(getRequestContractor.Error!);
        var requestContractor = getRequestContractor.Value!;

        var histories = requestContractor.Histories.ToList();

        List<long>? allIds = [];
        allIds.AddRange(histories.Where(x => x.CreatorId > 0).Select(x => x.CreatorId).Distinct().ToList() ?? []);
        var users = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        var models = new List<GetRequestContractorHistoriesModel>();
        foreach (var history in requestContractor.Histories)
        {
            models.Add(new GetRequestContractorHistoriesModel()
            {
                Created = history.Created,
                Description = history.Description,
                Status = history.Status,
                Creator = users?.Where(x => x.UserId == requestContractor.CreatorId).FirstOrDefault()?.FullName,
                CreatorId = history?.CreatorId,
                DescriptionStatus = history?.StatusDescription,
                Id = history?.Id,
                ProjectOperationDetailId = requestContractor.ProjectOperationDetail.Id,
                PublicCode = requestContractor.ProjectOperationDetail.OperationLocation.PublicCode,
                PublicName = requestContractor.ProjectOperationDetail.OperationLocation.PublicName,
                ServiceInfoId = requestContractor.ServiceInfo.Id,
                ServiceInfoCode = requestContractor.ServiceInfo.ServiceInfoCode,
                ServiceInfoName = requestContractor.ServiceInfo.ServiceInfoName,
                Volume = requestContractor.Volume
            });
        }

        return new GetRequestContractorHistoriesResponse()
        {
            Data = models.OrderByDescending(x => x.Created).ToList(),
            RowCount = requestContractor.Histories.Count,
            RequestNumber = requestContractor.RequestNumber,
            ProjectOperationDetailId = requestContractor.ProjectOperationDetail.Id,
            PublicCode = requestContractor.ProjectOperationDetail.OperationLocation.PublicCode,
            PublicName = requestContractor.ProjectOperationDetail.OperationLocation.PublicName,
            ServiceInfoId = requestContractor.ServiceInfo.Id,
            ServiceInfoCode = requestContractor.ServiceInfo.ServiceInfoCode,
            ServiceInfoName = requestContractor.ServiceInfo.ServiceInfoName,
        };
    }

    public async Task<Result<GetsRequestContractorExcelExporterResponse?>> GetsRequestContractorExcelExporter(GetsRequestContractorExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsRequestContractorExcelExporter");

        var isValidRequest = await request.IsValidAsync<GetsRequestContractorExcelExporterValidator, GetsRequestContractorExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsRequestContractorExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var getFiltered = await _mediator.Send(new GetFilteredRequestContractorsQuery(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ServiceInfoIds,
                request.ContractorIds,
                request.Status,
                request.FromDate,
                request.ToDate,
                request.CreatorId,
                request.FilterData,
                request.OrderBy,
                companyId,
                request.PageIndex,
                request.PageSize
                ), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetsRequestContractorExcelExporterResponse>(getFiltered.Error!);
        var requestContractors = getFiltered.Value!.Data!;

        List<long>? allIds = [];
        allIds.AddRange(requestContractors.Where(x => x.CreatorId != null && x.CreatorId > 0).Select(x => (long)x.CreatorId!).Distinct().ToList() ?? []);
        allIds.AddRange(requestContractors.Where(x => x.ConfirmUser != null && x.ConfirmUser > 0).Select(x => (long)x.ConfirmUser!).Distinct().ToList() ?? []);
        var users = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        var companyIds = requestContractors.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? thirdpartyIds = [];
        thirdpartyIds.AddRange(requestContractors.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdpartyIds, null, null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(requestContractors.Where(x => x.CurrencyId != null && x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList() ?? []);
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        requestContractors.ForEach(item =>
        {
            item.Creator = users?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.ConfirmUserName = users?.FirstOrDefault(x => x.UserId == item.ConfirmUser)?.FullName;
            item.ContractorName = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.FullName;
            item.ContractorNickName = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.Nickname;
            item.CompanyNameFa = companies?.FirstOrDefault(x => x.Id == item.CompanyId)?.NameFa;
        });

        //var data = requestContractors.Adapt<List<GetsRequestContractorExcelExporterResponseModel>>();

        var file = new FileContentResult(RequestContractorExcels.RequestContractorToExcel(requestContractors, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"RequestContractors-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsRequestContractorExcelExporterResponse(file);
    }

    public async Task<Result<GetsRequestContractorExcelEnumsResponse?>> GetsRequestContractorExcelEnums(GetsRequestContractorExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GetsRequestContractorExcelEnum>());
        return new GetsRequestContractorExcelEnumsResponse(response);
    }

    public async Task<Result<GetRequestContractorStatusResponse?>> GetRequestContractorStatus(GetRequestContractorStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<RequestContractorStatus>());
        return new GetRequestContractorStatusResponse(response);
    }

}

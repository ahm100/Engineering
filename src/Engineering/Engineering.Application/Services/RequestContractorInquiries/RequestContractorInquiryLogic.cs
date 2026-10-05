using Engineering.Application.Services.RequestContractorInquiries.Commands.CreateRequestContractorInquiry;
using Engineering.Application.Services.RequestContractorInquiries.Commands.CreateRequestContractorInquiryDocument;
using Engineering.Application.Services.RequestContractorInquiries.Commands.DeleteRequestContractorInquiry;
using Engineering.Application.Services.RequestContractorInquiries.Commands.DeleteRequestContractorInquiryDocument;
using Engineering.Application.Services.RequestContractorInquiries.Commands.UpdateRequestContractorInquiry;
using Engineering.Application.Services.RequestContractorInquiries.Models.CreateRequestContractorInquiry;
using Engineering.Application.Services.RequestContractorInquiries.Models.GetRequestContractorInquiryByRequestId;
using Engineering.Application.Services.RequestContractorInquiries.Models.UpdateRequestContractorInquiry;
using Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryById;
using Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryByRequestId;
using Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryModelById;
using Engineering.Application.Services.RequestContractors.Models.DeleteRequestContractorInquiry;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorInquiryById;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorType;
using Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetsCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractorInquiries;

public class RequestContractorInquiryLogic : IRequestContractorInquiryLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<RequestContractorInquiryLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public RequestContractorInquiryLogic(
        IMediator mediator,
        ILogger<RequestContractorInquiryLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateRequestContractorInquiryResponse?>> CreateRequestContractorInquiry(CreateRequestContractorInquiryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateRequestContractorInquiryValidator, CreateRequestContractorInquiryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateRequestContractorInquiryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestContractorByIdQuery(request.RequestContractorId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateRequestContractorInquiryResponse>(response.Error!);
        var requestContractor = response.Value!;

        if (!(ValidateRequestContractorStatus.AllowStatusForCreateInquiry.Any(x => x == requestContractor.Status)))
            return Result.Failure<CreateRequestContractorInquiryResponse>(RequestContractorErrors.UnvalidStatus);
        var inquiries = request.Inquiries.ToList();

        var inquiryContractorIds = inquiries.Select(x => x.ContractorId).Distinct().ToList();
        var thirdPartiesQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, inquiryContractorIds.Count, inquiryContractorIds, null, false, null), ct);
        if (thirdPartiesQuery.IsFailure || thirdPartiesQuery.Value is null || thirdPartiesQuery.Value.Data is null)
            return Result.Failure<CreateRequestContractorInquiryResponse>(thirdPartiesQuery.Error!);
        var thirdParties = thirdPartiesQuery.Value!.Data;

        var missingContractorIds = inquiryContractorIds.Except(thirdParties.Where(x => x is not null).Select(x => x!.Id)).ToList();
        if (missingContractorIds.Any())
            return Result.Failure<CreateRequestContractorInquiryResponse>(RequestContractorErrors.ContractorsNotFound);

        var inquiryCurrencyIds = inquiries.Select(x => x.CurrencyId).Distinct().ToList();
        var currenciesQuery = await _mediator.Send(new GetsCurrencyByIdQuery(1, inquiryCurrencyIds.Count, inquiryCurrencyIds, false), ct);
        if (currenciesQuery.IsFailure || currenciesQuery.Value is null || currenciesQuery.Value.Data is null)
            return Result.Failure<CreateRequestContractorInquiryResponse>(currenciesQuery.Error!);
        var currencies = currenciesQuery.Value!.Data;

        var missingCurrencyIds = inquiryCurrencyIds.Except(currencies.Select(x => x.Id)).ToList();
        if (missingCurrencyIds.Any())
            return Result.Failure<CreateRequestContractorInquiryResponse>(RequestContractorErrors.CurrenciesNotFound);

        foreach (var inquiry in inquiries)
        {
            var totalAmount = (inquiry.Amount - (inquiry.Discount ?? 0)) + (inquiry.Tax ?? 0);

            var createInquiryResponse = await _mediator.Send(new CreateRequestContractorInquiryCommand(
                inquiry.ContractorId,
                inquiry.Type,
                inquiry.CurrencyId,
                totalAmount,
                inquiry.Amount,
                inquiry.Discount,
                inquiry.Tax,
                inquiry.FromDate,
                inquiry.ToDate,
                inquiry.Description,
                requestContractor), ct);
            if (createInquiryResponse.IsFailure)
                return Result.Failure<CreateRequestContractorInquiryResponse>(createInquiryResponse.Error!);
            var requestContractorInquiry = createInquiryResponse.Value!;

            if (inquiry.Documents is not null && inquiry.Documents.Count > 0)
                foreach (var document in inquiry.Documents)
                {
                    var createDocumentResponse = await _mediator.Send(new CreateRequestContractorInquiryDocumentCommand(document, requestContractorInquiry), ct);
                    if (createDocumentResponse.IsFailure)
                        return Result.Failure<CreateRequestContractorInquiryResponse>(createDocumentResponse.Error!);
                }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateRequestContractorInquiryResponse(requestContractor.Id, true);
    }

    public async Task<Result<UpdateRequestContractorInquiryResponse?>> UpdateRequestContractorInquiry(UpdateRequestContractorInquiryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestContractorInquiryValidator, UpdateRequestContractorInquiryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestContractorInquiryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestContractorInquiryByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateRequestContractorInquiryResponse>(response.Error!);
        var requestContractorInquiry = response.Value!;

        var thirdPartiesQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long> { request.ContractorId }, null, false, null), ct);
        if (thirdPartiesQuery.IsFailure || thirdPartiesQuery.Value is null || thirdPartiesQuery.Value.Data is null)
            return Result.Failure<UpdateRequestContractorInquiryResponse>(thirdPartiesQuery.Error!);
        var thirdParties = thirdPartiesQuery.Value!.Data;

        var currenciesQuery = await _mediator.Send(new GetsCurrencyByIdQuery(1, 1, new List<long> { request.CurrencyId }, false), ct);
        if (currenciesQuery.IsFailure || currenciesQuery.Value is null || currenciesQuery.Value.Data is null)
            return Result.Failure<UpdateRequestContractorInquiryResponse>(currenciesQuery.Error!);
        var currencies = currenciesQuery.Value!.Data;

        var totalAmount = (request.Amount - (request.Discount ?? 0)) + (request.Tax ?? 0);

        var updateInquiryResponse = await _mediator.Send(new UpdateRequestContractorInquiryCommand(
            requestContractorInquiry,
            request.ContractorId,
            request.Type,
            request.CurrencyId,
            totalAmount,
            request.Amount,
            request.Discount,
            request.Tax,
            request.FromDate,
            request.ToDate,
            request.Description), ct);
        if (updateInquiryResponse.IsFailure)
            return Result.Failure<UpdateRequestContractorInquiryResponse>(updateInquiryResponse.Error!);
        var inqury = updateInquiryResponse.Value!;

        if (inqury.RequestContractorInquiryDocuments is not null && inqury.RequestContractorInquiryDocuments.Count > 0)
            foreach (var item in inqury.RequestContractorInquiryDocuments)
            {
                var deleteDocumentResponse = await _mediator.Send(new DeleteRequestContractorInquiryDocumentCommand(item.Id), ct);
                if (deleteDocumentResponse.IsFailure)
                    return Result.Failure<UpdateRequestContractorInquiryResponse>(deleteDocumentResponse.Error!);
            }

        if (request.Documents is not null && request.Documents.Count > 0)
            foreach (var document in request.Documents)
            {
                var createDocumentResponse = await _mediator.Send(new CreateRequestContractorInquiryDocumentCommand(document, requestContractorInquiry), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<UpdateRequestContractorInquiryResponse>(createDocumentResponse.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestContractorInquiryResponse(inqury.Id, true);
    }

    public async Task<Result<DeleteRequestContractorInquiryResponse?>> DeleteRequestContractorInquiry(DeleteRequestContractorInquiryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteRequestContractorInquiryValidator, DeleteRequestContractorInquiryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteRequestContractorInquiryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestContractorInquiryByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteRequestContractorInquiryResponse>(response.Error!);
        var requestContractorInquiry = response.Value!;

        var deleteInquiryResponse = await _mediator.Send(new DeleteRequestContractorInquiryCommand(
            requestContractorInquiry
            ), ct);
        if (deleteInquiryResponse.IsFailure)
            return Result.Failure<DeleteRequestContractorInquiryResponse>(deleteInquiryResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteRequestContractorInquiryResponse(request.Id, true);
    }

    public async Task<Result<GetRequestContractorInquiryByIdResponse?>> GetRequestContractorInquiryById(GetRequestContractorInquiryByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestContractorInquiryByIdValidator, GetRequestContractorInquiryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestContractorInquiryByIdResponse>(isValidRequest.Error!);

        var getRequestContractorInquiry = await _mediator.Send(new GetRequestContractorInquiryModelByIdQuery(request.RequestContractorInquiryId), ct);
        if (getRequestContractorInquiry.IsFailure)
            return Result.Failure<GetRequestContractorInquiryByIdResponse>(getRequestContractorInquiry.Error!);
        var requestContractorInquiry = getRequestContractorInquiry.Value!;

        var currency = await WebServicesLogic.CurrencyDataReceiver(requestContractorInquiry.CurrencyId, _mediator, ct);
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(new List<long> { (long)requestContractorInquiry.ContractorId! }, null, null, _mediator, ct);

        List<long>? ids = [];
        ids.Add(requestContractorInquiry.CreatorId ?? 0);
        ids.Add(requestContractorInquiry.InquiryCreatorId ?? 0);
        ids.Add(requestContractorInquiry.ConfirmUser ?? 0);
        var users = await WebServicesLogic.UserDataReceiver(ids.Where(x => x > 0).Distinct().ToList(), null, _mediator, ct);

        requestContractorInquiry.ConfirmUserName = users?.FirstOrDefault(x => x.UserId == requestContractorInquiry.ConfirmUser)?.FullName;
        requestContractorInquiry.Creator = users?.FirstOrDefault(x => x.UserId == requestContractorInquiry.CreatorId)?.FullName;
        requestContractorInquiry.InquiryCreator = users?.FirstOrDefault(x => x.UserId == requestContractorInquiry.InquiryCreatorId)?.FullName;
        requestContractorInquiry.CurrencyName = currency?.Name;
        requestContractorInquiry.ContractorName = thirdParties?.Where(x => x is not null).FirstOrDefault(x => x!.Id == requestContractorInquiry.ContractorId)?.FullName;
        requestContractorInquiry.ContractorNickName = thirdParties?.Where(x => x is not null).FirstOrDefault(x => x!.Id == requestContractorInquiry.ContractorId)?.Nickname;

        return requestContractorInquiry;
    }

    public async Task<Result<GetRequestContractorInquiryByRequestIdResponse?>> GetRequestContractorInquiryByRequestId(GetRequestContractorInquiryByRequestIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestContractorInquiryByRequestIdValidator, GetRequestContractorInquiryByRequestIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestContractorInquiryByRequestIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetRequestContractorInquiryByRequestIdQuery(
            request.RequestContractorId,
            companyId,
            request.PageIndex,
            request.PageSize
            ), ct);
        if (response.IsFailure)
            return Result.Failure<GetRequestContractorInquiryByRequestIdResponse>(response.Error!);
        var requestContractorInquiries = response.Value!.Data!;

        List<long>? currencyIds = [];
        currencyIds.AddRange(requestContractorInquiries.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList() ?? []);
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(requestContractorInquiries.Where(x => x.ContractorId is not null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        List<long>? ids = [];
        ids.AddRange(requestContractorInquiries.Where(x => x.CreatorId is not null && x.CreatorId > 0).Select(x => (long)x.CreatorId!).Distinct().ToList() ?? []);
        ids.AddRange(requestContractorInquiries.Where(x => x.InquiryCreatorId is not null && x.InquiryCreatorId > 0).Select(x => (long)x.InquiryCreatorId!).Distinct().ToList() ?? []);
        ids.AddRange(requestContractorInquiries.Where(x => x.ConfirmUser is not null && x.ConfirmUser > 0).Select(x => (long)x.ConfirmUser!).Distinct().ToList() ?? []);
        var users = await WebServicesLogic.UserDataReceiver(ids.Distinct().ToList(), null, _mediator, ct);

        requestContractorInquiries.ForEach(item =>
        {
            item.ConfirmUserName = users?.FirstOrDefault(x => x.UserId == item.ConfirmUser)?.FullName;
            item.Creator = users?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.InquiryCreator = users?.FirstOrDefault(x => x.UserId == item.InquiryCreatorId)?.FullName;
            item.CurrencyName = currencies?.FirstOrDefault(x => x!.Id == item.CurrencyId)?.Name;
            item.ContractorName = thirdParties?.Where(x => x is not null).FirstOrDefault(x => x!.Id == item.ContractorId)?.FullName;
            item.ContractorNickName = thirdParties?.Where(x => x is not null).FirstOrDefault(x => x!.Id == item.ContractorId)?.Nickname;
        });

        return new GetRequestContractorInquiryByRequestIdResponse(requestContractorInquiries ?? new List<GetRequestContractorInquiryByRequestIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetRequestContractorTypeResponse?>> GetRequestContractorType(GetRequestContractorTypeRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<RequestContractorType>());
        return new GetRequestContractorTypeResponse(response);
    }
}

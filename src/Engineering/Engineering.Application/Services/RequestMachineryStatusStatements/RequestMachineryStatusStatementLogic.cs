using Engineering.Application.Services.RequestMachineries.Queries.GetManagerConfirmMachineryReports;
using Engineering.Application.Services.RequestMachineryStatusStatements.Commands.CreateRequestMachineryStatusStatement;
using Engineering.Application.Services.RequestMachineryStatusStatements.Commands.CreateRequestMachineryStatusStatementDetail;
using Engineering.Application.Services.RequestMachineryStatusStatements.Commands.RequestMachineryStatusStatementStatusChanger;
using Engineering.Application.Services.RequestMachineryStatusStatements.Commands.UpdateRequestMachineryStatusStatementPaymentOrder;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.CreateRequestMachineryStatusStatement;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetFilteredRequestMachineryStatusStatement;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementById;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementStatus;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementUnit;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetsRequestMachineryStatusStatementExcelEnum;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetsRequestMachineryStatusStatementExcelExporter;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.RequestMachineryStatusStatementStatusChanger;
using Engineering.Application.Services.RequestMachineryStatusStatements.Queries.GetFilteredRequestMachineryStatusStatement;
using Engineering.Application.Services.RequestMachineryStatusStatements.Queries.GetRequestMachineryStatusStatementById;
using Engineering.Application.Services.Seasons.Queries.GetSeasonById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateRequestMachineryPaymentOrder;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;
using Gita.Backend.Shared.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetThirdPartyById;

namespace Engineering.Application.Services.RequestMachineryStatusStatements;

public partial class RequestMachineryStatusStatementLogic : IRequestMachineryStatusStatementLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<RequestMachineryStatusStatementLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public RequestMachineryStatusStatementLogic(IMediator mediator, ILogger<RequestMachineryStatusStatementLogic> logger, IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateRequestMachineryStatusStatementResponse?>> CreateRequestMachineryStatusStatement(CreateRequestMachineryStatusStatementRequest request, CT ct)
    {
        var transactionOptions = new System.Transactions.TransactionOptions();
        transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
        {
            var isValidRequest = await request.IsValidAsync<CreateRequestMachineryStatusStatementValidator, CreateRequestMachineryStatusStatementRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(isValidRequest.Error!);

            long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
            if (companyId >= 1)
            {
                var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
                if (companyResponse.IsFailure)
                    return Result.Failure<CreateRequestMachineryStatusStatementResponse>(companyResponse.Error!);
            }

            var getFiltered = await _mediator.Send(new GetManagerConfirmMachineryReportsQuery(
                    request.Ids,
                    request.CostCenterIds,
                    request.ProjectIds,
                    request.ContractorId,
                    request.ProjectOperationIds,
                    request.ProjectOperationDetailIds,
                    request.MachineryIds,
                    request.Unit,
                    request.Statuses,
                    request.StartDate,
                    request.EndDate,
                    request.ConfirmedFromDate,
                    request.ConfirmedToDate,
                    request.FilterData,
                    false,
                    null,
                    companyId,
                    0,
                    0), ct);
            if (getFiltered.IsFailure)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(getFiltered.Error!);
            var requestMachineries = getFiltered.Value!.Data!;

            if (requestMachineries.Any(c => c.ContractorId == null || c.ContractorId <= 0))
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(CSSErrors.InValidRequestContractorId);

            foreach (var item in requestMachineries)
            {
                if (item.RequestMachineryStatusStatementDetails.Any(c =>
                        c.RequestMachineryStatusStatement.Status != RequestMachineryStatusStatementStatus.Invalidated &&
                        c.RequestMachineryStatusStatement.PaymentDate is not null))
                    return Result.Failure<CreateRequestMachineryStatusStatementResponse>(RequestMachineryStatusStatementErrors
                        .InValidRequestMachhineryDate(item.RequestNumber.ToString() ?? string.Empty));
            }

            if (request.PaymentDate is not null)
                if (request.PaymentDate!.Value.Date < DateTime.Now.Date)
                    return Result.Failure<CreateRequestMachineryStatusStatementResponse>(CSSErrors.InValidPaymentDate);

            if (request.SeasonId is null || request.SeasonId <= 0)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(CSSErrors.InValidSeasonId);

            var responseSeason = await _mediator.Send(new GetSeasonByIdQuery(request.SeasonId!.Value), ct);
            if (responseSeason.IsFailure)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(responseSeason.Error!);

            var seasonValue = responseSeason.Value!;
            var queryContractor = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.ContractorId], null, false, null), ct);
            if (queryContractor.IsFailure)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(RequestMachineryStatusStatementErrors.InValidContractorId);
            var contractor = queryContractor.Value?.Data!.FirstOrDefault();

            decimal? totalFinalPrice = 0;
            foreach (var item in requestMachineries)
            {
                var totalPrice = CalculateTotalPrice(item);
                totalFinalPrice += totalPrice;
            }

            var totalRequestedCount = requestMachineries.Sum(x => x.RequestCount);
            var fromDate = requestMachineries.Select(x => x.ConfirmFromDate).Min();
            var toDate = requestMachineries.Select(x => x.ConfirmToDate)?.Max();

            var response = await _mediator.Send(new CreateRequestMachineryStatusStatementCommand(request.ContractorId, fromDate, toDate,
                request.PaymentDate, totalRequestedCount, totalFinalPrice != null ? totalFinalPrice.Value : 0, request.ContractorPrice,
                request.BankAccountId, request.IBAN, request.Description, request.CostCategoryId, request.CostGroupId, request.DocumentTypeId,
                request.PreferentialTypeId, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(response.Error!);
            var statusStatement = response.Value;

            List<RequestMachineryStatusStatementDetail>? statusStatementDetails = [];
            if (requestMachineries is not null && requestMachineries.Count > 0)
                foreach (var item in requestMachineries)
                {
                    var confirmedInquiry = item.InquiryOperators.SelectMany(x => x.Inquiries).Where(x => x.IsConfirmed == true).FirstOrDefault();
                    var finalPrice = confirmedInquiry?.TotalPrice;
                    var currencyId = confirmedInquiry?.CurrencyId;
                    var operatorId = confirmedInquiry?.RequestMachineryInquiryOperator.OperatorAppoinmentId;
                    var projectOperationIds = item.ProjectOperations.Select(x => x.ProjectOperation.Id).ToList();
                    var projectOperationDetailIds = item.ProjectOperationDetails.Select(x => x.ProjectOperationDetail.Id).ToList();
                    var unit = GetUnit(item);

                    var responseDetail = await _mediator.Send(new CreateRequestMachineryStatusStatementDetailCommand(
                        statusStatement!, item.Project, item.Machinery, item!, request.ContractorId, item.ConfirmFromDate, item.ConfirmToDate,
                        item.RequestCount, finalPrice != null ? finalPrice.Value : 0, currencyId, unit, operatorId, item.ConfirmedTimeRequired,
                        projectOperationIds, projectOperationDetailIds), ct);
                    if (responseDetail.IsFailure)
                        return Result.Failure<CreateRequestMachineryStatusStatementResponse>(response.Error!);
                    var detail = responseDetail.Value;

                    statusStatementDetails.Add(detail!);
                }

            await _unitOfWork.CommitAsync(ct);

            var getByIdresponse = await _mediator.Send(new GetRequestMachineryStatusStatementByIdQuery(statusStatement!.Id), ct);
            if (getByIdresponse.IsFailure)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(getByIdresponse.Error!);
            var value = response.Value!;

            var getThirdPartyByIdQuery = await _mediator.Send(new GetThirdPartyByIdQuery(value!.ContractorId), ct);
            if (getThirdPartyByIdQuery.IsFailure)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(getThirdPartyByIdQuery.Error!);
            var thirdParty = getThirdPartyByIdQuery.Value;

            if (request.IsPettyCash == true)
                if (string.IsNullOrEmpty(request.PettyCashId))
                    return Result.Failure<CreateRequestMachineryStatusStatementResponse>(RequestMachineryStatusStatementErrors.InValidPettyCash);

            var paymentOrder = await _mediator.Send(new CreateRequestMachineryPaymentOrderCommand(
                value!,
                thirdParty,
                seasonValue,
                request.ContractorPrice,
                value.BankAccountId,
                request.PaymentDate,
                request.Description,
                request.PettyCashId,
                request.IsPettyCash,
                request.CostCategoryId,
                request.CostGroupId,
                request.DocumentTypeId,
                request.PreferentialTypeId), ct);
            if (paymentOrder.IsFailure)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(paymentOrder.Error!);
            var payment = paymentOrder.Value;

            var updateInfoCommand = await _mediator.Send(new UpdateMachineryStatusStatementPaymentCommand(
                    statusStatement,
                    requestMachineries!,
                    payment!.PaymentOrderId,
                    RequestMachineryStatusStatementStatus.Paid),
                ct);
            if (updateInfoCommand.IsFailure)
                return Result.Failure<CreateRequestMachineryStatusStatementResponse>(response.Error!);

            await _unitOfWork.CommitAsync(ct);
            scope.Complete();
            return new CreateRequestMachineryStatusStatementResponse(response.Value!.Id);
        }
    }

    public async Task<Result<RequestMachineryStatusStatementStatusChangerResponse?>> RequestMachineryStatusStatementStatusChanger(RequestMachineryStatusStatementStatusChangerRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<RequestMachineryStatusStatementStatusChangerValidator, RequestMachineryStatusStatementStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<RequestMachineryStatusStatementStatusChangerResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryStatusStatementByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<RequestMachineryStatusStatementStatusChangerResponse>(response.Error!);
        var value = response.Value!;

        var responseChanges = await _mediator.Send(new RequestMachineryStatusStatementStatusChangerCommand(value, request.Status), ct);
        if (responseChanges.IsFailure)
            return Result.Failure<RequestMachineryStatusStatementStatusChangerResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new RequestMachineryStatusStatementStatusChangerResponse(responseChanges.Value!.Id);
    }

    public async Task<Result<GetRequestMachineryStatusStatementByIdResponse?>> GetRequestMachineryStatusStatementById(GetRequestMachineryStatusStatementByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryStatusStatementByIdValidator, GetRequestMachineryStatusStatementByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryStatusStatementByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestMachineryStatusStatementByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetRequestMachineryStatusStatementByIdResponse>(response.Error!);
        var value = response.Value!;

        var data = value.Adapt<GetRequestMachineryStatusStatementByIdResponse>();

        List<long>? contractorIds = [];
        contractorIds.Add(value.ContractorId);
        contractorIds?.AddRange(value.RequestMachineryStatusStatementDetails.Where(x => x.OperatorId != null && x.OperatorId > 0).Select(x => (long)x.OperatorId!).Distinct().ToList());
        List<UserModel?>? contractors = [];
        if (contractorIds is not null && contractorIds.Count > 0)
            contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);
        data.Contractor = contractors?.FirstOrDefault(x => x?.Id == value.ContractorId)?.FullName;
        data.ContractorNickname = contractors?.FirstOrDefault(x => x?.Id == value.ContractorId)?.Nickname;

        var currencyIds = value.RequestMachineryStatusStatementDetails.Where(x => x.CurrencyId != null && x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        if (value!.RequestMachineryStatusStatementDetails is not null && value!.RequestMachineryStatusStatementDetails.Count > 0)
            foreach (var item in value.RequestMachineryStatusStatementDetails)
            {
                var projectOperations = string.Join(",", item.StatusStatementDetailProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList());
                var projectOperationDetails = string.Join(",", item.StatusStatementDetailProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList());

                data.Detail?.Add(new GetFilteredRequestMachineryStatusStatementDetailModel()
                {
                    Id = item.Id,
                    StatusStatementId = value.Id,
                    Unit = item.Unit,
                    FromDate = item.FromDate,
                    ToDate = item.ToDate,
                    MachineryId = item.Machinery?.Id,
                    Machinery = item.Machinery?.MachineryName,
                    ProjectId = item.Project.Id,
                    Project = item.Project.ProjectName,
                    RequestedCount = item.RequestedCount,
                    RequestMachineryId = item.RequestMachinery.Id,
                    TimeRequired = Convert.ToString(item.TimeRequired),
                    ProjectOperations = projectOperations,
                    ProjectOperationDetails = projectOperationDetails,
                    ContractorId = contractors?.Where(x => item.RequestMachinery.MachineryReservations.Any(z => z.FixAssetMachinery.ContractorId.Equals(x?.Id)) || item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.ThirdPartyId.Equals(x?.Id)))).FirstOrDefault()?.Id,
                    Contractor = contractors?.Where(x => item.RequestMachinery.MachineryReservations.Any(z => z.FixAssetMachinery.ContractorId.Equals(x?.Id)) || item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.ThirdPartyId.Equals(x?.Id)))).FirstOrDefault()?.FullName,
                    CostCenter = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                    CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                    CurrencyId = currencies?.Where(x => item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId.Equals(x.Id)))).FirstOrDefault()?.Id,
                    Currency = currencies?.Where(x => item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId.Equals(x.Id)))).FirstOrDefault()?.Name,
                    FinalPrice = item.RequestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).Where(x => x.IsConfirmed == true).FirstOrDefault()!.TotalPrice,
                    OperatorId = item.RequestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).Where(x => x.IsConfirmed == true).FirstOrDefault()?.RequestMachineryInquiryOperator.OperatorAppoinmentId,
                    Operator = contractors?.FirstOrDefault(x => x?.Id == item.RequestMachinery.InquiryOperators.SelectMany(z => z.Inquiries).Where(z => z.IsConfirmed == true).FirstOrDefault()?.RequestMachineryInquiryOperator.OperatorAppoinmentId)?.FullName,
                    RequestMachineryNumber = item.RequestMachinery.RequestNumber.ToString(),
                });
            }

        return data;
    }

    public async Task<Result<GetFilteredRequestMachineryStatusStatementResponse?>> GetFilteredRequestMachineryStatusStatement(GetFilteredRequestMachineryStatusStatementRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredRequestMachineryStatusStatementValidator, GetFilteredRequestMachineryStatusStatementRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredRequestMachineryStatusStatementResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetFilteredRequestMachineryStatusStatementQuery(null, request.ContractorIds, request.CostCenterIds, request.ProjectIds,
            request.MachineryIds, request.Statuses, request.Unit, request.StartDate, request.EndDate, companyId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize),
            ct);
        if (response.IsFailure)
            return Result.Failure<GetFilteredRequestMachineryStatusStatementResponse>(response.Error!);
        var values = response.Value!.Data!;

        var currencyIds = values.SelectMany(x => x.RequestMachineryStatusStatementDetails).Where(x => x.CurrencyId != null && x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        List<long>? contractorIds = [];
        contractorIds = values?.Where(x => x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
        contractorIds?.AddRange(values!.SelectMany(x => x.RequestMachineryStatusStatementDetails).Where(x => x.OperatorId != null && x.OperatorId > 0).Select(x => (long)x.OperatorId!).Distinct().ToList());
        List<UserModel?>? contractors = [];
        if (contractorIds is not null && contractorIds.Count > 0)
            contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var datas = values.Adapt<List<GetFilteredRequestMachineryStatusStatementModel>>();
        foreach (var data in datas)
        {
            var value = values!.FirstOrDefault(x => x.Id == data.Id);
            data.Contractor = contractors?.FirstOrDefault(x => x?.Id == data.ContractorId)?.FullName;
            data.ContractorNickname = contractors?.FirstOrDefault(x => x?.Id == data.ContractorId)?.Nickname;
            if (value!.RequestMachineryStatusStatementDetails is not null && value!.RequestMachineryStatusStatementDetails.Count > 0)
                foreach (var item in value.RequestMachineryStatusStatementDetails)
                {
                    var projectOperations = string.Join(",", item.StatusStatementDetailProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList());
                    var projectOperationDetails = string.Join(",", item.StatusStatementDetailProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList());

                    data.Detail?.Add(new GetFilteredRequestMachineryStatusStatementDetailModel()
                    {
                        Id = item.Id,
                        StatusStatementId = value.Id,
                        Unit = item.Unit,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        MachineryId = item.Machinery?.Id,
                        Machinery = item.Machinery?.MachineryName,
                        ProjectId = item.Project.Id,
                        Project = item.Project.ProjectName,
                        RequestedCount = item.RequestedCount,
                        RequestMachineryId = item.RequestMachinery.Id,
                        TimeRequired = Convert.ToString(item.TimeRequired),
                        ProjectOperations = projectOperations,
                        ProjectOperationDetails = projectOperationDetails,
                        ContractorId = contractors?.Where(x => item.RequestMachinery.MachineryReservations.Any(z => z.FixAssetMachinery.ContractorId.Equals(x?.Id)) || item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.ThirdPartyId.Equals(x?.Id)))).FirstOrDefault()?.Id,
                        Contractor = contractors?.Where(x => item.RequestMachinery.MachineryReservations.Any(z => z.FixAssetMachinery.ContractorId.Equals(x?.Id)) || item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.ThirdPartyId.Equals(x?.Id)))).FirstOrDefault()?.FullName,
                        CostCenter = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                        CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                        CurrencyId = currencies?.Where(x => item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId.Equals(x.Id)))).FirstOrDefault()?.Id,
                        Currency = currencies?.Where(x => item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId.Equals(x.Id)))).FirstOrDefault()?.Name,
                        FinalPrice = item.RequestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).Where(x => x.IsConfirmed == true).FirstOrDefault()!.TotalPrice,
                        OperatorId = item.RequestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).Where(x => x.IsConfirmed == true).FirstOrDefault()?.RequestMachineryInquiryOperator.OperatorAppoinmentId,
                        Operator = contractors?.FirstOrDefault(x => x?.Id == item.RequestMachinery.InquiryOperators.SelectMany(z => z.Inquiries).Where(z => z.IsConfirmed == true).FirstOrDefault()?.RequestMachineryInquiryOperator.OperatorAppoinmentId)?.FullName,
                        RequestMachineryNumber = item.RequestMachinery.RequestNumber.ToString(),
                    });
                }
        }

        return new GetFilteredRequestMachineryStatusStatementResponse(datas, response.Value!.RowCount!);
    }

    public async Task<Result<GetRequestMachineryStatusStatementStatusResponse?>> GetRequestMachineryStatusStatementStatus(GetRequestMachineryStatusStatementStatusRequest request, CT ct)
    {
        return await Task.FromResult(new GetRequestMachineryStatusStatementStatusResponse(EnumExt.GetEnumObjectList<RequestMachineryStatusStatementStatus>()));
    }

    public async Task<Result<GetRequestMachineryStatusStatementUnitResponse?>> GetRequestMachineryStatusStatementUnit(GetRequestMachineryStatusStatementUnitRequest request, CT ct)
    {
        return await Task.FromResult(new GetRequestMachineryStatusStatementUnitResponse(EnumExt.GetEnumObjectList<RequestMachineryStatusStatementUnit>()));
    }

    public async Task<Result<GetsRequestMachineryStatusStatementExcelEnumResponse?>> GetsRequestMachineryStatusStatementExcelEnum(GetsRequestMachineryStatusStatementExcelEnumRequest request, CT ct)
    {
        return await Task.FromResult(new GetsRequestMachineryStatusStatementExcelEnumResponse(EnumExt.GetEnumObjectList<RequestMachineryStatusStatementExcelEnum>()));
    }

    public async Task<Result<GetsRequestMachineryStatusStatementExcelExporterResponse?>> GetsRequestMachineryStatusStatementExcelExporter(GetsRequestMachineryStatusStatementExcelExporterRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsRequestMachineryStatusStatementExcelExporterValidator, GetsRequestMachineryStatusStatementExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsRequestMachineryStatusStatementExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetFilteredRequestMachineryStatusStatementQuery(request.Ids, request.ContractorIds, request.CostCenterIds, request.ProjectIds,
            request.MachineryIds, request.Statuses, request.Unit, request.StartDate, request.EndDate, companyId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize),
            ct);
        if (response.IsFailure)
            return Result.Failure<GetsRequestMachineryStatusStatementExcelExporterResponse>(response.Error!);
        var values = response.Value!.Data!;

        var currencyIds = values.SelectMany(x => x.RequestMachineryStatusStatementDetails).Where(x => x.CurrencyId != null && x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        List<long>? contractorIds = [];
        contractorIds = values?.Where(x => x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
        contractorIds?.AddRange(values!.SelectMany(x => x.RequestMachineryStatusStatementDetails).Where(x => x.OperatorId != null && x.OperatorId > 0).Select(x => (long)x.OperatorId!).Distinct().ToList());
        List<UserModel?>? contractors = [];
        if (contractorIds is not null && contractorIds.Count > 0)
            contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var datas = values.Adapt<List<GetFilteredRequestMachineryStatusStatementModel>>();
        foreach (var data in datas)
        {
            var value = values!.FirstOrDefault(x => x.Id == data.Id);
            data.Contractor = contractors?.FirstOrDefault(x => x?.Id == data.ContractorId)?.FullName;
            data.ContractorNickname = contractors?.FirstOrDefault(x => x?.Id == data.ContractorId)?.Nickname;
            if (value!.RequestMachineryStatusStatementDetails is not null && value!.RequestMachineryStatusStatementDetails.Count > 0)
                foreach (var item in value.RequestMachineryStatusStatementDetails)
                {
                    var projectOperations = string.Join(",", item.StatusStatementDetailProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList());
                    var projectOperationDetails = string.Join(",", item.StatusStatementDetailProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList());

                    data.Detail?.Add(new GetFilteredRequestMachineryStatusStatementDetailModel()
                    {
                        Id = item.Id,
                        StatusStatementId = value.Id,
                        Unit = item.Unit,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        MachineryId = item.Machinery?.Id,
                        Machinery = item.Machinery?.MachineryName,
                        ProjectId = item.Project.Id,
                        Project = item.Project.ProjectName,
                        RequestedCount = item.RequestedCount,
                        RequestMachineryId = item.RequestMachinery.Id,
                        TimeRequired = Convert.ToString(item.TimeRequired),
                        ProjectOperations = projectOperations,
                        ProjectOperationDetails = projectOperationDetails,
                        ContractorId = contractors?.Where(x => item.RequestMachinery.MachineryReservations.Any(z => z.FixAssetMachinery.ContractorId.Equals(x?.Id)) || item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.ThirdPartyId.Equals(x?.Id)))).FirstOrDefault()?.Id,
                        Contractor = contractors?.Where(x => item.RequestMachinery.MachineryReservations.Any(z => z.FixAssetMachinery.ContractorId.Equals(x?.Id)) || item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.ThirdPartyId.Equals(x?.Id)))).FirstOrDefault()?.FullName,
                        CostCenter = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                        CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                        CurrencyId = currencies?.Where(x => item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId.Equals(x.Id)))).FirstOrDefault()?.Id,
                        Currency = currencies?.Where(x => item.RequestMachinery.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId.Equals(x.Id)))).FirstOrDefault()?.Name,
                        FinalPrice = item.RequestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).Where(x => x.IsConfirmed == true).FirstOrDefault()!.TotalPrice,
                        OperatorId = item.RequestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).Where(x => x.IsConfirmed == true).FirstOrDefault()?.RequestMachineryInquiryOperator.OperatorAppoinmentId,
                        Operator = contractors?.FirstOrDefault(x => x?.Id == item.RequestMachinery.InquiryOperators.SelectMany(z => z.Inquiries).Where(z => z.IsConfirmed == true).FirstOrDefault()?.RequestMachineryInquiryOperator.OperatorAppoinmentId)?.FullName,
                        RequestMachineryNumber = item.RequestMachinery.RequestNumber.ToString(),
                    });
                }
        }

        var exporterModels = datas.Adapt<List<GetsRequestMachineryStatusStatementExcelExporterModel>>();

        var file = new FileContentResult(RequestMachineryStatusStatementExcels.RequestMachineryStatusStatementToExcel(exporterModels, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"RequestMachineryStatusStatement-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsRequestMachineryStatusStatementExcelExporterResponse(file);
    }

    private RequestMachineryStatusStatementUnit GetUnit(RequestMachinery requestMachinery)
    {
        RequestMachineryStatusStatementUnit unit = new RequestMachineryStatusStatementUnit();
        if (requestMachinery.Unit == RequestMachineryUnit.Daily)
            unit = RequestMachineryStatusStatementUnit.Daily;

        if (requestMachinery.Unit == RequestMachineryUnit.Hourly)
            unit = RequestMachineryStatusStatementUnit.Hourly;

        if (requestMachinery.Unit == RequestMachineryUnit.Serviced)
            unit = RequestMachineryStatusStatementUnit.Serviced;

        if (requestMachinery.Unit == RequestMachineryUnit.Volume)
            unit = RequestMachineryStatusStatementUnit.Volume;

        return unit;
    }
    private decimal? CalculateTotalPrice(RequestMachinery item)
    {
        var confirmedInquiry = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed);
        if (confirmedInquiry is not null)
            return confirmedInquiry.TotalPrice;

        if (item.ContractorId > 0 || item.MachineryReservations == null || item.MachineryReservations.Count == 0)
            return null;

        var reserve = item.MachineryReservations.FirstOrDefault();
        var fixMachinery = reserve?.FixAssetMachinery;

        decimal? priceRate = reserve?.Unit switch
        {
            MachineryReservationUnit.Hourly => fixMachinery?.HourlyRate,
            MachineryReservationUnit.Daily => fixMachinery?.DailyRate,
            MachineryReservationUnit.Serviced => fixMachinery?.ServiceRate,
            MachineryReservationUnit.Volume => fixMachinery?.VolumeRate,
            _ => null
        };

        return item.RequestCount * priceRate * item.ConfirmedTimeRequired;
    }
}

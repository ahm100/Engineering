using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.Seasons.Queries.GetSeasonById;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportationRequestAfterPaymentCommand;
using Engineering.Application.Services.TransportationRequests.Models.CreateAirPlanePaymentOrder;
using Engineering.Application.Services.TransportationRequests.Models.CreateSnapPaymentOrder;
using Engineering.Application.Services.TransportationRequests.Models.CreateTransportationRequestPaymentOrder;
using Engineering.Application.Services.TransportationRequests.Queries.GetByIdForPayment;
using Engineering.Application.Services.TransportationRequests.Queries.GetByIdsForPayment;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Application.WebServices.MetaDataServices.Currencies.Queries.GetDefaultCurrency;
using Gita.Backend.Shared.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetThirdPartyById;
using CreateAirPlanePaymentOrder = Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateAirPlanePaymentOrder.CreateAirPlanePaymentOrderCommand;
using CreateSnapPaymentOrder = Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateSnapPaymentOrder.CreateSnapPaymentOrderCommand;
using CreateTransportationPaymentOrder = Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateTransportationRequestPaymentOrder.CreateTransportationRequestPaymentOrderCommand;

namespace Engineering.Application.Services.TransportationRequests;

public partial class TransportationRequestLogic : ITransportationRequestLogic
{
    public async Task<Result<CreateTransportationRequestPaymentOrderResponse?>> CreateTransportationRequestPaymentOrder(CreateTransportationRequestPaymentOrderRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateTransportationRequestPaymentOrderValidator, CreateTransportationRequestPaymentOrderRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTransportationRequestByIdForPaymentQuery(request.TransportationRequestId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(response.Error!);
        var value = response.Value!;

        if (request.PaymentDate is not null)
            if (request.PaymentDate!.Value.Date < DateTime.Now.Date)
                return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(CSSErrors.InValidPaymentDate);

        if (request.SeasonId is null || request.SeasonId <= 0)
            return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(CSSErrors.InValidSeasonId);

        var responseSeason = await _mediator.Send(new GetSeasonByIdQuery(request.SeasonId!.Value), ct);
        if (responseSeason.IsFailure)
            return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(responseSeason.Error!);
        var seasonValue = responseSeason.Value!;

        if (value.PaymentDate != null)
            return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(TransportationRequestErrors.InvalidPayment(TimeCalculator.ConvertToShamsi(value?.PaymentDate), value?.RequestNumber));

        CreatePaymentOrderWithAutoDetailResponseModel? paymentOrder = null;
        ThirdPartyByIdModel? thirdpartyModel = null;
        if (value.TransportationRequestStatus == TransportationRequestStatus.Accepted)
        {
            if (request.PaymentType == TransportationPaymentType.StatementPaid)
            {
                if (request.PaymentDate is null)
                    return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(TransportationRequestErrors.InValidConfirmedPaymentDate);

                if (request.BankAccountId is null || request.BankAccountId <= 0)
                    return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(TransportationRequestErrors.InValidConfirmedBankAccountId);

                if (string.IsNullOrEmpty(request.IBAN) && request.BankAccountId is null)
                    return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(TransportationRequestErrors.UnValidIban);

                if (request.IsAirPlane == true && request.ThirdpartyId is not null && request.ThirdpartyId > 0 && request.ThirdpartyId == value.TicketPayerId)
                {
                    var getThirdPartyByIdQuery = await _mediator.Send(new GetThirdPartyByIdQuery(request.ThirdpartyId!.Value), ct);
                    if (getThirdPartyByIdQuery.IsFailure)
                        return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(TransportationRequestErrors.UnValidPaymentThirdParty);
                    thirdpartyModel = getThirdPartyByIdQuery.Value;
                }
                else if (value!.DriverId is not null && value.DriverId > 0)
                {
                    var getThirdPartyByIdQuery = await _mediator.Send(new GetThirdPartyByIdQuery(value!.DriverId!.Value), ct);
                    thirdpartyModel = getThirdPartyByIdQuery.Value;
                }
                else if (request.ThirdpartyId is not null && request.ThirdpartyId > 0)
                {
                    var getThirdPartyByIdQuery = await _mediator.Send(new GetThirdPartyByIdQuery(request.ThirdpartyId!.Value), ct);
                    thirdpartyModel = getThirdPartyByIdQuery.Value;
                }
                else
                    return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(TransportationRequestErrors.UnValidPaymentThirdParty);
            }

            if (request.ConfirmedPrice is null || request.ConfirmedPrice <= 0)
                return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(TransportationRequestErrors.InValidConfirmedPrice);
            if (string.IsNullOrEmpty(request.Description))
                return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(TransportationRequestErrors.InValidDescription);

            var fullName = thirdpartyModel != null ? thirdpartyModel.FullName : value.DriverName;
            var description = request.PaymentType == TransportationPaymentType.PettyCash ? request.Description : request.Description + ", " + $"پرداخت به شخص {fullName} به شماره شبای {request.IBAN} انجام گیرد.";

            if (request.PaymentType == TransportationPaymentType.PettyCash)
                if (string.IsNullOrEmpty(request.PettyCashId))
                    return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(RequestMachineryStatusStatementErrors.InValidPettyCash);

            DateTime? paymentDate = null;
            if (request.PaymentType == TransportationPaymentType.PettyCash || request.PaymentType == TransportationPaymentType.StatementPaid)
            {
                bool isPettyCash = request.PaymentType == TransportationPaymentType.PettyCash ? true : false;
                var thirdparty = request.PaymentType == TransportationPaymentType.PettyCash ? null : thirdpartyModel;
                var bankAccountId = request.PaymentType == TransportationPaymentType.PettyCash ? null : request.BankAccountId;
                paymentDate = request.PaymentType == TransportationPaymentType.PettyCash ? DateTime.Now : request.PaymentDate;

                var createPaymentOrder = await _mediator.Send(new CreateTransportationPaymentOrder(
                    value,
                    thirdparty,
                    seasonValue,
                    request.ConfirmedPrice,
                    bankAccountId,
                    paymentDate,
                    description,
                    request.PettyCashId,
                    isPettyCash,
                    request.CostCategoryId ?? value.CostCategoryId,
                    request.CostGroupId ?? value.CostGroupId,
                    request.DocumentTypeId,
                    request.PreferentialTypeId), ct);
                if (createPaymentOrder.IsFailure)
                    return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(createPaymentOrder.Error!);
                paymentOrder = createPaymentOrder.Value;
            }

            var responseChanges = await _mediator.Send(new UpdateTransportationRequestAfterPaymentCommand(value, paymentOrder!.PaymentOrderId, request.Description, paymentDate, request.PaymentType), ct);
            if (responseChanges.IsFailure)
                return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(response.Error!);
        }
        else
            return Result.Failure<CreateTransportationRequestPaymentOrderResponse>(TransportationRequestErrors.UnValidStatus);

        await _unitOfWork.CommitAsync(ct);

        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
            await SendTelegramMessagePaidTransportationRequest(value, ct);

        return new CreateTransportationRequestPaymentOrderResponse(value.Id, paymentOrder.PaymentOrderId, true);
    }

    public async Task<Result<CreateAirPlanePaymentOrderResponse?>> CreateAirPlanePaymentOrder(CreateAirPlanePaymentOrderRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateAirPlanePaymentOrderValidator, CreateAirPlanePaymentOrderRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateAirPlanePaymentOrderResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTransportationRequestByIdForPaymentQuery(request.AirPlaneRequestId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateAirPlanePaymentOrderResponse>(response.Error!);
        var value = response.Value!;

        if (request.PaymentDate is not null)
            if (request.PaymentDate!.Value.Date < DateTime.Now.Date)
                return Result.Failure<CreateAirPlanePaymentOrderResponse>(CSSErrors.InValidPaymentDate);

        if (request.SeasonId is null || request.SeasonId <= 0)
            return Result.Failure<CreateAirPlanePaymentOrderResponse>(CSSErrors.InValidSeasonId);

        var responseSeason = await _mediator.Send(new GetSeasonByIdQuery(request.SeasonId!.Value), ct);
        if (responseSeason.IsFailure)
            return Result.Failure<CreateAirPlanePaymentOrderResponse>(responseSeason.Error!);
        var seasonValue = responseSeason.Value!;

        if (value.PaymentDate != null)
            return Result.Failure<CreateAirPlanePaymentOrderResponse>(TransportationRequestErrors.InvalidPayment(TimeCalculator.ConvertToShamsi(value?.PaymentDate), value?.RequestNumber));

        CreatePaymentOrderWithAutoDetailResponseModel? paymentOrder = null;
        ThirdPartyByIdModel? thirdpartyModel = null;
        if (value.TransportationRequestStatus == TransportationRequestStatus.Accepted)
        {
            if (request.PaymentType == TransportationPaymentType.StatementPaid)
            {
                if (request.PaymentDate is null)
                    return Result.Failure<CreateAirPlanePaymentOrderResponse>(TransportationRequestErrors.InValidConfirmedPaymentDate);

                if (request.BankAccountId is null || request.BankAccountId <= 0)
                    return Result.Failure<CreateAirPlanePaymentOrderResponse>(TransportationRequestErrors.InValidConfirmedBankAccountId);

                if (string.IsNullOrEmpty(request.IBAN) && request.BankAccountId is null)
                    return Result.Failure<CreateAirPlanePaymentOrderResponse>(TransportationRequestErrors.UnValidIban);

                if (request.ThirdpartyId is not null && request.ThirdpartyId > 0 && request.ThirdpartyId == value.TicketPayerId)
                {
                    var getThirdPartyByIdQuery = await _mediator.Send(new GetThirdPartyByIdQuery(request.ThirdpartyId!.Value), ct);
                    if (getThirdPartyByIdQuery.IsFailure)
                        return Result.Failure<CreateAirPlanePaymentOrderResponse>(TransportationRequestErrors.UnValidPaymentThirdParty);
                    thirdpartyModel = getThirdPartyByIdQuery.Value;
                }
                else
                    return Result.Failure<CreateAirPlanePaymentOrderResponse>(TransportationRequestErrors.UnValidPaymentThirdParty);
            }

            if (request.ConfirmedPrice is null || request.ConfirmedPrice <= 0)
                return Result.Failure<CreateAirPlanePaymentOrderResponse>(TransportationRequestErrors.InValidConfirmedPrice);
            if (string.IsNullOrEmpty(request.Description))
                return Result.Failure<CreateAirPlanePaymentOrderResponse>(TransportationRequestErrors.InValidDescription);

            var description = request.PaymentType == TransportationPaymentType.PettyCash ? request.Description : request.Description + ", " + $"پرداخت به شخص {thirdpartyModel?.FullName} به شماره شبای {request.IBAN} انجام گیرد.";

            if (request.PaymentType == TransportationPaymentType.PettyCash)
                if (string.IsNullOrEmpty(request.PettyCashId))
                    return Result.Failure<CreateAirPlanePaymentOrderResponse>(RequestMachineryStatusStatementErrors.InValidPettyCash);

            DateTime? paymentDate = null;
            if (request.PaymentType == TransportationPaymentType.PettyCash || request.PaymentType == TransportationPaymentType.StatementPaid)
            {
                bool isPettyCash = request.PaymentType == TransportationPaymentType.PettyCash ? true : false;
                var thirdparty = request.PaymentType == TransportationPaymentType.PettyCash ? null : thirdpartyModel;
                var bankAccountId = request.PaymentType == TransportationPaymentType.PettyCash ? null : request.BankAccountId;
                paymentDate = request.PaymentType == TransportationPaymentType.PettyCash ? DateTime.Now : request.PaymentDate;

                var createPaymentOrder = await _mediator.Send(new CreateAirPlanePaymentOrder(
                    value,
                    thirdparty,
                    seasonValue,
                    request.ConfirmedPrice,
                    bankAccountId,
                    paymentDate,
                    description,
                    request.PettyCashId,
                    isPettyCash,
                    request.CostCategoryId ?? value.CostCategoryId,
                    request.CostGroupId ?? value.CostGroupId,
                    request.DocumentTypeId,
                    request.PreferentialTypeId), ct);
                if (createPaymentOrder.IsFailure)
                    return Result.Failure<CreateAirPlanePaymentOrderResponse>(createPaymentOrder.Error!);
                paymentOrder = createPaymentOrder.Value;
            }

            var responseChanges = await _mediator.Send(new UpdateTransportationRequestAfterPaymentCommand(value, paymentOrder!.PaymentOrderId, request.Description, paymentDate, request.PaymentType), ct);
            if (responseChanges.IsFailure)
                return Result.Failure<CreateAirPlanePaymentOrderResponse>(response.Error!);
        }
        else
            return Result.Failure<CreateAirPlanePaymentOrderResponse>(TransportationRequestErrors.UnValidStatus);

        await _unitOfWork.CommitAsync(ct);

        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
            await SendTelegramMessagePaidTransportationRequest(value, ct);

        return new CreateAirPlanePaymentOrderResponse(value.Id, paymentOrder.PaymentOrderId, true);
    }

    public async Task<Result<CreateSnapPaymentOrderResponse?>> CreateSnapPaymentOrder(CreateSnapPaymentOrderRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateSnapPaymentOrderValidator, CreateSnapPaymentOrderRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateSnapPaymentOrderResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetByIdsForPaymentQuery(request.SnapRequestIds), ct);
        if (response.IsFailure)
            return Result.Failure<CreateSnapPaymentOrderResponse>(response.Error!);
        var values = response.Value!;

        if (request.IsPettyCash != true)
            if (request.ThirdpartyId == null)
                return Result.Failure<CreateSnapPaymentOrderResponse>(CSSErrors.ThirdpartyIdIsNull);

        if (request.PaymentDate is not null)
            if (request.PaymentDate!.Value.Date < DateTime.Now.Date)
                return Result.Failure<CreateSnapPaymentOrderResponse>(CSSErrors.InValidPaymentDate);

        if (request.SeasonId is null || request.SeasonId <= 0)
            return Result.Failure<CreateSnapPaymentOrderResponse>(CSSErrors.InValidSeasonId);

        var responseSeason = await _mediator.Send(new GetSeasonByIdQuery(request.SeasonId!.Value), ct);
        if (responseSeason.IsFailure)
            return Result.Failure<CreateSnapPaymentOrderResponse>(responseSeason.Error!);
        var seasonValue = responseSeason.Value!;

        var defaultCurrencyQuery = await _mediator.Send(new GetDefaultCurrencyQuery(), ct);
        var defaultCurrency = defaultCurrencyQuery.Value!;

        if (values.Any(x => x.PaymentDate != null))
        {
            var snapPayment = values.FirstOrDefault(x => x.PaymentDate != null);
            return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.InvalidPayment(TimeCalculator.ConvertToShamsi(snapPayment?.PaymentDate), snapPayment?.RequestNumber));
        }

        var thirdPartyIds = values.Select(x => x.SnapRequester).Distinct().ToList();
        thirdPartyIds.Add(request.ThirdpartyId);
        List<Application.WebServices.MetaDataServices.ThirdParties.Models.ThirdParty>? thirdpartiesModel = [];
        if (thirdPartyIds is not null && thirdPartyIds.Count > 0)
            thirdpartiesModel = await WebServicesLogic.ThirdPartiesDataReceiver(thirdPartyIds.Adapt<List<long>>().ToList(), _mediator, ct);

        CreatePaymentOrderWithAutoDetailResponseModel? paymentOrder = null;
        if (values.All(x => x.TransportationRequestStatus == TransportationRequestStatus.Accepted))
        {
            if (request.PaymentDate is null)
                return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.InValidConfirmedPaymentDate);
            if (request.ConfirmedPrice is null || request.ConfirmedPrice <= 0)
                return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.InValidConfirmedPrice);
            if (string.IsNullOrEmpty(request.Description))
                return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.InValidDescription);

            var description = "";
            if (request.IsPettyCash == true)
            {
                if (string.IsNullOrEmpty(request.PettyCashId))
                    return Result.Failure<CreateSnapPaymentOrderResponse>(RequestMachineryStatusStatementErrors.InValidPettyCash);

                if (values.Any(x => x.PersonalPayment == true))
                    return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.InValidPayment);

                description = request.Description + ", " + $"پرداخت به تنخواه {request.ThirdPartyName} باید انجام گیرد";
            }
            else
            {
                if (values.Any(x => x.PersonalPayment == false))
                    return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.InValidPersonalPayment);

                if (thirdpartiesModel is not null && thirdpartiesModel.Count > 0)
                {
                    if (thirdpartiesModel.Select(x => x.Id).ToList().Count > 1)
                        return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.InValidthirdpartiesCount);

                    if (thirdpartiesModel.All(x => x.Id != request.ThirdpartyId))
                        return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.InValidThirdPartyPayment);
                }

                if (request.BankAccountId is null || request.BankAccountId <= 0)
                    return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.InValidConfirmedBankAccountId);

                if (string.IsNullOrEmpty(request.IBAN) && request.BankAccountId is null)
                    return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.UnValidIban);

                var fullName = thirdpartiesModel?.FirstOrDefault(x => x.Id == request.ThirdpartyId)?.FullName;
                description = request.Description + ", " + $"پرداخت به شخص {fullName} به شماره شبای {request.IBAN} انجام گیرد.";
            }

            bool isPettyCash = request.IsPettyCash == true ? true : false;
            var thirdparties = request.IsPettyCash == true ? null : thirdpartiesModel;
            var bankAccountId = request.IsPettyCash == true ? null : request.BankAccountId;
            var paymentDate = request.IsPettyCash == true ? DateTime.Now : request.PaymentDate;

            var createPaymentOrder = await _mediator.Send(new CreateSnapPaymentOrder(
                values,
                thirdparties,
                seasonValue,
                request.ConfirmedPrice,
                paymentDate,
                request.ThirdPartyName,
                description,
                defaultCurrency.Id,
                request.PettyCashId,
                isPettyCash,
                request.ThirdpartyId,
                bankAccountId,
                request.CostCategoryId,
                request.CostGroupId,
                request.DocumentTypeId,
                request.PreferentialTypeId), ct);
            if (createPaymentOrder.IsFailure)
                return Result.Failure<CreateSnapPaymentOrderResponse>(createPaymentOrder.Error!);
            paymentOrder = createPaymentOrder.Value;

            foreach (var item in values)
            {
                var responseChanges = await _mediator.Send(new UpdateTransportationRequestAfterPaymentCommand(item, paymentOrder!.PaymentOrderId, request.Description, paymentDate, null), ct);
                if (responseChanges.IsFailure)
                    return Result.Failure<CreateSnapPaymentOrderResponse>(response.Error!);
            }
        }
        else
            return Result.Failure<CreateSnapPaymentOrderResponse>(TransportationRequestErrors.UnValidStatus);

        await _unitOfWork.CommitAsync(ct);

        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
            await SendTelegramMessagePaidSnap(values, request.ThirdPartyName, request.PaymentDate, request.ConfirmedPrice, ct);

        return new CreateSnapPaymentOrderResponse(values.Select(x => x.Id).ToList(), paymentOrder!.PaymentOrderId, true);
    }
}
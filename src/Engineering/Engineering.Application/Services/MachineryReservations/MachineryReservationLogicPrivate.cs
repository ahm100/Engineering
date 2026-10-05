using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryById;
using Engineering.Application.Services.MachineryReservations.Models.CreateMachineryReservation;
using Engineering.Application.Services.RequestMachineries.Commands.ChangeRequestMachineryStatus;
using Engineering.Application.Services.RequestMachineries.Commands.RequestMachineryAssignments;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdIncludeLess;
using Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiry;
using Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryOperator;
using Engineering.Application.Services.RequestMachineryManagements.Commands.SetInquiryConfirmedUser;
using Engineering.Application.Services.RequestMachineryManagements.Models.AssignMachineryForRequestMachinery;
using Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByCostCenterIds;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Queries.GetMachineryOperators;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.Messengers.Enums;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Gita.Backend.Shared.Application.Shared.Models.UserProfiles;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetUserById;
using Gita.Backend.Shared.Application.WebServices.MetaDataServices.Currencies.Queries.GetDefaultCurrency;

namespace Engineering.Application.Services.MachineryReservations;

public partial class MachineryReservationLogic : IMachineryReservationLogic
{
    private async Task<bool> SendMessageRequestMachinery(
        RequestMachinery requestMachinery,
        FixAssetMachinery fixAssetMachinery,
        MachineryReservation reservation,
        string? confirmer,
        CT ct)
    {
        var getTelegramChats = await _messengerChannelRepo.GetFltrChannel([requestMachinery.Project.Id],
            [requestMachinery.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId], MessengerMessageType.RequestMachinery, ct);

        if (getTelegramChats is null || getTelegramChats.Count > 0)
            return false;

        if (getTelegramChats is not null && getTelegramChats.Count > 0)
        {
            List<Guid> ids = new();
            if (requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList() is not null &&
                requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList().Any())
                foreach (var item in requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList())
                    ids.Add(Guid.Parse(item));

            var creator = "";
            var currentUserId = requestMachinery.CreatorId;
            var getUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
            if (getUser.IsSuccess && getUser.Value is not null)
                creator = getUser.Value.FullName;

            var contractor = "";
            if (fixAssetMachinery.FixAssetMachineryType == FixAssetMachineryType.rented)
                if (fixAssetMachinery.ContractorId is not null && fixAssetMachinery.ContractorId > 0)
                {
                    var thirdParty = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long> { fixAssetMachinery.ContractorId!.Value }, null, true, null), ct);
                    if (thirdParty.IsSuccess && thirdParty.Value is not null)
                        contractor = thirdParty.Value.Data?.FirstOrDefault()?.FullName;
                }

            foreach (var chat in getTelegramChats!)
            {
                var message = FixAssetMachineryMessageModel(requestMachinery, fixAssetMachinery, reservation, contractor, confirmer, creator);
                await TelegramServicesLogic.SendMessage(chat, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
            }
        }

        return true;
    }

    private async Task<bool> SendTelegramMessageRequestMachinery(
        RequestMachinery requestMachinery,
        FixAssetMachinery fixAssetMachinery,
        MachineryReservation reservation,
        string? confirmer,
        CT ct)
    {
        var costCenterId = requestMachinery.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId;

        // Get TelegramChats
        var getTelegramChats = await _mediator.Send(new GetsTelegramChatByCostCenterIdsQuery(
            [costCenterId],
            Domain.Entities.TelegramChats.Enums.TelegramMessageType.RequestMachinery,
            null,
            null,
            true,
            0,
            0), ct);
        if (getTelegramChats.IsFailure || getTelegramChats.Value is null || getTelegramChats.Value?.Data is null)
            return false;

        var telegramChats = getTelegramChats.Value.Data;

        var validChats = telegramChats
            .SelectMany(chat => chat.TelegramChatTypes)
            .Where(x => x.TelegramMessageType is Domain.Entities.TelegramChats.Enums.TelegramMessageType.RequestMachinery or
            Domain.Entities.TelegramChats.Enums.TelegramMessageType.OtherGroups)
            .GroupBy(x => x.ChatId)
            .Select(g => g.FirstOrDefault())
            .Where(chat => chat is not null)
            .ToList();

        if (validChats is not null && validChats.Count > 0)
        {
            List<Guid> ids = new();
            if (requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList() is not null &&
                requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList().Any())
                foreach (var item in requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList())
                    ids.Add(Guid.Parse(item));

            var creator = "";
            var currentUserId = requestMachinery.CreatorId;
            var getUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
            if (getUser.IsSuccess && getUser.Value is not null)
                creator = getUser.Value.FullName;

            var contractor = "";
            if (fixAssetMachinery.FixAssetMachineryType == FixAssetMachineryType.rented)
                if (fixAssetMachinery.ContractorId is not null && fixAssetMachinery.ContractorId > 0)
                {
                    var thirdParty = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long> { fixAssetMachinery.ContractorId!.Value }, null, true, null), ct);
                    if (thirdParty.IsSuccess && thirdParty.Value is not null)
                        contractor = thirdParty.Value.Data?.FirstOrDefault()?.FullName;
                }

            foreach (var chat in validChats!)
            {
                var createDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.Created);
                var fromDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.FromDate);
                var startDateShamsi = TimeCalculator.ConvertToShamsi(reservation.StartDate);
                var toDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.ToDate);
                var endDateShamsi = TimeCalculator.ConvertToShamsi(reservation.EndDate);
                var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var createTime = TimeZoneInfo.ConvertTime(requestMachinery.Created, tehranTimeZone).ToString("HH:mm:ss");

                var resultUrl = requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList() != null
                    ? string.Join(",", requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList())
                    : string.Empty;

#pragma warning disable CS8604 // Possible null reference argument.
                var x = await TelegramServicesLogic.FixAssetMachineryMessage(
                       chat?.ChatId,
                       requestMachinery,
                       fixAssetMachinery,
                       contractor,
                       fromDateShamsi,
                       startDateShamsi,
                       toDateShamsi,
                       endDateShamsi,
                       createDateShamsi,
                       createTime,
                       confirmer,
                       creator,
                       ids,
                       _mediator,
                       _telegramMessageHistoryLogic,
                       chat!.TelegramChat.Id,
                       resultUrl,
                       Domain.Entities.TelegramChats.Enums.TelegramMessageType.RequestMachinery,
                       _authorization,
                       _messageSenderConfig,
                       ct);
#pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        return true;
    }

    private RequestMachineryUnit GetUnit(
        MachineryReservation machineryReservation)
    {
        RequestMachineryUnit unit = new RequestMachineryUnit();
        if (machineryReservation.Unit == MachineryReservationUnit.Daily)
            unit = RequestMachineryUnit.Daily;

        if (machineryReservation.Unit == MachineryReservationUnit.Hourly)
            unit = RequestMachineryUnit.Hourly;

        if (machineryReservation.Unit == MachineryReservationUnit.Serviced)
            unit = RequestMachineryUnit.Serviced;

        if (machineryReservation.Unit == MachineryReservationUnit.Volume)
            unit = RequestMachineryUnit.Volume;

        return unit;
    }

    private async Task<Result<FixAssetMachinery?>> GetFixMachineryAsync(
        long fixAssetMachineryId, CT ct)
    {
        var fixMachineryQuery = await _mediator.Send(new GetFixAssetMachineryByIdQuery(fixAssetMachineryId), ct);
        return fixMachineryQuery.IsFailure ? Result.Failure<FixAssetMachinery?>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId) : fixMachineryQuery.Value;
    }

    private async Task<Result<RequestMachinery?>> GetRequestMachineryAsync(
        long requestMachineryId, CT ct)
    {
        var requestMachineryQuery = await _mediator.Send(new GetRequestMachineryByIdIncludeLessQuery(requestMachineryId), ct);
        return requestMachineryQuery.IsFailure || requestMachineryQuery.Value is null ? Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound) : requestMachineryQuery.Value;
    }

    private (decimal? confirmTime, decimal? totalHours) CalculateConfirmTime(
        CreateMachineryReservationRequest? request,
        RequestMachinery? requestMachinery)
    {
        decimal? confirmTime = null;
        decimal? totalHours = null;

        if (!string.IsNullOrEmpty(request?.ConfirmedTimeRequired))
        {
            if (request?.MachineryReservationUnit == MachineryReservationUnit.Hourly)
            {
                if (!(request.ConfirmedTimeRequired.Split(':')[0].Count() >= 2 && request.ConfirmedTimeRequired.Split(':')[1].Count() == 2))
                    return (null, null);

                if (int.Parse(request.ConfirmedTimeRequired.Split(':')[1]) > 59)
                    return (null, null);

                TimeSpan time = TimeSpan.Parse(request.ConfirmedTimeRequired);
                totalHours = (decimal)time.TotalHours;
            }
            else
            {
                totalHours = Convert.ToDecimal(request?.ConfirmedTimeRequired);
            }
        }

        confirmTime = totalHours ?? requestMachinery?.TimeRequired;
        return (confirmTime, totalHours);
    }

    private (DateTime? startDate, DateTime? endDate) CalculateDates(
        CreateMachineryReservationRequest request)
    {
        DateTime? startDate = request.StartTime != null ? request.StartDate.Date.Add(request.StartTime.Value) : null;
        DateTime? endDate = request.EndTime != null ? request.EndDate.Date.Add(request.EndTime.Value) : null;
        return (startDate, endDate);
    }

    private decimal? GetPriceRate(
        MachineryReservationUnit unit,
        FixAssetMachinery fixMachinery,
        DateTime startDate,
        DateTime endDate)
    {
        decimal? totalHourlyRate = 0;
        decimal? totalDailyRate = 0;
        decimal? totalServiceRate = 0;
        decimal? totalVolumeRate = 0;
        double? totalDays = 0;
        if (fixMachinery.FixAssetMachineryRates is not null && fixMachinery.FixAssetMachineryRates.Count > 0)
        {
            totalDays = (endDate - startDate).TotalDays;
            for (int i = 0; i <= totalDays; i++)
            {
                var currentDate = startDate.AddDays(i);

                var rates = fixMachinery.FixAssetMachineryRates.Where(x => x.StartDate.Date <= currentDate.Date && x.EndDate.Date >= currentDate.Date).ToList();

                var activeRates = rates
                    .Where(x => x.StartDate.Date <= currentDate.Date && x.EndDate.Date >= currentDate.Date)
                    .OrderByDescending(x => x.StartDate.Date)
                    .ToList();

                var selectedRate = activeRates.FirstOrDefault();

                if (selectedRate is null)
                {
                    selectedRate = rates
                        .OrderByDescending(x => x.StartDate.Date)
                        .FirstOrDefault();

                    totalHourlyRate += (selectedRate?.HourlyRate ?? 0);
                    totalDailyRate += (selectedRate?.DailyRate ?? 0);
                    totalServiceRate += (selectedRate?.ServiceRate ?? 0);
                    totalVolumeRate += (selectedRate?.VolumeRate ?? 0);
                }

                if (selectedRate is not null)
                {
                    totalHourlyRate += (selectedRate.HourlyRate ?? 0);
                    totalDailyRate += (selectedRate.DailyRate ?? 0);
                    totalServiceRate += (selectedRate.ServiceRate ?? 0);
                    totalVolumeRate += (selectedRate.VolumeRate ?? 0);
                }
            }
        }
        else
        {
            totalHourlyRate = fixMachinery.HourlyRate;
            totalDailyRate = fixMachinery.DailyRate;
            totalServiceRate = fixMachinery.ServiceRate;
            totalVolumeRate = fixMachinery.VolumeRate;
        }

        return unit switch
        {
            MachineryReservationUnit.Hourly => totalHourlyRate,
            MachineryReservationUnit.Daily => totalDailyRate,
            MachineryReservationUnit.Serviced => totalServiceRate,
            MachineryReservationUnit.Volume => totalVolumeRate,
            _ => null
        };
    }

    private async Task<Result> ConfirmRequestMachineryStatus(
        RequestMachinery requestMachinery, CT ct)
    {
        return await _mediator.Send(new ChangeRequestMachineryStatusCommand(requestMachinery, RequestMachineryStatus.Confirmed, null, null), ct);
    }

    private async Task<Result> HandleInquiryAsync(
        ExtraInfo currentUser,
        RequestMachinery requestMachinery,
        long? contractorId,
        decimal priceRate,
        decimal totalPrice,
        RequestMachineryUnit unit,
        decimal confirmTime,
        string? description, CT ct)
    {
        var getOfficers = await _mediator.Send(new GetMachineryOperatorsQuery(currentUser.ThirdPartyId, null, null, null, null, 1, 1), ct);
        if (getOfficers.IsFailure)
            return Result.Failure(RequestMachineryErrors.RequestMachineryOperatorNotFound);

        var officers = getOfficers.Value!.Data!.FirstOrDefault()!;
        if (officers.UserId != currentUser.Id)
            return Result.Failure(RequestMachineryInquiryErrors.InValidRequestMachineryInquiryOperator);

        var operatorUser = new RequestMachineryInquiryOperator(officers.Id, officers.UserId.Value, requestMachinery);

        var currencyQuery = await _mediator.Send(new GetDefaultCurrencyQuery(), ct);
        if (currencyQuery.IsFailure)
            return Result.Failure(currencyQuery.Error!);

        var currency = currencyQuery.Value;

        var inquiryOperatorResult = await _mediator.Send(new CreateRequestMachineryInquiryOperatorCommand(operatorUser.OperatorAppoinmentId, operatorUser.OperatorAppoinmentUserId, requestMachinery), ct);
        if (inquiryOperatorResult.IsFailure)
            return Result.Failure<AssignMachineryForRequestMachineryResponse>(inquiryOperatorResult.Error!);

        var createInquiryResponse = await _mediator.Send(new CreateRequestMachineryInquiryCommand(contractorId!.Value, currency!.Id, requestMachinery.RequestCount, unit, priceRate, totalPrice, description, confirmTime, operatorUser), ct);
        if (createInquiryResponse.IsFailure)
            return Result.Failure(createInquiryResponse.Error!);

        var requestMachineryInquiry = createInquiryResponse.Value!;

        var inquiryDoneResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(requestMachinery, RequestMachineryStatus.InquiryDone, null, null), ct);
        if (inquiryDoneResponse.IsFailure)
            return Result.Failure(inquiryDoneResponse.Error!);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(requestMachinery, RequestMachineryStatus.MachineryAppoinment, null, null), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure(updateStatusResponse.Error!);

        var confirmInquiryResponse = await _mediator.Send(new SetInquiryConfirmedUserCommand(requestMachineryInquiry, currentUser.Id), ct);
        return confirmInquiryResponse.IsFailure ? Result.Failure(confirmInquiryResponse.Error!) : Result.Success();
    }

    private async Task HandleAssignmentAsync(
        FixAssetMachinery fixMachinery,
        RequestMachinery requestMachinery, CT ct)
    {
        var assignmentIdentifier = !string.IsNullOrEmpty(fixMachinery?.NumberPlates) ? fixMachinery.NumberPlates :
                                   !string.IsNullOrEmpty(fixMachinery?.MachinerySpecification) ? fixMachinery.MachinerySpecification : "ماشین آلات فاقد پلاک";

        var createAssignmentResponse = await _mediator.Send(new CreateRequestMachineryAssignmentCommand(null, assignmentIdentifier, requestMachinery, false), ct);
        if (createAssignmentResponse.IsFailure)
            throw new Exception(createAssignmentResponse.Error!);
    }

    public static string FixAssetMachineryMessageModel(
        RequestMachinery requestMachinery,
        FixAssetMachinery? fixAssetMachinery,
        MachineryReservation? reservation,
        string? contractor,
        string? confirmer,
        string? creator)
    {
        string message = string.Empty;

        var createDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.Created);
        var fromDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.FromDate);
        var startDateShamsi = TimeCalculator.ConvertToShamsi(reservation?.StartDate);
        var toDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery?.ToDate);
        var endDateShamsi = TimeCalculator.ConvertToShamsi(reservation?.EndDate);
        var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        var createTime = TimeZoneInfo.ConvertTime(requestMachinery!.Created, tehranTimeZone).ToString("HH:mm:ss");

        var rate = fixAssetMachinery?.FixAssetMachineryRates
                .LastOrDefault(x =>
                    x.StartDate <= requestMachinery?.ConfirmFromDate &&
                    x.EndDate >= requestMachinery!.ConfirmToDate);

        var hourlyPrice = rate?.HourlyRate?.ToString("N0") + " ریال";
        var dailyPrice = rate?.DailyRate?.ToString("N0") + " ریال";
        var servicePrice = rate?.ServiceRate?.ToString("N0") + " ریال";

        message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}";
        message += $"{TelegramValues.TruckIcon}<b>درخواست ماشین آلات : {fixAssetMachinery?.Machinery.MachineryName}</b>  {Environment.NewLine}{Environment.NewLine}" +
                   $"<b>شماره درخواست :</b> {requestMachinery?.RequestNumber} {Environment.NewLine}" +
                   $"<b>مرکز هزینه :</b> {requestMachinery?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName} {Environment.NewLine}" +
                   $"<b>پروژه :</b> {requestMachinery?.Project.ProjectName} {Environment.NewLine}" +
                   $"<b>پیمانکار :</b> {(string.IsNullOrEmpty(contractor) ? "در مالکیت شرکت" : contractor)} {Environment.NewLine}" +
                   $"<b>از تاریخ :</b> {fromDateShamsi} {Environment.NewLine}" +
                   $"<b>تا تاریخ :</b> {toDateShamsi} {Environment.NewLine}" +
                   $"<b>شروع رزرو :</b> {startDateShamsi} {Environment.NewLine}" +
                   $"<b>پایان رزرو :</b> {endDateShamsi} {Environment.NewLine}" +
                   $"<b>زمان مورد نیاز :</b> {requestMachinery?.TimeRequired} {Environment.NewLine}" +
                   $"<b>واحد :</b> {requestMachinery?.Unit.GetEnumDescription()} {Environment.NewLine}" +
                   $"<b>نرخ ساعتی :</b> {hourlyPrice} {Environment.NewLine}" +
                   $"<b>نرخ روزانه :</b> {dailyPrice} {Environment.NewLine}" +
                   $"<b>نرخ سرویسی :</b> {servicePrice} {Environment.NewLine}" +
                   $"<b>تایید کننده :</b> {confirmer} {Environment.NewLine}" +
                   $"<b>درخواست دهنده :</b> {creator} {Environment.NewLine}" +
                   $"<b>تاریخ و ساعت درخواست :</b> {createTime} {createDateShamsi} {Environment.NewLine}" +
                   $"<b>توضیحات :</b> {requestMachinery?.Description} {Environment.NewLine}";

        return message;
    }
}


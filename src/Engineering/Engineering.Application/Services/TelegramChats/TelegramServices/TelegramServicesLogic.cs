using Engineering.Application.Configs;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.TelegramChats.Models.CommercialPackingTelegramMessage;
using Engineering.Application.Services.TelegramChats.TelegramServices.Models;
using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Application.Services.TelegramMessageHistorys.Models.Create;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;
using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.Messengers;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.TelegramChats.Enums;
using System.Net;

namespace Engineering.Application.Services.TelegramChats.TelegramServices;

public static class TelegramServicesLogic
{
    public static async Task<string?> SendMessage(
        MessengerChannel messengerChannel,
        string message,
        List<Guid>? urls,
        IMediator _mediator,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
        var files = fileResponse?.Files;

        var (responseTele, errorMessage) = await SendTelegramCore(
            messengerChannel.ChatId, message, files, authorization, messageSenderConfig, true, ct);

        if (files is not null && files.Count > 0)
            FileDeleter(files);

        messengerChannel.AddHistory(message, errorMessage, urls.Count > 0 ? string.Join(",", urls) : string.Empty, responseTele != null ? true : false);

        return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;

    }

    public static async Task<(HttpResponseMessage? response, string errorMessage)> SendTelegramCore(
        string chatId,
        string message,
        List<DownloadMultipleFileStreamsModel>? files,
        string? authorization,
        MessageSenderConfig config,
        bool useAltBot,
        CT ct)
    {
        using var client = new HttpClient();
        using var formData = new MultipartFormDataContent();

        if (useAltBot)
        {
            client.DefaultRequestHeaders.Add(TelegramValues.APIName2, authorization);

            formData.Add(new StringContent(chatId), "receiver");
            formData.Add(new StringContent(message), "content");
            formData.Add(new StringContent("2"), "type");
            formData.Add(new StringContent("1"), "priority");
        }
        else
        {
            client.DefaultRequestHeaders.Add(TelegramValues.APIName, TelegramValues.APIKEY);

            formData.Add(new StringContent(chatId), "ChatId");
            formData.Add(new StringContent("Gita"), "Title");
            formData.Add(new StringContent("1"), "priority");
            formData.Add(new StringContent(message), "Body");
        }

        if (files != null && files.Any())
            foreach (var file in files)
            {
                if (file?.Content != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream(file.Content))
                    {
                        using (FileStream fileStream = new FileStream(file.FileName, FileMode.OpenOrCreate,
                                   FileAccess.Write))
                        {
                            memoryStream.WriteTo(fileStream);
                        }

                        StreamContent streamContent = new StreamContent(new MemoryStream(file.Content));
                        formData.Add(streamContent, "Files", file.FileName);
                    }
                }
                //if (file?.Content != null)
                //{
                //    var streamContent = new StreamContent(new MemoryStream(file.Content));
                //    formData.Add(streamContent, "Files", file.FileName);
                //}
            }

        if (useAltBot)
        {
            var response = await client.PostAsync(config.Server + "/message/send", formData, ct);
            var errorMessage = response.StatusCode != HttpStatusCode.OK
                ? $"Telegram API Error: {response.StatusCode} - {response.ReasonPhrase}"
                : string.Empty;

            return (response, errorMessage);
        }
        else
        {
            var response = await client.PostAsync($"{TelegramValues.BotUrl}", formData);
            var errorMessage = response.StatusCode != HttpStatusCode.OK
                ? $"Telegram API Error: {response.StatusCode} - {response.ReasonPhrase}"
                : string.Empty;

            return (response, errorMessage);
        }

    }

    public static async Task SaveHistory(
        ITelegramMessageHistoryLogic logic,
        long chatId,
        string message,
        string error,
        List<Guid>? urls,
        TelegramMessageType type,
        string receiver,
        bool isSuccess,
        CT ct)
    {
        var fileUrls = urls != null ? string.Join(",", urls) : string.Empty;
        await logic.Create(new CreateRequest(
            chatId,
            message,
            error,
            fileUrls,
            type,
            receiver,
            isSuccess), ct);
    }

    //تست ارسال پیام    
    public static async Task<bool?> SendTestMessage(
        string chatId,
        string? messageTest,
        string? createDate,
        string? createTime,
        IMediator mediator,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var config = await mediator.Send(new GetActiveConfigQuery(), ct);
            if (config.IsBad() || config.Value is null || !config.Value.SendTelegramMessage)
                return false;

            message =
                $"<b>سیستم ERP تاپ تک</b>{Environment.NewLine}" +
                $"<b>جهت تست ارسال پیام</b>{Environment.NewLine}{Environment.NewLine}" +
                $"{messageTest}{Environment.NewLine}" +
                $"<b>تاریخ:</b> {createDate} {createTime}{Environment.NewLine}";

            var (response, errorMessage) = await SendTelegramCore(
                chatId, message, null, authorization, messageSenderConfig, false, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, null, TelegramMessageType.Test, chatId,
                response?.StatusCode == HttpStatusCode.OK, ct);

            return response?.StatusCode == HttpStatusCode.OK;
        }
        catch
        {
            return false;
        }
    }


    //ثبت و ویرایش کارکرد روزانه
    public static async Task<string?> DailyMessage(
        string chatId,
        string? costCenterName,
        string? projectName,
        string? projectOperationName,
        string? projectOperationDetailName,
        string? description,
        decimal finalAmount,
        string? unit,
        string? createDate,
        string? startDate,
        string? createTime,
        string? creator,
        List<Guid>? urls,
        bool isUpdate,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            message = $"<b>سیستم گیتا</b> {Environment.NewLine}";
            message += isUpdate
                ? $"{TelegramValues.EditIcon}<b>ویرایش کارکرد روزانه</b>{Environment.NewLine}{Environment.NewLine}"
                : $"{TelegramValues.AddIcon}<b>ثبت کارکرد روزانه</b>{Environment.NewLine}{Environment.NewLine}";

            message += $"<b>مرکز هزینه :</b> {costCenterName} {Environment.NewLine}" +
                       $"<b>پروژه :</b> {projectName} {Environment.NewLine}" +
                       $"<b>شرح عملیات :</b> {projectOperationName} {Environment.NewLine}" +
                       $"<b>موقعیت :</b> {projectOperationDetailName} {Environment.NewLine}" +
                       $"<b>توضیح :</b> {description} {Environment.NewLine}" +
                       $"<b>حجم :</b> {Math.Round(finalAmount, 5)} {unit} {Environment.NewLine}{Environment.NewLine}" +
                       $"{TelegramValues.CalendarIcon}<b>تاریخ کارکرد :</b> {startDate} {Environment.NewLine}" +
                       $"{TelegramValues.CalendarIcon}<b>تاریخ ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
                       $"{TelegramValues.UserIcon}<b>ثبت کننده :</b> {creator} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //درخواست ترابری
    public static async Task<string?> TransportationRequestMessage(
        string chatId,
        long? transportationRequestId,
        long? requestNumber,
        string? freightNumber,
        string? destinationCityName,
        string? startingCityName,
        string? destinationCityAddress,
        string? startingCityAddress,
        string? createDate,
        string? createTime,
        string? creator,
        string? confirmer,
        string? description,
        string? numberPlates,
        decimal? price,
        string? phoneNumber,
        string? carSpecifications,
        string? driverName,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            var priceWithComma = price is not null
                ? price.Value.ToString("N0") + " ریال"
                : null;

            message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}";
            message += $"{TelegramValues.TruckIcon}<b>تایید درخواست حمل و نقل باربری :</b>{Environment.NewLine}{Environment.NewLine}" +
                       $"<b>درخواست دهنده :</b> {creator} {Environment.NewLine}" +
                       $"<b>تاریخ و ساعت :</b> {createTime} {createDate} {Environment.NewLine}" +
                       $"<b>شماره بارنامه :</b> {freightNumber} {Environment.NewLine}" +
                       $"<b>شماره ترابری :</b> {requestNumber} {Environment.NewLine}" +
                       $"<b>تایید کننده درخواست :</b> {confirmer} {Environment.NewLine}" +
                       $"<b>مبلغ :</b> {priceWithComma} {Environment.NewLine}" +
                       $"<b>مبداء :</b> {startingCityName}: {startingCityAddress} {Environment.NewLine}" +
                       $"<b>مقصد :</b> {destinationCityName}: {destinationCityAddress} {Environment.NewLine}" +
                       $"<b>مشخصات راننده:</b> {carSpecifications} - {numberPlates} - {driverName} - {phoneNumber} {Environment.NewLine}" +
                       $"<b>توضیحات :</b> {description} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //درخواست ترابری
    public static async Task<string?> PaidTransportationRequestMessage(
        string chatId,
        long? transportationRequestId,
        long? requestNumber,
        string? freightNumber,
        string? destinationCityName,
        string? startingCityName,
        string? destinationCityAddress,
        string? startingCityAddress,
        string? createDate,
        string? createTime,
        string? creator,
        string? confirmer,
        string? description,
        string? numberPlates,
        decimal? price,
        string? phoneNumber,
        string? carSpecifications,
        string? driverName,
        string? paymentDate,
        string? status,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            var priceWithComma = price is not null
                ? price.Value.ToString("N0") + " ریال"
                : null;

            message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}";
            message += $"{TelegramValues.TruckIcon}<b>درخواست پرداخت حمل و نقل باربری :</b>{Environment.NewLine}{Environment.NewLine}" +
                       $"<b>درخواست دهنده :</b> {creator} {Environment.NewLine}" +
                       $"<b>تاریخ و ساعت :</b> {createTime} {createDate} {Environment.NewLine}" +
                       $"<b>شماره بارنامه :</b> {freightNumber} {Environment.NewLine}" +
                       $"<b>شماره ترابری :</b> {requestNumber} {Environment.NewLine}" +
                       $"<b>پرداخت تایید:</b> {confirmer} {Environment.NewLine}" +
                       $"<b>مبلغ :</b> {priceWithComma} {Environment.NewLine}" +
                       $"<b>مبداء :</b> {startingCityName}: {startingCityAddress} {Environment.NewLine}" +
                       $"<b>مقصد :</b> {destinationCityName}: {destinationCityAddress} {Environment.NewLine}" +
                       $"<b>مشخصات راننده:</b> {carSpecifications} - {numberPlates} - {driverName} - {phoneNumber} {Environment.NewLine}" +
                       $"<b>تاریخ دستور پرداخت :</b> {paymentDate} {Environment.NewLine}" +
                       $"<b>وضعیت :</b> {status} {Environment.NewLine}" +
                       $"<b>توضیحات :</b> {description} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //اسنپ 
    public static async Task<string?> PaidSnapRequestMessage(
        string chatId,
        List<string?>? snapRequestNumber,
        string? createDate,
        string? createTime,
        string? PettyCash,
        string? confirmer,
        decimal? price,
        string? paymentDate,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            var priceWithComma = price is not null ? price.Value.ToString("N0") + " ریال" : null;

            message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}";
            message += $"{TelegramValues.TruckIcon}<b>درخواست پرداخت حمل و نقل اسنپ :</b>  {Environment.NewLine}{Environment.NewLine}" +
                       $"<b>تنخواه گردان :</b> {PettyCash} {Environment.NewLine}" +
                       $"<b>تاریخ و ساعت :</b> {createTime} {createDate} {Environment.NewLine}" +
                       $"<b> شماره درخواست های ترابری :</b> {string.Join("-", snapRequestNumber ?? [])} {Environment.NewLine}" +
                       $"<b>صادر کننده دستور پرداخت:</b> {confirmer} {Environment.NewLine}" +
                       $"<b>مبلغ :</b> {priceWithComma} {Environment.NewLine}" +
                       $"<b>تاریخ دستور پرداخت :</b> {paymentDate} {Environment.NewLine}";


            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //درخواست ماشین آلات
    public static async Task<string?> RequestMachineryMessage(
        string chatId,
        RequestMachinery? requestMachinery,
        RequestMachineryInquiry? requestMachineryInquiry,
        string? contractor,
        string? fromDate,
        string? toDate,
        string? createDate,
        string? createTime,
        string? confirmer,
        string? creator,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            var priceWithComma = requestMachineryInquiry?.TotalPrice is not null
                ? requestMachineryInquiry.TotalPrice.ToString("N0") + " ریال"
                : null;

            message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}";
            message += $"{TelegramValues.TruckIcon}<b>درخواست ماشین آلات : {requestMachinery?.Machinery.MachineryName}</b>  {Environment.NewLine}{Environment.NewLine}" +
                       $"<b>شماره درخواست :</b> {requestMachinery?.RequestNumber} {Environment.NewLine}" +
                       $"<b>مرکز هزینه :</b> {requestMachinery?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName} {Environment.NewLine}" +
                       $"<b>پروژه :</b> {requestMachinery?.Project.ProjectName} {Environment.NewLine}" +
                       $"<b>پیمانکار :</b> {contractor} {Environment.NewLine}" +
                       $"<b>مبلغ :</b> {priceWithComma} {Environment.NewLine}" +
                       $"<b>از تاریخ :</b> {fromDate} {Environment.NewLine}" +
                       $"<b>تا تاریخ :</b> {toDate} {Environment.NewLine}" +
                       $"<b>زمان مورد نیاز :</b> {requestMachinery?.TimeRequired} {Environment.NewLine}" +
                       $"<b>واحد :</b> {requestMachinery?.Unit.GetEnumDescription()} {Environment.NewLine}" +
                       $"<b>تایید کننده :</b> {confirmer} {Environment.NewLine}" +
                       $"<b>درخواست دهنده :</b> {creator} {Environment.NewLine}" +
                       $"<b>تاریخ و ساعت درخواست :</b> {createTime} {createDate} {Environment.NewLine}" +
                       $"<b>توضیحات :</b> {requestMachinery?.Description} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //درخواست ماشین آلات
    public static async Task<string?> FixAssetMachineryMessage(
        string chatId,
        RequestMachinery? requestMachinery,
        FixAssetMachinery? fixAssetMachinery,
        string? contractor,
        string? fromDate,
        string? startDate,
        string? toDate,
        string? endDate,
        string? createDate,
        string? createTime,
        string? confirmer,
        string? creator,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

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
                       $"<b>از تاریخ :</b> {fromDate} {Environment.NewLine}" +
                       $"<b>تا تاریخ :</b> {toDate} {Environment.NewLine}" +
                       $"<b>شروع رزرو :</b> {startDate} {Environment.NewLine}" +
                       $"<b>پایان رزرو :</b> {endDate} {Environment.NewLine}" +
                       $"<b>زمان مورد نیاز :</b> {requestMachinery?.TimeRequired} {Environment.NewLine}" +
                       $"<b>واحد :</b> {requestMachinery?.Unit.GetEnumDescription()} {Environment.NewLine}" +
                       $"<b>نرخ ساعتی :</b> {hourlyPrice} {Environment.NewLine}" +
                       $"<b>نرخ روزانه :</b> {dailyPrice} {Environment.NewLine}" +
                       $"<b>نرخ سرویسی :</b> {servicePrice} {Environment.NewLine}" +
                       $"<b>تایید کننده :</b> {confirmer} {Environment.NewLine}" +
                       $"<b>درخواست دهنده :</b> {creator} {Environment.NewLine}" +
                       $"<b>تاریخ و ساعت درخواست :</b> {createTime} {createDate} {Environment.NewLine}" +
                       $"<b>توضیحات :</b> {requestMachinery?.Description} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //تغییر وضعیت به رد یا برگشت درخواست تامین
    public static async Task<string?> GoodsSupplyStatusChangerMessage(
        string chatId,
        GoodsSupplyDetailStatus? status,
        string? requestSerialNumber,
        string? costCenterName,
        string? projectName,
        List<ProductModel>? productModel,
        string? description,
        string? createDate,
        string? createTime,
        string? creator,
        string? creatorTelegramId,
        string? creatorRequest,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                      $"{TelegramValues.SendBackIcon}<b>تغییر وضعیت درخواست</b>{Environment.NewLine}{Environment.NewLine}" +
                      $"<b>درخواست دهنده:</b> {creatorRequest} {Environment.NewLine}" +
                      $"وضعیت درخواست شما با شماره <b>{requestSerialNumber}</b> به <b>{status.GetEnumDescription()}</b> تغییر کرد" +
                      $"{Environment.NewLine}<b>مرکز هزینه:</b> {costCenterName} {Environment.NewLine}" +
                      $"<b>پروژه:</b> {projectName} {Environment.NewLine}{Environment.NewLine}" +
                      $"{TelegramValues.Icon13}<b>لیست کالاها:</b>{Environment.NewLine}";

            if (productModel != null && productModel.Any())
            {
                foreach (var product in productModel)
                {
                    message += $"<b>نام کالا:</b> {product.ProductName} {Environment.NewLine}" +
                               $"<b>کد:</b> {product.ProductCode} {Environment.NewLine}" +
                               $"<b>تعداد:</b> {product.RequestedCount} {Environment.NewLine}{Environment.NewLine}";
                }
            }

            message += $"<b>علت {status.GetEnumDescription()}:</b> {description} {Environment.NewLine}" +
                       $"{TelegramValues.CalendarIcon}<b>تاریخ :</b> {createTime} {createDate} {Environment.NewLine}" +
                       $"{TelegramValues.UserIcon}<b>کاربر بررسی کننده درخواست :</b> {creator} {Environment.NewLine}" +
                       $"{creatorTelegramId}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, null, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, null, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //تغییر وضعیت درخواست بازرگانی
    public static async Task<string?> CommerceRequestWarehouseStatusChangeMessage(
        string chatId,
        string? status,
        long? requestNumber,
        string? costCenterName,
        string? projectName,
        string? description,
        string? createDate,
        string? createTime,
        string? creatorChange,
        string? creatorTelegramId,
        string? creatorRequest,
        string? productName,
        string? productCode,
        decimal? requestedCount,
        string? commerceRequestType,
        string? commercialRequestNumber,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        IMediator _mediator,
        long telegramchatId,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var commercialNumber = !string.IsNullOrEmpty(commercialRequestNumber)
                ? $"{Environment.NewLine}<b>شماره درخواست تامین:</b> {commercialRequestNumber} {Environment.NewLine}"
                : string.Empty;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.SendBackIcon}<b>تغییر وضعیت درخواست {commerceRequestType}</b>{Environment.NewLine}{Environment.NewLine}" +
                $"<b>درخواست دهنده:</b> {creatorRequest} {Environment.NewLine}" +
                $"وضعیت درخواست {commerceRequestType} شما با شماره <b>{requestNumber}</b> به <b>{status}</b> تغییر کرد" +
                commercialNumber +
                $"{Environment.NewLine}<b>مرکز هزینه:</b> {costCenterName} {Environment.NewLine}" +
                $"<b>پروژه:</b> {projectName} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها:</b>{Environment.NewLine}" +
                $"<b>نام کالا:</b> {productName} {Environment.NewLine}" +
                $"<b>کد:</b> {productCode} {Environment.NewLine}" +
                $"<b>تعداد:</b> {requestedCount} {Environment.NewLine}{Environment.NewLine}" +
                $"<b>علت {status}:</b> {description} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ :</b> {createTime} {createDate} {Environment.NewLine}" +
                $"{TelegramValues.UserIcon}<b>کاربر بررسی کننده درخواست: </b> {creatorChange} {Environment.NewLine}" +
                $"{creatorTelegramId}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, null, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, null, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //خروج مصرفی
    public static async Task<string?> ConsumerExitMessage(
        string chatId,
        string? warehouseName,
        string? documentNumber,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? thirdPartyName,
        string? receiverDelivery,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? exitDate,
        string? exitTime,
        string? creator,
        string? confirmCreator,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon7}<b>خروج مصرفی کالا از انبار {warehouseName} </b> {Environment.NewLine}{Environment.NewLine}" +
                $"<b>شماره سند : </b> {documentNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت خروج :</b> {exitTime}  {exitDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    message +=
                        $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
                }
            }

            message +=
                $"{Environment.NewLine}<b>طرف حساب :</b> {thirdPartyName} {Environment.NewLine}" +
                $"<b>توضیحات تایید :</b> {receiverDelivery} {Environment.NewLine}" +
                $"<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
                $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    // ورود کالا
    public static async Task<string?> EnteringToWarehouseMessage(
        string chatId,
        string? warehouseName,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? receiverDelivery,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? enterDate,
        string? enterTime,
        string? creator,
        string? confirmCreator,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon6}<b>ورود کالا به انبار {warehouseName} </b> {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ورود :</b>{enterTime} {enterDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

            if (products != null && products.Any())
                foreach (var product in products)
                {
                    message +=
                        $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} ({product.Status}) {Environment.NewLine}";
                }

            message +=
                $"{Environment.NewLine}<b>توضیحات تحویل :</b> {receiverDelivery} {Environment.NewLine}" +
                $"<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b>{createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
                $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //خروج بین انباری
    public static async Task<string?> ExitForRelocationMessage(
        string chatId,
        string? sourceWarehouseName,
        string? destinationWarehouseName,
        string? documentNumber,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? exitDate,
        string? exitTime,
        string? creator,
        string? confirmCreator,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon7}<b>خروج بین انباری</b> {Environment.NewLine}{Environment.NewLine}" +
                $"<b>انبار مبدا :</b> {sourceWarehouseName} {Environment.NewLine}" +
                $"<b>انبار مقصد :</b> {destinationWarehouseName} {Environment.NewLine}" +
                $"<b>شماره سند :</b> {documentNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت خروج :</b> {exitTime} {exitDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

            if (products?.Any() == true)
            {
                foreach (var product in products)
                {
                    message +=
                        $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
                }
            }

            message +=
                $"{Environment.NewLine}<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
                $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //تحویل موقت
    public static async Task<string?> TemporaryDeliveryMessage(
        string chatId,
        string? warehouseName,
        string? destinationWarehouse,
        List<ProductInvoiceForTelegramModel>? products,
        string? receiverDelivery,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? enterDate,
        string? enterTime,
        string? creator,
        string? confirmCreator,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon6}<b> ورود کالا به انبار تحویل موقت {warehouseName} </b> {Environment.NewLine}{Environment.NewLine}" +
                $"<b>انبار هدف :</b> {destinationWarehouse} {Environment.NewLine}" +
                $"<b>تاریخ ورود :</b> {enterDate} {Environment.NewLine}" +
                $"<b>ساعت ورود :</b> {enterTime} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

            if (products?.Any() == true)
            {
                foreach (var product in products)
                {
                    message +=
                        $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
                }
            }

            message +=
                $"{Environment.NewLine}<b>توضیحات تحویل :</b> {receiverDelivery} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
                $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //پرداخت بازرگانی
    public static async Task<string?> CommercialPaymentMessage(
        string chatId,
        string? documentNumber,
        string? shabaNo,
        decimal? amount,
        string? createDate,
        string? createTime,
        string? reason,
        string? description,
        string? thirdPartyName,
        List<Guid>? urls,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, null, ct);
            var files = fileResponse?.Files;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"<b>ارسال دستور پرداخت به سیستم ساتک</b> {Environment.NewLine}" +
                $"<b>مرکزهزینه : </b> {reason} {Environment.NewLine}" +
                $"<b>طرف حساب :</b> {thirdPartyName} {Environment.NewLine}" +
                $"<b>مبلغ : </b> {amount} {Environment.NewLine}" +
                $"<b>تاریخ :</b> {createDate} {createTime} {Environment.NewLine}" +
                $"<b>توضیحات :</b> {description} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //پکینگ بازرگانی
    public static async Task<string?> CommercialPackingMessage(
         bool isUpdated,
         string chatId,
         string? supplierName,
         string? thirdParty,
         string? followupName,
         string? destinationWarehouseName,
         long? requestNumber,
         List<WarehouseProductGroupModel>? products,
         string? description,
         string? createDate,
         string? createTime,
         string? deliveryDate,
         string? deliveryTime,
         List<Guid>? urls,
         List<string>? telegramIds,
         ExcelFileModel? excelFile,
         ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
         long telegramchatId,
         string? fileUrls,
         TelegramMessageType type,
         string? authorization,
         MessageSenderConfig messageSenderConfig,
         CT ct)
    {
        string message = string.Empty;
        string excelFileBase64 = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, null, ct);
            var files = fileResponse?.Files;

            string formattedCreateDate = createDate?.Replace("/", "") ?? string.Empty;
            string formattedDeliveryDate = deliveryDate?.Replace("/", "") ?? string.Empty;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.BoxEmoji} <b> مدیریت پکینگ</b> {Environment.NewLine}";

            if (isUpdated)
                message += $"<b> پکینگ اصلاحی</b>{Environment.NewLine}";

            message +=
                $"<b>شماره پکینگ :</b> {requestNumber}{Environment.NewLine}" +
                $"<b>انبار تحویل گیرنده :</b> {destinationWarehouseName}{Environment.NewLine}{Environment.NewLine}" +
                $"#Date_{formattedCreateDate} {Environment.NewLine}" +
                $"#DeliveryDate_{formattedDeliveryDate} {Environment.NewLine}{Environment.NewLine}";

            if (products != null && products.Any())
            {
                int counter = 1;

                foreach (var group in products)
                {
                    message += $"انبار:{group.WarehouseName} - مرکزهزینه : {group.CostCenterName}{Environment.NewLine}";

                    if (group.Products != null)
                    {
                        foreach (var product in group.Products)
                        {
                            message +=
                                $"<b>{counter}- شماره درخواست ({product.RequestNumber})</b>{Environment.NewLine}" +
                                $"{TelegramValues.BulletPoint} {product.ProductName} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";

                            counter++;
                        }
                    }

                    message += Environment.NewLine;
                }
            }

            message +=
                $"<b>رابط شرکت :</b>{supplierName} {Environment.NewLine}" +
                $"<b>فروشگاه :</b>{thirdParty} {Environment.NewLine}" +
                $"<b>مسئول خرید :</b>{followupName} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.MemoIcon} <b>توضیحات :</b>{description} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ ارسال :</b> {deliveryDate} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}";

            if (telegramIds != null && telegramIds.Any())
                message += string.Join(" ", telegramIds);

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //تایید دستور پرداخت در خزانه داری
    public static async Task<string?> AcceptPaymentOrderTelegramMessage(
        string chatId,
        string? referenceDetails,
        string? createDate,
        string? createTime,
        string? creator,
        List<Guid>? urls,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, null, ct);
            var files = fileResponse?.Files;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"<b>تایید دستور پرداخت</b> {Environment.NewLine}{Environment.NewLine}" +
                $"{referenceDetails} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ تایید :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده :</b> {creator} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //  پرداخت از نوع دستور پرداخت صورت وضعیت پیمانکار
    public static async Task<string?> ContractorStatementPaymentMessage(
        string chatId,
        string? date,
        string? time,
        string? number,
        string? paymentOrderNumber,
        string? thirdParty,
        string? description,
        string? defaultDescription,
        string? shabaNo,
        decimal? amount,
        List<Guid>? urls,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, null, ct);
            var files = fileResponse?.Files;

            var thirdPartyTag = thirdParty is not null ? AddHashTags(thirdParty) : string.Empty;
            var shabaNoTag = shabaNo is not null ? AddHashTags(shabaNo) : string.Empty;
            string formattedDate = date?.Replace("/", "") ?? string.Empty;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"<b>پرداخت صورت وضعیت پیمانکار</b> {Environment.NewLine}{Environment.NewLine}" +
                $"#Date_{formattedDate} {Environment.NewLine}" +
                $"<b>طرف حساب :</b> {thirdParty} {thirdPartyTag} {Environment.NewLine}" +
                $"<b>شماره شبا :</b> {shabaNoTag} {Environment.NewLine}" +
                $"<b>شماره سند (شماره دستور پرداخت):</b> #PaymentOrderNumber_{paymentOrderNumber} {Environment.NewLine}" +
                $"<b>شماره پرداخت :</b> #Number_{number} {Environment.NewLine}" +
                $"<b>مبلغ :</b> {amount:#,##0.##} {Environment.NewLine}" +
                $"<b>تاریخ :</b> {time} {date} {Environment.NewLine}" +
                $"<b>بابت :</b> {defaultDescription} {Environment.NewLine}" +
                $"<b>توضیحات :</b> {description} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //پرداخت خزانه داری
    public static async Task<string?> PaymentTreasuryTelegramMessage(
        string chatId,
        string? date,
        string? time,
        string? number,
        string? paymentOrderNumber,
        string? thirdParty,
        string? description,
        string? defaultDescription,
        string? shabaNo,
        decimal? amount,
        List<Guid>? urls,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, null, ct);
            var files = fileResponse?.Files;

            var thirdPartyTag = thirdParty is not null ? AddHashTags(thirdParty) : string.Empty;
            var shabaNoTag = shabaNo is not null ? AddHashTags(shabaNo) : string.Empty;
            string formattedDate = date?.Replace("/", "") ?? string.Empty;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"<b>پرداخت</b> {Environment.NewLine}{Environment.NewLine}" +
                $"#Date_{formattedDate} {Environment.NewLine}" +
                $"<b>طرف حساب :</b> {thirdParty} {thirdPartyTag} {Environment.NewLine}" +
                $"<b>شماره شبا :</b> {shabaNoTag} {Environment.NewLine}" +
                $"<b>شماره سند (شماره دستور پرداخت):</b> #PaymentOrderNumber_{paymentOrderNumber} {Environment.NewLine}" +
                $"<b>شماره پرداخت :</b> #Number_{number} {Environment.NewLine}" +
                $"<b>مبلغ :</b> {amount:#,##0.##} {Environment.NewLine}" +
                $"<b>تاریخ :</b> {time} {date} {Environment.NewLine}" +
                $"<b>بابت :</b> {defaultDescription} {Environment.NewLine}" +
                $"<b>توضیحات :</b> {description} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //خروج بین انباری برای تحویل موقت
    public static async Task<string?> ExitRelocationForTemporaryDeliveryMessage(
        string chatId,
        string? sourceWarehouseName,
        string? destinationWarehouseName,
        string? documentNumber,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? exitDate,
        string? exitTime,
        string? creator,
        string? confirmCreator,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon7}<b>خروج بین انباری برای تحویل موقت</b> {Environment.NewLine}{Environment.NewLine}" +
                $"<b>انبار مبدا :</b> {sourceWarehouseName} {Environment.NewLine}" +
                $"<b>انبار مقصد :</b> {destinationWarehouseName} {Environment.NewLine}" +
                $"<b>شماره سند :</b> {documentNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت خروج :</b> {exitTime} {exitDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    message +=
                        $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
                }
            }

            message +=
                $"{Environment.NewLine}<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
                $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //ورود بین انباری برای تحویل موقت
    public static async Task<string?> EntryThroughRelocationForTemporaryDeliveryMessage(
        string chatId,
        string? sourceWarehouseName,
        string? destinationWarehouseName,
        string? documentNumber,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? exitDate,
        string? exitTime,
        string? creator,
        string? confirmCreator,
        List<Guid>? urls,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
            var files = fileResponse?.Files;

            message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon7}<b>ورود بین انباری برای تحویل موقت</b> {Environment.NewLine}{Environment.NewLine}" +
                $"<b>انبار مبدا :</b> {sourceWarehouseName} {Environment.NewLine}" +
                $"<b>انبار مقصد :</b> {destinationWarehouseName} {Environment.NewLine}" +
                $"<b>شماره سند :</b> {documentNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ورود :</b> {exitTime} {exitDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    message +=
                        $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
                }
            }

            message +=
                $"{Environment.NewLine}<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
                $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, files, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, urls, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            if (files != null)
                FileDeleter(files);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //پرداخت خزانه داری
    public static async Task<string?> UserChangedTelegramMessage(
        string chatId,
        string? date,
        string? time,
        string? description,
        string? creator,
        IMediator mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        TelegramMessageType type,
        string? authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            message =
                $"<b>اپراتور :</b> {creator}{Environment.NewLine}" +
                $"<b>تاریخ :</b> {time} {date} {Environment.NewLine}" +
                $"{description}{Environment.NewLine}";

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, null, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, null, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    //ارسال مجدد پیام های ارسال نشده
    public static async Task<string?> ResendMessage(
        string chatId,
        string message,
        List<Guid>? urls,
        IMediator _mediator,
        string authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        using (var client = new HttpClient())
        {
            var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
            if (!config.IsBad() &&
            (config.Value is not null && config.Value!.SendTelegramMessage))
            {
                client.DefaultRequestHeaders.Add(TelegramValues.APIName2, authorization);
                //client.DefaultRequestHeaders.Add(TelegramValues.APIName, TelegramValues.APIKEY);

                var fileResponse = await WebServicesLogic.DownloadMultipleFileStream(urls, _mediator, ct);
                var files = fileResponse?.Files;

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(chatId), "receiver");
                    formData.Add(new StringContent(message), "content");

                    //formData.Add(new StringContent("1"), "priority");
                    formData.Add(new StringContent("2"), "type");

                    if (files is not null && files.Any())
                    {
                        foreach (var file in files)
                            if (file is not null && file.Content is not null)
                                using (MemoryStream memoryStream = new MemoryStream(file.Content))
                                {
                                    using (FileStream fileStream = new FileStream(file.FileName, FileMode.OpenOrCreate,
                                               FileAccess.Write))
                                    {
                                        memoryStream.WriteTo(fileStream);
                                    }

                                    StreamContent streamContent = new StreamContent(new MemoryStream(file.Content));
                                    formData.Add(streamContent, "Files", file.FileName);
                                }

                        var responseTele = await client.PostAsync(messageSenderConfig.Server + "/message/send", formData);
                        //var responseTele = await client.PostAsync($"{TelegramValues.BotUrl}", formData);
                        FileDeleter(files);
                        return await responseTele.Content.ReadAsStringAsync(ct);
                    }
                    else
                    {
                        var responseTele = await client.PostAsync(messageSenderConfig.Server + "/message/send", formData);
                        //var responseTele = await client.PostAsync($"{TelegramValues.BotUrl}", formData);
                        if (responseTele.StatusCode != HttpStatusCode.OK)
                            return null;

                        return await responseTele.Content.ReadAsStringAsync();
                    }
                }
            }
            return string.Empty;
        }
    }

    public static async Task<string?> WeatherConditionMessage(
        string chatId,
        string? cityName,
        string? dateTime,
        WeatherConditionType weatherType,
        Double averageTemperature,
        Double averageHumidity,
        WeatherConditionType nexDatWeatherType,
        IMediator _mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        string? fileUrls,
        TelegramMessageType type,
        string authorization,
        MessageSenderConfig messageSenderConfig,
        CT ct)
    {
        using (var client = new HttpClient())
        {
            var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
            if (!config.IsBad() &&
            (config.Value is not null && config.Value!.SendTelegramMessage))
            {
                client.DefaultRequestHeaders.Add(TelegramValues.APIName, authorization);
                //client.DefaultRequestHeaders.Add(TelegramValues.APIName, TelegramValues.APIKEY);

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(chatId), "receiver");
                    //
                    //formData.Add(new StringContent(chatId), "ChatId");
                    //formData.Add(new StringContent("Gita"), "Title");

                    var message =
                            $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                            $"{TelegramValues.Icon14} <b>{TelegramMessageType.WeatherCondition.GetEnumDescription()}</b> {Environment.NewLine}" +
                            $"<b>شهر:</b> {cityName} {Environment.NewLine}" +
                            $"<b>تاریخ:</b> {dateTime} {Environment.NewLine}" +
                            $"<b>وضعیت:</b> {weatherType.GetEnumDescription()} {Environment.NewLine}" +
                            $"<b>میانگین دما:</b> {averageTemperature} {Environment.NewLine}" +
                            $"<b>میانگین رطوبت:</b> {averageHumidity} {Environment.NewLine}" +
                            $"<b>پیش بینی هوای فردا:</b> {nexDatWeatherType.GetEnumDescription()} {Environment.NewLine}"
                        ;

                    formData.Add(new StringContent(message), "content");
                    //formData.Add(new StringContent("1"), "priority");
                    formData.Add(new StringContent("2"), "type");
                    //
                    //formData.Add(new StringContent(message), "Body");
                    formData.Add(new StringContent("1"), "priority");

                    string fileName = "WeatherCondition.jpg";
                    var files = await WebServicesLogic.DownloadMultipleStaticFilesByNameStream([fileName], _mediator,
                        ct);
                    var file = files?.Files.FirstOrDefault()?.Content;
                    if (file is not null)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(file))
                        {
                            using (FileStream fileStream = new FileStream("WeatherCondition.jpg", FileMode.OpenOrCreate,
                                       FileAccess.Write))
                            {
                                memoryStream.WriteTo(fileStream);
                                StreamContent streamContent = new StreamContent(memoryStream);
                                formData.Add(streamContent, "Files", fileName);

                                var responseTele = await client.PostAsync(messageSenderConfig.Server + "/message/send", formData);
                                //var responseTele = await client.PostAsync($"{TelegramValues.BotUrl}", formData);
                                fileStream.Close();
                                File.Delete("WeatherCondition.jpg");
                                return await responseTele.Content.ReadAsStringAsync(ct);
                            }
                        }
                    }
                    else
                    {
                        var responseTele = await client.PostAsync(messageSenderConfig.Server + "/message/send", formData);
                        //var responseTele = await client.PostAsync($"{TelegramValues.BotUrl}", formData);
                        return await responseTele.Content.ReadAsStringAsync(ct);
                    }
                }
            }
            return string.Empty;
        }
    }

    public static async Task<string?> TrafficOfPeopleMessage(
        string chatId,
        string? projectName,
        string? timeLine,
        string? dateTime,
        string authorization,
        MessageSenderConfig messageSenderConfig,
        IMediator mediator,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        long telegramchatId,
        TelegramMessageType type,
        List<string>? peoples,
        CT ct)
    {
        string message = string.Empty;

        try
        {
            var config = await mediator.Send(new GetActiveConfigQuery(), ct);
            if (config.IsBad() || config.Value is null || !config.Value.SendTelegramMessage)
                return string.Empty;

            var messageBuilder =
                $"<b>سیستم ERP تاپ تک</b>{Environment.NewLine}" +
                $"{TelegramValues.Icon14} <b>گزارش تردد افراد</b>{Environment.NewLine}{Environment.NewLine}" +
                $"<b>پروژه:</b> {projectName}{Environment.NewLine}" +
                $"<b>گزارش تردد:</b> {timeLine}{Environment.NewLine}" +
                $"<b>تاریخ:</b> {dateTime}{Environment.NewLine}";

            if (peoples is not null && peoples.Any())
            {
                messageBuilder += $"{Environment.NewLine}<b>افراد:</b>{Environment.NewLine}";
                foreach (var p in peoples)
                    messageBuilder += $"{TelegramValues.BulletPoint} {p}{Environment.NewLine}";
            }

            message = messageBuilder;

            var (responseTele, errorMessage) = await SendTelegramCore(
                chatId, message, null, authorization, messageSenderConfig, true, ct);

            await SaveHistory(
                telegramMessageHistoryLogic,
                telegramchatId, message, errorMessage, null, type, chatId,
                responseTele?.StatusCode == HttpStatusCode.OK, ct);

            return responseTele != null ? await responseTele.Content.ReadAsStringAsync() : null;
        }
        catch (Exception ex)
        {
            return await HandleTelegramException<string?>(
                ex, message, telegramMessageHistoryLogic,
                telegramchatId, type, chatId, ct);
        }
    }

    private static bool FileDeleter(List<DownloadMultipleFileStreamsModel> files)
    {
        foreach (var file in files)
            if (file is not null && file.Content is not null)
                File.Delete(file.FileName);

        return true;
    }

    private static string AddHashTags(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        return $"#{string.Join("_", input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))}";
    }

    private static async Task<T?> HandleTelegramException<T>(
        Exception ex,
        string message,
        ITelegramMessageHistoryLogic history,
        long telegramChatId,
        TelegramMessageType type,
        string chatId, CT ct)
    {
        var errorDetails = ex.InnerException != null
            ? $"{ex.Message} | Inner: {ex.InnerException.Message}"
            : ex.Message;

        var errorJson = JsonConvert.SerializeObject(new
        {
            ErrorMessage = errorDetails,
            ErrorType = ex.GetType().Name,
            StackTrace = ex.StackTrace?.Length > 500
                ? ex.StackTrace[..500]
                : ex.StackTrace,
            Time = DateTime.UtcNow
        });

        await SaveHistory(
            history,
            telegramChatId,
            message,
            errorJson,
            null,
            type,
            chatId,
            isSuccess: false,
            ct);

        return default;
    }
}
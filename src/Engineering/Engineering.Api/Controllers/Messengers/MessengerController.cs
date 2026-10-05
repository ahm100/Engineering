using Engineering.Api.Controllers.Messengers.Contracts.GetMessengerChannelHistoriesToExcel;
using Engineering.Api.Controllers.Messengers.Contracts.GetMessengerChannelsToExcel;
using Engineering.Api.Controllers.Messengers.Contracts.GetMessengersToExcel;
using Engineering.Api.Controllers.Messengers.Contracts.MessengerChannelHistoriesExel;
using Engineering.Api.Extensions.Enums;
using Engineering.Api.Helpers.ExcelTools;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.BillOfLadings.Contracts.ChangeBillOfLadingState;
using Engineering.Application.Services.GetFltrMessengerChannels.Contracts.GetFltrMessengerChannel;
using Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;
using Engineering.Application.Services.Messengers;
using Engineering.Application.Services.Messengers.Contracts.ChangeMessengerState;
using Engineering.Application.Services.Messengers.Contracts.CreateMessenger;
using Engineering.Application.Services.Messengers.Contracts.CreateMessengerChannel;
using Engineering.Application.Services.Messengers.Contracts.DeleteMessenger;
using Engineering.Application.Services.Messengers.Contracts.DeleteMessengerChannel;
using Engineering.Application.Services.Messengers.Contracts.GetMessengerById;
using Engineering.Application.Services.Messengers.Contracts.GetMessengers;
using Engineering.Application.Services.Messengers.Contracts.UpdateMessenger;
using Engineering.Application.Services.Messengers.Contracts.UpdateMessengerChannel;
using Engineering.Application.Services.TelegramChats.Models.ActiveTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.CreateTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.SendTestTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.StateChangerTelegramChats;
using Engineering.Application.Services.TelegramChats.Models.TelegramChatGroupDelete;
using Engineering.Application.Services.TelegramChats.Models.UpdateTelegramChat;
using Engineering.Domain.Entities.Messengers.Enums;
using System.ComponentModel;

namespace Engineering.Api.Controllers.Messengers;

[ApiController]
[Route("api/engineering/v1/messengers")]
public class MessengerController : ControllerBase
{
    private readonly IMessengerLogic _messLogic;
    private readonly ILogger<MessengerController> _logger;

    public MessengerController(
        IMessengerLogic messLogic, ILogger<MessengerController> logger)
    {
        _messLogic = messLogic;
        _logger = logger;
    }

    [HttpPost("CreateMessenger")]
    [Description("create Meessagger")]
    [ResponseSchema<CreateTelegramChatResponse>]
    public async Task<IResult> CreateMessenger(
        [FromBody] CreateMessengerRequest request, CT ct)
    {
        var result = await _messLogic.CreateMessenger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateMessenger")]
    [Description("update Meessagger")]
    [ResponseSchema<StateChangerTelegramChatsResponse>]
    public async Task<IResult> UpdateMessenger(
        [FromBody] UpdateMessengerRequest request, CT ct)
    {
        var result = await _messLogic.UpdateMessenger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteMessenger")]
    [Description("delete Meessagger")]
    [ResponseSchema<UpdateTelegramChatResponse>]
    public async Task<IResult> DeleteMessenger(
        [FromBody] DeleteMessengerRequest request, CT ct)
    {
        var result = await _messLogic.DeleteMessenger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMessengerById")]
    [Description("Get Meessagger by Id")]
    [ResponseSchema<GetMessengerByIdResponse>]
    public async Task<IResult> GetMessengerById(
        [FromQuery] GetMessengerByIdRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerById");
        var result = await _messLogic.GetMessengerById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetMessengers")]
    [Description("Get Filtered Messengers")]
    [ResponseSchema<GetMessengersResponse>]
    public async Task<IResult> GetMessengers(
        [FromBody] GetMessengersRequest request, CT ct)
    {
        _logger.LogInformation("GetMessengers");
        var result = await _messLogic.GetMessengers(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetMessengersEnumExcel")]
    [Description("Get enum values for messengerChannel histories enum excel filtering.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetMessengersEnumExcel(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengersEnumExcel");
        var result = EnumExtensions.GetEnums<MessengersEnumExcel>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetMessengersToExcel")]
    [Description("Export filtered MessengerChannelHistories to Excel.")]
    [ResponseSchema<GetMessengersResponse>]
    public async Task<IResult> GetMessengersToExcel(
        [FromBody] GetMessengersToExcelRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengersToExcel");
        var response = await _messLogic.GetMessengers(request.Adapt<GetMessengersRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetMessengersToExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, ""),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"MessengerChannelHistories-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetMessengersToExcelResponse?>(
            new(result)).GetHttpResponse();
    }

    [HttpPost("GetMessengerType")]
    [Description("Get enum values for  MessengerType filtering.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetMessengerType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerType");
        var result = EnumExtensions.GetEnums<MessengerType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetMessengerTargetType")]
    [Description("Get enum values for  Messenger Target Type filtering.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetMessengerTargetType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerType");
        var result = EnumExtensions.GetEnums<MessengerTargetType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("RetrySendMessage")]
    [Description("retry Send Message")]
    [ResponseSchema<GetMessengersRequest>]
    public async Task<IResult> RetrySendMessage(
        [FromBody] RetrySendMessageRequest request, CT ct)
    {
        _logger.LogInformation("RetrySendMessage");
        var result = await _messLogic.RetrySendMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendTestTelegramMessage")]
    [Description("retry Send Message")]
    [ResponseSchema<SendTestTelegramMessageResponse>]
    public async Task<IResult> SendTestTelegramMessage(
        [FromBody] SendTestTelegramMessageRequest request, CT ct)
    {
        var result = await _messLogic.SendTestMessage(request, ct);
        return result.GetHttpResponse();
    }


    [HttpPost("CreateMessengerChannel")]
    [Description("create Meessagger Channel")]
    [ResponseSchema<TelegramChatGroupDeleteResponse>]
    public async Task<IResult> CreateMessengerChannel(
        [FromBody] CreateMessengerChannelRequest request, CT ct)
    {
        _logger.LogInformation($"CreateMessengerChannel");
        var result = await _messLogic.CreateMessengerChannel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateMessengerChannel")]
    [Description("update Meessagger Channel")]
    [ResponseSchema<UpdateMessengerChannelResponse>]
    public async Task<IResult> UpdateMessengerChannel(
        [FromBody] UpdateMessengerChannelRequest request, CT ct)
    {
        _logger.LogInformation($"UpdateMessengerChannel");
        var result = await _messLogic.UpdateMessengerChannel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteMessengerChannel")]
    [ResponseSchema<ActiveTelegramChatResponse>]
    public async Task<IResult> DeleteMessengerChannel(
        [FromBody] DeleteMessengerChannelRequest request, CT ct)
    {
        _logger.LogInformation($"DeleteMessengerChannel");
        var result = await _messLogic.DeleteMessengerChannel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMessengerChannelById")]
    [Description("Get MessengerChannels ById")]
    [ResponseSchema<GetMessengerChannelByIdResponse>]
    public async Task<IResult> GetMessengerChannelById(
        [FromQuery] GetMessengerChannelByIdRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerChannelById");
        var result = await _messLogic.GetMessengerChannelById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetMessengerChannels")]
    [Description("Get Filtered MessengerChannels")]
    [ResponseSchema<GetMessengersResponse>]
    public async Task<IResult> GetMessengerChannels(
        [FromBody] GetMessengerChannelsRequest request, CT ct)
    {
        _logger.LogInformation("GetMessengerChannels");
        var result = await _messLogic.GetMessengerChannels(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetMessengerChannelsEnumExcel")]
    [Description("Get enum values for messengerChannel histories enum excel filtering.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetMessengerChannelsEnumExcel(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerChannelsEnumExcel");
        var result = EnumExtensions.GetEnums<MessengerChannelsEnumExcel>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetMessengerChannelsToExcel")]
    [Description("Export filtered MessengerChannel to Excel.")]
    [ResponseSchema<GetMessengerChannelsResponse>]
    public async Task<IResult> GetMessengerChannelsToExcel(
        [FromBody] GetMessengerChannelsToExcelRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerChannelsToExcel");
        var response = await _messLogic.GetMessengerChannels(request.Adapt<GetMessengerChannelsRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetMessengerChannelsToExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, ""),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"MessengerChannels-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetMessengerChannelsToExcelResponse?>(
            new(result)).GetHttpResponse();
    }

    [HttpPost("GetMessengerMessageType")]
    [Description("Get Messenger Message Type.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetMessengerMessageType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerMessageType");
        var result = EnumExtensions.GetEnums<MessengerMessageType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetMessengerChannelHistories")]
    [Description("Get MessengerChannelHistory")]
    [ResponseSchema<GetMessengerChannelHistoriesResponse>]
    public async Task<IResult> GetMessengerChannelHistories(
        [FromBody] GetMessengerChannelHistoriesRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerChannelHistories");
        var result = await _messLogic.GetMessengerChannelHistories(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetMessengerChannelHistoriesEnumExcel")]
    [Description("Get enum values for messengerChannel histories enum excel filtering.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetMessengerChannelHistoriesEnumExcel(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerChannelHistoriesEnumExcel");
        var result = EnumExtensions.GetEnums<MessengerChannelHistoryExcelColumn>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetMessengerChannelHistoriesToExcel")]
    [Description("Export filtered MessengerChannelHistories to Excel.")]
    [ResponseSchema<GetMessengerChannelHistoriesToExcelResponse>]
    public async Task<IResult> GetMessengerChannelHistoriesToExcel(
        [FromBody] GetMessengerChannelHistoriesToExcelRequest request, CT ct)
    {
        _logger.LogInformation($"GetMessengerChannelHistoriesToExcel");
        var response = await _messLogic.GetMessengerChannelHistories(request.Adapt<GetMessengerChannelHistoriesRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetMessengerChannelHistoriesToExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, ""),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"MessengerChannelHistories-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetMessengerChannelHistoriesToExcelResponse?>(
            new(result)).GetHttpResponse();
    }


    [HttpPut("ChangeMessengerState")]
    [ResponseSchema<ChangeMessengerStateResponse>]
    public async Task<IResult> ChangeMessengerState(
        [FromBody] ChangeMessengerStateRequest request, CT ct)
    {
        _logger.LogInformation("ChangeMessengerState");
        var result = await _messLogic.ChangeMessengerState(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveMessenger")]
    [ResponseSchema<ChangeMessengerStateResponse>]
    public async Task<IResult> ActiveMessenger(
        [FromBody] ActiveMessengerRequest request, CT ct)
    {
        _logger.LogInformation("ActiveMessenger");
        var result = await _messLogic.ChangeMessengerState(new([request.Id], true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InActiveMessenger")]
    [ResponseSchema<ChangeBillOfLadingStateResponse>]
    public async Task<IResult> InActiveMessenger(
        [FromBody] InActiveMessengerRequest request, CT ct)
    {
        _logger.LogInformation("InActiveMessenger");
        var result = await _messLogic.ChangeMessengerState(new([request.Id], false), ct);
        return result.GetHttpResponse();
    }

}
using Engineering.Application.Services.TelegramChats;
using Engineering.Application.Services.TelegramChats.Models.AcceptPaymentOrderTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ActiveTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.CommerceRequestWarehouseStatusChangeTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.CommercialPackingTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.CommercialPaymentTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ConsumerExitTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ContractorStatementPaymentMessage;
using Engineering.Application.Services.TelegramChats.Models.CreateTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.EnteringToWarehouseTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.EntryThroughRelocationForTemporaryDeliveryTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ExitForRelocationTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ExitRelocationForTemporaryDeliveryTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.GetActiveTelegramChats;
using Engineering.Application.Services.TelegramChats.Models.GetByCostCenterId;
using Engineering.Application.Services.TelegramChats.Models.GetsTelegramChatByCostCenterIds;
using Engineering.Application.Services.TelegramChats.Models.GetsTelegramMessageType;
using Engineering.Application.Services.TelegramChats.Models.GetTelegramChatById;
using Engineering.Application.Services.TelegramChats.Models.GetTelegramChatByName;
using Engineering.Application.Services.TelegramChats.Models.GetTelegramChats;
using Engineering.Application.Services.TelegramChats.Models.InactiveTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.PaymentTreasuryTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.SendMessage;
using Engineering.Application.Services.TelegramChats.Models.SendTestTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.StateChangerTelegramChats;
using Engineering.Application.Services.TelegramChats.Models.TelegramChatGroupDelete;
using Engineering.Application.Services.TelegramChats.Models.TemporaryDeliveryTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.UpdateTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.UserChangedTelegramMessage;

namespace Engineering.Api.Controllers.TelegramChats;

[ApiController]
[Route("api/engineering/v1/TelegramChat")]
public class TelegramChatController : ControllerBase
{
    private readonly ITelegramChatLogic _logic;

    public TelegramChatController(ITelegramChatLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddTelegramChat")]
    [ResponseSchema<CreateTelegramChatResponse>]
    public async Task<IResult> Create([FromBody] CreateTelegramChatRequest request, CT ct)
    {
        var result = await _logic.CreateTelegramChat(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TelegramChatGroupDelete")]
    [ResponseSchema<TelegramChatGroupDeleteResponse>]
    public async Task<IResult> GroupDelete([FromBody] TelegramChatGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.TelegramChatGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateTelegramChats")]
    [ResponseSchema<StateChangerTelegramChatsResponse>]
    public async Task<IResult> Activate([FromBody] ActivateTelegramChatsRequest request, CT ct)
    {
        var result = await _logic.StateChangerTelegramChats(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateTelegramChats")]
    [ResponseSchema<StateChangerTelegramChatsResponse>]
    public async Task<IResult> Inactivate([FromBody] InactivateTelegramChatsRequest request, CT ct)
    {
        var result = await _logic.StateChangerTelegramChats(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditTelegramChat")]
    [ResponseSchema<UpdateTelegramChatResponse>]
    public async Task<IResult> Update([FromBody] UpdateTelegramChatRequest request, CT ct)
    {
        var result = await _logic.UpdateTelegramChat(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveTelegramChat")]
    [ResponseSchema<ActiveTelegramChatResponse>]
    public async Task<IResult> Active([FromBody] ActiveTelegramChatRequest request, CT ct)
    {
        var result = await _logic.ActiveTelegramChat(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveTelegramChat")]
    [ResponseSchema<InactiveTelegramChatResponse>]
    public async Task<IResult> Inactive([FromBody] InactiveTelegramChatRequest request, CT ct)
    {
        var result = await _logic.InactiveTelegramChat(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTelegramChatById")]
    [ResponseSchema<GetTelegramChatByIdResponse>]
    public async Task<IResult> GetById([FromQuery] GetTelegramChatByIdRequest request, CT ct)
    {
        var result = await _logic.GetTelegramChatById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTelegramChatByName")]
    [ResponseSchema<GetTelegramChatByNameResponse>]
    public async Task<IResult> GetByName([FromQuery] GetTelegramChatByNameRequest request, CT ct)
    {
        var result = await _logic.GetTelegramChatByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetByCostCenterId")]
    [ResponseSchema<GetByCostCenterIdResponse>]
    public async Task<IResult> GetByCostCenter([FromQuery] GetByCostCenterIdRequest request, CT ct)
    {
        var result = await _logic.GetByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTelegramMessageType")]
    [ResponseSchema<GetsTelegramMessageTypeResponse>]
    public async Task<IResult> GetMessageTypes([FromQuery] GetsTelegramMessageTypeRequest request, CT ct)
    {
        var result = await _logic.GetsTelegramMessageType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTelegramChatByCostCenterIds")]
    [ResponseSchema<GetsTelegramChatByCostCenterIdsResponse>]
    public async Task<IResult> GetByCostCenters([FromBody] GetsTelegramChatByCostCenterIdsRequest request, CT ct)
    {
        var result = await _logic.GetsTelegramChatByCostCenterIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveTelegramChat")]
    [ResponseSchema<GetActiveTelegramChatsResponse>]
    public async Task<IResult> GetActive(
        [FromQuery] GetActiveTelegramChatsRequest request, CT ct)
    {
        var result = await _logic.GetsActiveTelegramChat(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTelegramChat")]
    [ResponseSchema<GetTelegramChatsResponse>]
    public async Task<IResult> GetFiltered([FromBody] GetTelegramChatsRequest request, CT ct)
    {
        var result = await _logic.GetsTelegramChat(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendExitForRelocationTelegramMessage")]
    [ResponseSchema<ExitForRelocationTelegramMessageResponse>]
    public async Task<IResult> ExitRelocation([FromBody] ExitForRelocationTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendExitForRelocationMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendExitRelocationForTemporaryDeliveryTelegramMessage")]
    [ResponseSchema<ExitRelocationForTemporaryDeliveryTelegramMessageResponse>]
    public async Task<IResult> ExitRelocationTemp([FromBody] ExitRelocationForTemporaryDeliveryTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendExitRelocationForTemporaryDeliveryMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendEntryThroughRelocationForTemporaryDeliveryTelegramMessage")]
    [ResponseSchema<EntryThroughRelocationForTemporaryDeliveryTelegramMessageResponse>]
    public async Task<IResult> EntryRelocationTemp([FromBody] EntryThroughRelocationForTemporaryDeliveryTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendEntryThroughRelocationForTemporaryDeliveryMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendEnteringToWarehouseTelegramMessage")]
    [ResponseSchema<EnteringToWarehouseTelegramMessageResponse>]
    public async Task<IResult> EnterWarehouse([FromBody] EnteringToWarehouseTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendEnteringToWarehouseMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendConsumerExitTelegramMessage")]
    [ResponseSchema<ConsumerExitTelegramMessageResponse>]
    public async Task<IResult> ConsumerExit([FromBody] ConsumerExitTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendConsumerExitMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendCommercialPaymentTelegramMessage")]
    [ResponseSchema<CommercialPaymentTelegramMessageResponse>]
    public async Task<IResult> CommercialPayment([FromBody] CommercialPaymentTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendCommercialPaymentMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendTemporaryDeliveryTelegramMessage")]
    [ResponseSchema<TemporaryDeliveryTelegramMessageResponse>]
    public async Task<IResult> TemporaryDelivery([FromBody] TemporaryDeliveryTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendTemporaryDeliveryMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendCommercialPackingTelegramMessage")]
    [ResponseSchema<CommercialPackingTelegramMessageResponse>]
    public async Task<IResult> Packing([FromBody] CommercialPackingTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendCommercialPackingMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendAcceptPaymentOrderTelegramMessage")]
    [ResponseSchema<AcceptPaymentOrderTelegramMessageResponse>]
    public async Task<IResult> AcceptPayment([FromBody] AcceptPaymentOrderTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendAcceptPaymentOrderMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendPaymentTreasuryTelegramMessage")]
    [ResponseSchema<PaymentTreasuryTelegramMessageResponse>]
    public async Task<IResult> TreasuryPayment([FromBody] PaymentTreasuryTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendPaymentTreasuryMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendTestTelegramMessage")]
    [ResponseSchema<SendTestTelegramMessageResponse>]
    public async Task<IResult> SendTest([FromBody] SendTestTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendTestMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendMessage")]
    [ResponseSchema<SendMessageResponse>]
    public async Task<IResult> SendMessage([FromBody] SendMessageRequest request, CT ct)
    {
        var result = await _logic.SendMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SendCommerceRequestWarehouseStatusChange")]
    [ResponseSchema<CommerceRequestWarehouseStatusChangeTelegramMessageResponse>]
    public async Task<IResult> WarehouseStatusChange([FromBody] CommerceRequestWarehouseStatusChangeTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.SendCommerceRequestWarehouseStatusChangeMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ContractorStatementPaymentMessage")]
    [ResponseSchema<ContractorStatementPaymentMessageResponse>]
    public async Task<IResult> ContractorPayment([FromBody] ContractorStatementPaymentMessageRequest request, CT ct)
    {
        var result = await _logic.ContractorStatementPaymentNewMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UserChangedTelegramMessage")]
    [ResponseSchema<UserChangedTelegramMessageResponse>]
    public async Task<IResult> UserChanged([FromBody] UserChangedTelegramMessageRequest request, CT ct)
    {
        var result = await _logic.UserChangedMessage(request, ct);
        return result.GetHttpResponse();
    }
}
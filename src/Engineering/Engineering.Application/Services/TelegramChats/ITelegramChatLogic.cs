using Engineering.Application.Services.TelegramChats.Models.AcceptPaymentOrderTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ActiveTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.CommerceRequestWarehouseStatusChangeTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.CommercialPackingTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.CommercialPaymentTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ConsumerExitTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ContractorStatementPaymentMessage;
using Engineering.Application.Services.TelegramChats.Models.CreateTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.DeleteTelegramChat;
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
using AcceptPaymentOrderTelegramMessageResponse = Engineering.Application.Services.TelegramChats.Models.AcceptPaymentOrderTelegramMessage.AcceptPaymentOrderTelegramMessageResponse;

namespace Engineering.Application.Services.TelegramChats;

public interface ITelegramChatLogic
{
    ///Commands
    Task<Result<CreateTelegramChatResponse?>> CreateTelegramChat(CreateTelegramChatRequest request, CT ct);

    Task<Result<DeleteTelegramChatResponse?>> DeleteTelegramChat(DeleteTelegramChatRequest request, CT ct);

    Task<Result<UpdateTelegramChatResponse?>> UpdateTelegramChat(UpdateTelegramChatRequest request, CT ct);

    Task<Result<InactiveTelegramChatResponse?>> InactiveTelegramChat(InactiveTelegramChatRequest request, CT ct);

    Task<Result<ActiveTelegramChatResponse?>> ActiveTelegramChat(ActiveTelegramChatRequest request, CT ct);

    Task<Result<StateChangerTelegramChatsResponse?>> StateChangerTelegramChats(StateChangerTelegramChatsRequest request, CT ct);

    Task<Result<TelegramChatGroupDeleteResponse?>> TelegramChatGroupDelete(TelegramChatGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetTelegramChatByIdResponse?>> GetTelegramChatById(GetTelegramChatByIdRequest request, CT ct);

    Task<Result<GetsTelegramMessageTypeResponse?>> GetsTelegramMessageType(GetsTelegramMessageTypeRequest request, CT ct);

    Task<Result<GetTelegramChatByNameResponse?>> GetTelegramChatByName(GetTelegramChatByNameRequest request, CT ct);

    Task<Result<GetActiveTelegramChatsResponse?>> GetsActiveTelegramChat(GetActiveTelegramChatsRequest request, CT ct);

    Task<Result<GetTelegramChatsResponse?>> GetsTelegramChat(GetTelegramChatsRequest request, CT ct);

    Task<Result<GetByCostCenterIdResponse?>> GetByCostCenterId(GetByCostCenterIdRequest request, CT ct);

    Task<Result<GetsTelegramChatByCostCenterIdsResponse?>> GetsTelegramChatByCostCenterIds(GetsTelegramChatByCostCenterIdsRequest request, CT ct);

    Task<Result<ExitForRelocationTelegramMessageResponse?>> SendExitForRelocationTelegramMessage(
        ExitForRelocationTelegramMessageRequest request,
        CT ct);

    Task<Result<EnteringToWarehouseTelegramMessageResponse?>> SendEnteringToWarehouseTelegramMessage(
        EnteringToWarehouseTelegramMessageRequest request,
        CT ct);

    Task<Result<ConsumerExitTelegramMessageResponse?>> SendConsumerExitTelegramMessage(
        ConsumerExitTelegramMessageRequest request,
        CT ct);

    Task<Result<CommercialPaymentTelegramMessageResponse?>> SendCommercialPaymentTelegramMessage(
        CommercialPaymentTelegramMessageRequest request,
        CT ct);

    Task<Result<TemporaryDeliveryTelegramMessageResponse?>> SendTemporaryDeliveryTelegramMessage(
        TemporaryDeliveryTelegramMessageRequest request,
        CT ct);

    Task<Result<CommercialPackingTelegramMessageResponse?>> SendCommercialPackingTelegramMessage(
        CommercialPackingTelegramMessageRequest request,
        CT ct);

    Task<Result<AcceptPaymentOrderTelegramMessageResponse?>> SendAcceptPaymentOrderTelegramMessage(
        AcceptPaymentOrderTelegramMessageRequest request,
        CT ct);

    Task<Result<PaymentTreasuryTelegramMessageResponse?>> SendPaymentTreasuryTelegramMessage(
        PaymentTreasuryTelegramMessageRequest request,
        CT ct);

    Task<Result<SendTestTelegramMessageResponse?>> SendTestTelegramMessage(
        SendTestTelegramMessageRequest request,
        CT ct);

    Task<Result<SendMessageResponse?>> SendMessage(
        SendMessageRequest request, CT ct);

    Task<Result<ExitRelocationForTemporaryDeliveryTelegramMessageResponse?>> SendExitRelocationForTemporaryDeliveryTelegramMessage(
        ExitRelocationForTemporaryDeliveryTelegramMessageRequest request,
        CT ct);

    Task<Result<EntryThroughRelocationForTemporaryDeliveryTelegramMessageResponse?>> SendEntryThroughRelocationForTemporaryDeliveryTelegramMessage(
        EntryThroughRelocationForTemporaryDeliveryTelegramMessageRequest request,
        CT ct);

    Task<Result<CommerceRequestWarehouseStatusChangeTelegramMessageResponse?>> SendCommerceRequestWarehouseStatusChange(
        CommerceRequestWarehouseStatusChangeTelegramMessageRequest request,
        CT ct);

    Task<Result<ContractorStatementPaymentMessageResponse?>> ContractorStatementPaymentMessage(
        ContractorStatementPaymentMessageRequest request,
        CT ct);

    Task<Result<UserChangedTelegramMessageResponse?>> UserChangedTelegramMessage(
        UserChangedTelegramMessageRequest request, CT ct);

    Task<Result<CommercialPaymentTelegramMessageResponse?>> SendCommercialPaymentMessage(
        CommercialPaymentTelegramMessageRequest request,
        CT ct);

    Task<Result<AcceptPaymentOrderTelegramMessageResponse?>> SendAcceptPaymentOrderMessage(
        AcceptPaymentOrderTelegramMessageRequest request, CT ct);

    Task<Result<EnteringToWarehouseTelegramMessageResponse?>> SendEnteringToWarehouseMessage(
        EnteringToWarehouseTelegramMessageRequest request,
        CT ct);

    Task<Result<ExitForRelocationTelegramMessageResponse?>> SendExitForRelocationMessage(
        ExitForRelocationTelegramMessageRequest request,
        CT ct);

    Task<Result<TemporaryDeliveryTelegramMessageResponse?>> SendTemporaryDeliveryMessage(
        TemporaryDeliveryTelegramMessageRequest request, CT ct);

    Task<Result<CommercialPackingTelegramMessageResponse?>> SendCommercialPackingMessage(
        CommercialPackingTelegramMessageRequest request, CT ct);

    Task<Result<ConsumerExitTelegramMessageResponse?>> SendConsumerExitMessage(
        ConsumerExitTelegramMessageRequest request,
        CT ct);

    Task<Result<ContractorStatementPaymentMessageResponse?>> ContractorStatementPaymentNewMessage(
        ContractorStatementPaymentMessageRequest request, CT ct);

    Task<Result<PaymentTreasuryTelegramMessageResponse?>> SendPaymentTreasuryMessage(
        PaymentTreasuryTelegramMessageRequest request, CT ct);

    Task<Result<UserChangedTelegramMessageResponse?>> UserChangedMessage(
        UserChangedTelegramMessageRequest request, CT ct);

    Task<Result<SendTestTelegramMessageResponse?>> SendTestMessage(
        SendTestTelegramMessageRequest request, CT ct);

    Task<Result<CommerceRequestWarehouseStatusChangeTelegramMessageResponse?>> SendCommerceRequestWarehouseStatusChangeMessage(
        CommerceRequestWarehouseStatusChangeTelegramMessageRequest request,
        CT ct);

    Task<Result<EntryThroughRelocationForTemporaryDeliveryTelegramMessageResponse?>> SendEntryThroughRelocationForTemporaryDeliveryMessage(
        EntryThroughRelocationForTemporaryDeliveryTelegramMessageRequest request,
        CT ct);

    Task<Result<ExitRelocationForTemporaryDeliveryTelegramMessageResponse?>> SendExitRelocationForTemporaryDeliveryMessage(
        ExitRelocationForTemporaryDeliveryTelegramMessageRequest request, CT ct);
}
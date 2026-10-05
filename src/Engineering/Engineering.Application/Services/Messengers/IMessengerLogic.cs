using Engineering.Application.Services.GetFltrMessengerChannels.Contracts.GetFltrMessengerChannel;
using Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;
using Engineering.Application.Services.Messengers.Contracts.ChangeMessengerState;
using Engineering.Application.Services.Messengers.Contracts.CreateMessenger;
using Engineering.Application.Services.Messengers.Contracts.CreateMessengerChannel;
using Engineering.Application.Services.Messengers.Contracts.DeleteMessenger;
using Engineering.Application.Services.Messengers.Contracts.DeleteMessengerChannel;
using Engineering.Application.Services.Messengers.Contracts.GetMessengerById;
using Engineering.Application.Services.Messengers.Contracts.GetMessengers;
using Engineering.Application.Services.Messengers.Contracts.UpdateMessenger;
using Engineering.Application.Services.Messengers.Contracts.UpdateMessengerChannel;
using Engineering.Application.Services.TelegramChats.Models.SendTestTelegramMessage;

namespace Engineering.Application.Services.Messengers;

public interface IMessengerLogic
{
    Task<Result<CreateMessengerResponse?>> CreateMessenger(
        CreateMessengerRequest request, CT ct);

    Task<Result<CreateMessengerChannelResponse?>> CreateMessengerChannel(
        CreateMessengerChannelRequest request, CT ct);

    Task<Result<UpdateMessengerResponse?>> UpdateMessenger(
        UpdateMessengerRequest request, CT ct);

    Task<Result<UpdateMessengerChannelResponse?>> UpdateMessengerChannel(
        UpdateMessengerChannelRequest request, CT ct);

    Task<Result<DeleteMessengerResponse?>> DeleteMessenger(
        DeleteMessengerRequest request, CT ct);

    Task<Result<DeleteMessengerChannelResponse?>> DeleteMessengerChannel(
        DeleteMessengerChannelRequest request, CT ct);

    Task<Result<GetMessengerByIdResponse?>> GetMessengerById(
        GetMessengerByIdRequest request, CT ct);

    Task<Result<GetMessengersResponse?>> GetMessengers(
       GetMessengersRequest request, CT ct);

    Task<Result<GetMessengerChannelByIdResponse?>> GetMessengerChannelById(
    GetMessengerChannelByIdRequest request, CT ct);

    Task<Result<GetMessengerChannelsResponse?>> GetMessengerChannels(
      GetMessengerChannelsRequest request, CT ct);

    Task<Result<GetMessengerChannelHistoriesResponse?>> GetMessengerChannelHistories(
        GetMessengerChannelHistoriesRequest request, CT ct);

    Task<Result> RetrySendMessage(
        RetrySendMessageRequest request, CT ct);

    Task<Result<ChangeMessengerStateResponse?>> ChangeMessengerState(
            ChangeMessengerStateRequest request, CT ct);

    Task<Result<SendTestTelegramMessageResponse?>> SendTestMessage(
        SendTestTelegramMessageRequest request, CT ct);
}

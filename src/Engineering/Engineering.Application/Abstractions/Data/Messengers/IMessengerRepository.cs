using Engineering.Application.Services.Messengers.Contracts.GetMessengerById;
using Engineering.Application.Services.Messengers.Contracts.GetMessengers;
using Engineering.Domain.Entities.Messengers;
using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Abstractions.Data.Messengers;

public interface IMessengerRepository : IBaseRepository<Messenger>
{
    Task<Messenger?> GetMessenger(
        long id, CT ct);

    Task<List<Messenger>> GetMessengers(
        List<long> ids, CT ct);

    Task<GetMessengerByIdResponse?> GetMessengerById(
        long id, CT ct);

    Task<(List<GetMessengersModel> Data, int RowCount)> GetMessengers(
        long? targetId,
        MessengerTargetType? messengerTargetType,
        MessengerType? messengerType,
        int pageIndex,
        int pageSize,
        CT ct);

}
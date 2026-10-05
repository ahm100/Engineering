using Engineering.Application.Services.TelegramChats.Models.ResponseModels;
using Engineering.Domain.Entities.TelegramChats;
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Abstractions.Data.TelegramChats;

public interface ITelegramChatRepository : IBaseRepository<TelegramChat>
{
    Task<TelegramChat?> FindByName(string name, CT ct);
    Task<TelegramChat?> GetById(long id, CT ct);
    Task<bool> FindTelegramChatByNames(List<string> names, CT ct);
    Task<TelegramChat?> FindForDelete(long id, CT ct);

    Task<List<TelegramChat>> GetsTelegramChatByIds(
        List<long> ids,
        CT ct);
    Task<(List<TelegramChat> Data, int RowCount)> GetTelegramChats(
        List<long>? ids,
        TelegramMessageType? telegramMessageType,
        string? filterData,
        long? costCenterId,
        long? projectId,
        string? name,
        bool? isActive,
        string[]? orderBy,
        bool? getOther,
        int pageIndex,
        int pageSize,
        CT ct);
    Task<(List<TelegramChat> Data, int RowCount)> GetActiveTelegramChats(
        string? filterData,
        long? costCenterId,
        long? projectId,
        string? name,
        bool? getOther,
        int pageIndex,
        int pageSize,
        CT ct);
    Task<(List<TelegramChat> Data, int RowCount)> GetByCostCenterId(
        long costCenterId,
        long? projectId,
        bool? getOther,
        int pageIndex,
        int pageSize,
        CT ct);
    Task<(List<TelegramChat> Data, int RowCount)> GetByProjectOperationDetailId(
        long projectOperationDetailId,
        bool? getOther,
        CT ct);
    Task<(List<TelegramChat> Data, int RowCount)> GetByRequestGoodsSupplyId(
        long requestGoodsSupplyId,
        bool? getOther,
        CT ct);
    Task<(List<TelegramChat> Data, int RowCount)> GetsTelegramChatByCostCenterIds(
        List<long>? costCenterIds,
        TelegramMessageType? telegramMessageType,
        string? filterData,
        bool? isActive,
        bool? getOther,
        int pageIndex,
        int pageSize,
        CT ct);
    Task<(List<TelegramMessageResponseModel> Data, int RowCount)> GetByTransportationRequestId(
        long transportationRequestId,
        bool? getOther,
        TelegramMessageType telegramMessageType,
        CT ct);
    Task<(List<TelegramChat> Data, int RowCount)> GetBySnapIds(
        List<long> ids,
        bool? getOther,
        TelegramMessageType telegramMessageType,
        CT ct);
}
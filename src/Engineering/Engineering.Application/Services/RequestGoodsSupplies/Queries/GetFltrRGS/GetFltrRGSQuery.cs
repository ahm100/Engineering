using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGS;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFltrRGS;

public record GetFltrRGSQuery(
    List<long>? ProjectIds,
    long? CityId,
    long? ProjectManagerId,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyType>? Types,
    List<long>? CreatorIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<GetFltrRGSResponse?>;
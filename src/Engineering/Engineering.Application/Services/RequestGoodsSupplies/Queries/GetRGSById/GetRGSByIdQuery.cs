using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSById;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRGSById;

public record GetRGSByIdQuery(
    long Id) : IQuery<GetRGSByIdResponse?>;
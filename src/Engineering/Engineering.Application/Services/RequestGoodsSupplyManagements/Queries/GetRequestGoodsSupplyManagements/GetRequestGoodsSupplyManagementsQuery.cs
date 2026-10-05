using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetRequestGoodsSupplyManagements;

public record GetRequestGoodsSupplyManagementsQuery(
    List<long>? RequestGoodsSupplies,
    List<long>? RequestGoodsSupplyDetailIds,
    long? InvoiceId
    ) : IQuery<List<RequestGoodsSupplyManagement>>;

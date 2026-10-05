using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetsSupplyManagementByInvoiceId;

public record GetsSupplyManagementByInvoiceIdQuery(
    long InvoiceId
    ) : IQuery<List<RequestGoodsSupplyManagement>>;

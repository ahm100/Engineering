
namespace Engineering.Application.RequestGoodsSupplyManagements.Models.RejectRequestGoodsSupplyManagement;

public record RejectRequestGoodsSupplyManagementRequest(
    long InvoiceId
    ) : IHttpRequest;
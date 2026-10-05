using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementByIdDetailManagementModel
{
    public long Id { get; set; }
    public long InvoiceId { get; set; }
    public GetFiduciaryProductManagementByIdDetailManagementWarehouseModel? Warehouse { get; set; }
    public FiduciaryProductDetailManagementStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
}

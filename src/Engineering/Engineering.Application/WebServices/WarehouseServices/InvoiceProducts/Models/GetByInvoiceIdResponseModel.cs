using Gita.Backend.Shared.Domain.Enums.Invoice;

namespace Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Models;

public class GetByInvoiceIdResponseModel
{
    [JsonProperty("data")]
    public List<GetAllActiveInvoiceProductsModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
public record GetAllActiveInvoiceProductsModel(
    long Id,
    long ProductId,
    decimal Quantity,
    decimal TotalQuantity,
    string Code,
    string Name,
    long? BrandId,
    string? BrandName,
    long? BrandModelId,
    string? BrandModelName,
    decimal InventoryQuantity,
    long MeasureUnitId,
    string MeasureUnitTitle,
    long PackageId,
    string PackageTitle,
    WarehouseInvoiceProductStatus Status,
    string StatusTitle,
    long InvoiceId,
    string? Description,
    bool HasSerialNo,
    bool? IsSerialGeneratedAutomatically);
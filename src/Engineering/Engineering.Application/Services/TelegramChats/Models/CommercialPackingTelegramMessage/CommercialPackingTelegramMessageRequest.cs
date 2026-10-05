namespace Engineering.Application.Services.TelegramChats.Models.CommercialPackingTelegramMessage;

public record CommercialPackingTelegramMessageRequest(
    bool IsUpdated,
    string? Description,
    string? DestinationWarehouseName,
    long? RequestNumber,
    string? SupplierName,//رابط شرکت
    string? ThirdParty,//فروشگاه 
    string? FollowupName, // مسول خرید
    List<ProductsListPackingForTelegramModel>? Products,
    DateTime? CreateDate,
    DateTime? DeliveryDate,
    List<string>? DocumentUrls,
    ExcelFileModel? File
) : IHttpRequest;

public class ExcelFileModel
{
    public byte[]? FileContents { get; set; }
    public string? FileName { get; set; }
    public string? ContentType { get; set; }
}

public class ProductsListPackingForTelegramModel
{
    public long? CostCenterId { get; set; }
    public long? WarehouseId { get; set; }
    public long? RequestNumber { get; set; }
    public string? ProductName { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? RequestCount { get; set; }
    public string? MeasureUnitName { get; set; }
}

public class CreatePackingExcelExporterResponseModel
{
    public long? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? Brand { get; set; }
    public string? BrandModel { get; set; }
    public string? MeasureUnitName { get; set; }
    public string? CostCenterName { get; set; }
    public string? ProjectName { get; set; }
    public long? RequestNumber { get; set; }
    public string? CommercialRequestNumber { get; set; }
    public string? ProjectOperationName { get; set; }
    public string? Description { get; set; }
    public string? ThirdParty { get; set; }
    public string? SupplierName { get; set; }
    public string? FollowupName { get; set; }
    public string? Owner { get; set; }
    public decimal? RequestCount { get; set; }
    public DateTime? RequestDate { get; set; }
    public string? PersianRequestDate { get; set; }
    public decimal? Quantity { get; set; }
}
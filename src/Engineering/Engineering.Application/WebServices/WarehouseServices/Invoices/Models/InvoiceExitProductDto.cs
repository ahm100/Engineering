using Engineering.Application.RequestGoodsSupplyManagements.Commands.CreateRequestGoodsSupplyManagement;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;

public record InvoiceExitProductDto
{
    public long ProductId { get; set; }
    public long MeasureId { get; set; }
    public double Quantity { get; set; }
    public long? ServiceReferenceId { get; set; }
    public List<InvoiceRequestItemDto>? Items { get; set; }
    public DateTime? RetrunDate { get; set; }
    public List<CreateRequestGoodsSupplyManagementModel>? Managements { get; set; }
};

public record InvoiceExitProductDtoNoManagement
{
    public long ProductId { get; set; }
    public long MeasureId { get; set; }
    public double Quantity { get; set; }
    public long? ServiceReferenceId { get; set; }
    public List<InvoiceRequestItemDto>? Items { get; set; }
    public DateTime? RetrunDate { get; set; }
};

public record InvoiceRequestItemDto(
    string? SerialNo
    );
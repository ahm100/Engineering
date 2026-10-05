using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Gita.Backend.Shared.Domain.Enums.Commerces;

namespace Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCRHeader;

public class CreateCRHeaderRequest
{
    public long? RequestNumber { get; set; }
    public int Type { get; set; }
    public long? CostCategoryId { get; set; }
    public long? CostGroupId { get; set; }
    public long? RequestGoodsSupplyId { get; set; }
    public string? RequestSerialNumber { get; set; }
    public string? Description { get; set; }
    public required List<CreateCRModel> Details { get; set; }
}

public class CreateCRModel
{
    public int Status { get; set; }
    public int? PreStatus { get; set; }
    public int Type { get; set; }
    public long? InquiryId { get; set; }
    public long? RequestNumber { get; set; }
    public long? ProjectId { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public long ReferenceId { get; set; }
    public int? RGSSupplyType { get; set; }
    public decimal MainRequestCount { get; set; }
    public decimal RealRequestCount { get; set; }
    public decimal? ConfirmedRequestCount { get; set; }
    public decimal? Price { get; set; }
    public long? CurrencyId { get; set; }
    public string? Description { get; set; }
    public string? RequestSerialNumber { get; set; }
    public string? CommercialRequestNumber { get; set; }
    public long? SupplyerId { get; set; }
    public DateTime? RequestedDate { get; set; }
    public DateTime? CreatedInSupply { get; set; }
    public int PaymentType { get; set; }
    public bool IsManually { get; set; }
    public bool CreateInvoice { get; set; }
    public List<string>? Urls { get; set; }
    public List<CreateCRDetailModel>? CCDetails { get; set; }
}

public class CreateCRDetailModel
{
    public long? CostCenterId { get; set; }
    public decimal? RequestCount { get; set; }
}
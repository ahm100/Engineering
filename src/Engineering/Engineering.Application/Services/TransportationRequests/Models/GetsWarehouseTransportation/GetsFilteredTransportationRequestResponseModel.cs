using Engineering.Application.Extensions.PlateHelper;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;

public record GetsWarehouseTransportationResponseModel
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public List<GetWarehouseCostCentersModel>? CostCenters { get; set; }
    public string? CostCenterNames => CostCenters != null && CostCenters.Count > 0 ?
        string.Join(", ", CostCenters!.Listed(z => z.CostCenterName)) : string.Empty;
    public List<GetWarehouseProjectsModel>? Projects { get; set; }
    public string? ProjectNames => Projects != null && Projects.Count > 0 ?
        string.Join(", ", Projects!.Listed(z => z.ProjectName)) : string.Empty;
    public TransportationRequestStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public long? TransportationId { get; set; }
    public string? TransportationName { get; set; }
    public decimal? TransferPrice { get; set; }
    public long? StartingCityId { get; set; }
    public string? StartingCityName { get; set; }
    public long? DestinationCityId { get; set; }
    public string? DestinationCityName { get; set; }
    public string? StartingCityAddress { get; set; }
    public string? DestinationAddress { get; set; }
    public string? Description { get; set; }
    public long? MachineTypeId { get; set; }
    public string? MachineType { get; set; }
    public long? DriverId { get; set; }
    public string? DriverName { get; set; }
    public string? NumberPlate { get; set; }
    public string? CertificateNumber { get; set; }
    public GetWarehouseTransportNumberPlate? NumberPlateModel => NumberPlate.ToPlateModel<GetWarehouseTransportNumberPlate>();
    public long? TransportationContractorId { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? MainName { get; set; } = string.Empty;
    public string? ContractorPhoneNumber { get; set; }
    public string? FreightNumber { get; set; }
    public string? Warehouses => PackingData != null && PackingData.Count > 0 ?
        string.Join(", ", PackingData!.Listed(z => z.Warehouse)) : string.Empty;
    public string? ThirdParties => PackingData != null && PackingData.Count > 0 ?
        string.Join(", ", PackingData!.Listed(z => z.ThirdParty)) : string.Empty;
    public string? ThirdPartiesName => PackingData != null && PackingData.Count > 0 ?
        string.Join(", ", PackingData!.Listed(z => z.ThirdPartyName)) : string.Empty;
    public string? PackingNumbers => PackingData != null && PackingData.Count > 0 ?
        string.Join(", ", PackingData!.Listed(z => z.PackingNumber)) : string.Empty;
    public string? PalletNumbers => PackingData != null && PackingData.Count > 0 ?
        string.Join(", ", PackingData!.Listed(z => z.PalletNumber)) : string.Empty;
    public string? ExitInvoices => PackingData != null && PackingData.Count > 0 ?
        string.Join(", ", PackingData!.Listed(z => z.ExitInvoice)) : string.Empty;
    public bool? IsAggregate { get; set; }
    public DateTime? PostageDate { get; set; }
    public string? PostageDateShamsi => TimeCalculator.ConvertToShamsi(PostageDate);
    public bool? IsUrgent => PostageDate == null ?
        null : PostageDate <= DateTime.UtcNow || (PostageDate - DateTime.UtcNow).Value.TotalDays <= 1;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public decimal? LoadWeight { get; set; }
    public decimal? Volume { get; set; }
    public DeliveryMethod? DeliveryMethod { get; set; }
    public string? DeliveryMethodTitle => DeliveryMethod?.GetEnumDescription();
    public DeliveryType? DeliveryType { get; set; }
    public string? DeliveryTypeTitle => DeliveryType?.GetEnumDescription();
    public string? AggregateInvoices => PackingData != null ?
        string.Join(", ", PackingData.Listed(x => x.ExitInvoice)) : string.Empty;
    public string? Invoices => PackingData != null ?
        string.Join(", ", PackingData.Listed(x => x.Invoices)) : string.Empty;
    public string? Channels => PackingData != null ?
        string.Join(", ", PackingData.Listed(x => x.Channel)) : string.Empty;
    public string? CompanyCodes => PackingData != null ?
        string.Join(", ", PackingData.Listed(x => x.CompanyCode)) : string.Empty;
    public string? CompanyNames => PackingData != null ?
        string.Join(", ", PackingData.Listed(x => x.CompanyName)) : string.Empty;
    public string? InvoiceNumbers => PackingData != null ?
        string.Join(", ", PackingData.Listed(x => x.InvoiceNumber)) : string.Empty;
    public decimal? PackingQuantity => PackingData != null ? PackingData.Count : 0;
    public decimal? ItemQuantity => PackingData != null ?
        PackingData.Where(z => z.Quantity != null).Select(x => x.Quantity).Sum(x => x) : 0;
    public List<GetTransportWarehouseModel>? PackingData { get; set; }
}

public record GetTransportWarehouseModel
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public long? SourceWarehouseId { get; set; }
    public string? SourceWarehouse { get; set; }
    public long? WarehouseId { get; set; }
    public string? Warehouse { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; }
    public string? ThirdPartyName { get; set; }
    public string? ThirdPartyUniqeCode { get; set; }
    public long? PackingId { get; set; }
    public long? PackingNumber { get; set; }
    public decimal? Price { get; set; }
    public decimal? TransferPrice { get; set; }
    public string? PalletNumber { get; set; }
    public string? PackagingSpec { get; set; }
    public string? Package { get; set; }
    public decimal? ProductQuantity { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Weight { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string? DeliveryDateShamsi => TimeCalculator.ConvertToShamsi(DeliveryDate);
    public long? ShippingCostId { get; set; }
    public decimal? ShippingCost { get; set; }
    public string? ExitInvoice { get; set; }
    public string? Invoices { get; set; }
    public string? Channel { get; set; }
    public decimal? Height { get; set; }
    public decimal? Width { get; set; }
    public decimal? Length { get; set; }
    public decimal? PackagingVolume => (Height ?? 1) * (Width ?? 1) * (Length ?? 1);
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public string? InvoiceNumber { get; set; }
    public string? CompanyCode { get; set; }
    public string? CompanyName { get; set; }
    public bool SecurityConfirm { get; set; }
    public string? SecurityConfirmTitle => SecurityConfirm == true ? "تایید" : "عدم تایید";
    public DateTime? SecurityConfirmDate { get; set; }
    public string? SecurityConfirmDateShamsi => TimeCalculator.ConvertToShamsi(SecurityConfirmDate);
}

public record GetWarehouseProjectsModel
{
    public long? TransportationProjectId { get; set; }
    public long? ProjectId { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectName { get; set; }
}

public record GetWarehouseCostCentersModel
{
    public long? TransportationCostCenterId { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterCode { get; set; }
    public string? CostCenterName { get; set; }
}

public record GetWarehouseTransportNumberPlate : PlateNumberHelper.IPlateNumberModel
{
    public string? Part1 { get; set; } = string.Empty;
    public string? Part2 { get; set; } = string.Empty;
    public string? Part3 { get; set; } = string.Empty;
    public string? Letter { get; set; } = string.Empty;
}


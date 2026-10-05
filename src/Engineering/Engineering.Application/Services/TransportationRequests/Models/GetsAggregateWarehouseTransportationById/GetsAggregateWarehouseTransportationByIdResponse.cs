using Engineering.Application.Extensions.PlateHelper;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Transportations.Enums;
using static Engineering.Application.Extensions.PlateHelper.PlateNumberHelper;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationById;

public record GetsAggregateWarehouseTransportationByIdResponse()
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public TransportationRequestStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
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
    public string? CarSpec { get; set; }
    public long? DriverId { get; set; }
    public string? DriverFullName { get; set; }
    public string? Driver { get; set; }
    public string? DriverPhoneNumber { get; set; }
    public string? GearBoxNumber { get; set; }
    public string? CertificateNumber { get; set; }
    public string? NumberPlate { get; set; }
    public GetAggregateWarehouseTransportNumberPlateById? NumberPlateModel => !string.IsNullOrEmpty(NumberPlate) ?
        NumberPlate.ToPlateModel<GetAggregateWarehouseTransportNumberPlateById>() : null;
    public long? TransportationContractorId { get; set; }
    public TransportationContractorCalculateType? CalculateType { get; set; }
    public string? CalculateTypeTitle => CalculateType?.GetEnumDescription();
    public long? ThirdPartyId { get; set; }
    public string? MainName { get; set; } = string.Empty;
    public string? MainCode { get; set; } = string.Empty;
    public string? MainAddress { get; set; } = string.Empty;
    public string? ContractorPhoneNumber { get; set; }
    public string? FreightNumber { get; set; }
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
    public GetAggregateTransportWarehouseDetailByIdModel? ExtraInfo { get; set; }
    public List<GetByIdAggregateDocuments>? Documents { get; set; }
}

public record GetByIdAggregateDocuments
{
    public long Id { get; set; }
    public bool IsBill { get; set; }
    public string Url { get; set; } = string.Empty;
}
public record GetAggregateTransportWarehouseDetailByIdModel
{
    public long Id { get; set; }
    public string? GlobalFreightNumber { get; set; }
    public string? ClassifiedFreightNumber { get; set; }
    public decimal? Tax { get; set; }
    public decimal? TransferPrice { get; set; }
    public decimal? ServicePrice { get; set; }
    public string? InsuranceNumber { get; set; }
    public decimal? InsurancePrice { get; set; }
    public decimal? ShippingCost { get; set; }
    public decimal? ProductTotalPrice { get; set; }
    public decimal? OutofRange { get; set; }
    public string? OrderNumber { get; set; }
}

public record GetAggregateWarehouseTransportNumberPlateById : IPlateNumberModel
{
    public string? Part1 { get; set; } = string.Empty;
    public string? Part2 { get; set; } = string.Empty;
    public string? Part3 { get; set; } = string.Empty;
    public string? Letter { get; set; } = string.Empty;
}

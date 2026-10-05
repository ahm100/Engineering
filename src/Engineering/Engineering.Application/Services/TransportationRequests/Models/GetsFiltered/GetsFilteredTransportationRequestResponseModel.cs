using Engineering.Application.Services.TransportationRequests.Models.GetById;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsFiltered;

public record GetsFilteredTransportationRequestResponseModel
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public string? CostCenterNames { get; set; }
    public string? ProjectNames { get; set; }
    public TransportationRequestStatus? TransportationRequestStatus { get; set; }
    public string? TransportationRequestStatusTitle => TransportationRequestStatus?.GetEnumDescription();
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public long? TransportationId { get; set; }
    public string? TransportationName { get; set; }
    public bool? IsPassenger { get; set; }
    public long? TripId { get; set; }
    public string? TripName { get; set; }
    public long? BillOfLadingId { get; set; }
    public string? BillOfLadingName { get; set; }
    public long? RequestById { get; set; }
    public string? RequestByName { get; set; }
    public decimal? Price { get; set; }
    public long? CurrencyUnitId { get; set; }
    public string? CurrencyUnitName { get; set; }
    public string? ManagerDescription { get; set; }
    public long? StartingCityId { get; set; }
    public string? StartingCityName { get; set; }
    public long? DestinationCityId { get; set; }
    public string? DestinationCityName { get; set; }
    public long? SecondDestinationCityId { get; set; }
    public string? SecondDestinationCityName { get; set; }
    public string? CarID { get; set; }
    public long? ConfirmUserId { get; set; }
    public string? ConfirmUser { get; set; }
    public DateTime? ConfirmDate { get; set; }
    public string? ConfirmDateShamsi => TimeCalculator.ConvertToShamsi(ConfirmDate);
    public long? TransportationCostGroupId { get; set; }
    public string? TransportationCostGroupTitle { get; set; }
    public string? TransportationCostGroupCode { get; set; }
    public long? TransportationCostCategoryId { get; set; }
    public string? TransportationCostCategoryTitle { get; set; }
    public string? TransportationCostCategoryCode { get; set; }
    public List<GetTransportationRequestCostCenter>? CostCenters { get; set; }
    public List<GetTransportationRequestProject>? Projects { get; set; }
    public List<GetTransportationRequestProjectOperation>? ProjectOperations { get; set; }
    public List<GetTransportationRequestProjectOperationDetail>? ProjectOperationDetails { get; set; }
    public string? Description { get; set; }
    public string? StartingCityAddress { get; set; }
    public string? DestinationAddress { get; set; }
    public long? DriverId { get; set; }
    public string? DriverUserName { get; set; }
    public string? DriverName { get; set; }
    public TransportationPaymentType? PaymentType { get; set; }
    public string? PaymentTypeTitle => PaymentType?.GetEnumDescription();
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDateShamsi => TimeCalculator.ConvertToShamsi(PaymentDate);
    public string? CardNumber { get; set; }
    public string? AccountName { get; set; }
    public string? IBAN { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public long? TicketPayerId { get; set; }
    public string? TicketPayer { get; set; }
    public bool? IsAirPlane => TicketPayerId != null && TicketPayerId > 0 ? true : false;
    public long? PrefernialId { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public long? TransportationContractorId { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? MainName { get; set; } = string.Empty;
    public string? ContractorPhoneNumber { get; set; }
    public long? AddressId { get; set; }
    public long? CityId { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string? PostalCode { get; set; }
    public List<GetFilteredRequestWarehousesModel>? Warehouses { get; set; }
}

public record GetFilteredRequestWarehousesModel
{
    public long? Id { get; set; }
    public long? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public string? WarehouseCode { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; }
    public string? ThirdPartyName { get; set; }
    public long? PackingId { get; set; }
    public long? PackingNumber { get; set; }
    public decimal? Price { get; set; }
    public long? ShippingCostId { get; set; }
    public decimal? ShippingCostPrice { get; set; }
    public List<GetFilteredRequestWarehousesProductModel>? WarehouseProducts { get; set; }
}

public record GetFilteredRequestWarehousesProductModel
{
    public long? Id { get; set; }
    public int? Quantity { get; set; }
    public long? ProductId { get; set; }
    public string? Product { get; set; }
    public string? PalletNumber { get; set; }
}
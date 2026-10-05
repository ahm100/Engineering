
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetById;

public record GetTransportationRequestByIdResponse()
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public string? CostCenterNames { get; set; }
    public string? ProjectNames { get; set; }
    public long? TransportationId { get; set; }
    public string? TransportationName { get; set; }
    public bool? IsPassenger { get; set; }
    public long? TripId { get; set; }
    public string? TripName { get; set; }
    public long? MachineTypeId { get; set; }
    public string? MachineTypeName { get; set; }
    public long? BillOfLadingId { get; set; }
    public string? BillOfLadingName { get; set; }
    public long? RequestById { get; set; }
    public string? RequestByName { get; set; }
    public long? StartingCityId { get; set; }
    public string? StartingCityName { get; set; }
    public long? DestinationCityId { get; set; }
    public string? DestinationCityName { get; set; }
    public long? SecondDestinationCityId { get; set; }
    public string? SecondDestinationCityName { get; set; }
    public string? ImageLink { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Description { get; set; }
    public long? DriverId { get; set; }
    public string? DriverUserName { get; set; }
    public string? DriverName { get; set; }
    public string? PostageDate { get; set; }
    public string? ReceivedDate { get; set; }
    public string? BillOfLadingImage { get; set; }
    public string? DelivererName { get; set; }
    public string? RecipientName { get; set; }
    public string? FreightNumber { get; set; }
    public decimal? LoadWeight { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CarSpecifications { get; set; }
    public string? NumberPlates { get; set; }
    public TransportationRequestNumberPlatesModel? NumberPlatesModel { get; set; }
    public string? AccountNumber { get; set; }
    public long? BankId { get; set; }
    public string? BankName { get; set; }
    public string? CardNumber { get; set; }
    public string? AccountName { get; set; }
    public string? IBAN { get; set; }
    public decimal? Price { get; set; }
    public long? CurrencyUnitId { get; set; }
    public string? CurrencyUnitName { get; set; }
    public string? AccountDescription { get; set; }
    public string? CarID { get; set; }
    public TransportationRequestTypesModel? StatusData { get; set; }
    public string? ManagerDescription { get; set; }
    public long? ConfrimUserId { get; set; }
    public string? ConfrimName { get; set; }
    public DateTime? ConfrimDate { get; set; }
    public string? ConfrimDateShamsi => TimeCalculator.ConvertToShamsi(ConfrimDate);
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
    public List<GetTransportationRequestDocumentByIdDocumentModel>? Documents { get; set; }
    public string? StartingCityAddress { get; set; }
    public string? DestinationAddress { get; set; }
    public long? SeasonId { get; set; }
    public string? Season { get; set; }
    public long? BranchId { get; set; }
    public string? Branch { get; set; }
    public long? CategoryId { get; set; }
    public string? Category { get; set; }
    public TransportationPaymentType? PaymentType { get; set; }
    public string? PaymentTypeTitle => PaymentType?.GetEnumDescription();
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDateShamsi => TimeCalculator.ConvertToShamsi(PaymentDate);
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public long? TicketPayerId { get; set; }
    public string? TicketPayer { get; set; }
    public bool? IsAirPlane => TicketPayerId != null && TicketPayerId > 0 ? true : false;
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
    public List<GetRequestWarehousesByIdModel>? Warehouses { get; set; }
}

public record GetRequestWarehousesByIdModel
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
    public List<GetRequestWarehousesProductByIdModel>? WarehouseProducts { get; set; }
}

public record GetRequestWarehousesProductByIdModel
{
    public long? Id { get; set; }
    public int? Quantity { get; set; }
    public long? ProductId { get; set; }
    public string? Product { get; set; }
    public string? PalletNumber { get; set; }
}
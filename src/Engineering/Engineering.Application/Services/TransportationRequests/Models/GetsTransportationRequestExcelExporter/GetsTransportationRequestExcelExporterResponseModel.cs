namespace Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelExporter;

public record GetsTransportationRequestExcelExporterResponseModel
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public string TransportationRequestStatusTitle { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string TransportationName { get; set; } = string.Empty;
    public bool IsPassenger { get; set; }
    public string TripName { get; set; } = string.Empty;
    public string? BillOfLadingName { get; set; } = string.Empty;
    public long RequestById { get; set; }
    public string? RequestByName { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public long? CurrencyUnitId { get; set; }
    public string? CurrencyUnitName { get; set; } = string.Empty;
    public string? ManagerDescription { get; set; } = string.Empty;
    public long StartingCityId { get; set; }
    public string? StartingCityName { get; set; }
    public long DestinationCityId { get; set; }
    public string? DestinationCityName { get; set; }
    public string? CostCenterNames { get; set; } = string.Empty;
    public string? ProjectNames { get; set; } = string.Empty;
    public string? CarID { get; set; } = string.Empty;
    public long? ConfirmUserId { get; set; }
    public string? ConfirmUser { get; set; } = string.Empty;
    public string? ConfirmDateShamsi { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public long? TransportationCostGroupId { get; set; }
    public string? TransportationCostGroupTitle { get; set; }
    public string? TransportationCostGroupCode { get; set; }
    public long? TransportationCostCategoryId { get; set; }
    public string? TransportationCostCategoryTitle { get; set; }
    public string? TransportationCostCategoryCode { get; set; }
    public string? StartingCityAddress { get; set; }
    public string? DestinationAddress { get; set; }
    public string? TransportationPaymentType { get; set; }
    public string? PaymentDateShamsi { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public long? TicketPayerId { get; set; }
    public string? TicketPayer { get; set; }
    public bool? IsAirPlane { get; set; }
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
};
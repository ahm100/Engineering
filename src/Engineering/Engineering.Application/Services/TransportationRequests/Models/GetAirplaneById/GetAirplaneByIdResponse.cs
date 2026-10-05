using Engineering.Application.Services.TransportationRequests.Models.GetById;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;

public record GetAirplaneByIdResponse
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public string? CostCenterNames { get; set; }
    public string? ProjectNames { get; set; }
    public long? TransportationId { get; set; }
    public string? TransportationName { get; set; }
    public TransportationType? TransportationType { get; set; }
    public string? TransportationTypeDescription => TransportationType.GetEnumDescription();
    public long? TripId { get; set; }
    public string? TripName { get; set; }
    public long? StartingCityId { get; set; }
    public string? StartingCityName { get; set; }
    public long? DestinationCityId { get; set; }
    public string? DestinationCityName { get; set; }
    public long? PassengerId { get; set; }
    public string? Passenger { get; set; }
    public string? PassengerName { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Description { get; set; }
    public string? AccountNumber { get; set; }
    public long? BankId { get; set; }
    public string? BankName { get; set; }
    public string? Iban { get; set; }
    public string? CardNumber { get; set; }
    public long? TicketPayerId { get; set; }
    public string? TicketPayer { get; set; }
    public long? CurrencyUnitId { get; set; }
    public string? CurrencyUnitName { get; set; }
    public AirplaneTypesModel? StatusData { get; set; }
    public TransportationRequestStatus TransportationRequestStatus { get; set; }
    public string? TransportationRequestStatusTitle => TransportationRequestStatus.GetEnumDescription();
    public string? StartDateShamsi { get; set; }
    public string? EndDateShamsi { get; set; }
    public string? DestinationAddress { get; set; }
    public decimal? FareAmount { get; set; }
    public long? TransportationCostGroupId { get; set; }
    public string? TransportationCostGroupTitle { get; set; }
    public string? TransportationCostGroupCode { get; set; }
    public long? TransportationCostCategoryId { get; set; }
    public string? TransportationCostCategoryTitle { get; set; }
    public string? TransportationCostCategoryCode { get; set; }
    public List<GetTransportationRequestCostCenter>? CostCenters { get; set; }
    public List<GetTransportationRequestProject>? Projects { get; set; }
    public List<GetAirplaneProjectOperation>? ProjectOperations { get; set; }
    public List<GetAirplaneProjectOperationDetail>? ProjectOperationDetails { get; set; }
    public List<GetAirplaneDocumentByIdDocumentModel>? Documents { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public string? ManagerDescription { get; set; }
    public long? ConfrimUserId { get; set; }
    public string? ConfrimName { get; set; }
    public DateTime? ConfrimDate { get; set; }
    public string? ConfrimDateShamsi => TimeCalculator.ConvertToShamsi(ConfrimDate);
    public TransportationPaymentType? PaymentType { get; set; }
    public string? PaymentTypeTitle => PaymentType?.GetEnumDescription();
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDateShamsi => TimeCalculator.ConvertToShamsi(PaymentDate);
    public string? AccountName { get; set; }
    public long? SeasonId { get; set; }
    public string? Season { get; set; }
    public long? BranchId { get; set; }
    public string? Branch { get; set; }
    public long? CategoryId { get; set; }
    public string? Category { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
}



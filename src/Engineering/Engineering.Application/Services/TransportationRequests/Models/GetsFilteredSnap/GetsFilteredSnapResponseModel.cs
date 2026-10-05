using Engineering.Application.Services.TransportationRequests.Models.GetById;
using Engineering.Application.Services.TransportationRequests.Models.GetSnapById;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredSnap;

public record GetsFilteredSnapResponseModel
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public string? CostCenterNames { get; set; }
    public string? ProjectNames { get; set; }
    public TransportationRequestStatus? TransportationRequestStatus { get; set; }
    public string? TransportationRequestStatusTitle => TransportationRequestStatus?.GetEnumDescription();
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? StartDateShamsi { get; set; }
    public string? EndDateShamsi { get; set; }
    public long? TransportationId { get; set; }
    public string? TransportationName { get; set; }
    public bool? IsPassenger { get; set; }
    public long? TripId { get; set; }
    public string? TripName { get; set; }
    public long? SnapRequester { get; set; }
    public string? SnapRequesterName { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public long? CurrencyUnitId { get; set; }
    public string? CurrencyUnitName { get; set; }
    public long? StartingCityId { get; set; }
    public string? StartingCityName { get; set; }
    public string? StartingCityAddress { get; set; }
    public long? DestinationCityId { get; set; }
    public string? DestinationCityName { get; set; }
    public string? DestinationAddress { get; set; }
    public long? SecondDestinationCityId { get; set; }
    public string? SecondDestinationCityName { get; set; }
    public string? SecondDestinationAddress { get; set; }
    public string? Description { get; set; }
    public long? DriverId { get; set; }
    public string? DriverUserName { get; set; }
    public string? DriverName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CarSpecifications { get; set; }
    public string? NumberPlates { get; set; }
    public SnapNumberPlatesModel? NumberPlatesModel { get; set; }
    public decimal? FareAmount { get; set; }
    public int? StopRate { get; set; }
    public bool? PersonalPayment { get; set; }
    public bool? ReturnToStart { get; set; }
    public long? TransportationCostGroupId { get; set; }
    public string? TransportationCostGroupTitle { get; set; }
    public string? TransportationCostGroupCode { get; set; }
    public long? TransportationCostCategoryId { get; set; }
    public string? TransportationCostCategoryTitle { get; set; }
    public string? TransportationCostCategoryCode { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public SnapTypesModel? StatusData { get; set; }
    public string? ManagerDescription { get; set; }
    public long? ConfrimUserId { get; set; }
    public string? ConfrimName { get; set; }
    public DateTime? ConfrimDate { get; set; }
    public string? ConfrimDateShamsi => TimeCalculator.ConvertToShamsi(ConfrimDate);
    public string? RecipientName { get; set; }
    public List<GetTransportationRequestCostCenter>? CostCenters { get; set; }
    public List<GetTransportationRequestProject>? Projects { get; set; }
    public List<GetSnapProjectOperation>? ProjectOperations { get; set; }
    public List<GetSnapProjectOperationDetail>? ProjectOperationDetails { get; set; }
    public List<GetSnapDocumentByIdDocumentModel>? Documents { get; set; }
    public TransportationPaymentType? PaymentType { get; set; }
    public string? PaymentTypeTitle => PaymentType?.GetEnumDescription();
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDateShamsi => TimeCalculator.ConvertToShamsi(PaymentDate);
    public long? PrefernialId { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
}


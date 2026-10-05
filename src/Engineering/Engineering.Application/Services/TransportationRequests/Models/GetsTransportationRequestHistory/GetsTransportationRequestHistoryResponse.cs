using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;

public record GetsTransportationRequestHistoryResponse()
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public long? TransportationId { get; set; }
    public string? TransportationName { get; set; }
    public long? TripId { get; set; }
    public string? TripName { get; set; }
    public long? MachineTypeId { get; set; }
    public string MachineTypeName { get; set; }
    public List<GetsTransportationRequestHistoryResponseModel> Data { get; set; }
    public int RowCount { get; set; }
}


public record GetsTransportationRequestHistoryResponseModel()
{
    public long? Id { get; set; }
    public long? RequestNumber { get; set; }
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public string? Description { get; set; } = string.Empty;
    public long? DriverId { get; set; }
    public string? DriverFullName { get; set; }
    public string? DriverName { get; set; }
    public string? AccountNumber { get; set; }
    public long? BankId { get; set; }
    public string? BankName { get; set; }
    public string? CardNumber { get; set; }
    public string? AccountName { get; set; }
    public string? IBAN { get; set; }
    public decimal? Price { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public string? AccountDescription { get; set; } = string.Empty;
    public TransportationRequestStatus? TransportationRequestStatus { get; set; }
    public string? StatusDescription => TransportationRequestStatus.GetEnumDescription();
    public TransportationPaymentType? TransportationPaymentType { get; set; }
    public string? PaymentTypeDescription => TransportationPaymentType.GetEnumDescription();
    public string? ManagerDescription { get; set; } = string.Empty;
    public long? ConfirmUserId { get; set; }
    public string? ConfirmUser { get; set; }
    public DateTime? ConfirmDate { get; set; }
    public string? ConfirmDateShamsi => TimeCalculator.ConvertToShamsi(ConfirmDate);
    public long? PaymentOrderId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDateShamsi => TimeCalculator.ConvertToShamsi(PaymentDate);
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
}

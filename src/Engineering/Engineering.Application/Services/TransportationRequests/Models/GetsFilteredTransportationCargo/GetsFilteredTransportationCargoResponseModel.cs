using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Gita.Backend.Shared.Domain.Enums.SaleChannels;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredTransportationCargo;

public record GetsFilteredTransportationCargoResponseModel
{
    public long Id { get; set; }
    public long? PackingNumber { get; set; }
    public long? PackingId { get; set; }
    public PackingStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public long? ThirdPartyId { get; set; }
    public string? ThirdPartyFullName { get; set; }
    public string? ThirdParty { get; set; }
    public DateTime? CargoCreated { get; set; }
    public string? CargoCreatedMiladi => TimeCalculators.DatePiker(CargoCreated);
    public string? CargoCreatedShamsi => TimeCalculator.ConvertToShamsi(CargoCreated);
    [System.Text.Json.Serialization.JsonIgnore]
    public IEnumerable<string>? TransportationRequestNumbers { get; set; }
    public string? TransportationRequestNumber => TransportationRequestNumbers.JoinListDiscComma();
    [System.Text.Json.Serialization.JsonIgnore]
    public IEnumerable<string>? PalletNumbers { get; set; }
    public string? PalletNumber => PalletNumbers.JoinListDiscComma();
    public DateTime? DeliveryDate { get; set; }
    public string? DeliveryDateShamsi => TimeCalculator.ConvertToShamsi(DeliveryDate);
    public string? ExitInvoice { get; set; }
    public SalesChannelType? SalesChannelType { get; set; }
    public string? SalesChannelTypeTitle => SalesChannelType?.GetEnumDescription();
    public string? Channels { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? CompanyCode { get; set; }
    public string? CompanyName { get; set; }
    public bool SecurityConfirm { get; set; }
    public string? SecurityConfirmTitle => SecurityConfirm == true ? "تایید" : "عدم تایید";
    public DateTime? SecurityConfirmDate { get; set; }
    public string? SecurityConfirmDateShamsi => TimeCalculator.ConvertToShamsi(SecurityConfirmDate);
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public bool CargoSecurityConfirm { get; set; }
    public string? CargoSecurityConfirmTitle => CargoSecurityConfirm == true ? "تایید حراست برای خروج" : "عدم تایید حراست برای خروج";
    public DateTime? CargoSecurityConfirmDate { get; set; }
    public string? CargoSecurityConfirmDateShamsi => TimeCalculator.ConvertToShamsi(CargoSecurityConfirmDate);
    public IEnumerable<CargoDcoumentsDto>? CargoDocs { get; set; }
    public List<CargoDcoumentsDto>? CargoDocuments => CargoDocs?.ToList() ?? [];
    public PalletTransportStatus? PalletTransportStatus { get; set; }
    public string? PalletTransportStatusTitle => PalletTransportStatus?.GetEnumDescription();
    [System.Text.Json.Serialization.JsonIgnore]
    public List<string?>? SourceAddresses { get; set; }
    public string? SourceAddress => string.Join(", ", SourceAddresses?.Where(z => !string.IsNullOrEmpty(z)).Distinct().ToList() ?? []);
    [System.Text.Json.Serialization.JsonIgnore]
    public List<string?>? DesAddresses { get; set; }
    public string? DesAddress => string.Join(", ", DesAddresses?.Where(z => !string.IsNullOrEmpty(z)).Distinct().ToList() ?? []);
    [System.Text.Json.Serialization.JsonIgnore]
    public List<long?>? PermitNumbers { get; set; }
    public string? PermitNumber => string.Join(", ", PermitNumbers?.Where(z => z != null && z > 0).Distinct().ToList() ?? []);

}

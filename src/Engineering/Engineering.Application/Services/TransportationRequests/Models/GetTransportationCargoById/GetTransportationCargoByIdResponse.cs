using Engineering.Application.Extensions.PlateHelper;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Gita.Backend.Shared.Domain.Enums.SaleChannels;
using static Engineering.Application.Extensions.PlateHelper.PlateNumberHelper;

namespace Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoById;

public record GetTransportationCargoByIdResponse
{
    public long Id { get; set; }
    public long? PackingNumber { get; set; }
    public long? PackingId { get; set; }
    public PackingStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public long? ThirdPartyId { get; set; }
    public string? ThirdPartyFullName { get; set; }
    public string? ThirdParty { get; set; }
    public long? TransportationContractorId { get; set; }
    public string? TransportationContractorTitle { get; set; }
    public string? TransportationContractorLegal { get; set; }
    public string? TransportationContractorName { get; set; }
    public DeliveryMethod? DeliveryMethod { get; set; }
    public string? DeliveryMethodTitle => DeliveryMethod?.GetEnumDescription();
    public DeliveryType? DeliveryType { get; set; }
    public string? DeliveryTypeTitle => DeliveryType?.GetEnumDescription();
    public PackingShippingType? PackingShippingType { get; set; }
    public string? PackingShippingTypeTitle => PackingShippingType?.GetEnumDescription();
    public string? VehicleName { get; set; }
    public string? NumberPlate { get; set; }
    public CargoShippingNumberPlateByIdDataModel? PlateModel => NumberPlate.ToPlateModel<CargoShippingNumberPlateByIdDataModel>();
    public string? Driver { get; set; }
    public string? DriverPhoneNumber { get; set; }
    public DateTime? PostageDate { get; set; }
    public string? PostageDateMiladi => TimeCalculators.DatePiker(PostageDate);
    public string? PostageDateShamsi => TimeCalculator.ConvertToShamsi(PostageDate);
    public DateTime? CargoCreated { get; set; }
    public string? CargoCreatedMiladi => TimeCalculators.DatePiker(CargoCreated);
    public string? CargoCreatedShamsi => TimeCalculator.ConvertToShamsi(CargoCreated);
    [JsonIgnore]
    public IEnumerable<string>? TransportationRequestNumbers { get; set; }
    public string? TransportationRequestNumber => TransportationRequestNumbers.JoinListDiscComma();
    [JsonIgnore]
    public IEnumerable<string>? PalletNumbers { get; set; }
    public string? PalletNumber => PalletNumbers.JoinListDiscComma();
    public DateTime? DeliveryDate { get; set; }
    public string? DeliveryDateShamsi => TimeCalculator.ConvertToShamsi(DeliveryDate);
    public string? ExitInvoice { get; set; }
    public SalesChannelType? SalesChannelType { get; set; }
    public string? SalesChannelTypeTitle => SalesChannelType?.GetEnumDescription();
    public string? channels { get; set; }
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
}

public record CargoShippingNumberPlateByIdDataModel : IPlateNumberModel
{
    public string? Part1 { get; set; } = string.Empty;
    public string? Part2 { get; set; } = string.Empty;
    public string? Part3 { get; set; } = string.Empty;
    public string? Letter { get; set; } = string.Empty;
}

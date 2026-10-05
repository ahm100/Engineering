using Engineering.Application.Extensions.PlateHelper;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packages;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Gita.Backend.Shared.Domain.Enums.SaleChannels;
using static Engineering.Application.Extensions.PlateHelper.PlateNumberHelper;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;

public record GetsTransportationCargoPalletResponseModel
{
    public long Id { get; set; }
    public long? CargoId { get; set; }
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
    public PalletShippingNumberPlateDataModel? PlateModel => NumberPlate.ToPlateModel<PalletShippingNumberPlateDataModel>();
    public string? Driver { get; set; }
    public string? DriverPhoneNumber { get; set; }
    public DateTime? PostageDate { get; set; }
    public string? PostageDateMiladi => TimeCalculators.DatePiker(PostageDate);
    public string? PostageDateShamsi => TimeCalculator.ConvertToShamsi(PostageDate);
    public DateTime? CargoCreated { get; set; }
    public string? CargoCreatedMiladi => TimeCalculators.DatePiker(CargoCreated);
    public string? CargoCreatedShamsi => TimeCalculator.ConvertToShamsi(CargoCreated);
    public long? TransportationRequestId { get; set; }
    public long? TransportationRequestNumber { get; set; }
    public long? PackingPalletId { get; set; }
    public string? PalletNumber { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public PackagingSpecType? PackagingSpecType { get; set; }
    public string? PackagingSpecTypeTitle => PackagingSpecType?.GetEnumDescription();
    public PalletPermitStatus? PalletPermitStatus { get; set; }
    public string? PalletPermitStatusTitle => PalletPermitStatus?.GetEnumDescription();
    public long? PermitNumber { get; set; }
    public DateTime? PermitDate { get; set; }
    public string? PermitDateShamsi => TimeCalculator.ConvertToShamsi(PermitDate);
    public string? PackagingSpecTitle { get; set; }
    public decimal PackagingSpecQuantity { get; set; }
    public decimal? PackagingSpecLength { get; set; }
    public decimal? PackagingSpecWidth { get; set; }
    public decimal? PackagingSpecHeight { get; set; }
    public decimal? PackagingSpecWeight { get; set; }
    public decimal? PackagingSpecRatio { get; set; }
    public long? PackagingSpecMeasureUnitId { get; set; }
    public string? PackagingSpecMeasureUnit { get; set; }
    public long? PackingSourceAddressId { get; set; }
    public long? PackingSourceAddressCityId { get; set; }
    public string? PackingSourceAddressCity { get; set; }
    public long? PackingSourceAddressWarehouseId { get; set; }
    public string? PackingSourceAddressWarehouse { get; set; }
    public string? PackingSourceAddress { get; set; }
    public long? PackingDestinationAddressId { get; set; }
    public long? PackingDestinationAddressCityId { get; set; }
    public string? PackingDestinationAddressCity { get; set; }
    public long? PackingDestinationAddressWarehouseId { get; set; }
    public string? PackingDestinationAddressWarehouse { get; set; }
    public string? PackingDestinationAddress { get; set; }
    public string? PackingDestinationPhoneNumber { get; set; }
    public string? PackingDestinationPostalCode { get; set; }
    public decimal? PalletPrice { get; set; }
    public decimal? PalletTransferPrice { get; set; }
    public decimal? PalletWeight { get; set; }
    public decimal? PalletQuantity { get; set; }
    public long? ShippingCostId { get; set; }
    public decimal? ShippingCostPrice { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedMiladi => TimeCalculators.DatePiker(Created);
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public DateTime? DeliveryDate { get; set; }
    public string? DeliveryDateShamsi => TimeCalculator.ConvertToShamsi(DeliveryDate);
    public string? ExitInvoice { get; set; }
    public SalesChannelType? SalesChannelType { get; set; }
    public string? SalesChannelTypeTitle => SalesChannelType?.GetEnumDescription();
    public string? channels { get; set; }
    public decimal? PackagingVolume => (PackagingSpecHeight ?? 1) * (PackagingSpecWidth ?? 1) * (PackagingSpecLength ?? 1);
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
    public List<CargoDcoumentsDto>? CargoDocuments { get; set; } = [];
    public List<TransportationRequestWarehousePalletDto>? PalletProducts { get; set; } = [];
}

public record CargoDcoumentsDto
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
}

public class TransportationRequestWarehousePalletDto
{
    public long Id { get; set; }
    public long? TransportationCargoPalletId { get; set; }
    public long? PackingProductId { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? PalletNumber { get; set; }
    public long? PackingSourceAddressId { get; set; }
    public long? PackingSourceAddressCityId { get; set; }
    public string? PackingSourceAddressCity { get; set; }
    public long? PackingSourceAddressWarehouseId { get; set; }
    public string? PackingSourceAddressWarehouse { get; set; }
    public string? PackingSourceAddress { get; set; }
    public long? PackingDestinationAddressId { get; set; }
    public long? PackingDestinationAddressCityId { get; set; }
    public string? PackingDestinationAddressCity { get; set; }
    public long? PackingDestinationAddressWarehouseId { get; set; }
    public string? PackingDestinationAddressWarehouse { get; set; }
    public string? PackingDestinationAddress { get; set; }
    public string? PackingDestinationPhoneNumber { get; set; }
    public string? PackingDestinationPostalCode { get; set; }
    public decimal? Price { get; set; }
    public decimal? Quantity { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedMiladi => TimeCalculators.DatePiker(Created);
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);

    public PalletPermitStatus? PalletPermitStatus { get; set; }
    public string? PalletPermitStatusTitle => PalletPermitStatus?.GetEnumDescription();
    public long? PermitNumber { get; set; }
    public DateTime? PermitDate { get; set; }
    public string? PermitDateShamsi => TimeCalculator.ConvertToShamsi(PermitDate);
}

public class PalletShippingNumberPlateDataModel : IPlateNumberModel
{
    public string? Part1 { get; set; } = string.Empty;
    public string? Part2 { get; set; } = string.Empty;
    public string? Part3 { get; set; } = string.Empty;
    public string? Letter { get; set; } = string.Empty;
}


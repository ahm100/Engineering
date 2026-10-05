using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Domain.Entities.Transportations;

public class TransportationCargoPallet : AuditableEntity<TransportationCargoPallet>
{
    [Description(TransportationRequestWarehouseComment.TransportationRequest)]
    public long TransportationCargoId { get; set; }
    public TransportationCargo TransportationCargo { get; set; }

    [Description(TransportationRequestWarehouseComment.TransportationRequest)]
    public long? TransportationRequestId { get; set; }
    public TransportationRequest? TransportationRequest { get; set; }

    [Description(TransportationRequestWarehouseComment.PalletNumber)]
    public string PalletNumber { get; set; }
    [Description(TransportationRequestWarehouseComment.PackingPalletId)]
    public long PackingPalletId { get; set; }
    public ViewPackingPallet PackingPallet { get; set; }

    [Description(TransportationRequestWarehouseComment.PackingAddressId)]
    public long? PackingSourceAddressId { get; set; }
    public ViewPackingAddress? PackingSourceAddress { get; set; }

    [Description(TransportationRequestWarehouseComment.PackingAddressId)]
    public long? PackingDestinationAddressId { get; set; }
    public ViewPackingAddress? PackingDestinationAddress { get; set; }

    [Description(TransportationRequestWarehouseComment.Price)]
    public decimal? Price { get; set; }

    [Description(TransportationRequestWarehouseComment.Price)]
    public decimal? TransferPrice { get; set; }

    [Description(TransportationRequestWarehouseComment.Weight)]
    public decimal? Weight { get; set; }

    [Description(TransportationRequestWarehouseComment.Quantity)]
    public decimal? Quantity { get; set; }

    [Description(TransportationRequestWarehouseComment.ShippingCostId)]
    public long? ShippingCostId { get; set; }
    public ShippingCost? ShippingCost { get; set; }

    public long? RefrenceId { get; set; }

    [Description(TransportationContractorCmts.TransportationContractorId)]
    public long? TransportationContractorId { get; private set; }

    [Description(TransportationContractorCmts.TransportationContractor)]
    public TransportationContractor? TransportationContractor { get; private set; }

    [Description(TransportationContractorCmts.DeliveryMethod)]
    public DeliveryMethod? DeliveryMethod { get; private set; }

    [Description(TransportationContractorCmts.DeliveryType)]
    public DeliveryType? DeliveryType { get; private set; }

    [Description(TransportationContractorCmts.VehicleName)]
    public string? VehicleName { get; private set; }

    [Description(TransportationContractorCmts.PostageDate)]
    public DateTime? PostageDate { get; private set; }

    [Description(TransportationContractorCmts.NumberPlate)]
    public string? NumberPlate { get; private set; }

    [Description(TransportationContractorCmts.Driver)]
    public string? Driver { get; private set; }

    [Description(TransportationContractorCmts.DriverPhoneNumber)]
    public string? DriverPhoneNumber { get; private set; }

    [Description(TransportationContractorCmts.PackingShippingType)]
    public PackingShippingType? PackingShippingType { get; private set; }
    public TransportationCargoPallet(
        TransportationCargo transportationCargo,
        TransportationRequest? transportationRequest,
        decimal? price,
        decimal? transferPrice,
        decimal? weight,
        decimal? quantity,
        string palletNumber,
        ShippingCost? shippingCost,
        long? sourceAddressId,
        long? destinationAddressId,
        long packingPalletId,
        TransportationContractor? transportationContractor,
        DeliveryMethod? deliveryMethod,
        DeliveryType? deliveryType,
        string? vehicleName,
        DateTime? postageDate,
        string? numberPlate,
        string? driver,
        string? driverPhoneNumber,
        PackingShippingType? packingShippingType
        ) : this()
    {
        SetTransportationCargo(transportationCargo);
        SetTransportRequest(transportationRequest);
        SetPrice(price);
        SetTransferPrice(transferPrice);
        SetShipping(shippingCost);
        SetPalletNumber(palletNumber);
        SetWeight(weight);
        SetQuantity(quantity);
        SetPackingPalletId(packingPalletId);
        SetPackingSourceAddressId(sourceAddressId);
        SetPackingDestinationAddressId(destinationAddressId);
        SetTransportationContractorId(transportationContractor);
        SetDeliveryMethod(deliveryMethod);
        SetDeliveryType(deliveryType);
        SetVehicleName(vehicleName);
        SetPostageDate(postageDate);
        SetNumberPlate(numberPlate);
        SetDriver(driver);
        SetDriverPhoneNumber(driverPhoneNumber);
        SetPackingShippingType(packingShippingType);
    }

    public void Update(
        TransportationCargo transportationCargo,
        TransportationRequest? transportationRequest,
        decimal? price,
        decimal? transferPrice,
        decimal? weight,
        decimal? quantity,
        string palletNumber,
        ShippingCost? shippingCost,
        long? sourceAddressId,
        long? destinationAddressId,
        long packingPalletId,
        TransportationContractor? transportationContractor,
        DeliveryMethod? deliveryMethod,
        DeliveryType? deliveryType,
        string? vehicleName,
        DateTime? postageDate,
        string? numberPlate,
        string? driver,
        string? driverPhoneNumber,
        PackingShippingType? packingShippingType
        )
    {
        SetTransportationCargo(transportationCargo);
        SetTransportRequest(transportationRequest);
        SetPrice(price);
        SetTransferPrice(transferPrice);
        SetShipping(shippingCost);
        SetPalletNumber(palletNumber);
        SetWeight(weight);
        SetQuantity(quantity);
        SetPackingPalletId(packingPalletId);
        SetPackingSourceAddressId(sourceAddressId);
        SetPackingDestinationAddressId(destinationAddressId);
        SetTransportationContractorId(transportationContractor);
        SetDeliveryMethod(deliveryMethod);
        SetDeliveryType(deliveryType);
        SetVehicleName(vehicleName);
        SetPostageDate(postageDate);
        SetNumberPlate(numberPlate);
        SetDriver(driver);
        SetDriverPhoneNumber(driverPhoneNumber);
        SetPackingShippingType(packingShippingType);
    }

    public void SetPackingShippingType(PackingShippingType? value)
    {
        PackingShippingType = value;
    }

    public void SetDriverPhoneNumber(string? value)
    {
        DriverPhoneNumber = value;
    }

    public void SetDriver(string? value)
    {
        Driver = value;
    }

    public void SetNumberPlate(string? value)
    {
        NumberPlate = value;
    }

    public void SetPostageDate(DateTime? value)
    {
        PostageDate = value;
    }

    public void SetVehicleName(string? value)
    {
        VehicleName = value;
    }

    public void SetDeliveryType(DeliveryType? value)
    {
        DeliveryType = value;
    }

    public void SetDeliveryMethod(DeliveryMethod? value)
    {
        DeliveryMethod = value;
    }

    public void SetTransportationContractorId(TransportationContractor? value)
    {
        TransportationContractor = value;
        TransportationContractorId = value?.Id;
    }

    public void UpdateShippingPrice(
        decimal? transferPrice)
    {
        TransferPrice = transferPrice;
    }

    public void UpdateShippingPrice(
        ShippingCost? shippingCost,
        decimal? transferPrice)
    {
        ShippingCost = shippingCost;
        TransferPrice = transferPrice;
    }

    public void SetTransportRequest(TransportationRequest? request)
    {
        TransportationRequest = request;
        TransportationRequestId = request?.Id;
    }

    public void SetTransportationCargo(TransportationCargo request)
    {
        TransportationCargo = request;
        TransportationCargoId = request.Id;
    }

    public void SetRefrenceId(long? value)
    {
        RefrenceId = value;
    }

    public void SetPalletNumber(string value)
    {
        PalletNumber = value;
    }

    public void SetWeight(decimal? value)
    {
        Weight = value;
    }

    public void SetQuantity(decimal? value)
    {
        Quantity = value;
    }

    public void SetPackingSourceAddressId(long? value)
    {
        PackingSourceAddressId = value;
    }

    public void SetPackingDestinationAddressId(long? value)
    {
        PackingDestinationAddressId = value;
    }

    public void SetPackingPalletId(long value)
    {
        PackingPalletId = value;
    }

    public void SetWeight(decimal weight)
    {
        Weight = weight;
    }

    public void Remove()
    {
        IsDeleted = true;
    }

    public void SetShippingCost(ShippingCost? shippingCost)
    {
        ShippingCost = shippingCost;
        ShippingCostId = shippingCost?.Id;
    }

    public void SetShipping(ShippingCost? shippingCost)
    {
        ShippingCost = shippingCost;
        ShippingCostId = shippingCost?.Id;
    }

    public void SetShippingPrice(decimal? shippingPrice)
    {
        TransferPrice = shippingPrice ?? 0;
    }

    public void SetPrice(decimal? value)
    {
        Price = value;
    }

    public void SetTransferPrice(decimal? value)
    {
        TransferPrice = value;
    }

    public void AddProduct(TransportationRequestWarehouse product)
    {
        _transportationRequestWarehouses.Add(product);
    }

    private List<TransportationRequestWarehouse> _transportationRequestWarehouses;
    public IReadOnlyList<TransportationRequestWarehouse> TransportationRequestWarehouses => _transportationRequestWarehouses;

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationCargoPallet()
    {
        _transportationRequestWarehouses = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

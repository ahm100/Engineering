using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Domain.Entities.Transportations;

public class TransportationRequestWarehouse : AuditableEntity<TransportationRequestWarehouse>
{
    [Description(TransportationRequestWarehouseComment.TransportationCargoPallet)]
    public long TransportationCargoPalletId { get; set; }
    public TransportationCargoPallet TransportationCargoPallet { get; set; }

    [Description(TransportationRequestWarehouseComment.PackingProductId)]
    public long? PackingProductId { get; set; }
    public ViewPackingProduct? PackingProduct { get; set; }

    [Description(TransportationRequestWarehouseComment.PalletNumber)]
    public string? PalletNumber { get; set; }

    [Description(TransportationRequestWarehouseComment.PackingAddressId)]
    public long? PackingSourceAddressId { get; set; }
    public ViewPackingAddress? PackingSourceAddress { get; set; }

    [Description(TransportationRequestWarehouseComment.PackingAddressId)]
    public long? PackingDestinationAddressId { get; set; }
    public ViewPackingAddress? PackingDestinationAddress { get; set; }

    [Description(TransportationRequestWarehouseComment.Price)]
    public decimal? Price { get; set; }

    [Description(TransportationRequestWarehouseComment.Quantity)]
    public decimal? Quantity { get; set; }

    public TransportationRequestWarehouse(
        TransportationCargoPallet transportationCargoPallet,
        long? packingProductId,
        string? palletNumber,
        long? packingSourceAddressId,
        long? packingDestinationAddressId,
        decimal? price,
        decimal? quantity
        ) : this()
    {
        SetTransportationCargoPallet(transportationCargoPallet);
        SetPrice(price);
        SetPalletNumber(palletNumber);
        SetPackingProductId(packingProductId);
        SetQuantity(quantity);
        SetPackingSourceAddressId(packingSourceAddressId);
        SetPackingDestinationAddressId(packingDestinationAddressId);
    }

    public void Update(
        TransportationCargoPallet transportationCargoPallet,
        long? packingProductId,
        string? palletNumber,
        long? packingSourceAddressId,
        long? packingDestinationAddressId,
        decimal? price,
        decimal? quantity
        )
    {
        SetTransportationCargoPallet(transportationCargoPallet);
        SetPrice(price);
        SetPalletNumber(palletNumber);
        SetPackingProductId(packingProductId);
        SetQuantity(quantity);
        SetPackingSourceAddressId(packingSourceAddressId);
        SetPackingDestinationAddressId(packingDestinationAddressId);
    }

    public void SetTransportationCargoPallet(TransportationCargoPallet request)
    {
        TransportationCargoPallet = request;
        TransportationCargoPalletId = request.Id;
    }

    public void SetPalletNumber(string? value)
    {
        PalletNumber = value;
    }

    public void SetPackingProductId(long? value)
    {
        PackingProductId = value;
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

    public void Remove()
    {
        IsDeleted = true;
    }

    public void SetPrice(decimal? value)
    {
        Price = value;
    }
    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationRequestWarehouse()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

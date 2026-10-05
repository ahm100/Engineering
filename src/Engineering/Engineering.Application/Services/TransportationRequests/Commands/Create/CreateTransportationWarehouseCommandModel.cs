using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Application.Services.TransportationRequests.Commands.Create;

public record CreateTransportationWarehouseCommandModel(
    long WarehouseId,
    long? ThirdPartyId,
    string? ThirdPartyName,
    ViewPacking Packing,
    decimal Price,
    ShippingCost ShippingCost,
    List<CreateTransportationWarehouseProductCommandModel>? WarehouseProduct
    );

public record CreateTransportationWarehouseProductCommandModel(
    int Quantity,
    long ProductId,
    string? PalletNumber
    );

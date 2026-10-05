using Engineering.Application.Services.TransportationRequests.Models.CreateWarehouseTransportation;
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.CreateWarehouseTransportation;

public record CreateWarehouseTransportationCommand(
    CreateWarehouseTransportationRequest Request,
    List<CreateWarehouseTransportationWarehouseModel> Packings
    ) : ICommand<List<TransportationCargo>?>;

public record CreateWarehouseTransportationWarehouseModel(
        ViewPacking ViewPacking,
        ViewPackingShippingDetail? ViewPackingShipping,
        ViewInvoice? ViewInvoice,
        TransportationContractor? TransportationContractor,
        List<TransportationContractorPriceWeight>? PriceWeights);
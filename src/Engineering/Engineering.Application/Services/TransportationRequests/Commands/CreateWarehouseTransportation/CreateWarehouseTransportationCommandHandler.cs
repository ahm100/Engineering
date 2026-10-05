using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.CreateWarehouseTransportation;

public class CreateWarehouseTransportationCommandHandler : ICommandHandler<CreateWarehouseTransportationCommand, List<TransportationCargo>?>
{
    private readonly ILogger<CreateWarehouseTransportationCommand> _logger;
    private readonly ITransportationCargoRepository _repository;
    private readonly ITransportationCargoPalletRepository _palletRepository;
    private readonly ITransportationRequestWarehouseRepository _transportationRequestWarehouse;
    private readonly IViewInvoiceRepository _invoiceRepository;

    public CreateWarehouseTransportationCommandHandler(
        ILogger<CreateWarehouseTransportationCommand> logger,
        ITransportationCargoRepository repository,
        ITransportationRequestWarehouseRepository transportationRequestWarehouse,
        ITransportationCargoPalletRepository palletRepository,
        IViewInvoiceRepository invoiceRepository)
    {
        _logger = logger;
        _repository = repository;
        _transportationRequestWarehouse = transportationRequestWarehouse;
        _palletRepository = palletRepository;
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<List<TransportationCargo>?>> Handle(CreateWarehouseTransportationCommand request, CT ct)
    {
        try
        {
            List<TransportationCargo>? cargos = [];
            if (request.Packings is not null && request.Packings.Count > 0)
            {
                var invoices = await _invoiceRepository.GetInvoiceByRequestNumbers(request.Packings.NullListed(x => x.ViewPacking.RequestNumber), ct);
                foreach (var item in request.Packings)
                {
                    var invoice = invoices.FirstOrDefault(x => x.PackingNumber == item.ViewPacking.RequestNumber!.Value);
                    var transportationCargo = new TransportationCargo(item.ViewPacking.RequestNumber!.Value,
                        item.ViewPacking.Id, invoice?.OwnerId, invoice?.OwnerName);
                    var cargo = await _repository.Create(transportationCargo, ct);
                    cargos.Add(cargo);
                    foreach (var pallet in item.ViewPacking.PackingPallets)
                    {
                        var packingProducts = item.ViewPacking.PackingProducts.Where(x => x.PackingPalletId == pallet.Id).ToList();
                        var calcPallet = CalcPriceWeights(item.ViewPacking, packingProducts);

                        decimal? shippingPrice = null;
                        if (item.TransportationContractor is not null && item.TransportationContractor?.Type == TransportationContractorCalculateType.Weight)
                            shippingPrice = Calculator.CalculateTransportPriceWeight(item.PriceWeights!.ToArray(), pallet.Weight != null && pallet.Weight > 0 ?
                                pallet.Weight.Value : calcPallet.weight);

                        var source = item.ViewPacking.PackingAddress.FirstOrDefault(x => x.Type == AddressType.Source);
                        var destination = item.ViewPacking.PackingAddress.FirstOrDefault(x => x.Type == AddressType.Destination);

                        TransportationCargoPallet palet = new(cargo, null, calcPallet.price, shippingPrice,
                            pallet.Weight != null && pallet.Weight > 0 ? pallet.Weight.Value : calcPallet.weight,
                            packingProducts.Sum(x => x.Quantity), pallet.Number, null, source?.Id, destination?.Id,
                            pallet.Id, item.TransportationContractor, request.Request.DeliveryMethod,
                            request.Request.DeliveryType, request.Request.VehicleName, request.Request.PostageDate,
                            request.Request.NumberPlate, request.Request.Driver, request.Request.DriverPhoneNumber,
                            request.Request.PackingShippingType);
                        cargo.AddCargoPallets(palet);

                        foreach (var product in packingProducts)
                        {
                            var calcProduct = CalcPriceWeight(item.ViewPacking, product);
                            var transportProduct = new TransportationRequestWarehouse(palet, product.Id, palet.PalletNumber,
                                product.SourcePackingAddressId ?? source!.Id, product.DestinationPackingAddressId, calcProduct.price, product.QcQuantity);

                            palet.AddProduct(transportProduct);
                        }
                    }
                }
            }

            return cargos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<TransportationCargo>?>(SharedErrors.UnknownError);
        }
    }

    private static (decimal weight, decimal price) CalcPriceWeight(ViewPacking item, ViewPackingProduct? pac)
    {
        var price = ((pac?.UnitPrice * pac?.QcQuantity ?? 0) - (pac?.Discount ?? 0)) +
                    (pac?.ShippingPrice ?? 0) + (pac?.Tax ?? 0) + (pac?.Other ?? 0) +
                    (pac?.PackagingPrice ?? 0);

        decimal weight = (pac!.QcQuantity * (pac.PackingPallet?.Weight ?? 1)) /
                         (item.PackingProducts.Sum(x => x.QcQuantity));

        return (weight, price);
    }

    private static (decimal weight, decimal price) CalcPriceWeights(ViewPacking item, List<ViewPackingProduct>? pacs)
    {
        var sumPrice = 0m;
        var sumWeights = 0m;
        pacs?.ForEach(pac =>
        {
            sumPrice += ((pac?.UnitPrice * pac?.QcQuantity ?? 0) - (pac?.Discount ?? 0)) +
                     (pac?.ShippingPrice ?? 0) + (pac?.Tax ?? 0) + (pac?.Other ?? 0) +
                     (pac?.PackagingPrice ?? 0);

            sumWeights += (pac!.QcQuantity * (pac.PackingPallet?.Weight ?? 1)) /
                         (item.PackingProducts.Sum(x => x.QcQuantity));
        });

        var price = pacs is not null && pacs.Count > 0 ? sumPrice : 0m;

        decimal weight = pacs is not null && pacs.Count > 0 ? sumWeights : 0m;

        return (weight, price);
    }
}
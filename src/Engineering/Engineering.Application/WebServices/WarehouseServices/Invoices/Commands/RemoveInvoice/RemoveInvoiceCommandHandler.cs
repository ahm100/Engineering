using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.RemoveInvoice;

namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Commands.RemoveInvoice;

public class RemoveInvoiceCommandHandler : ICommandHandler<RemoveInvoiceCommand, RemoveInvoiceResponse?>
{
    private readonly ILogger<RemoveInvoiceCommandHandler> _logger;
    private readonly IWarehouseService _warehouseService;

    public RemoveInvoiceCommandHandler(ILogger<RemoveInvoiceCommandHandler> logger, IWarehouseService warehouseService)
    {
        _logger = logger;
        _warehouseService = warehouseService;
    }

    public async Task<Result<RemoveInvoiceResponse?>> Handle(RemoveInvoiceCommand request, CT ct)
    {
        try
        {
            var result = await _warehouseService.RemoveInvoice(new RemoveInvoiceRequest(request.Id), ct);

            return result ?? Result.Failure<RemoveInvoiceResponse?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RemoveInvoiceResponse?>(SharedErrors.UnknownError);
        }
    }
}

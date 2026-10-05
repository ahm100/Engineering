using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Invoices.Models.CreateEntryThroughBorrow;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Commands.CreateEntryThroughBorrow;

public class CreateEntryThroughBorrowCommandHandler : ICommandHandler<CreateEntryThroughBorrowCommand, CreateInvoiceResponse?>
{
    private readonly ILogger<CreateEntryThroughBorrowCommandHandler> _logger;
    private readonly IWarehouseService _warehouseService;

    public CreateEntryThroughBorrowCommandHandler(ILogger<CreateEntryThroughBorrowCommandHandler> logger, IWarehouseService warehouseService)
    {
        _logger = logger;
        _warehouseService = warehouseService;
    }

    public async Task<Result<CreateInvoiceResponse?>> Handle(CreateEntryThroughBorrowCommand request, CT ct)
    {
        try
        {
            var result = await _warehouseService.CreateEntryThroughBorrow(new CreateEntryThroughBorrowRequest(request.WarehouseId, request.Description,
                request.CommercialRequestId, request.CommercialRequestNo, request.Products, (int?)request.Importance, request.OwnerId, request.OwnerName,
                request.Documents, request.IsManually), ct);
            if (result is null || result.IsFailure)
                return Result.Failure<CreateInvoiceResponse>(WarehouseErrors.ProviderError(result?.Error));

            return result;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var response = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            response!.Error.StatusCode = 422;
            return Result.Failure<CreateInvoiceResponse?>(WarehouseErrors.ProviderError(response!.Error.Message!));
        }
    }
}

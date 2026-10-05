using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Invoices.Models.CreateExitForBorrows;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Commands.CreateExitForBorrows;

public class CreateExitForBorrowsCommandHandler : ICommandHandler<CreateExitForBorrowsCommand, CreateInvoiceResponse?>
{
    private readonly ILogger<CreateExitForBorrowsCommandHandler> _logger;
    private readonly IWarehouseService _warehouseService;

    public CreateExitForBorrowsCommandHandler(ILogger<CreateExitForBorrowsCommandHandler> logger, IWarehouseService warehouseService)
    {
        _logger = logger;
        _warehouseService = warehouseService;
    }

    public async Task<Result<CreateInvoiceResponse?>> Handle(CreateExitForBorrowsCommand request, CT ct)
    {
        try
        {
            var result = await _warehouseService.CreateExitForBorrow(new CreateExitForBorrowRequest(request.WarehouseId, request.Description, request.Importance,
                request.CommercialRequestId, request.CommercialRequestNo, request.Products, request.OwnerId, request.OwnerName, request.Documents, request.IsManually), ct);
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

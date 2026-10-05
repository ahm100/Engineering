using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForRelocations;

using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForRelocation;

public class CreateExitForRelocationCommandHandler : ICommandHandler<CreateExitForRelocationCommand, CreateInvoiceResponse?>
{
    private readonly ILogger<CreateExitForRelocationCommandHandler> _logger;
    private readonly IWarehouseService _warehouseService;

    public CreateExitForRelocationCommandHandler(ILogger<CreateExitForRelocationCommandHandler> logger, IWarehouseService warehouseService)
    {
        _logger = logger;
        _warehouseService = warehouseService;
    }

    public async Task<Result<CreateInvoiceResponse?>> Handle(CreateExitForRelocationCommand request, CT ct)
    {
        try
        {
            var result = await _warehouseService.CreateExitForRelocation(new CreateExitForRelocationsRequest(
                request.SourceWarehouseId,
                request.DestinationWarehouseId,
                (int)request.Importance,
                request.CommercialRequestId,
                request.CommercialRequestNo,
                request.Products,
                request.Documents,
                request.UserRegisterId,
                request.IsManually,
                request.OwnerId), ct);
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

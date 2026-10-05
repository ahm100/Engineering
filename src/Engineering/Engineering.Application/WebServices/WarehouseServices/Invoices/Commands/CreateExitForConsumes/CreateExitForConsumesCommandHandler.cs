using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;

public class CreateExitForConsumesCommandHandler : ICommandHandler<CreateExitForConsumesCommand, CreateInvoiceResponse?>
{
    private readonly ILogger<CreateExitForConsumesCommandHandler> _logger;
    private readonly IWarehouseService _warehouseService;

    public CreateExitForConsumesCommandHandler(ILogger<CreateExitForConsumesCommandHandler> logger, IWarehouseService warehouseService)
    {
        _logger = logger;
        _warehouseService = warehouseService;
    }

    public async Task<Result<CreateInvoiceResponse?>> Handle(CreateExitForConsumesCommand request, CT ct)
    {
        try
        {
            var result = await _warehouseService.CreateExitForConsume(new CreateExitForConsumesRequest(
                request.WarehouseId,
                request.Description,
                request.UserRegisterId,
                (int)request.Importance,
                request.CommercialRequestId,
                request.CommercialRequestNo,
                request.Products,
                request.Documents,
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

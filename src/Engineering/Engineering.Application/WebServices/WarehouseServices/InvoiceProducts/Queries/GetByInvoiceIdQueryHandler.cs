using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Models;
using Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Models.GetByInvoiceId;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Queries;

public class GetByInvoiceIdQueryHandler : IQueryHandler<GetByInvoiceIdQuery, DataResult<List<GetAllActiveInvoiceProductsModel>?>?>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetByInvoiceIdQueryHandler> _logger;

    public GetByInvoiceIdQueryHandler(ILogger<GetByInvoiceIdQueryHandler> logger, IWarehouseService repository)
    {
        _logger = logger;
        _warehouseService = repository;
    }

    public async Task<Result<DataResult<List<GetAllActiveInvoiceProductsModel>?>?>> Handle(GetByInvoiceIdQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetByInvoiceId(request.Adapt<GetAllActiveInvoiceProductsRequest>(), ct);

            return (result?.Value?.Data?.Any() ?? false) ?
                new DataResult<List<GetAllActiveInvoiceProductsModel>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetAllActiveInvoiceProductsModel>?>>(SharedErrors.ItemNotFound);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GetAllActiveInvoiceProductsModel>?>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}

using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetsSupplyManagementByInvoiceId;

public class GetsSupplyManagementByInvoiceIdQueryHandler : IQueryHandler<GetsSupplyManagementByInvoiceIdQuery, List<RequestGoodsSupplyManagement>>
{
    private readonly IRequestGoodsSupplyManagementRepository _repository;
    private readonly ILogger<GetsSupplyManagementByInvoiceIdQueryHandler> _logger;

    public GetsSupplyManagementByInvoiceIdQueryHandler(IRequestGoodsSupplyManagementRepository repository, ILogger<GetsSupplyManagementByInvoiceIdQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<RequestGoodsSupplyManagement>?>> Handle(GetsSupplyManagementByInvoiceIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsSupplyManagementByInvoiceId(request.InvoiceId, ct);

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestGoodsSupplyManagement>>(SharedErrors.UnknownError);
        }
    }
}

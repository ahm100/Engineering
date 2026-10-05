using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetRequestGoodsSupplyManagements;

public class GetRequestGoodsSupplyManagementsQueryHandler : IQueryHandler<GetRequestGoodsSupplyManagementsQuery, List<RequestGoodsSupplyManagement>>
{
    private readonly IRequestGoodsSupplyManagementRepository _repository;
    private readonly ILogger<GetRequestGoodsSupplyManagementsQueryHandler> _logger;

    public GetRequestGoodsSupplyManagementsQueryHandler(IRequestGoodsSupplyManagementRepository repository, ILogger<GetRequestGoodsSupplyManagementsQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<RequestGoodsSupplyManagement>?>> Handle(GetRequestGoodsSupplyManagementsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetRequestGoodsSupplyManagementsAsync(request.RequestGoodsSupplies, request.RequestGoodsSupplyDetailIds, request.InvoiceId, ct);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestGoodsSupplyManagement>>(SharedErrors.UnknownError);
        }
    }
}

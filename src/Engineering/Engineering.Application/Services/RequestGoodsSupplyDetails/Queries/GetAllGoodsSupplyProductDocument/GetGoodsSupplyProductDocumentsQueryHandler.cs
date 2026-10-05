using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Documents;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetAllGoodsSupplyProductDocument;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetAllGoodsSupplyProductDocument;

public class GetAllGoodsSupplyProductDocumentQueryHandler : IQueryHandler<GetAllGoodsSupplyProductDocumentQuery, List<GetAllGoodsSupplyProductDocumentResponseModel>?>
{
    private readonly ILogger<GetAllGoodsSupplyProductDocumentQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailDocumentRepository _repository;

    public GetAllGoodsSupplyProductDocumentQueryHandler(ILogger<GetAllGoodsSupplyProductDocumentQueryHandler> logger, IRequestGoodsSupplyDetailDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetAllGoodsSupplyProductDocumentResponseModel>?>> Handle(GetAllGoodsSupplyProductDocumentQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetAllGoodsSupplyProductDocument(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ProductIds,
                request.CityId,
                request.Types,
                request.Statuses,
                request.StartDate,
                request.EndDate,
                request.PageIndex,
                request.PageSize,
                ct);

            return entities;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<GetAllGoodsSupplyProductDocumentResponseModel>?>(SharedErrors.UnknownError);
        }
    }
}

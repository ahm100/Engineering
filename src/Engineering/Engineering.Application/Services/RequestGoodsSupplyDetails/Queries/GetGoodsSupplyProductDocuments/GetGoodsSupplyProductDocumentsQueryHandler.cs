using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Documents;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductDocuments;

public class GetGoodsSupplyProductDocumentsQueryHandler : IQueryHandler<GetGoodsSupplyProductDocumentsQuery, List<string>?>
{
    private readonly ILogger<GetGoodsSupplyProductDocumentsQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailDocumentRepository _repository;

    public GetGoodsSupplyProductDocumentsQueryHandler(ILogger<GetGoodsSupplyProductDocumentsQueryHandler> logger, IRequestGoodsSupplyDetailDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<string>?>> Handle(GetGoodsSupplyProductDocumentsQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetGoodsSupplyProductDocuments(request.Id, ct);

            return entities;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<string>?>(SharedErrors.UnknownError);
        }
    }
}

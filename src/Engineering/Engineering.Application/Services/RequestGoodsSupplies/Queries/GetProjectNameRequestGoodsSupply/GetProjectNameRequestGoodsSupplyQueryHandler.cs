using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetProjectNameRequestGoodsSupply;


public class GetProjectNameRequestGoodsSupplyQueryHandler : IQueryHandler<GetProjectNameRequestGoodsSupplyQuery, string>
{
    private readonly ILogger<GetProjectNameRequestGoodsSupplyQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetProjectNameRequestGoodsSupplyQueryHandler(ILogger<GetProjectNameRequestGoodsSupplyQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(GetProjectNameRequestGoodsSupplyQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetProjectNameRequestGoodsSupply(request.Id, ct);

            return response ?? Result.Failure<string>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<string>(SharedErrors.UnknownError);
        }
    }
}

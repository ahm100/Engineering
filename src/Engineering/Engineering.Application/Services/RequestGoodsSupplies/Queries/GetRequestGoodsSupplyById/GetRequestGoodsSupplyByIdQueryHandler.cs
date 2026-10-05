using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyById;

public class GetRequestGoodsSupplyByIdQueryHandler : IQueryHandler<GetRequestGoodsSupplyByIdQuery, RequestGoodsSupply>
{
    private readonly ILogger<GetRequestGoodsSupplyByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetRequestGoodsSupplyByIdQueryHandler(ILogger<GetRequestGoodsSupplyByIdQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(GetRequestGoodsSupplyByIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetRequestGoodsSupplyById(request.Id, ct);

            return response ?? Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }
}

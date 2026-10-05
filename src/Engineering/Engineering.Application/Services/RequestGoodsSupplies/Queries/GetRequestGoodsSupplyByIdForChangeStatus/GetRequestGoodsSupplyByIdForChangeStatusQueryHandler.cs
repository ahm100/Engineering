using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyByIdForChangeStatus;

public class GetRequestGoodsSupplyByIdForChangeStatusQueryHandler : IQueryHandler<GetRequestGoodsSupplyByIdForChangeStatusQuery, RequestGoodsSupply>
{
    private readonly ILogger<GetRequestGoodsSupplyByIdForChangeStatusQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetRequestGoodsSupplyByIdForChangeStatusQueryHandler(ILogger<GetRequestGoodsSupplyByIdForChangeStatusQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(GetRequestGoodsSupplyByIdForChangeStatusQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetRequestGoodsSupplyByIdForChangeStatus(request.Id, ct);

            return response ?? Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }
}

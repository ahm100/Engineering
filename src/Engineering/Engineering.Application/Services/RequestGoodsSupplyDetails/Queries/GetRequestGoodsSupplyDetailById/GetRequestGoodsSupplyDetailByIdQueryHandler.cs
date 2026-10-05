using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyDetailById;

public class GetRequestGoodsSupplyDetailByIdQueryHandler : IQueryHandler<GetRequestGoodsSupplyDetailByIdQuery, RequestGoodsSupplyDetail>
{
    private readonly ILogger<GetRequestGoodsSupplyDetailByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetRequestGoodsSupplyDetailByIdQueryHandler(ILogger<GetRequestGoodsSupplyDetailByIdQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyDetail?>> Handle(GetRequestGoodsSupplyDetailByIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetRequestGoodsSupplyDetailById(request.Id, ct);

            return response ?? Result.Failure<RequestGoodsSupplyDetail>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestGoodsSupplyDetail>(SharedErrors.UnknownError);
        }
    }
}

using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsSupplyManagementById;

public class GetRequestGoodsSupplyManagementByIdQueryHandler : IQueryHandler<GetRequestGoodsSupplyManagementByIdQuery, RequestGoodsSupplyManagement>
{
    private readonly ILogger<GetRequestGoodsSupplyManagementByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyManagementRepository _repository;

    public GetRequestGoodsSupplyManagementByIdQueryHandler(ILogger<GetRequestGoodsSupplyManagementByIdQueryHandler> logger,
                                               IRequestGoodsSupplyManagementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyManagement?>> Handle(GetRequestGoodsSupplyManagementByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);
            return result ?? Result.Failure<RequestGoodsSupplyManagement>(RequestGoodsSupplyManagementErrors.RequestGoodsSupplyManagementWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestGoodsSupplyManagement>(SharedErrors.UnknownError);
        }
    }
}

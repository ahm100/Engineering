using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

namespace Engineering.Application.Services.Branchs.Queries.GetAllManagerGoods;

public class GetAllManagerGoodsQueryHandler : IQueryHandler<GetAllManagerGoodsQuery, List<long>>
{
    private readonly IGoodsManagerAssignmentRepository _repository;
    private readonly ILogger<GetAllManagerGoodsQueryHandler> _logger;

    public GetAllManagerGoodsQueryHandler(
        ILogger<GetAllManagerGoodsQueryHandler> logger,
        IGoodsManagerAssignmentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<long>?>> Handle(GetAllManagerGoodsQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetAllManagerGoods(
                request.OrganizationIds, ct);
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<long>>(SharedErrors.UnknownError);
        }
    }
}
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetDetailByRGSTypeId;

public class GetDetailByRGSTypeIdQueryHandler : IQueryHandler<GetDetailByRGSTypeIdQuery, GetDetailByRGSTypeIdResponse?>
{
    private readonly ILogger<GetDetailByRGSTypeIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyTypeDetailRepository _repository;

    public GetDetailByRGSTypeIdQueryHandler(ILogger<GetDetailByRGSTypeIdQueryHandler> logger,
        IRequestGoodsSupplyTypeDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetDetailByRGSTypeIdResponse?>> Handle(GetDetailByRGSTypeIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetDetailByRGSTypeId(request.Id,
                request.Statuses,
                request.PageIndex,
                request.PageSize, ct);

            if (response.Data is null || response.RowCount < 1)
                return Result.Failure<GetDetailByRGSTypeIdResponse?>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            return new GetDetailByRGSTypeIdResponse(response.Data, response.RowCount);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetDetailByRGSTypeIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
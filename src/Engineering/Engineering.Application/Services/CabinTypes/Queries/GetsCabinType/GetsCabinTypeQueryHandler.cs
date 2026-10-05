using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinType;

namespace Engineering.Application.Services.CabinTypes.Queries.GetsCabinType;

public class GetsCabinTypeQueryHandler : IQueryHandler<GetsCabinTypeQuery, DataResult<List<GetsCabinTypeResponseModel>>>
{
    private readonly ICabinTypeRepository _repository;
    private readonly ILogger<GetsCabinTypeQueryHandler> _logger;

    public GetsCabinTypeQueryHandler(
        ILogger<GetsCabinTypeQueryHandler> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsCabinTypeResponseModel>>?>> Handle(GetsCabinTypeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCabinType(
                request.Ids,
                request.FilterData,
                request.IsActive,
                request.CompanyId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsCabinTypeResponseModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsCabinTypeResponseModel>>>(CabinTypeErrors.CabinTypesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsCabinTypeResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}
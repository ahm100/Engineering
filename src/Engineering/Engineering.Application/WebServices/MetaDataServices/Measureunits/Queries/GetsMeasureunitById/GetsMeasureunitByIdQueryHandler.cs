using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetsMeasureunitById;

public class GetsMeasureunitByIdQueryHandler : IQueryHandler<GetsMeasureunitByIdQuery, DataResult<List<MeasureUnit>>>
{
    private readonly IMeasureUnitRepository _measureUnitRepository;
    private readonly ILogger<GetsMeasureunitByIdQueryHandler> _logger;

    public GetsMeasureunitByIdQueryHandler(ILogger<GetsMeasureunitByIdQueryHandler> logger, IMeasureUnitRepository repository)
    {
        _logger = logger;
        _measureUnitRepository = repository;
    }

    public async Task<Result<DataResult<List<MeasureUnit>>?>> Handle(GetsMeasureunitByIdQuery request, CT ct)
    {
        try
        {
            var result = await _measureUnitRepository.GetMeasureUnitsByIds(request.Ids, ct);

            return (result?.Any()) ?? false ?
                new DataResult<List<MeasureUnit>>
                {
                    Data = result,
                    RowCount = result.Count
                } : Result.Failure<DataResult<List<MeasureUnit>>>(SharedErrors.ItemNotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<MeasureUnit>>>(SharedErrors.UnknownError);
        }
    }
}
using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetsInspectionCreator;

public class GetsInspectionCreatorQueryHandler : IQueryHandler<GetsInspectionCreatorQuery, DataResult<List<long>>>
{
    private readonly IProjectOperationDetailInspectionRepository _repository;
    private readonly ILogger<GetsInspectionCreatorQueryHandler> _logger;

    public GetsInspectionCreatorQueryHandler(ILogger<GetsInspectionCreatorQueryHandler> logger, IProjectOperationDetailInspectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetsInspectionCreatorQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsInspectionCreator(ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}

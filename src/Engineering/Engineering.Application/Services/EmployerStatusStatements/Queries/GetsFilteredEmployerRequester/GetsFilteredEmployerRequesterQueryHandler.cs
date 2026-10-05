using Engineering.Application.Abstractions.Data.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsFilteredEmployerRequester;

public class GetsFilteredEmployerRequesterQueryHandler : IQueryHandler<GetsFilteredEmployerRequesterQuery, DataResult<List<long>>>
{
    private readonly IEmployerStatusStatementRepository _repository;
    private readonly ILogger<GetsFilteredEmployerRequesterQueryHandler> _logger;

    public GetsFilteredEmployerRequesterQueryHandler(ILogger<GetsFilteredEmployerRequesterQueryHandler> logger, IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetsFilteredEmployerRequesterQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredEmployerRequester(ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(EmployerStatusStatementErrors.RequesterIdsAreEmpty);

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

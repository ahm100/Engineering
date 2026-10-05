using Engineering.Application.Abstractions.Data.SessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecords;

namespace Engineering.Application.Services.SessionRecords.Queries.GetSessionRecords;

public class GetSessionRecordsQueryHandler : IQueryHandler<GetSessionRecordsQuery, GetSessionRecordsResponse>
{
    private readonly ILogger<GetSessionRecordsQueryHandler> _logger;
    private readonly ISessionRecordRepository _repository;

    public GetSessionRecordsQueryHandler(
        ILogger<GetSessionRecordsQueryHandler> logger,
        ISessionRecordRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetSessionRecordsResponse?>> Handle(
        GetSessionRecordsQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetSessionRecords(
                request.TitleFa, request.TitleEn, request.Code,
                request.Category, request.Type, request.ProjectId,
                request.PageIndex, request.PageSize, ct);

            if (response.IsFailure)
                return Result.Failure<GetSessionRecordsResponse?>(response.Error!);

            return new GetSessionRecordsResponse(response.Value.Data, response.Value.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error GetSessionRecordsResponse Query Handler");
            return Result.Failure<GetSessionRecordsResponse?>(SharedErrors.UnknownError)!;
        }
    }
}

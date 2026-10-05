using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Services.Branchs.Models.GetsBranchs;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchsResponseModel;

public class GetsBranchsResponseModelQueryHandler : IQueryHandler<GetsBranchsResponseModelQuery, GetsBranchsResponse?>
{
    private readonly ILogger<GetsBranchsResponseModelQueryHandler> _logger;
    private readonly IBranchRepository _repository;

    public GetsBranchsResponseModelQueryHandler(
        ILogger<GetsBranchsResponseModelQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetsBranchsResponse?>> Handle(GetsBranchsResponseModelQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsBranchsResponse(
                request.FilterData,
                request.CategoryId,
                request.BranchCode,
                request.BranchName,
                request.IsActive,
                request.PageIndex,
                request.PageSize, ct);

            if (items.Data is null || !items.Data.Any())
                return Result.Failure<GetsBranchsResponse?>(BranchErrors.FilteredBranchNotFound);

            return new GetsBranchsResponse(items.Data, items.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetsBranchsResponse?>(SharedErrors.UnknownError);
        }
    }
}
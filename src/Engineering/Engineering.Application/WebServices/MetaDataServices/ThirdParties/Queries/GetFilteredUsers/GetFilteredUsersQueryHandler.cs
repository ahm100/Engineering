using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredUsers;

public class GetFilteredUsersQueryHandler : IQueryHandler<GetFilteredUsersQuery, DataResult<List<FilteredUserResponseModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetFilteredUsersQueryHandler> _logger;

    public GetFilteredUsersQueryHandler(ILogger<GetFilteredUsersQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<FilteredUserResponseModel>>?>> Handle(GetFilteredUsersQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetFilteredUsers(request.Adapt<GetFilteredUsersRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<FilteredUserResponseModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<FilteredUserResponseModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<FilteredUserResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}
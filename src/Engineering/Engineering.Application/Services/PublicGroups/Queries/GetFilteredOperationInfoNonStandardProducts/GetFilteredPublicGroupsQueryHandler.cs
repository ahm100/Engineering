using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.PublicGroups.Queries.GetFilteredPublicGroups;

public class GetFilteredPublicGroupsQueryHandler : IQueryHandler<GetFilteredPublicGroupsQuery, DataResult<List<PublicGroup>>>
{
    private readonly IPublicGroupRepository _repository;
    private readonly ILogger<GetFilteredPublicGroupsQueryHandler> _logger;

    public GetFilteredPublicGroupsQueryHandler(ILogger<GetFilteredPublicGroupsQueryHandler> logger, IPublicGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<PublicGroup>>?>> Handle(GetFilteredPublicGroupsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsPublicGroupFiltered(request.ProductGroupIds, null, 0, 0, ct);

            return result.Data.Any() ?
               new DataResult<List<PublicGroup>>
               {
                   Data = result.Data,
                   RowCount = result.RowCount
               } : Result.Failure<DataResult<List<PublicGroup>>>(OperationInfoErrors.OperationInfoNonStandardNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<PublicGroup>>>(SharedErrors.UnknownError);
        }
    }
}

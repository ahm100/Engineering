using Engineering.Application.Abstractions.Data.Machineries;
using Engineering.Domain.Entities.Machineries;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineriesGroupByIds;

public class GetsMachineriesGroupByIdsQueryHandler : IQueryHandler<GetsMachineriesGroupByIdsQuery, List<MachineriesGroup>>
{
    private readonly IMachineriesGroupRepository _repository;
    private readonly ILogger<GetsMachineriesGroupByIdsQuery> _logger;

    public GetsMachineriesGroupByIdsQueryHandler(ILogger<GetsMachineriesGroupByIdsQuery> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<MachineriesGroup>?>> Handle(GetsMachineriesGroupByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsMachineriesGroupByIds(request.Items, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<MachineriesGroup>>(SharedErrors.UnknownError);
        }
    }
}
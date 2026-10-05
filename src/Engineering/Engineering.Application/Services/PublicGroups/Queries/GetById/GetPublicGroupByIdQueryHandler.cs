using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.PublicGroups.Queries.GetById;

public class GetPublicGroupByIdQueryHandler : IQueryHandler<GetPublicGroupByIdQuery, PublicGroup>
{
    private readonly IPublicGroupRepository _repository;
    private readonly ILogger<GetPublicGroupByIdQueryHandler> _logger;

    public GetPublicGroupByIdQueryHandler(ILogger<GetPublicGroupByIdQueryHandler> logger, IPublicGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<PublicGroup?>> Handle(GetPublicGroupByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);

            return result ?? Result.Failure<PublicGroup>(OperationInfoErrors.OperationInfoNonStandardWithIdNotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<PublicGroup>(SharedErrors.UnknownError);
        }
    }
}

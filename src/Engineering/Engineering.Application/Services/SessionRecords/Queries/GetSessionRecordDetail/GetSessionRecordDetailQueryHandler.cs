using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.SessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;

namespace Engineering.Application.Services.SessionRecords.Queries.GetSessionRecordDetail;

public class GetSessionRecordDetailQueryHandler : IQueryHandler<GetSessionRecordDetailQuery, GetSessionRecordDetailResponse?>
{
    private readonly ILogger<GetSessionRecordDetailQueryHandler> _logger;
    private readonly ISessionRecordRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetSessionRecordDetailQueryHandler(
        ILogger<GetSessionRecordDetailQueryHandler> logger,
        IViewThirdPartyRepository thirdPartyRepo,
        ISessionRecordRepository repository)
    {
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
        _logger = logger;
    }

    public async Task<Result<GetSessionRecordDetailResponse?>> Handle(
        GetSessionRecordDetailQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetSessionRecordDetail(request.Id, ct);

            if (response is null)
                return response;

            var invitees = response.Invitees ?? [];
            var actions = response.Actions ?? [];

            var userIds = invitees.Select(x => x.UserId)
                .Concat(actions.Select(x => x.UserId))
                .Where(id => id > 0)
                .Select(id => id!.Value)
                .Distinct()
                .ToList();

            var users = await _thirdPartyRepo.GetByUserIds(userIds, ct) ?? [];

            foreach (var item in invitees)
            {
                var user = users.FirstOrDefault(e => e.UserId == item.UserId);
                if (user is not null)
                    item.UserFullName = $"{user.FirstName} {user.LastName}".Trim();
            }

            foreach (var item in actions)
            {
                var user = users.FirstOrDefault(e => e.UserId == item.UserId);
                if (user is not null)
                    item.UserFullName = $"{user.FirstName} {user.LastName}".Trim();
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error GetSessionRecordDetail Query Handler");
            return Result.Failure<GetSessionRecordDetailResponse?>(SharedErrors.UnknownError)!;
        }
    }
}

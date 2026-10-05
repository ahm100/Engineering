using Engineering.Application.Services.SessionRecords.Commands.CreateSessionRecord;
using Engineering.Application.Services.SessionRecords.Commands.EditSessionRecord;
using Engineering.Application.Services.SessionRecords.Commands.RemoveSessionRecord;
using Engineering.Application.Services.SessionRecords.Contracts.CreateSessionRecord;
using Engineering.Application.Services.SessionRecords.Contracts.EditSessionRecord;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;
using Engineering.Application.Services.SessionRecords.Contracts.RemoveSessionRecord;
using Engineering.Application.Services.SessionRecords.Queries.GetSessionRecordDetail;
using Engineering.Application.Services.SessionRecords.Queries.GetSessionRecords;
using Engineering.Application.Services.SessionRecords.Queries.GetUserSessionRecordAction;
using Engineering.Application.WebServices.HumenResourceServices.Posts.Queries.GetPostTitleThirdParties;
using IdentityServer.ClientSdk.Services;
using NPOI.OpenXmlFormats.Wordprocessing;

namespace Engineering.Application.Services.SessionRecords;

public class SessionRecordLogic : ISessionRecordLogic
{
    private readonly ILogger<SessionRecordLogic> _logger;
    private readonly IMediator _mediator;
    private readonly IUserInfoProvider _userInfoProvider;

    public SessionRecordLogic(
        ILogger<SessionRecordLogic> logger,
        IMediator mediator,
        IUserInfoProvider userInfoProvider)
    {
        _logger = logger;
        _mediator = mediator;
        _userInfoProvider = userInfoProvider;
    }

    public async Task<Result<CreateSessionRecordResponse>> CreateSessionRecord(
        CreateSessionRecordRequest request, CT ct)
    {
        _logger.LogInformation("CreateSessionRecord");
        
        var response = await _mediator.Send(new CreateSessionRecordCommand
        {
            TitleFa = request.TitleFa,
            TitleEn = request.TitleEn,
            ProjectId = request.ProjectId,
            ContractId = request.ContractId,
            ProjectName = request.ProjectName,
            SessionDate = request.SessionDate,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Location = request.Location,
            Category = request.Category,
            Type = request.Type,
            Invitees = request.Invitees,
            Items = request.Items,
            Actions = request.Actions,
            ContractNumber = request.ContractNumber
        }, ct);
        if (response.IsFailure)
            return Result.Failure<CreateSessionRecordResponse>(response.Error!)!;
        
        return response.Value!;
    }

    public async Task<Result<GetUserSessionRecordActionResponse>> GetUserSessionRecordAction(
        GetUserSessionRecordActionRequest request, CT ct)
    {
        var userId = _userInfoProvider.UserId;
        _logger.LogInformation("GetUserSessionRecordAction UserId: {userId}", userId);
        
        var response = await _mediator.Send(new GetUserSessionRecordActionQuery(
            userId,
            request.Description,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetUserSessionRecordActionResponse>(response.Error!)!;
        
        return response.Value!;
    }

    public async Task<Result<RemoveSessionRecordResponse>> RemoveSessionRecord(
        RemoveSessionRecordRequest request, CT ct)
    {
        _logger.LogInformation("Remove Session Record Id: {Id}", request.Id);
        
        var response = await _mediator.Send(new RemoveSessionRecordCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<RemoveSessionRecordResponse>(response.Error!)!;
        
        return response.Value!;
    }

    public async Task<Result<GetSessionRecordsResponse>> GetSessionRecords(
        GetSessionRecordsRequest request, CT ct)
    {
        _logger.LogInformation("GetSessionRecords");
        
        var response = await _mediator.Send(new GetSessionRecordsQuery(
            request.TitleFa, request.TitleEn, request.Code,
            request.Category, request.Type, request.ProjectId,
            request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetSessionRecordsResponse>(response.Error!)!;
        
        return response.Value!;
    }

    public async Task<Result<EditSessionRecordResponse>> EditSessionRecord(
        EditSessionRecordRequest request, CT ct)
    {
        _logger.LogInformation("EditSessionRecord");
        
        var response = await _mediator.Send(new EditSessionRecordCommand
        {
            Id = request.Id,
            TitleFa = request.TitleFa,
            TitleEn = request.TitleEn,
            ProjectId = request.ProjectId,
            ContractId = request.ContractId,
            Category = request.Category,
            Type = request.Type,
            SessionDate = request.SessionDate,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Docs = request.Docs,
            Items = request.Items,
            Actions = request.Actions,
            Invitees = request.Invitees
        }, ct);

        if (response.IsFailure)
            return Result.Failure<EditSessionRecordResponse>(response.Error!)!;
        return response.Value!;
    }

    public async Task<Result<GetSessionRecordDetailResponse>> GetSessionRecordDetail(
        GetSessionRecordDetailRequest request, CT ct)
    {
        _logger.LogInformation("GetSessionRecordDetail by session id: {Id}", request.Id);

        var detail = await _mediator.Send(new GetSessionRecordDetailQuery(request.Id), ct);
        if (detail.IsFailure)
            return Result.Failure<GetSessionRecordDetailResponse>(detail.Error!)!;
        if (detail.Value is null)
            return Result.Failure<GetSessionRecordDetailResponse>(SharedErrors.EntityNotFoundError)!;

        var invitees = detail.Value.Invitees ?? [];
        var ids = invitees
            .Where(i => i.UserId > 0)
            .Select(i => i.UserId!.Value)
            .Distinct()
            .ToList();
        if (ids.Count > 0)
        {
            var postTitles = await _mediator.Send(new GetPostTitleThirdPartiesQuery(ids), ct);
            if (postTitles.IsFailure)
            {
                _logger.LogError("GetPostTitleThirdParties failed for session record {Id}", request.Id);
                return Result.Failure<GetSessionRecordDetailResponse>(postTitles.Error!)!;
            }

            var titleById = (postTitles.Value ?? [])
                .GroupBy(x => x.ThirdPartyId)
                .ToDictionary(g => g.Key, g => g.First().PostTitle);
            foreach (var item in invitees)
            {
                if (item.UserId is { } id
                    && titleById.TryGetValue(id, out var title)
                    && !string.IsNullOrWhiteSpace(title))
                    item.PostTitle = title;
            }
        }

        return Result.Success(detail.Value);
    }
}

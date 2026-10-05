using Engineering.Application.Abstractions.Data.Actions;
using Engineering.Application.Services.Actions.Contracts.ActionCodeCreator;
using Engineering.Application.Services.Actions.Contracts.CreateAction;
using Engineering.Application.Services.Actions.Contracts.DeleteActions;
using Engineering.Application.Services.Actions.Contracts.GetActionById;
using Engineering.Application.Services.Actions.Contracts.GetFilteredActions;
using Engineering.Application.Services.Actions.Contracts.GetsActiveActions;
using Engineering.Application.Services.Actions.Contracts.UpdateAction;
using Engineering.Domain.Errors.Actions;

namespace Engineering.Application.Services.Actions;

public partial class ActionLogic : IActionLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ActionLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IActionRepository _repository;

    public ActionLogic(
        IMediator mediator,
        ILogger<ActionLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IActionRepository repository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _repository = repository;
    }

    public async Task<Result<CreateActionResponse?>> CreateAction(
        CreateActionRequest request, CT ct)
    {
        _logger.LogInformation("CreateAction");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateActionResponse>(GlobalErrors.InvalidCompany);

        if (await IsDuplicateName(request.Name, ct))
            return Result.Failure<CreateActionResponse>(ActionErrors.NameIsDuplicate);
        if (await IsDuplicateCode(request.Code, ct))
            return Result.Failure<CreateActionResponse>(ActionErrors.CodeIsDuplicate);

        var result = await CreateActionHandler(request, companyId, ct);
        if (result.IsBad()) return result.Failure<CreateActionResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new CreateActionResponse(result.Value!.Id);
    }

    public async Task<Result<UpdateActionResponse?>> UpdateAction(
        UpdateActionRequest request, CT ct)
    {
        _logger.LogInformation("UpdateAction");
        var entity = await GetActionByIdHandler(request.Id, ct);
        if (entity.IsBad())
            return entity.Failure<UpdateActionResponse>()!;

        var result = await UpdateActionHandler(entity.Value!, request, ct);
        if (result.IsBad()) return result.Failure<UpdateActionResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new UpdateActionResponse(true);
    }

    public async Task<Result<DeleteActionResponse?>> DeleteAction(
        DeleteActionRequest request, CT ct)
    {
        _logger.LogInformation("DeleteAction");

        var entity = await GetActionByIdHandler(request.Id, ct);
        if (entity.IsBad())
            return entity.Failure<DeleteActionResponse>()!;

        var result = await DeleteActionHandler(entity.Value!, ct);
        if (result.IsBad()) return result.Failure<DeleteActionResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new DeleteActionResponse(true);
    }

    public async Task<Result<GetActionByIdResponse?>> GetActionById(
        GetActionByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetActionById");
        var result = await GetActionByIdHandler(request.Id, ct);
        if (result.IsBad()) return result.Failure<GetActionByIdResponse>()!;
        return result.Value!.Adapt<GetActionByIdResponse>();
    }

    public async Task<Result<GetsActiveActionsResponse?>> GetsActiveActions(
        GetsActiveActionsRequest request, CT ct)
    {
        _logger.LogInformation("GetsActiveAction");
        var result = await GetsActiveActionsHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetsActiveActionsResponse>()!;

        var data = result.Value!.Data.Adapt<List<GetsActiveActionsModel>>();
        return new GetsActiveActionsResponse(data, result.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetFilteredActionsResponse?>> GetFilteredActions(
        GetFilteredActionsRequest request, CT ct)
    {
        _logger.LogInformation("GetsFilteredActions");
        var result = await GetFilteredActionsHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetFilteredActionsResponse>()!;

        var data = result.Value!.Data.Adapt<List<GetFilteredActionsModel>>();
        return new GetFilteredActionsResponse(data, result.Value?.RowCount ?? 0);
    }

    public async Task<Result<ActionCodeCreatorResponse?>> ActionCodeCreator(
        ActionCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("ActionCodeCreator");
        var result = await ActionCodeCreatorHandle(ct);
        if (result.IsBad()) return result.Failure<ActionCodeCreatorResponse>()!;
        return new ActionCodeCreatorResponse(result.Value!);
    }
}
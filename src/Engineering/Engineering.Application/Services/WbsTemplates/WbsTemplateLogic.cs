using Engineering.Application.Services.WbsTemplates.Commands.CreateWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Commands.DeleteWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Commands.UpdateWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.CreateWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.DeleteWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.GetFltrWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateById;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateForProject;
using Engineering.Application.Services.WbsTemplates.Contracts.UpdateWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Queries.GetFltrWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Queries.GetWbsTemplateById;
using Engineering.Application.Services.WbsTemplates.Queries.GetWbsTemplateForProject;

namespace Engineering.Application.Services.WbsTemplates;

public class WbsTemplateLogic : IWbsTemplateLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<WbsTemplateLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public WbsTemplateLogic(IMediator mediator, ILogger<WbsTemplateLogic> logger, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateWbsTemplateResponse?>> CreateWbsTemplate(
        CreateWbsTemplateRequest request, CT ct)
    {
        _logger.LogInformation("CreateWbsTemplate");

        var result = await _mediator.Send(new CreateWbsTemplateCommand(request.Title, request.Code, request.Description, request.IsActive), ct);
        if (result.IsBad()) return result.Failure<CreateWbsTemplateResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new CreateWbsTemplateResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateWbsTemplateResponse?>> UpdateWbsTemplate(
        UpdateWbsTemplateRequest request, CT ct)
    {
        _logger.LogInformation("UpdateWbsTemplate");

        var result = await _mediator.Send(new UpdateWbsTemplateCommand(request.Id, request.Title, request.Code, request.Description, request.IsActive), ct);
        if (result.IsBad()) return result.Failure<UpdateWbsTemplateResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new UpdateWbsTemplateResponse(true);
    }

    public async Task<Result<DeleteWbsTemplateResponse?>> DeleteWbsTemplate(
        DeleteWbsTemplateRequest request, CT ct)
    {
        _logger.LogInformation("DeleteWbsTemplate");

        var result = await _mediator.Send(new DeleteWbsTemplateCommand(request.Id), ct);
        if (result.IsBad()) return result.Failure<DeleteWbsTemplateResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new DeleteWbsTemplateResponse(true);
    }

    public async Task<Result<GetFltrWbsTemplateResponse?>> GetFltrWbsTemplate(
        GetFltrWbsTemplateRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrWbsTemplate");

        var result = await _mediator.Send(new GetFltrWbsTemplateQuery(request.FilterData, request.PageIndex, request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetFltrWbsTemplateResponse>()!;
        return result;
    }

    public async Task<Result<GetWbsTemplateByIdResponse?>> GetWbsTemplateById(
        GetWbsTemplateByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetWbsTemplateById");

        var result = await _mediator.Send(new GetWbsTemplateByIdQuery(request.Id), ct);
        if (result.IsBad()) return result.Failure<GetWbsTemplateByIdResponse>()!;
        return result;
    }

    public async Task<Result<GetWbsTemplateForProjectResponse?>> GetWbsTemplateForProject(
        GetWbsTemplateForProjectRequest request, CT ct)
    {
        _logger.LogInformation("GetWbsTemplateForProject");

        var result = await _mediator.Send(new GetWbsTemplateForProjectQuery(request.ProjectId,
            request.ParentId,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);

        if (result.IsBad())
            return result.Failure<GetWbsTemplateForProjectResponse>()!;
        return result;
    }
}

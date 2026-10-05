using Engineering.Application.Services.Contracts.Queries.GetContractById;
using Engineering.Application.Services.ProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Commands.CreateProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Commands.EditProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Commands.RemoveProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Contracts.CreateProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Contracts.EditProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;
using Engineering.Application.Services.ProcesVerbal.Contracts.RemoveProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Queries.GetProcesVerbalDetailById;
using Engineering.Application.Services.ProcesVerbal.Queries.GetProcesVerbals;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.ProjectSessionRecords;

public class ProcesVerbalLogic : IProcesVerbalLogic
{
    private readonly ILogger<ProcesVerbalLogic> _logger;
    private readonly IMediator _mediator;
    private readonly IUserInfoProvider _userInfoProvider;

    public ProcesVerbalLogic(
        ILogger<ProcesVerbalLogic> logger,
        IMediator mediator,
        IUserInfoProvider userInfoProvider)
    {
        _logger = logger;
        _mediator = mediator;
        _userInfoProvider = userInfoProvider;
    }

    public async Task<Result<CreateProcesVerbalResponse>> CreateProcesVerbal(
        CreateProcesVerbalRequest request, CT ct)
    {
        _logger.LogInformation("CreateProcesVerbal");

        var response = await _mediator.Send(
            new CreateProcesVerbalCommand(
                request.ProjectId, request.ContractId,
                request.TitleFa, request.TitleEn,
                request.Type, request.RecordDateTime, request.Location,
                request.DeliveryStatus, request.Limitations, request.WorkStatus,
                request.LimitationStatus, request.WorkStartStatus,
                request.WorkStopReason, request.WorkStopStatus,
                request.Items, request.Docs, request.PODs, request.Products), ct);

        return response!;
    }

    public async Task<Result<RemoveProcesVerbalResponse>> RemoveProcesVerbal(
        RemoveProcesVerbalRequest request, CT ct)
    {
        _logger.LogInformation("RemoveProcesVerbal Id : {Id}", request.Id);

        var response = await _mediator.Send(new RemoveProcesVerbalCommand(
            request.Id), ct);

        return response!;
    }

    public async Task<Result<GetProcesVerbalsResponse>> GetProcesVerbals(
        GetProcesVerbalsRequest request, CT ct)
    {
        _logger.LogInformation("GetProcesVerbals");

        var response = await _mediator.Send(new GetProcesVerbalsQuery(
            request.TargetId, request.ProjectId, request.ContractId,
            request.Type, request.Title, request.PageIndex, request.PageSize), ct);

        if (response.IsBad())
            return response.Failure<GetProcesVerbalsResponse>()!;

        var data = response.Value!.Data;

        foreach (var item in data)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery(item.ProjectId), ct);
            if (project is null || project.IsBad() || project.Value is null)
            {
                _logger.LogError("Project not found for ProcesVerbal Id : {Id}, ProjectId : {ProjectId}",
                    item.Id, item.ProjectId);
                return Result.Failure<GetProcesVerbalsResponse>(SharedErrors.ItemNotFound)!;
            }

            var contract = await _mediator.Send(new GetContractByIdQuery(
                item.ContractId), ct);
            if (contract is null || contract.IsBad() || contract.Value is null)
            {
                _logger.LogError("Contract not found for ProcesVerbal Id : {Id}, ContractId : {ContractId}",
                    item.Id, item.ContractId);
                return Result.Failure<GetProcesVerbalsResponse>(SharedErrors.ItemNotFound)!;
            }

            item.ProjectName = project.Value!.ProjectName;
            item.ContractNum = contract.Value!.ContractNumber;
        };

        return new GetProcesVerbalsResponse(data, response.Value!.RowCount);
    }

    public async Task<Result<GetProcesVerbalDetailByIdResponse>> GetProcesVerbalDetailById(
        GetProcesVerbalDetailByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetProcesVerbalDetailById");
        var response = await _mediator.Send(new GetProcesVerbalDetailByIdQuery(
            request.Id), ct);
        return response!;
    }

    public async Task<Result<EditProcesVerbalResponse>> EditProcesVerbal(
        EditProcesVerbalRequest request, CT ct)
    {
        _logger.LogInformation("GetProcesVerbalDetailById");

        var response = await _mediator.Send(new EditProcesVerbalCommand 
        {
            Id = request.Id,
            TitleFa = request.TitleFa,
            TitleEn = request.TitleEn,
            RecordDateTime = request.RecordDateTime,
            Location = request.Location,
            Type = request.Type 
        }, ct);
        return response!;
    }
}

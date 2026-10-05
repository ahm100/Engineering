using Engineering.Application.Services.EmployerEmployees.Commands.CreateEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Commands.DeleteEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Commands.UpdateEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Contracts.CreateEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Contracts.DeleteEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;
using Engineering.Application.Services.EmployerEmployees.Contracts.UpdateEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Queries.GetEmployeesByEmployerId;
using Engineering.Application.Services.EmployerEmployees.Queries.GetFltrEmployees;

namespace Engineering.Application.Services.EmployerEmployees;

public class EmployerEmployeeLogic : IEmployerEmployeeLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<EmployerEmployeeLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public EmployerEmployeeLogic(IMediator mediator,
        ILogger<EmployerEmployeeLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateEmployerEmployeeResponse?>> CreateEmployerEmployee(
        CreateEmployerEmployeeRequest request, CT ct)
    {
        _logger.LogInformation("CreateEmployerEmployee");
        var result = await _mediator.Send(new CreateEmployerEmployeeCommand(request.EmployeeId, request.EmployerId, request.IsActive), ct);
        if (result.IsBad()) return result.Failure<CreateEmployerEmployeeResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateEmployerEmployeeResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateEmployerEmployeeResponse?>> UpdateEmployerEmployee(
        UpdateEmployerEmployeeRequest request, CT ct)
    {
        _logger.LogInformation("UpdateEmployerEmployee");
        var result = await _mediator.Send(new UpdateEmployerEmployeeCommand(request.Id, request.EmployerId, request.IsActive), ct);
        if (result.IsBad()) return result.Failure<UpdateEmployerEmployeeResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateEmployerEmployeeResponse(result.Value!.Id, true);
    }

    public async Task<Result<DeleteEmployerEmployeeResponse?>> DeleteEmployerEmployee(
        DeleteEmployerEmployeeRequest request, CT ct)
    {
        _logger.LogInformation("DeleteEmployerEmployee");
        var result = await _mediator.Send(new DeleteEmployerEmployeeCommand(request.Id), ct);
        if (result.IsBad()) return result.Failure<DeleteEmployerEmployeeResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteEmployerEmployeeResponse(true);
    }

    public async Task<Result<GetEmployeesByEmployerIdResponse?>> GetEmployeesByEmployerId(
        GetEmployeesByEmployerIdRequest request, CT ct)
    {
        _logger.LogInformation("GetEmployeesByEmployerId");
        var result = await _mediator.Send(new GetEmployeesByEmployerIdQuery(request.EmployerId, request.PageIndex, request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetEmployeesByEmployerIdResponse>()!;

        return result;
    }

    public async Task<Result<GetFltrEmployeesResponse?>> GetFltrEmployees(
        GetFltrEmployeesRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrEmployees");
        var result = await _mediator.Send(new GetFltrEmployeesQuery(request.PageIndex, request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetFltrEmployeesResponse>()!;

        return result;
    }
}

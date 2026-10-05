using Engineering.Application.Services.EmployerEmployees;
using Engineering.Application.Services.EmployerEmployees.Contracts.CreateEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Contracts.DeleteEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;
using Engineering.Application.Services.EmployerEmployees.Contracts.UpdateEmployerEmployee;

namespace Engineering.Api.Controllers.EmployerEmployees;

[Authorize]
[Route("api/engineering/v1/EmployerEmployee")]
public class EmployerEmployeeController : ControllerBase
{
    private readonly ILogger<EmployerEmployeeController> _logger;
    private readonly IEmployerEmployeeLogic _logic;

    public EmployerEmployeeController(
        ILogger<EmployerEmployeeController> logger,
        IEmployerEmployeeLogic logic) : base()
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("CreateEmployerEmployee")]
    [ResponseSchema<CreateEmployerEmployeeResponse>]
    public async Task<IResult> CreateEmployerEmployee(
        [FromBody] CreateEmployerEmployeeRequest request, CT ct)
    {
        _logger.LogInformation("CreateEmployerEmployee");
        var result = await _logic.CreateEmployerEmployee(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateEmployerEmployee")]
    [ResponseSchema<UpdateEmployerEmployeeResponse>]
    public async Task<IResult> UpdateEmployerEmployee(
        [FromBody] UpdateEmployerEmployeeRequest request, CT ct)
    {
        _logger.LogInformation("UpdateEmployerEmployee");
        var result = await _logic.UpdateEmployerEmployee(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteEmployerEmployee")]
    [ResponseSchema<DeleteEmployerEmployeeResponse>]
    public async Task<IResult> DeleteEmployerEmployee(
        [FromQuery] DeleteEmployerEmployeeRequest request, CT ct)
    {
        _logger.LogInformation("DeleteEmployerEmployee");
        var result = await _logic.DeleteEmployerEmployee(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetEmployeesByEmployerId")]
    [ResponseSchema<GetEmployeesByEmployerIdResponse>]
    public async Task<IResult> GetEmployeesByEmployerId(
        [FromQuery] GetEmployeesByEmployerIdRequest request, CT ct)
    {
        _logger.LogInformation("GetEmployeesByEmployerId");
        var result = await _logic.GetEmployeesByEmployerId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrEmployees")]
    [ResponseSchema<GetFltrEmployeesResponse>]
    public async Task<IResult> GetFltrEmployees(
        [FromBody] GetFltrEmployeesRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrEmployees");
        var result = await _logic.GetFltrEmployees(request, ct);
        return result.GetHttpResponse();
    }
}
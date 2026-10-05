
using Engineering.Application.ContractorServices;
using Engineering.Application.ContractorServices.Models.CreateContractors;
using Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetContractorEmployeesByContractorId;
using Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetsContractorEmployeeBySkill;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorsByServiceIds;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorServicesByContractorId;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetFilteredContractors;

[Authorize]
[Route("api/engineering/v1/Contractors")]
[Tags("Contractors")]
public class ContractorController : ControllerBase
{
    private readonly ILogger<ContractorController> _logger;
    private readonly IContractorLogic _logic;

    public ContractorController(
        ILogger<ContractorController> logger,
        IContractorLogic logic)
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("CreateContractor")]
    [ResponseSchema<CreateContractorsResponse>]
    public async Task<IResult> CreateContractor(
    [FromBody] CreateContractorsRequest request, CT ct)
    {
        _logger.LogInformation("CreateContractor");
        var result = await _logic.CreateContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractorServiceByContractorId")]
    [ResponseSchema<GetContractorServicesByContractorIdResponse>]
    public async Task<IResult> GetContractorServiceByContractorId(
        [FromBody] GetContractorServicesByContractorIdRequest request, CT ct)
    {
        _logger.LogInformation("GetContractorServiceByContractorId");
        var result = await _logic.GetContractorServicesByContractorId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractorEmployeeByContractorId")]
    [ResponseSchema<GetContractorEmployeesByContractorIdResponse>]
    public async Task<IResult> GetContractorEmployeeByContractorId(
        [FromBody] GetContractorEmployeesByContractorIdRequest request, CT ct)
    {
        _logger.LogInformation("GetContractorEmployeeByContractorId");
        var result = await _logic.GetContractorEmployeesByContractorId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractorsByServiceInfoIds")]
    [ResponseSchema<GetContractorsByServiceIdsResponse>]
    public async Task<IResult> GetContractorsByServiceInfoIds(
        [FromBody] GetContractorsByServiceIdsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractorsByServiceInfoIds");
        var result = await _logic.GetContractorsByServiceIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredContractors")]
    [ResponseSchema<GetFilteredContractorsResponse>]
    public async Task<IResult> GetFilteredContractors(
        [FromBody] GetFilteredContractorsRequest request, CT ct)
    {
        _logger.LogInformation("GetFilteredContractors");
        var result = await _logic.GetFilteredContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsContractorEmployeeBySkill")]
    [ResponseSchema<GetsContractorEmployeeBySkillResponse>]
    public async Task<IResult> GetsContractorEmployeeBySkill(
        [FromQuery] GetsContractorEmployeeBySkillRequest request, CT ct)
    {
        _logger.LogInformation("GetsContractorEmployeeBySkill");
        var result = await _logic.GetsContractorEmployeeBySkill(request, ct);
        return result.GetHttpResponse();
    }
}
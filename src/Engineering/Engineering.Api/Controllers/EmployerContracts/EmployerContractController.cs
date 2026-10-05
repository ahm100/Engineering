using Engineering.Application.Services.EmployerContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;
using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.EContractFinancialCover;
using Engineering.Application.Services.EmployerContracts.Contracts.GetConsiderationTypes;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHeaderById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractOperationHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractStatus;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractTypes;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEDocumentTypes;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOProductHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOServiceHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads.Enum;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads.Exporter;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Enum;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Exporter;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEmployers;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractFinancial;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractVolume;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Api.Controllers.EmployerContracts;

[ApiController]
[Route("api/engineering/v1/EmployerContract")]
public class EmployerContractsController : ControllerBase
{
    private readonly IEContractHeaderLogic _logic;

    public EmployerContractsController(IEContractHeaderLogic logic)
    {
        _logic = logic;
    }

    [ResponseSchema<CreateEContractHeaderResponse>]
    [HttpPost("CreateEContractHeader")]
    public async Task<IResult> CreateEContractHeader([FromBody] CreateEContractHeaderRequest request, CT ct)
    {
        var result = await _logic.CreateEContractHeader(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<CreateEContractResponse>]
    [HttpPost("CreateEContract")]
    public async Task<IResult> CreateEContract([FromBody] CreateEContractRequest request, CT ct)
    {
        var result = await _logic.CreateEContract(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<UpdateEContractHeaderResponse>]
    [HttpPut("UpdateEContractHeader")]
    public async Task<IResult> UpdateEContractHeader([FromBody] UpdateEContractHeaderRequest request, CT ct)
    {
        var result = await _logic.UpdateEContractHeader(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<UpdateEContractResponse>]
    [HttpPut("UpdateEContract")]
    public async Task<IResult> UpdateEContract([FromBody] UpdateEContractRequest request, CT ct)
    {
        var result = await _logic.UpdateEContract(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetFltrEContractsResponse>]
    [HttpPost("GetFltrEContracts")]
    public async Task<IResult> GetFltrEContracts([FromBody] GetFltrEContractsRequest request, CT ct)
    {
        var result = await _logic.GetFltrEContracts(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetFltrEmployersResponse>]
    [HttpPost("GetFltrEmployers")]
    public async Task<IResult> GetFltrEmployers([FromBody] GetFltrEmployersRequest request, CT ct)
    {
        var result = await _logic.GetFltrEmployers(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetFltrEContractHeadsResponse>]
    [HttpPost("GetFltrEContractHeads")]
    public async Task<IResult> GetFltrEContractHeads([FromBody] GetFltrEContractHeadsRequest request, CT ct)
    {
        var result = await _logic.GetFltrEContractHeads(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetEContractHeaderByIdResponse>]
    [HttpGet("GetEContractHeaderById")]
    public async Task<IResult> GetEContractHeaderById([FromQuery] GetEContractHeaderByIdRequest request, CT ct)
    {
        var result = await _logic.GetEContractHeaderById(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetEContractByIdResponse>]
    [HttpGet("GetEContractById")]
    public async Task<IResult> GetEContractById([FromQuery] GetEContractByIdRequest request, CT ct)
    {
        var result = await _logic.GetEContractById(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetEContractTypesResponse>]
    [HttpGet("GetEContractTypes")]
    public async Task<IResult> GetEContractTypes([FromQuery] GetEContractTypesRequest request, CT ct)
    {
        var result = await _logic.GetEContractTypes(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetConsiderationTypesResponse>]
    [HttpGet("GetConsiderationTypes")]
    public async Task<IResult> GetConsiderationTypes([FromQuery] GetConsiderationTypesRequest request, CT ct)
    {
        var result = await _logic.GetConsiderationTypes(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetEContractStatusResponse>]
    [HttpGet("GetEContractStatus")]
    public async Task<IResult> GetEContractStatus([FromQuery] GetEContractStatusRequest request, CT ct)
    {
        var result = await _logic.GetEContractStatus(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetEDocumentTypesResponse>]
    [HttpGet("GetEDocumentTypes")]
    public async Task<IResult> GetEDocumentTypes([FromQuery] GetEDocumentTypesRequest request, CT ct)
    {
        var result = await _logic.GetEDocumentTypes(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<DeleteEContractHeaderResponse>]
    [HttpDelete("DeleteEContractHeader")]
    public async Task<IResult> DeleteEContractHeader([FromQuery] DeleteEContractHeaderRequest request, CT ct)
    {
        var result = await _logic.DeleteEContractHeader(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<DeleteEContractResponse>]
    [HttpDelete("DeleteEContract")]
    public async Task<IResult> DeleteEContract([FromBody] DeleteEContractRequest request, CT ct)
    {
        var result = await _logic.DeleteEContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToProjectManagerConfirmed")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToProjectManagerConfirmed([FromBody] SetECToProjectManagerConfirmedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ProjectManagerConfirmed,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToProjectManagerPending")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToProjectManagerPending([FromBody] SetECToProjectManagerPendingRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ProjectManagerPending,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToProjectManagerReturned")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToProjectManagerReturned([FromBody] SetECToProjectManagerReturnedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ProjectManagerReturned,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToContractExpertConfirmed")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToContractExpertConfirmed([FromBody] SetECToContractExpertConfirmedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ContractExpertConfirmed,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToContractExpertPending")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToContractExpertPending([FromBody] SetECToContractExpertPendingRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ContractExpertPending,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToContractExpertReturned")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToContractExpertReturned([FromBody] SetECToContractExpertReturnedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ContractExpertReturned,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToContractExpertReturnToUser")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToContractExpertReturnToUser([FromBody] SetECToContractorExpertReturnToUserRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ContractExpertReturnToUser,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToEmployerPending")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToEmployerPending([FromBody] SetECToEmployerPendingRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.EmployerPending,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToEmployerReturned")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToEmployerReturned([FromBody] SetECToEmployerReturnedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.EmployerReturned,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToEmployerConfirmed")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToEmployerConfirmed([FromBody] SetECToEmployerConfirmedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.EmployerConfirmed,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToContractSupervisorConfirmed")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToContractSupervisorConfirmed([FromBody] SetECToContractSupervisorConfirmedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ContractSupervisorConfirmed,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToContractSupervisorPending")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToContractSupervisorPending([FromBody] SetECToContractSupervisorPendingRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ContractSupervisorPending,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToContractSupervisorReturned")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToContractSupervisorReturned([FromBody] SetECToContractSupervisorReturnedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ReturnToContractExpert,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToContractSupervisorReturnToUser")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToContractSupervisorReturnToUser([FromBody] SetECToContractorSupervisorReturnToUserRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.ContractSupervisorReturnedToUser,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToPrimaryManagerConfirmed")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToPrimaryManagerConfirmed([FromBody] SetECToPrimaryManagerConfirmedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.PrimaryManagerConfirmed,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToPrimaryManagerReturned")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToPrimaryManagerReturned([FromBody] SetECToPrimaryManagerReturnedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.PrimaryManagerReturned,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToFinalManagerReturned")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToFinalManagerReturned([FromBody] SetECToFinalManagerReturnedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.FinalManagerReturned,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetECToFinalManagerConfirmed")]
    [ResponseSchema<SetECStatusResponse>]
    public async Task<IResult> SetECToFinalManagerConfirmed([FromBody] SetECToFinalManagerConfirmedRequest request, CT ct)
    {
        var result = await _logic.EContractStatusChanger(new SetECStatusRequest(
            request.Id,
            EContractStatus.FinalManagerConfirmed,
            request.Description), ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetFltrEContractHeadsEnumResponse>]
    [HttpGet("GetFltrEContractHeadsEnum")]
    public async Task<IResult> GetFltrEContractHeadsEnum([FromQuery] GetFltrEContractHeadsEnumRequest request, CT ct)
    {
        var result = await _logic.GetFltrEContractHeadsEnum(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetFltrEContractHeadsExporterResponse>]
    [HttpPost("GetFltrEContractHeadsExporter")]
    public async Task<IResult> GetFltrEContractHeadsExporter([FromBody] GetFltrEContractHeadsExporterRequest request, CT ct)
    {
        var result = await _logic.GetFltrEContractHeadsExporter(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetFltrEContractsEnumResponse>]
    [HttpGet("GetFltrEContractsEnum")]
    public async Task<IResult> GetFltrEContractsEnum([FromQuery] GetFltrEContractsEnumRequest request, CT ct)
    {
        var result = await _logic.GetFltrEContractsEnum(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetFltrEContractsExporterResponse>]
    [HttpPost("GetFltrEContractExporter")]
    public async Task<IResult> GetFltrEContractExporter([FromBody] GetFltrEContractsExporterRequest request, CT ct)
    {
        var result = await _logic.GetFltrEContractsExporter(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetEContractHistoryResponse>]
    [HttpGet("GetEContractHistory")]
    public async Task<IResult> GetEContractHistory([FromQuery] GetEContractHistoryRequest request, CT ct)
    {
        var result = await _logic.GetEContractHistory(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetEOProductHistoryResponse>]
    [HttpGet("GetEOProductHistory")]
    public async Task<IResult> GetEOProductHistory([FromQuery] GetEOProductHistoryRequest request, CT ct)
    {
        var result = await _logic.GetEOProductHistory(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetEOServiceHistoryResponse>]
    [HttpGet("GetEOServiceHistory")]
    public async Task<IResult> GetEOServiceHistory([FromQuery] GetEOServiceHistoryRequest request, CT ct)
    {
        var result = await _logic.GetEOServiceHistory(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<GetEContractOperationHistoryResponse>]
    [HttpGet("GetEContractOperationHistory")]
    public async Task<IResult> GetEContractOperationHistory([FromQuery] GetEContractOperationHistoryRequest request, CT ct)
    {
        var result = await _logic.GetEContractOperationHistory(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<UpdateEContractVolumeResponse>]
    [HttpPut("UpdateEContractVolume")]
    public async Task<IResult> UpdateEContractVolume([FromBody] UpdateEContractVolumeRequest request, CT ct)
    {
        var result = await _logic.UpdateEContractVolumeService(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<EContractFinancialCoverResponse>]
    [HttpPut("EContractFinancialCover")]
    public async Task<IResult> EContractFinancialCover([FromBody] EContractFinancialCoverRequest request, CT ct)
    {
        var result = await _logic.EContractFinancialCover(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<UpdateEContractFinancialResponse>]
    [HttpPut("UpdateEContractFinancial")]
    public async Task<IResult> UpdateEContractFinancial([FromBody] UpdateEContractFinancialRequest request, CT ct)
    {
        var result = await _logic.UpdateEContractFinancial(request, ct);
        return result.GetHttpResponse();
    }
}
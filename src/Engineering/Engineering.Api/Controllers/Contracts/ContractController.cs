using Engineering.Api.Extensions.Enums;
using Engineering.Application.Services.Contracts;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentIndexState;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentReferenceState;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractGuaranteeStatus;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractStatus;
using Engineering.Application.Services.Contracts.Contracts.CreateContract;
using Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentIndex;
using Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentReference;
using Engineering.Application.Services.Contracts.Contracts.CreateContractChange;
using Engineering.Application.Services.Contracts.Contracts.CreateContractFinancialInformation;
using Engineering.Application.Services.Contracts.Contracts.CreateContractGuarantee;
using Engineering.Application.Services.Contracts.Contracts.CreateContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.CreateContractSummaryChange;
using Engineering.Application.Services.Contracts.Contracts.CreateContractType;
using Engineering.Application.Services.Contracts.Contracts.CreateContractTypeDetail;
using Engineering.Application.Services.Contracts.Contracts.DeleteContract;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractChange;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractGuarantee;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractType;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractTypeDetail;
using Engineering.Application.Services.Contracts.Contracts.FinalizeContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.GetAvailableContractTypeDetailSources;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexes;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferenceById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferences;
using Engineering.Application.Services.Contracts.Contracts.GetContractById;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeAvailableItems;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractChanges;
using Engineering.Application.Services.Contracts.Contracts.GetContractFinancialInformation;
using Engineering.Application.Services.Contracts.Contracts.GetContractForProcesVerbal;
using Engineering.Application.Services.Contracts.Contracts.GetContractGuaranteeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractGuarantees;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationById;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;
using Engineering.Application.Services.Contracts.Contracts.GetContractsByStatus;
using Engineering.Application.Services.Contracts.Contracts.GetContractStructure;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetailById;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetails;
using Engineering.Application.Services.Contracts.Contracts.GetFilteredContracts;
using Engineering.Application.Services.Contracts.Contracts.UpdateContract;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentIndex;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentReference;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractChange;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractFinancialInformation;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractGuarantee;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractStructure;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractSummaryChange;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractType;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractTypeDetail;
using Engineering.Domain.Entities.Contracts.Enums;
using System.ComponentModel;

namespace Engineering.Api.Controllers.Contracts;

[ApiController]
[Authorize]
[Route("api/engineering/v1/Contract")]
public class ContractController : ControllerBase
{
    private readonly ILogger<ContractController> _logger;
    private readonly IContractLogic _logic;

    #region Constructor

    public ContractController(
        ILogger<ContractController> logger,
        IContractLogic logic)
    {
        _logger = logger;
        _logic = logic;
    }

    #endregion

    #region Contract Commands

    [HttpPost("CreateContract")]
    [ResponseSchema<CreateContractResponse>]
    public async Task<IResult> CreateContract(
        [FromBody] CreateContractRequest request, CT ct)
    {
        var result = await _logic.CreateContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateContractRegistration")]
    [ResponseSchema<CreateContractRegistrationResponse>]
    public async Task<IResult> CreateContractRegistration(
        [FromBody] CreateContractRegistrationRequest request, CT ct)
    {
        var result = await _logic.CreateContractRegistration(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractRegistration")]
    [ResponseSchema<UpdateContractRegistrationResponse>]
    public async Task<IResult> UpdateContractRegistration(
        [FromBody] UpdateContractRegistrationRequest request, CT ct)
    {
        var result = await _logic.UpdateContractRegistration(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("FinalizeContractRegistration")]
    [ResponseSchema<FinalizeContractRegistrationResponse>]
    public async Task<IResult> FinalizeContractRegistration(
        [FromBody] FinalizeContractRegistrationRequest request, CT ct)
    {
        var result = await _logic.FinalizeContractRegistration(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContract")]
    [ResponseSchema<UpdateContractResponse>]
    public async Task<IResult> UpdateContract(
        [FromBody] UpdateContractRequest request, CT ct)
    {
        var result = await _logic.UpdateContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractStructure")]
    [ResponseSchema<UpdateContractStructureResponse>]
    public async Task<IResult> UpdateContractStructure(
        [FromBody] UpdateContractStructureRequest request, CT ct)
    {
        var result = await _logic.UpdateContractStructure(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteContract")]
    [ResponseSchema<DeleteContractResponse>]
    public async Task<IResult> DeleteContract(
        [FromBody] DeleteContractRequest request, CT ct)
    {
        var result = await _logic.DeleteContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ChangeContractStatus")]
    [ResponseSchema<ChangeContractStatusResponse>]
    public async Task<IResult> ChangeContractStatus(
        [FromBody] ChangeContractStatusRequest request, CT ct)
    {
        var result = await _logic.ChangeContractStatus(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractType Commands

    [HttpPost("CreateContractType")]
    [ResponseSchema<CreateContractTypeResponse>]
    public async Task<IResult> CreateContractType([FromBody] CreateContractTypeRequest request, CT ct)
    {
        var result = await _logic.CreateContractType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractType")]
    [ResponseSchema<UpdateContractTypeResponse>]
    public async Task<IResult> UpdateContractType([FromBody] UpdateContractTypeRequest request, CT ct)
    {
        var result = await _logic.UpdateContractType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteContractType")]
    [ResponseSchema<DeleteContractTypeResponse>]
    public async Task<IResult> DeleteContractType([FromBody] DeleteContractTypeRequest request, CT ct)
    {
        var result = await _logic.DeleteContractType(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractTypeDetail Commands

    [HttpPost("CreateContractTypeDetail")]
    [ResponseSchema<CreateContractTypeDetailResponse>]
    public async Task<IResult> CreateContractTypeDetail(
        [FromBody] CreateContractTypeDetailRequest request,
        CT ct)
    {
        var result = await _logic.CreateContractTypeDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractTypeDetail")]
    [ResponseSchema<UpdateContractTypeDetailResponse>]
    public async Task<IResult> UpdateContractTypeDetail(
    [FromBody] UpdateContractTypeDetailRequest request,
    CT ct)
    {
        var result = await _logic.UpdateContractTypeDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteContractTypeDetail")]
    [ResponseSchema<DeleteContractTypeDetailResponse>]
    public async Task<IResult> DeleteContractTypeDetail(
        [FromBody] DeleteContractTypeDetailRequest request,
        CT ct)
    {
        var result = await _logic.DeleteContractTypeDetail(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractFinancialInformation Commands

    [HttpPost("CreateContractFinancialInformation")]
    [ResponseSchema<CreateContractFinancialInformationResponse>]
    public async Task<IResult> CreateContractFinancialInformation(
        [FromBody] CreateContractFinancialInformationRequest request,
        CT ct)
    {
        var result = await _logic.CreateContractFinancialInformation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractFinancialInformation")]
    [ResponseSchema<UpdateContractFinancialInformationResponse>]
    public async Task<IResult> UpdateContractFinancialInformation(
        [FromBody] UpdateContractFinancialInformationRequest request,
        CT ct)
    {
        var result = await _logic.UpdateContractFinancialInformation(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractAdjustmentConfiguration Commands

    [HttpPost("CreateContractAdjustmentConfiguration")]
    [ResponseSchema<CreateContractAdjustmentConfigurationResponse>]
    public async Task<IResult> CreateContractAdjustmentConfiguration(
        [FromBody] CreateContractAdjustmentConfigurationRequest request, CT ct)
    {
        var result = await _logic.CreateContractAdjustmentConfiguration(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractAdjustmentConfiguration")]
    [ResponseSchema<UpdateContractAdjustmentConfigurationResponse>]
    public async Task<IResult> UpdateContractAdjustmentConfiguration(
        [FromBody] UpdateContractAdjustmentConfigurationRequest request, CT ct)
    {
        var result = await _logic.UpdateContractAdjustmentConfiguration(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteContractAdjustmentConfiguration")]
    [ResponseSchema<DeleteContractAdjustmentConfigurationResponse>]
    public async Task<IResult> DeleteContractAdjustmentConfiguration(
        [FromBody] DeleteContractAdjustmentConfigurationRequest request, CT ct)
    {
        var result = await _logic.DeleteContractAdjustmentConfiguration(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractGuarantee Commands

    [HttpPost("CreateContractGuarantee")]
    [ResponseSchema<CreateContractGuaranteeResponse>]
    public async Task<IResult> CreateContractGuarantee(
        [FromBody] CreateContractGuaranteeRequest request, CT ct)
    {
        var result = await _logic.CreateContractGuarantee(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractGuarantee")]
    [ResponseSchema<UpdateContractGuaranteeResponse>]
    public async Task<IResult> UpdateContractGuarantee(
        [FromBody] UpdateContractGuaranteeRequest request, CT ct)
    {
        var result = await _logic.UpdateContractGuarantee(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeContractGuaranteeStatus")]
    [ResponseSchema<ChangeContractGuaranteeStatusResponse>]
    public async Task<IResult> ChangeContractGuaranteeStatus(
        [FromBody] ChangeContractGuaranteeStatusRequest request, CT ct)
    {
        var result = await _logic.ChangeContractGuaranteeStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteContractGuarantee")]
    [ResponseSchema<DeleteContractGuaranteeResponse>]
    public async Task<IResult> DeleteContractGuarantee(
        [FromBody] DeleteContractGuaranteeRequest request, CT ct)
    {
        var result = await _logic.DeleteContractGuarantee(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractChange Commands

    [HttpPost("CreateContractChange")]
    [ResponseSchema<CreateContractChangeResponse>]
    public async Task<IResult> CreateContractChange(
        [FromBody] CreateContractChangeRequest request,
        CT ct)
    {
        var result = await _logic.CreateContractChange(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateContractSummaryChange")]
    [ResponseSchema<CreateContractSummaryChangeResponse>]
    public async Task<IResult> CreateContractSummaryChange(
        [FromBody] CreateContractSummaryChangeRequest request, CT ct)
    {
        var result = await _logic.CreateContractSummaryChange(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractChange")]
    [ResponseSchema<UpdateContractChangeResponse>]
    public async Task<IResult> UpdateContractChange(
        [FromBody] UpdateContractChangeRequest request,
        CT ct)
    {
        var result = await _logic.UpdateContractChange(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractSummaryChange")]
    [ResponseSchema<UpdateContractSummaryChangeResponse>]
    public async Task<IResult> UpdateContractSummaryChange(
        [FromBody] UpdateContractSummaryChangeRequest request, CT ct)
    {
        var result = await _logic.UpdateContractSummaryChange(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteContractChange")]
    [ResponseSchema<DeleteContractChangeResponse>]
    public async Task<IResult> DeleteContractChange(
        [FromBody] DeleteContractChangeRequest request,
        CT ct)
    {
        var result = await _logic.DeleteContractChange(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractAdjustmentReference Commands

    [HttpPost("CreateContractAdjustmentReference")]
    [ResponseSchema<CreateContractAdjustmentReferenceResponse>]
    public async Task<IResult> CreateContractAdjustmentReference(
        [FromBody] CreateContractAdjustmentReferenceRequest request,
        CT ct)
    {
        var result = await _logic.CreateContractAdjustmentReference(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractAdjustmentReference")]
    [ResponseSchema<UpdateContractAdjustmentReferenceResponse>]
    public async Task<IResult> UpdateContractAdjustmentReference(
        [FromBody] UpdateContractAdjustmentReferenceRequest request,
        CT ct)
    {
        var result = await _logic.UpdateContractAdjustmentReference(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeContractAdjustmentReferenceState")]
    [ResponseSchema<ChangeContractAdjustmentReferenceStateResponse>]
    public async Task<IResult> ChangeContractAdjustmentReferenceState(
        [FromBody] ChangeContractAdjustmentReferenceStateRequest request,
        CT ct)
    {
        var result = await _logic.ChangeContractAdjustmentReferenceState(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateContractAdjustmentIndex")]
    [ResponseSchema<CreateContractAdjustmentIndexResponse>]
    public async Task<IResult> CreateContractAdjustmentIndex(
        [FromBody] CreateContractAdjustmentIndexRequest request,
        CT ct)
    {
        var result = await _logic.CreateContractAdjustmentIndex(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractAdjustmentIndex")]
    [ResponseSchema<UpdateContractAdjustmentIndexResponse>]
    public async Task<IResult> UpdateContractAdjustmentIndex(
        [FromBody] UpdateContractAdjustmentIndexRequest request,
        CT ct)
    {
        var result = await _logic.UpdateContractAdjustmentIndex(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeContractAdjustmentIndexState")]
    [ResponseSchema<ChangeContractAdjustmentIndexStateResponse>]
    public async Task<IResult> ChangeContractAdjustmentIndexState(
        [FromBody] ChangeContractAdjustmentIndexStateRequest request,
        CT ct)
    {
        var result = await _logic.ChangeContractAdjustmentIndexState(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region Contract Queries

    [HttpPost("GetContractById")]
    [ResponseSchema<GetContractByIdResponse>]
    public async Task<IResult> GetContractById([FromBody] GetContractByIdRequest request, CT ct)
    {
        var result = await _logic.GetContractById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractRegistrationById")]
    [ResponseSchema<GetContractRegistrationByIdResponse>]
    public async Task<IResult> GetContractRegistrationById(
        [FromBody] GetContractRegistrationByIdRequest request, CT ct)
    {
        var result = await _logic.GetContractRegistrationById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractRegistrationGrid")]
    [ResponseSchema<GetContractRegistrationGridResponse>]
    public async Task<IResult> GetContractRegistrationGrid(
        [FromBody] GetContractRegistrationGridRequest request, CT ct)
    {
        var result = await _logic.GetContractRegistrationGrid(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredContracts")]
    [ResponseSchema<GetFilteredContractsResponse>]
    public async Task<IResult> GetFilteredContracts([FromBody] GetFilteredContractsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredContracts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractsByStatus")]
    [ResponseSchema<GetContractsByStatusResponse>]
    public async Task<IResult> GetContractsByStatus([FromBody] GetContractsByStatusRequest request, CT ct)
    {
        var result = await _logic.GetContractsByStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractStructure")]
    [ResponseSchema<GetContractStructureResponse>]
    public async Task<IResult> GetContractStructure([FromBody] GetContractStructureRequest request, CT ct)
    {
        var result = await _logic.GetContractStructure(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractType Queries

    [HttpPost("GetContractTypeById")]
    [ResponseSchema<GetContractTypeByIdResponse>]
    public async Task<IResult> GetContractTypeById([FromBody] GetContractTypeByIdRequest request, CT ct)
    {
        var result = await _logic.GetContractTypeById(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractTypeDetail Queries

    [HttpPost("GetContractTypeDetailById")]
    [ResponseSchema<GetContractTypeDetailByIdResponse>]
    public async Task<IResult> GetContractTypeDetailById(
        [FromBody] GetContractTypeDetailByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetContractTypeDetailById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractTypeDetails")]
    [ResponseSchema<GetContractTypeDetailsResponse>]
    public async Task<IResult> GetContractTypeDetails(
        [FromBody] GetContractTypeDetailsRequest request,
        CT ct)
    {
        var result = await _logic.GetContractTypeDetails(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetAvailableContractTypeDetailSources")]
    [ResponseSchema<GetAvailableContractTypeDetailSourcesResponse>]
    public async Task<IResult> GetAvailableContractTypeDetailSources(
    [FromBody] GetAvailableContractTypeDetailSourcesRequest request,
    CT ct)
    {
        var result = await _logic.GetAvailableContractTypeDetailSources(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractFinancialInformation Queries

    [HttpPost("GetContractFinancialInformation")]
    [ResponseSchema<GetContractFinancialInformationResponse>]
    public async Task<IResult> GetContractFinancialInformation(
        [FromBody] GetContractFinancialInformationRequest request,
        CT ct)
    {
        var result = await _logic.GetContractFinancialInformation(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractAdjustmentConfiguration Queries

    [HttpPost("GetContractAdjustmentConfiguration")]
    [ResponseSchema<GetContractAdjustmentConfigurationResponse>]
    public async Task<IResult> GetContractAdjustmentConfiguration(
        [FromBody] GetContractAdjustmentConfigurationRequest request, CT ct)
    {
        var result = await _logic.GetContractAdjustmentConfiguration(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractGuarantee Queries

    [HttpPost("GetContractGuaranteeById")]
    [ResponseSchema<GetContractGuaranteeByIdResponse>]
    public async Task<IResult> GetContractGuaranteeById(
        [FromBody] GetContractGuaranteeByIdRequest request, CT ct)
    {
        var result = await _logic.GetContractGuaranteeById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractGuarantees")]
    [ResponseSchema<GetContractGuaranteesResponse>]
    public async Task<IResult> GetContractGuarantees(
        [FromBody] GetContractGuaranteesRequest request, CT ct)
    {
        var result = await _logic.GetContractGuarantees(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractChange Queries

    [HttpPost("GetContractChangeById")]
    [ResponseSchema<GetContractChangeByIdResponse>]
    public async Task<IResult> GetContractChangeById(
        [FromBody] GetContractChangeByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetContractChangeById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractChanges")]
    [ResponseSchema<GetContractChangesResponse>]
    public async Task<IResult> GetContractChanges(
        [FromBody] GetContractChangesRequest request,
        CT ct)
    {
        var result = await _logic.GetContractChanges(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractChangeAvailableItems")]
    [ResponseSchema<GetContractChangeAvailableItemsResponse>]
    public async Task<IResult> GetContractChangeAvailableItems(
        [FromBody] GetContractChangeAvailableItemsRequest request,
        CT ct)
    {
        var result = await _logic.GetContractChangeAvailableItems(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region ContractForProcesVerbal Queries
    [HttpGet("GetContractForProcesVerbal")]
    [ResponseSchema<GetContractForProcesVerbalResponse>]
    public async Task<IResult> GetContractForProcesVerbal(
        [FromQuery] GetContractForProcesVerbalRequest request, CT ct)
    {
        _logger.LogInformation("GetContractsByProjectIdForProcesVerbal");
        var result = await _logic.GetContractForProcesVerbal(request, ct);
        return result.GetHttpResponse();
    }
    #endregion

    #region ContractAdjustmentReference Queries

    [HttpPost("GetContractAdjustmentReferenceById")]
    [ResponseSchema<GetContractAdjustmentReferenceByIdResponse>]
    public async Task<IResult> GetContractAdjustmentReferenceById(
        [FromBody] GetContractAdjustmentReferenceByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetContractAdjustmentReferenceById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractAdjustmentReferences")]
    [ResponseSchema<GetContractAdjustmentReferencesResponse>]
    public async Task<IResult> GetContractAdjustmentReferences(
        [FromBody] GetContractAdjustmentReferencesRequest request,
        CT ct)
    {
        var result = await _logic.GetContractAdjustmentReferences(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractAdjustmentIndexById")]
    [ResponseSchema<GetContractAdjustmentIndexByIdResponse>]
    public async Task<IResult> GetContractAdjustmentIndexById(
        [FromBody] GetContractAdjustmentIndexByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetContractAdjustmentIndexById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractAdjustmentIndexes")]
    [ResponseSchema<GetContractAdjustmentIndexesResponse>]
    public async Task<IResult> GetContractAdjustmentIndexes(
        [FromBody] GetContractAdjustmentIndexesRequest request,
        CT ct)
    {
        var result = await _logic.GetContractAdjustmentIndexes(request, ct);
        return result.GetHttpResponse();
    }

    #endregion

    #region Enum Queries

    [HttpPost("GetContractDurationUnitEnum")]
    [Description("Get enum values for contract duration units.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractDurationUnitEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractDurationUnitEnum");

        var result = EnumExtensions.GetEnums<ContractDurationUnit>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractChangeTypeEnum")]
    [Description("Get enum values for contract change types.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractChangeTypeEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractChangeTypeEnum");

        var result = EnumExtensions.GetEnums<ContractChangeType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractStatusEnum")]
    [Description("Get enum values for contract statuses.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractStatusEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractStatusEnum");

        var result = EnumExtensions.GetEnums<ContractStatus>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractTypeKindEnum")]
    [Description("Get enum values for contract type kinds.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractTypeKindEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractTypeKindEnum");

        var result = EnumExtensions.GetEnums<ContractTypeKind>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetPricingMethodEnum")]
    [Description("Get enum values for pricing methods.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetPricingMethodEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetPricingMethodEnum");

        var result = EnumExtensions.GetEnums<PricingMethod>(
            request,
            [(int)PricingMethod.CostPlus]);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractAdjustmentLimitTypeEnum")]
    [Description("Get enum values for contract adjustment limit types.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractAdjustmentLimitTypeEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractAdjustmentLimitTypeEnum");

        var result = EnumExtensions.GetEnums<ContractAdjustmentLimitType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractAdjustmentMethodEnum")]
    [Description("Get enum values for contract adjustment methods.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractAdjustmentMethodEnum(
        [FromBody] GetEnumsRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractAdjustmentMethodEnum");

        var result = EnumExtensions.GetEnums<ContractAdjustmentMethod>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetPrepaymentAmortizationMethodEnum")]
    [Description("Get enum values for prepayment amortization methods.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetPrepaymentAmortizationMethodEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetPrepaymentAmortizationMethodEnum");

        var result = EnumExtensions.GetEnums<PrepaymentAmortizationMethod>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractTypeDetailAdjustmentTypeEnum")]
    [Description("Get enum values for contract type detail adjustment types.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractTypeDetailAdjustmentTypeEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractTypeDetailAdjustmentTypeEnum");

        var result = EnumExtensions.GetEnums<ContractTypeDetailAdjustmentType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractAdjustmentPeriodEnum")]
    [Description("Get enum values for contract adjustment periods.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractAdjustmentPeriodEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractAdjustmentPeriodEnum");

        var result = EnumExtensions.GetEnums<ContractAdjustmentPeriod>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractGuaranteeTypeEnum")]
    [Description("Get enum values for contract guarantee types.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractGuaranteeTypeEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractGuaranteeTypeEnum");

        var result = EnumExtensions.GetEnums<ContractGuaranteeType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractGuaranteeStatusEnum")]
    [Description("Get enum values for contract guarantee statuses.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractGuaranteeStatusEnum([FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetContractGuaranteeStatusEnum");

        var result = EnumExtensions.GetEnums<ContractGuaranteeStatus>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetContractAdjustmentCurrencyReferenceTypeEnum")]
    [Description("Get enum values for contract adjustment currency reference types.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetContractAdjustmentCurrencyReferenceTypeEnum(
        [FromBody] GetEnumsRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractAdjustmentCurrencyReferenceTypeEnum");

        var result = EnumExtensions.GetEnums<ContractAdjustmentCurrencyReferenceType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    #endregion

}

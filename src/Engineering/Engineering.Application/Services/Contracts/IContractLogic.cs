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

namespace Engineering.Application.Services.Contracts;

public interface IContractLogic
{
    #region Contract Commands

    Task<Result<CreateContractResponse?>> CreateContract(CreateContractRequest request, CT ct);
    Task<Result<CreateContractRegistrationResponse?>> CreateContractRegistration(CreateContractRegistrationRequest request, CT ct);
    Task<Result<UpdateContractRegistrationResponse?>> UpdateContractRegistration(UpdateContractRegistrationRequest request, CT ct);
    Task<Result<FinalizeContractRegistrationResponse?>> FinalizeContractRegistration(FinalizeContractRegistrationRequest request, CT ct);
    Task<Result<UpdateContractResponse?>> UpdateContract(UpdateContractRequest request, CT ct);
    Task<Result<DeleteContractResponse?>> DeleteContract(DeleteContractRequest request, CT ct);
    Task<Result<ChangeContractStatusResponse?>> ChangeContractStatus(ChangeContractStatusRequest request, CT ct);
    Task<Result<UpdateContractStructureResponse?>> UpdateContractStructure(UpdateContractStructureRequest request, CT ct);

    #endregion

    #region ContractType Commands

    Task<Result<CreateContractTypeResponse?>> CreateContractType(CreateContractTypeRequest request, CT ct);
    Task<Result<UpdateContractTypeResponse?>> UpdateContractType(UpdateContractTypeRequest request, CT ct);
    Task<Result<DeleteContractTypeResponse?>> DeleteContractType(DeleteContractTypeRequest request, CT ct);

    #endregion

    #region ContractTypeDetail Commands

    Task<Result<CreateContractTypeDetailResponse?>> CreateContractTypeDetail(CreateContractTypeDetailRequest request, CT ct);
    Task<Result<UpdateContractTypeDetailResponse?>> UpdateContractTypeDetail(UpdateContractTypeDetailRequest request, CT ct);
    Task<Result<DeleteContractTypeDetailResponse?>> DeleteContractTypeDetail(DeleteContractTypeDetailRequest request, CT ct);

    #endregion

    #region ContractFinancialInformation Commands

    Task<Result<CreateContractFinancialInformationResponse?>> CreateContractFinancialInformation(CreateContractFinancialInformationRequest request, CT ct);
    Task<Result<UpdateContractFinancialInformationResponse?>> UpdateContractFinancialInformation(UpdateContractFinancialInformationRequest request, CT ct);

    #endregion

    #region ContractChange Commands

    Task<Result<CreateContractChangeResponse?>> CreateContractChange(CreateContractChangeRequest request, CT ct);
    Task<Result<CreateContractSummaryChangeResponse?>> CreateContractSummaryChange(CreateContractSummaryChangeRequest request, CT ct);
    Task<Result<UpdateContractChangeResponse?>> UpdateContractChange(UpdateContractChangeRequest request, CT ct);
    Task<Result<UpdateContractSummaryChangeResponse?>> UpdateContractSummaryChange(UpdateContractSummaryChangeRequest request, CT ct);
    Task<Result<DeleteContractChangeResponse?>> DeleteContractChange(DeleteContractChangeRequest request, CT ct);

    #endregion

    #region ContractGuarantee Commands

    Task<Result<CreateContractGuaranteeResponse?>> CreateContractGuarantee(CreateContractGuaranteeRequest request, CT ct);
    Task<Result<UpdateContractGuaranteeResponse?>> UpdateContractGuarantee(UpdateContractGuaranteeRequest request, CT ct);
    Task<Result<DeleteContractGuaranteeResponse?>> DeleteContractGuarantee(DeleteContractGuaranteeRequest request, CT ct);
    Task<Result<ChangeContractGuaranteeStatusResponse?>> ChangeContractGuaranteeStatus(ChangeContractGuaranteeStatusRequest request, CT ct);

    #endregion

    #region ContractAdjustmentConfiguration Commands

    Task<Result<CreateContractAdjustmentConfigurationResponse?>> CreateContractAdjustmentConfiguration(CreateContractAdjustmentConfigurationRequest request, CT ct);
    Task<Result<UpdateContractAdjustmentConfigurationResponse?>> UpdateContractAdjustmentConfiguration(UpdateContractAdjustmentConfigurationRequest request, CT ct);
    Task<Result<DeleteContractAdjustmentConfigurationResponse?>> DeleteContractAdjustmentConfiguration(DeleteContractAdjustmentConfigurationRequest request, CT ct);

    #endregion

    #region ContractAdjustmentReference Commands

    Task<Result<CreateContractAdjustmentReferenceResponse?>> CreateContractAdjustmentReference(
        CreateContractAdjustmentReferenceRequest request, CT ct);
    Task<Result<UpdateContractAdjustmentReferenceResponse?>> UpdateContractAdjustmentReference(
        UpdateContractAdjustmentReferenceRequest request, CT ct);
    Task<Result<ChangeContractAdjustmentReferenceStateResponse?>> ChangeContractAdjustmentReferenceState(
        ChangeContractAdjustmentReferenceStateRequest request, CT ct);
    Task<Result<CreateContractAdjustmentIndexResponse?>> CreateContractAdjustmentIndex(
        CreateContractAdjustmentIndexRequest request, CT ct);
    Task<Result<UpdateContractAdjustmentIndexResponse?>> UpdateContractAdjustmentIndex(
        UpdateContractAdjustmentIndexRequest request, CT ct);
    Task<Result<ChangeContractAdjustmentIndexStateResponse?>> ChangeContractAdjustmentIndexState(
        ChangeContractAdjustmentIndexStateRequest request, CT ct);

    #endregion

    #region Contract Queries

    Task<Result<GetContractByIdResponse?>> GetContractById(GetContractByIdRequest request, CT ct);
    Task<Result<GetContractRegistrationByIdResponse?>> GetContractRegistrationById(GetContractRegistrationByIdRequest request, CT ct);
    Task<Result<GetContractRegistrationGridResponse?>> GetContractRegistrationGrid(GetContractRegistrationGridRequest request, CT ct);
    Task<Result<GetFilteredContractsResponse?>> GetFilteredContracts(GetFilteredContractsRequest request, CT ct);
    Task<Result<GetContractsByStatusResponse?>> GetContractsByStatus(GetContractsByStatusRequest request, CT ct);
    Task<Result<GetContractStructureResponse?>> GetContractStructure(GetContractStructureRequest request, CT ct);

    #endregion

    #region ContractType and ContractTypeDetail Queries

    Task<Result<GetContractTypeByIdResponse?>> GetContractTypeById(GetContractTypeByIdRequest request, CT ct);
    Task<Result<GetContractTypeDetailByIdResponse?>> GetContractTypeDetailById(GetContractTypeDetailByIdRequest request, CT ct);
    Task<Result<GetContractTypeDetailsResponse?>> GetContractTypeDetails(GetContractTypeDetailsRequest request, CT ct);
    Task<Result<GetAvailableContractTypeDetailSourcesResponse?>> GetAvailableContractTypeDetailSources(GetAvailableContractTypeDetailSourcesRequest request, CT ct);

    #endregion

    #region ContractFinancialInformation Queries

    Task<Result<GetContractFinancialInformationResponse?>> GetContractFinancialInformation(GetContractFinancialInformationRequest request, CT ct);

    #endregion

    #region ContractChange Queries

    Task<Result<GetContractChangeByIdResponse?>> GetContractChangeById(GetContractChangeByIdRequest request, CT ct);
    Task<Result<GetContractChangesResponse?>> GetContractChanges(GetContractChangesRequest request, CT ct);
    Task<Result<GetContractChangeAvailableItemsResponse?>> GetContractChangeAvailableItems(GetContractChangeAvailableItemsRequest request, CT ct);

    #endregion

    #region ContractGuarantee Queries

    Task<Result<GetContractGuaranteeByIdResponse?>> GetContractGuaranteeById(GetContractGuaranteeByIdRequest request, CT ct);
    Task<Result<GetContractGuaranteesResponse?>> GetContractGuarantees(GetContractGuaranteesRequest request, CT ct);

    #endregion

    #region ContractAdjustmentConfiguration Queries

    Task<Result<GetContractAdjustmentConfigurationResponse?>> GetContractAdjustmentConfiguration(GetContractAdjustmentConfigurationRequest request, CT ct);

    #endregion

    #region ContractAdjustmentReference Queries

    Task<Result<GetContractAdjustmentReferenceByIdResponse?>> GetContractAdjustmentReferenceById(
        GetContractAdjustmentReferenceByIdRequest request, CT ct);
    Task<Result<GetContractAdjustmentReferencesResponse?>> GetContractAdjustmentReferences(
        GetContractAdjustmentReferencesRequest request, CT ct);
    Task<Result<GetContractAdjustmentIndexByIdResponse?>> GetContractAdjustmentIndexById(
        GetContractAdjustmentIndexByIdRequest request, CT ct);
    Task<Result<GetContractAdjustmentIndexesResponse?>> GetContractAdjustmentIndexes(
        GetContractAdjustmentIndexesRequest request, CT ct);

    #endregion

    #region ContractForProcesVerbal Queries
    Task<Result<List<GetContractForProcesVerbalResponse>>> GetContractForProcesVerbal(
    GetContractForProcesVerbalRequest request, CT ct);
    #endregion

}

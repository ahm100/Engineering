using Engineering.Application.Services.ProjectOperationDetails.Commands.CreateVizardProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.CreateVizardProjectOperationDetail;

public record CreateVizardProjectOperationDetailRequestModel(
    List<long> ProjectOperationIds,
    DateTime? StartDate,
    DateTime? EndDate,
    decimal Length,
    bool LengthChangeable,
    decimal Width,
    bool WidthChangeable,
    decimal Height,
    bool HeightChangeable,
    decimal Weight,
    bool WeightChangeable,
    decimal Number,
    bool NumberChangeable,
    int Status,
    int Priority,
    int? Day,
    int? Hour,
    long? CreatedProductId,
    string? Description,
    List<long>? PlannerRequests,
    List<long>? ImplementationAssistantRequests,
    List<long>? TechnicalAssistantRequests,
    List<CreateContractorServiceRequestModel>? ContractorServiceRequests,
    List<CreateExpertRequestModel>? ExpertRequests,
    List<ContractorExpertLinkModel>? ContractorExpertLinks,
    List<CreateMachineryRequestModel>? MachineryRequests,
    List<CreateProductRequestModel>? ProductRequests,
    List<CreateDeductionRequestModel>? DeductionRequests,
    List<string>? Urls
    );
